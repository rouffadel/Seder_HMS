<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="PTReport.aspx.cs" Inherits="AECLOGIC.ERP.HMS.PTReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <table>
        <tr>
            <td>
                <asp:GridView ID="grdPTReport" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                    EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="PTRange" HeaderText="Employees whose monthly salaries or wages or both are" />
                        <asp:BoundField DataField="NumofEmployees" HeaderText="Number of Employees" />
                        <asp:BoundField DataField="Amount" HeaderText="Rate of Tax per month (Rs)" />
                        <asp:BoundField DataField="Amountdeduct" HeaderText="Amount of Tax deducted" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
