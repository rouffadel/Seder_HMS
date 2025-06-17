<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="True"
    CodeBehind="MapWorksite.aspx.cs" Inherits="MapWorksite" %>

<%@ Register Src="Controls/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function Multiply(contorl) {
            if (checkdecmial(contorl) == false) {
                contorl.focus();
                return false;
            }
        }

    </script>
    <asp:UpdatePanel ID="Up1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <AEC:Topmenu ID="topmenu" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <ajax:Accordion ID="ACC" runat="server" Width="100%" TransitionDuration="50" FramesPerSecond="10"
                            HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent" RequireOpenedPane="False" SuppressHeaderPostbacks="True">
                            <Panes>
                                <ajax:AccordionPane ID="Ap1" runat="server" ContentCssClass="" HeaderCssClass="">
                                    <Header>
                                        <b>Search Criteria </b>
                                    </Header>
                                    <Content>
                                        <table id="t11" cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td valign="top">
                                                                &nbsp;
                                                                <asp:Label ID="LblWorkSite" runat="server" Text="Worksite:"></asp:Label>
                                                                &nbsp;<asp:DropDownList ID="ddlDropWorkSite" runat="server" CssClass="droplist" AccessKey="w"
                                                                    ToolTip="[Alt+w OR Alt+w+Enter]" TabIndex="1">
                                                                </asp:DropDownList>
                                                                <ajax:ListSearchExtender ID="ListSearchExtender1" runat="server" Enabled="True" IsSorted="True"
                                                                    PromptText="Type Here To Search" QueryPattern="Contains" TargetControlID="ddlDropWorkSite">
                                                                </ajax:ListSearchExtender>
                                                            </td>
                                                            <td valign="top">
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="lblDepartments" runat="server" Text="Departments:"></asp:Label>
                                                                &nbsp;<asp:DropDownList ID="ddlDept" runat="server" CssClass="droplist" AccessKey="1"
                                                                    ToolTip="[Alt+1]" TabIndex="2">
                                                                </asp:DropDownList>
                                                                <ajax:ListSearchExtender ID="ListSearchExtender2" runat="server" Enabled="True" IsSorted="True"
                                                                    PromptText="Type Here To Search" QueryPattern="Contains" TargetControlID="ddlDept">
                                                                </ajax:ListSearchExtender>
                                                            </td>
                                                            <td valign="top">
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="lblEmpName" runat="server" Text="EmpName:"></asp:Label>
                                                                &nbsp;<asp:TextBox ID="txtname" runat="server" TabIndex="3" AccessKey="2" ToolTip="[Alt+2]"></asp:TextBox>
                                                                <ajax:TextBoxWatermarkExtender ID="TW1" runat="server" Enabled="True" TargetControlID="txtname"
                                                                    WatermarkText="[Employe Name]">
                                                                </ajax:TextBoxWatermarkExtender>
                                                            </td>
                                                            <td valign="top">
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="BtnSearch" runat="server" CssClass="ButtonStyle" Text="Search" OnClick="BtnSearch_Click"
                                                                    TabIndex="4" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </Content>
                                </ajax:AccordionPane>
                            </Panes>
                        </ajax:Accordion>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tbl1Grid" runat="server" visible="False" width="100%">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <asp:GridView ID="GVAtten" runat="server" Width="100%" HeaderStyle-CssClass="tableHead"
                                        AutoGenerateColumns="false" AllowSorting="true" EmptyDataText="No Record(s) Found"
                                        EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BackColor="GhostWhite"
                                        OnRowCommand="GVAtten_RowCommand" OnRowDataBound="GVAtten_RowDataBound" CssClass="gridview">
                                        <HeaderStyle CssClass="tableHead" />
                                        <RowStyle CssClass="gentext" />
                                        <AlternatingRowStyle BackColor="GhostWhite" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" AutoPostBack="false" ID="Chk" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblempid" runat="server" Text='<%#Eval("empid") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SiteName" HeaderText="Worksite" SortExpression="SiteName">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="departmentname" HeaderText="Department" SortExpression="departmentname">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="name" HeaderText="Employee Name" SortExpression="name">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="designation" HeaderText="Designation" SortExpression="designation">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="Ed" CommandArgument='<%#Eval("empid") %>' Text="Edit"
                                                        runat="server" ID="EditE"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <uc1:Paging ID="MAPaging" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hdnSearchChange" Value="0" runat="server" />
                                </td>
                            </tr>
                            <tr id="Tr2" runat="server">
                                <td id="Td2" runat="server">
                                    <br />
                                    <asp:CheckBoxList runat="server" ID="ChkWS" CellSpacing="2" RepeatColumns="4" RepeatDirection="Horizontal"
                                        TabIndex="5" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1">
                            <ProgressTemplate>
                                <asp:Panel ID="Panel1" CssClass="overlay" runat="server">
                                    <asp:Panel ID="Panel2" CssClass="loader" runat="server">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/updateProgress.gif" ImageAlign="AbsMiddle"
                                            Height="62px" Width="82px" />
                                        please wait...</asp:Panel>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="BtnSave" CssClass="ButtonStyle" runat="server" Text="Save" OnClick="BtnSave_Click"
                            AccessKey="s" TabIndex="6" ToolTip="[Alt+s OR Alt+s+Enter]" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
