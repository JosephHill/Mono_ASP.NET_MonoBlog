//=-------
// Copyright 2003, Chris Anderson
// 
//   Provided as is, with no warrenty, etc.
//   License is granted to use, copy, modify, 
//   with or without credit to me, just don't
//   blame me if it doesn't work.
//=-------
namespace Anderson.Chris.BlogX.WebClient
{
    using System;
    using System.Text.RegularExpressions;
    using System.IO;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.SessionState;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using Anderson.Chris.BlogX.Runtime;

    /// <summary>
    /// Summary description for WebForm1.
    /// </summary>
    public class Referrers : System.Web.UI.Page
    {
        protected System.Web.UI.HtmlControls.HtmlGenericControl content;
    
        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
            }

        }

		#region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }
		
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {    
            this.Load += new System.EventHandler(this.Page_Load);
            this.PreRender += new System.EventHandler(this.WebForm1_PreRender);

        }
		#endregion

        #region RefItem (used for sorting the table)
        class RefItem
        {
            internal string url;
            internal int count;
            internal RefItem(string url, int count)
            {
                this.url = url;
                this.count = count;
            }

            public class Comparer : IComparer
            {
                public int Compare(object left, object right)
                {
                    int leftValue = ((RefItem)left).count;
                    int rightValue = ((RefItem)right).count;
                    if (leftValue > rightValue)
                    {
                        return -1;
                    }
                    else if (leftValue < rightValue)
                    {
                        return 1;
                    }
                    return 0;
                }
            }
        }
        #endregion

        private void WebForm1_PreRender(object sender, System.EventArgs e)
        {
            Control root = content;

            SiteConfig config = SiteConfig.GetSiteConfig();
            string siteRoot = config.Root.ToUpper();

            Hashtable data = new Hashtable();

            foreach (LogDataItem log in BlogXUtils.GetReferrerLog(DateTime.Now))
            {
                bool exclude = false;
                if (log.UrlReferrer != null)
                {
                    exclude = log.UrlReferrer.ToUpper().StartsWith(siteRoot);
                }
                if (!exclude)
                {
                    if (!data.Contains(log.UrlReferrer))
                    {
                        data[log.UrlReferrer] = 0;
                    }
                    data[log.UrlReferrer] = ((int)data[log.UrlReferrer]) + 1;
                }
            }

            ArrayList list = new ArrayList(data.Count);
            foreach (DictionaryEntry de in data)
            {
                list.Add(new RefItem(de.Key.ToString(), (int)de.Value));
            }

            list.Sort(new RefItem.Comparer());

            int total = 0;
            Table table = new Table();
            foreach (RefItem item in list)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell());
                row.Cells.Add(new TableCell());

                HyperLink link = new HyperLink();
                string text = item.url;
                if (text != null && text.Length > 0)
                {
                    if (text.Length > 40) 
                    {
                        text = text.Substring(0, 40) + "...";
                    }
                }
                else 
                {
                    text = "(none)";
                }
                link.Text = text;
                link.NavigateUrl = item.url.ToString();
                row.Cells[0].Controls.Add(link);
                row.Cells[1].Text = item.count.ToString();
                total += item.count;

                table.Rows.Add(row);
            }

            TableRow totalRow = new TableRow();
            totalRow.Cells.Add(new TableCell());
            totalRow.Cells.Add(new TableCell());
            totalRow.Cells[0].Text = "Total";
            totalRow.Cells[1].Text = total.ToString();
            table.Rows.Add(totalRow);

            root.Controls.Add(table);

        }
    }
}
