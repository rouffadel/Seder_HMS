<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="ViewSelectedApplicantList.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
    Inherits="AECLOGIC.ERP.HMS.ViewSelectedApplicantList" Title="View Selected ApplicantList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
    <table width="100%">
        <tr>
            
            <td align="right">
                <asp:HyperLink ID="HyperLink1" NavigateUrl="http://humanresources.about.com/od/recruiting/tp/recruiting_employee.htm"
                    runat="server" ForeColor="Blue" CssClass="btn btn-primary"><u>10 Tips for Hiring Right Employee</u></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="pageheader">
                Candidates at Selected Stage
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    <Panes>
                        <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            Worksite:
                                            <asp:DropDownList Visible="false" ID="ddlWs" runat="server" CssClass="droplist">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtSearchWorksite" OnTextChanged="btnSearcWorksite" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                           
                                            &nbsp;&nbsp; Positions:
                                            <asp:DropDownList ID="ddlPosition" visible="false" runat="server" AutoPostBack="True" CssClass="droplist"
                                                TabIndex="1" AccessKey="1" ToolTip="[Alt+1]">
                                            </asp:DropDownList>
                                            <cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search..." PromptPosition="Top" 
                                     PromptCssClass="PromptText" QueryPattern="Contains" runat="server" TargetControlID="ddlPosition"></cc1:ListSearchExtender>
                                             <asp:TextBox ID="txtdept" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListDep" ServicePath="" TargetControlID="txtdept"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtdept"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]">
                                            </cc1:TextBoxWatermarkExtender>

                                            &nbsp;
                                            <asp:Button ID="Showall" runat="server" Text="Show" CssClass="btn btn-success" TabIndex="3"
                                                AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" OnClick="Showall_Click" />
                                        </td>
                                        <td>
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
                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gvApplicantList" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                                HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:BoundField DataField="ApplicantName" HeaderText="Applicant Name" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:TemplateField HeaderText="Qualification">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrga" runat="server" Text='<%#Eval("Qua")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Last Company">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrga" runat="server" Text='<%#Eval("Org")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Last Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesig" runat="server" Text='<%#Eval("Desig")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CurrentCTC" HeaderText="Current CTC" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ExpectedCTC" HeaderText="Expected CTC" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DOB" HeaderText="DOB" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Applied">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProgres" Text='<%#FormatInput(Eval("EntryType")) %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:HyperLinkField HeaderStyle-Width="4%" DataNavigateUrlFields="AppID,FPage" Text="View"
                                        DataNavigateUrlFormatString="ViewApplicantDetails.aspx?id={0}&FPage={1}" HeaderText="Details"  >
                                        <ItemStyle Width="80px" />
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField DataNavigateUrlFields="AppID" Text="Create" DataNavigateUrlFormatString="CreateOfferLetter.aspx?id={0}"
                                        HeaderText="Offer letter" HeaderStyle-HorizontalAlign="Left" >
                                        <ItemStyle />
                                    </asp:HyperLinkField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 17px">
                <uc1:Paging ID="ViewSelApplilstPaging" runat="server" Visible="true" />
            </td>
        </tr>
    </table>
 </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
