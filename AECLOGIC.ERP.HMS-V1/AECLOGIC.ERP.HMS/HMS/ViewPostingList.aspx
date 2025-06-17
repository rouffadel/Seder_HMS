<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="ViewPostingList.aspx.cs"  
    Inherits="AECLOGIC.ERP.HMS.ViewPostings" MasterPageFile="~/Templates/CommonMaster.master" Title="View Posting"   %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        //        function showWindowDailog(url, width, height) {
        //            var newWindow = window.showModalDialog(url, '', 'dialogHeight: ' + height + 'px; dialogWidth: ' + width + 'px; edge: Raised; center: Yes; resizable: Yes; status: No;');
        //        }
        // 
        function showpreview(id) {
            window.open('EditPostingDetails.aspx?id=' + id, 'PrintWindow', 'width=800,height=700,toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes');
        }
        function AddNewItem() {
            retval = window.showModalDialog("EditPostingDetails.aspx?id=PosID", "", "dialogheight:500px;dialogwidth:400px;status:no;edge:sunken;unadorned:no;resizable:no;");
            if (retval == 1) {
                window.location.href = "ViewPostingList.aspx";
            }
            else {
                return false;
            }
        }

        function GetWorkID(source, eventArgs)
        {
            var HdnKey = eventArgs.get_value();
           // alert(HdnKey);
            document.getElementById('<%=ddlWs_hid.ClientID %>').value = HdnKey;
        }
    </script>
    
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
        
        <tr>
            <td>
                <cc1:Accordion ID="ViewPostingAccordion" runat="server" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%">
                    <Panes>
                        <cc1:AccordionPane ID="ViewPostingAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="rblstStatus" runat="server" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rblstStatus_SelectedIndexChanged" AutoPostBack="true"
                                                TabIndex="1" AccessKey="1" ToolTip="[Alt+1]">
                                                <asp:ListItem Text="Opened Positions" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Closed Positions" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
                                           <%-- <div class="UpdateProgressCSS">
                                                <ajax:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
                                                    <ProgressTemplate>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" />
                                                        please wait...
                                                    </ProgressTemplate>
                                                </ajax:UpdateProgress>
                                            </div>--%>
                                        </td>
                                    </tr>
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

                                            &nbsp;&nbsp; Position:
                                            <asp:TextBox ID="txtposition" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtposition"
                                                WatermarkCssClass="watermark" WatermarkText="[Ex:Accountant]">
                                            </cc1:TextBoxWatermarkExtender>
                                            Experience:<asp:TextBox ID="txtExp" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtExp"
                                                WatermarkCssClass="watermark" WatermarkText="[Ex:3]">
                                            </cc1:TextBoxWatermarkExtender>
                                            <cc1:FilteredTextBoxExtender FilterType="Numbers" TargetControlID="txtExp" ValidChars="."
                                                runat="server">
                                            </cc1:FilteredTextBoxExtender>
                                            &nbsp;&nbsp; Date:
                                            <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="txtWMEDate" runat="server" TargetControlID="txtDate" WatermarkText="[Select date]"></cc1:TextBoxWatermarkExtender>
                                            <cc1:FilteredTextBoxExtender ID="txtdateTxtDate" runat="server" FilterType="Custom,Numbers"
                                                TargetControlID="txtDate" ValidChars="/" />
                                            <cc1:CalendarExtender ID="caltxtDate" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                CssClass="btn btn-primary" Width="80px" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]"
                                                TabIndex="6" />
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
                <asp:GridView ID="gvPosting" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    CssClass="gridview" OnRowDataBound="gvPosting_RowDataBound" ForeColor="#333333"
                    EmptyDataText="No Records Found" OnRowCommand="gvPosting_RowCommand" EmptyDataRowStyle-CssClass="EmptyRowData"
                    HeaderStyle-CssClass="tableHead"  >
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                    <Columns>
                        <asp:BoundField DataField="Position" HeaderText="Position" HeaderStyle-HorizontalAlign="Left"
                            HeaderStyle-Width="15%"></asp:BoundField>
                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="FromDate" HeaderText="From Date" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="ToDate" HeaderText="To Date" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="Timings" HeaderText="Timings" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="Posts" HeaderText="Posts" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="Exp">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "ExpFrom")%>
                                        </td>
                                        <td><span style="color: #E8E8E8">|</span></td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "ExpTo")%>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Qualifications" HeaderText="Qualifications" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="InterviewType" HeaderText="InterviewType" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a id="A1" target="_blank" href='<%# String.Format("EditPostingDetails.aspx?id={0}",DataBinder.Eval(Container.DataItem,"PosID")) %>'
                                    runat="server" class="anchor__grd edit_grd">Edit</a>
                            </ItemTemplate>
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
        <tr>
            <td colspan="2" style="height: 17px">
                <uc1:Paging ID="ViewPostinglstPaging" runat="server" />
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
