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
    internal class TDTagInfo : TagInfo 
    {
        public TDTagInfo() : base("td", FormattingFlags.None, WhiteSpaceType.NotSignificant, WhiteSpaceType.NotSignificant, ElementType.Other) 
        {
        }

        public override bool CanContainTag(TagInfo info) 
        {
            if (info.Type == ElementType.Any) 
            {
                return true;
            }

            if ((info.Type == ElementType.Inline) |
                (info.Type == ElementType.Block)) 
            {
                return true;
            }

            return false;
        }
    }

}
