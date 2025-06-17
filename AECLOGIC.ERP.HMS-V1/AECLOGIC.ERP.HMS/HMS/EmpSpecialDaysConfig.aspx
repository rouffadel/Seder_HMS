<%@ Page Title="" Language="C#" AutoEventWireup="True" CodeBehind="EmpSpecialDaysConfig.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.EmpRamzanConfig" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function controlEnter(event) {
            // alert('hello');
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13) {
                // alert("Enter fired");
                document.getElementById('<%= btnSearch.ClientID %>').click();
            return false;
        }
        else {
            return true;
        }
    }
    function SelectAll(CheckBox) {
        TotalChkBx = parseInt('<%= this.grdEmpMessAtt.Rows.Count %>');
        var TargetBaseControl = document.getElementById('<%= this.grdEmpMessAtt.ClientID %>');
        var TargetChildControl = "chkAll";
        var Inputs = TargetBaseControl.getElementsByTagName("input");
        for (var iCount = 0; iCount < Inputs.length; ++iCount) {
            if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0)
                Inputs[iCount].checked = CheckBox.checked;
        }
    }
    function validateCheckBox() {
        var isValid = false;
        var gvChk = document.getElementById('<%= grdEmpMessAtt.ClientID %>');
        for (var i = 1; i < gvChk.rows.length; i++) {
            var chkInput = gvChk.rows[i].getElementsByTagName('input');
            if (chkInput != null) {
                if (chkInput[0].type == "checkbox") {
                    if (chkInput[0].checked) {
                        isValid = true;
                        return true;
                    }
                }
            }
        }
        alert("Please select atleast one Employee.");
        return false;
    }
    function Validate() {
        <%--  if (!chkName('<%=txtSpDayName.ClientID%>', "Name", "true", "[ Employee Nature]"))
             {
                return false;
             }  --%>
        if (document.getElementById('<%=txtSpDayName.ClientID%>').value == "") {
            alert("Please Enter Name");
            document.getElementById('<%=txtSpDayName.ClientID%>').focus();
            return false;
        }
        if (!chkDate('<%= txtFromDate.ClientID%>', "Date", true, "")) {
            return false;
        }
        if (!chkDate('<%= txtToDate.ClientID%>', "Date", true, "")) {
            return false;
        }
        if (!chkNumber('<%=txtWHS.ClientID %>', 'Working Hours', true, ''))
            return false;
    }
    //function Reval(object, msg, waterMark) {
    //    var elm = getObj(object);
    //    var val = Trim(elm.value);
    //    if (val == '' || val.length == 0 || val == waterMark) {
    //        alert(msg + " should not be empty!!! ");
    //        //elm.value = waterMark;
    //        elm.focus();
    //        return false;
    //    }
    //    return true;
    //}
    function GetID(source, eventArgs) {
        var HdnKey = eventArgs.get_value();
        //  alert(HdnKey);
        document.getElementById('<%=ddlWorkSite_hid.ClientID %>').value = HdnKey;
    }
    function GETDEPT_ID(source, eventArgs) {
        var HdnKey = eventArgs.get_value();
        //  alert(HdnKey);
        document.getElementById('<%=ddlDept_hid.ClientID %>').value = HdnKey;
    }
    function GETDesg_ID(source, eventArgs) {
        var HdnKey = eventArgs.get_value();
        //  alert(HdnKey);
        document.getElementById('<%=ddlDesif2_hid.ClientID %>').value = HdnKey;
    }
    function isNumber(evt) {
        var iKeyCode = (evt.which) ? evt.which : evt.keyCode
        if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
            return false;
        return true;
    }
    </script>
    <%--<asp:Panel ID="pnlOuterTop" runat="server" BackColor="#DCDCDC" Width="100%">--%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <asp:LinkButton ID="lnkEmp" runat="server" Text="Emp Config" CssClass="savebutton"
                            OnClick="lnkEmp_Click"></asp:LinkButton>
                        &nbsp;| &nbsp;
            <asp:LinkButton ID="lnkDates" runat="server" Text="Dates" CssClass="savebutton"
                OnClick="lnkDates_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:MultiView ID="MainView" runat="server">
                            <asp:View ID="EmpConfigvw" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <table width="100%" id="tblSearch">
                                                <tr>
                                                    <td>
                                                        <cc1:Accordion ID="LstOfHolidayConAccordion" runat="server" HeaderCssClass="accordionHeader"
                                                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                                            RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%">
                                                            <Panes>
                                                                <cc1:AccordionPane ID="LstOfHolidayConAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                                    ContentCssClass="accordionContent">
                                                                    <Header>
                                            Search Criteria</Header>
                                                                    <Content>
                                                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <b>
                                                                                        <asp:Label ID="lblWorksite" runat="server" Text="Worksite"></asp:Label>:</b>
                                                                                    &nbsp;
                                                         <asp:HiddenField ID="ddlWorkSite_hid" runat="server" />
                                                                                    <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                                                    </cc1:AutoCompleteExtender>
                                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <b>
                                                                                        <asp:Label ID="lblDept" runat="server" Text="Department"></asp:Label>:</b> &nbsp;
                                                         <asp:HiddenField ID="ddlDept_hid" runat="server" />
                                                                                    <asp:TextBox ID="txtdepartment" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Department" ServicePath="" TargetControlID="txtdepartment"
                                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETDEPT_ID">
                                                                                    </cc1:AutoCompleteExtender>
                                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtdepartment"
                                                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                                                </td>
                                                                                <td>EMPID:
                                                                                    <asp:TextBox ID="txtEmpID" Height="22px" Width="189px" runat="server" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>&nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <b>
                                                                                        <asp:Label ID="lblDesig" runat="server" Text="Designation"></asp:Label>:</b>
                                                                                    <%-- <asp:DropDownList ID="ddlDesif2" runat="server" CssClass="droplist">
                                                        </asp:DropDownList>--%>
                                                                                    <asp:HiddenField ID="ddlDesif2_hid" runat="server" />
                                                                                    <asp:TextBox ID="TxtDsg" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Desigination" ServicePath="" TargetControlID="TxtDsg"
                                                                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETDesg_ID">
                                                                                    </cc1:AutoCompleteExtender>
                                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="TxtDsg"
                                                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                                                </td>
                                                                                <td>
                                                                                    <b>
                                                                                        <asp:Label ID="lblNature" runat="server" Text="Emp Nature"></asp:Label>:</b>
                                                                                    <asp:DropDownList ID="ddlEmpNature" runat="server" CssClass="droplist">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <asp:Label ID="Label1" runat="server" Text="Special Days"></asp:Label>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlSpecialDays" runat="server" CssClass="droplist">
                                                                                    </asp:DropDownList></td>
                                                                                <td>
                                                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></td>
                                                                            </tr>
                                                                        </table>
                                                                    </Content>
                                                                </cc1:AccordionPane>
                                                            </Panes>
                                                        </cc1:Accordion>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="100%" id="tblgrid">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:GridView ID="grdEmpMessAtt" runat="server" CssClass="gridview" EmptyDataText="No Records Found"
                                                            AutoGenerateColumns="false" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="EmpID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("EmpId")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" onclick="SelectAll(this);" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkAll" runat="server" Checked='<%#Eval("Status")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="EmpName">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Designation">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Trade">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTrade" runat="server" Text='<%#Eval("Category")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblWS" runat="server" Text='<%#Eval("WS")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAll" runat="server" Text="Submit" CssClass="btn btn-success"
                                                            OnClientClick="javascript:return validateCheckBox();" OnClick="btnAll_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 17px">
                                                        <uc1:Paging ID="EmpMessAttPaging" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="DatesConfigvw" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>Name<span style="color: #ff0000">*</span> </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSpDayName" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>From Date<span style="color: #ff0000">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFromDate" runat="server" Width="80px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                                            PopupButtonID="txtDOB" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>To Date<span style="color: #ff0000">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtToDate" runat="server" Width="80px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                                            PopupButtonID="txtDOB" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Working Hours<span style="color: #ff0000">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtWHS" runat="server" Width="40px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" Width="100px"
                                                            OnClientClick="javascript:return Validate();" OnClick="btnSubmit_Click1" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:GridView ID="gvramzan" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                                OnRowCommand="gvWages_RowCommand" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSpDayName" runat="server" Text='<%#Eval("SpDayName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
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
                                                    <asp:TemplateField HeaderText="WHS" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWHS" runat="server" Text='<%#Eval("WorkingHrs")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="anchor__grd edit_grd" CommandArgument='<%#Eval("RID")%>'
                                                                CommandName="Edt"></asp:LinkButton>
                                                        </ItemTemplate>
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
                            </asp:View>
                        </asp:MultiView>
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
