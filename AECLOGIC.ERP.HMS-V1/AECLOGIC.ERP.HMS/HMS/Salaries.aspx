<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="Salaries.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Salaries" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function controlEnter(event) {
            // alert('hello');
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                // alert("Enter fired");
                document.getElementById('<%= btnSearch.ClientID %>').click();
                return false;
            }
            else {
                return true;
            }
        }
        <%--function EnterEvent(e) {
            if (e.keyCode == 13) {
                __doPostBack('<%=btnSearch.UniqueID%>', "");
            }
        }--%>
        function DisplayMonthYear() {
            var Result = AjaxDAL.GetStartDate();
            //var ddl = document.getElementById("<%=ddlMonth.ClientID%>");
            //var SelVal = ddlMonth.options[ddl.selectedIndex].text;
            //var SelVal = ddlMonth.options[ddlMonth.selectedIndex].value;
            //alert(SelVal); //SelVal is the selected Value
            var frommonth = ddlMonth.options[ddlMonth.selectedIndex].text;
            var fromyear = ddlYear.options[ddlYear.selectedIndex].text;
            var fromdate = '21' + '/' + frommonth + '/' + fromyear;
            var todate;
            if (ddlMonth.options[ddlMonth.selectedIndex].value == 12) {
                //todate=
            }
            else {
            }
            document.getElementById("txtToDate").value = fromdate;
        }
    </script>
    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td width="60%">
                        <AEC:Topmenu ID="topmenu" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="SalariesAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="SalariesAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <%-- <td colspan="2">--%>
                                                            <tr>
                                                                <td>Worksite:
                                                                       &nbsp;&nbsp;
                                                                <asp:DropDownList ID="ddlworksites" runat="server" Visible="false" CssClass="droplist" AccessKey="w"
                                                                    ToolTip="[Alt+w OR Alt+w+Enter]" AutoPostBack="true" OnSelectedIndexChanged="ddlworksites_SelectedIndexChanged" TabIndex="1">
                                                                </asp:DropDownList>
                                                                    <asp:TextBox ID="txtSearchWorksite" OnTextChanged="Worksidechangewithdep" Height="22px" Width="140px"
                                                                        runat="server" AutoPostBack="True">                                                                   
                                                                    </asp:TextBox>
                                                                    <%--onkeypress="return EnterEvent(event)"--%>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                Department:
                                                                <asp:DropDownList ID="ddldepartments" Visible="false" runat="server" CssClass="droplist" TabIndex="2"
                                                                    AccessKey="1" ToolTip="[Alt+1]">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                    <asp:TextBox ID="txtSearchdept" OnTextChanged="GetDepartment" Height="22px" Width="140px"
                                                                        runat="server" AutoPostBack="True">                                                                   
                                                                    </asp:TextBox><%--onkeypress="return EnterEvent(event)"--%>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListdept" ServicePath="" TargetControlID="txtSearchdept"
                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchdept"
                                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                                    <%--  <td colspan="2" >--%>
                                                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                 EmpID:<asp:TextBox ID="txtEmpID" Height="20px" Width="40Px" runat="server" AccessKey="4" ToolTip="[Alt+4]"
                                                                     TabIndex="5">                                                                 
                                                                 </asp:TextBox><%-- onkeypress="return EnterEvent(event)"--%>
                                                                    <cc1:FilteredTextBoxExtender ID="FTBEmpID" runat="server" TargetControlID="txtEmpID" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                                                    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Name:<asp:TextBox ID="txtEmpName" Height="20px" runat="server" TabIndex="6" AccessKey="5"
                                                                        ToolTip="[Alt+5]">                                                              
                                                                    </asp:TextBox>
                                                                    <%--onkeypress="return EnterEvent(event)"--%>
                                                                    <cc1:TextBoxWatermarkExtender ID="txtwmeEmpName" runat="server" WatermarkText="[Filter Name]"
                                                                        TargetControlID="txtEmpName"></cc1:TextBoxWatermarkExtender>
                                                                    <%-- <asp:Label ID="lblDates" runat="server"></asp:Label>
                                                                <input type="text" id="lblDates1" readonly />--%>
                                                                       &nbsp;&nbsp;&nbsp;
                                                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                                    TabIndex="7" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" CssClass="btn btn-primary"
                                                                    Width="80px" />
                                                                    <%-- </td>--%>
                                                            </tr>
                                                            <%--</td>--%>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblNature" runat="server" Text="Emp Nature:"></asp:Label>
                                                                    <asp:DropDownList ID="ddlEmpNature" runat="server" CssClass="droplist" Width="90">
                                                                    </asp:DropDownList>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Month:&nbsp;
                                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="droplist" TabIndex="3" AccessKey="2"
                                                                    ToolTip="[Alt+2]" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" Width="90">
                                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                    <asp:ListItem Value="1">January</asp:ListItem>
                                                                    <asp:ListItem Value="2">February</asp:ListItem>
                                                                    <asp:ListItem Value="3">March</asp:ListItem>
                                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                                    <asp:ListItem Value="8">August</asp:ListItem>
                                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                                    <asp:ListItem Value="10">October</asp:ListItem>
                                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                                    <asp:ListItem Value="12">December</asp:ListItem>
                                                                </asp:DropDownList>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Year:
                                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="droplist" TabIndex="4" ToolTip="[Alt+3]"
                                                                    AccessKey="3" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Width="90">
                                                                </asp:DropDownList>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;&nbsp;&nbsp;&nbsp;
                                                                         <asp:Label ID="lblTodate" runat="server"> Till Date:</asp:Label>
                                                                    <asp:TextBox ID="txtTodate" runat="server" Width="90"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="calextnd" runat="server" TargetControlID="txtTodate" PopupButtonID="txtTodate"
                                                                        Format="dd/MM/yyyy" Enabled="true"></cc1:CalendarExtender>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                           <asp:Button ID="btnSync" OnClientClick="return confirm('Are you sure to synchronise salaries? 1. Absent penalities must be posted to accounts before affecting salaries.\n 2. All Week offs must be set. 3. All Allowances must be configured. 4. Non-CTC Allowances must be set to time periods. 5. See if employee is considered for vacation settlements ')"
                                                                               ToolTip="Sync Emp if Record not found in Grid " runat="server" Text="Sync Salaries" CssClass="btn btn-primary"
                                                                               Tag="0" OnClick="btnSync_Click" TabIndex="8" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                 <asp:Button ID="btnApproveSalas" OnClientClick="return confirm('Are you sure to approve salaries to accounts?')"
                                                                     ToolTip="Sync Emp if Record not found in Grid " runat="server" Text="Approve" CssClass="btn btn-primary"
                                                                     Tag="1" OnClick="btnSync_Click" TabIndex="8" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                            </tr>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </Content>
                                            </cc1:AccordionPane>
                                        </Panes>
                                    </cc1:Accordion>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;<asp:RadioButton ID="rbActive" runat="server" AutoPostBack="True" Checked="True"
                                    GroupName="Emp" Text="Active EmployeeList" OnCheckedChanged="rbActive_CheckedChanged"
                                    TabIndex="7" />
                                    <asp:RadioButton ID="rbInActive" runat="server" AutoPostBack="True" GroupName="Emp"
                                        Text="Inactive EmployeeList" OnCheckedChanged="rbInActive_CheckedChanged" />
                                    <asp:RadioButton ID="rbView" runat="server" AutoPostBack="True" GroupName="Emp"
                                        Text="View" OnCheckedChanged="rbInActive_CheckedChanged" />
                                    <asp:CheckBox ID="chkHis" runat="server" Text="Show Historical ID" AutoPostBack="true"
                                        TabIndex="8" OnCheckedChanged="chkHis_CheckedChanged" />
                                    <asp:CheckBox ID="chkPaySlip" runat="server" Text="Dtl Rpt To Excel" AutoPostBack="true"
                                        TabIndex="8" />
                                    <asp:LinkButton ID="lnkViewAttendance" runat="server" Text="ViewAttendance" OnClick="lnkViewAttendance_Click"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td id="tbAccruals" style="width: 100%;" class="tableHead" runat="server">
                                    <table border="0" cellpadding="0" style="width: 100%;" cellspacing="0">
                                        <tr>
                                            <td>
                                                <b>Salaries</b>
                                            </td>
                                            <td style="text-align: right; padding-right: 30px; width: 80%;">
                                                <asp:Button ID="btnOutPutExcel" runat="server" Text="Export to Excel" OnClientClick="return confirm('Are u Sure to Export?')"
                                                    OnClick="BtnExportGrid_Click" CssClass="savebutton" TabIndex="9" />
                                                <asp:Button ID="btnDetailedRptExpToXL" runat="server" Text="Dtl Rpt To Excel"
                                                    CssClass="savebutton" OnClick="btnDetailedRptExpToXL_Click" Visible="false" />
                                                <asp:Label ID="lblResultCount" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvPaySlip" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                                        Width="100%" CellPadding="4" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        ShowFooter="True" OnRowCommand="gvPaySlip_RowCommand" HeaderStyle-CssClass="tableHead"
                                        OnSorting="gvPaySlip_Sorting" AllowSorting="True" AlternatingRowStyle-BackColor="GhostWhite">
                                        <EmptyDataRowStyle CssClass="EmptyRowData" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Historical ID" DataField="HisID" Visible="false" SortExpression="HisID">
                                                <ControlStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EmpId" HeaderText="EmpID" HeaderStyle-HorizontalAlign="Left" SortExpression="EmpId"
                                                Visible="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CTC" HeaderText="Gross" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TotalWages" HeaderText="Wages" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AllowanceAmount" HeaderText="Allowances" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NONCTCAmount" HeaderText="NON CTC" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <%--  <asp:TemplateField HeaderText="Contributions" HeaderStyle-HorizontalAlign="right"
                                                ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContributionsAmount" runat="server" Text='<%# GetContributionsAmount(decimal.Parse(Eval("ContributionAmount").ToString())).ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblContributionsTot" runat="server" Text='<%# GetContributions().ToString()%>'></asp:Label></b>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="OT" HeaderStyle-HorizontalAlign="right"
                                                ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContributionsAmount" runat="server" Text='<%# GetOTNetAmount(decimal.Parse(Eval("OTAmount").ToString())).ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblContributionsTot" runat="server" Text='<%# GetOTTotalAmount().ToString()%>'></asp:Label></b>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Special" HeaderStyle-HorizontalAlign="right"
                                                ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPaySlipID" runat="server" Text='<%# Eval("PaySlipID").ToString() %>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="lblSpecial" runat="server" Text='<%# GetSpecialNetAmount(decimal.Parse(Eval("SpecialAmt").ToString())).ToString()%>'></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblSpecial_Foot" runat="server" Text='<%# GetSpecialTotalAmount().ToString()%>'></asp:Label></b>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Absent Penalties" HeaderStyle-HorizontalAlign="right"
                                                ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# GetAbsPNetAmount(decimal.Parse(Eval("AbsPAmount").ToString())).ToString()%>'></asp:Label>
                                                    <%--<asp:Label ID="Label1" runat="server" Text='<%# Eval("AbsPAmount")%>'></asp:Label>--%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# GetAbsPTotalAmount().ToString()%>'></asp:Label></b>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EMP Penalties" HeaderStyle-HorizontalAlign="right"
                                                ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# GetEmPPNetAmount(decimal.Parse(Eval("EmpPAmount").ToString())).ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# GetEmpPTotalAmount().ToString()%>'></asp:Label></b>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="St.Deductions" HeaderStyle-HorizontalAlign="right"
                                                ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeductSatatutoryAmount" runat="server" Text='<%# GetDeductSatatutoryAmount(decimal.Parse(Eval("StatutoryAmount").ToString())).ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblDeductSatatutoryTot" runat="server" Text='<%# GetDeductSatatutory().ToString()%>'></asp:Label></b>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="TDS" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTDSAmount" runat="server" Text='<%# GetTDSAmount(decimal.Parse(Eval("TDS").ToString())).ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblTDSTot" runat="server" Text='<%# GetTDS().ToString()%>'></asp:Label></b>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PF" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPFAmount" runat="server" Text='<%# GetPFAmount(decimal.Parse(Eval("PF").ToString())).ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblPFTot" runat="server" Text='<%# GetPF().ToString()%>'></asp:Label></b>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ESI" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblESIAmount" runat="server" Text='<%# GetESIAmount(decimal.Parse(Eval("ESI").ToString())).ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblESITot" runat="server" Text='<%# GetESI().ToString()%>'></asp:Label></b>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PT" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPTAmount" runat="server" Text='<%# GetPTAmount(decimal.Parse(Eval("PT").ToString())).ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblPTTot" runat="server" Text='<%# GetPT().ToString()%>'></asp:Label></b>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Advance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAdv" runat="server" Text='<%# GetAdvance(decimal.Parse(Eval("Advance").ToString())).ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblPTAdv" runat="server" Text='<%# GetTotAdvance().ToString()%>'></asp:Label></b>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile Bill">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMobileBill" runat="server" Text='<%# GetMobileBill(decimal.Parse(Eval("MobileExp").ToString())).ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblPTMobileBill" runat="server" Text='<%# GetTotMobileBill().ToString()%>'></asp:Label></b>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NetAmount" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                FooterStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# GetNetAmount(decimal.Parse(Eval("NetAmount").ToString())).ToString()%>'></asp:Label>
                                                    <%--<asp:Label ID="lblAmount" runat="server" Text='<%# Eval("NetAmount")%>'></asp:Label>--%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblNetAmountTot" runat="server" Text='<%# GetAmount().ToString()%>'></asp:Label></b>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ApprovalText" HeaderText="Status" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Payslip" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a id="A1" target="_blank" href='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "EmpId").ToString()) %>'
                                                        runat="server" class="btn btn-success">View</a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PaymentStatus" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkStatus" runat="server" Text='<%# Eval("paymentStatus")%>'
                                                        CommandName="Status" Enabled='<%# GetStatus(Eval("paymentStatus").ToString())%>'
                                                        CommandArgument='<%#Eval("EmpId")%>' CssClass="btn btn-primary"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BreakUp" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a id="A3" href='<%# String.Format("EmpSalHikes.aspx?EmpID={0}",DataBinder.Eval(Container.DataItem, "EmpId")) %>'
                                                        runat="server" target="_blank" class="btn btn-primary">Revise</a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnRun" CommandName="Run" CommandArgument='<%#Eval("EmpId") %>'  OnClientClick="return confirm('Are you sure to synchronise salary to accounts?')" runat="server">Sync</asp:LinkButton>
                                </ItemTemplate>
                                </asp:TemplateField>--%>
                                        </Columns>
                                        <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle CssClass="tableHead" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />
                                    </asp:GridView>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary"
                                        OnClick="btnUpdate_Click" Visible="true" />
                                    <asp:GridView ID="gvSalaDetails" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                                        Width="100%" CellPadding="4" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        ShowFooter="True" HeaderStyle-CssClass="tableHead"
                                        AllowSorting="True" AlternatingRowStyle-BackColor="GhostWhite">
                                        <EmptyDataRowStyle CssClass="EmptyRowData" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Historical ID" DataField="HisID" Visible="false" SortExpression="EmpID">
                                                <ControlStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="SL NO" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Iqama" HeaderText="Iqama" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AccountNumber" HeaderText="IBAN" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SWIFT" HeaderText="Bank Code" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TotalSalary" HeaderText="Total Salary Payable" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BasicSalary" HeaderText="Basic" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="HRASalary" HeaderText="HRA" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OthersSalary" HeaderText="Others" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DeductionSalary" HeaderText="Deductions" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Addrass" HeaderText="Address" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EMPID" HeaderText="EMP ID" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Spnsership" HeaderText="Spnsership" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BankName" HeaderText="Bank Name" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BranchName" HeaderText="Private or Company a/c" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                        <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle CssClass="tableHead" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 17px">
                                    <uc1:Paging ID="EmpListPaging" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnOutPutExcel" />
            <asp:PostBackTrigger ControlID="btnDetailedRptExpToXL" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="SalariesUpdPanel">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
