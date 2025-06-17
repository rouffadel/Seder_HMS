<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustodianDocs.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.CustodianDocs" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlEmpid_hid.ClientID %>').value = HdnKey;
        }
        function GetWSID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlWorkSite_hid.ClientID %>').value = HdnKey;
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblStatus" Text="" Font-Size="14px"></asp:Label>
                        <table id="tblAllView" width="100%" runat="server">
                            <tr>
                                <td colspan="2" style="width: 100%">
                                    <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                        ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                        FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                                        SelectedIndex="0">
                                        <Panes>
                                            <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td>Employee Name:
                                                                <asp:HiddenField ID="ddlEmpid_hid" runat="server" />
                                                                <asp:TextBox ID="txtempid" runat="server" Height="21px" TabIndex="1"
                                                                    Width="200px"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtempid"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtempid"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Employee ID/NAME]"></cc1:TextBoxWatermarkExtender>
                                                                WorkSite:
                                                                <asp:HiddenField ID="ddlWorkSite_hid" runat="server" />
                                                                <asp:TextBox ID="txtSWorkSite" runat="server" AccessKey="w" Height="21px" ToolTip="[Alt+w OR Alt+w+Enter]" TabIndex="2"
                                                                    Width="200px"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSWorkSite"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetWSID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSWorkSite"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" TabIndex="4" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Doc Type :
                                                            <asp:DropDownList ID="ddldoctype" runat="server" AutoPostBack="true" CssClass="droplist"
                                                                OnSelectedIndexChanged="ddldoctype_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                                    Font-Bold="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                                                    <asp:ListItem Text="Emps with Docs at Co." Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Emps without Docs at Co." Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
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
                                    <asp:GridView ID="gvEmpList" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead" GridLines="Both"
                                        EmptyDataText="No Records Found" OnRowCommand="gvEmpList_RowCommand" Width="100%" CssClass="gridview">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Emp ID" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpId" runat="server" Text='<%#Eval("EmpId")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Select" CommandName="Sel" CommandArgument='<%#Eval("EmpId")%>' CssClass="anchor__grd edit_grd"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <uc1:Paging ID="EmpListPaging" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tblEmpView" runat="server" width="100%" visible="false">
                            <tr>
                                <td>
                                    <b>Employee Name: </b>&nbsp<asp:Label ID="lblname" runat="server" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GvEmpDocs" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead" CssClass="gridview"
                                        EmptyDataText="No Records Found" Width="100%" OnRowCommand="GvEmpDocs_RowCommand" GridLines="Both">
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocID" runat="server" Text='<%#Eval("DocID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doc Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("DocumentName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Worksite" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="180" />
                                                <HeaderStyle Width="180" />
                                                <ItemTemplate>
                                                    <asp:DropDownList Width="180" ID="grdddlworksites" runat="server" DataTextField="Site_Name"
                                                        CssClass="droplist" AutoPostBack="true" DataValueField="Site_ID" DataSource='<%# BindSites()%>'
                                                        OnSelectedIndexChanged="grdddlworksites_SelectedIndexChanged" SelectedIndex='<%# GetSiteIndex(Eval("SiteID").ToString())%>'>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Custody Holder" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle />
                                                <HeaderStyle />
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="grdddlHeads" runat="server" DataTextField="name" AutoPostBack="true"
                                                        CssClass="droplist" DataValueField="EmpId" DataSource='<%# BindHeads()%>' SelectedIndex='<%# GetInvHolderIndex(Eval("InvHolderID").ToString())%>'>
                                                    </asp:DropDownList>
                                                    </asp:DropDownList>
                                                         <asp:DropDownList ID="grdNewddlHeads" runat="server" DataTextField="name" AutoPostBack="true"
                                                             DataValueField="EmpId" DataSource='<%# BindHeads()%>' Visible="false">
                                                         </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upload Proof" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fuUploadProof" runat="server"></asp:FileUpload>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpload" runat="server" Text="Assign" CommandName="Upld" CommandArgument='<%#Eval("DocID")%>' CssClass="anchor__grd edit_grd"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Proof" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a id="A1" target="_blank" href='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "ProofID").ToString(),DataBinder.Eval(Container.DataItem, "Ext").ToString().ToString()) %>'
                                                        runat="server" visible='<%# Visble(DataBinder.Eval(Container.DataItem, "Ext").ToString().ToString()) %>' class="anchor__grd vw_grd">View</a>
                                                    <asp:Label ID="lblProofID" runat="server" Text='<%#Eval("Ext")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GvEmpDocs" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
