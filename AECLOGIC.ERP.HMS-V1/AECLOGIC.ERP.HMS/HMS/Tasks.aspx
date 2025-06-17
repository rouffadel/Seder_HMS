<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="Tasks.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.Tasks" Title="" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function validatesave() {
            if (document.getElementById('<%=txtTaskName.ClientID%>').value == "") {
                alert("Please Enter  TaskName ");
                return false;
            }

        }
    </script>
    <table style="width: 100%" align="center">
         
        <tr>
            <td>
                <asp:MultiView ID="mainview" runat="server">
                    <asp:View ID="Newvieew" runat="server">
                        <table width="100%">
                            <tr>
                                <td colspan="2" class="pageheader">
                                    Tasks
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 103px">
                                    Date
                                </td>
                                <td style="height: 22px">
                                    &nbsp;<asp:Label ID="lbldate" runat="server" Text="lblDate"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 103px">
                                    TaskName
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTaskName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 103px">
                                    DueDate
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDueDate" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDueDate"
                                        PopupButtonID="txtDueDate" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 103px">
                                    Status
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkStatus" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 103px">
                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="savebutton" Width="100px"
                                        OnClick="btnsubmit_Click" />
                                </td>
                                <td style="text-align: left">
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="EditView" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                            <tr>
                                <td class="pageheader">
                                    Tasks List
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnknew" runat="server" Text="New" OnClick="lnknew_Click" Font-Underline="true"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 100%">
                                    <asp:GridView ID="gvTasks" Width="100%" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" ForeColor="#333333" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        OnRowCommand="gvTasks_RowCommand" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="TaskName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaskName" runat="server" Text='<%#Eval("TaskName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Due Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblduedate" runat="server" Text='<%#Eval("DueDate")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkedt" runat="server" CommandArgument='<%#Eval("TaskID") %>'
                                                        CommandName="Edt" Text="Edit"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="60px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkdel" runat="server" CommandArgument='<%#Eval("TaskID") %>'
                                                        CommandName="Del" Text="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="60px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />
                                        <EmptyDataRowStyle CssClass="EmptyRowData" />
                                        <HeaderStyle CssClass="tableHead" />
                                    </asp:GridView>
                                </td>
                            </tr>
                         
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>
</asp:Content>
