<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="EmployeesGratuity.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmployeesGratuity" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server"> <table>
            
            <tr>
                <td>
                    <asp:GridView ID="gvEmployeesGratuity" EmptyDataText="No Record Found" AutoGenerateColumns="False"
                        runat="server" HeaderStyle-CssClass="header" CssClass="gridview">
                        <Columns>
                         <asp:BoundField HeaderText="EmpID" DataField="EmpID"  >
                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>
                            
                            <asp:HyperLinkField DataNavigateUrlFields="EmpID" 
                                DataNavigateUrlFormatString="EMPGratuity.aspx?Id={0}" DataTextField="Name" 
                                HeaderText="Employee" />
                            <asp:BoundField HeaderText="Gratuity ON" DataField="GratuityON" DataFormatString="{0:dd MMM yyyy}" >
                                <HeaderStyle HorizontalAlign="Left" Width="200"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" Width="200" />
                            </asp:BoundField>
                           
                            <asp:BoundField HeaderText="Accrued" DataField="Accrued"  DataFormatString="{0:N2}" ItemStyle-VerticalAlign="Middle">
                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="If Resign" DataField="Resign"  DataFormatString="{0:N2}" ItemStyle-VerticalAlign="Middle">
                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="If Terminated" DataField="Terminated"  DataFormatString="{0:N2}" ItemStyle-VerticalAlign="Middle">
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

</asp:Content>

