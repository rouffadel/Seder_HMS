<%@ Page Title="" Language="C#" AutoEventWireup="True"
    CodeBehind="DeductStatutory.aspx.cs" Inherits="AECLOGIC.ERP.HMS.DeductStatutory" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">
        function validatesave() {
            if (!chkDropDownList('<%=ddlItem.ClientID%>', "  Payroll Item", "", ""))
                return false;
            if (!chkDropDownList('<%=ddlFinancial.ClientID%>', " Financial Year", "", ""))
                return false;
            if (!chkDropDownList('<%=ddlWages.ClientID%>', " Wages ", "", ""))
                return false;
            if (!chkFloatNumber('<%=txtContributionRate.ClientID%>', "  Deduction Rate", "true", ""))
                return false;
            if (!chkFloatNumber('<%=txtWageLimit.ClientID%>', " Wage Limit", "true", ""))
                return false;
            if (!chkFloatNumber('<%=txtAmtLimit.ClientID%>', "Amount Limit", "true", ""))
                return false;
        }
    </script>
    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <AEC:Topmenu ID="topmenu" runat="server" />
                    </td>
                    <tr>
                        <td>
                            <asp:Panel ID="pnltblNew" runat="server" CssClass="box box-primary" Visible="false"
                                Width="50%">
                                <table id="tblNew" runat="server" visible="false">
                                    <tr>
                                        <td colspan="2" class="pageheader">New Deduction Statutory
                                        </td>
                                        <td class="pageheader" style="vertical-align: top; height: 250px; width: 50%" rowspan="8">
                                            <ajaxToolkit:Accordion ID="WagesAccordion" runat="Server" Style="display: none;"
                                                SelectedIndex="0" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                                ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="true" TransitionDuration="250"
                                                FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                                <Panes>
                                                    <ajaxToolkit:AccordionPane ID="paneWages" runat="server" HeaderCssClass="accordionHeader"
                                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                                        <Header>
                                                            <b>Wages</b>
                                                        </Header>
                                                        <Content>
                                                            <div style="height: 150px; overflow: scroll; width: 100%">
                                                                <asp:CheckBoxList ID="cblWages" runat="server" DataTextField="Name" DataValueField="WagesID">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                        </Content>
                                                    </ajaxToolkit:AccordionPane>
                                                    <ajaxToolkit:AccordionPane ID="paneAllowences" runat="server" HeaderCssClass="accordionHeader"
                                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                                        <Header>
                                                            <b>Allowances</b>
                                                        </Header>
                                                        <Content>
                                                            <div style="height: 150px; overflow: scroll; width: 100%">
                                                                <asp:CheckBoxList ID="cblAllowences" runat="server" DataTextField="Name" DataValueField="AllowId">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                        </Content>
                                                    </ajaxToolkit:AccordionPane>
                                                </Panes>
                                            </ajaxToolkit:Accordion>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px">Payroll Item<span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 106px">
                                            <asp:DropDownList ID="ddlItem" CssClass="droplist" runat="server" TabIndex="1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px">Financial Year<span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 106px">
                                            <asp:DropDownList ID="ddlFinancial" CssClass="droplist" runat="server" TabIndex="2">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 124px">Wages<span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 106px">
                                            <asp:DropDownList ID="ddlWages" CssClass="droplist" runat="server" TabIndex="3"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlWages_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Salary" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Gross" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Custom" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Deduction Rate<span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 106px">
                                            <asp:TextBox ID="txtContributionRate" runat="server" TabIndex="4"></asp:TextBox>In
                                            Percentage (%)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Wage Limit <span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 106px">
                                            <asp:TextBox ID="txtWageLimit" runat="server" TabIndex="5"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Amount Limit <span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 106px">
                                            <asp:TextBox ID="txtAmtLimit" runat="server" TabIndex="6"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Width="100px"
                                                OnClick="btnSubmit_Click" OnClientClick="javascript:return validatesave();" AccessKey="s"
                                                TabIndex="7" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                            <table id="tblEdit" runat="server" visible="false" width="100%">
                                <tr>
                                    <td>
                                        <cc1:Accordion ID="DedStaAccordion" runat="server" HeaderCssClass="accordionHeader"
                                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                            RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                            <Panes>
                                                <cc1:AccordionPane ID="DedStaAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                    ContentCssClass="accordionContent">
                                                    <Header>
                                                        Search Criteria</Header>
                                                    <Content>
                                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <b>Financial Year:</b>
                                                                    <asp:DropDownList ID="ddlFinYear" TabIndex="8" CssClass="droplist" AutoPostBack="true"
                                                                        runat="server" OnSelectedIndexChanged="ddlFinYear_SelectedIndexChanged">
                                                                    </asp:DropDownList>
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
                                    <td style="width: 100%">
                                        <asp:GridView ID="gvAllowances" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                            OnRowCommand="gvWages_RowCommand" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Payroll Item">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("payrollitem")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Financial Year">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFinancial" runat="server" Text='<%#Eval("FinaciaYear")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Wage">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWage" runat="server" Text='<%#Eval("LongName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deduction Rate" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPercentage" runat="server" Text='<%#Eval("ContrRate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Wage Limit" DataField="WageLimit" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField HeaderText="Amount Limit" DataField="AmountLimit" ItemStyle-HorizontalAlign="Right" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("DedID")%>'
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
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="SalariesUpdPanel">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
