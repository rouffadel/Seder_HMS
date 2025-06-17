<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Directors.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.Directors" Title="" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td>
                        <AEC:Topmenu ID="topmenu" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:RadioButtonList ID="gvDirector" runat="server">
                        </asp:RadioButtonList>
                        <asp:Button ID="btnSave" Text="Save" runat="server"
                            CssClass="savebutton btn btn-success" OnClick="btnSave_Click"
                            Height="21px" AccessKey="s" ToolTip="[Alt+s OR Alt+s+Enter]"
                            TabIndex="1" /><br />
                        <br />
                        <font color="#ff0000">Note: Selected person will be the Chairman</font>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gveditkbipl" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpId"
                            EmptyDataText="No Records Found" HeaderStyle-CssClass="tableHead" EmptyDataRowStyle-CssClass="EmptyRowData"
                            CssClass="gridview" OnRowCommand="gveditkbipl_RowCommand">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="EmpID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpId" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpName" runat="server" Text='<%#String.Format("{0} {1} {2} {3}", DataBinder.Eval(Container.DataItem, "EmpId"),DataBinder.Eval(Container.DataItem, "FName"), DataBinder.Eval(Container.DataItem, "MName"), DataBinder.Eval(Container.DataItem, "LName")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Design" HeaderText="Designation" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                <asp:BoundField DataField="Category" HeaderText="Trades" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                <asp:TemplateField HeaderText="Worksite" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorksite" runat="server" Text='<%# Eval("Site_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("DepartmentName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Mobile1" HeaderText="Mobile1" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>

                                <asp:ButtonField CommandName="Edit" HeaderText="Edit" InsertVisible="False" Text="Edit"
                                    ControlStyle-ForeColor="Blue" HeaderStyle-HorizontalAlign="Left" />

                                <asp:TemplateField HeaderText="Documents" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <a id="A1" href='<%# String.Format("empdocuments.aspx?eid={0}",DataBinder.Eval(Container.DataItem, "EmpId")) %>'
                                            runat="server">View</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payroll" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <a id="A2" href='<%# String.Format("EmpPayRoleConfig.aspx?eid={0}",DataBinder.Eval(Container.DataItem, "EmpId")) %>'
                                            runat="server">Configure</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CTC" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <a id="A3" href='<%# String.Format("EmpSalHikes.aspx?EmpID={0}",DataBinder.Eval(Container.DataItem, "EmpId")) %>'
                                            runat="server">Revise</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Savings" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <a id="A4" href='<%# String.Format("ITSavings.aspx?EmpID={0}",DataBinder.Eval(Container.DataItem, "EmpId")) %>'
                                            runat="server">View</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                        </asp:GridView>
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
