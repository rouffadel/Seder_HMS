<%@ Page Title="" Language="C#" AutoEventWireup="True"
    CodeBehind="DeductProfTax.aspx.cs" Inherits="AECLOGIC.ERP.HMS.DeductProfTax" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">
        function validatesave() {
            if (!chkDropDownList('<%=ddlState.ClientID%>', "state", "", ""))
                return false;
            if (!chkDropDownList('<%=ddlFinancial.ClientID%>', " Financial Year", "", ""))
                return false;
            if (!chkDropDownList('<%=ddlWages.ClientID%>', " Wages ", "", ""))
                return false;
            if (!chkFloatNumber('<%=txtMinAmount.ClientID%>', "  Min Amount", "true", ""))
                return false;
            if (!chkFloatNumber('<%=txtMaxAmount.ClientID%>', " Max Amount", false, ""))
                return false;
            if (!chkFloatNumber('<%=txtAmt.ClientID%>', " Amount", "true", ""))
                return false;
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <AEC:Topmenu ID="topmenu" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnltblNew" runat="server" CssClass="box box-primary" Visible="false" Width="50%">
                            <table id="tblNew" runat="server" visible="false">
                                <tr>
                                    <td colspan="2" class="pageheader">New Deduction Profession Tax
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px" width="130px">State<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlState" CssClass="droplist" runat="server" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lnkState" Text="Add New" runat="server" CssClass="btn btn-success" OnClientClick="window.showModalDialog('NewState.aspx','','dialogWidth:690px; dialogHeight:650px;scrollbars=yes, center:yes');"
                                            TabIndex="2"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px" width="130px">Financial Year<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFinancial" CssClass="droplist" runat="server" TabIndex="3">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px">Calculation Based on<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlWages" CssClass="droplist" runat="server" TabIndex="4">
                                            <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Salary" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Gross" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="130px">Lower Limit<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMinAmount" runat="server" TabIndex="5"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="130px">Upper Limit <span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaxAmount" runat="server" TabIndex="6"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="130px">Tax Amount<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmt" runat="server" TabIndex="7"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Width="100px"
                                            OnClick="btnSubmit_Click" OnClientClick="javascript:return validatesave();" TabIndex="8"
                                            ToolTip="[Alt+s  OR  Alt+s+Enter]" AccessKey="s" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <table id="tblEdit" runat="server" visible="false">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="DedProfAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="DedProfAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <b>Financial Year:</b>
                                                                <asp:DropDownList ID="ddlFinYear" AutoPostBack="true" TabIndex="9" runat="server"
                                                                    CssClass="droplist" OnSelectedIndexChanged="ddlFinYear_SelectedIndexChanged">
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
                                            <asp:TemplateField HeaderText="State">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("StateName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Financial Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFinancial" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Wage">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWage" runat="server" Text='<%#Eval("LongName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Lower Limit" DataField="AmtMin"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Upper Limit" DataField="MaxAmt"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblClaculated" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="anchor__grd edit_grd" CommandArgument='<%#Eval("PTId")%>'
                                                        CommandName="Edt"></asp:LinkButton>
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
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
