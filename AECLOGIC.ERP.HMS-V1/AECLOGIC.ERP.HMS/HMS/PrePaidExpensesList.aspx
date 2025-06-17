<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrePaidExpensesList.aspx.cs" Inherits="AECLOGIC.ERP.HMS.PrePaidExpensesList"
    MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>  function GetResourceID(source, eventArgs) { var HdnKey = eventArgs.get_value(); document.getElementById('<%=hdSResourceID .ClientID %>').value = HdnKey; }
        function GetModuleID(source, eventArgs) { var HdnKey = eventArgs.get_value(); document.getElementById('<%=hdSModuleID .ClientID %>').value = HdnKey; }
        function GetwsiD(source, eventArgs) { var HdnKey = eventArgs.get_value(); document.getElementById('<%=hdSwsiD .ClientID %>').value = HdnKey; }
        function GetprjID(source, eventArgs) { var HdnKey = eventArgs.get_value(); document.getElementById('<%=hdSprjID .ClientID %>').value = HdnKey; } 
</script>
    <!--THIS IS SAMPLE SEARCH FILTER-->
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <AEC:Topmenu ID="topmenu" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="NewView" runat="server">
                            <table width="100%">
                                <tr>
                                    <td colspan="2" class="pageheader"></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:Label ID="lblTransID" runat="server">TransID<span style="color: #ff0000">*</span></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTransID" runat="server" Width="200px" MaxLength="30" TabIndex="2"></asp:DropDownList><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqTransID" ValidationGroup="save" ControlToValidate="ddlTransID" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator><asp:HiddenField ID="hdMpEtID" runat="server" Value="0"></asp:HiddenField>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:Label ID="lblTransamt" runat="server">Transamt<span style="color: #ff0000">*</span></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTransamt" runat="server" Width="200px" MaxLength="30" TabIndex="3"></asp:TextBox><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqTransamt" ValidationGroup="save" ControlToValidate="txtTransamt" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:Label ID="lblResourceID" runat="server">ResourceID<span style="color: #ff0000">*</span></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlResourceID" runat="server" Width="200px" MaxLength="30" TabIndex="4"></asp:DropDownList><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqResourceID" ValidationGroup="save" ControlToValidate="ddlResourceID" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:Label ID="lblModuleID" runat="server">ModuleID<span style="color: #ff0000">*</span></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlModuleID" runat="server" Width="200px" MaxLength="30" TabIndex="5"></asp:DropDownList><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqModuleID" ValidationGroup="save" ControlToValidate="ddlModuleID" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:Label ID="lblcn" runat="server">cn<span style="color: #ff0000">*</span></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtcn" runat="server" Width="200px" MaxLength="30" TabIndex="6"></asp:TextBox><cc1:CalendarExtender ID="calcn" runat="server" TargetControlID="txtcn" PopupButtonID="txtcn" Format="dd MMM yyyy" Enabled="true"></cc1:CalendarExtender>
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqcn" ValidationGroup="save" ControlToValidate="txtcn" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:Label ID="lblcb" runat="server">cb<span style="color: #ff0000">*</span></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlcb" runat="server" Width="200px" MaxLength="30" TabIndex="7"></asp:DropDownList><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqcb" ValidationGroup="save" ControlToValidate="ddlcb" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:Label ID="lblNoOfMonths" runat="server">NoOfMonths<span style="color: #ff0000">*</span></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlNoOfMonths" runat="server" Width="200px" MaxLength="30" TabIndex="8"></asp:DropDownList><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqNoOfMonths" ValidationGroup="save" ControlToValidate="ddlNoOfMonths" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:Label ID="lblBillTransID" runat="server">BillTransID<span style="color: #ff0000">*</span></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBillTransID" runat="server" Width="200px" MaxLength="30" TabIndex="9"></asp:DropDownList><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqBillTransID" ValidationGroup="save" ControlToValidate="ddlBillTransID" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:Label ID="lblwsiD" runat="server">wsiD<span style="color: #ff0000">*</span></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlwsiD" runat="server" Width="200px" MaxLength="30" TabIndex="10"></asp:DropDownList><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqwsiD" ValidationGroup="save" ControlToValidate="ddlwsiD" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:Label ID="lblprjID" runat="server">prjID<span style="color: #ff0000">*</span></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlprjID" runat="server" Width="200px" MaxLength="30" TabIndex="11"></asp:DropDownList><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqprjID" ValidationGroup="save" ControlToValidate="ddlprjID" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator></td>
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
                                                                    <asp:TextBox ID="txtSResourceID" runat="server" Height="22px" Width="140px" AutoPostBack="True">  
                                                                    </asp:TextBox>
                                                                    <asp:HiddenField ID="hdSResourceID" runat="server" Value="0" />
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderResourceID" runat="server"
                                                                        DelimiterCharacters="" Enabled="true" MinimumPrefixLength="1"
                                                                        ServiceMethod="GetCompletionResourceID" ServicePath="" TargetControlID="txtSResourceID"
                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" OnClientItemSelected="GetResourceID" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderResourceID" runat="server"
                                                                        TargetControlID="txtSResourceID" WatermarkCssClass="watermark" WatermarkText="[Enter ResourceID]"></cc1:TextBoxWatermarkExtender>
                                                                    <asp:TextBox ID="txtSModuleID" runat="server" Height="22px" Width="140px" AutoPostBack="True">  </asp:TextBox>
                                                                    <asp:HiddenField ID="hdSModuleID" runat="server" Value="0" />
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderModuleID" runat="server" DelimiterCharacters="" Enabled="true" MinimumPrefixLength="1" ServiceMethod="GetCompletionModuleID" ServicePath="" TargetControlID="txtSModuleID" UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem" OnClientItemSelected="GetModuleID" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"></cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderModuleID" runat="server" TargetControlID="txtSModuleID" WatermarkCssClass="watermark" WatermarkText="[Enter ModuleID]"></cc1:TextBoxWatermarkExtender>
                                                                    <asp:TextBox ID="txtSwsiD" runat="server" Height="22px" Width="140px" AutoPostBack="True">  </asp:TextBox>
                                                                    <asp:HiddenField ID="hdSwsiD" runat="server" Value="0" />
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderwsiD" runat="server" DelimiterCharacters="" Enabled="true" MinimumPrefixLength="1"
                                                                         ServiceMethod="GetCompletionwsiD" ServicePath="" TargetControlID="txtSwsiD" UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem" OnClientItemSelected="GetwsiD" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"></cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderwsiD" runat="server" TargetControlID="txtSwsiD" WatermarkCssClass="watermark" WatermarkText="[Enter wsiD]"></cc1:TextBoxWatermarkExtender>
                                                                    <asp:TextBox ID="txtSprjID" runat="server" Height="22px" Width="140px" AutoPostBack="True">  </asp:TextBox>
                                                                    <asp:HiddenField ID="hdSprjID" runat="server" Value="0" />
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderprjID" runat="server" DelimiterCharacters="" Enabled="true" MinimumPrefixLength="1"
                                                                         ServiceMethod="GetCompletionprjID" ServicePath="" TargetControlID="txtSprjID" UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem" OnClientItemSelected="GetprjID" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"></cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderprjID" runat="server" TargetControlID="txtSprjID" WatermarkCssClass="watermark" WatermarkText="[Enter prjID]"></cc1:TextBoxWatermarkExtender>
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
                                        <asp:GridView ID="gvEV" OnRowCommand="gvEV_RowCommand" runat="server"
                                            OnClientClick="return confirm('Are you sure to Delete Record!')"
                                            AutoGenerateColumns="false" Width="100%" CssClass="gridview" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BorderColor="GhostWhite">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PEID">
                                                    <ItemTemplate>
                                                        <asp:LinkButton CommandName="MpEtID" Text='<%#Eval("MpEtID")%>' runat="server" ID="lnkMpEtID"
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"MpEtID") %>'
                                                            Font-Bold="False" Font-Underline="True" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="MpEtID" DataField="MpEtID" />--%>
                                                <asp:BoundField HeaderText="WoNo" DataField="wono" />
                                                <asp:BoundField HeaderText="Worksite" DataField="wsName" />
                                                <asp:BoundField HeaderText="Project" DataField="prjName" />
                                                <asp:BoundField HeaderText="Module" DataField="Module" />
                                                <asp:BoundField HeaderText="Resource" DataField="ResouceName" />
                                                <asp:BoundField HeaderText="Bill TrID" DataField="BillTransID" />
                                                <asp:BoundField HeaderText="Prepaid TrID" DataField="TransID" />
                                                <asp:BoundField HeaderText="Prepaid Amt" DataField="Transamt" />
                                                <asp:BoundField HeaderText="Posted Amt" DataField="PostedAmt" />
                                                <asp:BoundField HeaderText="Created On" DataField="cn" />
                                                <asp:BoundField HeaderText="Created By" DataField="empname" />
                                                <asp:BoundField HeaderText="From" DataField="FromDt" />
                                                <asp:BoundField HeaderText="To" DataField="ToDt" />
                                                <asp:BoundField HeaderText="Months" DataField="NoOfMonths" />
                                                <%-- <asp:BoundField HeaderText="ia" DataField="ia" />--%>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd" Text="Edit" CommandArgument='<%#Eval("MpEtID")%>' CommandName="Edt"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="anchor__grd dlt" Text="Delete" CommandArgument='<%#Eval("MpEtID")%>' CommandName="Del"></asp:LinkButton>
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
                        <div id="MonthWiseDet" runat="server">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label Text="Prepaid Expenses Reversing Details" runat="server" Font-Size="12" ForeColor="RoyalBlue" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvDet" OnRowCommand="gvDet_RowCommand" runat="server"
                                            OnClientClick="return confirm('Are you sure to Delete Record!')"
                                            AutoGenerateColumns="false" Width="40%" CssClass="gridview" EmptyDataText="No Records Found"
                                            EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BorderColor="GhostWhite">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="PEID" DataField="MpEtID" ControlStyle-Width="40px" />
                                                <asp:BoundField HeaderText="DetID" DataField="MpEtDetID" ControlStyle-Width="40px" />
                                                <asp:BoundField HeaderText="Month Year" DataField="MonthYear" ControlStyle-Width="60px" />
                                                <asp:BoundField HeaderText="TransID" DataField="mTransID" />
                                                <asp:BoundField HeaderText="Created On" DataField="cn" />
                                                <asp:BoundField HeaderText="Amount" DataField="Amt" />
                                                <%--<asp:BoundField HeaderText="ia" DataField="ia" />--%>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd" Text="Edit" CommandArgument='<%#Eval("MpEtID")%>' CommandName="Edt"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="anchor__grd dlt" Text="Delete" CommandArgument='<%#Eval("MpEtID")%>' CommandName="Del"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 17px">
                                        <uc1:Paging ID="PagingDet" runat="server" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
