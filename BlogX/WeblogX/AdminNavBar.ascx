<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AdminNavBar.ascx.cs" Inherits="Anderson.Chris.BlogX.WebClient.AdminNavBar" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
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
<div class="section">
    <h3>Admin</h3>
    <ul>
        <li>
            <A href="EntryEdit.aspx">Add Entry</A></li>
        <li>
            <A href="Referrers.aspx">Referrers</A></li>
        <li>
            <asp:LinkButton Runat="server" ID="logout">Logout</asp:LinkButton></li>
    </ul>
</div>
