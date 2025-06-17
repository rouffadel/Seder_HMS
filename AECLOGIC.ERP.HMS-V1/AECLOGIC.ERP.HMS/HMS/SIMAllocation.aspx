<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="SIMAllocation.aspx.cs" Inherits="AECLOGIC.ERP.HMS.SIMAllocation" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
 <tr>
          <td class="pageheader">
              >> SIM Cards
            <br/>
             <asp:LinkButton ID="lnkAdd" runat="server" Text="Add" onclick="lnkAdd_Click"></asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" 
                  onclick="lnkEdit_Click"></asp:LinkButton> 
          </td>
        
        </tr>
       
        
         
        <tr>
        <td>
           
                
                      <table id="tblNew" runat="server" visible="false">
                            
                    
                    <tr>
                        <td style="width: 124px" >
                           Employee ID<span style="color: #ff0000">*</span></td>
                        <td>
                            <asp:TextBox ID="txtEmpID" runat="server"></asp:TextBox></td>
                    </tr>
                 <tr>
                        <td style="width: 124px" >
                           Mobile NO<span style="color: #ff0000">*</span></td>
                        <td>
                            <asp:TextBox ID="txtMobileNO" runat="server" ></asp:TextBox></td>
                    </tr>
                    
                    <tr>
                        <td style="width: 124px" >
                           SIM NO<span style="color: #ff0000">*</span></td>
                        <td>
                            <asp:TextBox ID="txtSIMNO" runat="server" ></asp:TextBox></td>
                    </tr>
                    
                     <tr>
                        <td style="width: 124px" >
                           Amount Limit<span style="color: #ff0000">*</span></td>
                        <td>
                            <asp:TextBox ID="txtAmountLt" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 124px" >
                           Vendors<span style="color: #ff0000">*</span></td>
                        <td>
                            <asp:DropDownList ID="ddlVendors" runat="server" CssClass="droplist" ></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        
                        <td colspan="2">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit"  CssClass="savebutton" 
                                Width="100px" OnClick="btnSubmit_Click" OnClientClick="javascript:return validatesave();"/></td>
                    </tr>
                      
                      
                      </table>                
                <br />
                
                      <table id="tblEdit" runat="server" visible="false">
                   
                    <tr>
                    <td  style="width:100%">
                        
            <asp:GridView ID="gvLeaveProfile"  runat="server" AutoGenerateColumns="False" 
                            CellPadding="4"  ForeColor="#333333" GridLines="Both" 
                            EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" 
                            onrowcommand="gvLeaveProfile_RowCommand" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%"  />
                                <Columns>
                                       
                                        <asp:TemplateField HeaderText="Name"  HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                
                                                </ItemTemplate>
                                       </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Mobile No"  HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblMobileno" runat="server" Text='<%#Eval("MobileNo")%>'></asp:Label>
                                                
                                                </ItemTemplate>
                                       </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="SIM No"  HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblSIMNO" runat="server" Text='<%#Eval("SIMNo")%>'></asp:Label>
                                                
                                                </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Amount Limit"  HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblAmtLt" runat="server" Text='<%#Eval("AmountLimit")%>'></asp:Label>
                                                
                                                </ItemTemplate>
                                       </asp:TemplateField>
                                       
                                       <asp:TemplateField HeaderText="Vendor Name"  HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblVendor" runat="server" Text='<%#Eval("vendor_name")%>'></asp:Label>
                                                
                                                </ItemTemplate>
                                       </asp:TemplateField>
                                       
                                       <asp:TemplateField>
                                                <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("CSID")%>'  CommandName="Edt"></asp:LinkButton></ItemTemplate>
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
              
        
        </td>    
    
    </tr>
</table>
</asp:Content>

