<%@ Page Language="C#"   AutoEventWireup="True" ValidateRequest="false" MasterPageFile="~/Templates/CommonMaster.master" 
    CodeBehind="ViewOfferLetterList.aspx.cs" Inherits="AECLOGIC.ERP.HMS.OfferLetterList" Title="View Offer Letter List" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
      <script language="javascript" type="text/javascript">
     function GetWorkID(source, eventArgs)
        {
            var HdnKey = eventArgs.get_value();
           // alert(HdnKey);
            document.getElementById('<%=ddlWs_hid.ClientID %>').value = HdnKey;
     }
           </script>
    <div>
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
                    Candidates at Offer Stage
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:Accordion ID="ViewAppLstAccordion" runat="server" HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="201%">
                        <Panes>
                            <cc1:AccordionPane ID="ViewAppLstAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                ContentCssClass="accordionContent">
                                <Header>
                                    Search Criteria</Header>
                                <Content>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td>
                                                Worksite:
                                              
                                                 <asp:HiddenField ID="ddlWs_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchWorksite"  AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetWorkID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                           </cc1:TextBoxWatermarkExtender>
                                                Positions:
                                                <asp:DropDownList ID="ddlPosition" runat="server" AutoPostBack="True" TabIndex="1"
                                                    AccessKey="1" ToolTip="[Alt+1]" CssClass="droplist">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search..." PromptPosition="Top" 
                                     PromptCssClass="PromptText" QueryPattern="Contains" runat="server" TargetControlID="ddlPosition"></cc1:ListSearchExtender>
                                                &nbsp;
                                                <asp:Button ID="Showall" runat="server" Text="Show" CssClass="btn btn-success" OnClick="Showall_Click"
                                                    TabIndex="3" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />
                                                &nbsp;
                                                <asp:Button ID="btnExpToXL" runat="server" CssClass="btn btn-primary" Text="Export to Excel" OnClick="btnExpToXL_Click"/>
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
                <td colspan="2">
                    <asp:GridView ID="gvApplicantList" DataKeyNames="AppID" runat="server" EmptyDataText="No Records Found"
                        EmptyDataRowStyle-CssClass="EmptyRowData" AutoGenerateColumns="False" Width="100%"
                        CssClass="gridview" HeaderStyle-CssClass="tableHead" OnRowCommand="gvApplicantList_RowCommand"
                        OnRowDataBound="gvApplicantList_RowDataBound" ForeColor="#333333" GridLines="Both">
                        <Columns>
                            <asp:BoundField DataField="ApplicantName" HeaderText="Applicant Name" HeaderStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="DOJ" HeaderText="Date of Join" HeaderStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Salary" HeaderText="CTC" HeaderStyle-HorizontalAlign="Left">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Designation" HeaderText="Position" HeaderStyle-HorizontalAlign="Left">
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField DataField="WS" HeaderText="Worksite" HeaderStyle-HorizontalAlign="Left">
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:HyperLinkField HeaderStyle-Width="4%" DataNavigateUrlFields="AppID" Text="View"
                                DataNavigateUrlFormatString="~/HMS/ViewApplicantDetails.aspx?id={0}" HeaderText="Details" >
                                <ItemStyle Width="80px" />
                            </asp:HyperLinkField>
                            <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Applied">
                                <ItemTemplate>
                                    <asp:Label ID="lblProgres" Text='<%#FormatInput(Eval("EntryType")) %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="8%">
                                <ItemTemplate>
                                    <asp:LinkButton OnClientClick='<%#ShowLetter(Eval("AppID"))%>' runat="server" CssClass="btn btn-primary">Offer Letter</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:HyperLinkField DataNavigateUrlFields="AppID" HeaderStyle-Width="4%" Text="Edit"
                                DataNavigateUrlFormatString="~/HMS/EditOfferLetter.aspx?id={0}" HeaderStyle-HorizontalAlign="Left"  >
                            </asp:HyperLinkField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAccept" runat="server" CommandArgument='<%#Eval("AppID")%>'
                                        OnClientClick="return confirm('Accept?');" CommandName="Accept" Text="Accept" CssClass="btn btn-primary"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkReject" runat="server" CommandArgument='<%#Eval("AppID")%>'
                                        OnClientClick="return confirm('Reject?');" CommandName="Reject" Text="Reject" CssClass="btn btn-danger"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 17px">
                    <uc1:Paging ID="ViewOfferlilstPaging" runat="server" />
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
    </div>
</asp:Content>
