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
    internal class TRTagInfo : TagInfo 
    {
        public TRTagInfo() : base("tr", FormattingFlags.None, WhiteSpaceType.NotSignificant, WhiteSpaceType.NotSignificant, ElementType.Other) 
        {
        }

        public override bool CanContainTag(TagInfo info) 
        {
            if (info.Type == ElementType.Any) 
            {
                return true;
            }

            if ((info.TagName.ToLower().Equals("th")) |
                (info.TagName.ToLower().Equals("td"))) 
            {
                return true;
            }

            return false;
        }
    }
}
