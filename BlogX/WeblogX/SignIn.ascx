<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SignIn.ascx.cs" Inherits="Anderson.Chris.BlogX.WebClient.SignIn" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
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
<div id="login" class="section">
    <h3>Sign In</h3>
    <p>Username:
        <asp:TextBox id="username" runat="server"></asp:TextBox></p>
    <p>Password:
    <asp:TextBox TextMode="Password" id="password" runat="server"></asp:TextBox></p>
    <p><asp:CheckBox id="rememberCheckbox" runat="server" Text="Remember Login"></asp:CheckBox></p>
    <p><asp:Button id="doSignIn" runat="server" Text="Sign In"></asp:Button></p>
</div>
