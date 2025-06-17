<%@ Page Title="" Language="C#" AutoEventWireup="True"
    CodeBehind="AllowancesCalculations.aspx.cs" Inherits="AECLOGIC.ERP.HMS.AllowancesCalculations" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">

        function validatesave() {

            if (!chkDropDownList('<%=ddlAllowances.ClientID%>', "Allowances", "", ""))
                return false;
            if (!chkDropDownList('<%=ddlWages.ClientID%>', "Wages", "", ""))
                return false;
            if (!chkDropDownList('<%=ddlFinancial.ClientID%>', " Financial Year", "", ""))
                return false;
            if (!chkFloatNumber('<%=txtPercentage.ClientID%>', " Percentage", "true", ""))
                return false;
            //if (!chkFloatNumber('<%=txtLimited.ClientID%>', "  Limited To","true",""))
            //return false;
        }

    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <asp:Panel ID="pnltblNew" runat="server" CssClass="box box-primary" Visible="false" Width="50%">

                            <table id="tblNew" runat="server" visible="false">
                                <tr>
                                    <td style="width: 124px">Allowances<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAllowances" CssClass="droplist" runat="server"
                                            TabIndex="1">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">Calculation based on<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlWages" CssClass="droplist" runat="server" TabIndex="2">
                                            <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Salary" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="CTC" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">Financial Year<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFinancial" CssClass="droplist" runat="server"
                                            TabIndex="3">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Percentage<span style="color: #ff0000">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPercentage" runat="server" TabIndex="4"></asp:TextBox>In Percentage (%)
                                    </td>
                                </tr>
                                <tr>
                                    <td>Limited To<%--<span style="color: #ff0000">*</span>--%></td>
                                    <td>
                                        <asp:TextBox ID="txtLimited" runat="server" TabIndex="5"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Width="100px"
                                            OnClick="btnSubmit_Click"
                                            OnClientClick="javascript:return validatesave();" TabIndex="6"
                                            AccessKey="s" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <table id="tblEdit" runat="server" visible="false">
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
                                                                <asp:DropDownList ID="ddlFinYear" AutoPostBack="true" TabIndex="7" runat="server" CssClass="droplist"
                                                                    OnSelectedIndexChanged="ddlFinYear_SelectedIndexChanged">
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
                                            <asp:TemplateField HeaderText="Financial Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDays" runat="server" Text='<%#Eval("FinaciaYear")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allowance" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Percentage" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPercentage" runat="server" Text='<%#Convert.ToDouble(Eval("Percentage")).ToString("#.00%")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Claculated On">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblClaculated" runat="server" Text='<%#Eval("CalculatedOn")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Limited To" DataField="LimitedTo"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="anchor__grd edit_grd" CommandArgument='<%#Eval("AllowCalcId")%>'
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
                        <br />
                        <asp:Panel ID="pnltblOrder" runat="server" CssClass="box box-primary" Visible="false" Width="50%">

                            <table id="tblOrder" runat="server" visible="false">
                                <tr>
                                    <td style="vertical-align: top; width: 200px">
                                        <asp:ListBox ID="lstDepartments" runat="server" Height="400px" AccessKey="1"
                                            TabIndex="8" ToolTip="[Alt+1]"></asp:ListBox>
                                    </td>
                                    <td style="vertical-align: middle; width: 100px">
                                        <asp:Button ID="btnFirst" runat="server" Text="Move First" Width="80px"
                                            OnClick="btnFirst_Click" AccessKey="2" CssClass="btn btn-success" TabIndex="9" ToolTip="[Alt+2]" /><br />
                                        <br />
                                        <asp:Button ID="btnUp" runat="server" Text="Move Up" Width="80px"
                                            OnClick="btnUp_Click" AccessKey="3" TabIndex="10" CssClass="btn btn-primary" ToolTip="[Alt+3]" /><br />
                                        <br />
                                        <asp:Button ID="btnDown" runat="server" Text="Move Down" Width="80px"
                                            OnClick="btnDown_Click" AccessKey="4" TabIndex="11" CssClass="btn btn-warning" ToolTip="[Alt+4]" /><br />
                                        <br />
                                        <asp:Button ID="btnLast" runat="server" Text="Move Last" Width="80px"
                                            OnClick="btnLast_Click" AccessKey="5" TabIndex="12" CssClass="btn btn-danger" ToolTip="[Alt+5]" /><br />
                                        <br />
                                    </td>
                                    <td style="vertical-align: middle">
                                        <asp:Button ID="btnOrder" runat="server" Text="Submit" Width="100px"
                                            OnClick="btnOrder_Click" AccessKey="s" CssClass="btn btn-primary" TabIndex="13"
                                            ToolTip="[Alt+s OR Alt+s+Enter]" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
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
