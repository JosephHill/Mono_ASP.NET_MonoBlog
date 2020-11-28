<%@ Register TagPrefix="uc1" TagName="BlogXFooter" Src="BlogXFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BlogXHeader" Src="BlogXHeader.ascx" %>
<%@ Page language="c#" Codebehind="CommentView.aspx.cs" AutoEventWireup="false" Inherits="Anderson.Chris.BlogX.WebClient.CommentView" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
    <HEAD>
        <title id="title">CommentView</title>
        <meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
        <meta name="CODE_LANGUAGE" Content="C#">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    </HEAD>
    <body>
        <uc1:BlogXHeader id="BlogXHeader1" runat="server"></uc1:BlogXHeader>
        <div id="content" runat="server" width="100%">
            <div runat="server" id="comments" class="date">
            </div>
            <div id="AddNew" class="date">
                <div class="comment">
                    <h3 class="commentTitle">Add New</h3>
                    <div class="commentBody">
                        <form id="CommentView" method="post" runat="server">
                            <p><asp:Label id="Label1" runat="server">Name</asp:Label>
                                <asp:TextBox id="name" runat="server"></asp:TextBox></p>
                            <p><asp:Label id="Label2" runat="server">Email</asp:Label>
                                <asp:TextBox id="email" runat="server"></asp:TextBox></p>
                            <p><asp:Label id="Label3" runat="server">Homepage</asp:Label>
                                <asp:TextBox id="homepage" runat="server"></asp:TextBox></p>
                            <div id="commentSpamGuard" runat="server">
                                <p><asp:Label id="Label6" runat="server">Security Word</asp:Label>
                                    <asp:Image id="securityWordImage" runat="server" /></p>
                                <p><asp:Label id="Label5" runat="server">Type in the security Word</asp:Label>
                                    <asp:TextBox id="securityWord" runat="server"></asp:TextBox></p>
                            </div>
                            <p><asp:CheckBox id="rememberMe" runat="server" Text="Remember Me" Checked="True"></asp:CheckBox></p>
                            <p><asp:Label id="Label4" runat="server">Content (HTML not allowed)</asp:Label></p>
                            <p><asp:TextBox Rows="12" Columns="40" id="comment" runat="server" TextMode="MultiLine"></asp:TextBox></p>
                            <p><asp:Button id="add" runat="server" Text="Add"></asp:Button></p>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <uc1:blogxfooter id="BlogXFooter1" runat="server"></uc1:blogxfooter>
    </body>
</HTML>
