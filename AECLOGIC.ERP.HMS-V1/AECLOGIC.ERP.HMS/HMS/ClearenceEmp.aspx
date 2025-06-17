<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClearenceEmp.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ClearenceEmp" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
          //  alert(HdnKey);
            document.getElementById('<%=txtSearchDeprt_hid.ClientID %>').value = HdnKey;
        }
        function Confirm() {
            var con = confirm("Do you want delete this Record ?");
            if (con == true) {
                return true;
            }
            else {
                return false;
            }
        }

            </script>
     <asp:updatepanel runat="server" ID="UpdatePanel1">
  <ContentTemplate>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
          
         <tr>
            <td>
                 <asp:MultiView ID="mainview" runat="server">
                    <asp:View ID="Newvieew" runat="server">
                        <table width="100%">
                            <tr>
                                <td colspan="2" class="pageheader">
                                </td>
                            </tr>
                             <tr>
                            <td style="width: 124px">
                                 <b>Dept Name</b>   <span style="color: #ff0000">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DptName" CssClass="droplist" Width="20%" Height="100%" runat="server" TabIndex="3">   
                                    </asp:DropDownList>
                                     <cc1:ListSearchExtender ID="ListSearchExtender3" QueryPattern="Contains" runat="server" TargetControlID="DptName" PromptText="Type to search..." PromptCssClass="PromptText" PromptPosition="Top" IsSorted="true"></cc1:ListSearchExtender>
                                    </td>
                            </tr>
                             <tr>
                                <td align="left" style="width: 72px">
                                 <b>   Clearance Type </b> <span style="color: red">*</span>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtname" runat="server" Width="316px" MaxLength="1000" TabIndex="1"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td style="padding-left: 125Px" colspan="2">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="btn btn-success"
                                        Width="100px" OnClientClick="javascript:return validate();" AccessKey="s" TabIndex="5"
                                        ToolTip="[Alt+s OR Alt+s+Enter]" />
                                </td>
                            </tr>
                            </table>
                        </asp:View>
                      <asp:View ID="EditView" runat="server">
                        <table width="90%">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="EdtViewAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="75%">
                                        <Panes>
                                            <cc1:AccordionPane ID="EdtViewAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                        <td>
                                                              <b>  Department Name </b> &nbsp
                                                                <asp:TextBox ID="txtSItemname" runat="server" Visible="false"></asp:TextBox>
                                                             <asp:HiddenField ID="txtSearchDeprt_hid" runat="server" />
                                                                                                         <asp:TextBox ID="txtSearchDeprt" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchDeprt"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchDeprt"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]">
                                           </cc1:TextBoxWatermarkExtender>

                                                                &nbsp
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" 
                                                                                        Enabled="True"
                                                                              MinimumPrefixLength="1" ServiceMethod="GetSearchVendorDetails" ServicePath="" TargetControlID="txtSItemname"
                                                                                 UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                                  CompletionListItemCssClass="autocomplete_listItem" 
                                                                                 CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                     </cc1:AutoCompleteExtender>
                                                                     <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" 
                                                                             TargetControlID="txtSItemname"
                                                                                  WatermarkCssClass="watermark" WatermarkText="[Enter Item to Search]" Enabled="True">
                                                                    </cc1:TextBoxWatermarkExtender>
                                                             </td>
                                                        <td>
                                                            <asp:RadioButton ID="rbShowActive" AutoPostBack="true" runat="server" Checked="True" TabIndex="1" AccessKey="1" ToolTip="[Alt+1]"
                                                          GroupName="show" Text="Active" OnCheckedChanged="rbShow_CheckedChanged" Style="font-weight: bold" />
                                                            <asp:RadioButton ID="rbShowInactive" AutoPostBack="true" runat="server" GroupName="show"
                                                                Text="Deleted" OnCheckedChanged="rbShow_CheckedChanged" Style="font-weight: bold" />
                                                        </td>
                                                        </tr>
                                                    </table>
                                                </Content>
                                            </cc1:AccordionPane>
                                        </Panes>
                                    </cc1:Accordion>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvWS" runat="server" AutoGenerateColumns="False" Width="75%" OnRowCommand="gdvWS_RowCommand"
                                         HeaderStyle-CssClass="tableHead" AllowSorting="True"
                                        CssClass="gridview" EmptyDataText="No Records Found"
                                        EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BorderColor="GhostWhite">
                                        <Columns>
                                                    <asp:TemplateField HeaderText="DepartmentUId" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitem" runat="server" Text='<%#Bind("ClearID")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                     </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DepartmentUId" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbdeptid" runat="server" Text='<%#Bind("departmentuid")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="DepartmentUId" Visible="false">
                                                            <ItemTemplate>
                                                                    <asp:Label ID="lbldepartID" runat="server" Text='<%#Bind("departmentname")%>' Visible="true"></asp:Label>
                                                             </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="departmentname" HeaderText="Department" ItemStyle-HorizontalAlign="Left">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Clearance Type">
                                                        <HeaderStyle Width="350" />
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblCategary" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"itemname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField>
                                                             <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd" Text="Edit" CommandArgument='<%#Eval("itemname")%>'
                                                                             CommandName="Edt" ></asp:LinkButton>
                                                             </ItemTemplate>
                                                                    <ItemStyle Width="50px" />
                                                                    <HeaderStyle Width="50px" />
                                                     </asp:TemplateField>
                                                
                                            <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDel" runat="server" CssClass="anchor__grd dlt "  Text='<%#GetText()%>' CommandArgument='<%#Eval("ClearID")%>'
                                                    CommandName="del"></asp:LinkButton></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 17px">
                                    <uc1:Paging ID="EmpListPaging" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
           </td>
        </tr>
         <asp:HiddenField ID="hdn" runat="server" />
</table>
      </ContentTemplate>
</asp:updatepanel>
<asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
  <ProgressTemplate>
   <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" 
/>
    please wait...
  </ProgressTemplate>

 </asp:UpdateProgress>
</asp:Content>

