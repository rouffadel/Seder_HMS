<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpSalhikesV2.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMSV1.EmpSalhikesV2V1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
       
        <ContentTemplate>
            <asp:Label runat="server" ID="lblStatus" Text="" Font-Size="14px"></asp:Label>
            <script language="javascript" type="text/javascript">
                function Validate() {
                    if (!chkNumber('<%=txtSalary.ClientID%>', "Salary", true, "")) {
                        return false;
                    }
                }
                function isNumber(evt) {
                    var iKeyCode = (evt.which) ? evt.which : evt.keyCode
                    if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                        return false;
                    return true;
                }
                function UpdateWages(ctrl, wageid, empid, Status, CentageID, UserID, Centage, EmpSalID, empsaltext, basicID) {
                    //debugger;
                    var obj = document.getElementById(ctrl.id.replace(ctrl.id, Centage)); // $get(Centage).value; 
                    var Cent = $get(Centage).value;
                    if (Cent == "" || Cent == null) {
                        Cent = "0";
                    }
                    var row = ctrl.parentNode.parentNode;
                    var rowIndex = row.rowIndex - 1;
                    var chkStatus = row.cells[0].getElementsByTagName("input")[0].checked;// $get(Status).checked;
                    var basicVal = parseFloat($get(basicID).value);
                    var updatedSal = (basicVal*12).toFixed(0);
                    var Result = AjaxDAL.UpdateWagesPercent(empid, wageid, chkStatus, CentageID, UserID, "1.0", EmpSalID, updatedSal);
                    alert(Result.value);
                }
                function UpdateAllowances(ctrl, allowid, empid, Status, CentageID, UserID, Centage, EmpSalID, ITStatus) {
                    //debugger;
                    var row = ctrl.parentNode.parentNode;
                    var rowIndex = row.rowIndex - 1;
                    var Cent = row.cells[7].getElementsByTagName("input")[0].value; //obj.innerHTML;
                    var chkStatus = row.cells[0].getElementsByTagName("input")[0].checked;//objchk.checked;//$get(Status).checked;
                    var chkITStatus = row.cells[1].getElementsByTagName("input")[0].checked;// $get(ITStatus).checked;
                    if (Cent == "" || Cent == null) {
                        Cent = "0";
                    }
                    var Result = AjaxDAL.UpdateAllowancesPercent(empid, allowid, chkStatus, CentageID, UserID, Cent, EmpSalID, chkITStatus);
                    alert(Result.value);
                }
                function UpdateEmpContribution(ctrl, allowid, empid, Status, CentageID, UserID, Centage, EmpSalID, ITStatus) {
                    var obj = document.getElementById(ctrl.id.replace(ctrl.id, Centage)); // $get(Centage).value; 
                    var Cent = $get(Centage).value; //obj.innerHTML;
                    var row = ctrl.parentNode.parentNode;
                    var rowIndex = row.rowIndex - 1;
                    var chkStatus = row.cells[0].getElementsByTagName("input")[0].checked;//$get(Status).checked;
                    var chkITStatus = row.cells[1].getElementsByTagName("input")[0].checked// $get(ITStatus).checked;
                    if (Cent == "" || Cent == null) {
                        var Result = AjaxDAL.UpdateEmpContributionPercent(empid, allowid, chkStatus, CentageID, UserID, Cent, EmpSalID, chkITStatus);
                        alert(Result.value);
                    }
                    else {
                        if (Cent == 0) {
                            alert('Limit Exceed');
                        }
                        else {
                            var Result = AjaxDAL.UpdateEmpContributionPercent(empid, allowid, chkStatus, CentageID, UserID, Cent, EmpSalID, chkITStatus);
                            alert(Result.value);
                        }
                    }
                }
                function UpdateEmpDeductions(ctrl, allowid, empid, Status, CentageID, UserID, Centage, EmpSalID, ITStatus) {
                    var obj = document.getElementById(ctrl.id.replace(ctrl.id, Centage)); // $get(Centage).value; 
                    var Cent = $get(Centage).value; //obj.innerHTML;
                    var row = ctrl.parentNode.parentNode;
                    var rowIndex = row.rowIndex - 1;
                    var chkStatus = row.cells[0].getElementsByTagName("input")[0].checked;//$get(Status).checked;
                    var chkITStatus = row.cells[1].getElementsByTagName("input")[0].checked// $get(ITStatus).checked;
                    var Result = AjaxDAL.UpdateEmpDeductionsPercent(empid, allowid, chkStatus, CentageID, UserID, Cent, EmpSalID, chkITStatus);
                    alert(Result.value);
                }
                function UpdateEmpNonCTCComp(ctrl, allowid, empid, Status, Centage, EmpSalID) {
                    var row = ctrl.parentNode.parentNode;
                    var Cent = row.cells[1].getElementsByTagName("input")[0].value;//obj.innerHTML;
                    var chkStatus = row.cells[0].getElementsByTagName("input")[0].checked;//$get(Status).checked;
                    if (Cent != "" && Cent != null) {
                        var Result = AjaxDAL.UpdateNOnCTCComponents(empid, allowid, chkStatus, EmpSalID, Cent);
                        alert(Result.value);
                    }
                    else {
                        alert('Enter Value');
                    }
                }
                function WagesPercentageCal(ctrl, Centage, CentageValue, EmpSal) {
                    var vrCent = ctrl.value;// $get(txtvalid).value; //  var vrCent = $get(CentageValue).value; by kaushal:13/3/16
                    if (vrCent != "" && vrCent != null) {
                        var Result = AjaxDAL.WagesPercentCalculation(vrCent, EmpSal);
                        var row = ctrl.parentNode.parentNode;
                        row.cells[4].getElementsByTagName("input")[0].value = Result.value;// $get(Centage).value = Result.value;
                    }
                }
                function AllowancesPercentageCal(ctrl, allowid, Centage, CentageValue, EmpSal, empid, EmpSalID) {
                    //debugger;
                    var row = ctrl.parentNode.parentNode;
                    var rowIndex = row.rowIndex - 1;
                    // var city = ;
                    var vrCent = ctrl.value; //  var vrCent = $get(CentageValue).value;// by kaushal:13/3/16
                    var centge = document.getElementById(ctrl.id.replace(ctrl.id, Centage))
                    if (vrCent != "" && vrCent != null) {
                        var Result = AjaxDAL.AllowancesPercentCalculation(vrCent, EmpSal, allowid, empid, EmpSalID);
                        //$get(Centage).value = Result.value;
                        row.cells[7].getElementsByTagName("input")[0].value = Result.value;// Result.value;
                    }
                }
                function ContrPercentageCal(ctrl, ItemID, Centage, CentageValue, EmpSal, empid, EmpSalID) {
                    var vrCent = ctrl.value; //$get(CentageValue).value;
                    if (vrCent != "" || vrCent != null) {
                        var Result = AjaxDAL.ContrPercentageCal(vrCent, EmpSal, ItemID, empid, EmpSalID);
                        var row = ctrl.parentNode.parentNode;
                        row.cells[6].getElementsByTagName("input")[0].value = Result.value;// $get(Centage).value = Result.value;
                    }
                }
                function DedudPercentageCal(ctrl, ItemID, Centage, CentageValue, EmpSal, empid, EmpSalID) {
                    //debugger;
                    var vrCent = ctrl.value;//$get(CentageValue).value;
                    if (vrCent != "" || vrCent != null) {
                        var Result = AjaxDAL.DedudPercentageCal(vrCent, EmpSal, ItemID, empid, EmpSalID);
                        var row = ctrl.parentNode.parentNode;
                        row.cells[6].getElementsByTagName("input")[0].value = Result.value;// $get(Centage).value = Result.value;
                    }
                }
            </script>
            <table id="EmpList">
                <tr>
                    <td>
                        <cc1:Accordion ID="SimAlloListAccordion" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="false" SuppressHeaderPostbacks="true">
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
                                                                <asp:DropDownList ID="ddldepartments" Visible="false" runat="server" CssClass="droplist"
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
                                                            Category :
                                            <asp:DropDownList ID="ddlSearCategory" Visible="false" CssClass="droplist" runat="server">
                                            </asp:DropDownList>
                                                    <asp:TextBox ID="txtCategorySearch" OnTextChanged="GetCategory" Height="22px" Width="130px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters="" Enabled="True"
                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionListCat" ServicePath="" TargetControlID="txtCategorySearch"
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
                                                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                                    CssClass="savebutton btn btn-primary" Width="80px" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]"
                                                                    TabIndex="6" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSalaryRevisionDiscrepancy" runat="server" Text="SalaryRevisionDiscrepancy" OnClick="btnSalaryRevisionDiscrepancy_Click" CssClass="btn btn-primary" />
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
                        <asp:GridView ID="gveditkbipl" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpId"
                            OnRowCommand="gveditkbipl_RowCommand" Width="100%"
                            CssClass="gridview"
                            EmptyDataText="No Records Found" HeaderStyle-CssClass="tableHead" EmptyDataRowStyle-CssClass="EmptyRowData"
                            AllowSorting="True">
                            <Columns>
                                <asp:BoundField HeaderText="EmpId" DataField="EmpId" SortExpression="EmpId">
                                    <ControlStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Historical ID" DataField="HisID" Visible="false" SortExpression="HisID">
                                    <ControlStyle Width="50px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Employee" HeaderStyle-HorizontalAlign="Left" SortExpression="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("name") %>' ToolTip='<%#Eval("OldEmpID")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Design" HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Category" HeaderText="Trades" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Worksite" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorksite" runat="server" Text='<%#GetWorkSite(DataBinder.Eval(Container.DataItem, "Categary").ToString())%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepartment" runat="server" Text='<%#GetDepartment(DataBinder.Eval(Container.DataItem, "DeptNo").ToString())%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gross" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkRevise" runat="server" CssClass="anchor__grd edit_grd" Text="Revise" CommandName="Revise"
                                            CommandArgument='<%#Eval("EmpId")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="EmptyRowData" />
                            <HeaderStyle CssClass="tableHead" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px; width: 75%">
                        <uc1:Paging ID="EmpListPaging" runat="server" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%" id="tblmain" runat="server" visible="false">
                <tr>
                    <td colspan="2" style="width: 22%; vertical-align: top">
                        <table style="border: solid">
                            <tr>
                                <td colspan="2">
                                    <asp:LinkButton ID="lnkAdd" CssClass="btn btn-success" runat="server" Text="Add" OnClick="lnkAdd_Click"></asp:LinkButton>&nbsp;
                <asp:LinkButton ID="lnkEdit" CssClass="btn btn-primary" runat="server" Text="View" OnClick="lnkEdit_Click"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td class="pageheader" colspan="2">
                                    <b>CTC/Salary-Revisions</b>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <strong><b>Name:</b> </strong>:
                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <strong><b>Department:</b> </strong>:
                                        <asp:Label ID="lblDept" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <strong><b>Designation:</b> </strong>:
                                        <asp:Label ID="lbldesig" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <strong><b>Date of Joining :</b></strong>
                                    <asp:Label ID="lblDOJ" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table id="tblEdit" runat="server">
                                        <tr>
                                            <td align="Left" style="vertical-align: top; width: 20%;" colspan="2">
                                                <asp:GridView ID="gvAllowances" runat="server" AutoGenerateColumns="False" CellPadding="4" OnRowDataBound="gvAllowances_RowDataBound"
                                                    ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                                    OnRowCommand="gvWages_RowCommand" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="FromDate" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFromDate" runat="server" Text='<%#Eval("FromDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ToDate" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblToDate" runat="server" Text='<%#Eval("ToDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Salary[Yearly]" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSalary" runat="server" Text='<%#Eval("Salary")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAudited" runat="server" Text='<%#Eval("IsAudited")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="anchor__grd edit_grd" Text="Edit" CommandArgument='<%#Eval("EmpSalID")%>'
                                                                    CommandName="Edt"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkBreak" runat="server" CssClass="anchor__grd edit_grd" Text="Breakup" CommandArgument='<%#Eval("EmpSalID")%>'
                                                                    CommandName="brkup" ToolTip='<%#Eval("CreatedBy") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Status" DataField="AppliedStatus"  />
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
                            <tr>
                                <td colspan="2">
                                    <table id="tblNew" runat="server" visible="false" style="border-top-color: #0094ff; border-top-width: 1px; border-top-style: solid; border-top-left-radius: 20px; border-top-color: #0094ff; border-top-width: 1px; border-top-style: solid; border-top-left-radius: 20px;" width="100%">
                                        <tr>
                                            <td>Revised CTC/Salary(Per Annum)<span style="color: #ff0000">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSalary" runat="server" Width="100px" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Year <span style="color: #ff0000">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlyear" runat="server" CssClass="droplist"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Month <span style="color: #ff0000">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlmonth" runat="server" CssClass="droplist" AutoPostBack="true" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
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
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Effective From
                                            </td>
                                            <td>
                                                <asp:Label ID="lbleffectivefrom" runat="server" Enabled="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="border-top-color: #0094ff; border-top-width: 1px; border-top-style: solid; border-top-left-radius: 20px; border-top-color: #0094ff; border-top-width: 1px; border-top-style: solid; border-top-left-radius: 20px;">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="savebutton btn btn-success" Width="50px"
                                                    OnClick="btnSubmit_Click" OnClientClick="javascript:return Validate();" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="Left" style="vertical-align: top; width: 78%;">
                        <table runat="server" id="tblaccordin" width="100%">
                            <tr>
                                <td>
                                    <div id="MyAccordion" runat="Server">
                                        <table width="100%">
                                            <tr>
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnConfigureOtherItems" runat="server" Text="Configure Other Items" OnClick="btnConfigureOtherItems_Click" CssClass="btn btn-success" />
                                                    <asp:GridView ID="grdWages" runat="server" AutoGenerateColumns="false" OnRowCommand="grdWages_RowCommand1"
                                                        OnRowDataBound="grdWages_RowDataBound" ForeColor="#333333" GridLines="Both" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Salary">
                                                                <ItemTemplate>
                                                                    <%--<asp:CheckBox ID="chkWages" runat="server" Text='<%#Eval("Name")%>' Checked='<%#Eval("Access")%>'></asp:CheckBox>--%>
                                                                    <asp:CheckBox ID="chkWages" runat="server" Text='<%#Eval("Name")%>' Checked='<%#Eval("Access")%>'></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="% on CTC/Salary" DataField="CentageOnCTC" DataFormatString="{0:P2}" />
                                                            <asp:BoundField HeaderText="Extg Val[Monthly]" DataField="Value" DataFormatString="{0:N2}" />
                                                            <asp:TemplateField HeaderText="Revised Val[Monthly]">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCentageValue1" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Revised %">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtwagepercent" runat="server" Enabled="false" Width="60px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdt" runat="server" CssClass="anchor__grd vw_grd" Text="Save"
                                                                        CommandName="edt" CommandArgument='<%#Eval("WagesId")%>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="grdAllowances" runat="server" AutoGenerateColumns="false"
                                                         ForeColor="#333333" GridLines="Both" HeaderStyle-CssClass="tableHead" CssClass="gridview"
                                                        OnRowDataBound="grdAllowances_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Allowances">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkAllowances" runat="server" Text='<%#Eval("Name")%>' Checked='<%#Eval("Access")%>'></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="IT Exempted">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkAllowancesIT" runat="server" Checked='<%#Eval("ITExemptions")%>'></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Calculated on">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAllowanceBased" runat="server" Text='<%#Eval("CalculatedOn")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Extg %" DataField="CentageOnCTC" DataFormatString="{0:P2}" />
                                                            <asp:BoundField HeaderText="Extg Val[Monthly]" DataField="Value" DataFormatString="{0:N2}" />
                                                            <asp:TemplateField HeaderText="Max Val">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaxAllowancevalue" runat="server" Text='<%#Eval("Limit")%>'> </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Revised Val[Monthly]">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCentageValue2" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Revised %">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtAllowancepercent" runat="server" Enabled="false" Width="60px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdt1" runat="server" CssClass="anchor__grd vw_grd" Text="Save" CommandName="edt" CommandArgument='<%#Eval("AllowId")%>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="grdCContribution" runat="server" AutoGenerateColumns="false" ForeColor="#333333" GridLines="Both"
                                                        HeaderStyle-CssClass="tableHead" CssClass="gridview"
                                                        OnRowDataBound="grdCContribution_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Company Contribution">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkCContribution" runat="server" Text='<%#Eval("Name")%>' Checked='<%#Eval("Access")%>'></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="IT Exempted">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkCContributionIT" runat="server" Checked='<%#Eval("ITExemptions")%>'></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Pre Centage" DataField="CentageOnCTC" DataFormatString="{0:P2}" />
                                                            <asp:BoundField HeaderText="Pre Value" DataField="Value" DataFormatString="{0:N2}" />
                                                            <asp:TemplateField HeaderText="Max Value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaxCContributionvalue" runat="server" Text='<%#Eval("Limit")%>'> </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Value[Monthly]">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCentageValue3" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Centage">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCContributionpercent" runat="server" Enabled="false" Width="60px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdt2" runat="server" CssClass="anchor__grd vw_grd" Text="Save" CommandName="edt" CommandArgument='<%#Eval("Itemid")%>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="grdStatutory" runat="server" AutoGenerateColumns="false" CssClass="gridview"
                                                        ForeColor="#333333" GridLines="Both" HeaderStyle-CssClass="tableHead" OnRowDataBound="grdStatutory_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Deduct Statutory">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkDStatutory" runat="server" Text='<%#Eval("Name")%>' Checked='<%#Eval("Access")%>'></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="IT Exempted">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkDStatutoryIT" runat="server" Checked='<%#Eval("ITExemptions")%>'></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Pre Centage" DataField="CentageOnCTC" DataFormatString="{0:P}" />
                                                            <asp:BoundField HeaderText="Pre Value[Monthly]" DataField="Value" DataFormatString="{0:N2}" />
                                                            <asp:TemplateField HeaderText="Max Value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaxDStatutoryvalue" runat="server" Text='<%#Eval("Limit")%>'> </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Value[Monthly]">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCentageValue4" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Centage">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDStatutorypercent" runat="server" Enabled="false" Width="60px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdt3" runat="server" CssClass="anchor__grd vw_grd" Text="Save" CommandName="edt" CommandArgument='<%#Eval("Itemid")%>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="grdNonCTCComponts" runat="server" AutoGenerateColumns="false" CssClass="gridview"
                                                        ForeColor="#333333" GridLines="Both" HeaderStyle-CssClass="tableHead" OnRowDataBound="grdNonCTCComponts_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Non CTC Components">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkNonCTCComp" runat="server" Text='<%#Eval("LongName")%>' Checked='<%#Eval("Access")%>'></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Value[Yearly]">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCentageValue" runat="server" Text='<%#Eval("Amount")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdt4" runat="server" CssClass="anchor__grd vw_grd" Text="Save" CommandName="edt" CommandArgument='<%#Eval("CompID")%>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" CssClass="btn btn-warning" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table id="tblSalaryRevisionDiscontinuity" runat="server">
                <tr>
                    <td>
                        <asp:Label ID="lblSalaryrev" Text="Salary Revision Discontinuity" runat="server" Font-Size="16px" ForeColor="#e89107" Visible="false"></asp:Label>
                        <asp:GridView ID="gvSalRevision" runat="server" AutoGenerateColumns="false"
                            GridLines="Both" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                            <Columns>
                                <asp:BoundField HeaderText="Empid" DataField="empid" />
                                <asp:BoundField HeaderText="EmpSalID" DataField="EmpSalID" />
                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                <asp:BoundField HeaderText="From Date" DataField="fromdate" />
                                <asp:BoundField HeaderText="To date" DataField="todate" />
                                <asp:BoundField HeaderText="Diff Days" DataField="diffdays" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
