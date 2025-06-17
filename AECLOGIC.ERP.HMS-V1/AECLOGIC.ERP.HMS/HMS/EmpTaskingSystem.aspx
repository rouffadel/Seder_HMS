<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="EmpTaskingSystem.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpTaskingSystem" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript">
        function SelectAll(hChkBox, grid, tCtrl) { var oGrid = document.getElementById(grid); var IPs = oGrid.getElementsByTagName("input"); for (var iCount = 0; iCount < IPs.length; ++iCount) { if (IPs[iCount].type == 'checkbox' && IPs[iCount].id.indexOf(tCtrl, 0) >= 0) IPs[iCount].checked = hChkBox.checked; } }
    </script>
    
    <table width="100%">
        <tr>
            <td width="130Px">
                <asp:Button ID="btnback" CssClass="savebutton" runat="server" Text="Back" OnClick="btnback_Click"
                    AccessKey="b" TabIndex="1" ToolTip="[Alt+b OR Alt+b+Enter]" />
            </td>
            <td align="left">
                <asp:Label ID="lblsystem" runat="server" Text="Employee Tasking System" Font-Bold="True"
                    Font-Size="Small" ForeColor="#006600"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="tblUpdaters" visible="false" runat="server" width="100%">
                    <tr>
                        <td class="pageheader">
                            <u>Total Employees in Tasking System</u>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView AutoGenerateColumns="false" CssClass="gridview" EmptyDataText="No Records Found"
                                HeaderStyle-CssClass="tableHead" EmptyDataRowStyle-CssClass="EmptyRowData" ID="gvUpdaters"
                                runat="server" AllowSorting="true" OnSorting="gvUpdaters_Sorting">
                                <Columns>
                                    <asp:TemplateField SortExpression="EmpId" HeaderText="EmpID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpId" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" SortExpression="name" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" Text='<%#Bind("name")%>'> </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Category" HeaderText="Trades" HeaderStyle-HorizontalAlign="Left">Text='<%#String.Format("{0} {1} {2}", DataBinder.Eval(Container.DataItem, "FName"), DataBinder.Eval(Container.DataItem, "MName"), DataBinder.Eval(Container.DataItem, "LName")) %>'>
                                    </asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Worksite" SortExpression="Categary" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorksite" runat="server" Text='<%# GetWorkSite(DataBinder.Eval(Container.DataItem, "Categary").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department" SortExpression="DepartmentName" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" runat="server" Text='<%#Bind("DepartmentName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Design" SortExpression="Design" HeaderText="Designation"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" HeaderStyle-HorizontalAlign="Left">
                                    </asp:BoundField>
    
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="tblUpdated" visible="false" runat="server" width="100%">
                    <tr>
                        <td class="pageheader">
                            <u>Employees Updated Yesterday's Work </u>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvUpdated" CssClass="gridview" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                                AllowSorting="true" EmptyDataRowStyle-CssClass="EmptyRowData" EmptyDataText="No Records Found"
                                runat="server" OnRowCommand="gvUpdated_RowCommand" OnSorting="gvUpdated_Sorting">
                                <Columns>
                                    <asp:TemplateField HeaderText="EmpID" SortExpression="EmpID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpId" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" SortExpression="name" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" Text='<%#Bind("name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Worksite" SortExpression="Categary" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorksite" runat="server" Text='<%# GetWorkSite(DataBinder.Eval(Container.DataItem, "Categary").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department" SortExpression="DepartmentName" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" runat="server" Text='<%#Bind("DepartmentName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Design" HeaderText="Designation" SortExpression="Design"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" HeaderStyle-HorizontalAlign="Left">
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Working Task" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkTask" runat="server" Text='<%#Bind("Task") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkWorkTask" CommandName="sms" CommandArgument='<%#Eval("EmpId")%>'
                                                runat="server">SMS</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkWorkTaskMail" CommandName="mail" CommandArgument='<%#Eval("EmpId")%>'
                                                runat="server">MAIL</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="tblUpdatedSMS" visible="false" runat="server" width="100%">
                    <tr>
                        <td style="width: 97px">
                            <b>Message To:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b>Mobile No:</b>
                        </td>
                        <td>
                            <asp:RadioButton ID="rbCompany" runat="server" Text="Company" AutoPostBack="True"
                                OnCheckedChanged="rbCompany_CheckedChanged" TabIndex="2" />
                            <asp:TextBox ID="txtCompany" runat="server" Height="16px" TabIndex="3"></asp:TextBox>&nbsp;<asp:RadioButton
                                ID="rbPersonal" runat="server" Text="Personal" AutoPostBack="True" OnCheckedChanged="rbPersonal_CheckedChanged"
                                TabIndex="4" />
                            <asp:TextBox ID="txtPersonal" runat="server" TabIndex="5"></asp:TextBox>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b>Message:</b><!--template required to send message... temporarly disallow this-->
                        </td>
                        <td>
                            <asp:TextBox ID="txtMessage" runat="server" Rows="4" TextMode="MultiLine" Width="30%"
                                BorderColor="#CC6600" BorderStyle="Outset" Height="60px" TabIndex="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b></b>
                        </td>
                        <td>
                            &nbsp;<asp:Button ID="btnSend" runat="server" CssClass="savebutton" Text="Send" OnClick="btnSend_Click"
                                AccessKey="s" TabIndex="7" ToolTip="[Alt+s OR Alt+s+Enter]" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="tblMailto" visible="false" runat="server" width="100%">
                    <tr>
                        <td style="width: 97px">
                            <b>Mail To:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblmailto1" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b>MailID:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblMailidTo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b>Content:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtContent" runat="server" Rows="4" TextMode="MultiLine" Width="30%"
                                BorderColor="#CC6600" BorderStyle="Outset" Height="60px" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b></b>
                        </td>
                        <td>
                            &nbsp;<asp:Button ID="btnMailto" runat="server" CssClass="savebutton" Text="Send"
                                OnClick="btnMailto_Click" AccessKey="s" TabIndex="3" ToolTip="[Alt+s OR Alt+s+Enter]" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="tblNotUpdated" visible="false" runat="server" width="100%">
                    <tr>
                        <td class="pageheader">
                            <u>Employees Not Updated Yesterday's Work</u>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvNotUpdated" CssClass="gridview" HeaderStyle-CssClass="tableHead"
                                AutoGenerateColumns="false" runat="server" OnRowDataBound="gvNotUpdated_RowDataBound"
                                AllowSorting="true" OnSorting="gvNotUpdated_Sorting">
                                <Columns>
                                    <asp:TemplateField HeaderText="EmpID" SortExpression="EmpId">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpId" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" SortExpression="name" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" Text='<%#Bind("name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Worksite" SortExpression="Categary" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorksite" runat="server" Text='<%# GetWorkSite(DataBinder.Eval(Container.DataItem, "Categary").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department" SortExpression="DepartmentName" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" runat="server" Text='<%#Bind("DepartmentName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Design" SortExpression="Design" HeaderText="Designation"
                                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" HeaderStyle-HorizontalAlign="Left">
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderStyle-Width="12%">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHMail2All" Text="Mail To All" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkMail2All" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="12%">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHSMS2All" Text="SMS To All" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSMS2All" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="tblSMSnotUpdated" visible="false" runat="server" width="100%">
                    <tr>
                        <td style="width: 111px">
                            <b>Enter Message:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNotUPdateSMS" runat="server" BorderColor="#CC6600" BorderStyle="Inset"
                                Rows="4" TextMode="MultiLine" Width="30%" Height="60px" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 111px">
                        </td>
                        <td>
                            <asp:Button ID="btnSMSsend" ToolTip="[Alt+s OR Alt+s+Enter]" runat="server" Text="Send"
                                CssClass="savebutton" OnClick="btnSMSsend_Click" AccessKey="s" TabIndex="3" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="tblNewJoin" visible="false" runat="server" width="100%">
                    <tr>
                        <td class="pageheader">
                            <u>New Joined Employees </u>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvNewJoin" CssClass="gridview" EmptyDataText="No Records Found"
                                HeaderStyle-CssClass="tableHead" EmptyDataRowStyle-CssClass="EmptyRowData" AutoGenerateColumns="false"
                                runat="server" OnRowCommand="gvNewJoin_RowCommand" AllowSorting="True" OnSorting="gvNewJoin_Sorting">
                                <EmptyDataRowStyle CssClass="EmptyRowData"></EmptyDataRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="EmpID" SortExpression="EmpId">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpId" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" SortExpression="name" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" Text='<%#Bind("name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Worksite" SortExpression="Categary" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorksite" runat="server" Text='<%# GetWorkSite(DataBinder.Eval(Container.DataItem, "Categary").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department" SortExpression="DepartmentName" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" runat="server" Text='<%#Bind("DepartmentName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Design" SortExpression="Design" HeaderText="Designation"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Working Task" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkTask" runat="server" Text='<%#Bind("Task") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkWorkTask" CommandName="sms" CommandArgument='<%#Eval("EmpId")%>'
                                                runat="server">SMS</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkWorkTaskMail3" CommandName="mail" CommandArgument='<%#Eval("EmpId")%>'
                                                runat="server">MAIL</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="tableHead"></HeaderStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="tblsms2" visible="false" runat="server" width="100%">
                    <tr>
                        <td style="width: 97px">
                            <b>Message To:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblmsg2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b>Mobile No:</b>
                        </td>
                        <td>
                            <asp:RadioButton ID="rbCompany2" runat="server" Text="Company" AutoPostBack="True"
                                OnCheckedChanged="rbCompany2_CheckedChanged" TabIndex="2" />
                            <asp:TextBox ID="txtCompany2" runat="server" Height="16px" TabIndex="3"></asp:TextBox>&nbsp;<asp:RadioButton
                                ID="rbPersonal2" runat="server" Text="Personal" AutoPostBack="True" OnCheckedChanged="rbPersonal2_CheckedChanged"
                                TabIndex="4" />
                            <asp:TextBox ID="txtPersonal2" runat="server" TabIndex="5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b>Message:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMessage2" runat="server" Rows="4" TextMode="MultiLine" Width="30%"
                                BorderColor="#CC6600" BorderStyle="Outset" Height="60px" TabIndex="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b></b>
                        </td>
                        <td>
                            &nbsp;<asp:Button ID="btnSMS2" runat="server" CssClass="savebutton" Text="Send" OnClick="btnSMS2_Click"
                                AccessKey="s" TabIndex="7" ToolTip="[Alt+s OR Alt+s+Enter]" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="tblMail4" visible="false" runat="server" width="100%">
                    <tr>
                        <td style="width: 97px">
                            <b>Mail To:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblMailto4" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b>MailID:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblMailId4" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b>Content:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtContent4" runat="server" Rows="4" TextMode="MultiLine" Width="30%"
                                BorderColor="#CC6600" BorderStyle="Outset" Height="60px" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b></b>
                        </td>
                        <td>
                            &nbsp;<asp:Button ID="btnsave4" runat="server" CssClass="savebutton" Text="Send"
                                OnClick="btnMailto4_Click" AccessKey="s" TabIndex="3" ToolTip="[Alt+s OR Alt+s+Enter]" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="tblUpdateToday" visible="false" runat="server" width="100%">
                    <tr>
                        <td class="pageheader">
                            <u>Employees Posted Today's Work </u>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvUpdateToday" CssClass="gridview" AutoGenerateColumns="false"
                                EmptyDataText="No Records Found" HeaderStyle-CssClass="tableHead" EmptyDataRowStyle-CssClass="EmptyRowData"
                                runat="server" OnRowCommand="gvUpdateToday_RowCommand" AllowSorting="True" OnSorting="gvUpdateToday_Sorting">
                                <EmptyDataRowStyle CssClass="EmptyRowData"></EmptyDataRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="EmpID" SortExpression="EmpId">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpId" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" SortExpression="name" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" Text='<%#Bind("name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Worksite" SortExpression="Categary" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorksite" runat="server" Text='<%# GetWorkSite(DataBinder.Eval(Container.DataItem, "Categary").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department" SortExpression="DepartmentName" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" runat="server" Text='<%#Bind("DepartmentName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Design" SortExpression="Design" HeaderText="Designation"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Working Task" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkTask" runat="server" Text='<%#Bind("Task") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkWorkTask" CommandName="sms" CommandArgument='<%#Eval("EmpId")%>'
                                                runat="server">SMS</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkWorkTaskMail2" CommandName="mail" CommandArgument='<%#Eval("EmpId")%>'
                                                runat="server">MAIL</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="tableHead"></HeaderStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="tblSms3" runat="server" visible="false" width="100%">
                    <tr>
                        <td style="width: 97px">
                            <b>Message To:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblsms3" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b>Mobile No:</b>
                        </td>
                        <td>
                            <asp:RadioButton ID="rbCompany3" runat="server" Text="Company" AutoPostBack="True"
                                OnCheckedChanged="rbCompany3_CheckedChanged" TabIndex="2" />
                            <asp:TextBox ID="txtCompany3" runat="server" Height="16px" 
                                TabIndex="3"></asp:TextBox>&nbsp;<asp:RadioButton ID="rbPersonal3" runat="server"
                                    Text="Personal" AutoPostBack="True" OnCheckedChanged="rbPersonal3_CheckedChanged"
                                    TabIndex="4" />
                            <asp:TextBox ID="txtPersonal3" runat="server" TabIndex="5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b>Message:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMessage3" runat="server" Rows="4" TextMode="MultiLine" Width="30%"
                                BorderColor="#CC6600" BorderStyle="Outset" Height="60px" TabIndex="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b></b>
                        </td>
                        <td>
                            &nbsp;<asp:Button ID="btnSMS3" runat="server" CssClass="savebutton" Text="Send" OnClick="btnSMS3_Click"
                                AccessKey="s" TabIndex="7" ToolTip="[Alt+s OR Alt+s+Enter]" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="tblMailNew3" runat="server" visible="false" width="100%">
                    <tr>
                        <td style="width: 97px">
                            <b>Mail To:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblMailto2" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b>MailID:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblMailidto2" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b>Content:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMailto2" runat="server" Rows="4" TextMode="MultiLine" Width="30%"
                                BorderColor="#CC6600" BorderStyle="Outset" Height="60px" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <b></b>
                        </td>
                        <td>
                            &nbsp;<asp:Button ID="btnMailto2" runat="server" CssClass="savebutton" Text="Send"
                                OnClick="btnMailto2_Click" AccessKey="s" TabIndex="3" ToolTip="[Alt+s OR Alt+s+Enter]" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
