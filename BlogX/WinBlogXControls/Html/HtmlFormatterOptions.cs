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
    public sealed class HtmlFormatterOptions 
    {
        private HtmlFormatterCase _elementCasing;
        private HtmlFormatterCase _attributeCasing;
        private bool _makeXhtml;
        private char _indentChar;
        private int _indentSize;
        private int _maxLineLength;

        public HtmlFormatterOptions(char indentChar, int indentSize, int maxLineLength, bool makeXhtml) : this(indentChar, indentSize, maxLineLength, HtmlFormatterCase.LowerCase, HtmlFormatterCase.LowerCase, makeXhtml) 
        {
        }

        public HtmlFormatterOptions(char indentChar, int indentSize, int maxLineLength, HtmlFormatterCase elementCasing, HtmlFormatterCase attributeCasing, bool makeXhtml) 
        {
            _indentChar = indentChar;
            _indentSize = indentSize;
            _maxLineLength = maxLineLength;
            _elementCasing = elementCasing;
            _attributeCasing = attributeCasing;
            _makeXhtml = makeXhtml;
        }

        public HtmlFormatterCase AttributeCasing 
        {
            get 
            {
                return _attributeCasing;
            }
        }

        public HtmlFormatterCase ElementCasing 
        {
            get 
            {
                return _elementCasing;
            }
        }

        public char IndentChar 
        {
            get 
            {
                return _indentChar;
            }
        }

        public int IndentSize 
        {
            get 
            {
                return _indentSize;
            }
        }

        public bool MakeXhtml 
        {
            get 
            {
                return _makeXhtml;
            }
        }    
    
        public int MaxLineLength 
        {
            get 
            {
                return _maxLineLength;
            }
        }
    }
}
