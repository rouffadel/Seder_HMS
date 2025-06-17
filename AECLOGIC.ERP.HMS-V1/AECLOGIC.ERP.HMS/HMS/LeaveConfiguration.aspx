<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="LeaveConfiguration.aspx.cs" Inherits="AECLOGIC.ERP.HMS.LeaveConfiguration" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>  
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
<script language="javascript" type="text/javascript" src="JS/common.js"></script>
  <script language="javascript" type="text/javascript">

        function validatesave()
        {
          
             
              if (!chkName('<%=txtLeaveType.ClientID%>', " Leave Type","true","[Leave Type]"))
             return false;
             if (! chkNumber('<%=txtNoOfDays.ClientID%>', " Days","true","[Days]"))
             return false;
             
        }

    </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
       
      
        
        <tr>
                        <td colspan="2" class="pageheader">
                             New IT-Sections</td>
                    </tr>
        <tr>
        <td>
                      <table id="tblNew" runat="server" visible="false">
                    <tr>
                        <td style="width: 124px" >
                            Leave Type<span style="color: #ff0000">*</span></td>
                        <td>
                            <asp:TextBox ID="txtLeaveType" runat="server" Width="300"></asp:TextBox></td>
                    </tr>
                     <tr>
                        <td style="width: 124px" >
                            Days<span style="color: #ff0000">*</span></td>
                        <td>
                            <asp:TextBox ID="txtNoOfDays" runat="server" Width="300"></asp:TextBox>(Per Annum)</td>
                    </tr>
                    <tr>
                        <td style="width: 124px"  >
                            Status</td>
                        <td >
                            <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" /></td>
                    </tr>
                    <tr>
                        
                        <td colspan="2">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit"  CssClass="savebutton" 
                                Width="100px" onclick="btnSubmit_Click" OnClientClick="javascript:return validatesave();"  /></td>
                    </tr>
                      
                      
                      </table>                
                <br />
                
                      <table id="tblEdit" runat="server" visible="false">
                  
                    
                    
                   
                    <tr>
                    <td  style="width:100%">
                        
                        <asp:GridView ID="gvLeaveConfig" runat="server" AutoGenerateColumns="False" 
                            CellPadding="4" EmptyDataRowStyle-CssClass="EmptyRowData" 
                            EmptyDataText="No Records Found" ForeColor="#333333" GridLines="Both" 
                            HeaderStyle-CssClass="tableHead" onrowcommand="gvLeaveConfig_RowCommand" CssClass="gridview">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
                                Width="100%" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="LeaveType">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLeaveType" runat="server" Text='<%#Eval("LeaveType")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Days">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLeavedays" runat="server" Text='<%#Eval("Days")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" 
                                            CommandArgument='<%#Eval("LeaveTypeID")%>' CommandName="Edt" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDel" runat="server" 
                                            CommandArgument='<%#Eval("LeaveTypeID")%>' CommandName="Del" Text="Delete"></asp:LinkButton>
                                    </ItemTemplate>
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

