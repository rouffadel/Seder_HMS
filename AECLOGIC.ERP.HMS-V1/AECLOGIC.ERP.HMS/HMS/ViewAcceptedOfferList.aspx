<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="ViewAcceptedOfferList.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
    Inherits="AECLOGIC.ERP.HMS.ViewAcceptedOfferList" Title="View Accepted OfferList"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
       <script language="javascript" type="text/javascript">
           function GetWorkID(source, eventArgs) {
               var HdnKey = eventArgs.get_value();
               // alert(HdnKey);
               document.getElementById('<%=ddlWs_hid.ClientID %>').value = HdnKey;
           }
    </script>
    
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
    <table width="100%">
        <tr>
            <td>
             
            </td>
        </tr>
        <tr>
            <td colspan="2" class="pageheader">
                Candidates at Appointment Stage
            </td>
        </tr>
        <tr>
            <td>
                <cc1:Accordion ID="ViewAppLstAccordion" runat="server" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%">
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
                                                AccessKey="1" ToolTip="[Alt+1]" CssClass="droplist" OnSelectedIndexChanged="ddlPosition_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search..." PromptPosition="Top" 
                                            PromptCssClass="PromptText" QueryPattern="Contains" runat="server" TargetControlID="ddlPosition"></cc1:ListSearchExtender>
                                            &nbsp;
                                            <asp:Button ID="Showall" runat="server" Text="Show" CssClass="btn btn-success" TabIndex="3"
                                                AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" OnClick="Showall_Click" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkJoinEmpLst" runat="server" Text="Show Joined Employees" OnCheckedChanged="chkJoinEmpLst_CheckedChanged"
                                                AutoPostBack="true" />
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
                <asp:GridView ID="gvAcceptedOfferList" runat="server" AutoGenerateColumns="False"
                    Visible="false" CssClass="gridview" Width="100%" HeaderStyle-CssClass="tableHead"
                    OnRowCommand="gvAcceptedOfferList_RowCommand" ForeColor="#333333" GridLines="Both"
                    EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" OnRowDataBound="gvAcceptedOfferList_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="ApplicantName" HeaderText="Applicant Name" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="DOJ" HeaderText="Date of Join" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Salary" HeaderText="CTC" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Designation" HeaderText="Position" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="WS" HeaderText="Worksite" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle />
                        </asp:BoundField>
                        <asp:HyperLinkField HeaderStyle-Width="4%" DataNavigateUrlFields="AppID" Text="View"
                            DataNavigateUrlFormatString="~/HMS/ViewApplicantDetails.aspx?id={0}" HeaderText="Details">
                            <ItemStyle Width="80px" />
                        </asp:HyperLinkField>
                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Applied">
                            <ItemTemplate>
                                <asp:Label ID="lblProgres" Text='<%#FormatInput(Eval("EntryType")) %>' runat="server" CssClass="btn btn-primary" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField HeaderStyle-Width="8%" DataNavigateUrlFields="AppID" Text="Offer Letter"
                            DataNavigateUrlFormatString="~/HMS/OfferLetterPreview.aspx?id={0}">
                            <ItemStyle />
                        </asp:HyperLinkField>
                        <asp:TemplateField HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkJoin" runat="server" CommandArgument='<%#Eval("AppID")%>'
                                    CommandName="Join" OnClientClick="return confirm('Join?');" Text="Join" CssClass="btn btn-primary"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
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
            <td colspan="2">
                <asp:GridView ID="grdJoinedEmp" runat="server" AutoGenerateColumns="False" Visible="false"
                    CssClass="gridview" Width="100%" HeaderStyle-CssClass="tableHead" OnRowCommand="gvAcceptedOfferList_RowCommand"
                    ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData">
                    <Columns>
                        <asp:BoundField DataField="ApplicantName" HeaderText="Applicant Name" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="DOJ" HeaderText="Date of Join" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Salary" HeaderText="CTC" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Designation" HeaderText="Position" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="WS" HeaderText="Worksite" HeaderStyle-HorizontalAlign="Left">
                            <ItemStyle />
                        </asp:BoundField>
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
                <uc1:Paging ID="ViewAccOfferlilstPaging" runat="server" />
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
