<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="EmpSalReport.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpSalReport" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
<script language="javascript" type="text/javascript">
    function Valid() {
        if (document.getElementById('<%=ddlEmp.ClientID %>').selectedIndex == 0) {
            alert("Select employee.!");
            document.getElementById('<%=ddlEmp.ClientID %>').focus();
            return false;
        }
    }
    //chaitanya:validation
    function isNumber(evt) {
        var iKeyCode = (evt.which) ? evt.which : evt.keyCode
        if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
            return false;

        return true;
    }
</script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td align="left" colspan="2">
                <asp:UpdatePanel runat="server" ID="updpnl">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="SimAlloListAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="SimAlloListAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                Worksite:<asp:DropDownList ID="ddlworksites" visible="false" AutoPostBack="true" CssClass="droplist"
                                                                    runat="server" OnSelectedIndexChanged="ddlworksites_SelectedIndexChanged" TabIndex="1" AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]">
                                                                </asp:DropDownList>
                                                                    <asp:TextBox ID="txtSearchWorksite" OnTextChanged="Worksidechangewithdep" Height="22px" Width="140px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                                                    </cc1:TextBoxWatermarkExtender>
                                                                Department:
                                                                <asp:DropDownList ID="ddldepartments" visible="false" AutoPostBack="true" CssClass="droplist" runat="server"
                                                                    OnSelectedIndexChanged="ddldepartments_SelectedIndexChanged" TabIndex="2" AccessKey="1" ToolTip="[Alt+1]">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                    <asp:TextBox ID="txtSearchdept" OnTextChanged="GetDepartmentSearch" Height="22px" Width="160px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionListdept" ServicePath="" TargetControlID="txtSearchdept"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchdept"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]">
                                                                    </cc1:TextBoxWatermarkExtender>

                                                                Status:<asp:DropDownList ID="ddlStatus" AutoPostBack="true" CssClass="droplist" runat="server"
                                                                    OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" TabIndex="3" AccessKey="2" ToolTip="[Alt+2]">
                                                                    <asp:ListItem Text="Active" Value="y"></asp:ListItem>
                                                                    <asp:ListItem Text="In-Active" Value="n"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                Month:<asp:DropDownList ID="ddlMonth" CssClass="droplist" runat="server" TabIndex="4" AccessKey="3" ToolTip="[Alt+3]">
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
                                                                Year:<asp:DropDownList ID="ddlYear" AutoPostBack="true" CssClass="droplist" runat="server" AccessKey="4" ToolTip="[Alt+4]"
                                                                    OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" TabIndex="5">
                                                                </asp:DropDownList>
                                                                Fin..Year:<asp:DropDownList ID="ddlFinyear" AutoPostBack="true" CssClass="droplist" AccessKey="5" ToolTip="[Alt+5]"
                                                                    runat="server" OnSelectedIndexChanged="ddlFinyear_SelectedIndexChanged" TabIndex="6">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp; &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Employee:<asp:DropDownList ID="ddlEmp" AutoPostBack="true" CssClass="droplist" runat="server"
                                                                    OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged" TabIndex="7" AccessKey="6" ToolTip="[Alt+6]">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtSearchEmp" OnTextChanged="GetEmployeeSearch" Height="22px" Width="160px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtSearchEmp"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtSearchEmp"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name]">
                                                                    </cc1:TextBoxWatermarkExtender>
                                                                &nbsp;<asp:Label ID="lblCount" runat="server" ForeColor="Blue" Text="Label"></asp:Label>
                                                                &nbsp;&nbsp;&nbsp;&nbsp; EmpID:
                                                                <asp:TextBox ID="txtEmpID" Width="50Px" runat="server" TabIndex="8"  AccessKey="7" ToolTip="[Alt+7]" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FTBEmpID" runat="server" TargetControlID="txtEmpID" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Show" OnClick="btnSearch_Click"  AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]"
                                                                    CssClass="savebutton" Width="80px"  TabIndex="9"/>
                                                                &nbsp;
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
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="updpnl2" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td style="width: 70%" align="left">
                                    <table id="tblInfo" runat="server" width="100%">
                                        <tr>
                                            <td colspan="2" class="pageheader">
                                                Employee Info
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 94px">
                                                EmpID:
                                            </td>
                                            <td style="width: 1229px">
                                                <asp:Label ID="lblEmpID" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>&nbsp;
                                                Name:
                                                <asp:Label ID="lblName" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                &nbsp; Department:<asp:Label ID="lblDept" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                &nbsp; &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 94px">
                                                WorkSite:
                                            </td>
                                            <td>
                                                <asp:Label ID="lblWs" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                &nbsp;&nbsp;Designation:<asp:Label ID="lblDesig" runat="server" Font-Bold="True"
                                                    ForeColor="Blue"></asp:Label>
                                                &nbsp; Status:<asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                &nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 94px">
                                                DOJ:
                                            </td>
                                            <td style="width: 1229px">
                                                <asp:Label ID="lblDOJ" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblDOR" runat="server" ForeColor="#666666" Text="Date of Relieve:"></asp:Label><asp:Label
                                                    ID="lblRelive" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                &nbsp;&nbsp;<asp:Label ID="lblSevice" runat="server" Text="Service:"></asp:Label>
                                                &nbsp;<asp:Label ID="lblShowRelive" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                &nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 94px">
                                                Report:<td>
                                                    <asp:Label ID="lblReportType" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                    &nbsp;
                                                    <asp:Label ID="lblRejoin" runat="server" Text="Date of ReJoin:"></asp:Label>
                                                    <asp:Label ID="lblDORejoin" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                </td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 94px">
                                                Qualification:<td>
                                                    <asp:Label ID="lblQualification" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                    &nbsp;
                                                </td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 30%" align="left">
                                    <table width="30%" id="tblImage">
                                        <tr>
                                            <td>
                                                <asp:Image ID="imgEmp" runat="server" BorderColor="#CC6600" BorderStyle="None" BorderWidth="2px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
            <ProgressTemplate>
                <img src="IMAGES/updateProgress.gif" alt="update is in progress" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
        <ContentTemplate>
            <table width="100%">
              
                <tr>
                    <td>
                        <asp:GridView ID="gvPaySlip" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                            Width="100%" EmptyDataText="No Records Found!" EmptyDataRowStyle-CssClass="EmptyRowData"
                            ShowFooter="True">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EmptyDataRowStyle CssClass="EmptyRowData" />
                            <Columns>
                                <asp:BoundField DataField="PaySlipID" HeaderText="PaySlipID" Visible="false" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EmpID" HeaderText="EmpID" HeaderStyle-HorizontalAlign="Left">
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" runat="server" Text='<%#GetMonth(Eval("Month").ToString())%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Year" HeaderText="Year" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TransID" HeaderText="TransID" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CTC" HeaderText="Gross" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NumberofDaysWorked" HeaderText="WorkedDays" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NumberofdaysPayable" HeaderText="PayableDays" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TDS" HeaderText="TDS" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Right">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
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
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Advance" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAdvance" runat="server" Text='<%#GetAdvance(Eval("EmpId").ToString(),Eval("Month").ToString(),Eval("Year").ToString()).ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <b>
                                            <asp:Label ID="lblAdvTot" runat="server" Text='<%# GetTotAdvance().ToString()%>'></asp:Label></b>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MobileBill" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobileBill" runat="server" Text='<%#GetMobile(Eval("EmpId").ToString(),Eval("Month").ToString(),Eval("Year").ToString()).ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <b>
                                            <asp:Label ID="lblMobTot" runat="server" Text='<%# GetTotMobileExp().ToString()%>'></asp:Label></b>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Take Home" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                    FooterStyle-HorizontalAlign="right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%#GetNetAmount(Eval("NetAmount").ToString(),Eval("EmpId").ToString(),Eval("Month").ToString(),Eval("Year").ToString()).ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <b>
                                            <asp:Label ID="lblNetAmountTot" runat="server" Text='<%#GetAmount().ToString()%>'></asp:Label></b>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payslip" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%--<asp:HyperLink  NavigateUrl="#" ID="hlnk1"  onclick='<%#DocNavigateUrl(Eval("EmpId").ToString(),Eval("Month").ToString(),Eval("Year").ToString()).ToString()%>' runat="server" >View</asp:HyperLink>--%>
                                        <a id="A1" target="_blank" href='<%#DocNavigateUrl(Eval("EmpId").ToString(),Eval("Month").ToString(),Eval("Year").ToString()).ToString()%>'
                                            runat="server">View</a>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
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
                    <td style="height: 17px">
                        <uc1:Paging ID="EmpListPaging" Visible="false" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
