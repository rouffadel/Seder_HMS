<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="EmpToDoList.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpToDoList" %>

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

                //   if(!chkDropDownList('<%=ddlEmp.ClientID %>','AssignedBy'))
                //    return false;
                if (!DateCompare('<%=txtStartDate.ClientID %>', '<%=txtDueDate.ClientID %>'))
                    return false;
            }
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
   
    <asp:Panel ID="pnlEmpToDoAdd" runat="server" CssClass="DivBorderOlive" Visible="true">
        <table id="tblTodo" runat="server" visible="false" width="100%">
            <tr>
                <td style="width: 124px">
                    <asp:Button ID="btnBack" CssClass="savebutton" runat="server" Visible="false" Text="Back"
                        OnClick="btnBack_Click" TabIndex="1" />
                </td>
            </tr>
            <tr>
                <td class="style2" style="width: 124px">
                    <b>Subject:</b>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtSubject" Width="400Px" runat="server" TabIndex="2"></asp:TextBox>
                    <asp:Label ID="lblSubject" runat="server" ForeColor="Blue" Visible="False" Width="400px"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2" style="width: 124px">
                    <b>Start Date:</b>
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" Width="80px" runat="server" Height="18px" TabIndex="3"></asp:TextBox><cc1:CalendarExtender
                        ID="CalendarExtender2" runat="server" TargetControlID="txtStartDate" PopupButtonID="txtStartDate"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <asp:Label ID="lblStartDate" runat="server" Font-Bold="True" ForeColor="Blue" Visible="False"></asp:Label>
                </td>
                <td colspan="2" align="left">
                </td>
                <td align="left" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <b>Due Date:</b>
                </td>
                <td>
                    <asp:TextBox ID="txtDueDate" Width="80Px" runat="server" TabIndex="4"></asp:TextBox><cc1:CalendarExtender
                        ID="CalendarExtender1" runat="server" TargetControlID="txtDueDate" PopupButtonID="txtDueDate"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <asp:Label ID="lblDueDate" runat="server" Font-Bold="True" ForeColor="Blue" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 124px">
                    <b>Priority</b>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPriority" CssClass="droplist" runat="server" TabIndex="5">
                        <asp:ListItem Text="Normal" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Low" Value="2"></asp:ListItem>
                        <asp:ListItem Text="High" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Urgent" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Very Urgent" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;<asp:Label ID="lblPriority" runat="server" Font-Bold="True" ForeColor="Blue"
                        Visible="False"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="style2" style="width: 124px">
                    <b>Assigned By:</b>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEmp" runat="server" CssClass="droplist" Height="16px" TabIndex="6">
                    </asp:DropDownList>
                    <cc1:ListSearchExtender ID="ListSearchExtender1" IsSorted="true" PromptText="Type Here To Search"
                        PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                        TargetControlID="ddlEmp" />
                    <asp:TextBox ID="txtAssigned" Width="250Px" Visible="false" runat="server"></asp:TextBox>&nbsp;<asp:Label
                        ID="lblAssignedBy" runat="server" Font-Bold="True" ForeColor="Blue" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 124px">
                    <b>Assignment:</b>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtTask" runat="server" Font-Bold="False" TextMode="MultiLine" BorderColor="#CC6600"
                        BorderStyle="Outset" Rows="8" Width="370Px" Height="80px" TabIndex="7"></asp:TextBox>
                    <asp:Label ID="lblTask" runat="server" ForeColor="Blue" Visible="False" Width="600px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td id="tdReport" class="style2" style="width: 124px">
                    <asp:Label ID="lblTaskReport" runat="server" Font-Bold="True" Text="Progress Report:"></asp:Label>
                    &nbsp;
                </td>
                <td id="tdReportt" colspan="2">
                    <asp:TextBox ID="txtReport" Width="270px" runat="server" TextMode="MultiLine" Rows="4"
                        BorderColor="#CC6600" BorderStyle="Inset" Height="60px" TabIndex="8"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtReport"
                        WatermarkCssClass="Watermark" WatermarkText="[Enter Work Report you have done so far!]">
                    </cc1:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr>
                <td class="style2" style="width: 124px; height: 27px;">
                    <asp:Label ID="lblComplete" runat="server" Font-Bold="True" Text="Progress(%):"></asp:Label>
                </td>
                <td colspan="3" style="height: 27px">
                    <asp:DropDownList ID="ddlComplete" CssClass="droplist" runat="server" TabIndex="9">
                        <asp:ListItem Text="0%" Value="1"></asp:ListItem>
                        <asp:ListItem Text="25%" Value="2"></asp:ListItem>
                        <asp:ListItem Text="50%" Value="3"></asp:ListItem>
                        <asp:ListItem Text="75%" Value="4"></asp:ListItem>
                        <asp:ListItem Text="100%" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <b>Status:</b>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" CssClass="droplist" runat="server" TabIndex="10">
                        <asp:ListItem Text="Not Started" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                        <asp:ListItem Text="In Progress" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Reminder:</b><asp:CheckBox ID="chkReminder" AutoPostBack="true" runat="server"
                        OnCheckedChanged="chkReminder_CheckedChanged" TabIndex="11" />
                </td>
                <td>
                    <b>Date: </b>
                    <asp:TextBox ID="txtReminder" Width="80Px" runat="server" TabIndex="12"></asp:TextBox><b>&nbsp;
                        Time:&nbsp; </b>
                    <asp:DropDownList ID="ddlReminder" CssClass="droplist" runat="server" TabIndex="13">
                        <asp:ListItem Text="10:00" Value="1"></asp:ListItem>
                        <asp:ListItem Text="10:30" Value="2"></asp:ListItem>
                        <asp:ListItem Text="11:00" Value="3"></asp:ListItem>
                        <asp:ListItem Text="11:30" Value="4"></asp:ListItem>
                        <asp:ListItem Text="12:00" Value="5"></asp:ListItem>
                        <asp:ListItem Text="12:30" Value="6"></asp:ListItem>
                        <asp:ListItem Text="13:00" Value="7"></asp:ListItem>
                        <asp:ListItem Text="13:30" Value="8"></asp:ListItem>
                        <asp:ListItem Text="14:00" Value="9"></asp:ListItem>
                        <asp:ListItem Text="14:30" Value="10"></asp:ListItem>
                        <asp:ListItem Text="15:00" Value="11"></asp:ListItem>
                        <asp:ListItem Text="15:30" Value="12"></asp:ListItem>
                        <asp:ListItem Text="16:00" Value="13"></asp:ListItem>
                        <asp:ListItem Text="16:30" Value="14"></asp:ListItem>
                        <asp:ListItem Text="17:00" Value="15"></asp:ListItem>
                        <asp:ListItem Text="17:30" Value="16"></asp:ListItem>
                        <asp:ListItem Text="18:00" Value="17"></asp:ListItem>
                        <asp:ListItem Text="18:30" Value="18"></asp:ListItem>
                        <asp:ListItem Text="19:00" Value="19"></asp:ListItem>
                        <asp:ListItem Text="19:30" Value="20"></asp:ListItem>
                        <asp:ListItem Text="20:00" Value="21"></asp:ListItem>
                    </asp:DropDownList>
   
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td style="width: 124px">
                    <asp:Button ID="btnSave" runat="server" CssClass="savebutton" Text="Submit" OnClientClick="javascript:return validate();"
                        OnClick="btnSave_Click" AccessKey="s" TabIndex="14" ToolTip="[Alt+s OR Alt+s+Enter]" />
                </td>
                <td style="padding-left: 01Px" colspan="4">
                    <asp:Button ID="btnCancel" runat="server" CssClass="savebutton" Text="Cancel" OnClick="btnCancel_Click"
                        AccessKey="b" TabIndex="15" ToolTip="[Alt+b OR Alt+b+Enter]" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table id="tblviewTodo" runat="server" width="100%" visible="false">
        <tr>
            <td class="pageheader" align="left" style="height: 7px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvTodoList" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                    runat="server" AutoGenerateColumns="false" Width="80%" HeaderStyle-CssClass="tableHead"
                    ForeColor="#333333" GridLines="Both" OnRowCommand="gvTodoList_RowCommand" CssClass="gridview">
                    <Columns>
                        <asp:BoundField DataField="ToDoID" Visible="true" HeaderText="TaskNo" />
                        <asp:BoundField DataField="Subject" HeaderText="Subject" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" />
                        <asp:BoundField DataField="DueDate" HeaderText="Due Date" />
                        <asp:BoundField DataField="Complete" HeaderText="Progress(%)" />
                        <asp:BoundField DataField="Priority" HeaderText="Priority" />
                        <asp:BoundField DataField="AssignedBy" HeaderText="Assigned By" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSchedule" CommandName="Schedule" CommandArgument='<%#Eval("ToDoID")%>'
                                    runat="server">Schedule</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
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
    </table>
    <table id="tblTaskHistory" runat="server" width="100%" visible="false">
        <tr>
            <td colspan="2" class="pageheader" align="left">
                History
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvTaskHistory" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                    runat="server" AutoGenerateColumns="false" Width="80%" HeaderStyle-CssClass="tableHead"
                    ForeColor="#333333" CssClass="gridview" GridLines="Both">
                    <Columns>
                        <asp:BoundField DataField="TaskNo" Visible="false" HeaderText="TaskNo" />
                        <asp:BoundField DataField="ToDoID" Visible="false" />
                        <asp:BoundField DataField="Subject" HeaderText="Subject" />
                        <asp:BoundField DataField="AssignedBy" HeaderText="Reported By" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="Complete" HeaderText="Progress(%)" />
                        <asp:BoundField DataField="ReportedOn" HeaderText="Reported On" />
                        <asp:BoundField DataField="Report" HeaderStyle-Width="23%" HeaderText="Report" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table id="tblSchedule" runat="server" width="100%">
        <tr>
            <td>
                <table id="tblDetails" runat="server" width="100%">
                    <tr>
                        <td class="pageheader" bgcolor="#A7A7A7">
                            Details:&nbsp;
                        </td>
                        <td style="width: 100%" bgcolor="#A7A7A7">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSub" runat="server" Text="Subject"></asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="lblSubDetails" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Task"></asp:Label>:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTskDetails" runat="server" TextMode="MultiLine" Height="75px" Enabled="false"
                                Width="375px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Priority"></asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="lblPriorityDetails" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAssign" runat="server" Text="AssignedBy"></asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="lblAssignedByDetails" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStrtDate" runat="server" Text="StartDate"></asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="lblStartDateDetails" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="DueDate"></asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="lblDueDateDetails" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="tblScheduleDetails" runat="server">
                    <tr>
                        <td class="pageheader" bgcolor="#A7A7A7">
                            Reply:&nbsp;
                        </td>
                        <td style="width: 100%" bgcolor="#A7A7A7">
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="width: 100">
                            <asp:Label ID="lblScheduleTime" runat="server" Text="Time Required"></asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="lblRplyYears" runat="server" Text="Years"></asp:Label>:
                            <asp:DropDownList ID="ddlYears" runat="server" TabIndex="1" CssClass="droplist">
   
                            </asp:DropDownList>
   
                            <asp:Label ID="lblRplyMonths" runat="server" Text="Months"></asp:Label>:
                            <asp:DropDownList ID="ddlMnths" runat="server" TabIndex="2" CssClass="droplist">
                            </asp:DropDownList>
   
                            <asp:Label ID="lblRplyDays" runat="server" Text="Days"></asp:Label>:
                            <asp:DropDownList ID="ddlDays" runat="server" TabIndex="3" CssClass="droplist">
                            </asp:DropDownList>
   
                            <asp:Label ID="lblHours" runat="server" Text="Hours"></asp:Label>:
                            <asp:DropDownList ID="ddlHours" runat="server" TabIndex="4" CssClass="droplist">
                            </asp:DropDownList>
   
                            <asp:Label ID="lblMin" runat="server" Text="Min"></asp:Label>:
                            <asp:DropDownList ID="ddlMin" runat="server" TabIndex="5" CssClass="droplist">
                            </asp:DropDownList>
   
                            <asp:Label ID="lblSec" runat="server" Text="Sec"></asp:Label>:
                            <asp:DropDownList ID="ddlSec" runat="server" TabIndex="6" CssClass="droplist">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRplyFrmDate" runat="server" Text="FromDate"></asp:Label>:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFrmDate" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalExtDOB" runat="server" TargetControlID="txtFrmDate"
                                Format="dd/MM/yyyy" Enabled="true">
                            </cc1:CalendarExtender>
                            <cc1:FilteredTextBoxExtender FilterType="Custom, Numbers" ID="FilteredTextBoxExtender3"
                                runat="server" TargetControlID="txtFrmDate" ValidChars="/" Enabled="True">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStartTime" runat="server" Text="StartTime"></asp:Label>:
                        </td>
                        <td>
                            HR:<asp:DropDownList ID="ddlStrtHr" runat="server" CssClass="droplist">
                            </asp:DropDownList>
                            Min:<asp:DropDownList ID="ddlStrtMin" runat="server" CssClass="droplist">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlStrtAMPM" runat="server" CssClass="droplist">
                                <asp:ListItem Text="AM" Value="1"></asp:ListItem>
                                <asp:ListItem Text="PM" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRplyToDate" runat="server" Text="DuetDate"></asp:Label>:
                        </td>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtToDate"
                                Format="dd/MM/yyyy" Enabled="true">
                            </cc1:CalendarExtender>
                            <cc1:FilteredTextBoxExtender FilterType="Custom, Numbers" ID="FilteredTextBoxExtender1"
                                runat="server" TargetControlID="txtToDate" ValidChars="/" Enabled="True">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEndTime" runat="server" Text="EndTime"></asp:Label>:
                        </td>
                        <td>
                            HR:<asp:DropDownList ID="ddlEndHr" runat="server" CssClass="droplist">
                            </asp:DropDownList>
                            Min:<asp:DropDownList ID="ddlEndMin" runat="server" CssClass="droplist">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlEndAMPM" runat="server" CssClass="droplist">
                                <asp:ListItem Text="AM" Value="1"></asp:ListItem>
                                <asp:ListItem Text="PM" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRply" runat="server" Text="Reply"></asp:Label>:
                        </td>
                        <td>
                            <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Height="75px" Width="375px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSubSchedule" runat="server" Text="Submit" CssClass="savebutton"
                                OnClick="btnSubSchedule_Click" />
                            <asp:Button ID="btnCancelSchedule" runat="server" Text="Cancel" CssClass="savebutton"
                                OnClick="btnCancelSchedule_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
