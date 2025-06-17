<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthlyPaymentsApprovedV4_NEW.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.MonthlyPaymentsApprovedV4_NEW" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="server">
    <script language="javascript" type="text/javascript">
        function controlEnter(event) {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                document.getElementById('<%= btnSearch.ClientID %>').click();
                return false;
            }
            else {
                return true;
            }
        }
        function GetEmpID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=txtEmpNameHidden.ClientID %>').value = HdnKey;
          }

          function DisplayMonthYear() {
              var Result = AjaxDAL.GetStartDate();

              var frommonth = ddlMonth.options[ddlMonth.selectedIndex].text;
              var fromyear = ddlYear.options[ddlYear.selectedIndex].text;
              var fromdate = '21' + '/' + frommonth + '/' + fromyear;
              var todate;
              if (ddlMonth.options[ddlMonth.selectedIndex].value == 12) {
              }
              else {
              }
              document.getElementById("txtToDate").value = fromdate;
          }
    </script>
    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td>Name:
                                                                       <asp:HiddenField ID="txtEmpNameHidden" runat="server" />
                                    <asp:TextBox ID="txtEmpName" Height="20px" runat="server" TabIndex="6" AccessKey="5"
                                        ToolTip="[Alt+5]">                                                              
                                    </asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters="" Enabled="True"
                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList_EmpName" ServicePath="" TargetControlID="txtEmpName"
                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetEmpID">
                                    </cc1:AutoCompleteExtender>
                                    <cc1:TextBoxWatermarkExtender ID="txtwmeEmpName" runat="server" WatermarkText="[Filter Name]"
                                        TargetControlID="txtEmpName"></cc1:TextBoxWatermarkExtender>
                                    &nbsp;&nbsp;&nbsp;  Year:
                                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="droplist" TabIndex="4" ToolTip="[Alt+3]"
                                                                    AccessKey="3" Width="90" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                    &nbsp;&nbsp;Month:&nbsp;
                                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="droplist" TabIndex="3" AccessKey="2"
                                                                    ToolTip="[Alt+2]" Width="90" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                    &nbsp;&nbsp;
                                                                       Worksite
                                                                       <span style="color: #ff0000">*</span>
                                    <asp:DropDownList ID="ddlworksite" runat="server" CssClass="droplist"></asp:DropDownList>
                                    &nbsp;&nbsp;
                                                                       <%--  Format
                                                                       <span style="color: #ff0000">*</span>--%>
                                    <asp:DropDownList ID="ddlFormat" runat="server" CssClass="droplist" Visible="false">
                                        <asp:ListItem Text="Select" Value="0" />
                                        <asp:ListItem Text="Format1" Value="1" />
                                        <asp:ListItem Text="Format2" Value="2" />
                                        <asp:ListItem Text="Format3" Value="3" />
                                    </asp:DropDownList>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                        TabIndex="7" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" CssClass="btn btn-primary"
                                        Width="80px" />
                                    <asp:Button ID="btnExcelExport" Visible="false" runat="server" Text="Export Excel" OnClientClick="return confirm('Are You Sure to Export?')" OnClick="btnExcelExport_Click"
                                        TabIndex="7" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" CssClass="btn btn-primary"
                                        Width="80px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="overflow-x: scroll; width: 63%;">
                                        <asp:GridView ID="gvPaySlip" runat="server" AutoGenerateColumns="true" CssClass="gridview"
                                            Width="80%" CellPadding="4" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                            HeaderStyle-CssClass="tableHead"
                                            AllowSorting="True" AlternatingRowStyle-BackColor="GhostWhite" OnPreRender="gvPaySlip_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Payslip" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkView" target="_blank" href='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "Eid").ToString()) %>'
                                                            runat="server" class="btn btn-success">View</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle CssClass="tableHead" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <EditRowStyle BackColor="#999999" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 17px">
                                    <uc1:Paging ID="EmpListPaging" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelExport" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="SalariesUpdPanel">
            <ProgressTemplate>
                <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                    ID="imgs" />
                please wait...
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
