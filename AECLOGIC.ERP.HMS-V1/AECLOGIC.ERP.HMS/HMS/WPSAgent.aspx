<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="WPSAgent.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Default2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
 <script language="javascript" type="text/javascript">

     function ValidateSave()
         {
              if (!chkNumber('<%=txtAgentID.ClientID %>', 'AgentID', true, ''))
                return false;
         }
        
    </script>

<table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <AEC:Topmenu ID="topmenu" runat="server"></AEC:Topmenu>
                    </td>
                </tr>

                <tr>
                   <td>
                       <table width="40%">
                        <tr>
                                        <td >
                                            AgentID<span style="color: #ff0000">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAgentID" runat="server"  TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
                 <tr>
                                    <td>
                                      &nbsp;
                                    </td>
                                        <td  >
                                            <asp:Button ID="btnSave" runat="server" CssClass="savebutton" 
                                                Text="Submit"  AccessKey="s" TabIndex="3" 
                                                ToolTip="[Alt+s OR Alt+s+Enter]" onclick="btnSave_Click" OnClientClick="javascript:return ValidateSave();" />
                                        </td>
                                    </tr>
                       
                       </table>
                   
                   </td>
                
                </tr>


                
                                    <tr>
                                        <td >
                                           &nbsp;
                                        </td>
                                    </tr>


                                     <tr>
                                        <td >
                                          <table width="40%">
                                          <tr>
                                        <td  >
                                            <asp:GridView ID="gvEditdept" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                DataKeyNames="ID"  Width="80%"
                                                CssClass="gridview" HeaderStyle-CssClass="tableHead" AllowSorting="true"
                                                EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" 
                                                onrowcommand="gvEditdept_RowCommand" >
                                                <Columns>
                                                    <asp:BoundField DataField="AgentID" HeaderText="AgentID">
                                                        <HeaderStyle Width="150" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdt" runat="server" Text="Edit" CommandArgument='<%#Bind("ID")%>'
                                                                ControlStyle-ForeColor="Blue" CommandName="Edt"></asp:LinkButton></ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                          </table>
                                        </td>
                                    </tr>

                                    

</table>

</asp:Content>

