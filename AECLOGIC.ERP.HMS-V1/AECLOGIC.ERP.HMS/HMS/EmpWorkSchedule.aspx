<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EmpWorkSchedule.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.EmpWorkSchedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
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
                    <asp:Label ID="lblTask" runat="server" Text="Task"></asp:Label>:
                </td>
                <%--<td>
                <asp:Label ID="lblTaskDetails" runat="server"></asp:Label>
            </td>--%>
                <td>
                    <asp:TextBox ID="txtTskDetails" runat="server" TextMode="MultiLine" Enabled="false"
                        Height="75px" Width="375px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPriority" runat="server" Text="Priority"></asp:Label>:
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
                    <asp:Label ID="lblAssignedBy" runat="server"></asp:Label>
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
                    <asp:Label ID="lblDueDate" runat="server" Text="DuetDate"></asp:Label>:
                </td>
                <td>
                    <asp:Label ID="lblDueDateDetails" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td class="pageheader" bgcolor="#A7A7A7">
                    Reply:&nbsp;
                </td>
                <td style="width: 100%" bgcolor="#A7A7A7">
                </td>
            </tr>
        </table>
        </br>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblScheduleTime" runat="server" Text="Time Required"></asp:Label>:
                </td>
                <td>
                    <asp:Label ID="lblRplyYears" runat="server" Text="Years"></asp:Label>:
                    <asp:DropDownList ID="ddlYears" runat="server" TabIndex="1">
                        <%-- <asp:ListItem Text="00" Value="1"></asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblRplyMonths" runat="server" Text="Months"></asp:Label>:
                    <asp:DropDownList ID="ddlMnths" runat="server" TabIndex="2">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblRplyDays" runat="server" Text="Days"></asp:Label>:
                    <asp:DropDownList ID="ddlDays" runat="server" TabIndex="3">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblHours" runat="server" Text="Hours"></asp:Label>:
                    <asp:DropDownList ID="ddlHours" runat="server" TabIndex="4">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblMin" runat="server" Text="Min"></asp:Label>:
                    <asp:DropDownList ID="ddlMin" runat="server" TabIndex="5">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSec" runat="server" Text="Sec"></asp:Label>:
                    <asp:DropDownList ID="ddlSec" runat="server" TabIndex="6">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        </br>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblRplyFrmDate" runat="server" Text="FromDate"></asp:Label>:
                </td>
                <td>
                    <asp:TextBox ID="txtFrmDate" runat="server"></asp:TextBox>
                   <cc1:CalendarExtender ID="CalExtFrmDate" runat="server" TargetControlID="txtFrmDate"></cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblStartTime" runat="server" Text="StartTime"></asp:Label>:
                </td>
                <td>
                    HR:<asp:DropDownList ID="ddlStrtHr" runat="server">
                    </asp:DropDownList>
                    Min:<asp:DropDownList ID="ddlStrtMin" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlStrtAMPM" runat="server">
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
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEndTime" runat="server" Text="EndTime"></asp:Label>:
                </td>
                <td>
                    HR:<asp:DropDownList ID="ddlEndHr" runat="server">
                    </asp:DropDownList>
                    Min:<asp:DropDownList ID="ddlEndMin" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlEndAMPM" runat="server">
                        <asp:ListItem Text="AM" Value="1"></asp:ListItem>
                        <asp:ListItem Text="PM" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
