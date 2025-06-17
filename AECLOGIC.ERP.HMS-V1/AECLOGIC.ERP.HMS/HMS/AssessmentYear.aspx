<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="AssessmentYear.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.AssessmentYear" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>

    <script language="javascript" type="text/javascript">

        function validatesave() {

            if (!chkNumber('<%=txtName.ClientID %>', "Assement Year", "true", ""))
                return false;

          <%--  if (!chkDate('<%=txtFromDate.ClientID%>', "FromDate", "true", ""))
                return false;

            if (!chkDate('<%=txtToDate.ClientID%>', "ToDate", "true", ""))
                return false;--%>
        }
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlaasignid_hid.ClientID %>').value = HdnKey;
        }
    </script>
     <asp:updatepanel runat="server" ID="UpdatePanel1">
  <ContentTemplate>

        <div id="dvAdd" runat="server">
                                <asp:Panel ID="Panel6" runat="server" CssClass="DivBorderOlive" Width="50%">

                <table id="tblNew" runat="server"  >
                    <tr>
                        <td colspan="2" class="pageheader">
                             New Assessment Year
                         </td>
                    </tr>
                    <tr>
                        <td>
                            Assessment Year<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtName"
                                                WatermarkCssClass="Watermarktxtbox" WatermarkText="[Enter Year]">
                                            </cc1:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            From Date<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" OnTextChanged="txtFromDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                PopupButtonID="txtDOB" Format="dd MMM yyyy">
                            </cc1:CalendarExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtFromDate"
                                                WatermarkCssClass="Watermarktxtbox" WatermarkText="[Select Fromdate]">
                                            </cc1:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            To Date<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                PopupButtonID="txtDOB" Format="dd MMM yyyy">
                            </cc1:CalendarExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtToDate"
                                                WatermarkCssClass="Watermarktxtbox" WatermarkText="[Select Todate]">
                                            </cc1:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" Width="100px"
                                OnClick="btnSubmit_Click" OnClientClick="javascript:return validatesave();" />
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                </div>
                <br />
                <div id="dvEdit" runat="server">
                                 

                <table id="tblEdit" runat="server" width="80%">
                       <tr>
                        <td>
                  <cc1:Accordion ID="DesigAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="DesigAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 80%">
                                                        <tr>
                                                            <td style="padding-right:0px">
                                                                        <asp:HiddenField ID="ddlaasignid_hid" runat="server" />
                                                                        <asp:TextBox ID="txtsearchassignment" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletiondeptList" ServicePath="" TargetControlID="txtsearchassignment"
                                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                                        </cc1:AutoCompleteExtender>
                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtsearchassignment"
                                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Assessment Year]"></cc1:TextBoxWatermarkExtender>
                                                                    </td>
                                                                  <td style="padding-right:530px">
                                                                        <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="btn btn-primary" />
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
                        <td  >
                            <asp:GridView ID="gvFinancialYear" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                OnRowCommand="gvFinancialYear_RowCommand" HeaderStyle-CssClass="tableHead" 
                                CssClass="gridview" Width="1000px">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                <Columns>
                                
                                 <asp:TemplateField HeaderText="Assessment Year">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("AssessmentYear")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="From Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHoliday" runat="server" Text='<%#Eval("FromDate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="To Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDays" runat="server" Text='<%#Eval("ToDate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("AssYearId")%>'
                                                CommandName="Edt"></asp:LinkButton></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
 
                </div>
 </ContentTemplate>
</asp:updatepanel>
<asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
  <ProgressTemplate>
   <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
    please wait...
  </ProgressTemplate>
 </asp:UpdateProgress>
</asp:Content>

