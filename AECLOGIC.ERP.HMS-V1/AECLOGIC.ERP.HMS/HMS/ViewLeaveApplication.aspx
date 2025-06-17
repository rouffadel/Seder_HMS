<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="ViewLeaveApplication.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ViewLeaveApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
    <table width="100%">
<tr><td class="pageheader"> List of Leave Applications
</td></tr>
<tr><td>
    <asp:GridView ID="gvLeaveList" AutoGenerateColumns="false" CssClass="gridview" runat="server">
    <Columns>
    <asp:BoundField DataField="EmpID" HeaderText="EmpID" />
    <asp:BoundField DataField="WorkSite" HeaderText="WorkSite" />
    <asp:BoundField DataField="EmpName" HeaderText="EmpName" />
    <asp:BoundField DataField="CL" HeaderText="CL" />
    <asp:BoundField DataField="EL" HeaderText="EL" />
    </Columns>
    
    </asp:GridView> </td></tr>
</table>

</asp:Content>

