<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="TodoList.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.TodoList" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function validatesave() {
            if (document.getElementById('<%=txtListItem.ClientID%>').value == "") {
                alert("Please Enter  TaskName ");
                return false;
            }
            if (document.getElementById('<%=txtAuthority.ClientID%>').value == "") {
                alert("Please Enter  Authority ");
                return false;
            }
        }
    </script>
    <table width="100%">
         
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:MultiView ID="mainview" runat="server">
                                <asp:View ID="Newvieew" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" class="pageheader">
                                                Todo List Item
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 103px">
                                                Task
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtListItem" runat="server" Width="406px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 103px">
                                                Authority
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAuthority" runat="server" Width="405px"></asp:TextBox>
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
                                                Todo List
                                            </td>
                                           
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width: 100%">
                                                <asp:GridView ID="gvToddoList" Width="100%" runat="server" AutoGenerateColumns="False"
                                                    CellPadding="4" ForeColor="#333333" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                                    HeaderStyle-CssClass="tableHead" OnRowCommand="gvToddoList_RowCommand" CssClass="gridview">
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCheckListID" runat="server" Text='<%#Eval("CheckListID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Task">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblListItem" runat="server" Text='<%#Eval("ListItem")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Authority">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAuthority" runat="server" Text='<%#Eval("Authority")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="StartDate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStartDate" runat="server" Text='<%#Eval("StartDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FinishDate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFinishDate" runat="server" Text='<%#Eval("EndDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTodoListID" runat="server" Text='<%#Eval("ListID")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
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
            </td>
        </tr>
    </table>
</asp:Content>
