<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="Paging.ascx.cs" Inherits="AECLOGIC.ERP.COMMON.Paging" %>

<div id="dvPager" runat="server" class="mrg-top-20">
<div class="col-sm-6 form-inline nogaps">
    <div class="dataTables_info">
        <label class="col-sm-4 nogaps"> Show Rows
    <asp:DropDownList ID="ddlShowRows"  runat="server" Width="45px" AutoPostBack="True"
        CssClass="droplist form-control" OnSelectedIndexChanged="ddlShowRows_SelectedIndexChanged">
        <asp:ListItem>10</asp:ListItem>
        <asp:ListItem>20</asp:ListItem>
        <asp:ListItem>30</asp:ListItem>
        <asp:ListItem>40</asp:ListItem>
        <asp:ListItem>50</asp:ListItem>
        <asp:ListItem>100</asp:ListItem>
        <asp:ListItem>200</asp:ListItem>
        <asp:ListItem>300</asp:ListItem>
        <asp:ListItem>400</asp:ListItem>
        <asp:ListItem>500</asp:ListItem>
    </asp:DropDownList></label>

        <label class="col-sm-8 nogaps"> Goto Page
    <asp:DropDownList ID="ddlGotoPages" runat="server" AutoPostBack="True" Width="45px"
        CssClass="droplist form-control" OnSelectedIndexChanged="ddlGotoPages_SelectedIndexChanged">
    </asp:DropDownList>
        <asp:Label ID="lblShowPages" runat="server"></asp:Label></label>
    </div></div>

<div class="col-sm-6 dataTables_paginate paging_simple_numbers nogaps">

    <ul class="pagination pull-left" >
        <li class="paginate_button previous">
            <asp:LinkButton ID="lnkBtnFirst" runat="server" AccessKey="s" OnClick="lnkBtnFirst_Click">First</asp:LinkButton></li>
        <li class="paginate_button ">
            <asp:LinkButton ID="lnkBtnPrevious" runat="server" AccessKey="v" OnClick="lnkBtnPrevious_Click">Previous</asp:LinkButton>
        </li>
        <li class="paginate_button ">
            <asp:LinkButton ID="lnkBtnNext" runat="server" AccessKey="x" OnClick="lnkBtnNext_Click">Next</asp:LinkButton></li>
        <li class="paginate_button next">
            <asp:LinkButton ID="lnkBtnLast" runat="server" AccessKey="l" OnClick="lnkBtnLast_Click">Last</asp:LinkButton>
        </li></ul>


</div>

</div>
 
