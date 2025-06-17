<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="CompContribution.aspx.cs" Inherits="AECLOGIC.ERP.HMS.CompContribution" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">

    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">

        function validatesave()
        {
        
               if (!chkDropDownList('<%=ddlItem.ClientID%>', "  Payroll Item","",""))
             return false;
             
               if (!chkDropDownList('<%=ddlFinancial.ClientID%>', " Financial Year","",""))
             return false;
             
               if (!chkDropDownList('<%=ddlWages.ClientID%>', " Wages ","",""))
             return false;
             
              if (!chkFloatNumber('<%=txtContributionRate.ClientID%>', "  Contribution Rate","true",""))
             return false;
             
              if (!chkFloatNumber('<%=txtWageLimit.ClientID%>', " Wage Limit","true",""))
             return false;
             
              if (!chkFloatNumber('<%=txtAmtLimit.ClientID%>', "Amount Limit","true",""))
             return false;
            
        }

    </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
        <td>
           
                
                      <table id="tblNew" runat="server" visible="false">
                              <tr>
                                      <td colspan="2" class="pageheader">
                                         New Contribution Item
                                      </td>
                                 </tr>
                    
                   <tr>
                        <td style="width: 130px" >
                           Contribution Item<span style="color: #ff0000">*</span></td>
                        <td>
                        <asp:DropDownList ID="ddlItem" CssClass="droplist"  runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 130px" >
                           Financial Year<span style="color: #ff0000">*</span></td>
                        <td>
                        <asp:DropDownList ID="ddlFinancial" CssClass="droplist"  runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 130px" >
                          Calculation based on<span style="color: #ff0000">*</span></td>
                        <td>
                        <asp:DropDownList ID="ddlWages"  CssClass="droplist"  runat="server">
                         <asp:ListItem Text="--Select--" Value="-1" ></asp:ListItem>
                         <asp:ListItem Text="Salary" Value="1" ></asp:ListItem>
                          <asp:ListItem Text="Gross" Value="2" ></asp:ListItem>
                        </asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="width: 130px"  >
                           Contribution Rate<span style="color: #ff0000">*</span></td>
                        <td>
                            <asp:TextBox ID="txtContributionRate" runat="server" ></asp:TextBox></td>
                    </tr>
                  
                     <tr>
                        <td style="width: 130px"  >
                          Wage Limit <span style="color: #ff0000">*</span></td>
                        <td>
                            <asp:TextBox ID="txtWageLimit" runat="server"></asp:TextBox></td>
                    </tr>
                     <tr>
                        <td style="width: 130px"  >
                          Amount Limit <span style="color: #ff0000">*</span></td>
                        <td>
                            <asp:TextBox ID="txtAmtLimit" runat="server"></asp:TextBox>
                            
                            </td>
                    </tr>
                    <tr>
                        
                        <td colspan="2">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit"  CssClass="savebutton" 
                                Width="100px" OnClick="btnSubmit_Click" OnClientClick="javascript:return validatesave();" /></td>
                    </tr>
                      
                      
                      </table>                
                <br />
                
                      <table id="tblEdit" runat="server" visible="false">
                    <tr><td colspan="2"><b>Financial Year:</b>
                       <asp:DropDownList ID="ddlFinYear" AutoPostBack="true"  runat="server" CssClass="droplist" 
                           onselectedindexchanged="ddlFinYear_SelectedIndexChanged">
                       </asp:DropDownList></td></tr>
                    <tr>
                    <td  style="width:100%">
                        
            <asp:GridView ID="gvAllowances"  runat="server" AutoGenerateColumns="False" 
                            CellPadding="4"  ForeColor="#333333" GridLines="Both" 
                            EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" 
                            onrowcommand="gvWages_RowCommand" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%"  />
                                <Columns>
                                        <asp:TemplateField HeaderText="Financial Year">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblFinancial" runat="server" Text='<%#Eval("FinaciaYear")%>'></asp:Label>
                                                
                                                </ItemTemplate>
                                       </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contribution Item">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("payrollitem")%>'></asp:Label>
                                                
                                                </ItemTemplate>
                                       </asp:TemplateField>
                                       
                                       
                                       <asp:TemplateField HeaderText="Percentage" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblPercentage" runat="server" Text='<%#Convert.ToDouble(Eval("ContrRate")).ToString("#.00%")%>'></asp:Label>
                                                
                                                </ItemTemplate>
                                       </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Calculation based on">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblDays" runat="server" Text='<%#Eval("LongName")%>'></asp:Label>
                                                
                                                </ItemTemplate>
                                       </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Wage Limit"  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblLimited" runat="server" Text='<%#Eval("WageLimit")%>'></asp:Label>
                                                
                                                </ItemTemplate>
                                       </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Amount Limit"  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblClaculated" runat="server" Text='<%#Eval("AmountLimit")%>'></asp:Label>
                                                
                                                </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField>
                                                <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("CCIId")%>'  CommandName="Edt"></asp:LinkButton></ItemTemplate>
                                       </asp:TemplateField>
                                     
                                </Columns>
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                            </asp:GridView>
            
            </td>
        
        </tr>
                      
                      
                      </table>
              
        <br />
                    <table id="tblOrder" runat="server" visible="false">
                    <tr>
           <td style="vertical-align:top; width:200px">
               <asp:ListBox ID="lstDepartments" runat="server" Height="400px"></asp:ListBox>
           
           </td>
           <td style="vertical-align:middle; width:100px">
              <asp:Button ID="btnFirst" runat="server" Text="Move First" Width="80px" OnClick="btnFirst_Click" /><br /><br />
              <asp:Button ID="btnUp" runat="server" Text="Move Up" Width="80px" OnClick="btnUp_Click"/><br /><br />
              <asp:Button ID="btnDown" runat="server" Text="Move Down" Width="80px" OnClick="btnDown_Click"/><br /><br />
              <asp:Button ID="btnLast" runat="server" Text="Move Last" Width="80px" OnClick="btnLast_Click"/><br /><br />
           </td>
           
                                 <td  style="vertical-align:middle">
                                     <asp:Button ID="btnOrder" runat="server" Text="Submit"  CssClass="savebutton" Width="100px" OnClick="btnOrder_Click" />
                                 </td>
        
        </tr>
                    
                    
                    </table>
        </td>    
    
    </tr>
        
        
        
        
        
    </table>
</asp:Content>

