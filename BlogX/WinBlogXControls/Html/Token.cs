//=-------
// Copyright 2003, Microsoft Coporation
//
// Original source code by Nikhil Kothari
// 
// Integrated into BlogX by Chris Anderson
//
//   Provided as is, with no warrenty, etc.
//   License is granted to use, copy, modify, 
//   with or without credit to me, just don't
//   blame me if it doesn't work.
//=-------
namespace Anderson.Chris.BlogX.WindowsClient.Html 
{

    using System;

    /// <summary>
    /// </summary>
    public class Token 
    {
        public const int Whitespace = 0;
        public const int TagName = 1;
        public const int AttrName = 2;
        public const int AttrVal = 3;
        public const int TextToken = 4;
        public const int SelfTerminating = 5;
        public const int Empty = 6;
        public const int Comment = 7;
        public const int Error = 8;

        public const int OpenBracket = 10;
        public const int CloseBracket = 11;
        public const int ForwardSlash = 12;
        public const int DoubleQuote = 13;
        public const int SingleQuote = 14;
        public const int EqualsChar = 15;

        public const int ClientScriptBlock = 20;
        public const int Style = 21;
        public const int InlineServerScript = 22;
        public const int ServerScriptBlock = 23;
        public const int XmlDirective = 24;

        private int _type;
        private char[] _chars;
        private int _charsLength;
        private string _text;
        private int _startIndex;
        private int _endIndex;
        private int _endState;

        public Token(int type, int endState, int startIndex, int endIndex, char[] chars, int charsLength) 
        {
            _type = type;
            _chars = chars;
            _charsLength = charsLength;
            _startIndex = startIndex;
            _endIndex = endIndex;
            _endState = endState;
        }

        internal char[] Chars 
        {
            get 
            {
                return _chars;
            }
        }

        internal int CharsLength 
        {
            get 
            {
                return _charsLength;
            }
        }

        public int EndIndex 
        {
            get 
            {
                return _endIndex;
            }
        }

        public int EndState 
        {
            get 
            {
                return _endState;
            }
        }

        public int Length 
        {
            get 
            {
                return _endIndex - _startIndex;
            }
        }

        public int StartIndex 
        {
            get 
            {
                return _startIndex;
            }
        }

        public string Text 
        {
            get 
            {
                if (_text == null) 
                {
                    _text = new String(_chars, StartIndex, EndIndex - StartIndex);
                }
                return _text;
            }
        }

        public int Type 
        {
            get 
            {
                return _type;
            }
        }

#if DEBUG
        public override string ToString() 
        {
            string s = "\'" + Text + "\'";
            switch (Type) 
            {
                case Whitespace:
                    s += "(Whitespace)";
                    break;
                case ForwardSlash:
                    s += "(ForwardSlash)";
                    break;
                case DoubleQuote:
                    s += "(DoubleQuote)";
                    break;
                case SingleQuote:
                    s += "(SingleQuote)";
                    break;
                case OpenBracket:
                    s += "(OpenBracket)";
                    break;
                case CloseBracket:
                    s += "(CloseBracket)";
                    break;
                case EqualsChar:
                    s += "(Equals)";
                    break;
                case TagName:
                    s += "(Tag)";
                    break;
                case AttrName:
                    s += "(AttrName)";
                    break;
                case AttrVal:
                    s += "(AttrVal)";
                    break;
                case TextToken:
                    s += "(Text)";
                    break;
                case SelfTerminating:
                    s += "(SelfTerm)";
                    break;
                case Empty:
                    s += "(Empty)";
                    break;
                case Comment:
                    s += "(Comment)";
                    break;
                case Error:
                    s += "(Error)";
                    break;
                case ClientScriptBlock:
                    s += "(ClientScriptBlock)";
                    break;
                case Style:
                    s += "(Style)";
                    break;
                case InlineServerScript:
                    s += "(InlineServerScript)";
                    break;
                case ServerScriptBlock:
                    s += "(ServerScriptBlock)";
                    break;
            }

            return s;
        }
#endif //DEBUG
    }
}
