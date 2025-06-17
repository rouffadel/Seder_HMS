<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YstrDayOutPaging.ascx.cs" Inherits="AECLOGIC.ERP.HMS.Templates.YstrDayOutPaging" %>
<div id="dvPager" style="width: 100%; height: 25px; padding-left: 5px; vertical-align: middle;
    text-align: left;" runat="server">
    Show Rows
    <asp:DropDownList ID="ddlShowRows" runat="server" Width="45px" AutoPostBack="True" CssClass="droplist"
         OnSelectedIndexChanged="ddlShowRows_SelectedIndexChanged">
         <asp:ListItem>5</asp:ListItem>
        <asp:ListItem>10</asp:ListItem>
        <asp:ListItem>15</asp:ListItem>
        <asp:ListItem>20</asp:ListItem>
        <asp:ListItem>25</asp:ListItem>
        <asp:ListItem>30</asp:ListItem>
        <asp:ListItem>35</asp:ListItem>
        <asp:ListItem>40</asp:ListItem>
        <asp:ListItem>45</asp:ListItem>
        <asp:ListItem>50</asp:ListItem>
    </asp:DropDownList>
    &nbsp; Goto Page
    <asp:DropDownList ID="ddlGotoPages" runat="server" AutoPostBack="True" Width="45px"
        CssClass="droplist" OnSelectedIndexChanged="ddlGotoPages_SelectedIndexChanged">
    </asp:DropDownList>
    &nbsp;
    <asp:Label ID="lblShowPages" runat="server"></asp:Label>&nbsp;
    <asp:LinkButton ID="lnkBtnFirst" runat="server" AccessKey="s" OnClick="lnkBtnFirst_Click">First</asp:LinkButton>|
    <asp:LinkButton ID="lnkBtnPrevious"  runat="server" AccessKey="v" onclick="lnkBtnPrevious_Click">Previous</asp:LinkButton>|
    <asp:LinkButton ID="lnkBtnNext" runat="server" AccessKey="x" OnClick="lnkBtnNext_Click">Next</asp:LinkButton>|
    <asp:LinkButton ID="lnkBtnLast" runat="server" AccessKey="l" OnClick="lnkBtnLast_Click">Last</asp:LinkButton>


</div>
