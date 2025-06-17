<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VacationalPrint2.aspx.cs" Inherits="AECLOGIC.ERP.HMS.VacationalPrint2" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">

    </head>
    <body runat="server">

    
    <table>
        <tr>
            <td>
                 <img border="0" alt="Print" onclick="javascript:window.print();  window.close();"
                                                                                class="right" src="Images/print.png" />
            </td>
        </tr>

        <tr>
            <td>

                <asp:DataList ID="dtlvacation" runat="server" HeaderStyle-CssClass="datalistHead"
                                        Width="100%"  OnItemDataBound="dtlvacation_ItemDataBound">

                                        <ItemTemplate>
                                            <div class="DivBorderOlive" style="margin-bottom: 20px">
                                                <table style="width: 100%; background-color: #efefef;">
                                                    <tr>
                                                        <td>
                                                            <b>EmployeeName :</b>
                                                            <asp:Label ID="lblempname" runat="server" Text='<%#Eval("name")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <b>EmployeeId :</b>
                                                            <asp:Label ID="lblempid" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <b>DateOfJoin :</b>
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("Dateofjoin")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Balance Annual Leaves:</b>
                                                            <asp:Label ID="lblAAL" runat="server" Text='<%#Eval("AvailableLeaves")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <b>Requested Annual Leaves :</b>
                                                            <asp:Label ID="lblRAL" runat="server" Text='<%#Eval("RequestedLeaves")%>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <b>Granted Annual Leaves :</b>
                                                            <asp:Label ID="lblGAL" runat="server" Text='<%#Eval("GrantedDays")%>'></asp:Label>
                                                        </td>


                                                        <td>
                                                            <b>OverTime Hours :</b>
                                                            <asp:Label ID="lblOT" runat="server" Text='<%#Eval("OTHours")%>'></asp:Label>
                                                        </td>


                                                    </tr>
                                                </table>
                                                <form runat="server">   
                                                <asp:GridView ID="GVVacation"  Visible="true" runat="server" AlternatingRowStyle-BackColor="GhostWhite"
                                                    AutoGenerateColumns="false" ShowFooter="true" OnRowCommand="GVVacation_RowCommand" 
                                                    DataSource='<%#BindTransdetails(Eval("Empid").ToString())%>' OnRowDataBound="GVVacation_RowDataBound" 
                                                    HeaderStyle-CssClass="tableHead" Width="100%">

                                                    <Columns>

                                                        <%-- <asp:TemplateField HeaderText="Credit/Debit">
                                                              <%--  <asp:ItemTemplate>
                                                                    <asp:Label ID="lbldesc" runat="server" Text='<%#Bind("Description")%>'></asp:Label>
                                                                </asp:ItemTemplate
                                                               
                                                            </asp:TemplateField>--%>
                                                        <asp:BoundField DataField="Description" HeaderText="Credit/Debit"></asp:BoundField>

                                                        <asp:TemplateField HeaderText="Value">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtA1" OnTextChanged="QtyChanged" Style="text-align: right" AutoPostBack="true" Width="120px" Height="20px" runat="server" Text='<%#Bind("Amount")%>'></asp:TextBox>
                                                            </ItemTemplate>

                                                            <FooterTemplate>
                                                                <div class="DivBorderOlive1" style="margin-bottom: 20px; font: bold; font-size: 17px">
                                                                    <asp:Label ID="lbl1" runat="server" Text="Total= "></asp:Label>
                                                                    <asp:Label ID="lblvalue" runat="server" Text="0.000"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtA6" Style="text-align: right" runat="server" Width="120px" Height="20px" Visible="false"></asp:TextBox>
                                                              
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">

                                                            <ItemTemplate>
                                                                <asp:Button ID="btnCal" Style="text-align: right" AutoPostBack="true" Width="10px" Height="20px" runat="server" Text='Get'  Visible="false"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">

                                                            <ItemTemplate>
                                                                 <asp:LinkButton runat="server" id="lnkatt_Viewk" AutoPostBack="true" Text="Attendance" Visible="false"  tooltip="Click to complete attendance details"   ></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Details" HeaderText="Details" />
                                                    </Columns>
                                                    <FooterStyle />

                                                    <FooterStyle BackColor="#cccccc" ForeColor="Black" HorizontalAlign="left" />
                                                </asp:GridView>
                                                        
                                                </form>
                                            </div>

                                        </ItemTemplate>
                                    </asp:DataList>



            </td>
        </tr>

    </table>

</body>

</html>