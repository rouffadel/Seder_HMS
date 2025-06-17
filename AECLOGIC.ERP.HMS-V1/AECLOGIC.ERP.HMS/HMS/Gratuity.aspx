<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="Gratuity.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Gratuity" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function SelectAll(hChkBox, grid, tCtrl) {
            var oGrid = document.getElementById(grid);
            var IPs = oGrid.getElementsByTagName("input");
            for (var iCount = 0; iCount < IPs.length; ++iCount) {
                if (IPs[iCount].type == 'checkbox')
                    IPs[iCount].checked = hChkBox.checked;
            }
        }
        function GetdeptID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlDepartment_hid.ClientID %>').value = HdnKey;
            <%-- alert(document.getElementById('<%=ddlWs_hid.ClientID %>').value);--%>
        }
        function GetMdeptID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlMDepartment_hid.ClientID %>').value = HdnKey;
        }
        function GetFdeptID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlFDepartment_hid.ClientID %>').value = HdnKey;
        }
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlWorksite_hid.ClientID %>').value = HdnKey;
            <%-- alert(document.getElementById('<%=ddlWs_hid.ClientID %>').value);--%>
        }
        function GetIDM(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlMWorksite_hid.ClientID %>').value = HdnKey;
        }
        function GetIDF(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlFWorksite_hid.ClientID %>').value = HdnKey;
        }
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
        function validatesave() {
            if (document.getElementById('<%=ddlMMonth.ClientID%>').selectedIndex == 0) {
                alert("Please Select Month ");
                document.getElementById('<%=ddlMMonth.ClientID%>').focus();
                return false;
            }
        }
    </script>
    <%--<asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>--%>
            <div>
                <table id="tblCutOff" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%" runat="server" visible="false">
                    <tr>
                        <td colspan="2">
                            <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                <Panes>
                                    <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                        ContentCssClass="accordionContent">
                                        <Header>
                                Search Criteria</Header>
                                        <Content>
                                            <table>
                                                <tr>
                                                    <b>Worksite:</b>
                                                    <asp:HiddenField ID="ddlWorksite_hid" runat="server" />
                                                    <asp:TextBox ID="txtSearchWorksite" runat="server" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="150px"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                        OnClientItemSelected="GetID">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchWorksite"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                    <strong>Department&nbsp;
					                                         <asp:HiddenField ID="ddlDepartment_hid" runat="server" />
                                                        <asp:TextBox ID="textdept" OnTextChanged="Getdept" AutoPostBack="false" Height="22px" Width="170px" runat="server" runat="server"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList_dept" ServicePath="" TargetControlID="textdept"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetdeptID">
                                                        </cc1:AutoCompleteExtender>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="textdept"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="droplist" TabIndex="2" Visible="false">
                                                        </asp:DropDownList>
                                                        <b>Emp ID/Name </b>
                                                        <asp:TextBox ID="txtName" Height="20px" Width="150px" runat="server"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtName"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                        </cc1:AutoCompleteExtender>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtName"
                                                            WatermarkCssClass="Watermarktxtbox" WatermarkText="[Emp ID/Name]"></cc1:TextBoxWatermarkExtender>
                                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="droplist" Visible="false">
                                                            <asp:ListItem Value="0">--ALL--</asp:ListItem>
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
                                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="droplist" TabIndex="3" Visible="false">
                                                        </asp:DropDownList>
                                                        <b>A/C Posted/Non-Posted</b>
                                                        <asp:DropDownList ID="ddlAccPost" CssClass="droplist" runat="server"
                                                            TabIndex="35">
                                                            <asp:ListItem Value="1">A/C Non-Posted</asp:ListItem>
                                                            <asp:ListItem Value="2">A/C Posted</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                                                            TabIndex="4" Text="Search" CssClass="btn btn-primary" />
                                                        <asp:Button ID="btClear" runat="server" Text="Reset" OnClick=" btClear_Click"
                                                            TabIndex="5" Width="50px" CssClass="btn btn-danger" />
                                                </tr>
                                            </table>
                                        </Content>
                                    </cc1:AccordionPane>
                                </Panes>
                            </cc1:Accordion>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvGratuity" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                HeaderStyle-CssClass="tableHead" CssClass="gridview" GridLines="Both" OnRowDataBound="gvGratuity_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" Text="All" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkToTransfer" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp Id" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpId" runat="server" Text='<%#Eval("EmpId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp Name" HeaderStyle-Width="160px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DOJ" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDoj" runat="server" Text='<%#Eval("DOJ")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gross Salary" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBasic" runat="server" Text='<%#Eval("salary")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eligible Days" HeaderStyle-Width="80px" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoDay" runat="server" Text='<%#Eval("nDays")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Years" HeaderStyle-Width="80px" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYears" runat="server" Text='<%#Eval("Years")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gratuity Days(Per Year)" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGratuityDays" runat="server" Text='<%#Eval("gdays")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gratuity Amount(Per Year)" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGratuityAmt" runat="server" Text='<%#Eval("gamt")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Arrears Amount" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblArrearsAmt" runat="server" Text='<%#Eval("arrears")%>'></asp:Label>--%>
                                            <asp:TextBox ID="lblArrearsAmt" Style="width: 60px;" onkeypress="javascript:return isNumber(event)" Text='<%#Eval("arrears")%>' Width="1000px" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trans Id" HeaderStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("transid")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trans Date" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("transdate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cut Off Date" HeaderStyle-Width="100px" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("cutdate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Allowance" HeaderStyle-Width="60px" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAllowance" runat="server" Text='<%#Eval("Allowance")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%" id="tblMonthly" visible="false" runat="server">
                    <tr>
                        <td>
                            <cc1:Accordion ID="Accordion1" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                <Panes>
                                    <cc1:AccordionPane ID="AccordionPane2" runat="server" HeaderCssClass="accordionHeader"
                                        ContentCssClass="accordionContent">
                                        <Header>
                                Search Criteria</Header>
                                        <Content>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <b>Worksite:
                                                  <asp:HiddenField ID="ddlMWorksite_hid" runat="server" />
                                                        <asp:TextBox ID="txtMSearchWorksite" runat="server" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="150px"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtMSearchWorksite"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                            OnClientItemSelected="GetIDM">
                                                        </cc1:AutoCompleteExtender>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtMSearchWorksite"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                    </b>
                                                    <b>
                                                        <strong>Department&nbsp;
					                                         <asp:HiddenField ID="ddlMDepartment_hid" runat="server" />
                                                            <asp:TextBox ID="textMdept" OnTextChanged="Getdept" AutoPostBack="false" Height="22px" Width="170px" runat="server" runat="server"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList_dept" ServicePath="" TargetControlID="textMdept"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetMdeptID">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="textMdept"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                            <b>
                                                                <b>
                                                                    <asp:DropDownList ID="ddlMCompany" runat="server" CssClass="droplist" TabIndex="2" Visible="false">
                                                                    </asp:DropDownList></b>
                                                                <b><b>Emp ID/Name
                                                                </b>
                                                                    <asp:TextBox ID="txtMEmpId" Height="20px" Width="150px" runat="server"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters="" Enabled="True"
                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtMEmpId"
                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtMEmpId"
                                                                        WatermarkCssClass="Watermarktxtbox" WatermarkText="[Employee ID/Name]"></cc1:TextBoxWatermarkExtender>
                                                                    <b>Month:
                                            <asp:DropDownList ID="ddlMMonth" runat="server" CssClass="droplist">
                                                <asp:ListItem Value="0">--ALL--</asp:ListItem>
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
                                            </asp:DropDownList><br />
                                                                        &nbsp; Year&nbsp;:&nbsp;
                                            <asp:DropDownList ID="ddlMYear" runat="server" CssClass="droplist" TabIndex="1">
                                            </asp:DropDownList>
                                                                    </b>
                                                </tr>
                                                <tr>
                                                    <b>A/C Posted/Non-Posted
                                            <asp:DropDownList ID="ddlMAccPost" CssClass="droplist" runat="server"
                                                TabIndex="35">
                                                <asp:ListItem Value="1">A/C Non-Posted</asp:ListItem>
                                                <asp:ListItem Value="2">A/C Posted</asp:ListItem>
                                            </asp:DropDownList>
                                                        <asp:TextBox ID="txtMCutdate" runat="server" Visible="false"></asp:TextBox>
                                                    </b>
                                                    <asp:Button Width="80px" ID="btnMSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-primary"
                                                        TabIndex="4" Text="Search" />
                                                    <asp:Button Width="80px" ID="btnMClear" runat="server" Text="Reset" OnClick=" btClear_Click" CssClass="btn btn-danger"
                                                        TabIndex="5" />
                                                    &nbsp;&nbsp;
                                            &nbsp;
                                              <asp:Button ID="btnExcelExport" runat="server" Text="Export Excel" OnClientClick="return confirm('Are You Sure to Export?')"
                                                  OnClick="btnExcelExport_Click"
                                                  TabIndex="7" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" CssClass="btn btn-primary"
                                                  Width="80px" />
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
                            <asp:GridView ID="gvMGratuity" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                HeaderStyle-CssClass="tableHead" CssClass="gridview" GridLines="Both" OnRowDataBound="gvMGratuity_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkMSelectAll" Text="All" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkMToTransfer" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp Id" HeaderStyle-Width="40px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMEmpId" runat="server" Text='<%#Eval("EmpId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp Name" HeaderStyle-Width="160px">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DOJ" HeaderStyle-Width="60px" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMDoj" runat="server" Text='<%#Eval("DOJ")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gross Salary" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMBasic" runat="server" Text='<%#Eval("salary")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Month" HeaderStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonth" runat="server" Text='<%#Eval("Month")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Year" HeaderStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eligible Days" HeaderStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMDays" runat="server" Text='<%#Eval("nDays")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Years" HeaderStyle-Width="80px" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMyears" runat="server" Text='<%#Eval("Years")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gratuity Days" HeaderStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMGDays" runat="server" Text='<%#Eval("gdays")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gratuity Amount" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMGAmt" runat="server" Text='<%#Eval("gamt")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening Balance" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpeningBalance" runat="server" Text='<%#Eval("openingbalance")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Balance" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblclosingbalance" runat="server" Text='<%#Eval("closingbalance")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trans Id" HeaderStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMTransId" runat="server" Text='<%#Eval("transid")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trans Date" HeaderStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMTransDate" runat="server" Text='<%#Eval("transdate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%" id="tblFinal" visible="false" runat="server">
                    <tr>
                        <td>
                            <cc1:Accordion ID="Accordion2" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                <Panes>
                                    <cc1:AccordionPane ID="AccordionPane3" runat="server" HeaderCssClass="accordionHeader"
                                        ContentCssClass="accordionContent">
                                        <Header>
                                Search Criteria</Header>
                                        <Content>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td>Worksite:
                                                  <asp:HiddenField ID="ddlFWorksite_hid" runat="server" />
                                                        <asp:TextBox ID="txtFSearchWorksite" runat="server" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="150px"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtFSearchWorksite"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                            OnClientItemSelected="GetIDF">
                                                        </cc1:AutoCompleteExtender>
                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtFSearchWorksite"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                        <strong>Department&nbsp;
					                                         <asp:HiddenField ID="ddlFDepartment_hid" runat="server" />
                                                            <asp:TextBox ID="textFdept" OnTextChanged="Getdept" AutoPostBack="false" Height="22px" Width="170px" runat="server" runat="server"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList_dept" ServicePath="" TargetControlID="textFdept"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetFdeptID">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="textFdept"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                            <asp:DropDownList ID="ddlFCompany" runat="server" CssClass="droplist" TabIndex="2" Visible="false">
                                                            </asp:DropDownList>
                                                            Emp ID/Name
                            <asp:TextBox ID="txtFEmpId" Height="20px" Width="150px" runat="server"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender9" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtFEmpId"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtFEmpId"
                                                                WatermarkCssClass="Watermarktxtbox" WatermarkText="[Emp ID/Name]"></cc1:TextBoxWatermarkExtender>
                                                            A/C Posted/Non-Posted
                                            <asp:DropDownList ID="ddlFAccPost" CssClass="droplist" runat="server"
                                                TabIndex="35">
                                                <asp:ListItem Value="1">A/C Non-Posted</asp:ListItem>
                                                <asp:ListItem Value="2">A/C Posted</asp:ListItem>
                                            </asp:DropDownList>
                                                    <td>
                                                        <asp:Button ID="Button1" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-primary"
                                                            TabIndex="4" Text="Search" />
                                                        <asp:Button Width="50px" ID="Button2" runat="server" Text="Reset" OnClick=" btClear_Click" CssClass="btn btn-danger"
                                                            TabIndex="5" />
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
                            <asp:GridView ID="gvFinal" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                HeaderStyle-CssClass="tableHead" CssClass="gridview" GridLines="Both" OnRowDataBound="gvFinal_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkFSelectAll" Text="All" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkFToTransfer" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp Id" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFEmpId" runat="server" Text='<%#Eval("EmpId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp Name" HeaderStyle-Width="160px">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DOJ" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDoj" runat="server" Text='<%#Eval("DOJ")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gross Salary" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFBasic" runat="server" Text='<%#Eval("salary")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eligible Days" HeaderStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDays" runat="server" Text='<%#Eval("nDays")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Years" HeaderStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFYears" runat="server" Text='<%#Eval("Years")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gratuity Days(Per Year)" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFGDays" runat="server" Text='<%#Eval("gdays")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gratuity Amount" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFGAmt" runat="server" Text='<%#Eval("gamt")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trans Id" HeaderStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:Label ID="Label13" runat="server" Text='<%#Eval("transid")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trans Date" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="Label14" runat="server" Text='<%#Eval("transdate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="100px" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("GratuityRemarks")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Allowance" HeaderStyle-Width="60px" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAllowance" runat="server" Text='<%#Eval("Allowance")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table id="tblEmp" style="width: 100%;" runat="server" visible="false">
                    <tr>
                        <td>
                            <asp:GridView ID="gvEmpDetails" runat="server" Width="70%" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                HeaderStyle-CssClass="tableHead" CssClass="gridview" GridLines="Both" OnRowDataBound="gvEmpDetails_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="20px">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkESelectAll" Text="All" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEToTransfer" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSEmpId" runat="server" Text='<%#Eval("EmpId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DOJ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDOJ" runat="server" Text='<%#Eval("DOJ")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            <table width="100%">
                <tr>
                    <td style="height: 17px">
                        <uc1:Paging ID="GrtPaging" runat="server" />
                    </td>
                </tr>
            </table>
            <table id="tblcal" runat="server" visible="false">
                <tr>
                    <td>
                        <asp:Label ID="lblcutdate" Text="Cut Off Date" Font-Bold="true" runat="server"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtCutDate" Height="20px" Width="150px" ToolTip="Cut Off Date" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCutDate"
                            PopupButtonID="txtDOB" Format="dd MMM yyyy"></cc1:CalendarExtender>
                    </td>
                </tr>
                <tr style="height: 250px">
                    <td colspan="2" align="left">
                        <asp:Button Width="150px" ID="Button3" runat="server" OnClick="btnCaluculate_Click" CssClass="btn btn-success"
                            TabIndex="4" Text="Caluculate" />
                    </td>
                </tr>
            </table>
            <table id="tblMcal" runat="server" visible="false" style="height: 250px">
                <tr>
                    <td align="left">
                        <asp:Button Width="150px" ID="Button4" OnClientClick="javascript:return validatesave();" runat="server" OnClick="btnCaluculate_Click"
                            TabIndex="4" Text="Caluculate" CssClass="btn btn-success" />
                    </td>
                </tr>
            </table>
            <table id="tblFCal" runat="server" visible="false">
                <tr>
                    <td style="width: 150px">Exit Type<asp:DropDownList ID="ddlEmpDeAct" runat="server">
                    </asp:DropDownList>
                    </td>
                    <td>Date of Settlement
                <br />
                        <asp:TextBox ID="txtFinalDate" Height="20px" Width="150px" ToolTip="Final Date" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFinalDate"
                            PopupButtonID="txtDOB" Format="dd MMM yyyy"></cc1:CalendarExtender>
                    </td>
                </tr>
                <tr style="height: 300px;">
                    <td align="left" colspan="2">
                        <asp:Button Width="80px" ID="Button5" OnClientClick="javascript:return validatesave();" runat="server" OnClick="btnCaluculate_Click"
                            TabIndex="4" Text="Caluculate" CssClass="btn btn-primary" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnTransferAcc" ToolTip="Transfer To Accounts Dept."
                            runat="server" Text="A/C Posting" OnClick="btnTransferAcc_Click" Visible="false" CssClass="btn btn-success" />
                    </td>
                </tr>
            </table>
        <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelExport" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>
