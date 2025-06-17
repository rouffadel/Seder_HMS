<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeDutyRoster.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmployeeDutyRoster"
    MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>  function GetEmpid(source, eventArgs) { var HdnKey = eventArgs.get_value(); document.getElementById('<%=hdSEmpid .ClientID %>').value = HdnKey; } 
</script>
    <!--THIS IS SAMPLE SEARCH FILTER-->
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        
        <tr>
            <td>
                <div id="NewView" runat="server">
                    <table width="100%">
                        <tr>
                            <td colspan="2" class="pageheader"></td>
                        </tr>
                        <tr>
                            <td style="width: 124px">
                                <asp:Label ID="lblEmpid" runat="server">Empid<span style="color: #ff0000">*</span></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEmpid" runat="server" Width="200px" MaxLength="30" TabIndex="2"></asp:DropDownList><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqEmpid" ValidationGroup="save" ControlToValidate="ddlEmpid" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator><asp:HiddenField ID="hdRid" runat="server" Value="0"></asp:HiddenField>
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 124px">
                                <asp:Label ID="lblReason" runat="server">Reason<span style="color: #ff0000">*</span></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtReason" runat="server" Width="200px" MaxLength="30" TabIndex="3"></asp:TextBox><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqReason" ValidationGroup="save" ControlToValidate="txtReason" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator></td>
                        </tr>

                        <tr>
                            <td style="width: 124px">
                                <asp:Label ID="lblDate" runat="server">Date<span style="color: #ff0000">*</span></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDate" runat="server" Width="200px" MaxLength="30" TabIndex="4"></asp:TextBox><cc1:CalendarExtender ID="calDate" runat="server" TargetControlID="txtDate" PopupButtonID="txtDate" Format="dd MMM yyyy" Enabled="true"></cc1:CalendarExtender>
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqDate" ValidationGroup="save" ControlToValidate="txtDate" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator></td>
                        </tr>

                        <tr>
                            <td style="padding-left: 125Px" colspan="2">
                                <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-success" Width="100px" AccessKey="s" TabIndex="5" OnClick="btnSubmit_Click" ToolTip="[Alt+s OR Alt+s+Enter]" ValidationGroup="save" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="EditView" runat="server">

                    <table width="100%">
                        <tr>
                            <td>
                                <cc1:Accordion ID="EdtViewAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="70%">
                                    <Panes>
                                        <cc1:AccordionPane ID="EdtViewAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                            ContentCssClass="accordionContent">
                                            <Header>
Search Criteria</Header>
                                            <Content>

                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtSEmpid" runat="server" Height="22px" Width="140px" AutoPostBack="false">  </asp:TextBox>
                                                            <asp:HiddenField ID="hdSEmpid" runat="server" Value="0" />
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtenderEmpid" runat="server" DelimiterCharacters="" Enabled="true" MinimumPrefixLength="1" ServiceMethod="GetCompletionEmpid" ServicePath="" TargetControlID="txtSEmpid" UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem" OnClientItemSelected="GetEmpid" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"></cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderEmpid" runat="server" TargetControlID="txtSEmpid" WatermarkCssClass="watermark" WatermarkText="[Enter Empid]"></cc1:TextBoxWatermarkExtender>
                                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
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
                                <asp:GridView ID="gvEV" OnRowCommand="gvEV_RowCommand" runat="server" OnClientClick="return confirm('Are you sure to Delete Record!')" AutoGenerateColumns="false" Width="50%" CssClass="gridview" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BorderColor="GhostWhite">
                                    <Columns>
                                        <asp:BoundField HeaderText="Rid" DataField="Rid" />
                                        <asp:BoundField HeaderText="Empid" DataField="Empid" />
                                        <asp:BoundField HeaderText="Reason" DataField="Reason" />
                                        <asp:BoundField HeaderText="Date" DataField="Date" />
                                        <asp:BoundField HeaderText="IsInService" DataField="IsInService" Visible="false" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd" Text="Edit" CommandArgument='<%#Eval("Rid")%>' CommandName="Edt"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CssClass="anchor__grd dlt" Text="Delete" CommandArgument='<%#Eval("Rid")%>' CommandName="Del"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 17px">
                                <uc1:Paging ID="EmpListPaging" runat="server" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
