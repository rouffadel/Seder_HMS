<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="ExceRptInOutTimes.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ExceRptInOutTimes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
     <script language="javascript" type="text/javascript">
         //chaitanya:for validation purpose
         function isNumber(evt) {
             var iKeyCode = (evt.which) ? evt.which : evt.keyCode
             if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                 return false;

             return true;
         }
    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td style="height: 26px; text-align: left;">
                <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                    SelectedIndex="0">
                    <Panes>
                        <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table>
                                    <tr>
                                        <td valign="top">
                                            <strong>EmpID&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEmpID" Width="50" runat="server" TabIndex="1" AccessKey="1" ToolTip="[Alt+1]" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FTBEmpID" runat="server" TargetControlID="txtEmpID" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                            &nbsp;&nbsp;&nbsp;
                                            </strong><strong>Emp Name&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEmpName" Width="50" TabIndex="2" AccessKey="2" ToolTip="[Alt+2]"
                                                runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp; </strong>&nbsp;&nbsp;<asp:Label ID="lblMonth"
                                                    runat="server" Text="Month Wise"></asp:Label>
                                            &nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" CssClass="droplist" TabIndex="3" AccessKey="3" ToolTip="[Alt+3]">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="droplist" TabIndex="4" AccessKey="4" ToolTip="[Alt+4]">
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnMonthReport" ToolTip="Generate Month Report" runat="server" CssClass="savebutton"
                                                OnClick="btnMonthReport_Click" Text="Month Report" TabIndex="5" AccessKey="i"  />
                                            &nbsp;&nbsp; &nbsp;
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
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="grdAllEmployees" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                HeaderStyle-CssClass="tableHead" EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview"
                                Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="EmpId" HeaderText="EmpID" Visible="false" />
                                    <asp:BoundField DataField="EMpName" HeaderText="EmpName" />
                                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                    <asp:BoundField DataField="Date" HeaderText="Date" />
                                    <asp:BoundField DataField="InTime" HeaderText="In Time" />
                                    <asp:BoundField DataField="OutTime" HeaderText="Out Time" />
                                    <asp:BoundField DataField="WHours" HeaderText="Working Hours" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
