<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="TaskAssignment.aspx.cs" Inherits="AECLOGIC.ERP.HMS.TaskAssignment" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
            function validate() {

            if (document.getElementById('<%=txtSubject.ClientID%>').value == "") {
                alert("Please Enter Subject!");
                document.getElementById('<%=txtSubject.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtTask.ClientID%>').value == "") {
                alert("Please Enter Assignment!");
                document.getElementById('<%=txtTask.ClientID%>').focus();
                return false;
            }
            if (!chkDropDownList('<%=ddlEmp.ClientID %>', 'AssignedBy'))
                return false;
            if (!DateCompare('<%=txtStartDate.ClientID %>', '<%=txtDueDate.ClientID %>'))
                return false;
        }


        function DateCompare(From, To) {
            var elmFrom = getObj(From);
            var elmTo = getObj(To);
            var datevalue = elmFrom.value;
            var year = datevalue.substring(6, datevalue.length);
            var month = datevalue.substring(3, 5); var day = datevalue.substring(0, 2);
            var FromDate = new Date();
            FromDate.setFullYear(year, (month - 1), day);

            datevalue = elmTo.value;
            year = datevalue.substring(6, datevalue.length);
            month = datevalue.substring(3, 5); day = datevalue.substring(0, 2);
            var ToDate = new Date();
            ToDate.setFullYear(year, (month - 1), day);
            if (ToDate < FromDate) {
                alert("‘From Date’ should be lessthan or equal to the ‘To Date’"); return false;
            }

            return true;
        }
       
        
    </script>
    <table width="100%">
        
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
    <table id="tblTodo" runat="server" visible="false" width="100%">
        <tr>
            <td class="style2" style="width: 112px">
                <b>Subject:</b>
            </td>
            <td>
                <asp:TextBox ID="txtSubject" Width="350Px" runat="server" TabIndex="1"></asp:TextBox>
                <asp:Label ID="lblSubject" Width="350Px" runat="server" Visible="false" BorderColor="#CC6600"
                    ForeColor="Blue"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 112px">
                <b>Start Date:</b>
            </td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server" TabIndex="2"></asp:TextBox><cc1:CalendarExtender
                    ID="CalendarExtender2" runat="server" TargetControlID="txtStartDate" PopupButtonID="txtStartDate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                <b>Due Date:</b>
            </td>
            <td>
                <asp:TextBox ID="txtDueDate" runat="server" TabIndex="3"></asp:TextBox><cc1:CalendarExtender
                    ID="CalendarExtender1" runat="server" TargetControlID="txtDueDate" PopupButtonID="txtDueDate"
                    Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 112px">
                <b>Priority:</b>
            </td>
            <td>
                <asp:DropDownList ID="ddlPriority" runat="server" CssClass="droplist" TabIndex="4">
                    <asp:ListItem Text="Normal" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Low" Value="1"></asp:ListItem>
                    <asp:ListItem Text="High" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Urgent" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Very Urgent" Value="5"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 112px">
                <b></b>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 112px">
                <b>Assignment:</b>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtTask" runat="server" Font-Bold="False" TextMode="MultiLine" BorderColor="#CC6600"
                    BorderStyle="Outset" Rows="8" Width="51%" Height="70px" TabIndex="5"></asp:TextBox>
                <asp:Label ID="lblTask" Visible="False" Width="51%" runat="server" BorderColor="#CC6600"
                    ForeColor="Blue"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 112px; height: 22px;">
                <b>Assigned To:</b>
            </td>
            <td style="height: 22px">
                &nbsp;
                <asp:DropDownList ID="ddlEmp" runat="server" CssClass="droplist" TabIndex="6">
                </asp:DropDownList>
                <cc1:ListSearchExtender ID="ListSearchExtender1" IsSorted="true" PromptText="Type Here To Search"
                    PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                    TargetControlID="ddlEmp" />
                &nbsp;&nbsp;<asp:TextBox ID="txtEmp" Width="130px" Visible="true" runat="server"
                    AutoPostBack="True" Height="18px" OnTextChanged="txtEmp_TextChanged" TabIndex="7"></asp:TextBox>
                &nbsp;&nbsp;
                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtEmp"
                    WatermarkCssClass="Watermarktxtbox" WatermarkText="[Enter Employee Name]">
                </cc1:TextBoxWatermarkExtender>
                <asp:Button ID="btnEmpSearch" runat="server" Visible="False" CssClass="savebutton"
                      Text="Search" AccessKey="i" TabIndex="8" ToolTip="[Alt+i OR Alt+i+Enter]" />
            </td>
        </tr>
        <tr>
            <td class="style2" style="width: 112px">
                <asp:Button ID="btnSave" runat="server" CssClass="savebutton" Text="Assign" OnClick="btnSave_Click"
                    Visible="False" AccessKey="s" TabIndex="9" ToolTip="[Alt+s OR Alt+s+Enter]" />
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" CssClass="savebutton" Text="Cancel" OnClick="btnCancel_Click"
                    Visible="False" AccessKey="b" TabIndex="10" ToolTip="[Alt+b OR Alt+b+Enter]" />
            </td>
        </tr>
    </table>
    <table id="tblEmpResult" width="100%" runat="server" visible="false">
        <tr>
            <td colspan="2" class="pageheader" style="height: 21px">
                <asp:Label ID="Label2" runat="server" Text="Select Employee"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
    <table id="tblviewTodo" runat="server" width="100%" visible="false">
        <tr>
            <td>
                <cc1:Accordion ID="SimAlloListAccordion" runat="server" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    <Panes>
                        <cc1:AccordionPane ID="SimAlloListAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr id="trAll" runat="server">
                                        <td>
                                            <asp:Label ID="lblAllWS" runat="server" Text="Worksite">
                                            </asp:Label>:
                                            <asp:DropDownList ID="ddlAllWS" visible="false" runat="server" CssClass="droplist">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtSearchWorksite" OnTextChanged="Worksidechangewithdep" Height="22px" Width="140px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                                            </cc1:TextBoxWatermarkExtender>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAllDept" runat="server"  Text="Department"></asp:Label>:
                                            <asp:DropDownList ID="ddlAllDept" visible="false" runat="server" CssClass="droplist">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtSearchdept" OnTextChanged="GetDepartmentSearch" Height="22px" Width="140px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListdept" ServicePath="" TargetControlID="txtSearchdept"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtSearchdept"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                                                </cc1:TextBoxWatermarkExtender>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEmpTaskStatus" runat="server" Text="Status"></asp:Label>:
                                            <asp:DropDownList ID="ddlEmpTaskStatus" runat="server" CssClass="droplist" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlEmpTaskStatus_SelectedIndexChanged">
                                                <asp:ListItem Text="---All---" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Not Started" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Opend/Scheduled" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="In Progress" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Closed" Value="4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAllEmpId" runat="server" Text="EmpID"></asp:Label>:
                                            <asp:TextBox ID="txtAllEmpID" runat="server"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FTBAllEmpID" runat="server" TargetControlID="txtAllEmpID" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                            <cc1:TextBoxWatermarkExtender ID="txtAllWMEEmpID" runat="server" TargetControlID="txtAllEmpID"
                                                WatermarkText="[Filter EmpID]">
                                            </cc1:TextBoxWatermarkExtender>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAllEmpName" runat="server" Text="EmpName"></asp:Label>:
                                            <asp:TextBox ID="txtAllEmpName" runat="server"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="txtWMEAllEmpName" runat="server" TargetControlID="txtAllEmpName"
                                                WatermarkText="[Filter EmpName]">
                                            </cc1:TextBoxWatermarkExtender>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAllSubmit" runat="server" Text="Submit" CssClass="savebutton" OnClick="btnAllSubmit_Click"/>
                                        </td>
                                    </tr>
                                    <tr id="trSchedule" runat="server">
                                        <td>
                                            <asp:Label ID="lblSchWS" runat="server" Text="Worksite">
                                            </asp:Label>:
                                            <asp:DropDownList ID="ddlSecWS" runat="server" CssClass="droplist">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSchDept" runat="server" Text="Department"></asp:Label>:
                                            <asp:DropDownList ID="ddlSecDept" runat="server" CssClass="droplist">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSchEmpID" runat="server" Text="EmpID"></asp:Label>:
                                            <asp:TextBox ID="txtSchEmpID" runat="server"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FTBSchEmpID" runat="server" TargetControlID="txtSchEmpID" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                            <cc1:TextBoxWatermarkExtender ID="txtSchWMEEmpID" runat="server" TargetControlID="txtSchEmpID"
                                                WatermarkText="[Filter EmpID]">
                                            </cc1:TextBoxWatermarkExtender>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSchEmpName" runat="server" Text="EmpName"></asp:Label>:
                                            <asp:TextBox ID="txtSchEmpName" runat="server"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="txtWMExtSchEmpName" runat="server" TargetControlID="txtSchEmpName"
                                                WatermarkText="[Filter Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSechSubmit" runat="server" Text="Submit" CssClass="savebutton" OnClick="btnSechSubmit_Click"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="droplist">
                                            <asp:CheckBox ID="chkSche" runat="server" Text="Show Scheduled Tasks Details" AutoPostBack="true"
                                                Checked="false" TabIndex="9" OnCheckedChanged="chkSche_CheckedChanged" />
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
            <td class="pageheader">
                &nbsp;
            </td>
        </tr>
        <tr id="trgrdSch" runat="server" visible="false">
            <td>
                <asp:GridView ID="grdSch" EmptyDataText="No Employee(s) Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                    runat="server" AutoGenerateColumns="false" Width="100%" HeaderStyle-CssClass="tableHead"
                    ForeColor="#333333" GridLines="Both" OnRowCommand="gvTodoList_RowCommand" CssClass="gridview">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="EmpName" />
                        <asp:BoundField DataField="AssignedBy" HeaderText="AssignedBy" />
                        <asp:BoundField DataField="Subject" HeaderText="Subject" />
                        <asp:BoundField DataField="TimeRequired" HeaderText="TimeRequired" />
                        <asp:BoundField DataField="FromDate" HeaderText="Starts From" />
                        <asp:BoundField DataField="StartTime" HeaderText="Start Time" />
                        <asp:BoundField DataField="ToDate" HeaderText="End Date" />
                        <asp:BoundField DataField="EndTime" HeaderText="End Time" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr id="trgrdSchPaging" runat="server">
            <td style="height: 17px">
                <uc1:Paging ID="EmpSchPaging" runat="server" />
            </td>
        </tr>
        <tr id="trgrdToDolist" runat="server">
            <td>
                <asp:GridView ID="gvTodoList" EmptyDataText="No Employee(s) Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                    runat="server" AutoGenerateColumns="false" Width="100%" HeaderStyle-CssClass="tableHead"
                    ForeColor="#333333" GridLines="Both" OnRowCommand="gvTodoList_RowCommand" CssClass="gridview">
                    <Columns>
                        <asp:BoundField DataField="ToDoID" HeaderText="TaskID" Visible="true" />
                        <asp:BoundField DataField="Subject" HeaderText="Subject" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" />
                        <asp:BoundField DataField="DueDate" HeaderText="Due Date" />
                        <asp:BoundField DataField="Complete" HeaderText="Progress(%)" />
                        <asp:BoundField DataField="Priority" HeaderText="Priority" />
                        <asp:BoundField DataField="AssignedTo" HeaderText="Assigned To" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkView" CommandName="view" CommandArgument='<%#Eval("ToDoID")%>'
                                    runat="server">View</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDel" CommandName="Del" CommandArgument='<%#Eval("ToDoID")%>'
                                    runat="server">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr id="trgrdToDolistPaging" runat="server">
            <td style="height: 17px">
                <uc1:Paging ID="EmpTaskAssignPaging" runat="server" />
            </td>
        </tr>
    </table>
    <table id="tblTaskHistory" runat="server" width="100%" visible="false">
        <tr>
            <td colspan="2" class="pageheader" align="left">
                &nbsp;History
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvTaskHistory" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                    runat="server" AutoGenerateColumns="false" Width="80%" HeaderStyle-CssClass="tableHead"
                    ForeColor="#333333" CssClass="gridview" GridLines="Both">
                    <Columns>
                        <asp:BoundField DataField="TaskNo" Visible="false" />
                        <asp:BoundField DataField="ToDoID" Visible="false" />
                        <asp:BoundField DataField="Subject" HeaderText="Subject" />
                        <asp:BoundField DataField="AssignedBy" HeaderText="Assigned By" Visible="false" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="Complete" HeaderText="Progress(%)" />
                        <asp:BoundField DataField="ReportedOn" HeaderText="Reported On" />
                        <asp:BoundField DataField="Report" HeaderStyle-Width="23%" HeaderText="Report" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
