<%@ Page language="c#" Codebehind="EntryEdit.aspx.cs" AutoEventWireup="false" Inherits="Anderson.Chris.BlogX.WebClient.EntryEdit" %>
<%@ Register TagPrefix="uc1" TagName="BlogXFooter" Src="BlogXFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BlogXHeader" Src="BlogXHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
    <HEAD>
        <title>Entry Edit</title>
        <meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
        <meta name="CODE_LANGUAGE" Content="C#">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <LINK rel="stylesheet" type="text/css" href="SiteConfig/blogx.css">
    </HEAD>
    <body>
        <form id="EntryEdit" method="post" runat="server">
            <P>
                <uc1:BlogXHeader id="BlogXHeader1" runat="server"></uc1:BlogXHeader></P>
            <P>
                <asp:Label id="Label1" runat="server">Title:</asp:Label>
                <asp:TextBox id="entryTitle" runat="server" Width="80%"></asp:TextBox>
            </P>
            <P>
                <asp:Label id="Label2" runat="server">Description:</asp:Label>
                <asp:TextBox id="entryAbstract" runat="server" Width="80%"></asp:TextBox>
            </P>
            <P>
                HTML Content:
                <asp:TextBox TextMode="MultiLine" id="entryContent" runat="server" Width="100%" DESIGNTIMEDRAGDROP="96" Height="10em">
                </asp:TextBox>
            </P>
            Categories (semi-colon delimited):<asp:TextBox Width="60%" id="entryCategories" runat="server"></asp:TextBox>
            <P></P>
            <asp:Button id="save" runat="server" Text="Save"></asp:Button>
        </form>
        <uc1:BlogXFooter id="BlogXFooter1" runat="server"></uc1:BlogXFooter>
        <P></P>
    </body>
</HTML>
