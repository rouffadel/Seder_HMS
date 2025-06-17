<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalarySignOffStatement.aspx.cs" Inherits="AECLOGIC.ERP.HMS.SalarySignOffStatement" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>  function GetEmpID(source, eventArgs) { var HdnKey = eventArgs.get_value(); document.getElementById('<%=hdSEmpID .ClientID %>').value = HdnKey; } 
</script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
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
                                        <asp:Label ID="lblEmpID" runat="server">EmpID<span style="color: #ff0000">*</span></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEmpID" runat="server" Width="200px" MaxLength="30" TabIndex="2"></asp:DropDownList><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqEmpID" ValidationGroup="save" ControlToValidate="ddlEmpID" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator><asp:HiddenField ID="hdID" runat="server" Value="0"></asp:HiddenField>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:Label ID="lblprifix" runat="server">prifix<span style="color: #ff0000">*</span></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtprifix" runat="server" Width="200px" MaxLength="30" TabIndex="3"></asp:TextBox><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqprifix" ValidationGroup="save" ControlToValidate="txtprifix" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:Label ID="lblDisplayDesignation" runat="server">DisplayDesignation<span style="color: #ff0000">*</span></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDisplayDesignation" runat="server" Width="200px" MaxLength="30" TabIndex="4"></asp:TextBox><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqDisplayDesignation" ValidationGroup="save" ControlToValidate="txtDisplayDesignation" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:Label ID="lblOrderOfSing" runat="server">OrderOfSing<span style="color: #ff0000">*</span></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlOrderOfSing" runat="server" Width="200px" MaxLength="30" TabIndex="5"></asp:DropDownList><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqOrderOfSing" ValidationGroup="save" ControlToValidate="ddlOrderOfSing" SetFocusOnError="true" InitialValue=""></asp:RequiredFieldValidator></td>
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
                                                                    <asp:TextBox ID="txtSEmpID" runat="server" Height="22px" Width="140px" AutoPostBack="True">  </asp:TextBox>
                                                                    <asp:HiddenField ID="hdSEmpID" runat="server" Value="0" />
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtenderEmpID" runat="server" 
                                                                        DelimiterCharacters="" Enabled="true" MinimumPrefixLength="1" ServiceMethod="GetCompletionEmpID" ServicePath="" TargetControlID="txtSEmpID" UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem" OnClientItemSelected="GetEmpID" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"></cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderEmpID" runat="server" TargetControlID="txtSEmpID" WatermarkCssClass="watermark" WatermarkText="[Enter EmpID]"></cc1:TextBoxWatermarkExtender>
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
                                                <asp:BoundField HeaderText="ID" DataField="ID" />
                                                <asp:BoundField HeaderText="Emp Name" DataField="EmpName" />
                                                <asp:BoundField HeaderText="prifix" DataField="prifix" />
                                                <asp:BoundField HeaderText="DisplayDesignation" DataField="DisplayDesignation" />
                                                <asp:BoundField HeaderText="OrderOfSing" DataField="OrderOfSing" />
                                                <asp:BoundField HeaderText="IsActive" DataField="IsActive" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd" Text="Edit" CommandArgument='<%#Eval("ID")%>' CommandName="Edt"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="anchor__grd dlt" Text="Delete" CommandArgument='<%#Eval("ID")%>' CommandName="Del"></asp:LinkButton>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
