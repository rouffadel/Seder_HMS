<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ViewReimbursements.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ViewReimbursements" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

    <link href="Includes/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="Includes/CSS/sddm.css" rel="stylesheet" type="text/css" />
    <link href="Includes/CSS/base.css" rel="stylesheet" type="text/css" />
    <title></title>


    <div>
    <table id="tblShow" runat="server" width="100%">
                     <tr><td>
                         <asp:Label ID="Label1" runat="server" Text="Items View" Font-Bold="True" 
                             Font-Size="Medium" ForeColor="#CC6600"></asp:Label>
                         </td></tr>
                     <tr><td> 
                         <asp:GridView ID="gvShow" CssClass="gridview" runat="server" AutoGenerateColumns="false" 
                                    
                                     Width="100%"> 
                                    
                                     <Columns>
                                    
                                      
                                          <asp:TemplateField HeaderText="Sl.No">
                                             <ItemTemplate>
                                             <%#Container.DataItemIndex + 1%>
                                             
                                             </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="30" />
                                             <HeaderStyle HorizontalAlign="Left" Width="30" />
                                         </asp:TemplateField>
                                         
                                         <asp:TemplateField HeaderText="EmpID">
                                         <ItemTemplate> 
                                          <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID")%>' runat="server" ></asp:Label></ItemTemplate>
                                         </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="80Px" HeaderText="ReimburseItem">
                                             <ItemTemplate>
                                                 <asp:Label ID="lblRItem" Text='<%#Eval("RItem")%>' runat="server" ></asp:Label>
                                             </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="30" />
                                             <HeaderStyle HorizontalAlign="Left" Width="30" />
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Units of Measure">
                                             <ItemTemplate>
                                                 <asp:DropDownList ID="ddlunits" DataSource='<%#GetAUDataSet() %>'  CssClass="droplist" Enabled="false" DataTextField="AU_Name" DataValueField="AU_Id" SelectedIndex='<%#GetAUIndex(Eval("AUID").ToString())%>' runat="server" AutoPostBack="false" >
                                                 </asp:DropDownList>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Unit Rate">
                                         <ItemTemplate>
                                           <asp:Label ID="txtRate" Text='<%#Eval("UnitRate") %>' runat="server" ></asp:Label>
                                           </ItemTemplate>
                                           </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Quantity">
                                             <ItemTemplate>
                                              <asp:Label ID="txtQty" Text='<%#Eval("Qty") %>' runat="server" ></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Amount">
                                         <ItemTemplate>
                                         <asp:Label ID="txtAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                           </ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-Width="100Px" HeaderText="Purpose">
                                             <ItemTemplate>
                                              <asp:Label ID="txtPurpose" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"Purpose") %>' ></asp:Label>
                                             </ItemTemplate>

                                                <HeaderStyle Width="100px"></HeaderStyle>

                                             <ItemStyle HorizontalAlign="Left" />
                                         </asp:TemplateField>
                                         <asp:TemplateField Visible="false">
                                         <ItemTemplate>
                                             <asp:Label ID="lblRItemNo" runat="server" Text='<%#Eval("RItemID")%>'></asp:Label>
                                         </ItemTemplate>
                                         </asp:TemplateField> 
                                         <asp:TemplateField HeaderText="Spent On">
                                            <ItemTemplate>
                                             <asp:Label ID="txtSpentOn" runat="server" Text='<%#Eval("DateOfSpent")%>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Submitted On">
                                            <ItemTemplate>
                                             <asp:Label ID="txtDOS" runat="server" Text='<%#Eval("DOS")%>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ERID">
                                            <ItemTemplate>
                                             <asp:Label ID="lblERID" runat="server" Text='<%#Eval("ERID")%>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                            <ItemTemplate>
                                                 <asp:HyperLink ID="hlnkView"  NavigateUrl='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "Proof").ToString())%>'  runat="server">Proof</asp:HyperLink>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                     </Columns>
                         </asp:GridView></td></tr>
                     </table>
    </div>
 </asp:Content>
