<%@ Page Title="" Language="C#" AutoEventWireup="True"
    CodeBehind="HolidayPaidRules.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HolidayPaidRules" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script language="javascript" type="text/javascript">
        function validatesave() {
            if (!chkName('<%=txtRuleName.ClientID%>', "Rule Name", "true", "[Short Name]"))
                return false;
            if (!chkDropDownList('<%=ddlCombination1.ClientID%>', "Combination1", "", ""))
                return false;
            if (!chkDropDownList('<%=ddlCombination2.ClientID%>', "Combination2", "", ""))
                return false;
            if (!chkDropDownList('<%=ddlCombination3.ClientID%>', "Combination3", "", ""))
                return false;
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnltblNew" runat="server" CssClass="DivBorderOlive" Visible="false"
                            Width="100%">
                            <table id="tblNew" runat="server" visible="false">
                                <tr>
                                    <td colspan="2" class="pageheader">New Rule
                                    </td>
                                </tr>
                                <tr>
                                    <td>Rule Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRuleName" runat="server" TabIndex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Combination1
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCombination1" CssClass="droplist" runat="server"
                                            TabIndex="2">
                                            <asp:ListItem Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Text="Present" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Abesnt" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="WO & PH" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Combination2
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCombination2" CssClass="droplist" runat="server"
                                            TabIndex="3">
                                            <asp:ListItem Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Text="Present" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Abesnt" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="WO & PH" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Combination3
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCombination3" CssClass="droplist" runat="server"
                                            TabIndex="4">
                                            <asp:ListItem Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Text="Present" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Abesnt" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="WO & PH" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Is Paid
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkStatus" runat="server" Checked="True" TabIndex="5" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tblEdit" runat="server" visible="false">
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvPaidRules" runat="server" AutoGenerateColumns="False" CellPadding="4" OnRowDataBound="gvPaidRules_RowDataBound"
                                        ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        OnRowCommand="gvLeaveProfile_RowCommand" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("RuleName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNature" runat="server" Text='<%#Eval("Comb1")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAllotment" runat="server" Text='<%#Eval("Comb2")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMaxLeaveEligibility" runat="server" Text='<%#Eval("Comb3")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMinDaysOfWork" runat="server" Text='<%#Eval("IsPaid")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="120px">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlIsPaid" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlIsPaid_SelectedIndexChanged">
                                                        <asp:ListItem Text="Payable " Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Non-Payable" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("RuleId")%>'
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
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSave" runat="server" Visible="false" Text="Save" OnClick="btnSave_Click" CssClass="savebutton"
                                        OnClientClick="javascript:return validatesave();" AccessKey="s"
                                        TabIndex="6" ToolTip="[Alt+s OR Alt+s+Enter]" />
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
