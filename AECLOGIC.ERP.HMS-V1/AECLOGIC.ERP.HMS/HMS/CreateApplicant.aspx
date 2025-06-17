<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="CreateApplicant.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.CreateApplicant" MasterPageFile="~/Templates/CommonMaster.master" Title="Create Applicant"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <%--<head runat="server">
    <title>Create Applicant</title>
    <link href="Includes/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="Includes/CSS/sddm.css" rel="stylesheet" type="text/css" />
    <link href="Includes/CSS/base.css" rel="stylesheet" type="text/css" />
</head>--%>
    <script language="javascript" type="text/javascript">

        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }

        function Valid() {

            //test//

            //test//
            //  Job Applied For 
            if (!chkDropDownList('<%=ddlPosition.ClientID %>', 'Job Applied For'))
                return false;
            //  First Name
            if (!chkName('<%=txtFirstName.ClientID %>', 'First Name', true, ''))
                return false;
            // Middle Name
            if (!chkName('<%=txtMiddleName.ClientID %>', 'Middle Name', false, ''))
                return false;
            // Last Name
            if (!chkName('<%=txtSurnname.ClientID %>', 'Last Name', true, ''))
                return false;
            // EmailId
            if (!chkEmail('<%=txtEmailId.ClientID %>', 'Email Id', true, '[Enter Email ID]'))
                return false;
            //Addresss
            if (!chkAddress('<%=txtaddress.ClientID %>', 'Address', true, ''))
                return false;

            //City
            if (!chkName('<%=txtcity1.ClientID %>', 'City', true, ''))
                return false;
            //State
            if (!chkName('<%=txtstate.ClientID %>', 'State', true, ''))
                return false;
            //Country
            if (!chkName('<%=txtcountry.ClientID %>', 'Country', true, ''))
                return false;
            //PIN
            if (!chkPinCode('<%=txtpin.ClientID %>', 'PIN', true, ''))
                return false;
            //Mobile
            if (!chkMobile('<%=txtMobileNumber.ClientID %>', 'Mobile Number', true, ''))
                return false;
            //Home Number
            if (!chkPhoneOrMobile('<%=txtHomeNumber.ClientID %>', 'Home Number', false, ''))
                return false;
            //current Location
            if (!chkName('<%=txtcurrentlocation.ClientID %>', 'Current Location', false, ''))
                return false;
            //Date of Birth
            if (!chkDate('<%=txtDateofBirth.ClientID %>', 'Date of Birth', true, '', true))
                return false;
            //father Name
            if (!chkName('<%=txtFathersName.ClientID %>', 'Father Name', false, ''))
                return false;
            //Passport Number
            if (!chkAddress('<%=txtPassportNumber.ClientID %>', 'Passport Number', false, ''))
                return false;
            //Place Of Issue (passport)
            if (!chkName('<%=txtPlaceIssue.ClientID %>', 'Place Of Issue', false, ''))
                return false;
            // Passport Issue Date
            if (!chkDate('<%=txtDatePlaceIssue.ClientID %>', 'Passport Issue Date', false, '', true))
                return false;
            //Passport Expirey Dtae
            if (!chkDate('<%=txtDateOfExpiry.ClientID %>', 'Passport Expiry Date', false, '', false))
                return false;
            //Passport issue Date and Expiry Date validation
            if (!DateCompare('<%=txtDatePlaceIssue.ClientID %>', '<%=txtDateOfExpiry.ClientID %>'))
                return false;
            //CTC
            if (!chkNumber('<%=txtCurrentCTC.ClientID %>', 'Current CTC', true, ''))
                return false;
            //Exp CTC
            if (!chkNumber('<%=txtExpectedCTC.ClientID %>', 'Expected CTC', true, ''))
                return false;
            // file upload
            if (!checkFileLength('<%=fuResume.ClientID %>', 'Resume'))
                return false;
            return true;
        }

        function valid() {

            //Qualification
            if (!chkAddress('<%=txtQualification.ClientID %>', 'Qualification', true, ''))
                return false;
            //University
            if (!chkAddress('<%= txtCollegeInsUniv.ClientID %>', 'College/University', true, ''))
                return false;
            //for year of passing  
            if (!checkYear('<%=txtYearofPass.ClientID %>', 'Year of Passing', true, ''))
                return false;
            //Specialization
            if (!chkAddress('<%=txtSpecialization.ClientID %>', 'Specialization', true, ''))
                return false;
            //for percentage
            if (!chkPercentage('<%=txtPercentage.ClientID %>', 'Percentage', true, ''))
                return false;

            return true;

        }
        function VALID() {
            if (chkorg('<%=txtOrganization.ClientID %>')) {
                //Oraganisation
                if (!chkAddress('<%=txtOrganization.ClientID %>', 'Oraganisation', true, ''))
                    return false;
                //City
                if (!chkName('<%=txtCity.ClientID %>', 'City', true, ''))
                    return false;
                //from date
                if (!chkDate('<%=txtDurationFrom.ClientID %>', 'From Date ', true, '', true))
                    return false;
                // To Date
                if (!chkDate('<%=txtDurationTo.ClientID %>', 'To Date', true, '', true))
                    return false;
                //From Date and To Date validation
                if (!DateCompare('<%=txtDurationFrom.ClientID %>', '<%=txtDurationTo.ClientID %>'))
                    return false;
                //Designation
                if (!chkAddress('<%=txtDesignation.ClientID %>', 'Designation ', true, ''))
                    return false;
                //C.T.C
                if (!chkNumber('<%=txtCTC.ClientID %>', 'C.T.C', true, ''))
                    return false;
                return true;
            }
            else { return false; } return true;

        }

        //for date formate dd/MMM/yyyy

        function fDate(object) {
            var elm = getObj(object);
            var val = elm.value;
            alert(val);

            var Ary = val.split('/');
            var d, m, y;
            if (Ary[0].length == 1)
                d = "0" + Ary[0];
            if (Ary[1].length == 1)
                m = "0" + Ary[1];
            if (Ary[2].length == 3)
                y = Ary[2] + "0";
            if (Ary[2].length == 2)
                y = Ary[2] + "00";
            if (Ary[2].length == 1)
                y = Ary[2] + "000";
            //elm.value=d+'/'+m+'/'+y;

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
                if (!datechk(val, isPastDate))
                { elm.focus(); return false; } 
            } return true;
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
                    alert("Date should be less than the Today "); return false;
                }
                else if (actDate.getFullYear() < 1900) { alert("Date should be more than 1900"); return false; }

            } else {
                if (actDate < CurDate) {
                    alert("Date should be greater than the Today date"); return false;
                }
            }
            //if(eval(year)>eval(curYear) || flgDate==1){alert("please enter the date >(01/01/1900) and < current date!!!");return false;}
            return true;
        }

        //Is Leapyear
        function isLeap(year) {
            if ((year / 4) != Math.floor(year / 4)) return false;
            if ((year / 100) != Math.floor(year / 100)) return true;
            if ((year / 400) != Math.floor(year / 400)) return false;
            return true;
        }
        //DATE COMPARE
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



        //For PIN Code

        function chkPinCode(object, msg, isRequired, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (isRequired) {
                if (!Reval(object, msg, waterMark))
                { return false; } 
            }
            if (val != '') {
                var rx = new RegExp("\\d{6}");
                var matches = rx.exec(val);
                if (matches == null || val != matches[0]) {
                    alert("Invalid PIN! \n\nEnter 6 Digits valid PIN.");
                    //elm.value='';
                    elm.focus();
                    return false;
                } 
            } return true;
        }
        //For Percentage

        function chkPercentage(object, msg, isRequired, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (isRequired) {
                if (!Reval(object, msg, waterMark))
                { return false; } 
            }
            if (val != '') {
                var rx = new RegExp("\\d{2}");
                var matches = rx.exec(val);
                if (matches == null || val != matches[0]) {
                    alert("Invalid Percentage.Percentage should be in two digits !!!");
                    //elm.value='';
                    elm.focus();
                    return false;
                } 
            } return true;
        }



        //For Mobile

        function chkMobile(object, msg, isRequired, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (isRequired) {
                if (!Reval(object, msg, waterMark))
                { return false; } 
            }
            if (val != '') {
                var rx = new RegExp("\\d{10}");
                var matches = rx.exec(val);
                if (matches == null || val != matches[0]) {
                    alert("Invalid Mobile Number! \n\nEnter 10 digits valid Mobile Number.");
                    //elm.value='';
                    elm.focus();
                    return false;
                } 
            } return true;
        }

        //For Phone /Mobile

        function chkPhoneOrMobile(object, msg, isRequired, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (isRequired) {
                if (!Reval(object, msg, waterMark))
                { return false; } 
            }
            if (val != '') {
                var rx = new RegExp("\\d{10,11}");
                var matches = rx.exec(val);
                if (matches == null || val != matches[0]) {
                    alert("Invalid Phone\Mobile Number! \n\nEnter 11 digits Phone number.\nOr\nEnter 10 digits Mobile number");
                    //elm.value='';
                    elm.focus();
                    return false;
                } 
            } return true;
        }

        //Email Address	
        function chkEmail(object, msg, isRequired, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (isRequired) {
                if (!Reval(object, msg, waterMark))
                { return false; } 
            }
            if (val != '') {
                var rx = new RegExp("\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
                var matches = rx.exec(val);
                if (matches == null || val != matches[0]) {
                    alert("Invalid " + msg + "!!!"); //elm.value='';
                    elm.focus();
                    return false;
                } 
            } return true;
        }

        //For First Name, Last Name and Middle Name

        function chkName(object, msg, isRequired, waterMark) {
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
                    elm.value = '';
                    elm.focus();
                    return false;
                }
            } return true;
        }

        //For Address, Street, Town

        function chkAddress(object, msg, IsRequired, waterMark) {
            var elm = getObj(object);
            var val = Trim(elm.value);
            if (IsRequired) {
                if (!Reval(object, msg, waterMark))
                { return false; } 
            }
            //    if(val!='')
            //    {
            //   	var rx=new RegExp("[a-z A-Z 0-9  , - / ]*");
            //	var matches=rx.exec(val);
            //	if (matches==null || val != matches[0])
            //	{alert(msg + " can take alphabets only!!!" );
            //		//elm.value='';
            //		elm.focus();
            //		return false;}}
            return true;
        }
        // for year check
        function checkYear(object, msg, isRequired, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (isRequired) {
                if (!Reval(object, msg, waterMark))
                { return false; } 
            }
            if (val != '') {
                var rx = new RegExp("\\d{4}");
                var matches = rx.exec(val);
                if (matches == null || val != matches[0]) {
                    alert(msg + " should be valid !!!");
                    //elm.value='';
                    elm.focus();
                    return false;
                }
                if (!yearchk(val))
                { elm.focus(); return false; } 
            } return true;

        }
        // year
        function yearchk(year) {
            var actDate = new Date();
            actDate.setFullYear(year);
            var CurDate = new Date();
            CurDate.setFullYear('<% =DateTime.Now.Year %>');

            if (CurDate < actDate) {
                alert("Year should be less than the Today "); return false;
            }
            else if (actDate.getFullYear() < 1900) { alert("Year should be more than 1900"); return false; }



            return true;
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

        //geting the object
        function getObj(the_id) {
            if (typeof (the_id) == "object") {
                return the_id;
            }
            if (typeof document.getElementById != 'undefined') {
                return document.getElementById(the_id);
            }
            else if (typeof document.all != 'undefined') {
                return document.all[the_id];
            }
            else if (typeof document.layers != 'undefined') {
                return document.layers[the_id];
            }
            else {
                return null;
            }
        }
        //Required Validation
        function Reval(object, msg, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (val == '' || val.length == 0 || val == waterMark) {
                alert(msg + " should not be empty!!! ");
                //elm.value = waterMark;
                elm.focus();
                return false;
            }
            return true;
        }
        //for checking org
        function chkorg(object) {
            var elm = getObj(object);
            var val = elm.value;
            if (val == '' || val.length == 0) {

                return false;
            }
            return true;
        }


        function Trim(str) {
            return str.replace(/^\s*|\s*$/g, "");
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

        function checkFileLength(object, msg) {
            var elm = getObj(object);


            var filePath = elm.value;

            if (filePath == "") {
                alert("Please select file to upload.");
                elm.focus();
                return false;
            }
            else {
                if (filePath.indexOf('.') == -1)
                    return false;

                var validExtensions = new Array();
                var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();

                validExtensions[0] = 'doc';
                validExtensions[1] = 'docx';
                validExtensions[2] = 'rtf';
                validExtensions[3] = 'txt';
                validExtensions[4] = 'tiff';
                validExtensions[5] = 'tif';
                validExtensions[6] = 'mdf';
                validExtensions[7] = 'pdf';

                var count = 0;
                for (var i = 0; i < validExtensions.length; i++)
                {
                    if (ext == validExtensions[i])
                        count = count + 1;
                      
                }
                if (count > 0) {
                    return true;
                }
                else {
                    alert('Invalid file format only Accept(doc,docx,rtf,txt,tiff,tif,mdf,pdf).');
                    return false;
                }

               
                var who = obj;

                who.value = ""; var who2 = who.cloneNode(false);
                who2.onchange = who.onchange;

                who.parentNode.replaceChild(who2, who);

                return false;
            } return true;
        }    



    </script>
    <%-- <ajax:ScriptManager ID="ScriptManager1" runat="server">
        </ajax:ScriptManager>--%>
    
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
    <table width="100%">
       
        
              
                <tr>
                    <td>
                        <div>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" class="pageheader">
                                        Personal Details:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Job Applied For(Postion):
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPosition" runat="server" Width="252px" 
                                            CssClass="droplist" TabIndex="1">
                                        </asp:DropDownList>
                                        <span style="color: #ff0000">*</span>
                                        <cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search..." PromptPosition="Top" 
                                     PromptCssClass="PromptText" QueryPattern="Contains" runat="server" TargetControlID="ddlPosition"></cc1:ListSearchExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        First Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName" runat="server" TabIndex="2"></asp:TextBox><span style="color: #ff0000">*</span>Maximum
                                        50 characters
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Middle Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMiddleName" runat="server" TabIndex="3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Last Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSurnname" runat="server" TabIndex="4"></asp:TextBox><span style="color: #ff0000">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Gender
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rdblstgender" runat="server" 
                                            RepeatDirection="Horizontal" TabIndex="5">
                                            <asp:ListItem Selected="True" Text="Male" Value="Male"></asp:ListItem>
                                            <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email Id
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmailId"  runat="server" 
                                            OnTextChanged="txtEmailId_TextChanged" TabIndex="6"></asp:TextBox><span
                                            style="color: #ff0000">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 57px">
                                        Address
                                    </td>
                                    <td style="height: 57px">
                                        <asp:TextBox ID="txtaddress" runat="server" TextMode="multiline" Height="50px" Width="250px"
                                            MaxLength="500" TabIndex="7"></asp:TextBox>
                                        <span style="color: #ff0000">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        City
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtcity1" Text="<%$AppSettings:City%>" runat="server" 
                                            MaxLength="50" TabIndex="8"></asp:TextBox><span
                                            style="color: #ff0000">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        State
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtstate" Text="<%$AppSettings:State%>" runat="server" 
                                            MaxLength="50" TabIndex="9"></asp:TextBox><span
                                            style="color: #ff0000">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Country
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtcountry" Text="<%$AppSettings:Country%>" runat="server" MaxLength="50"
                                            Height="18px" TabIndex="10"></asp:TextBox><span style="color: #ff0000">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Pin
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtpin" runat="server" MaxLength="10" TabIndex="11"></asp:TextBox><span style="color: #ff0000">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Mobile Number
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMobileNumber"  runat="server"  onkeypress="javascript:return isNumber(event)" TabIndex="12" ></asp:TextBox><span style="color: #ff0000">*</span>Ex:
                                        9440024365
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Home Number
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHomeNumber" onkeypress="javascript:return isNumber(event)" runat="server" TabIndex="13"></asp:TextBox>*&nbsp; Fill either
                                        Land number or Mobile number.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Current Location
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtcurrentlocation" runat="server" MaxLength="50" 
                                            TabIndex="14"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date of Birth
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateofBirth" runat="server" TabIndex="15"></asp:TextBox><span style="color: #ff0000">*</span>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtDateofBirth"
                                            TargetControlID="txtDateofBirth" Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Father's Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFathersName" runat="server" TabIndex="16"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Passport Number
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPassportNumber" runat="server" TabIndex="17"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Place of Issue
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPlaceIssue" runat="server" TabIndex="18"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Issued Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDatePlaceIssue" runat="server" TabIndex="19"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtDatePlaceIssue"
                                            TargetControlID="txtDatePlaceIssue" Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date of Expiry
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateOfExpiry" runat="server" TabIndex="20"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtDateOfExpiry"
                                            TargetControlID="txtDateOfExpiry" Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Marital Status
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMatrialStatus" CssClass="droplist" runat="server" 
                                            TabIndex="21">
                                            <asp:ListItem Selected="True" Value="0">Single</asp:ListItem>
                                            <asp:ListItem Value="1">Married</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Current CTC:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCurrentCTC" runat="server" TabIndex="22"></asp:TextBox><span style="color: #ff0000">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Expected CTC:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExpectedCTC" runat="server" TabIndex="23"></asp:TextBox><span style="color: #ff0000">*</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Upload Resume:
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="fuResume" runat="server" TabIndex="24" /><span style="color: #ff0000">*</span>Note:
                                        Please Enclose your Resume
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        &nbsp;
                                    </td>
                                    <td>
                                      
                                        <asp:Button ID="btnSave" Text="Submit" runat="server" CssClass="btn btn-success" Width="100px"
                                            OnClientClick="javascript:return Valid();" OnClick="btnSave_Click" 
                                            TabIndex="25" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="dvAcademic" runat="server" visible="false" width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2" style="height: 10px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="pageheader">
                                                        Educational Qualification Details:
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="height: 20px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="width: 100%">
                                                        <table border="0"  style="border-width:1px;border-radius:20px;border-color:#0094ff" width="100%">
                                                            <tr>
                                                                <td style="width: 250px; height: 20px">
                                                                    Qualification
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtQualification" runat="server" Width="116px" TabIndex="26"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 250px; height: 20px">
                                                                    College/Institute &amp; University
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCollegeInsUniv" runat="server" Width="116px" TabIndex="27"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 250px; height: 20px">
                                                                    Year of Passing
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtYearofPass" runat="server" Width="116px" MaxLength="4" 
                                                                        TabIndex="28"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 250px; height: 20px">
                                                                    Specialization
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSpecialization" runat="server" Width="116px" TabIndex="29"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 250px; height: 20px">
                                                                    Percentage (%)
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPercentage" runat="server" Width="116px" TabIndex="30"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 250px; height: 20px">
                                                                    Education Type
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlFullPartTime" CssClass="droplist" runat="server" 
                                                                        Width="120px" TabIndex="31">
                                                                        <asp:ListItem Selected="True" Value="1">Full Time</asp:ListItem>
                                                                        <asp:ListItem Value="2">Part Time</asp:ListItem>
                                                                        <asp:ListItem Value="3">Correspondence</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                               
                                                                <td colspan ="2" style="border-top :1px;border-radius:20px;border-top-color :#0094ff">
                                                                    <asp:Button ID="btnAddAcademicDetails" runat="server" Text="Add" OnClick="btnAddAcademicDetails_Click"
                                                                        OnClientClick="javascript:return valid();" CssClass="btn btn-success" 
                                                                        Width="100px" TabIndex="32" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid ID="dgvQualDetails" runat="server" AutoGenerateColumns="False" DataKeyField="AppEduID"
                                                            OnItemCommand="dgvQualDetails_ItemCommand" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                                            <Columns>
                                                                <asp:BoundColumn DataField="Qualification" HeaderText="Qualification">
                                                                    <ItemStyle Width="200px" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="Institute" HeaderText="Institute">
                                                                    <ItemStyle Width="250px" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="YOP" HeaderText="YOP">
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="Specialization" HeaderText="Specialization">
                                                                    <ItemStyle Width="200px" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="Percentage" HeaderText="Percentage">
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="Mode" HeaderText="Mode">
                                                                    <ItemStyle Width="140px" />
                                                                </asp:BoundColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 100%">
                                        <div id="dvEmployment" runat="server" visible="false">
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2" style="height: 20px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="height: 30px" class="pageheader">
                                                        Professional Details:
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="height: 20px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <table border="0"  style="border-width:1px;border-radius:20px;border-color:#0094ff" width="100%">
                                                            <tr>
                                                                <td style="width: 250px; height: 20px">
                                                                    Organization
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOrganization" runat="server" Width="261px" TabIndex="33"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 250px; height: 20px">
                                                                    City
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCity" runat="server" Width="187px" TabIndex="34"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 250px; height: 20px">
                                                                    Permanent/Contract
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlPermanent" runat="server" CssClass="droplist" 
                                                                          TabIndex="35">
                                                                        <asp:ListItem Selected="True" Value="1">Permanent</asp:ListItem>
                                                                        <asp:ListItem Value="2">Contract</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 250px; height: 20px">
                                                                    From Date
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDurationFrom" runat="server" Width="116px" TabIndex="36"></asp:TextBox>
                                                                     <cc1:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="txtDurationFrom"
                                                                        TargetControlID="txtDurationFrom" Format="dd/MM/yyyy">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 250px; height: 20px">
                                                                    To Date
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDurationTo" runat="server" Width="116px" TabIndex="37"></asp:TextBox>
                                                                     <cc1:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="txtDurationTo"
                                                                        TargetControlID="txtDurationTo" Format="dd/MM/yyyy">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 250px; height: 20px">
                                                                    Designation
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDesignation" runat="server" Width="204px" TabIndex="38"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 250px; height: 20px">
                                                                    CTC(p.a)
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCTC" runat="server" Width="116px" TabIndex="39"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                               
                                                                <td  colspan="2" style="border-top :1px;border-radius:20px;border-top-color :#0094ff" >
                                                                    <asp:Button ID="btnAddEmp" runat="server" Text="Add" OnClick="btnAddEmp_Click" OnClientClick="javascript:return VALID();"
                                                                        CssClass="btn btn-success" Width="100px" TabIndex="40" />
                                                                </td>
                                                               
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:DataGrid ID="dgvPreviousEmpHist" runat="server" AutoGenerateColumns="False"
                                                            DataKeyField="AppExpID" OnItemCommand="dgvPreviousEmpHist_ItemCommand" HeaderStyle-CssClass="tableHead">
                                                            <Columns>
                                                                <asp:BoundColumn DataField="Organization" HeaderText="Organization">
                                                                    <ItemStyle Width="200px" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="City" HeaderText="City">
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="Type" HeaderText="Type">
                                                                    <ItemStyle Width="120px" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="FromDate" HeaderText="FromDate">
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="ToDate" HeaderText="ToDate">
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="Designation" HeaderText="Designation">
                                                                    <ItemStyle Width="200px" />
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="CTC" HeaderText="CTC">
                                                                    <ItemStyle Width="100px" />
                                                                </asp:BoundColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        <asp:Button ID="btnFinish" runat="server" Text="Finish" OnClick="btnFinish_Click"
                                                            CssClass="btn btn-prmary" Width="100px" TabIndex="41" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
               
              
    </table>
</ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
