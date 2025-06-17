<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Encashment_AL.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Encashment_AL"
    MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">
        function fnConfirm() {
            if (confirm("The item will be deleted. Are you sure want to continue?") == true)
                return true;
            else
                return false;
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <div id="dvAdd" runat="server">
                <asp:Panel ID="Panel6" runat="server" CssClass="box box-primary" Width="50%">
                    <table id="tblNew" runat="server">
                        <tr>
                            <td colspan="2" class="pageheader">Encashment of AL limited
                            </td>
                        </tr>
                        <tr>
                            <td>Emp ID/Name<span style="color: #ff0000">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" Height="20px" Width="150px" runat="server"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtName"
                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                </cc1:AutoCompleteExtender>
                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtName"
                                    WatermarkCssClass="Watermarktxtbox" WatermarkText="[Emp ID/Name]"></cc1:TextBoxWatermarkExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbllvrd" runat="server" ToolTip="Last Vacation Return Date(LVRD)" Text="Last Vacation Return Date (LVRD)"></asp:Label>
                                <span style="color: #ff0000">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtlvrd" Height="20px" Width="150px" ToolTip="Last Vacation Return Date" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtlvrd"
                                    PopupButtonID="txtDOB" Format="dd MMM yyyy"></cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbllop" runat="server" ToolTip="Loss of Pay Opening Balance" Text="Loss of Pay Opening Balance(LOP OB)"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtlopt" Height="20px" Width="150px" ToolTip="Loss of Pay" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" ToolTip="Utilized LOP after LVRD " Text="Utilized LOP after LVRD(ULOP)"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtlop2v" Height="20px" Width="150px" ToolTip="Utilized LOP after LVRD(ULOP)" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAAl" runat="server" ToolTip="Utilized AL after LVRD (UAL)" Text="Utilized AL after LVRD (UAL)"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAAL" Height="20px" Width="150px" ToolTip="Utilized AL after LVRD-AAL" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" ToolTip="Penalty Occurrence Opening Balance (POOB)" Text="Penalty Occurrence Opening Balance (POOB)"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOccuranceOOP" Height="20px" Width="150px" ToolTip="Occurance OOP" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" ToolTip="A/C Start Date" Text="A/C Start Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtActionDt" Height="20px" Width="150px" Text="01 jan 2016" ToolTip="A/C Start Date" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtActionDt"
                                    PopupButtonID="txtActionDt" Format="dd MMM yyyy"></cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>.</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="2">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" Width="80px"
                                    OnClick="btnSubmit_Click" OnClientClick="javascript:return validatesave();" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <br />
            <div id="dvEdit" runat="server">
                <table id="tblEdit" runat="server" width="100%">
                    <tr>
                        <td align="left">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td colspan="2">
                                        <AEC:Topmenu ID="topmenu1" runat="server" />
                                        <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                            ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                            FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                                            SelectedIndex="0">
                                            <Panes>
                                                <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                    ContentCssClass="accordionContent">
                                                    <Header>
                                            Search Criteria</Header>
                                                    <Content>
                                                        <table>
                                                            <tr>
                                                                <td>&nbsp;
                                                        <asp:Label ID="lblEmpName" runat="server" Text="Employee Id/Name:-"></asp:Label>
                                                                    <asp:TextBox ID="txtSearchEmployee" Height="22px" Width="140px" runat="server"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtSearchEmployee"
                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchEmployee"
                                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Emp ID/Name]"></cc1:TextBoxWatermarkExtender>
                                                                    &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                                        CssClass="btn btn-primary" Width="60px" AccessKey="i" ToolTip="[Alt+i]" TabIndex="6" />
                                                            </tr>
                                                        </table>
                                                    </Content>
                                                </cc1:AccordionPane>
                                            </Panes>
                                        </cc1:Accordion>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="gvFinancialYear" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                OnRowCommand="gvFinancialYear_RowCommand" HeaderStyle-CssClass="tableHead"
                                CssClass="gridview" Width="100%">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                <Columns>
                                    <asp:TemplateField HeaderText="EmpId">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Empid")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmployee" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Last Vacation Return Date (LVRD)">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("LVRD")%>' Style="width: 25%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LOP(For Gratuity)">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("lop2")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Utilized LOP after LVRD (ULOP)">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("lop2v")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Utilized AL after LVRD (UAL)">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("AAl")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Penalty Occurrence Opening Balance(POOB)">
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%#Eval("occuranceoop")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="A/C Start Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActionDt" runat="server" Text='<%#Eval("ActionDt")%>' Style="width: 25%"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="anchor__grd edit_grd" CommandArgument='<%#Eval("Empid")%>'
                                                CommandName="Edt"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" OnClientClick="return fnConfirm();" runat="server" CssClass="anchor__grd dlt" Text="Delete" CommandArgument='<%#Eval("Empid")%>'
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
                    <tr>
                        <td colspan="2" style="height: 17px">
                            <uc1:Paging ID="EmpListPaging" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
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
