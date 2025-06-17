<%@ Page Title="" Language="C#" AutoEventWireup="True"
    CodeBehind="TDS.aspx.cs" Inherits="AECLOGIC.ERP.HMS.TDS" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">
        function validatesave() {
            if (!chkDropDownList('<%=ddlAssessmentType.ClientID%>', "Assessment Type Item", "", ""))
                return false;
            if (!chkDropDownList('<%=ddlAssessmentYear.ClientID%>', "Assessment  Year", "", ""))
                return false;
            if (!chkFloatNumber('<%=txtRangeFrom.ClientID%>', "  Range from", "true", ""))
                return false;
            if (!chkFloatNumber('<%=txtRangeTo.ClientID%>', " Range to", "", ""))
                return false;
            if (!chkFloatNumber('<%=txtRate.ClientID%>', "Rate", "true", ""))
                return false;
        }
        //txtRangeTo
    </script>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td class="pageheader">
                        <asp:LinkButton ID="lnkAdd" runat="server" Text="Add" OnClick="lnkAdd_Click"></asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="lnkEdit_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnltblNew" runat="server" CssClass="DivBorderOlive" Visible="false" Width="50%">
                            <table id="tblNew" runat="server" visible="false">
                                <tr>
                                    <td style="width: 124px">AssessmentType<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAssessmentType" CssClass="droplist" runat="server" TabIndex="1">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">Financial Year<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAssessmentYear" CssClass="droplist" runat="server" TabIndex="2">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Range From<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRangeFrom" runat="server" TabIndex="3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Range To <span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRangeTo" runat="server" TabIndex="4"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Rate<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRate" runat="server" TabIndex="5"></asp:TextBox>
                                    </td>
                                    <td>In Percentage (%)
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="savebutton" Width="100px"
                                            OnClick="btnSubmit_Click" OnClientClick="javascript:return validatesave();" AccessKey="s"
                                            TabIndex="6" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <table id="tblEdit" runat="server" visible="false">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="TDSAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="TDSAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td colspan="2">
                                                                <b>Financial Year:</b>
                                                                <asp:DropDownList ID="ddlFinYear" TabIndex="7" AutoPostBack="true" runat="server"
                                                                    OnSelectedIndexChanged="ddlFinYear_SelectedIndexChanged" CssClass="droplist">
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
                                            <asp:TemplateField HeaderText="Assessment Type" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("Assesse")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Financial Year" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFinancial" runat="server" Text='<%#Eval("AssessmentYear")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Range From" DataField="RangeFrom"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Range To" DataField="RangeTo1"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <asp:TemplateField HeaderText="Tax Rate" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblClaculated" runat="server" Text='<%#Convert.ToDouble(Eval("Rate")).ToString("#0.00%")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("TDSId")%>'
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
