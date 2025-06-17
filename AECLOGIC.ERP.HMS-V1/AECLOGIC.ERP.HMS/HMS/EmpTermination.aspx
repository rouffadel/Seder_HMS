<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="EmpTermination.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpTermination" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function RadioCheck(rb) {
            var gv = document.getElementById("<%=grdEmployees.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }
        function Getid(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlworksites_hid.ClientID %>').value = HdnKey;
        }
        <%-- alert(document.getElementById('<%=ddlWs_hid.ClientID %>').value);--%>
        function Deptid(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddldepartments_hid.ClientID %>').value = HdnKey;
            <%-- alert(document.getElementById('<%=ddlWs_hid.ClientID %>').value);--%>
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td colspan="2" class="pageheader">Employee Termination
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 100%;">
                        <table id="tblSearch" runat="server" width="100%" visible="false">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                        ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                        FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td colspan="2" width="600Px">
                                                                <b>
                                                                    <asp:Label ID="lblWS" runat="server" Text="WorkSite:"></asp:Label>
                                                                </b>&nbsp;<%--<asp:DropDownList ID="ddlworksites" Visible="false" runat="server" CssClass="droplist" AutoPostBack="true" OnSelectedIndexChanged="ddlworksites_SelectedIndexChanged"  Width="15%" AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]" TabIndex="1">
                                                                 </asp:DropDownList>--%>
                                                                <b>
                                                                    <asp:HiddenField ID="ddlworksites_hid" runat="server" />
                                                                    <asp:TextBox ID="txtSearchWorksite" OnTextChanged="Worksidechangewithdep" AutoPostBack="true" Height="22Px" Width="140Px" runat="server"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="Getid">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblDept" runat="server" Text="Departments:"></asp:Label>
                                                                </b>
                                                                <b>
                                                                    <asp:HiddenField ID="ddldepartments_hid" runat="server" />
                                                                    <asp:TextBox ID="txtSearchdept" OnTextChanged="GetDepartmentSearch" Height="22px" Width="150px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListdept" ServicePath="" TargetControlID="txtSearchdept"
                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="Deptid">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchdept"
                                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblEmpID" runat="server" Text="EmpID:"></asp:Label>
                                                                </b>
                                                                <asp:TextBox ID="txtempid" runat="server" Width="5%" TabIndex="3" AccessKey="2" ToolTip="[Alt+2]"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FTBEmpID" runat="server" TargetControlID="txtempid" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                                                &nbsp;&nbsp;&nbsp;
                                                                <b>&nbsp;&nbsp;
                                                                    <asp:Label ID="lblEmpName" runat="server" Text="Name:"></asp:Label>
                                                                </b>
                                                                <asp:TextBox ID="txtusername" runat="server" TabIndex="4" AccessKey="3" ToolTip="[Alt+3]" MaxLength="50"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionListemp" ServicePath="" TargetControlID="txtusername"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtusername"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Filter Name]"></cc1:TextBoxWatermarkExtender>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" TabIndex="5" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]"
                                                                    CssClass="btn btn-primary" Width="100px" />
                                                            </td>
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
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table id="tblEmpResult" width="100%" runat="server" visible="false">
                            <tr>
                                <td colspan="2" class="pageheader" style="height: 21px">
                                    <asp:Label ID="Label1" runat="server" Text="Select Employee"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="grdEmployees" EmptyDataText="No Employee(s) Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        runat="server" AutoGenerateColumns="false" Width="80%"
                                        HeaderStyle-CssClass="tableHead" ForeColor="#333333" GridLines="Both" CssClass="gridview">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" HeaderStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="rbSelect" AutoPostBack="true" onclientclick="javascript:RadioCheck(this);"
                                                        OnCheckedChanged="rbSelect_CheckedChanged" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EmpID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblempid" runat="server" Text='<%#Eval("EmpId") %>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Worksite" SortExpression="Categary" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# GetWorkSite(DataBinder.Eval(Container.DataItem, "Categary").ToString())%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Department" HeaderText="Department" />
                                            <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="150" />
                                                <HeaderStyle Width="150" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesignation" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>
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
                            <tr>
                                <td style="height: 17px">
                                    <uc1:Paging ID="EmpTerminationPaging" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 17px">
                                    <asp:HiddenField ID="hdnSearchChange" Value="0" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table id="tblTerminate" runat="server" width="100%" visible="false">
                            <tr>
                                <td colspan="2">
                                    <asp:DetailsView ID="dvTermination" EmptyDataRowStyle-CssClass="EmptyRowData" HeaderText="Employee Details"
                                        RowStyle-CssClass="gridview" Font-Size="Small" HeaderStyle-CssClass="tableHead"
                                        ForeColor="#333333" GridLines="Both" EmptyDataText="No Employee(s) Found" CssClass="gridview"
                                        AutoGenerateRows="false" runat="server" Height="50px" Width="358px">
                                        <RowStyle CssClass="gridview"></RowStyle>
                                        <EmptyDataRowStyle CssClass="EmptyRowData"></EmptyDataRowStyle>
                                        <Fields>
                                            <asp:BoundField DataField="EmpID" HeaderText="EmpID:" />
                                            <asp:BoundField DataField="Name" HeaderText="Name:" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation:" />
                                            <asp:TemplateField HeaderText="Worksite:" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWorksite" runat="server" Text='<%# GetWorkSite(DataBinder.Eval(Container.DataItem, "Categary").ToString())%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department:" />
                                            <asp:BoundField DataField="Manager" HeaderText="Head:" />
                                            <asp:BoundField DataField="Mobile" HeaderText="Mobile:" />
                                            <asp:BoundField DataField="Mailid" HeaderText="E-Mail:" />
                                            <asp:BoundField DataField="DOJ" HeaderText="DOJ:" />
                                            <asp:BoundField DataField="ResAddress" HeaderText="Present Address:" />
                                            <asp:BoundField DataField="Salary" HeaderText="Salary/Month:" />
                                        </Fields>
                                        <HeaderStyle CssClass="tableHead"></HeaderStyle>
                                    </asp:DetailsView>
                                </td>
                            </tr>
                            <tr>
                                <td width="10Px"></td>
                                <td>
                                    <asp:Button ID="btnTerminate" runat="server" CssClass="btn btn-primary" Text="Terminate"
                                        OnClientClick="return confirm('Are you Sure?');" OnClick="btnTerminate_Click" AccessKey="s" ToolTip="[Alt+s+Enter]" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" AccessKey="b" ToolTip="[Alt+b+Enter]" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table id="tblSMSMail" runat="server" width="100%" visible="false">
                            <tr>
                                <td style="padding-right: 300Px" class="pageheader" align="center">SMS and Mailing System
                                </td>
                            </tr>
                            <tr>
                                <td class="pageheader" colspan="2">Employee
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvSMSMailEmp" runat="server" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        EmptyDataText="No Records Found" AutoGenerateColumns="false" Width="70%" CssClass="gridview"
                                        HeaderStyle-CssClass="tableHead">
                                        <Columns>
                                            <%--<asp:BoundField DataField="EmpType"  />--%>
                                            <asp:TemplateField HeaderText="EmpID" Visible="false" SortExpression="EmpId">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpId" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name" SortExpression="name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpName" runat="server" Text='<%#Bind("name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Department" SortExpression="DepartmentName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartment" runat="server" Text='<%#Bind("DepartmentName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Designation" HeaderStyle-HorizontalAlign="Left" HeaderText="Designation"
                                                SortExpression="Design" />
                                            <asp:TemplateField HeaderText="SMS" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSmsEmp" Checked="true" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mail" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMailEmp" Checked="true" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="pageheader" colspan="2">Employee's Head
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvSMSMailHead" runat="server" AutoGenerateColumns="false" Width="70%"
                                        CssClass="gridview" EmptyDataRowStyle-CssClass="EmptyRowData" EmptyDataText="No Records Found"
                                        HeaderStyle-CssClass="tableHead">
                                        <Columns>
                                            <%-- <asp:BoundField DataField="EmpType" />--%>
                                            <asp:TemplateField HeaderText="EmpID" Visible="false" SortExpression="EmpId">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpId" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name" SortExpression="name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpName" runat="server" Text='<%#Bind("name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Department" SortExpression="DepartmentName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartment" runat="server" Text='<%#Bind("DepartmentName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Designation" HeaderStyle-HorizontalAlign="Left" HeaderText="Designation"
                                                SortExpression="Design" />
                                            <asp:TemplateField HeaderText="SMS" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSmsHead" Checked="true" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mail" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMailHead" Checked="true" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="pageheader" colspan="2">Accounts Department,Head Office
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvSMSMailAccDept" Width="70%" runat="server" AutoGenerateColumns="false"
                                        CssClass="gridview" EmptyDataRowStyle-CssClass="EmptyRowData" EmptyDataText="No Records Found"
                                        HeaderStyle-CssClass="tableHead">
                                        <Columns>
                                            <%--  <asp:BoundField DataField="EmpType" />--%>
                                            <asp:TemplateField HeaderText="EmpID" Visible="false" SortExpression="EmpId">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpId" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name" SortExpression="name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpName" runat="server" Text='<%#Bind("name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Department" SortExpression="DepartmentName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartment" runat="server" Text='<%#Bind("DepartmentName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Designation" HeaderStyle-HorizontalAlign="Left" HeaderText="Designation"
                                                SortExpression="Design" />
                                            <asp:TemplateField HeaderText="SMS" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSmsAcc" Checked="true" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mail" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMailAcc" Checked="true" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 300Px">
                                    <asp:Button ID="btnSMSMail" runat="server" Text="Send" CssClass="btn btn-primary" OnClick="btnSMSMail_Click" AccessKey="s" ToolTip="[Alt+s+Enter]" />
                                </td>
                                <td>&nbsp;
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
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
