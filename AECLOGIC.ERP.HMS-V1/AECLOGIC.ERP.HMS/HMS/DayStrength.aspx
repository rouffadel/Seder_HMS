<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Templates/CommonMaster.master" CodeBehind="DayStrength.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMS.DayStrength" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <style type="text/css">
        .HeaderTextAlign {
            text-align: center;
        }
        .item-a {
            grid-column: 1;
            grid-row: 1 / 3;
        }
        .item-e {
            grid-column: 5;
            grid-row: 1 / 3;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function Validate(dtNow, dtText) {
            var count = 0;
            if (document.getElementById('<%=txtDay.ClientID%>').value == "") {
                alert("Please Select Date");
                return false;
            }
            else {
                var Today = new Date(dtNow);
                var dt = new String(document.getElementById('<%=txtDay.ClientID%>').value);
                dt = dt.split('/')[1] + '/' + dt.split('/')[0] + '/' + dt.split('/')[2];
                var Rep = new Date(dt);
                if (Rep > Today) {
                    alert("Date must be less than or equal to Current Date");
                    return false;
                }
            }
        }
        function validateMonth() {
            if (document.getElementById('<%=ddlMonth.ClientID%>').value == "0") {
                alert("Please Select Month");
                return false;
            }
        }
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlWorksite_hid.ClientID %>').value = HdnKey;
            <%-- alert(document.getElementById('<%=ddlWs_hid.ClientID %>').value);--%>
        }
        function GetdeptID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlDepartment_hid.ClientID %>').value = HdnKey;
            <%-- alert(document.getElementById('<%=ddlWs_hid.ClientID 
%>').value);--%>
        }
<%--        function GetempnatureID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlEmpNature_hid.ClientID %>').value = HdnKey;
            <%-- alert(document.getElementById('<%=ddlWs_hid.ClientID %>').value);
        }--%>
        //chaitanya:for validation purpose
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
        function GetEmpID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=txtEmpNameHidden.ClientID %>').value = HdnKey;
        }
    </script>
    <table style="width: 100%;">
        <tr>
            <td>
                <AEC:Topmenu ID="topmenu" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <div runat="server" width="100%">
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="2">
                                <cc1:Accordion Width="100%" ID="Accordion1" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                    <Panes>
                                        <cc1:AccordionPane ID="AccordionPane2" Width="100%" runat="server" HeaderCssClass="accordionHeader"
                                            ContentCssClass="accordionContent">
                                            <Header>
                                                Search Criteria</Header>
                                            <Content>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <strong>Current Worksite&nbsp;
					                                                <asp:HiddenField ID="ddlWorksite_hid" runat="server" />
                                                                <asp:TextBox ID="txtSearchWorksite" runat="server" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="140px"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                    OnClientItemSelected="GetID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchWorksite"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                                <%-- </strong>&nbsp;&nbsp;<strong> Department&nbsp;--%>
                                                                <asp:HiddenField ID="ddlDepartment_hid" runat="server" />
                                                                <asp:TextBox ID="textdept" Visible="false" AutoPostBack="false" Height="22px" Width="120px" runat="server" runat="server"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionList_dept" ServicePath="" TargetControlID="textdept"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetdeptID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="textdept"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                                <%--  <strong>EmpID&nbsp;--%>
                                                                <asp:TextBox ID="txtEmpID" Height="22px" Width="90px" runat="server" Visible="false" AccessKey="1" ToolTip="[Alt+1]" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" DelimiterCharacters="" Enabled="true"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetEmpidList" ServicePath="" TargetControlID="txtEmpID" UseContextKey="true"
                                                                    CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ShowOnlyCurrentWordInCompletionListItem="true"
                                                                    FirstRowSelected="True">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtEmpID"
                                                                    WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Empid]"></cc1:TextBoxWatermarkExtender>
                                                                &nbsp;Name&nbsp;
                                                        <asp:HiddenField ID="txtEmpNameHidden" runat="server" />
                                                                <asp:TextBox ID="txtEmpName" runat="server" Height="22px" Width="300px" AccessKey="2" ToolTip="[Alt+2]"></asp:TextBox>&nbsp;
						                                         <cc1:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters="" Enabled="True"
                                                                     MinimumPrefixLength="1" ServiceMethod="GetCompletionList_EmpName" ServicePath="" TargetControlID="txtEmpName"
                                                                     UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                     CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetEmpID">
                                                                 </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtEmpName"
                                                                    WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Name]"></cc1:TextBoxWatermarkExtender>
                                                            </strong>
                                                            <asp:CheckBox ID="chkMonth" runat="server" Visible="false" />
                                                            <asp:Label Visible="false" ID="lblMonth" runat="server" Text="Month Wise"></asp:Label>
                                                            <asp:DropDownList ID="ddlMonth" runat="server" Visible="false" CssClass="droplist"
                                                                OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                                <asp:ListItem Value="01">January</asp:ListItem>
                                                                <asp:ListItem Value="02">February</asp:ListItem>
                                                                <asp:ListItem Value="03">March</asp:ListItem>
                                                                <asp:ListItem Value="04">April</asp:ListItem>
                                                                <asp:ListItem Value="05">May</asp:ListItem>
                                                                <asp:ListItem Value="06">June</asp:ListItem>
                                                                <asp:ListItem Value="07">July</asp:ListItem>
                                                                <asp:ListItem Value="08">August</asp:ListItem>
                                                                <asp:ListItem Value="09">September</asp:ListItem>
                                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                                <asp:ListItem Value="12">December</asp:ListItem>
                                                            </asp:DropDownList>
                                                            &nbsp;
			                                            <asp:DropDownList Visible="false" ID="ddlYear" runat="server" CssClass="droplist"
                                                            OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                            &nbsp;
                                                             <asp:CheckBox ID="chkDay" runat="server" Visible="false" />
                                                            <asp:Label ID="lblDayWise" runat="server" Text="Day Wise"></asp:Label>
                                                            &nbsp;<asp:TextBox ID="txtDay" runat="server" Height="22px" Width="100px" AccessKey="t" ToolTip="[Alt+t OR Alt+t+Enter]"></asp:TextBox>
                                                            &nbsp;
			                                                <asp:Image ID="imgDay" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" />
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd MMM yyyy" PopupButtonID="imgDay"
                                                                PopupPosition="BottomLeft" TargetControlID="txtDay"></cc1:CalendarExtender>
                                                            &nbsp;
			                                                <asp:Button ID="btnDaySearch" Visible="false" ToolTip="Generate Day Report" runat="server" CssClass="btn btn-success"
                                                                OnClick="btnDaySearch_Click" Text="Day Report" />
                                                            &nbsp;
			                                                <asp:Button ID="btnDayExporttoExcel" Visible="false" ToolTip="Export to Excel Day Report" runat="server"
                                                                CssClass="btn btn-primary" Text="Export to Excel" />
                                                            &nbsp;<asp:Button ID="btnPrint" ToolTip="Print Day Report" runat="server" CssClass="btn btn-primary"
                                                                Text="Print" Visible="false" />
                                                            <asp:Button ID="btnSearch" Visible="false" CssClass="btn btn-success" runat="server" Text="Salary Month" OnClick="btnSearch_Click" />
                                                            <asp:Button ID="btnMonthfirst" Visible="false" runat="server" CssClass="btn btn-success" Text="Calendar Month" />
                                                            <asp:Button ID="btnDayStrength" runat="server" CssClass="btn btn-success" Text="Strength" OnClick="btnDayStrength_Click" />
                                                            <asp:Button ID="btnMonthReport" ToolTip="Generate Month Report" runat="server" CssClass="btn btn-success"
                                                                Text="Month Report" Visible="false" />
                                                            &nbsp;
			                                            <asp:Button ID="btnMonth" Visible="false" ToolTip="Export to Excel Month Report" runat="server" CssClass="btn btn-primary"
                                                            Text="Export to Excel" />
                                                            &nbsp;
			                                            <asp:Button ID="btnMPrint" Visible="false" ToolTip="Print to Month Report" runat="server" CssClass="btn btn-primary"
                                                            Text="Print" OnClick="btnMPrint_Click" />
                                                            &nbsp;
			                                            <%--   OnClientClick="javascript:return validateMonth();"--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDates" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <table id="tblAttStatus" runat="server" visible="false" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="grdAttStatusCount" Visible="false" runat="server" AutoGenerateColumns="true" CssClass="gridview">
                                                                            <Columns>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <asp:GridView ID="gdWSRPT" Visible="false" runat="server" AutoGenerateColumns="False" Width="50%"
                                                                            ShowFooter="True" EmptyDataText="No Records Found" HeaderStyle-CssClass="tableHead"
                                                                            EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BorderColor="GhostWhite">
                                                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                            <Columns>
                                                                                <asp:TemplateField Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPrjID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Site_ID")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Worksites">
                                                                                    <HeaderStyle Width="120" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCategary" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Site_Name")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Nos" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center"
                                                                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="HeaderTextAlign">
                                                                                    <HeaderStyle Width="80" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NoofEMP")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <b>
                                                                                            <asp:Label ID="Label6_1" runat="server" Text='<%# TotNoEMP.ToString()%>'></asp:Label></b>
                                                                                    </FooterTemplate>
                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Presents" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="HeaderTextAlign" FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                                    <HeaderStyle Width="80" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NoofPresent")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <b>
                                                                                            <asp:Label ID="Label7" runat="server" Text='<%# TotNoofPresent.ToString()%>'></asp:Label></b>
                                                                                    </FooterTemplate>
                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Absents" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="HeaderTextAlign" FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                                    <HeaderStyle Width="80" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NoofAbsent")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <b>
                                                                                            <asp:Label ID="Label8" runat="server" Text='<%# TotNoofAbsent.ToString()%>'></asp:Label></b>
                                                                                    </FooterTemplate>
                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Other" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="HeaderTextAlign" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                                                    <HeaderStyle Width="80" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Noofother")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <b>
                                                                                            <asp:Label ID="Label9" runat="server" Text='<%# TotNoofother.ToString()%>'></asp:Label></b>
                                                                                    </FooterTemplate>
                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Marked" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="HeaderTextAlign" FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                                    <HeaderStyle Width="80" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Marked_rt")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <b>
                                                                                            <asp:Label ID="Label10" runat="server" Text='<%# TotMarked_rt.ToString()%>'></asp:Label></b>
                                                                                    </FooterTemplate>
                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Not Marked" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="HeaderTextAlign" FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                                    <HeaderStyle Width="100" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"UnMarked_rt")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <b>
                                                                                            <asp:Label ID="Label11" runat="server" Text='<%# TotUnMarked_rt.ToString()%>'></asp:Label></b>
                                                                                    </FooterTemplate>
                                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                            <td style="text-align: left" colspan="2">
                                <asp:Table ID="tblAtt" runat="server" CssClass="item-a" BorderWidth="2" GridLines="Both">
                                </asp:Table>
                                <asp:GridView ID="gvAttLog" runat="server" AutoGenerateColumns="false" CssClass="gridview">
                                    <Columns>
                                        <asp:BoundField DataField="INOutType" HeaderText="IN/OUT" />
                                        <asp:BoundField DataField="INOUTTym" HeaderText="TIME" />
                                    </Columns>
                                </asp:GridView>
                                <uc1:Paging ID="EmpListPaging" Visible="false" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hdn" runat="server" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
