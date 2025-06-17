<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EmployeeList.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
    Inherits="AECLOGIC.ERP.HMSV1.EmployeeListV1" Title="Employee List" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=lstColumns]').multiselect({
                includeSelectAllOption: true,
                width: '500px'
            });
        });
    </script>
   <style>
         input[type="text"] {
            max-width: 400px;
            margin: 2px 0;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function GetNationality(source, eventArgs) { var HdnKey = eventArgs.get_value(); document.getElementById('<%=hdSNationality .ClientID %>').value = HdnKey; }
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
        function showApp(EmpID) {
            window.showModalDialog("../HMS/empdocuments.aspx?Eid=" + EmpID, "", "dialogheight:900px;dialogwidth:900px;status:no;edge:sunken;unadorned:no;resizable:yes;");
        }
        //Check Number
        function chkNumber(object, msg, isRequired, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (isRequired) {
                if (!Reval(object, msg, waterMark))
                { return false; }
            }
            if (val != '') {
                var rx = new RegExp("[0-9]*");
                var matches = rx.exec(val);
                if (matches == null || val != matches[0]) {
                    alert(msg + " can take numbers only!!!");
                    elm.focus();
                    return false;
                }
            } return true;
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
        function getObj(the_id) {
            if (typeof (the_id) == "object") {
                return the_id;
            }
            if (typeof document.getElementById != 'undefined') {
                return document.getElementById(the_id);
            } else if (typeof document.all != 'undefined') {
                return document.all[the_id];
            } else if (typeof document.layers != 'undefined') {
                return document.layers[the_id];
            } else {
                return null;
            }
        }
        //Required Validation
        function Reval(object, msg, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (val == '' || val.length == 0 || val == waterMark) {
                alert(msg + " should not be empty!!! ");
                elm.focus();
                return false;
            }
            return true;
        }
        function valids() {
            if (document.getElementById('<%=chkRejoin.ClientID %>') != null) {
                if (document.getElementById('<%=txtRejoin.ClientID%>').value == "") {
                    alert("Please select date.!");
                    document.getElementById('<%=txtRejoin.ClientID%>').focus();
                    return false;
                }
                if (document.getElementById('<%=txtStatusRemarks.ClientID %>').value == "") {
                    alert("Enter Remarks.!");
                    document.getElementById('<%=txtStatusRemarks.ClientID%>').focus();
                    return false;
                }
            }
            else {
                if (document.getElementById('<%=txtStatusRemarks.ClientID %>').value == "") {
                    document.getElementById('<%=txtStatusRemarks.ClientID%>').focus();
                    alert("Enter Remarks.!");
                    return false;
                }
            }
        }
        function ShowHideRejoin() {
            if (document.getElementById('<%=chkRejoin.ClientID %>').checked == true)
                spnReJoinDate.style.display = "block";
            else
                spnReJoinDate.style.display = "none";
        }
        //chaitanya:validations for below code
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
    <asp:UpdatePanel ID="SimAllocationUpdPanel" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td>
                        <%--<cc1:Accordion ID="Accordion1" runat="server" HeaderCssClass="accordionHeader"
                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                <Panes>
                                    <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                        ContentCssClass="accordionContent">
                                        <Header>
                                        Search Criteria</Header>
                                        <Content>--%>

                        <table style="width: 100%">
                            <tr>
                                <td style="width: 100px">
                                    <asp:ListBox ID="lstColumns" CssClass="droplist" size="3" runat="server" SelectionMode="Multiple" DataTextField="Name" DataValueField="ID"></asp:ListBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 400px">
                                    <table style="width:100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtSearchString" placeholder="Search String" MaxLength="500"  runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 50px">
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btnAdvanceSearchFilter" runat="server" CssClass="ButtonStyle" ImageUrl="~/images/Search-icon16.png"
                                    OnClick="btnAdvanceSearchFilter_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td style="width: 80px">
                                    <asp:DropDownList ID="ddlFilterType" CssClass="droplist" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 80px">&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblContainstable" Text="Sort With Rank" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkFilterContainstable" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <%-- </Content>
                                    </cc1:AccordionPane>
                                </Panes>
                            </cc1:Accordion>--%>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" class="DivBorderMaroon">
                <tr>
                    <td align="left">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="SimAlloListAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="864px" Visible="false">
                                        <Panes>
                                            <cc1:AccordionPane ID="SimAlloListAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td colspan="2">WorkSite: &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:DropDownList ID="ddlworksites" Visible="false" runat="server" CssClass="droplist" AutoPostBack="true" OnSelectedIndexChanged="ddlworksites_SelectedIndexChanged"
                                                                    AccessKey="w" ToolTip="[Alt+w+Enter]" TabIndex="1">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtSearchWorksite" OnTextChanged="Worksidechangewithdep" Height="22px" Width="110px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                &nbsp;Department:&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:DropDownList ID="ddldepartments" Visible="false" runat="server" CssClass="droplist" OnSelectedIndexChanged="ddldepartments_SelectedIndexChanged"
                                                                    TabIndex="2">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtSearchdept" OnTextChanged="GetDepartmentSearch" Height="22px" Width="130px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionListdept" ServicePath="" TargetControlID="txtSearchdept"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchdept"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                                &nbsp;<asp:Label ID="lblNature" runat="server" Text="Emp Nature"></asp:Label>
                                                                <asp:DropDownList ID="ddlEmpNature" AutoPostBack="true" Height="22px" Width="110px" runat="server" CssClass="droplist">
                                                                </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                                                               &nbsp;Designation :
                                                                <asp:DropDownList ID="ddlSearDesigantion" Visible="false" CssClass="droplist" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtDesignationSerach" OnTextChanged="GetDesignation" Height="22px" Width="130px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionListdesi" ServicePath="" TargetControlID="txtDesignationSerach"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtDesignationSerach"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Designation Name]"></cc1:TextBoxWatermarkExtender>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                            &nbsp;
                                                            Trade:
                                                                <asp:DropDownList ID="ddlSearCategory" Visible="false" CssClass="droplist" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtCategorySearch" OnTextChanged="GetCategory" Height="22px" Width="130px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod=" " ServicePath="" TargetControlID="txtCategorySearch"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtCategorySearch"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Category Name]"></cc1:TextBoxWatermarkExtender>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;Historical ID:<asp:TextBox ID="txtOldEmpID" Width="50" runat="server" AccessKey="1"
                                                                ToolTip="[Alt+1]" TabIndex="3"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                &nbsp;EmpID:<asp:TextBox ID="txtEmpID" Width="50" runat="server" AccessKey="2" ToolTip="[Alt+2]"
                                                                    TabIndex="4" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FTBEmpID" runat="server" TargetControlID="txtEmpID"
                                                                    FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                &nbsp;Name:<asp:TextBox ID="txtusername" Width="90" runat="server" MaxLength="30"
                                                                    OnTextChanged="btnSearch_Click" CssClass="droplist" AccessKey="3" ToolTip="[Alt+3]"
                                                                    TabIndex="5"></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtusername"
                                                                    WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Name]"></cc1:TextBoxWatermarkExtender>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                Nationality :
                                                                 <asp:TextBox ID="txtSNationality" runat="server" Height="22px" Width="140px" AutoPostBack="True">  </asp:TextBox>
                                                                <asp:HiddenField ID="hdSNationality" runat="server" Value="0" />
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtenderNationality" runat="server" DelimiterCharacters="" Enabled="true" MinimumPrefixLength="1"
                                                                    ServiceMethod="GetCompletionNationality" ServicePath="" TargetControlID="txtSNationality" UseContextKey="True" CompletionInterval="10"
                                                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                                    OnClientItemSelected="GetNationality" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderNationality" runat="server" TargetControlID="txtSNationality"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Nationality]"></cc1:TextBoxWatermarkExtender>
                                                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                                    CssClass="savebutton btn btn-primary" Width="80px" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]"
                                                                    TabIndex="6" />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="droplist">&nbsp;<asp:RadioButton ID="rbActive" runat="server" AutoPostBack="True" Checked="True"
                                                                GroupName="Emp" Text="Active" OnCheckedChanged="rbActive_CheckedChanged" TabIndex="7" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:RadioButton ID="rbInActive" runat="server" AutoPostBack="True" GroupName="Emp"
                                                                    Text="Inactive" OnCheckedChanged="rbInActive_CheckedChanged" TabIndex="8" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:CheckBox ID="chkHis" runat="server" Text="Show Historical ID" AutoPostBack="true"
                                                                    OnCheckedChanged="chkHis_CheckedChanged" TabIndex="9" />&nbsp;&nbsp;&nbsp;&nbsp;
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
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gveditkbipl" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpId"
                                        AlternatingRowStyle-BackColor="GhostWhite" OnRowCommand="GridView1_RowCommand"
                                        CssClass="gridview" OnRowDeleting="gveditkbipl_RowDeleting" OnRowDataBound="gveditkbipl_RowDataBound"
                                        EmptyDataText="No Records Found" HeaderStyle-CssClass="tableHead" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        AllowSorting="True" OnSorting="gveditkbipl_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="EmpID" Visible="true" SortExpression="EmpId">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpId" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Historical ID" DataField="HisID" Visible="false" SortExpression="HisID">
                                                <ControlStyle Width="50px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Employee" HeaderStyle-HorizontalAlign="Left" SortExpression="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("Employee") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Trade" HeaderText="Trades" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Worksite" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWorksite" runat="server" Text='<%#Eval("Worksite") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Department") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PersonalMobile" HeaderText="Personal Mobile" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CompanyMobile" HeaderText="Company Mobile" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:ButtonField CommandName="Edt" HeaderText="Edit" InsertVisible="False" Text="Edit"
                                                ControlStyle-CssClass="anchor__grd edit_grd" HeaderStyle-HorizontalAlign="Left ">
                                                <ControlStyle ForeColor="Blue" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:ButtonField>
                                            <%--<asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkstatus" CssClass="anchor__grd dlt" ToolTip='<%#BindInActBy(DataBinder.Eval(Container.DataItem, "EmpId").ToString())%>'
                                                        OnClientClick="return confirm('Are you Sure?');" runat="server" CommandName="Status"
                                                        Text='<%#Bind("CurrentStatus")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Docs" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a id="A1" href='<%# String.Format("empdocuments.aspx?eid={0}",DataBinder.Eval(Container.DataItem, "EmpId")) %>'
                                                        runat="server" class="btn btn-primary">View</a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gross" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a id="A3" href='<%# String.Format("EmpSalhikesV2.aspx?Empid={0}",DataBinder.Eval(Container.DataItem, "EmpId")) %>'
                                                        runat="server" class="btn btn-warning">Revise</a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other Details" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a id="A4" href='<%# String.Format("OtherDetails.aspx?EmpID={0}",DataBinder.Eval(Container.DataItem, "EmpId")) %>'
                                                        runat="server" class="btn btn-primary">View</a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Resume">
                                                <ItemTemplate>
                                                    <a id="A6" target="_blank" href='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "Resume").ToString(),DataBinder.Eval(Container.DataItem, "AppID").ToString()) %>'
                                                        runat="server" class="btn btn-primary">View</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Qua/Exp" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkQuaExp" runat="server" CssClass="anchor__grd vw_grd" Text="Add" CommandName="AddQuaExp"
                                                        CommandArgument='<%#Eval("EmpId")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="EmptyRowData" />
                                        <HeaderStyle CssClass="tableHead" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 17px">
                                    <uc1:Paging ID="EmpListPaging" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table id="tblStatus" runat="server" class="DivBorderBlue" width="40%">
                <tr id="tblRejoin" runat="server">
                    <td style="width: 98px">
                        <b>Rejoin</b>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkRejoin" Text="Yes" runat="server" Font-Bold="True" onchange="Javascript:return ShowHideRejoin();" />&nbsp;&nbsp;
                        <span id="spnReJoinDate" style="display: none"><b>Rejoin On : </b><b><span style="color: #ff0000">*</span></b>&nbsp;<asp:TextBox ID="txtRejoin" runat="server" Format="dd/MM/yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtRejoin"
                                TargetControlID="txtRejoin" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                        </span>
                    </td>
                </tr>
                <tr id="trDeactivate" runat="server">
                    <td>
                        <b>Inactivate Type</b>:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEmpDeAct" runat="server">
                            <asp:ListItem Text="Resignation" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Termination" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Indiscipline" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Contract Of Contract" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 158px">
                        <b>Remarks:<span style="color: #ff0000">*</span></b>
                    </td>
                    <td>&nbsp;<asp:TextBox ID="txtStatusRemarks" ToolTip="Status Remarks" TextMode="MultiLine"
                        runat="server" BorderColor="#CC6600" BorderStyle="Outset" BorderWidth="2px" Height="100px"
                        Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 158px"></td>
                    <td style="padding-left: 100Px">
                        <asp:Button ID="btnStatusRemarks" ToolTip="Save Remarks" CssClass="savebutton" runat="server"
                            Text="Save" OnClick="btnStatusRemarks_Click" OnClientClick="javascript:return valids();" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdvanceSearchFilter" />
            <asp:PostBackTrigger ControlID="txtSearchString" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="SimAllocationUpdPanel">
            <ProgressTemplate>
                <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                    ID="imgs" />
                please wait...
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>

