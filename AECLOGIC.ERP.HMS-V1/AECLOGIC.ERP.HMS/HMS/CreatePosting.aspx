<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="CreatePosting.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.CreatePosting" Title="Create Posting" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script type="text/javascript" language="javascript">

        function AddNewItem() {
            retval = window.showModalDialog("AddInterviewType.aspx", "", "dialogheight:500px;dialogwidth:400px;status:no;edge:sunken;unadorned:no;resizable:no;");
            if (retval == 1) {
                window.location.href = "CreatePosting.aspx";
            }
            else {
                return false;
            }
        }
        function Valid() {
            //for Position
            <%--     if (!chkPosition('<%=txtPosition.ClientID %>', 'position', true, ''))
                return false;--%>
            if (document.getElementById('<%=txtPosition.ClientID%>').value == "") {
                alert("Please Enter Position");
                document.getElementById('<%=txtPosition.ClientID%>').focus();
                return false;
            }
            if (!chkDropDownList('<%= ddlDesig.ClientID%>', 'Designation'))
                return false;
            if (!chkDropDownList('<%= ddlCategory.ClientID%>', 'Trade'))
                return false;
            if (!chkDropDownList('<%= ddlDesig.ClientID%>', 'Designation'))
                return false;
            if (!chkDropDownList('<%= ddlWS.ClientID%>', 'Worksite'))
                return false;
            //For Department
            if (!chkDropDownList('<%= ddldepartment.ClientID%>', 'Department'))
                return false;
            if (!chkFloatNumber('<%= txtFrm.ClientID%>', 'From experience', true, ''))
                return false;
            //            if (!chkFloatNumber('<%= txtTo.ClientID%>', 'To experience', true, ''))
            //                return false;    
            //for Description
            if (!Reval('<%=txtdescription.ClientID %>', 'Description', ''))
                return false;
            //No. Of Post
            if (!chkNumber('<%=txtposts.ClientID %>', 'No Of Post', true, ''))
                return false;
            //from Date
            if (!chkDate('<%=txtFromDate.ClientID %>', 'From Date', true, '', false))
                return false;
            //to date
            if (!chkDate('<%=txtToDate.ClientID %>', 'To date', true, '', false))
                return false;
            //From Date and To Date validation
            if (!DateCompare('<%=txtFromDate.ClientID %>', '<%=txtToDate.ClientID %>'))
                return false;

            //Qualification
            if (!Reval('<%=txtQualifications.ClientID %>', 'Qualification', ''))
                return false;

            //Interview Type
            if (!chkDropDownList('<%=ddlinterviewtype.ClientID %>', 'Interview Type'))
                return false;


            //alert("sucess");
            //return false;

        }


        //For DropDownList
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
            } else if (typeof document.all != 'undefined') {
                return document.all[the_id];
            } else if (typeof document.layers != 'undefined') {
                return document.layers[the_id];
            } else {
                return null;
            }
        }
        function chkPosition(object, msg, isRequired, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (isRequired) {
                if (!Reval(object, msg, waterMark))
                { return false; }
            }
            if (val != '') {
                var rx = new RegExp("[a-z A-Z]*");
                var matches = rx.exec(val);
                if (matches == null || val != matches[0]) {
                    alert(msg + " can take alphabets only!!!");
                    //elm.value='';
                    elm.focus();
                    return false;
                }
            } return true;
        }

        //Required Validation
        function Reval(object, msg, waterMark) {
            var elm = getObj(object);
            var val = Trim(elm.value);
            if (val == '' || val.length == 0 || val == waterMark) {
                alert(msg + " should not be empty!!! ");
                //elm.value = waterMark;
                elm.focus();
                return false;
            }
            return true;
        }
        function Trim(str) {
            return str.replace(/^\s*|\s*$/g, "");
        }
        //Date of Birth
        function chkDate(object, msg, isRequired, waterMark, isPastDate) {
            var elm = getObj(object);
            var val = elm.value;

            if (isRequired) {
                if (!Reval(object, msg, waterMark))
                { return false; }
            }
            if (val != '') {
                var rx = new RegExp("^\\s*(\\d{2})([/])(\\d{2})([/])(\\d{4})\\s*$");
                var matches = rx.exec(val);

                if (matches == null || val != matches[0]) {
                    alert("Invalid Date,The required format is(dd/MMM/yyyy)!!! ");
                    elm.focus(); return false;
                }
                if (!datechk(val, isPastDate)) {
                    elm.focus(); return false;
                }
            }
            return true;
        }
        //Date Checking


        function datechk(datevalue, isPastDate) {
            var year = datevalue.substring(6, datevalue.length);
            var month = datevalue.substring(3, 5); var day = datevalue.substring(0, 2);
            var mChk = 0; var dChk = 0; var curDate = new Date(); var curYear = curDate.getFullYear();
            var curMonth = curDate.getMonth(); var curDay = curDate.getDate();
            //checking for month
            if (eval(month) > 12 || eval(month) < 1) {
                alert("Invalid Month,The required format is(dd/MMM/yyyy) !!!  \n Eg: Date: 01/01/2009 ");
                return false;
            }
            //checking for day
            if (eval(month) == 01 || eval(month) == 03 || eval(month) == 05 || eval(month) == 07 || eval(month) == 08 || eval(month) == 10 || eval(month) == 12) {
                if (eval(day) > 31 || eval(day) < 1) {
                    alert(" Invalid Day,The required format is(dd/MMM/yyyy) !!!  \n Eg: Date: 01/01/2009  ");
                    return false;
                }
            }
            else {
                if (eval(month) == 02) {
                    if (isLeap(year)) {
                        if (eval(day) > 29 || eval(day) < 1)
                        { alert(" Invalid Day,The required format is(dd/MMM/yyyy) !!!  \n Eg: Date: 01/01/2009 "); return false; }
                    }
                    else {
                        if (eval(day) > 28 || eval(day) < 1)
                        { alert(" Invalid Day,The required format is(dd/MMM/yyyy) !!! \n Eg: Date: 01/01/2009  "); return false; }
                    }
                }
                else { if (eval(day) > 30 || eval(day) < 1) { alert(" Invalid Day,The required format is(dd/MMM/yyyy) !!!  \n Eg: Date: 01/01/2009 "); return false; } }
            }
            // checking for Year
            var flgDate = 0;
            //Same year
            curMonth = curDate.getMonth();
            if (eval(year) == eval(curYear)) {
                if (eval(month) > eval(curMonth) + 1)
                { flgDate = 1; }
                if (eval(month) == eval(curMonth) + 1) {
                    if (eval(day) > eval(curDay))
                    { flgDate = 1; }
                }
            }

            var actDate = new Date();
            actDate.setFullYear(year, (month - 1), day);
            var CurDate = new Date();
            CurDate.setFullYear('<% =DateTime.Now.Year %>', (eval('<% =DateTime.Now.Month %>') - 1), '<% =DateTime.Now.Day %>');
            if (isPastDate) {
                if (CurDate <= actDate) {
                    alert("Date should be lessthan the Today date"); return false;
                }
                else if (actDate.getFullYear() < 1900) { alert("Date should be more than 1900"); return false; }
            } else {
                if (actDate < CurDate) {
                    alert("Date should be greaterthan the Today date"); return false;
                }
            }
            //if(eval(year)>eval(curYear) || flgDate==1){alert("please enter the date >(01/01/1900) and < current date!!!");return false;}
            return true;
        }


        function DateCompare(From, To) {
            var elmFrom = getObj(From);
            var elmTo = getObj(To);
            var datevalue = elmFrom.value;
            var year = datevalue.substring(6, datevalue.length);
            var month = datevalue.substring(3, 5); var day = datevalue.substring(0, 2);
            var FromDate = new Date();
            FromDate.setFullYear(year, (month - 1), day);

            datevalue = elmTo.value;
            year = datevalue.substring(6, datevalue.length);
            month = datevalue.substring(3, 5); day = datevalue.substring(0, 2);
            var ToDate = new Date();
            ToDate.setFullYear(year, (month - 1), day);
            if (ToDate < FromDate) {
                alert("‘From Date’ should be lessthan or equal to the ‘To Date’"); return false;
            }

            return true;
        }
        //Is Leapyear
        function isLeap(year) {
            if ((year / 4) != Math.floor(year / 4)) return false;
            if ((year / 100) != Math.floor(year / 100)) return true;
            if ((year / 400) != Math.floor(year / 400)) return false;
            return true;
        }
        //Check Number
        function chkNumber(object, msg, isRequired, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (isRequired) {
                if (!Reval(object, msg, waterMark)) {
                    return false;
                }
            }
            if (val != '') {
                var rx = new RegExp("[0-9]*");
                var matches = rx.exec(val);
                if (matches == null || val != matches[0]) {
                    alert(msg + " can take numbers only!!!");
                    elm.value = '';
                    elm.focus();
                    return false;
                }
            }
            return true;
        }

    </script>
    <table cellpadding="2" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="2">
                <AEC:Topmenu ID="topmenu" runat="server" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlCreatePostiong" runat="server" CssClass="DivBorderOlive">
        <table cellpadding="2" cellspacing="0" border="0" width="100%">
            <tr>
                <td style="width: 250px">
                    Position Available <span style="color: #ff0000">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtPosition" runat="server" MaxLength="50" Width="475px" TabIndex="1"></asp:TextBox>
                
                </td>
            </tr>
            <tr>
                <td >
                    Designation<span style="color: #ff0000">*</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDesig" runat="server" CssClass="droplist" TabIndex="2">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td >
                    <asp:Label ID="lblTrade" runat="server" Text="Trades(Expertise)"></asp:Label><span
                        style="color: #ff0000">*</span> :
                </td>
                <td >
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="droplist" TabIndex="3"
                        AccessKey="3" ToolTip="[Alt+3]">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 250px">
                    Worksite<span style="color: #ff0000">*</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlWS" runat="server" CssClass="droplist" TabIndex="2" 
                        Height="17px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 250px">
                    Department<span style="color: #ff0000">*</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddldepartment" runat="server" CssClass="droplist" TabIndex="2">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 250px">
                    Experience<span style="color: #ff0000">*</span>
                </td>
                <td>
                    <asp:Label ID="lblFrm" runat="server" Text="From"></asp:Label><span style="color: #ff0000">*</span>:
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txtFrm" runat="server" Width="40px" MaxLength="4"></asp:TextBox>&nbsp;&nbsp;
                    <cc1:FilteredTextBoxExtender ID="txtExpFrm" runat="server" FilterType="Custom,Numbers"
                        TargetControlID="txtFrm" ValidChars="." />
                    <asp:Label ID="lblTo" runat="server" Text="To"></asp:Label>
                    : &nbsp;&nbsp;
                    <asp:TextBox ID="txtTo" runat="server" Width="40px" MaxLength="4"></asp:TextBox>&nbsp;&nbsp;
                    <cc1:FilteredTextBoxExtender ID="txtExpTo" runat="server" FilterType="Custom,Numbers"
                        TargetControlID="txtTo" ValidChars="." />
                    <asp:Label ID="lblExexp" runat="server" Text="[Ex: From: 2.0 To: 4.5]"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 250px" valign="top">
                    Job Description (max 400 chars)
                </td>
                <td>
                    <asp:TextBox ID="txtdescription" runat="server" MaxLength="200" Height="75px" TextMode="MultiLine"
                        Width="700px" TabIndex="3"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtdescription"
                        WatermarkCssClass="watermark" WatermarkText="[Enter Job Description Ex:Account Manager With 2-3 Years of Experience In Construction Industry]">
                    </cc1:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 250px">
                    Number of Posts<span style="color: #ff0000">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtposts" runat="server" Width="47px" TabIndex="4"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 250px">
                    Recruitments Opens From<span style="color: #ff0000">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" Width="75px" TabIndex="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 250px">
                    Recruitments Closes On<span style="color: #ff0000">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" Width="75px" TabIndex="6"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 250px">
                    Timings
                </td>
                <td>
                    <asp:TextBox ID="txttimings" runat="server" MaxLength="50" TabIndex="7"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 250px">
                    Qualification
                </td>
                <td>
                    <asp:TextBox ID="txtQualifications" runat="server" MaxLength="200" Width="700px"
                        TabIndex="8"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtQualifications"
                        WatermarkCssClass="watermark" WatermarkText="[Enter Qualification Ex: M.B.A/M.Com With 2-3 Years of Experience ]">
                    </cc1:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 250px">
                    InterView Type<span style="color: #ff0000">*</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlinterviewtype" CssClass="droplist" runat="server" Width="165px"
                        TabIndex="9">
                    </asp:DropDownList>
                    <asp:LinkButton ID="lnkAdd" runat="server" Font-Bold="True" OnClick="lnkAdd_Click"
                        TabIndex="10" CssClass="btn btn-primary">Add New</asp:LinkButton>
                    <asp:Button ID="Button1" runat="server" Font-Bold="False" ForeColor="#CC6600" OnClick="Button1_Click1"
                        Text="Refresh" Visible="true" TabIndex="11" CssClass="btn btn-primary" />
                </td>
            </tr>
            <tr>
                <td style="width: 250px">
                    Postition (If Checked Position Is Open)
                </td>
                <td>
                    <asp:CheckBox ID="rbClosed" runat="server" Checked="False" Text="Status" TabIndex="12" />
                  
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 250px">
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                        CssClass="btn btn-success" OnClientClick="javascript:return Valid();" Width="100px"
                        AccessKey="s" TabIndex="13" ToolTip="[Alt+s OR Alt+s+Enter]" />
                    <asp:Button ID="SubmitNew" runat="server" CssClass="btn btn-success" 
                        OnClientClick="javascript:return Valid();" Text="Submit" Visible="False" Width="100px"
                        TabIndex="14" ToolTip="[Alt+s OR Alt+s+Enter]" />
                    <asp:Button ID="btnCancel" runat="server" Text="Reset" OnClick="btnCancel_Click"
                        CssClass="btn btn-danger" Width="100px" AccessKey="b" TabIndex="15" ToolTip="[Alt+b OR Alt+b+Enter]" />
                </td>
            </tr>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtFromDate"
                TargetControlID="txtFromDate" Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtToDate"
                Format="dd/MM/yyyy" TargetControlID="txtToDate">
            </cc1:CalendarExtender>
        </table>
    </asp:Panel>
</asp:Content>
