<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="ListOfEmpMessConfiguration.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ListOfEmpMessConfiguration" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script type="text/javascript" language="javascript">
        function ValidMessConfig() {

            if (!chkDropDownList('<%=ddlMessType.ClientID%>', " Mess type", "", ""))
                return false;

            if (!chkDropDownList('<%=ddlWS.ClientID%>', "Worksite", "", ""))
                return false;

            if (!chkDropDownList('<%=ddlProfileType.ClientID%>', " Employee nature", "", ""))
                return false;

            if (!chkDate('<%=txtFrmDate.ClientID%>', "From date", "true", ""))
                return false;

            if (!chkDate('<%=txtToDate.ClientID%>', "To date", "true", ""))
                return false;
            if (!chkFloatNumber('<%=txtAmount.ClientID%>', "Amount", "true", ""))
                return false;
        }
    </script>
   
    <div id="dvAdd" runat="server" visible="false">
        <asp:Panel ID="pnl11" runat="server" CssClass="DivBorderOlive">
            <table id="tblNew" runat="server" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="subheader" colspan="2" style="width: 124px">
                        <asp:Label ID="lblHeadline" runat="server" Text="Mess Config Setup"></asp:Label>
                    </td>
                    <td class="subheader" colspan="2" style="width: 800px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 124px">
                        <asp:Label ID="lblMesstype" runat="server" Text="MealType"></asp:Label>
                        <span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMessType" runat="server" CssClass="droplist" AccessKey="1"
                            TabIndex="1" ToolTip="[Alt+1]">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 124px">
                        <asp:Label ID="lblWS" runat="server" Text="Worksite"></asp:Label>
                        <span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlWS" Visible="false" runat="server" CssClass="droplist" AccessKey="w" TabIndex="2"
                            ToolTip="[Alt+w OR Alt+w+Enter]">
                        </asp:DropDownList>
                         <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                            </cc1:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 124px">
                        <asp:Label ID="lblEmpNature" runat="server" Text="Emp Nature"></asp:Label>
                        <span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProfileType" CssClass="droplist" runat="server" TabIndex="3"
                            AccessKey="2" ToolTip="[Alt+2]">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 124px">
                        <asp:Label ID="lblFrmDate" runat="server" Text="From Date"></asp:Label>
                        <span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFrmDate" runat="server" TabIndex="4" AccessKey="t" ToolTip="[Alt+t OR Alt+t+Enter]"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFrmDate"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="txtwmext" runat="server" TargetControlID="txtFrmDate"
                            WatermarkText="[Select from date]">
                        </cc1:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 124px">
                        <asp:Label ID="lblToDate" runat="server" Text="ToDate"></asp:Label>
                        <span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtToDate" runat="server" TabIndex="5" AccessKey="y" ToolTip="[Alt+y OR Alt+y+Enter]"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="txtwmexttodate" runat="server" TargetControlID="txtToDate"
                            WatermarkText="[Select to date]">
                        </cc1:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 124px">
                        <asp:Label ID="lblAmt" runat="server" Text="Amount"></asp:Label>
                        <span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server" AccessKey="3" TabIndex="6" ToolTip="[Alt+3]"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtwmexAmt" runat="server" TargetControlID="txtAmount"
                            WatermarkText="[Enter amount]">
                        </cc1:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 124px">
                        <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" TabIndex="7"
                            AccessKey="4" ToolTip="[Alt+4]" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="savebutton" Width="100px"
                            AccessKey="s" TabIndex="8" ToolTip="[Alt+s OR Alt+s+Enter]" OnClientClick="javascript:return ValidMessConfig();"
                            OnClick="btnSubmit_Click" />
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
                                            <td style="width: 175px">
                                                <b>
                                                    <asp:Label ID="Label1" runat="server" Text="Worksite"></asp:Label>:</b> &nbsp;<asp:DropDownList
                                                        ID="ddlWorkSite" runat="server" CssClass="droplist" TabIndex="1" AccessKey="w"
                                                        ToolTip="[Alt+w OR Alt+w+Enter]">
                                                    </asp:DropDownList>
                                            </td>
                                            <td style="width: 75px">
                                                <b>
                                                    <asp:Label ID="lblMestype" runat="server" Text="MealType"></asp:Label>:</b>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="ddlMessType1" runat="server" CssClass="droplist" AccessKey="1"
                                                    ToolTip="[Alt+1]" TabIndex="2">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 125px">
                                                <b>
                                                    <asp:Label ID="lblEmpNat" runat="server" Text="Emp Nature"></asp:Label>:</b>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="ddlEmpNat" runat="server" CssClass="droplist" AccessKey="2"
                                                    ToolTip="[Alt+2]" TabIndex="3">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 50px">
                                                <b>
                                                    <asp:Label ID="lblMonth" runat="server" Text="Month"></asp:Label>:</b>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="droplist" AccessKey="3"
                                                    ToolTip="[Alt+3]" TabIndex="4">
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
                                            </td>
                                            <td style="width: 50px">
                                                <b>
                                                    <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>: </b>
                                            </td>
                                            <td style="width: 100px">
                                                <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" CssClass="droplist"
                                                    AccessKey="4" ToolTip="[Alt+4]" TabIndex="5">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="savebutton" OnClick="btnSearch_Click"
                                                    TabIndex="6" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="rblMessStatus" runat="server" RepeatDirection="Horizontal"
                                                    TabIndex="7" AccessKey="5" ToolTip="[Alt+5]" OnSelectedIndexChanged="rblMessStatus_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Selected="True" Value="1" Text="Active"></asp:ListItem>
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
                <td colspan="2" style="width: 100%">
                    <asp:GridView ID="gvMessTypes" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                        HeaderStyle-CssClass="tableHead" CssClass="gridview" Width="100%" OnRowCommand="gvMessTypes_RowCommand">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                        <Columns>
                            <asp:TemplateField HeaderText="MCID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblMCID" runat="server" Text='<%#Eval("MCID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MealType">
                                <ItemTemplate>
                                    <asp:Label ID="lblMesstype" runat="server" Text='<%#Eval("MessType")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Worksite">
                                <ItemTemplate>
                                    <asp:Label ID="lblWorkSite" runat="server" Text='<%#Eval("WS")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nature">
                                <ItemTemplate>
                                    <asp:Label ID="lblNature" runat="server" Text='<%#Eval("EmpNat")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Value")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("MCID")%>'
                                        CommandName="Edt"></asp:LinkButton></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDel" runat="server" Text='<%#GetText()%>' CommandArgument='<%#Eval("MCID")%>'
                                        CommandName="del"></asp:LinkButton></ItemTemplate>
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
                    <uc1:Paging ID="ListOfEmpMessPaging" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
