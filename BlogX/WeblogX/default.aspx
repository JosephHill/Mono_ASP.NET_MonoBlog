<%@ Register TagPrefix="uc1" TagName="BlogXFooter" Src="BlogXFooter.ascx" %>
<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="false" Inherits="Anderson.Chris.BlogX.WebClient.HomePage" %>
<%@ Register TagPrefix="uc1" TagName="SideBarOpmlList" Src="SideBarOpmlList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SideBarList" Src="SideBarList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CategoryList" Src="CategoryList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BlogXHeader" Src="BlogXHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
    <HEAD>
        <title id="title" runat="server">ChrisAn's BlogX</title> 
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
        <LINK title="RSS" href="BlogxBrowsing.asmx/GetRss?" type="application/rss+xml" rel="alternate">
            <LINK href="SiteConfig/blogx.css" type="text/css" rel="stylesheet">
    </HEAD>
    <body>
        <uc1:blogxheader id="BlogXHeader1" runat="server"></uc1:blogxheader>
        <form id="Form1" method="post" runat="server">
            <div id="content">
                <asp:placeholder id="blogContent" runat="server"></asp:placeholder>
            </div>
            <div class="sidebar" id="rightBar" runat="server"><asp:calendar id="blogCal" runat="server" TitleFormat="Month" CssClass="navCalendar" CellPadding="4" DayNameFormat="FirstLetter">
                    <TodayDayStyle CssClass="navTodayStyle"></TodayDayStyle>
                    <SelectorStyle CssClass="navSelectorStyle"></SelectorStyle>
                    <DayStyle CssClass="navDayStyle"></DayStyle>
                    <NextPrevStyle CssClass="navNextPrevStyle"></NextPrevStyle>
                    <DayHeaderStyle CssClass="navDayHeader"></DayHeaderStyle>
                    <SelectedDayStyle CssClass="navSelectedDayStyle"></SelectedDayStyle>
                    <TitleStyle CssClass="navTitleStyle"></TitleStyle>
                    <WeekendDayStyle CssClass="navWeekendDayStyle"></WeekendDayStyle>
                    <OtherMonthDayStyle CssClass="navOtherMonthDayStyle"></OtherMonthDayStyle>
                </asp:calendar><br>
                <uc1:SideBarOpmlList id="SideBarOpmlList1" runat="server"></uc1:SideBarOpmlList><br>
                <uc1:sidebarlist id="navLinkList" runat="server" FileName="links.xml"></uc1:sidebarlist><br>
                <uc1:categorylist id="categoryList" runat="server"></uc1:categorylist><br>
            </div>
        </form>
        <uc1:blogxfooter id="BlogXFooter1" runat="server"></uc1:blogxfooter>
    </body>
</HTML>
