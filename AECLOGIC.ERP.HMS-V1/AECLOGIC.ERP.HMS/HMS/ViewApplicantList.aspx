<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="ViewApplicantList.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
    Inherits="AECLOGIC.ERP.HMS.ViewApplicants" Title="View Applicants List"  %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
      <script language="javascript" type="text/javascript">
          function GetWorkID(source, eventArgs) {
              var HdnKey = eventArgs.get_value();
              // alert(HdnKey);
              document.getElementById('<%=ddlWs_hid.ClientID %>').value = HdnKey;
          }
    </script>
    <table width="100%">
        <tr>
            <td width="60%">
              
            </td>
            <td align="right">
                <asp:HyperLink ID="HyperLink2" NavigateUrl="http://humanresources.about.com/cs/selectionstaffing/a/hiringchecklist.htm"
                    runat="server" ForeColor="Blue" CssClass="btn btn-primary"><u>How to Recruit and Hire the Best</u></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td colspan="2" class="pageheader">
                                    Applicants List
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
                                                                Positions::
                                                                <asp:DropDownList ID="ddlPosition" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPosition_SelectedIndexChanged"
                                                                    Width="190px" TabIndex="1" AccessKey="1" ToolTip="[Alt+1]" CssClass="droplist">
                                                                </asp:DropDownList>
                                                                <cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search..." PromptPosition="Top" 
                                                                    PromptCssClass="PromptText" QueryPattern="Contains" runat="server" TargetControlID="ddlPosition"></cc1:ListSearchExtender>
                                                                &nbsp;Applicant Status::
                                                                <asp:DropDownList ID="ddlApplicantStatus" runat="server" OnSelectedIndexChanged="ddlPosition_SelectedIndexChanged"
                                                                    Width="126px" AutoPostBack="True" TabIndex="2" AccessKey="2" ToolTip="[Alt+2]"
                                                                    CssClass="droplist">
                                                                    <asp:ListItem Text="New Entry" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Next Round" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Rejected" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                &nbsp;
                                                                <asp:Button ID="Showall" runat="server" Text="Show" CssClass="btn btn-success" OnClick="Showall_Click"
                                                                    TabIndex="3" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />
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
                                    <asp:GridView ID="gvApplicantList" runat="server" AutoGenerateColumns="False" Width="100%"
                                        CssClass="gridview" HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found"
                                        EmptyDataRowStyle-CssClass="EmptyRowData" 
                                        onrowdatabound="gvApplicantList_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="ApplicantName" HeaderStyle-Width="20%" HeaderText="Applicant Name" />
                                            <asp:TemplateField HeaderText="Qualification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQua" runat="server" Text='<%#Eval("Qua")%>'></asp:Label>
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
                                            <asp:BoundField DataField="CurrentCTC" HeaderStyle-Width="7%" HeaderText="Current CTC">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ExpectedCTC" HeaderStyle-Width="7%" HeaderText="Expected CTC">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="WS" HeaderStyle-Width="7%" HeaderText="Worksite">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DOB" HeaderStyle-Width="7%" HeaderText="DOB">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                           
                                            <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Applied">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProgres" Text='<%#FormatInput(Eval("EntryType")) %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:HyperLinkField HeaderStyle-Width="4%" DataNavigateUrlFields="AppID" Text="View"
                                                DataNavigateUrlFormatString="ViewApplicantDetails.aspx?id={0}" HeaderText="Details" >
                                                <ItemStyle Width="80px" />
                                            </asp:HyperLinkField>
                                            <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Resume">
                                                <ItemTemplate>
                                                    <a id="A1" target="_blank" href='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "Resume").ToString(),DataBinder.Eval(Container.DataItem, "AppID").ToString()) %>'
                                                        runat="server" class="anchor__grd vw_grd">View</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 17px">
                                    <uc1:Paging ID="ViewApplilstPaging" runat="server" />
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
            </td>
        </tr>
    </table>
</asp:Content>
