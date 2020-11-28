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
namespace Anderson.Chris.BlogX.WindowsClient.Html.Formatting 
{

    using System;

    /// <summary>
    /// </summary>
    [Flags]
    internal enum FormattingFlags 
    {
        None = 0,
        Inline = 0x1,
        NoIndent = 0x2,
        NoEndTag =  0x4,
        PreserveContent = 0x8,
        Xml = 0x10,
        Comment = 0x20,
        AllowPartialTags = 0x40
    }
}
