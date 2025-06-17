<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustodianDocument.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"  Inherits="AECLOGIC.ERP.HMS.CustodianDocument" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" >

        function validatesave() {
            if (document.getElementById('<%=txtDocName.ClientID%>').value == "") {
                alert("Enter Document Name.!");
                return false;
            }
    }

</script>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
     <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
       
         <tr>
             <td>
                 <table id="tbladd" runat="server" visible="false">
                     <tr>
                         <td><b>
                              Document Name</b> <span style="color: #ff0000">*</span>
                         </td>
                         <td>
                             <asp:TextBox ID="txtDocName" runat="server" Width="360px" TabIndex="1"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td valign="top"><b>
                             Document Procedure</b>
                         </td>
                         <td>
                             <asp:TextBox ID="txtProcedure" runat="server" TextMode="MultiLine" Height="107px" TabIndex="2"
                                Width="287px"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td>
                               <b>Sample Document</b>
                         </td>
                         <td>
                             <asp:FileUpload ID="fuUploadProof" runat="server" TabIndex="3" />
                         </td>
                     </tr>
                     <tr>
                          <td colspan="2">
                              <asp:Button ID="btnsave" Text="Submit" CssClass="btn btn-primary" Width="100px" runat="server" OnClick="btnsave_Click"
                                  OnClientClick="javascript:return validatesave();" />
                         </td>
                     </tr>
                 </table>
             </td>
         </tr>
         <tr>
             <td>
                 <table id ="tblList" runat="server" visible="false" width="100%">
                     <tr>
                         <td>
                               <asp:RadioButton ID="rbShowActive" AutoPostBack="true" runat="server" Checked="True" TabIndex="1" AccessKey="1" ToolTip="[Alt+1]"
                                      GroupName="show" Text="Active" OnCheckedChanged="rbShow_CheckedChanged" Style="font-weight: bold" />
                                        <asp:RadioButton ID="rbShowInactive" AutoPostBack="true" runat="server" GroupName="show"
                                            Text="Deleted" OnCheckedChanged="rbShow_CheckedChanged" Style="font-weight: bold" />
                         </td>
                     </tr>
                     <tr>
                         <td>
                             <asp:GridView ID="gvDocs" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                                                        EmptyDataText="No Records" width="100%" OnRowCommand="gvDocs_RowCommand"
                                 ForeColor="#333333" GridLines="Both" EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview">
                                 <Columns>
                                     <asp:TemplateField HeaderText="Document Name" HeaderStyle-HorizontalAlign="Left">
                                         <ItemTemplate>
                                             <asp:Label ID="lblDocs" runat="server" Text='<%#Eval("DocumentName")%>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Procedure" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProcedure" runat="server" Text='<%#Eval("DocProcedure")%>'></asp:Label>
                                        </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Form" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                             <a id="A1" target="_blank"  CssClass ="anchor__grd vw_grd" href ='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "Docid").ToString(),DataBinder.Eval(Container.DataItem, "Ext").ToString().ToString()) %>' visible='<%# Visble(DataBinder.Eval(Container.DataItem, "Ext").ToString().ToString()) %>' runat="server"  >View</a>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                      <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd " Text="Edit" CommandName="Edt" CommandArgument='<%#Eval("Docid")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                     </asp:TemplateField>
                                      <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDel" runat="server" CssClass="anchor__grd dlt "  Text='<%#GetText()%>' CommandArgument='<%#Eval("Docid")%>'
                                                    CommandName="Del"></asp:LinkButton></ItemTemplate>
                                            </asp:TemplateField>
                                 </Columns>
                             </asp:GridView>
                         </td>
                     </tr>
                      <tr>
                <td style="height: 17px">
                    <uc1:Paging ID="AdvancedLeaveAppOthPaging" runat="server" />
                </td>
            </tr>
                 </table>
             </td>
         </tr>
          </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnsave" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" 
ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>