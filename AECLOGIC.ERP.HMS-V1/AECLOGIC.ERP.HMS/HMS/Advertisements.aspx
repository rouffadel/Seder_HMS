<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="Advertisements.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Advertisements" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
<script language="javascript" type="text/javascript">
     

    function validatesave() {

        if (!chkName('<%=txtAdvtName.ClientID%>', "Document Name", true, ""))
            return false;
    }
 
         
    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
<tr><td>
 
    &nbsp;</td></tr> <tr><td><asp:MultiView ID="mvAddvtizmnt" runat="server">
    <asp:View ID="Add" runat="server">
    <table style="width: 100%;"><tr><td>Enter Advertisement Name: 
        <asp:TextBox ID="txtAdvtName" Width="380px" runat="server"></asp:TextBox>   </td></tr>
        <tr><td></td></tr>
        <tr>
                                  <td  >
                                      <b> Note*</b>  Paste from Wordpad/Notepd (Don&#39;t use MS-Word ) to maintain stable format
                                  </td>
                                  
                               </tr>

        <tr>
                                  <td>
                                      <asp:TextBox runat="server" ID="DocEditor" TextMode="MultiLine" Columns="50" Rows="10"  Width="950px"  Height="600px"
                Text="Hello <b>world!</b>" /><br />
                                   <ajax:HtmlEditorExtender ID="htmlExtDocEditor"   EnableSanitization="false" TargetControlID="DocEditor"  runat="server"/>
                                    <br />
                                     <asp:Button id="btnSubmit" Text="Submit" Runat="server" CssClass="savebutton" onclick="btnSubmit_Click" 
                                     OnClientClick="javascript:return validatesave();"/>
                                  </td>
                              
                              </tr>
        </table>
    
    </asp:View>
    <asp:View ID="Edit" runat="server">
    <table style="width: 100%;"><tr><td>    </td></tr>
    
    <tr>
                                  <td>
                                      <asp:GridView ID="gvAdvtment" runat="server" AutoGenerateColumns="False" ForeColor="#333333" Width="600px" CellPadding="4" OnRowCommand="gvAdvtment_RowCommand"
                                 HeaderStyle-CssClass="tableHead"   CssClass="gridview" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="advtID" runat="server" Text='<%# Eval("AdvtID")%>'></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date")%>'></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    
                                    <asp:TemplateField HeaderText="Advertisement Name" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocName" runat="server" Text='<%# Eval("AdvtName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="tableHead" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" >
                                        <ItemTemplate>
                                             <asp:LinkButton ID="lnkStatus" runat="server" Text='<%# Eval("Status")%>' CommandName="Status" CommandArgument='<%# Eval("AdvtID")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" CssClass="tableHead" />
                                    </asp:TemplateField>
                                    <asp:TemplateField >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edt" CommandArgument='<%# Eval("AdvtID")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                     
                                    
                                </Columns>
                                <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                                <HeaderStyle CssClass="tableHead" />
                            </asp:GridView>
                                  </td>
                              
                              </tr>
    </table>
    
    
    
    </asp:View>
    </asp:MultiView> </td></tr></table>
    

</asp:Content>

