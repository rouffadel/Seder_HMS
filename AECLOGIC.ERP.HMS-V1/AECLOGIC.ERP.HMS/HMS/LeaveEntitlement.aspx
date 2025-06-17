<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="LeaveEntitlement.aspx.cs" Inherits="AECLOGIC.ERP.HMS.LeaveEntitlement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">
        function validatesave() {
            if (!chkDropDownList('<%=ddlLeaveType.ClientID%>', " Leave Type", "", ""))
                return false;
            if (!chkDropDownList('<%=ddlAllotmentCycle.ClientID%>', "  Allotment Cycle  ", "", ""))
                return false;
            if (!chkNumber('<%=txtMinDaysofWork.ClientID%>', " Min Days of Work", "true", "[Days]"))
                return false;
            if (!chkFloatNumber('<%=txtMaxLeaveElg.ClientID%>', "Max Leave Eligibilty per month", "true", "[Days]"))
                return false;
            if (!chkFloatNumber('<%=txtxmaxLeaveElgyear.ClientID%>', "Max Leave Eligibilty per year", "true", "[Days]"))
                return false;
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pnltblNew" runat="server" CssClass="box box-primary" Visible="false"
                            Width="100%">
                            <table width="100%" runat="server">
                                <tr>
                                    <td style="width: 350px">
                                        <table id="tblNew" runat="server" visible="false">
                                            <tr>
                                                <td colspan="2" class="pageheader">New Leave Entitlement
                                                </td>
                                                <td class="pageheader" style="width: 85px">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Leave Type
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlLeaveType" CssClass="droplist" runat="server"
                                                        TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 85px">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Grade of Employee
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTrade" CssClass="droplist" runat="server" OnSelectedIndexChanged="ddlTrade_SelectedIndexChanged"
                                                        TabIndex="2">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 85px">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Allotment Cycle
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAllotmentCycle" CssClass="droplist" runat="server"
                                                        TabIndex="3">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 85px">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 124px">Is Accruable
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkisAccruable" runat="server" Checked="True" Text="Active"
                                                        TabIndex="4" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 124px">Is C/Fwd
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkC_Fwd" runat="server" Checked="True" Text="Active"
                                                        TabIndex="4" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 124px">Is Encashable
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkisEncashable" runat="server" Checked="True" Text="Active"
                                                        TabIndex="4" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 124px">Pay Coeff
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPayCoeff" runat="server" Width="300" Text="1" TabIndex="2"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 124px">Payable Type<span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPayType" CssClass="droplist" runat="server"
                                                        TabIndex="3">
                                                        <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Payable" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Non-Payable" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Min Days of Work
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinDaysofWork" runat="server" TabIndex="4"></asp:TextBox>
                                                </td>
                                                <td style="width: 85px">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Max Leave Eligibility
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtxmaxLeaveElgyear" runat="server" TabIndex="6" AutoPostBack="True" OnTextChanged="txtxmaxLeaveElgyear_TextChanged"></asp:TextBox>
                                                </td>
                                                <td style="width: 85px">&nbsp;per year
                                                </td>
                                            </tr>
                                            <tr style="display: none;">
                                                <td>Max Leave Eligibility
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMaxLeaveElg" runat="server" TabIndex="5" ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td style="width: 85px">&nbsp;per month
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Min Service
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMinService" runat="server" TabIndex="6"></asp:TextBox>
                                                </td>
                                                <td style="width: 85px">&nbsp;year
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 124px">For Gender<span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlGender" CssClass="droplist" runat="server"
                                                        TabIndex="3">
                                                        <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Both" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-success"
                                                        OnClientClick="javascript:return validatesave();" AccessKey="s"
                                                        TabIndex="7" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                                </td>
                                                <td style="width: 85px">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td rowspan="11">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <%-- OnSelectedIndexChanged="rblDesg_SelectedIndexChanged" --%>
                                                    <asp:RadioButtonList ID="rblDesg" AutoPostBack="true" runat="server" Font-Bold="True" TabIndex="1" OnSelectedIndexChanged="rblDesg_SelectedIndexChanged"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Active" Selected="True" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="In-Active" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>
                                                        <asp:Label ID="lbltrade" Font-Bold="true" runat="server" Text="Emp Grades Config Details"></asp:Label></b>
                                                    <asp:GridView ID="gvEMPTrade" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                                        ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                                        OnRowCommand="gvLeaveProfile_RowCommand" CssClass="gridview" HeaderStyle-CssClass="tableHead" BorderWidth="2px" Width="100%">
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%"
                                                            CssClass="gridview" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Position Category" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPositionCategory" runat="server" Text='<%#Eval("PositionCategory")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Grade" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGrade" runat="server" Text='<%#Eval("Grade")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Basic From" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSalaryFrom" runat="server" Text='<%#Eval("SalaryFrom")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Basic TO" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSalaryTo" runat="server" Text='<%#Eval("SalaryTo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Housing (%)" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHRA" runat="server" Text='<%#Eval("HRA")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tpt (%)" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTransport" runat="server" Text='<%#Eval("Transport")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Food" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFood" runat="server" Text='<%#Eval("Food")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mobile" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("Mobile")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AL" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAnnualLeave" runat="server" Text='<%#Eval("AnnualLeave")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Entitlement" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFamilyEntitlement" runat="server" Text='<%#Eval("FamilyEntitlement")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tickets" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTickets" runat="server" Text='<%#Eval("Tickets")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="VISA" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExitEntryVISA" runat="server" Text='<%#Eval("ExitEntryVISA")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Medical" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMedical" runat="server" Text='<%#Eval("Medical")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MedicalNos" ItemStyle-BorderColor="Navy" ItemStyle-BorderWidth="1px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMedicalNos" runat="server" Text='<%#Eval("MedicalNos")%>'></asp:Label>
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
                                        </table>
                                        <br />
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 687px" rowspan="11">
                                                    <b>
                                                        <asp:Label Font-Bold="true" ID="lblLT" runat="server" Text="Leave Type Details"></asp:Label></b>
                                                    <asp:GridView ID="gvLeave" Width="100%" runat="server" AutoGenerateColumns="false" ForeColor="#333333"
                                                        HeaderStyle-CssClass="tableHead" CssClass="gridview" GridLines="Both" EmptyDataText="No Records Found"
                                                        EmptyDataRowStyle-CssClass="EmptyRowData">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S N">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDays" runat="server" Text='<%#Eval("ShortName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payable Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPayable" runat="server" Text='<%#Eval("PayType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gender">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Is Accruable">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblisAccruable" runat="server" Text='<%#Eval("isAccruable")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="C/Fwd">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCFwd" runat="server" Text='<%#Eval("CFwd")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Is Encashable">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblisEncashable" runat="server" Text='<%#Eval("isEncashable")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PayCoeft">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPayCoeft" runat="server" Text='<%#Eval("PayCoeft")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Min Service Yrs" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMinServiceYrs" runat="server" Text='<%#Eval("MinServiceYrs")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Max Entitlement / Yr">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaxEntitlementYr" runat="server" Text='<%#Eval("MaxEntitlementYr")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Labour Law Ref">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLabourLawRef" runat="server" Text='<%#Eval("LabourLawRef")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td rowspan="2">
                        <table id="tblEdit" runat="server" visible="false">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="LstOfHolidayConAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%">
                                        <Panes>
                                            <cc1:AccordionPane ID="LstOfHolidayConAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <b>&nbsp;&nbsp;&nbsp; Emp Grade:&nbsp;&nbsp; </b>
                                                                <asp:DropDownList ID="ddlEmpGradeSearch" AutoPostBack="true" CssClass="droplist" runat="server" TabIndex="8" AccessKey="2"
                                                                    ToolTip="[Alt+2]" OnSelectedIndexChanged="ddlEmpNature_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                &nbsp;&nbsp;
                                                                <b>
                                                                    <asp:HyperLink ID="lnkLeave" runat="server" Text="Leave Type Master" NavigateUrl="~/HMS/TypeOfLeaves.aspx" Target="_blank"></asp:HyperLink></b>
                                                                <b>
                                                                    <asp:CheckBox ID="chkByworkingdays" runat="server" Checked="true" Enabled="false"
                                                                        Text="Calculation is based on PAYABLE DAYS" /></b><%--  OnCheckedChanged="chkByworkingdays_CheckedChanged" AutoPostBack="true"--%>
                                                                <asp:Button ID="btnPaybledaysSave" runat="server" Text="Save" OnClick="btnSavePD_Click" CssClass="btn btn-success"
                                                                    TabIndex="7" ToolTip="[Alt+s OR Alt+s+Enter]" />
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
                                    <asp:GridView ID="gvLeaveEL" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        OnRowCommand="gvLeaveProfile_RowCommand" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%"
                                            CssClass="gridview" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Leave Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp Grade">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNature" runat="server" Text='<%#Eval("Nature")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allotment Cycle">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAllotment" runat="server" Text='<%#Eval("Allotment")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is Accruable">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblisAccruable" runat="server" Text='<%#Eval("isAccruable")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="C/Fwd">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCFwd" runat="server" Text='<%#Eval("CFwd") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is Encashable">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblisEncashable" runat="server" Text='<%#Eval("isEncashable") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PayCoeft">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPayCoeft" runat="server" Text='<%#Eval("PayCoeft")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Min Service in Months" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMinServiceYrs" runat="server" Text='<%#Eval("MinService")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Min Days Of Work" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMinDaysOfWork" runat="server" Text='<%#Eval("MinDaysOfWork")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Max Leave Eligibility(Year)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMaxLeaveEligibilityyear" runat="server" Text='<%#Eval("MaxLeaveElgYear")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Max Leave Eligibility(Month)" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMaxLeaveEligibility" runat="server" Text='<%#Eval("MaxLeaveEligibility")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payable Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPayable" runat="server" Text='<%#Eval("PayType")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="For Gender">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGender" runat="server" Text='<%#Eval("Gender")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("LEId")%>'
                                                        CommandName="Edt" CssClass="anchor__grd edit_grd"></asp:LinkButton>
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
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
