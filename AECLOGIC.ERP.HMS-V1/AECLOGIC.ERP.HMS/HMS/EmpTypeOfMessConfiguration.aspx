<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="EmpTypeOfMessConfiguration.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpTypeOfMessConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function ValidMess() {
            if (!chkName('<%=txtName.ClientID%>', "Name", "true", "[Name]"))
                return false;

            if (!chkName('<%=txtSName.ClientID%>', " Short Name", "true", "[ Short Name]"))
                return false;
        }
    </script>
  
    <div id="dvAdd" runat="server" visible="false">
        <asp:Panel ID="pnl11" runat="server" CssClass="DivBorderOlive">
            <table id="tblNewMessConfig" runat="server" cellspacing="0" cellpadding="2">
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="subheader" colspan="2" style="width: 124px">
                        <asp:Label ID="lblHeadline" runat="server" Text="Add Mess Config Type"></asp:Label>
                    </td>
                    <td class="subheader" colspan="2" style="width: 800px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 124px">
                        <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                        <span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Width="150" TabIndex="1" AccessKey="1" ToolTip="[Alt+1]"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtwmetxtname" runat="server" TargetControlID="txtName"
                            WatermarkText="[Enter Name]">
                        </cc1:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 124px">
                        <asp:Label ID="lblShortName" runat="server" Text="Short Name"></asp:Label>
                        <span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSName" runat="server" Width="150" TabIndex="2" AccessKey="2"
                            ToolTip="[Alt+2]"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtwmetxtSname" runat="server" TargetControlID="txtSName"
                            WatermarkText="[Enter short name]">
                        </cc1:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 124px">
                        <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" TabIndex="3"
                            AccessKey="3" ToolTip="[Alt+3]" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="savebutton" OnClientClick="javascript:return ValidMess();"
                            AccessKey="s" TabIndex="5" ToolTip="[Alt+s OR Alt+s+Enter]" OnClick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <br />
    <div id="dvEdit" runat="server" visible="false">
        <table id="tblEdit" runat="server" width="100%">
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
                                                <asp:RadioButtonList ID="rblStatus" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="rblStatus_SelectedIndexChanged" AccessKey="1" ToolTip="[Alt+1]"
                                                    TabIndex="1">
                                                    <asp:ListItem Value="1" Text="Active" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                                </asp:RadioButtonList>
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
                    <asp:GridView ID="gvLeaveProfile" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                        CellPadding="4" ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found"
                        EmptyDataRowStyle-CssClass="EmptyRowData" HeaderStyle-CssClass="tableHead" OnRowCommand="gvLeaveProfile_RowCommand">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="200%" />
                        <Columns>
                            <asp:TemplateField HeaderText="MID"  Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblMID" runat="server" Text='<%#Eval("MID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblMessName" runat="server" Width="200px" Text='<%#Eval("Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Short Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblSName" runat="server" Width="200px" Text='<%#Eval("ShortName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("MID")%>'
                                        CommandName="Edt"></asp:LinkButton></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDel" runat="server" Text='<%#GetText()%>' CommandArgument='<%#Eval("MID")%>'
                                        CommandName="Del"></asp:LinkButton>
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
    </div>
</asp:Content>
