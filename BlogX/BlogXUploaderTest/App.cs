//=-------
// Copyright 2003, Chris Anderson
// 
//   Provided as is, with no warrenty, etc.
//   License is granted to use, copy, modify, 
//   with or without credit to me, just don't
//   blame me if it doesn't work.
//=-------
namespace Anderson.Chris.BlogX.UploaderTest
{
    using System;
    using Anderson.Chris.BlogX.Runtime;

    class App
    {
        static void Main(string[] args)
        {
            FtpUpstreamer up = new FtpUpstreamer();
            up.FtpServer = "www.urbanpotato.net";
            up.FtpServerRootPath = "/temp";
            up.FtpServerUserName = "Ftpuser";
            up.LocalRoot = @"c:\temp\upstream";
            up.FtpServerPassword = args[0];
            up.UpstreamerStateFile = @"c:\temp\state.file";
            up.FtpUsePassive = false;
            up.FileAction += new FileActionEventHandler(FileAction);
            up.Upstream();
        }
        static void FileAction(object sender, FileActionEventArgs e)
        {
            Console.WriteLine("{1}: '{0}'", e.File, e.Action);
        }
    }
}
