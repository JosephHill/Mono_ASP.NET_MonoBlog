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

    using System.Diagnostics;

    /// <summary>
    /// </summary>
    internal class TagInfo 
    {
        private string _tagName;
        private WhiteSpaceType _inner;
        private WhiteSpaceType _following;
        private FormattingFlags _flags;
        private ElementType _type;

        public TagInfo(string tagName, FormattingFlags flags) : this(tagName, flags, WhiteSpaceType.CarryThrough, WhiteSpaceType.CarryThrough, ElementType.Other) 
        {
        }

        public TagInfo(string tagName, FormattingFlags flags, ElementType type) : this(tagName, flags, WhiteSpaceType.CarryThrough, WhiteSpaceType.CarryThrough, type) 
        {
        }

        public TagInfo(string tagName, FormattingFlags flags, WhiteSpaceType innerWhiteSpace, WhiteSpaceType followingWhiteSpace) : this(tagName, flags, innerWhiteSpace, followingWhiteSpace, ElementType.Other) 
        {
        }

        public TagInfo(string tagName, FormattingFlags flags, WhiteSpaceType innerWhiteSpace, WhiteSpaceType followingWhiteSpace, ElementType type) 
        {
            Debug.Assert((innerWhiteSpace == WhiteSpaceType.NotSignificant) ||
                (innerWhiteSpace == WhiteSpaceType.Significant) ||
                (innerWhiteSpace == WhiteSpaceType.CarryThrough), "Invalid whitespace type");

            Debug.Assert((followingWhiteSpace == WhiteSpaceType.NotSignificant) ||
                (followingWhiteSpace == WhiteSpaceType.Significant) ||
                (followingWhiteSpace == WhiteSpaceType.CarryThrough), "Invalid whitespace type");

            _tagName = tagName;
            _inner = innerWhiteSpace;
            _following = followingWhiteSpace;
            _flags = flags;
            _type = type;
        }

        public TagInfo(string newTagName, TagInfo info) 
        {
            _tagName = newTagName;
            _inner = info.InnerWhiteSpaceType;
            _following = info.FollowingWhiteSpaceType;
            _flags = info.Flags;
            _type = info.Type;
        }

        public ElementType Type 
        {
            get 
            {
                return _type;
            }
        }

        public FormattingFlags Flags 
        {
            get 
            {
                return _flags;
            }
        }

        public WhiteSpaceType FollowingWhiteSpaceType 
        {
            get 
            {
                return _following;
            }
        }

        public WhiteSpaceType InnerWhiteSpaceType 
        {
            get 
            {
                return _inner;
            }
        }

        public bool IsComment 
        {
            get 
            {
                return ((_flags & FormattingFlags.Comment) != 0);
            }
        }

        public bool IsInline 
        {
            get 
            {
                return ((_flags & FormattingFlags.Inline) != 0);
            }
        }

        public bool IsXml 
        {
            get 
            {
                return ((_flags & FormattingFlags.Xml) != 0);
            }
        }

        public bool NoEndTag 
        {
            get 
            {
                return ((_flags & FormattingFlags.NoEndTag) != 0);
            }
        }

        public bool NoIndent 
        {
            get 
            {
                return (((_flags & FormattingFlags.NoIndent) != 0) || NoEndTag);
            }
        }

        public bool PreserveContent 
        {
            get 
            {
                return ((_flags & FormattingFlags.PreserveContent) != 0);
            }
        }

        public string TagName 
        {
            get 
            {
                return _tagName;
            }
        }

        public virtual bool CanContainTag(TagInfo info) 
        {
            return true;
        }
    }
}
