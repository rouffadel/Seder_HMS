<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="NRIDocType.aspx.cs" Inherits="AECLOGIC.ERP.HMS.PassportVisa" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
<script type="text/javascript" language="javascript">

    function Validate() {
        if (!chkName('<%=txtDocName.ClientID%>', "Document Name", true, "")) {
            return false;
        }
        
    }

    </script>

     <table width="100%">

        <tr>
            <td>
            
              <table>
                          <tr>
                              <td>
                                   <table>
                                    
                     

                    <tr>
                       <td>
                       Document Name<span style="color: #ff0000">*</span>
                       
                       </td>

                       <td>
                       <asp:TextBox ID="txtDocName" runat="server" ></asp:TextBox>
                       
                       </td>
                    
                    </tr>

                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="savebutton" Width="100px"
                                 OnClientClick="javascript:return Validate();" onclick="btnSubmit_Click" />
                        </td>
                    </tr>
                                   
                                   </table> 
                              
                              </td>
                          
                          
                          </tr>
                    
                    
                    </table>
                    
                    <table>
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="gvDocs" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                HeaderStyle-CssClass="tableHead" CssClass="gridview" 
                                onrowcommand="gvDocs_RowCommand">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Document Name" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocname" runat="server" Text='<%#Eval("DocName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("ID")%>'
                                                CommandName="Edt"></asp:LinkButton></ItemTemplate>
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

