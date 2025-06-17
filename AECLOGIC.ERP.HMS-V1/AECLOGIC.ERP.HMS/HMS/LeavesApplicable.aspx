<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="LeavesApplicable.aspx.cs" Inherits="AECLOGIC.ERP.HMS.LeavesApplicable" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function Validate() {


            //for Worksite
            if (!chkDropDownList('<%=ddlLeaveType.ClientID%>', 'Leave Type'))
                return false;
            //for Manager
            if (!chkDropDownList('<%=ddlLeaveProfiler.ClientID %>', 'Profiler'))
                return false;

            return true;

        }
        //For Dropdown list
        function chkDropDownList(object, msg) {

            var elm = getObj(object);

            if (elm.selectedIndex == 0) {
                alert("Select " + msg + "!!!");
                elm.focus();
                return false;
            } return true;
        }
        //geting the object
        function getObj(the_id) {
            if (typeof (the_id) == "object") {
                return the_id;
            }
            if (typeof document.getElementById != 'undefined') {
                return document.getElementById(the_id);
            }
            else if (typeof document.all != 'undefined') {
                return document.all[the_id];
            }
            else if (typeof document.layers != 'undefined') {
                return document.layers[the_id];
            }
            else {
                return null;
            }
        }
        //Required Validation
        function Reval(object, msg, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (val == '' || val.length == 0 || val == waterMark) {
                alert(msg + " should not be empty!!! ");
                //elm.value = waterMark;
                elm.focus();
                return false;
            }
            return true;
        }

    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
      
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td colspan="2" class="pageheader">
                            Leaves Applicable
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 113px">
                            Leave Type<span style="color: red">*</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlLeaveType"  CssClass="droplist" runat="server" Width="200">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 113px">
                            Profiler Type<span style="color: #ff0000">*</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlLeaveProfiler"  CssClass="droplist" runat="server" Width="200">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 124px">
                            Applicable Days<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicableDays" runat="server" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 124px">
                            Status
                        </td>
                        <td>
                            <asp:CheckBox ID="chkStatus" runat="server" Checked="True" Text="Active" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnSubmit" runat="server" Text="Apply" CssClass="savebutton" Width="100px"
                                OnClientClick="javascript:return Validate()" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvLeaveApplicable" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                OnRowCommand="gvLeaveApplicable_RowCommand" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%#Eval("AllocatedID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LeaveType">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeaveType" runat="server" Text='<%#Eval("LeaveType")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ProfilerType">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProfilerType" runat="server" Text='<%#Eval("ProfilerType")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Applicable Days">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApplicableDays" runat="server" Text='<%#Eval("AllocatedDays")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("AllocatedID")%>'
                                                CommandName="Edt"></asp:LinkButton></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDel" runat="server" Text="Delete" CommandArgument='<%#Eval("AllocatedID")%>'
                                                CommandName="Del"></asp:LinkButton></ItemTemplate>
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
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
