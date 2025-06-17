<%@ Page Language="C#" AutoEventWireup="True"   CodeBehind="EMPGratuity.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.EMPGratuity" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:HyperLink ID="lnkBack" NavigateUrl="EmployeesGratuity.aspx" runat="server">Back</asp:HyperLink><br />
                    <asp:GridView ID="gvEMPGratuity" EmptyDataText="No Record Found" AutoGenerateColumns="False"
                        runat="server" HeaderStyle-CssClass="header" CssClass="gridview">
                        <Columns>
                            <asp:BoundField HeaderText="Gratuity ON" DataField="GratuityON"  DataFormatString="{0:dd MMM yyyy}">
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Qlfy Days" DataField="QualifyDays" ItemStyle-VerticalAlign="Middle">
                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="No Of Days" DataField="NoOfDays" ItemStyle-VerticalAlign="Middle">
                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Tot Qlfy Days" DataField="TotQlfyDays" ItemStyle-VerticalAlign="Middle">
                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Basic" DataField="BasicPerMonth" DataFormatString="{0:N2}" ItemStyle-VerticalAlign="Middle">
                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Cum. Gratuity" DataField="CumGratuity"  DataFormatString="{0:N2}" ItemStyle-VerticalAlign="Middle">
                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Gratuity" DataField="EffectivGratuity"  DataFormatString="{0:N2}" ItemStyle-VerticalAlign="Middle">
                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle CssClass="header"></HeaderStyle>
                    </asp:GridView>
                    <uc1:Paging ID="AllowancePaging" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
