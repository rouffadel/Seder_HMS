<%@ Page Title="" Language="C#"    AutoEventWireup="True"
    CodeBehind="ExceRptLeaves.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ExceRptLeaves" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
       <script language="javascript" type="text/javascript">

   function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
          //  alert(HdnKey);
            document.getElementById('<%=ddlWorksite_hid.ClientID %>').value = HdnKey;
   }
           function GETDEPT_ID(source, eventArgs) {
               var HdnKey = eventArgs.get_value();
             //  alert(HdnKey);
               document.getElementById('<%=ddlDepartment_hid.ClientID %>').value = HdnKey;
           }
           //chaitanya:for validation purpose
           function isNumber(evt) {
               var iKeyCode = (evt.which) ? evt.which : evt.keyCode
               if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                   return false;

               return true;
           }
    </script>
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
                                        <td>
                                            <strong>Worksite&nbsp;&nbsp;&nbsp;
                                              
                                                   <asp:HiddenField ID="ddlWorksite_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                           </cc1:TextBoxWatermarkExtender>
                                            </strong>&nbsp;&nbsp; <strong>Department&nbsp;&nbsp;&nbsp;
                                           
                                                <asp:HiddenField ID="ddlDepartment_hid" runat="server" />
                                                 <asp:TextBox ID="txtdepartment" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Department" ServicePath="" TargetControlID="txtdepartment"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETDEPT_ID"  >
                                            </cc1:AutoCompleteExtender>        
                                                 <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtdepartment"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]">
                                           </cc1:TextBoxWatermarkExtender>

                                            </strong>&nbsp;&nbsp; <strong>EmpID&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEmpID" Width="50" TabIndex="3" AccessKey="2" ToolTip="[Alt+2]"
                                                runat="server" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FTBEmpID" runat="server" TargetControlID="txtEmpID" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                                &nbsp;&nbsp;&nbsp; </strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="lblDayWise" runat="server" Text="Day Wise"></asp:Label>
                                            &nbsp;&nbsp;<asp:TextBox ID="txtDay" runat="server" TabIndex="4" AccessKey="t" ToolTip="[Alt+t OR Alt+t+Enter]"></asp:TextBox>
                                            &nbsp;&nbsp;
                                            <asp:Image ID="imgDay" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" />
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDay"
                                                PopupPosition="BottomLeft" TargetControlID="txtDay">
                                            </cc1:CalendarExtender>
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnDaySearch" ToolTip="Generate Day Report" runat="server" CssClass="savebutton"
                                                OnClick="btnDaySearch_Click" Text="Day Report" TabIndex="5" AccessKey="i"/>
                                            &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;<asp:Label ID="lblMonth" runat="server" Text="Month Wise"></asp:Label>
                                            &nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" CssClass="droplist" TabIndex="6" AccessKey="3" ToolTip="[Alt+3]">
                                                <asp:ListItem Value="0">--All--</asp:ListItem>
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
                                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="droplist" TabIndex="7" AccessKey="4" ToolTip="[Alt+4]">
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnMonthReport" ToolTip="Generate Month Report" runat="server" CssClass="savebutton"
                                                OnClick="btnMonthReport_Click" Text="Month Report"  TabIndex="8"/>
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
                <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="AllEmployeesView" runat="server">
                        <table>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="grdAllEmployees" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                        HeaderStyle-CssClass="tableHead" EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview"
                                        OnRowCommand="grdAllEmployees_RowCommand" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="EmpId" HeaderText="EmpID" Visible="false" />
                                            <asp:BoundField DataField="EMpName" HeaderText="EmpName" />
                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                            <asp:BoundField DataField="NoofLeaves" HeaderText="Away from duty" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkView" runat="server" Text="Details" CommandName="detail" CommandArgument='<%#Eval("EmpId")%>'> </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                    </asp:View>
                    <asp:View ID="IndEmployeeByMonth" runat="server">
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Button ID="btnClsoe" runat="server" Text="Close" CssClass="pageheader" OnClick="btnClsoe_Click"
                                        Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GrdByMonth" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                        HeaderStyle-CssClass="tableHead" EmptyDataRowStyle-CssClass="EmptyRowData" CssClass="gridview">
                                        <Columns>
                                            <asp:BoundField DataField="EmpId" HeaderText="EmpID" />
                                            <asp:BoundField DataField="EMpName" HeaderText="EmpName" />
                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                            <asp:BoundField DataField="Date" HeaderText="Date" />
                                            <asp:BoundField DataField="ShortName" HeaderText="Type of Leave" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>
</asp:Content>
