//=-------
// Copyright 2003, Chris Anderson
// 
//   Provided as is, with no warrenty, etc.
//   License is granted to use, copy, modify, 
//   with or without credit to me, just don't
//   blame me if it doesn't work.
//=-------
namespace Anderson.Chris.BlogX.WindowsClient
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Windows.Forms;

    /// <summary>
    /// Summary description for HtmlViewer.
    /// </summary>
    public class HtmlViewer : Anderson.Chris.BlogX.WindowsClient.Html.HtmlControl
    {
        public HtmlViewer()
        {
        }

        public string Html
        {
            get
            {
                return SaveHtml();
            }
            set
            {
                LoadHtml(value);
            }
        }
    }
}
