<%@ Register TagPrefix="uc1" TagName="BlogXHeader" Src="BlogXHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BlogXFooter" Src="BlogXFooter.ascx" %>
<%@ Page language="c#" Codebehind="CategoryView.aspx.cs" AutoEventWireup="false" Inherits="Anderson.Chris.BlogX.WebClient.CategoryView" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
    <HEAD>
        <title id="title" runat="server">Category</title> 
        <!--
//= - - - - - - -
// Copyright 2003, Chris Anderson
// 
//   Provided as is, with no warrenty, etc.
//   License is granted to use, copy, modify, 
//   with or without credit to me, just don't
//   blame me if it doesn't work.
//= - - - - - - -
-->
        <meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
        <meta name="CODE_LANGUAGE" Content="C#">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    </HEAD>
    <body>
        <uc1:BlogXHeader id="BlogXHeader1" runat="server"></uc1:BlogXHeader>
        <form id="Form1" method="post" runat="server">
            <div id="content" runat="server"></div>
        </form>
        <uc1:BlogXFooter id="BlogXFooter1" runat="server"></uc1:BlogXFooter>
    </body>
</HTML>
