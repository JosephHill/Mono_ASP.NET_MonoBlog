//=-------
// Copyright 2003, Chris Anderson
//
//  
//   Original source code by Joe Beda
//
//
//   Provided as is, with no warrenty, etc.
//   License is granted to use, copy, modify, 
//   with or without credit to me, just don't
//   blame me if it doesn't work.
//=-------
namespace Anderson.Chris.BlogX.Runtime
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.IO;
    using System.Threading;

    public delegate void UploadCallback(float percentUpload);

    public class FtpConnection : IDisposable
    {
        public FtpConnection(string server, int port, string userName, string password)
        {
            this.server = server;
            this.port = port;
            this.userName = userName;
            this.password = password;

            OpenControlSocket();
        }

        public void Close()
        {
            if (socketControl != null)
            {
                SendCommand("QUIT", null);
                RecieveControlResponse();
                socketControl.Shutdown(SocketShutdown.Both);
                socketControl.Close();
                socketControl = null;
            }
        }

        #region Implementation of IDisposable
        public void Dispose()
        {
            Close();
        }
        #endregion  

        public bool IsOpen { get { return socketControl != null; } }

        public bool UsePassive { get { return usePassive; } set { usePassive = value; } }

        public string List()
        {
            Socket localDataSocket = null;

            OpenDataSocket();

            try
            {
                SendCommand("TYPE", "A");
                ControlResponse response = RecieveControlResponse();

                SendCommand("LIST", "-aL");
                response = RecieveControlResponse();
                if(!response.PositivePreliminary)
                    throw new Exception("Error returned by error for LIST command");

                localDataSocket = ActivateDataSocket();

                Byte[] recvBuffer = new Byte[256];

                StringBuilder sbData = new StringBuilder();

                while(true)
                {
                    int bytesRead = localDataSocket.Receive(recvBuffer);
                    if (bytesRead <= 0)
                        break;
                    sbData.Append(Encoding.ASCII.GetString(recvBuffer, 0, bytesRead));                    
                }

                Trace.Write(sbData.ToString(), "FTPDataResponse");

                return sbData.ToString();
            }
            finally
            {
                CloseDataSocket(localDataSocket);
            }
        }

        public void EnsureDirectory(string dirName)
        {
            if(dirName == null || dirName.Length == 0 || dirName[0] != '/')
                throw new ArgumentException("Bad directory passed to dirName", dirName);

            string[] directories = dirName.Split('/');

            ControlResponse response;

            directories[0] = "/";

            foreach(string dir in directories)
            {
                // Try to CWD into the dir
                SendCommand("CWD", dir);
                response = RecieveControlResponse();
                if (!response.PositiveCompletion)
                {
                    // Couldn't CWD to directory, try to make it
                    SendCommand("MKD", dir);
                    response = RecieveControlResponse();
                    if (!response.PositiveCompletion)
                        throw new Exception("Could not create or change to directory");

                    // According to the RFC, I should parse the response from MKD and
                    // actually CD into that, but no systems I'm going to be connecting to
                    // will require this
                    SendCommand("CWD", dir);
                    response = RecieveControlResponse();
                    if (!response.PositiveCompletion)
                        throw new Exception("Could not change to directory after creating it...");
                }
            }
        }

        public void Put(string fileName, string remoteFile, UploadCallback callback)
        {
            Socket localDataSocket = null;

            OpenDataSocket();

            try
            {
                SendCommand("TYPE", "I");
                ControlResponse response = RecieveControlResponse();

                SendCommand("STOR", remoteFile);
                response = RecieveControlResponse();
                if(!response.PositivePreliminary)
                    throw new Exception("Error returned by error for LIST command");

                localDataSocket = ActivateDataSocket();

                Byte[] sendBuffer = new Byte[8096];
                int totalSizeRead = 0;
                using(FileStream strm = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    int sizeRead;
                    while(0 != (sizeRead = strm.Read(sendBuffer,0, 8096)))
                    {
                        totalSizeRead += sizeRead;
                        localDataSocket.Send(sendBuffer, 0, sizeRead, SocketFlags.None);
                        if (callback != null)
                            callback((float)totalSizeRead/(float)strm.Length);

                        Trace.WriteLine(String.Format("Writing file, {0:f2}% complete.", ((float)totalSizeRead/(float)strm.Length)*100.0f), "FTPDataResponse");
                    }
                }

                CloseDataSocket(localDataSocket);
                localDataSocket = null;

                response = RecieveControlResponse();

                Trace.WriteLine("File written successfully", "FTPDataResponse");
            }
            finally
            {
                CloseDataSocket(localDataSocket);
            }
        }

        private void OpenControlSocket()
        {
            Debug.Assert(socketControl == null);

            socketControl = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                IPAddress hostAddr = Dns.Resolve(server).AddressList[0];
                IPEndPoint endpointHost = new IPEndPoint(hostAddr, port);

                socketControl.Connect(endpointHost);
                if(!socketControl.Connected)
                {
                    throw new Exception("Could not connect to FTP host");
                }
            }
            catch (Exception)
            {
                socketControl.Close();
                socketControl = null;
                throw;
            }

            RecieveControlResponse();
            
            SendCommand("USER", userName);
            ControlResponse response = RecieveControlResponse();
            if (response.SCode != 331)
            {
                throw new Exception("USER command rejected");
            }

            SendCommand("PASS", password);
            response = RecieveControlResponse();
            if (response.SCode != 230)
            {
                throw new Exception("PASS command rejected");
            }
        }

        private ControlResponse RecieveControlResponse()
        {
            int sCode = -1;
            Byte[] recvBuffer = new Byte[256];

            StringBuilder sbResponse = new StringBuilder();

            while(true)
            {
                int bytesRead = socketControl.Receive(recvBuffer);
                sbResponse.Append(Encoding.ASCII.GetString(recvBuffer, 0, bytesRead));
                sCode = AnalyzeResponse(sbResponse.ToString());
                if (sCode != -1)
                {
                    ControlResponse ret = new FtpConnection.ControlResponse(sCode, sbResponse.ToString());
                    Trace.Write(ret.Response, "FTPResponse");
                    return ret;
                }
            }
        }

        private int AnalyzeResponse(string response)
        {
            int sCode = AnalyzeResponseLine(response, true);

            if (sCode == -1)
                return -1;

            if (response[3] == ' ')
            {
                return sCode;
            }
            else if (response[3] != '-')
            {
                throw new Exception("Bad response from FTP server");
            }

            // We have a multiline response.  Find the beginning of the last line.
            string[] lines = GetResponseLines(response);
            if (lines.Length > 1)
            {
                string lastLine = GetLastNonBlankLine(lines);
                int sCodeLastLine = AnalyzeResponseLine(lastLine, false);
                if (lastLine[3] == ' ' && sCodeLastLine != -1 && sCodeLastLine == sCode)
                    return sCode;
            }

            return -1;
        }

        string GetLastNonBlankLine(string[] lines)
        {
            for (int i=lines.Length-1; i>=0; i--)
            {
                if (lines[i].Length > 0) return lines[i];
            }
            return null;
        }

        string[] GetResponseLines(string response)
        {
            string[] lines = response.Split('\r');
            for (int i=0; i<lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
            }
            return lines;
        }

        private int AnalyzeResponseLine(string response, bool checkCrLf)
        {
            int sCode = -1;

            // Minimum length is XYZ[SP][CR][LF]
            if (response.Length < 6)
                return sCode;

            if (checkCrLf && !response.EndsWith("\r\n"))
                return sCode;

            for (int i=0; i<3; i++)
            {
                if (!Char.IsDigit(response, i))
                    return sCode;
            }

            sCode = Int16.Parse(response.Substring(0, 3));
            
            return sCode;
        }

        private void SendCommand(string method, string arguments)
        {
            Debug.Assert(socketControl != null);

            string command = method;
            if (arguments != null && arguments.Length > 0)
                command = method + " " + arguments;
            command += "\r\n";
            Trace.Write(command, "FTPCommand");
            int cbSent = socketControl.Send(Encoding.ASCII.GetBytes(command));
        }

        private void OpenDataSocket()
        {
            if (usePassive)
            {
                OpenPassiveDataSocket();
            }
            else
            {
                OpenRegularDataSocket();
            }
        }

        private Socket ActivateDataSocket()
        {
            if (usePassive)
            {
                return socketData;
            }
            else
            {
                return socketData.Accept();
            }
        }

        private void CloseDataSocket(Socket localSocket)
        {
            if (localSocket == socketData)
            {
                localSocket = null;
            }

            if (localSocket != null)
            {               
                localSocket.Shutdown(SocketShutdown.Both);
                localSocket.Close();
            }
            if (socketData != null)
            {
                socketData.Close();
            }
            socketData = null;
        }

        private void OpenRegularDataSocket()
        {
            Debug.Assert(socketControl != null);
            Debug.Assert(socketData == null);

            try
            {
                IPEndPoint epLocalData;
                
                socketData = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                epLocalData = new IPEndPoint(((IPEndPoint)socketControl.LocalEndPoint).Address.Address, 0);
                
                socketData.Bind(epLocalData);
                socketData.Listen(1);

                epLocalData = (IPEndPoint)socketData.LocalEndPoint;
                String stringAddress = FormatAddress(epLocalData.Address.Address, epLocalData.Port);

                SendCommand("PORT", stringAddress);
                ControlResponse response = RecieveControlResponse();
                if (!response.PositiveCompletion)
                {
                    throw new Exception("Server returned error when setting up regular data connection.");
                }
            }
            catch (Exception)
            {
                if (socketData != null)
                    socketData.Close();
                socketData = null;
                throw;
            }

        }

        private string FormatAddress(long ipAddress, int port)
        {
            StringBuilder sb = new StringBuilder(32);

            UInt32 dwAddress = (UInt32)ipAddress;

            for (int i=0; i < 4; i++)
            {
                sb.Append(dwAddress & 0xFF);
                sb.Append(',');
                dwAddress = dwAddress >> 8;
            }

            sb.Append(port >> 8);
            sb.Append(',');
            sb.Append(port & 0xFF);

            return sb.ToString();
        }

        private void OpenPassiveDataSocket()
        {
            Debug.Assert(socketControl != null);
            Debug.Assert(socketData == null);

            Thread.Sleep(10);
            SendCommand("PASV", null);
            ControlResponse response = RecieveControlResponse();
            if (!response.PositiveCompletion)
            {
                throw new Exception("Server returned error when requesting passive connection.");
            }

            IPEndPoint dataEndPoint = ParsePassiveIPAddress(response.Response);

            Trace.WriteLine("Opening passive data connection to: "+dataEndPoint.ToString());

            try
            {
                socketData = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socketData.Connect(dataEndPoint);                
            }
            catch (Exception)
            {
                if (socketData != null)
                    socketData.Close();
                socketData = null;
                throw;
            }
        }

        private IPEndPoint ParsePassiveIPAddress(string response)
        {
            int idxStart = response.IndexOf('(');
            int idxEnd = response.IndexOf(')');
            if (idxStart <= 0 || idxEnd <= 0 || idxStart > idxEnd)
                throw new Exception("Bad response from server when entering passive mode");

            string addressString = response.Substring(idxStart+1, idxEnd - idxStart - 1);
            string [] addressStringList = addressString.Split(',');
            
            long address = 0;

            for (int i = 3; i >= 0; i--)
            {
                address <<= 8;
                address += Int16.Parse(addressStringList[i]);
            }

            int port = 0;
            for (int i = 4; i < 6; i++)
            {
                port <<= 8;
                port += Int16.Parse(addressStringList[i]);
            }

            return new IPEndPoint(address, port);
        }

        private string server;
        private int port;
        private string userName;
        private string password;
        private Socket socketData;
        private Socket socketControl;
        private bool usePassive = true;

        private struct ControlResponse
        {
            public ControlResponse(int sCode, string response)
            {
                this.sCode = sCode;
                this.response = response;                
            }

            public int SCode { get { return sCode; } }
            public string Response { get { return response; } }

            public bool PositivePreliminary 
            { get { return ( sCode / 100 == 1); } }

            public bool PositiveCompletion 
            { get { return ( sCode / 100 == 2); } }

            public bool PositiveIntermediate 
            { get { return ( sCode / 100 == 3); } }
    
            public bool TransientNegativeCompletion 
            { get { return ( sCode / 100 == 4); } }

            public bool PermanentNegativeCompletion 
            { get { return ( sCode / 100 == 5); } }
            
            int sCode;
            string response;
        }
    }
}

