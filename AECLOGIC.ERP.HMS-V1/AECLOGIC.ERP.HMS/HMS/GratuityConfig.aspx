<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="GratuityConfig.aspx.cs" Inherits="AECLOGIC.ERP.HMS.GratuityConfig" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td>
                <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="Addview" runat="server">
                        <tr>
                            <td>
                                <asp:Panel ID="pnltblNew" runat="server" CssClass="DivBorderTeal" Width="50%">
                                    <table>
                                     
                                        <tr>
                                            <td>
                                                No Of Days Worked From
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtGrFrom" runat="server" Width="120" TabIndex="1"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="fteGrFrom" runat="server" FilterType="Numbers,Custom"
                                                    FilterMode="ValidChars" ValidChars="." TargetControlID="txtGrFrom"></cc1:FilteredTextBoxExtender>
                                            </td>
                                            <%-- </tr>
                                        <tr>--%>
                                            <td>
                                                To
                                                <asp:TextBox ID="txtGrTo" runat="server" Width="120" TabIndex="2"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="fteGrTo" runat="server" FilterType="Numbers,Custom"
                                                    FilterMode="ValidChars" ValidChars="." TargetControlID="txtGrTo"></cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 124px">
                                                No Of Days To Accure :<span style="color: #ff0000">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEligibleDays" runat="server" Width="120" TabIndex="3"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="fteEligibleDays" runat="server" FilterType="Numbers,Custom"
                                                    FilterMode="ValidChars" ValidChars="." TargetControlID="txtEligibleDays"></cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 124px">
                                                Payment Days On Resignation :<span style="color: #ff0000">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtresignation" runat="server" Width="120" TabIndex="4"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="fteresignation" runat="server" FilterType="Numbers,Custom"
                                                    FilterMode="ValidChars" ValidChars="." TargetControlID="txtresignation"></cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 124px">
                                                Payment Days On Termination :<span style="color: #ff0000">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTermination" runat="server" Width="120" TabIndex="4"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
                                                    FilterMode="ValidChars" ValidChars="." TargetControlID="txtTermination"></cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 124px">
                                                Indiscipline :<span style="color: #ff0000">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtIndispline" runat="server" Width="120" TabIndex="4"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom"
                                                    FilterMode="ValidChars" ValidChars="." TargetControlID="txtIndispline"></cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 124px">
                                                On Contract Completion :<span style="color: #ff0000">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtContract" runat="server" Width="120" TabIndex="5"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="fteContract" runat="server" FilterType="Numbers,Custom"
                                                    FilterMode="ValidChars" ValidChars="." TargetControlID="txtContract"></cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                       
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" Width="60px"
                                                    TabIndex="6" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-danger" Width="60px"
                                                    TabIndex="7" OnClick="btnClear_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
            </tr> </asp:View>
            <asp:View ID="EditView" runat="server">
                <tr>
                    <td>
                        <asp:GridView ID="gvGratuity" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="AliceBlue"
                            EmptyDataText="No Records Found" HeaderStyle-CssClass="tableHead" Width="60%"
                            OnRowCommand="gvGratuity_RowCommand"  >
                            <AlternatingRowStyle BackColor="AliceBlue" />
                            <Columns>
                                <asp:TemplateField HeaderText="From">
                                    <HeaderStyle Width="80" />
                                    <ItemStyle Width="80" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblFromyear" runat="server" Text='<%#Eval("From")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To">
                                    <HeaderStyle Width="80" />
                                    <ItemStyle Width="80" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblToyear" runat="server" Text='<%#Eval("To")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accure">
                                    <HeaderStyle Width="120" />
                                    <ItemStyle Width="80" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEligibleDays" runat="server" Text='<%#Eval("Accrue")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resignation">
                                    <HeaderStyle Width="90" />
                                    <ItemStyle Width="80" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblResignation" runat="server" Text='<%#Eval("Resign")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Termination">
                                    <HeaderStyle Width="90" />
                                    <ItemStyle Width="80" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTermination" runat="server" Text='<%#Eval("Termination")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Indisciplane" >
                                <HeaderStyle Width="90" />
                                    <ItemStyle Width="80" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndisplane" runat="server" Text='<%#Eval("Indiscipline")%>'></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contract Completion" >
                                <HeaderStyle Width="200px" />
                                    <ItemStyle Width="200px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblContract" runat="server" Text='<%#Eval("ContractCompleation")%>'></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("GID")%>' CommandName="Edt"
                                            Text="Edit" CssClass="anchor__grd edit_grd"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="tableHead" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 29px">
                        <uc1:Paging ID="PagingList" runat="server" />
                    </td>
                </tr>
            </asp:View>
            </asp:MultiView> </td>
        </tr>
    </table>
</asp:Content>
