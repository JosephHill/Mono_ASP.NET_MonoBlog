<%@ Register TagPrefix="uc1" TagName="BlogXHeader" Src="BlogXHeader.ascx" %>
<%@ Page language="c#" Codebehind="FormatPage.aspx.cs" AutoEventWireup="false" Inherits="Anderson.Chris.BlogX.WebClient.FormatPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
    <HEAD>
        <title id="title">BlogX</title>
        <meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
        <meta name="CODE_LANGUAGE" Content="C#">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <LINK rel="stylesheet" type="text/css" href="SiteConfig/blogx.css">
    </HEAD>
    <body>
        <uc1:BlogXHeader id="BlogXHeader1" runat="server"></uc1:BlogXHeader>
        <asp:PlaceHolder ID="content" Runat="server"></asp:PlaceHolder>
    </body>
</HTML>
