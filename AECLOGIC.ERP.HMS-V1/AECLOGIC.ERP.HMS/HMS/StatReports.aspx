<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="StatReports.aspx.cs" Inherits="AECLOGIC.ERP.HMS.StatReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">

 <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">

 <tr>
    <td align="left">
    <asp:LinkButton ID="lnkESI" runat="server" Text="ESI" onclick="lnkESI_Click"></asp:LinkButton>&nbsp;&nbsp;|&nbsp;&nbsp;
    <asp:LinkButton ID="lnkPF" runat="server" Text="PF" onclick="lnkPF_Click"></asp:LinkButton>&nbsp;&nbsp;|&nbsp;&nbsp;
    <asp:LinkButton ID="lnkTDS" runat="server" Text="TDS" onclick="lnkTDS_Click"></asp:LinkButton>
    </td>
 
 </tr>
  <tr>
                                <td align="left">
                                 <strong>Worksite&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlWorksite" runat="server" CssClass="droplist" 
                                                   >
                                                </asp:DropDownList>
                                                </strong>
&nbsp;&nbsp;
                                   From Date <asp:TextBox
                                            ID="txtDay" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDay"
                                        PopupPosition="BottomLeft" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                    &nbsp;&nbsp;To Date<asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                        PopupPosition="BottomLeft" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                    <asp:Button ID="btnDaySearch" runat="server" Text="Generate Report" 
                                        CssClass="savebutton" onclick="btnDaySearch_Click" />
                                </td>
                            </tr>

                            <tr>
                        <td  align="left">
                           <asp:GridView ID="gvStatReport" runat="server" AutoGenerateColumns="False" EmptyDataText="No Records Found" HeaderStyle-CssClass="tableHead" 
                           EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                
                                    <asp:BoundField HeaderText="EmpID" DataField="EmpId" />
                                    <asp:BoundField HeaderText="EmpName" DataField="Name" />
                                    <asp:BoundField  HeaderText="Designation" DataField="Designation"/>
                                      <asp:TemplateField HeaderText="Deducted Amount" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                       <asp:Label ID="lblESIAmount" runat="server" Text='<%# GetstatAmount(decimal.Parse(Eval("Amount").ToString())).ToString()%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <b><asp:Label ID="lblESITot" runat="server" Text='<%# Getstat().ToString()%>'></asp:Label></b>
                                    </FooterTemplate>
                                  </asp:TemplateField>
                                   
                                    
                                    
                                </Columns>
                                <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                            </asp:GridView>
                        </td>
                    </tr>

 </table>
</asp:Content>

