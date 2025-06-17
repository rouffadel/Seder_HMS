<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="SMSEmpTransfers.aspx.cs" Inherits="AECLOGIC.ERP.HMS.SMSEmpTransfers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function HeadsAssign(ctrl, SiteID, DeptID, HeadID, UserID, EmpID, Mgnr, TransID) {
            var locSiteID = $get(SiteID).value;
            var locDeptID = $get(DeptID).value;
            var locHeadID = $get(HeadID).value;
            var locUserID = UserID;
            var locEmpID = EmpID;
            var locMgnr = Mgnr;
            var locTransID = TransID;


            AjaxDAL.UpdSiteDeptChanges(locEmpID, locSiteID, locDeptID, locHeadID, locUserID);
            AjaxDAL.UpdSMSEMPTransferStatus(TransID);
            return true;

            return true;
        }
        

       function GetID(source, eventArgs) {
           var HdnKey = eventArgs.get_value();
           document.getElementById('<%=ddlworksites_hid.ClientID %>').value = HdnKey;        
           <%-- alert(document.getElementById('<%=ddlWs_hid.ClientID %>').value);--%>
       }
        function depatID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddldepartments_hid.ClientID %>').value = HdnKey;
            <%-- alert(document.getElementById('<%=ddlWs_hid.ClientID %>').value);--%>
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="dvSMS" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td>
                            <AEC:Topmenu ID="topmenu" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <tr>
                                <td>
                                    <asp:Label ID="lblSMSEmpWS" runat="server" Text="Worksite:"></asp:Label>
                                    &nbsp;
                                    
                                     <asp:HiddenField ID="ddlworksites_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="false" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                           </cc1:TextBoxWatermarkExtender>
                                    <asp:DropDownList ID="ddlworksites" runat="server"  visible ="false" CssClass="droplist" TabIndex="1" AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]" >
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:Label ID="lblSMSEmpFromDept" runat="server" Text="From Dept:"></asp:Label>
                                    &nbsp;&nbsp;
                                    
                                     <asp:HiddenField ID="ddldepartments_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchdepat" OnTextChanged="Getdepat" AutoPostBack="false" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList_depat" ServicePath="" TargetControlID="txtSearchdepat"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="depatID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchdepat"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                           </cc1:TextBoxWatermarkExtender>
                                    <asp:DropDownList ID="ddldepartments" runat="server" visible="false" CssClass="droplist"  TabIndex="2" AccessKey="1" ToolTip="[Alt+1]"  >
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:Label ID="lblSMSEmpEmpID" runat="server" Text="EmpID:"></asp:Label>
                                    <asp:TextBox ID="txtEmpID" runat="server" Width="65px" TabIndex="3" AccessKey="2" ToolTip="[Alt+2]" ></asp:TextBox>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblSMSEmpName" runat="server" Text="Name:"></asp:Label>
                                    <asp:TextBox ID="txtEmpname" runat="server" TabIndex="4" AccessKey="3" ToolTip="[Alt+3]" ></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                        Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath=""
                                        TargetControlID="txtEmpName" UseContextKey="True" CompletionInterval="10" CompletionListCssClass="AutoExtender"
                                        CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                    </cc1:AutoCompleteExtender>
                                    &nbsp;
                                    <asp:Button ID="btnSearch" runat="server" Text="Show" CssClass="savebutton" Width="100px"
                                        OnClick="btnSearch_Click"  TabIndex="5" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />
                                    &nbsp;
                                </td>
                            </tr>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td>
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="dvgrd" runat="server" visible="false" style="width: 100%">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="updgrid" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="grdSMSEmpTrnsfers" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                                                                    GridLines="Both" HeaderStyle-CssClass="tableHead" Width="100%" CellPadding="5"
                                                                    EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="grdSMSEmpTrnsfers_RowCommand"
                                                                    OnRowDataBound="grdSMSEmpTrnsfers_RowDataBound">
                                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="EmpID" Visible="false" SortExpression="EmpId">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEmpID" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ControlStyle-Width="175px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("[Name]")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Font-Names="Arial" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Message" HeaderStyle-HorizontalAlign="Left" ControlStyle-Width="250px">
                                                                            <HeaderStyle Width="180" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Font-Names="Arial" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sender Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSenderName" runat="server" Text='<%#Eval("SenderName")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Font-Names="Arial" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sender Worksite">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSenderWS" runat="server" Text='<%#Eval("SenderSite")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Font-Names="Arial" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkConsolidate" runat="server" CommandName="UPD" Text="Consolidate"
                                                                                    CommandArgument='<%#Bind("EmpID")%>'></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="TransID" Visible="false" SortExpression="TransID">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTransID" runat="server" Text='<%#Bind("TransID")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDel" runat="server" CommandName="Del" Text="Delete"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                    <EditRowStyle BackColor="#999999" />
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <uc1:Paging ID="SMSEMPTransferPaging" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hdnSearchChange" Value="0" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="dvTransto" runat="server" visible="false">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Label ID="lblRemark" runat="server" Width="200%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="subheader" colspan="3">
                            <asp:Label ID="lblEmpDetails" runat="server" Text="EmployeeDetails"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 180px">
                            <span>
                                <asp:Label ID="lblEmpName" runat="server" Text="EmpName"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : </span>
                        </td>
                        <td style="width: 300px">
                            <asp:Label ID="lblEmployeeName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 180px">
                            <span>
                                <asp:Label ID="lblEmpCurWS" runat="server" Text="Current WorkSite"></asp:Label>:</span>
                        </td>
                        <td style="width: 300px;">
                            <asp:Label ID="lblEmpCurrentWorksite" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 180px">
                            <span>
                                <asp:Label ID="lblEmuCurDept" runat="server" Text="Current Dept"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</span>
                        </td>
                        <td style="width: 300px;">
                            <asp:Label ID="lblEmpCurrentDept" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="subheader" colspan="3">
                            <asp:Label ID="lblTransTo" runat="server" Text="Transfer To"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 180px;">
                            <asp:Label ID="lblWS" runat="server" Text="WorkSite"></asp:Label><span style="color: #ff0000">*</span>
                        </td>
                        <td style="width: 300px;">
                            <asp:DropDownList ID="ddlWS"  CssClass="droplist" runat="server" AutoPostBack="false" Width="98%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 180px;">
                            <asp:Label ID="lblDept" runat="server" Text="Department"></asp:Label><span style="color: #ff0000">*</span>
                        </td>
                        <td style="width: 300px;">
                            <asp:DropDownList ID="ddlDept" CssClass="droplist"  runat="server" AutoPostBack="true" Width="98%" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 180px;">
                            <asp:Label ID="lblReportingTo" runat="server" Text="Reporting To"></asp:Label><span
                                style="color: #ff0000">*</span>
                        </td>
                        <td style="width: 300px;">
                            <asp:DropDownList ID="ddlReportingTo" runat="server" AutoPostBack="true"  CssClass="droplist" Width="98%"
                                OnSelectedIndexChanged="ddlReportingTo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnTransfer" runat="server" Text="Transfer" OnClick="btnTransfer_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
