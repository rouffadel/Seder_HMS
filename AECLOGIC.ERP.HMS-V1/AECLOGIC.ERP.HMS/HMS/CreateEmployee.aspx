<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="CreateEmployee.aspx.cs" MaintainScrollPositionOnPostback="true"
    Inherits="AECLOGIC.ERP.HMS.CreateEmployee" Title="Create Employee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceholder1" runat="Server">

    <script language="javascript" type="text/javascript">
	$(document).ready(function () {
	
		var field = 'Id';
		var url = window.location.href;
		if(url.indexOf('?' + field + '=') != -1)
		{
		 document.getElementById('<%=chkPWD.ClientID%>').disabled = true;
		 
    		 document.getElementById('<%=ddlWorksite.ClientID%>').disabled = true;
		}
		else 
		{
    		  document.getElementById('<%=ddlWorksite.ClientID%>').disabled = false;
		}
           	 if (<%=Server.HtmlEncode(Session["UserId"].ToString())%>==1) 
		  {
 			document.getElementById('<%=ddlWorksite.ClientID%>').disabled = false;
                        document.getElementById('<%=chkPWD.ClientID%>').disabled = false;
		  }
            });

	

        function OpenPopup() {

            window.open("NewState.aspx", "List", "toolbar=no, location=no,status=yes,menubar=no,scrollbars=yes,resizable=no, width=400,height=300,left=430,top=100");
            return false;
        }
        function OneTextToother() {
            var chkPerAddress = document.getElementById('<%=chkAddress.ClientID%>')
            if (chkPerAddress.checked) {

              <%--  var ddlComuCoun = document.getElementById('<%=ddlComuCoun.ClientID%>')
                var ddlComuPerCou = document.getElementById('<%=ddlComuPerCou.ClientID%>')
                ddlComuPerCou.value = ddlComuCoun.value;

                var ddlComuState = document.getElementById('<%=ddlComuState.ClientID%>')
                var ddlComuPerState = document.getElementById('<%=ddlComuPerState.ClientID%>')
                ddlComuPerState.value = ddlComuState.value;

                var ddlComuCity = document.getElementById('<%=ddlComuCity.ClientID%>')
                var ddlComuPerCity = document.getElementById('<%=ddlComuPerCity.ClientID%>')
                ddlComuPerCity.value = ddlComuCity.value;--%>

                var naddress = document.getElementById('<%=txtAddress.ClientID%>')
                var nPeraddress = document.getElementById('<%=txtPer_Address.ClientID%>')
                nPeraddress.value = naddress.value;

                var nPin = document.getElementById('<%=txtPIN.ClientID%>')
                var nPerPin = document.getElementById('<%=txtPer_PIN.ClientID%>')
                nPerPin.value = nPin.value;

                var nPhone = document.getElementById('<%=txtPhone.ClientID%>')
                var nPerPhone = document.getElementById('<%=txtPer_Phone.ClientID%>')
                nPerPhone.value = nPhone.value;

                var nPerDoor = document.getElementById('<%=txtPerDoor.ClientID%>')
                var nResDoor = document.getElementById('<%=txtResDoor.ClientID%>')
                nResDoor.value = nPerDoor.value;

                var nPerBuild = document.getElementById('<%=txtPerBuilding.ClientID%>')
                var nResBuild = document.getElementById('<%=txtResBuilding.ClientID%>')
                nResBuild.value = nPerBuild.value;

                var nPerStreet = document.getElementById('<%=txtPerStreet.ClientID%>')
                var nResStreet = document.getElementById('<%=txtResStreet.ClientID%>')
                nResStreet.value = nPerStreet.value;

                var nPerArea = document.getElementById('<%=txtPerArea.ClientID%>')
                var nResArea = document.getElementById('<%=txtResArea.ClientID%>')
                nResArea.value = nPerArea.value;

            }
        }
        function OnlyNumeric(evt) {

            var chCode = evt.keyCode ? evt.keyCode : evt.charCode ? evt.charCode : evt.which;

            if (chCode >= 48 && chCode <= 57 ||
             chCode == 46) {
                return true;
            }
            else
                return false;
        }


        function chkPWD_CheckedChanged_js() {

            var name = '<%=ViewState["UserName"]%>';
            if (name == null || name == '')
                document.getElementById('<%=txtUsername.ClientID%>').value = document.getElementById('<%=txtName.ClientID%>').value;
        else
            document.getElementById('<%=txtUsername.ClientID%>').value = name;
        if (document.getElementById('<%=chkPWD.ClientID%>').checked == true) {
                //$('#txtUsername').attr("disabled", false);  
                document.getElementById('txtPassword').disabled = false;

                //      document.getElementById('<%=txtUsername.ClientID%>').disabled=false ;
         //      document.getElementById('<%=txtPassword.ClientID%>').disabled=false ;
         //      document.getElementById('<%=txtReenterPassword.ClientID%>').disabled=false ;
         //      document.getElementById('<%=lblUserAvailable.ClientID%>').disabled=false ;
         //      document.getElementById('<%=btnSubmit.ClientID%>').disabled=false;
         //  
     }
     else {
         document.getElementById('txtPassword').readOnly = false;
         //         document.getElementById('<%=txtUsername.ClientID%>').disabled=true ;
         //      document.getElementById('<%=txtPassword.ClientID%>').disabled=true ;
         //      document.getElementById('<%=txtReenterPassword.ClientID%>').disabled=true ;
         //      document.getElementById('<%=lblUserAvailable.ClientID%>').disabled=true ;
         //      document.getElementById('<%=btnSubmit.ClientID%>').disabled=true;
     }
        }

        function chkCTH_SelectedIndexChanged() {
            if (document.getElementById('<%= chkCTH.ClientID %>').checked)
                document.getElementById('<%= pnlCTH.ClientID %>').style.display = '';
            else
                document.getElementById('<%= pnlCTH.ClientID %>').style.display = 'none';
        }

        function ddlDependies_SelectedIndexChanged_j() {
            var valddl = document.getElementById('<%=ddlDependies.ClientID%>').value;
            if (valddl == "0") {
                document.getElementById("trDepdAge").style.display = 'none';
                document.getElementById("trDepdBGrp").style.display = 'none';
                document.getElementById("trDepdGender").style.display = 'none';
                document.getElementById("trDepdName").style.display = 'none';
            }
            else {
                document.getElementById("trDepdAge").style.display = '';
                document.getElementById("trDepdBGrp").style.display = '';
                document.getElementById("trDepdGender").style.display = '';
                document.getElementById("trDepdName").style.display = '';
                if (valddl == 1 || valddl == 4)
                    document.getElementById('<%=ddlDepGender.ClientID%>').value = "1";
            else
                document.getElementById('<%=ddlDepGender.ClientID%>').value = "0";
        }
    }
    function ddlPaymentMode_SelectedIndexChanged_js() {
        var valddl = document.getElementById('<%=ddlPaymentMode.ClientID%>').value;
     if (valddl == "2")
         document.getElementById("trBankDts").style.display = '';
     else
         document.getElementById("trBankDts").style.display = 'none';
 }
 function ValidBank() {
     //For Bank Name
     if (document.getElementById('<%=txtNewBankName.ClientID%>').value == "") {
          alert("Enter Bank Branch ");
          document.getElementById('<%=txtNewBankName.ClientID%>').focus();
                return false;
            }
        }
        function ValidAddCity() {
            if (!chkDropDownList('<%=ddlCityCountry.ClientID%>', "Country")) {
                return false;
            }
            if (!chkDropDownList('<%=ddlCityState.ClientID%>', "State")) {
                return false;
            }
            if (!chkName('<%=txtCityName.ClientID%>', "City", true, "")) {
                return false;
            }
        }
        function ValidAddState() {
            if (!chkDropDownList('<%=ddlStateCountry.ClientID%>', "Country")) {
                return false;
            }
            if (!chkName('<%=txtState.ClientID%>', "State", true, "")) {
                return false;
            }
        }

        function ValidBranch() {
            //For Branch Name

            if (document.getElementById('<%=txtBranchName.ClientID%>').value == "") {
                alert("Enter Bank Branch ");
                document.getElementById('<%=txtBranchName.ClientID%>').focus();
                return false;
            }

            //For Country
            if (document.getElementById('<%=ddlCou.ClientID%>').selectedIndex == 0) {
                alert("Select country");
                document.getElementById('<%=ddlCou.ClientID%>').focus();
                return false;
            }
            //For State
            if (document.getElementById('<%=ddlState1.ClientID%>').selectedIndex == 0) {
                alert("Select state");
                document.getElementById('<%=ddlState1.ClientID%>').focus();
                return false;
            }
            //For Location
            if (document.getElementById('<%=ddlLoc.ClientID%>').selectedIndex == 0) {
                alert("Select location");
                document.getElementById('<%=ddlLoc.ClientID%>').focus();
                return false;
            }
        }

        function ValidCountry() {
            if (document.getElementById('<%=txtCountry.ClientID%>').value == "") {
                alert("Please Enter Country Name ");
                document.getElementById('<%=txtCountry.ClientID%>').focus();
                return false;
            }

            if (document.getElementById('<%=txtNationlity.ClientID%>').value == "") {
                alert("Please Enter Nationality ");
                document.getElementById('<%=txtNationlity.ClientID%>').focus();
                return false;
            }
        }
        function CheckManager() {
            var SelVal = document.getElementById('<%=ddlEmpType.ClientID%>').options[document.getElementById('<%=ddlEmpType.ClientID%>').selectedIndex].text;

            if (SelVal == "General" || SelVal == "Section Head" || SelVal == "Project Manager" || SelVal == "Department Head") {
                document.getElementById('<%=ddlManager.ClientID%>').disabled = false;
            }
            else {
                document.getElementById('<%=ddlManager.ClientID%>').disabled = true;
            }
        }




        function Validdoj() {
            //For Bank Name
            if (document.getElementById('<%= txtDoj.ClientID  %>').value == "") {
                alert("Enter Date of Joining First!");
                return false;
            }
        }
        function ValidFamily() {
            //For Dependent
            if (!chkName('<%=txtDepndName.ClientID%>', "Name", true, "")) {
                return false;
            }
            if (document.getElementById('<%= txtDepndAge.ClientID  %>').value == "") {
                alert("Select DOB!");
                return false;
            }
            //txtDepndName

           <%-- if (!chkDropDownList('<%=ddlDepndBGroup.ClientID%>', "Blood Group", false, "")) {
                return false;
            }--%>
        }





        function NameAutoFill(source) {

            var Name = source.value;
            Name = Name.replace(" ", "").toLowerCase();
            $get('<%=txtMailId.ClientID %>').value = Name + '@' + $get('<%=lblDomain.ClientID %>').value;
            $get('<%=txtUsername.ClientID %>').value = Name;

        }



        function valids() {



            if (!chkName('<%=txtName.ClientID%>', "First Name", true, "")) {
                return false;
            }

            if (!chkName('<%=txtMName.ClientID%>', "Middle Name", false, "")) {
                return false;
            }

            if (!chkName('<%=txtLName.ClientID%>', "Last Name", true, "")) {
                return false;
            }







            // Pernonal mobile1
            if (!chkNumber('<%=txtMobile1.ClientID%>', 'Mobile', false, '')) {
                return false;
            }

            //For Comp Mobile
            if (!chkNumber('<%=txtMobile2.ClientID %>', ' Phone', false, ' ')) {
                return false;
            }



            //For Alt Email
            if (!chkEmail('<%=txtAltvMail.ClientID %>', 'Alternate Email', false, ' ')) {
                return false;
            }
            //For Desig
            if (document.getElementById('<%=ddlDesignation.ClientID%>').selectedIndex == 0) {
                alert("Please Select Designation");
                document.getElementById('<%=ddlDesignation.ClientID%>').focus();
                return false;
            }
            //For Trde
            if (document.getElementById('<%=ddlCategory.ClientID%>').selectedIndex == 0) {
                alert("Please Select Trade");
                document.getElementById('<%=ddlCategory.ClientID%>').focus();
                return false;
            }

            // E-MailID
            if (!chkEmail('<%=txtMailId.ClientID %>', 'Email', false, '')) {
                return false;
            }
            //For EmpType
            if (document.getElementById('<%=ddlEmpType.ClientID%>').selectedIndex == 0) {
                alert("Please Select Employee type");
                document.getElementById('<%=ddlEmpType.ClientID%>').focus();
                return false;
            }

            //For Nature
            if (document.getElementById('<%=ddlEmpnature.ClientID%>').selectedIndex == 0) {
                alert("Select Employee Nature");
                document.getElementById('<%=ddlEmpnature.ClientID%>').focus();
                return false;
            }

            //For WS

            if (document.getElementById('<%=ddlWorksite.ClientID%>').selectedIndex == 0) {
                alert("Please Select Worksite ");
                document.getElementById('<%=ddlWorksite.ClientID%>').focus();
                return false;
            }
            //For Dept
            if (document.getElementById('<%=ddldept.ClientID%>').selectedIndex == 0) {
                alert("Please Select Department ");
                document.getElementById('<%=ddldept.ClientID%>').focus();
                return false;
            }



            //For Sal
            if (!chkNumber('<%=txtSal.ClientID%>', "Salary", true, "")) {
                return false;
            }

            //For PAN
           // if (!chkPAN('<%=txtPan.ClientID %>', ' PAN', false, ' ')) {
           //     return false;
           // }



            //For Address
            if (document.getElementById('<%=txtAddress.ClientID%>').value == "") {
                alert("Please Enter Address");
                document.getElementById('<%=txtAddress.ClientID%>').focus();
                return false;
            }
            //For City

            //For State

            //For Country

            //For Phone
            if (!chkNumber('<%=txtPhone.ClientID %>', ' Phone', false, ' ')) {
                return false;
            }


            // For PaymentMode           
            if (!chkDropDownList('<%=ddlPaymentMode.ClientID%>', "Payment Mode")) {
                return false;
            }

            //For password
            if (document.getElementById('<%=chkPWD.ClientID%>').checked == true) {

                if (document.getElementById('<%=txtPassword.ClientID%>').value == "" && document.getElementById('<%=txtPassword.ClientID%>').disabled == false) {
                    alert("Please Enter Password");
                    document.getElementById('<%=txtPassword.ClientID%>').focus();
                    return false;
                }
            }

            //Reenter Password
            if (document.getElementById('<%=chkPWD.ClientID%>').checked == true) {
                if (document.getElementById('<%=txtReenterPassword.ClientID%>').value == "" && document.getElementById('<%=txtPassword.ClientID%>').disabled == false) {
                    alert("Please Enter ConfirmPassWord");
                    document.getElementById('<%=txtReenterPassword.ClientID%>').focus();
                    return false;
                }
            }

            //Confrom Password
            if (document.getElementById('<%=chkPWD.ClientID%>').checked == true) {
                if (document.getElementById('<%=txtPassword.ClientID%>').value != document.getElementById('<%=txtReenterPassword.ClientID%>').value) {
                    alert("Password and ConfirmPassWord are not matched");
                    document.getElementById('<%=txtPassword.ClientID%>').focus();
                    return false;
                }
            }
            if (document.getElementById('<%= chkCTH.ClientID %>').checked) {
                if (!chkDateDDMMMYYYY('<%= txtEOC_CTH.ClientID %>', "Date of  END CONTRACT!", true, "")) {
                    return false;
                }
                if (!chkNumber('<%= txtyearCTH.ClientID %>', "CTH Year!", true, "")) {
                    return false;
                }
                if (!chkNumber('<%=  txtmonthCTH.ClientID  %>', "CTH Month!", true, "")) {
                    return false;
                }
                if (!chkNumber('<%=  txtdaysCTH.ClientID   %>', "CTH Days!", true, "")) {
                    return false;
                }
            }

        }


        function validsCity() {


            //For Country
            if (document.getElementById('<%=ddlCountry.ClientID%>').selectedIndex == 0) {
                alert("Please select country.!");
                document.getElementById('<%=ddlCountry.ClientID%>').focus();
                return false;
            }


            //For State
            if (document.getElementById('<%=DdlState.ClientID%>').selectedIndex == 0) {
                alert("Please select state.!");
                document.getElementById('<%=DdlState.ClientID%>').focus();
                return false;
            }

            //For CityName
            if (!chkName('<%=txtNewLocation.ClientID%>', "Location", true, "")) {
                return false;
            }



        }

        function ConFirmAppointment(Empid) {
            alert('New Employee Created Successfully');
            var con = confirm('Do you wish to generate Appointment Letter to this Employee?');
            if (con == true) {
                window.location.href = "../HMS/AppointmentLetter.aspxid=" + Empid;
            }
            else {
                return false;
            }

        }

        function ConfirmUpdate() {
            window.alert("Employee Updated Successfully");

            return false;
        }

        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("Select a day earlier than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }



        function ValidPersonalDetails() {
            //For FirstName
            if (!chkName('<%=txtName.ClientID%>', "First Name", true, "")) {
                return false;
            }
            //For MiddleName
            if (!chkName('<%=txtMName.ClientID%>', "Middle Name", false, "")) {
                return false;
            }
            //For LastName
            if (!chkName('<%=txtLName.ClientID%>', "Last Name", true, "")) {
                return false;
            }
            if (document.getElementById('<%=txtDOB.ClientID%>').value == "") {
                alert("Please select DOB date");
                document.getElementById('<%=txtDOB.ClientID%>').focus();
                return false;
            }

            //if (!chkDate('<%=txtDOB.ClientID %>', 'DOB', '', '')) return false;
            if (!chkDateDDMMMYYYY('<%=txtDOB.ClientID %>', 'DOB', '', '')) return false;
        }
        function ValidCommunication() {
            //For Address
            if (document.getElementById('<%=txtAddress.ClientID%>').value == "") {
                alert("Please Enter Address");
                document.getElementById('<%=txtAddress.ClientID%>').focus();
                return false;
            }


            //For Country
            if (!chkDropDownList('<%=ddlComuCoun.ClientID %>', 'Country', true, '')) {
                return false;
            }
            if (document.getElementById('<%=ddlComuCoun.ClientID%>').value == -1) {
                alert("Please select country.!");
                document.getElementById('<%=ddlComuCoun.ClientID%>').focus();
                return false;
            }
            //For State
            if (!chkDropDownList('<%=ddlComuState.ClientID %>', 'State')) {
                return false;
            }
            if (document.getElementById('<%=ddlComuState.ClientID%>').value == -1) {
                alert("Please select state.!");
                document.getElementById('<%=ddlComuCoun.ClientID%>').focus();
                return false;
            }
            //For City
            if (!chkDropDownList('<%=ddlComuCity.ClientID %>', 'City')) {
                return false;
            }
            if (document.getElementById('<%=ddlComuCity.ClientID%>').value == -1) {
                alert("Please select city.!");
                document.getElementById('<%=ddlComuCity.ClientID%>').focus();
                return false;
            }
            //PIN Code chkPinCode
         <%--   if (!chkNumber('<%=txtPIN.ClientID %>', 'PIN', false, '')) {
                return false;
            }--%>
            //For Phone
            if (!chkNumber('<%=txtPhone.ClientID %>', ' Phone', false, ' ')) {
                return false;
            }

            // E-MailID
            if (!chkEmail('<%=txtMailId.ClientID %>', 'Email', false, '')) {
                return false;
            }
            //For Alt Email
            if (!chkEmail('<%=txtAltvMail.ClientID %>', 'Alternate Email', false, ' ')) {
                return false;
            }

            // Pernonal mobile1
            if (!chkNumber('<%=txtMobile1.ClientID%>', 'Mobile', false, '')) {
                return false;
            }

            //For Comp Mobile
            if (!chkNumber('<%=txtMobile2.ClientID %>', ' Phone', false, ' ')) {
                return false;
            }

        }

        function ValidJobDetails() {
            //For Desig
            if (document.getElementById('<%=ddlDesignation.ClientID%>').selectedIndex == 0) {
                alert("Please Select Designation");
                document.getElementById('<%=ddlDesignation.ClientID%>').focus();
                return false;
            }
            //For Trde
            if (document.getElementById('<%=ddlCategory.ClientID%>').selectedIndex == 0) {
                alert("Please Select Trade");
                document.getElementById('<%=ddlCategory.ClientID%>').focus();
                return false;
            }
            //For EmpType
            if (document.getElementById('<%=ddlEmpType.ClientID%>').selectedIndex == 0) {
                alert("Please Select Employee type");
                document.getElementById('<%=ddlEmpType.ClientID%>').focus();
                return false;
            }
            //For Nature
            if (document.getElementById('<%=ddlEmpnature.ClientID%>').selectedIndex == 0) {
                alert("Select Employee Nature");
                document.getElementById('<%=ddlEmpnature.ClientID%>').focus();
                return false;
            }
            //For WS
            if (document.getElementById('<%=ddlWorksite.ClientID%>').selectedIndex == 0) {
                alert("Please Select Worksite ");
                document.getElementById('<%=ddlWorksite.ClientID%>').focus();
                return false;
            }
            //For Dept
            if (document.getElementById('<%=ddldept.ClientID%>').selectedIndex == 0) {
                alert("Please Select Department ");
                document.getElementById('<%=ddldept.ClientID%>').focus();
                return false;
            }
            //For Date Of Join
            if (!chkDateDDMMMYYYY('<%=txtDoj.ClientID%>', "Date of join", true, "")) {
                return false;
            }

            //For Sal
            if (!chkNumber('<%=txtSal.ClientID%>', "Salary", true, "")) {
                return false; txtDOB
            }
            //For Shift
            if (!chkDropDownList('<%=ddlShift.ClientID%>', "Shift")) {
                return false;
            }
            if (document.getElementById('<%= chkCTH.ClientID %>').checked) {
                if (!chkDateDDMMMYYYY('<%= txtEOC_CTH.ClientID %>', "Date of  END CONTRACT!", true, "")) {
                    return false;
                }
                if (!chkNumber('<%= txtyearCTH.ClientID %>', "CTH Year!", true, "")) {
                    return false;
                }
                if (!chkNumber('<%=  txtmonthCTH.ClientID  %>', "CTH Month!", true, "")) {
                    return false;
                }
                if (!chkNumber('<%=  txtdaysCTH.ClientID   %>', "CTH Days!", true, "")) {
                    return false;
                }
            }

        }
        function ValidAccountDetails() {
            //For PAN  
            //if (!chkPAN('<%=txtPan.ClientID %>', ' PAN', false, ' ')) {
             //   return false;
            //}
        }

        function CountryChange(obj) {
            var Cou = obj.value;
            var Sta = document.getElementById(ddlmpState);

            if (Sta.value.length > 1) {
                var Res = CreateEmployee.FillStates(Cou);
                var len = Sta.options.length;
                for (i = len - 1; i >= 0; i--) {
                    Sta.options.remove(i);
                }
                var res1 = Res.value.split('|');
                var s = res1.length;
                addOption(Sta, '...Select...', '0');
                for (var i = 0; i < s - 1; ++i) {
                    var Result = res1[i].split('@');
                    addOption(Sta, Result[0], Result[1]);
                }
            }
        }
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("Select a day earlier than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>
   <%-- <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>--%>
            <table width="100%">
                <tr>
                    <td align="left" colspan="2">
                        <%--  <cc1:DragPanelExtender ID="DragPanelExtender1" TargetControlID="pnl11" runat="server"
                            DragHandleID="pnl11" Enabled="True">
                        </cc1:DragPanelExtender>--%>
                        <asp:Panel ID="pnl11" runat="server" CssClass="box box-primary">
                            <cc1:TabContainer ID="tb" runat="server" ActiveTabIndex="2" AutoPostBack="false"
                                Width="850px" BorderWidth="0" Height="445px">
                                <cc1:TabPanel runat="server" HeaderText=" Personal Details" ID="tabPerDetails" Enabled="true">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel5" runat="server" Height="435px">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td align="left">
                                                        <asp:Button ID="btnBack" Visible="false" runat="server" Text="Back" CssClass="savebutton btn btn-success " OnClick="btnBack_Click"
                                                            TabIndex="1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFName" runat="server" Text="First Name"></asp:Label><span style="color: #ff0000">*</span>
                                                        :
                                                    </td>
                                                    <td style="width: 300px">
                                                        <asp:TextBox ID="txtName" runat="server" MaxLength="25" Width="155px" 
                                                            OnTextChanged="txtName_TextChanged" AccessKey="1" ToolTip="[Alt+1]" TabIndex="2"></asp:TextBox>
                                                            
                                                              

                                                            <cc1:TextBoxWatermarkExtender
                                                                ID="txtWMExt" runat="server" WatermarkText="[Enter first name]" TargetControlID="txtName"
                                                                Enabled="True">
                                                            </cc1:TextBoxWatermarkExtender>
                                                    </td>
                                                    <td rowspan="5" style="vertical-align: top; text-align: justify">
                                                        <asp:Image ID="imgEmp" runat="server" Visible="False" />&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMName" runat="server" Text="Middle Name"></asp:Label>:
                                                    </td>
                                                    <td style="width: 300px;">
                                                        <asp:TextBox ID="txtMName" runat="server" MaxLength="25" Width="155px" AccessKey="2"
                                                            ToolTip="[Alt+2]" TabIndex="3"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 180px">
                                                        <asp:Label ID="lblLName" runat="server" Text="Last Name"></asp:Label><span style="color: #ff0000">*</span>
                                                        :
                                                    </td>
                                                    <td style="width: 300px;">
                                                        <asp:TextBox ID="txtLName" runat="server" MaxLength="25" Width="155px" AccessKey="3"
                                                            ToolTip="[Alt+3]" TabIndex="4"></asp:TextBox><cc1:TextBoxWatermarkExtender ID="txtWMLname"
                                                                runat="server" TargetControlID="txtLName" WatermarkText="[Enter last name]" Enabled="True">
                                                            </cc1:TextBoxWatermarkExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 180px;">
                                                        <asp:Label ID="lblDOB" runat="server" Text="DOB (dd MMM yyyy)"></asp:Label>
                                                        <span style="color: #ff0000">*:</span>
                                                    </td>
                                                    <td style="width: 100px;">
                                                        <asp:TextBox ID="txtDOB" Width="80px" runat="server"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalExtDOB" runat="server" TargetControlID="txtDOB" PopupButtonID="txtDOB"
                                                            Format="dd MMM yyyy" Enabled="true" OnClientDateSelectionChanged="checkDate">
                                                        </cc1:CalendarExtender>
                                                       
                                                    </td>
                                                </tr>
                                                <tr style="height: 90px">
                                                    <td>
                                                        <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>:
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rdblstgender" runat="server" RepeatDirection="Horizontal"
                                                            TabIndex="6" AccessKey="4" ToolTip="[Alt+4]">
                                                            <asp:ListItem Selected="True" Text="Male" Value="M"></asp:ListItem>
                                                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 180px;">
                                                        <asp:Label ID="lblQulification" runat="server" Text="Qualification"></asp:Label>:
                                                    </td>
                                                    <td style="width: 300px;">
                                                        <asp:TextBox ID="txtQualifcation" Width="50%" MaxLength="50" runat="server" TabIndex="7"
                                                            AccessKey="5" ToolTip="[Alt+5]"></asp:TextBox>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: justify">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 180px">
                                                        <asp:Label ID="lblBG2" runat="server" Text="Blood Group"></asp:Label>:
                                                    </td>
                                                    <td style="width: 100px">
                                                        <asp:DropDownList ID="ddlBldGrp" CssClass="droplist" width="60px" runat="server" TabIndex="8"
                                                            AccessKey="6" ToolTip="[Alt+6]">
                                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="A+"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="A-"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="B+"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="B-"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="AB+"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="AB-"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="O+"></asp:ListItem>
                                                            <asp:ListItem Value="8" Text="O-"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="vertical-align: top; text-align: justify">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr style="height: 150px">
                                                    <td style="width: 177px; vertical-align: text-top;">
                                                        <asp:Label ID="lblPlaceOfBirth" runat="server" Text=" City of Birth"></asp:Label>:
                                                    </td>
                                                    <td style="vertical-align: top">
                                                        <asp:DropDownList runat="server" ID="DdlLocation" CssClass="droplist" Width="120px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="DdlLocation_SelectedIndexChanged" TabIndex="9" AccessKey="7"
                                                            ToolTip="[Alt+7]" />
                                                        <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender5" runat="server"
                                                            TargetControlID="DdlLocation" PromptCssClass="PromptText" IsSorted="True" Enabled="True" />
                                                        <br />
                                                        <asp:Panel ID="PnlLocation" runat="server"  Visible="False">
                                                            <div id="tblNewLocation" runat="server" style='display: none;'>
                                                                <table>
                                                                    <tr>
                                                                        <td align="left" style="width: 196px">
                                                                            <asp:Label ID="lblCountry" runat="server" Text="Country:"></asp:Label><span style="color: #ff0000">*</span>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="droplist" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="width: 196px">
                                                                            <asp:Label ID="lblState" runat="server" Text="State:"></asp:Label><span style="color: #ff0000">*</span>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="DdlState" CssClass="droplist" runat="server">
                                                                            </asp:DropDownList>
                                                                              <asp:LinkButton ID="lnkState" Text="Add New" CssClass="btn btn-primary" runat="server" OnClientClick="OpenPopup()">
                                                                                 </asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="width: 196px">
                                                                            <asp:Label ID="lblLocation" runat="server" Text="New Location:"></asp:Label><span
                                                                                style="color: #ff0000">*</span>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtNewLocation" runat="server" MaxLength="50"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td align="left" style="width: 196px">
                                                                            <asp:Button ID="txtSaveLocatin" CssClass="btn btn-success" runat="server" Text="Save"
                                                                                OnClick="txtSaveLocatin_Click" OnClientClick="javascript:return validsCity();" /><asp:Button
                                                                                    ID="btnPlaceOfBirthCan" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnPlaceOfBirthCan_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Button ID="btnNextPer" runat="server" Text="Next" CssClass="savebutton btn btn-primary" OnClientClick="javascript:return ValidPersonalDetails();"
                                                            OnClick="btnNextPer_Click" TabIndex="10" />
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    
                              
</ContentTemplate>
</cc1:TabPanel>

                                <cc1:TabPanel runat="server" HeaderText="Communication" ID="tabCommunication" Enabled="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlCommu" runat="server"  Height="435px"
                                            Visible="False">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td valign="top">
                                                        <asp:Panel ID="pnl1" runat="server" >
                                                            <table>
                                                                <tr>
                                                                    <td class="subheader" colspan="3">
                                                                        <asp:Label ID="lblPrstAdd" runat="server" Text="Present Address"></asp:Label><br />
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 180px;">
                                                                        <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label><span style="color: #ff0000">*</span>
                                                                        :
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:TextBox ID="txtAddress" onkeyup="OneTextToother()" runat="server" Height="45px" MaxLength="100" Rows="6"
                                                                            TextMode="MultiLine" Width="98%" TabIndex="1" AccessKey="1" ToolTip="[Alt+1]"></asp:TextBox>
                                                                    </td>
                                                                    <td style="vertical-align: top; text-align: justify">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 180px;">
                                                                        <asp:Label ID="lblCountry1" runat="server" Text="Country"></asp:Label><span style="color: #ff0000">*</span>
                                                                        :
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:DropDownList ID="ddlComuCoun" runat="server" CssClass="droplist" onchange="OneTextToother();"  OnSelectedIndexChanged="ddlComuCoun_SelectedIndexChanged"
                                                                            AutoPostBack="True" TabIndex="2" AccessKey="2" ToolTip="[Alt+2]">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="vertical-align: top; text-align: justify; height: 19px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 180px;">
                                                                        <asp:Label ID="lblState1" runat="server" Text="State"></asp:Label><span style="color: #ff0000">*</span>
                                                                        :
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:DropDownList ID="ddlComuState" runat="server" CssClass="droplist" onchange="OneTextToother();" OnSelectedIndexChanged="ddlComuState_SelectedIndexChanged"
                                                                            AutoPostBack="True" TabIndex="3" AccessKey="3" ToolTip="[Alt+3]">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="vertical-align: top; text-align: justify">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 180px;">
                                                                        <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label><span style="color: #ff0000">*</span>
                                                                        :
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:DropDownList ID="ddlComuCity" runat="server" onchange="OneTextToother();" CssClass="droplist" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="ddlComuCity_SelectedIndexChanged" TabIndex="4">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="vertical-align: top; text-align: justify">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 180px;">
                                                                        <asp:Label ID="lblPIN" runat="server" Text="P.O.BOX / PIN"></asp:Label>:
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:TextBox ID="txtPIN" runat="server"   onkeyup="OneTextToother()"
                                                                            Width="98%" TabIndex="5" ToolTip="[Alt+5]"></asp:TextBox><cc1:FilteredTextBoxExtender
                                                                                ID="FTEPin" runat="server" FilterMode="ValidChars" FilterType="Numbers,Custom"
                                                                                TargetControlID="txtPIN">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td style="vertical-align: top; text-align: justify">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 180px;">
                                                                        <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label>:
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:TextBox ID="txtPhone" onkeyup="OneTextToother()" runat="server"   MaxLength="15"
                                                                            Width="98%" TabIndex="6" ToolTip="[Alt+6]"></asp:TextBox><cc1:FilteredTextBoxExtender
                                                                                ID="FilteredTextBoxExtender2" runat="server" Enabled="True" FilterType="Custom, Numbers"
                                                                                TargetControlID="txtPhone">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td style="vertical-align: top; text-align: justify">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblPerDoorNo" runat="server" Text=" Flat/Door/Block No"></asp:Label>:
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:TextBox ID="txtPerDoor" onkeyup="OneTextToother()" runat="server"  Width="98%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblPerBuilding" runat="server" Text="Name Of Building"></asp:Label>:
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:TextBox ID="txtPerBuilding" onkeyup="OneTextToother()" runat="server"  Width="98%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblPerStreet" runat="server" Text="Street"></asp:Label>:
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:TextBox ID="txtPerStreet" onkeyup="OneTextToother()" runat="server"  Width="98%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblPerArea" runat="server" Text="Area"></asp:Label>:
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:TextBox ID="txtPerArea" onkeyup="OneTextToother()" runat="server"  Width="98%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Panel ID="Panel1" runat="server" >
                                                            <table>
                                                                <tr>
                                                                    <td class="subheader" colspan="3">
                                                                        <asp:Label ID="lblPermanentAdd" runat="server" Text="Permanent Address"></asp:Label><br />
                                                                        <asp:CheckBox ID="chkAddress" runat="server" AutoPostBack="True" Font-Bold="False"
                                                                            Font-Italic="True" Text="Check if permanent address same as present address"
                                                                            TabIndex="7" OnCheckedChanged="chkAddress_CheckedChanged" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 180px;">
                                                                        <asp:Label ID="lblAddress1" runat="server" Text="Address"></asp:Label>:
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:TextBox ID="txtPer_Address" runat="server" Height="45px" MaxLength="100" Rows="6"
                                                                            TextMode="MultiLine" Width="98%" TabIndex="8"></asp:TextBox>
                                                                    </td>
                                                                    <td style="vertical-align: top; text-align: justify">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 180px; height: 23px;">
                                                                        <asp:Label ID="lblCountry2" runat="server" Text="Country"></asp:Label>:
                                                                    </td>
                                                                    <td style="width: 300px; height: 23px;">
                                                                        <asp:DropDownList ID="ddlComuPerCou" runat="server" CssClass="droplist" OnSelectedIndexChanged="ddlComuPerCou_SelectedIndexChanged"
                                                                            AutoPostBack="True" TabIndex="9">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="vertical-align: top; text-align: justify; height: 23px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 180px;">
                                                                        <asp:Label ID="lblState2" runat="server" Text="State"></asp:Label>:
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:DropDownList ID="ddlComuPerState" runat="server" CssClass="droplist" OnSelectedIndexChanged="ddlComuPerState_SelectedIndexChanged"
                                                                            AutoPostBack="True" TabIndex="10">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="vertical-align: top; text-align: justify">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 180px;">
                                                                        <asp:Label ID="lblCity1" runat="server" Text="City"></asp:Label>:
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:DropDownList ID="ddlComuPerCity" runat="server" CssClass="droplist" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="ddlComuPerCity_SelectedIndexChanged" TabIndex="11">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="vertical-align: top; text-align: justify">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 180px;">
                                                                        <asp:Label ID="lblPIN1" runat="server" Text="P.O.BOX / PIN"></asp:Label>:
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:TextBox ID="txtPer_PIN" runat="server"   
                                                                            Width="98%" TabIndex="12"></asp:TextBox><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                                                                                runat="server" FilterMode="ValidChars" FilterType="Numbers,Custom" TargetControlID="txtPer_PIN">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td style="vertical-align: top; text-align: justify">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 180px;">
                                                                        <asp:Label ID="lblPhone1" runat="server" Text="Phone"></asp:Label>:
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:TextBox ID="txtPer_Phone" runat="server"  MaxLength="15"
                                                                             Width="98%" TabIndex="13"></asp:TextBox>

                                                                            <cc1:FilteredTextBoxExtender
                                                                                ID="FilteredTextBoxExtender5" runat="server" Enabled="True" FilterType="Custom, Numbers"
                                                                                TargetControlID="txtPer_Phone">
                                                                            </cc1:FilteredTextBoxExtender>


                                                                    </td>
                                                                    <td style="vertical-align: top; text-align: justify">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblResDoor" runat="server" Text=" Flat/Door/Block No"></asp:Label>:
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:TextBox ID="txtResDoor" runat="server"  Width="98%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblResBuilding" runat="server" Text="Name Of Building"></asp:Label>:
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:TextBox ID="txtResBuilding" runat="server"  Width="98%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblResStreet" runat="server" Text="Street"></asp:Label>:
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:TextBox ID="txtResStreet" runat="server"  Width="98%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblResArea" runat="server" Text="Area"></asp:Label>:
                                                                    </td>
                                                                    <td style="width: 300px;">
                                                                        <asp:TextBox ID="txtResArea" runat="server"  Width="98%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 180px;">
                                                                    <asp:Label ID="lblMailID" runat="server" Text="Mail ID"></asp:Label><span style="color: #ff0000"></span>
                                                                    :
                                                                </td>
                                                                <td style="width: 300px;">
                                                                    <asp:TextBox ID="txtMailId" runat="server" Width="98%" MaxLength="100" TabIndex="14"
                                                                        AccessKey="7" ToolTip="[Alt+7]"></asp:TextBox><%--<cc1:TextBoxWatermarkExtender ID="txtwmMailId"
                                                                            runat="server" Enabled="True" TargetControlID="txtMailId" WatermarkText="[Ex: name@rediff.com]">
                                                                        </cc1:TextBoxWatermarkExtender>--%>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: justify">
                                                                    <asp:Label ID="lblMailWarn" runat="server" ForeColor="Red"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 180px;">
                                                                    <asp:Label ID="lblAltMailID" runat="server" Text="Alt. Mail ID"></asp:Label>:
                                                                </td>
                                                                <td style="width: 300px;">
                                                                    <asp:TextBox ID="txtAltvMail" runat="server" Width="98%" MaxLength="100" TabIndex="15"
                                                                        AccessKey="8" ToolTip="[Alt+8]"></asp:TextBox>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: justify">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 180px;">
                                                                    <asp:Label ID="lblSkypeID" runat="server" Text="Skype ID"></asp:Label>:
                                                                </td>
                                                                <td style="width: 300px;">
                                                                    <asp:TextBox ID="txtskypeid" runat="server" Width="98%" MaxLength="50" TabIndex="16"></asp:TextBox>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: justify">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 180px;">
                                                                    <asp:Label ID="lblPerMobile" runat="server" Text="Personal Mobile"></asp:Label>:
                                                                </td>
                                                                <td style="width: 300px;">
                                                                    <asp:TextBox ID="txtMobile1" runat="server"  MaxLength="20" Width="98%"
                                                                         TabIndex="17"></asp:TextBox>

                                                                          <cc1:FilteredTextBoxExtender
                                                                                ID="FilteredTextBoxExtender6" runat="server" Enabled="True" FilterType="Custom, Numbers"
                                                                                TargetControlID="txtMobile1">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: justify">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 180px;">
                                                                    <asp:Label ID="lblCompMobile" runat="server" Text=" Company Mobile"></asp:Label>:
                                                                </td>
                                                                <td style="width: 300px;">
                                                                    <asp:TextBox ID="txtMobile2" runat="server" 
                                                                        MaxLength="15" Width="98%" TabIndex="18"></asp:TextBox>

                                                                        <cc1:FilteredTextBoxExtender
                                                                                ID="FilteredTextBoxExtender7" runat="server" Enabled="True" FilterType="Custom, Numbers"
                                                                                TargetControlID="txtMobile2">
                                                                            </cc1:FilteredTextBoxExtender>

                                                                </td>
                                                                <td style="vertical-align: top; text-align: justify">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Button ID="btnPreAdd" runat="server" CssClass="savebutton btn btn-primary " OnClick="btnPreAdd_Click"
                                                            Text="Previous" TabIndex="19" Width="70px" /><asp:Button ID="btnNextAdd" runat="server"
                                                                Text="Next" CssClass="savebutton btn btn-success" OnClick="btnNextAdd_Click" OnClientClick="javascript:return ValidCommunication();"
                                                                TabIndex="20" />
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <table>
                                            <tr style="height: 300px">
                                                <td style="width: 300px">
                                                </td>
                                                <td>
                                                    <asp:Panel ID="pnlAddConStacity" runat="server"  Height="100px"
                                                        Visible="False">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblCountry3" runat="server" Text="Country Name"></asp:Label>
                                                                    <span style="color: #FF0000">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCountry" runat="server" TabIndex="21" AccessKey="1" ToolTip="[Alt+1]"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblNationality" runat="server" Text="Nationality"></asp:Label><span
                                                                        style="color: #FF0000">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNationlity" runat="server" TabIndex="22" AccessKey="2" ToolTip="[Alt+2]"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnSubmitCou" runat="server" Text="Submit" CssClass="btn btn-success"
                                                                        OnClick="btnSubmitCou_Click" OnClientClick="javascript:return ValidCountry();"
                                                                        TabIndex="23" /><asp:Button ID="btnCanelForCou" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                                                            OnClick="btnCanelForCou_Click" TabIndex="24" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlAddState" runat="server"  Height="100px"
                                                        Visible="False">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblStateCountry" runat="server" Text="Country"></asp:Label>
                                                                    <span style="color: #FF0000">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlStateCountry" runat="server" CssClass="droplist" TabIndex="25"
                                                                        AccessKey="1" ToolTip="[Alt+1]">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblStateStaName" runat="server" Text="State Name"></asp:Label>
                                                                    <span style="color: #FF0000">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtState" runat="server" TabIndex="26" AccessKey="2" ToolTip="[Alt+2]"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Button ID="btnStateSubmit" runat="server" CssClass="btn btn-success" Text="Submit"
                                                                        OnClientClick="javascript:return ValidAddState();" OnClick="btnStateSubmit_Click"
                                                                        TabIndex="27" />
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Button ID="btnStateCancel" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                                                        OnClick="btnStateCancel_Click" TabIndex="28" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlAddCity" runat="server"  Height="100px"
                                                        Visible="False">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblCityCoun" runat="server" Text="Country"></asp:Label>
                                                                    <span style="color: #FF0000">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlCityCountry" runat="server" CssClass="droplist" TabIndex="29"
                                                                        AccessKey="1" ToolTip="[Alt+1]">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblCityState" runat="server" Text="State"></asp:Label>
                                                                    <span style="color: #FF0000">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlCityState" runat="server" CssClass="droplist" TabIndex="30"
                                                                        AccessKey="2" ToolTip="[Alt+2]">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblCityCityName" runat="server" Text="City"></asp:Label>
                                                                    <span style="color: #FF0000">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCityName" runat="server" TabIndex="31" AccessKey="3" ToolTip="[Alt+3]"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnCitySubmit" runat="server" CssClass="btn btn-success" Text="Submit"
                                                                        OnClick="btnCitySubmit_Click" OnClientClick="javascript:return ValidAddCity();"
                                                                        TabIndex="32" />
                                                                    <asp:Button ID="btnCityCancel" runat="server" CssClass="btn btn-danger" Text="Cancel"
                                                                        OnClick="btnCityCancel_Click" TabIndex="33" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    
                        </ContentTemplate> </cc1:TabPanel>
                                <cc1:TabPanel runat="server" HeaderText="Identification & Dependencies" ID="tabUPDImage"
                                    Enabled="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel8" runat="server"  Height="435px">
                                            <asp:Panel ID="Panel2" runat="server"  Height="100px">
                                                <table style="width: 100%">
                                                   <tr id="Tr4" runat="server">
                                                        <td id="Td19" class="subheader" colspan="3" runat="server">
                                                            Moles
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 180px">
                                                            <asp:Label ID="lblMOle1" runat="server" Text="Mole 1"></asp:Label>:
                                                        </td>
                                                        <td style="vertical-align: top; text-align: justify">
                                                            <asp:TextBox ID="txtMole1" runat="server" MaxLength="50" Width="600px" TabIndex="2"
                                                                AccessKey="2" ToolTip="[Alt+2]"></asp:TextBox>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 180px">
                                                            <asp:Label ID="lblMole2" runat="server" Text="Mole 2"></asp:Label>:
                                                        </td>
                                                        <td style="vertical-align: top; text-align: justify">
                                                            <asp:TextBox ID="txtMole2" runat="server" MaxLength="50" Width="600px" TabIndex="3"
                                                                AccessKey="3" ToolTip="[Alt+3]"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel3" runat="server"  Height="290px">
                                                <table id="Table1"  width="100%">
                                                    <tr  >
                                                        <td class="subheader" colspan="3"  >
                                                            Dependencies
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr1" >
                                                        <td  style="width: 180px;" >
                                                            <asp:Label ID="lblAddDepen" runat="server"   Text="Add Dependencies"></asp:Label>:
                                                        </td>
                                                        <td  colspan="2" align="left"  style="width: 300px;"  >
                                                            <asp:DropDownList ID="ddlDependies"   CssClass="droplist" width="100px" runat="server"   onchange="ddlDependies_SelectedIndexChanged_j();"
                                                                 TabIndex="4" AccessKey="4"
                                                                ToolTip="[Alt+4]">
                                                                <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Father" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Mother" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="spouse" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="Son" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="Daughter" Value="5"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                         
                                                    </tr>
                                                    <tr id="trDepdName" style="display:none" >
                                                        <td  style="width: 180px;" >
                                                            <asp:Label ID="lblName" runat="server"  Text="Name"></asp:Label><span style="color: #ff0000">*</span>:
                                                        </td>
                                                        <td align="left"  style="width: 300px;"  >
                                                            <asp:TextBox ID="txtDepndName" Width="175px" runat="server" TabIndex="5" AccessKey="5"
                                                                ToolTip="[Alt+5]"></asp:TextBox>
                                                        </td>
                                                        <td id="Td6" style="vertical-align: top; text-align: justify" runat="server">
                                                        </td>
                                                    </tr>
                                                    <tr id="trDepdAge" style="display:none" >
                                                        <td  style="width: 180px;" >
                                                            <asp:Label ID="lblDOB1" runat="server"  Text="DOB"></asp:Label><span style="color: #ff0000">*</span>:
                                                        </td>
                                                        <td  align="left"    style="width: 300px;" >
                                                            <asp:TextBox ID="txtDepndAge" Width="150px" runat="server" TabIndex="6" AccessKey="6"
                                                                ToolTip="[Alt+6]"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDepndAge" PopupButtonID="txtDepndAge"
                                                            Format="dd MMM yyyy" Enabled="true" OnClientDateSelectionChanged="checkDate">
                                                        </cc1:CalendarExtender>
                                                        
                                                        </td>
                                                        <td  style="vertical-align: top; text-align: justify" >
                                                        </td>
                                                    </tr>
                                                    <tr id="trDepdBGrp"  style="display:none">
                                                        <td  style="width: 180px;" >
                                                            <asp:Label ID="lblBG" runat="server"  Text="Blood Group"></asp:Label>:
                                                        </td>
                                                        <td align="left"  style="width: 100px;">
                                                             <asp:DropDownList ID="ddlDepndBGroup" CssClass="droplist" Width="70px" runat="server" TabIndex="7"
                                                            AccessKey="7" ToolTip="[Alt+7]">
                                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="A+"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="A-"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="B+"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="B-"></asp:ListItem>
                                                            <asp:ListItem Value="5" Text="AB+"></asp:ListItem>
                                                            <asp:ListItem Value="6" Text="AB-"></asp:ListItem>
                                                            <asp:ListItem Value="7" Text="O+"></asp:ListItem>
                                                            <asp:ListItem Value="8" Text="O-"></asp:ListItem>
                                                        </asp:DropDownList><asp:Label ID="lblEx" runat="server" Text="Ex: A+ or B-"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: top; text-align: justify" >
                                                        </td>
                                                    </tr>
                                                    <tr id="trDepdGender" style="display:none" >
                                                        <td  style="width: 180px;">
                                                            <asp:Label ID="lblGender1" runat="server" Text="Gender"></asp:Label>:
                                                        </td>
                                                        <td  align="left"  style="width: 100px;" >
                                                            <asp:DropDownList ID="ddlDepGender" CssClass="droplist" Enabled="False" width="120px" runat="server" TabIndex="8"
                                                                AccessKey="8" ToolTip="[Alt+8]">
                                                                <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Female" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            &#160;&#160;<asp:Button ID="btnDepdAdd" CssClass="savebutton btn btn-success" runat="server" Text="Save"
                                                               OnClientClick="javascript:return ValidFamily();" OnClick="btnDepdAdd_Click" TabIndex="9"
                                                                AccessKey="s" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                                        </td>
                                                        <td  style="vertical-align: top; text-align: justify" >
                                                        </td>
                                                    </tr>
                                                    <tr id="trFamilyGrid"  >
                                                        <td  colspan="2"  >
                                                            <asp:GridView ID="gvFamily" AutoGenerateColumns="False" CssClass="gridview" runat="server"
                                                                OnRowCommand="gvFamily_RowCommand">
                                                                <Columns>
                                                                    <asp:BoundField DataField="RID" Visible="False" />
                                                                    <asp:BoundField DataField="Relation" HeaderText="Relation" />
                                                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                                                    <asp:BoundField DataField="DOB" HeaderText="DOB/Age" />
                                                                    <asp:BoundField DataField="BloodGroup" HeaderText="Blood Group" />
                                                                    <asp:BoundField DataField="Gender" HeaderText="Gender" />
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEdit" CommandArgument='<%#Eval("RID")%>' CommandName="edt"
                                                                                runat="server">Edit</asp:LinkButton></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkDelete" CommandName="del" runat="server">Delete</asp:LinkButton></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                        <td  style="vertical-align: top"  >
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr2">
                                                        <td colspan="3" >
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <table>
                                                <tr id="Tr3" runat="server">
                                                    <td id="Td20" align="center" runat="server">
                                                        <asp:Button ID="btnPreDepen" runat="server" Text="Previous" CssClass="savebutton btn btn-primary"
                                                            OnClick="btnPreDepen_Click" Width="70px" TabIndex="10" /><asp:Button ID="btnNextDepen"
                                                                runat="server" Text="Next" CssClass="savebutton btn btn-success" OnClick="btnNextDepen_Click"
                                                                TabIndex="11" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    
                            </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="tabJobDetails" runat="server" HeaderText="Reporting/Assignments" Enabled="false">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel6" runat="server"  Height="435px">
                                            <table>
                                                <tr>
                                                    <td style="width: 180px;">
                                                        <asp:Label ID="lblHisID" runat="server" Text=" Historical ID"></asp:Label>:
                                                    </td>
                                                    <td style="width: 300px;">
                                                        <asp:TextBox ID="txtOldEmpID" runat="server" TabIndex="1" AccessKey="1" ToolTip="[Alt+1]"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 180px; height: 21px;">
                                                        <asp:Label ID="lblDesig" runat="server" Text="Designation"></asp:Label><span style="color: #ff0000">*</span>
                                                        :
                                                    </td>
                                                    <td style="width: 300px; height: 21px;">
                                                        <asp:DropDownList ID="ddlDesignation" CssClass="droplist" runat="server" TabIndex="2"
                                                            AccessKey="2" ToolTip="[Alt+2]">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 180px;">
                                                        <asp:Label ID="lblTrade" runat="server" Text="Trades(Expertise)"></asp:Label><span
                                                            style="color: #ff0000">*</span> :
                                                    </td>
                                                    <td style="width: 300px;">
                                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="droplist" TabIndex="3"
                                                            AccessKey="3" ToolTip="[Alt+3]">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 180px;">
                                                        <asp:Label ID="lblEmptype" runat="server" Text="Employee Type"></asp:Label><span
                                                            style="color: #ff0000">*</span> :
                                                    </td>
                                                    <td style="width: 300px;">
                                                        <asp:DropDownList ID="ddlEmpType" runat="server" CssClass="droplist" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlEmpType_SelectedIndexChanged" TabIndex="4" AccessKey="4"
                                                            ToolTip="[Alt+4]">
                                                       </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 180px;">
                                                        <asp:Label ID="lblEmpNature" runat="server" Text=" Employee Nature"></asp:Label><span
                                                            style="color: #ff0000">*</span> :
                                                    </td>
                                                    <td style="width: 300px;">
                                                        <asp:DropDownList ID="ddlEmpnature" CssClass="droplist" runat="server" TabIndex="5"
                                                            AccessKey="5" ToolTip="[Alt+5]">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 180px;">
                                                        <asp:Label ID="lblWS" runat="server" Text="Worksite"></asp:Label><span style="color: #ff0000">*</span>
                                                        :
                                                    </td>
                                                    <td style="width: 300px;">
                                                        <asp:DropDownList ID="ddlWorksite" CssClass="droplist" runat="server" 
                                                            OnSelectedIndexChanged="ddlWorksite_SelectedIndexChanged" TabIndex="6" AccessKey="w"
                                                            ToolTip="[Alt+w OR Alt+w+Enter]">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 180px;">
                                                        <asp:Label ID="lblDept" runat="server" Text="Department"></asp:Label><span style="color: #ff0000">*</span>
                                                        :
                                                    </td>
                                                    <td style="width: 300px;">
                                                        <asp:DropDownList ID="ddldept" CssClass="droplist"  runat="server"
                                                            OnSelectedIndexChanged="ddldept_SelectedIndexChanged" TabIndex="7" AccessKey="6"
                                                            ToolTip="[Alt+6]">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 180px;">
                                                        <asp:Label ID="lblRptTo" runat="server" Text="Reporting To"></asp:Label>:
                                                    </td>
                                                    <td style="width: 300px;">
                                                        <asp:DropDownList ID="ddlManager" runat="server" CssClass="droplist" TabIndex="8"
                                                            AccessKey="7" ToolTip="[Alt+7]">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 200px;">
                                                        <asp:Label ID="lblDateOfJoin" runat="server" Text="Date Of Join(dd MMM yyyy)"></asp:Label><span
                                                            style="color: #ff0000">*</span> :
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtDoj" runat="server" TabIndex="9" AccessKey="t" ToolTip="[Alt+t OR Alt+t+Enter]"></asp:TextBox><cc1:CalendarExtender
                                                            ID="CalendarExtender1" runat="server" TargetControlID="txtDoj" PopupButtonID="txtDoj"
                                                            Format="dd MMM yyyy" Enabled="True">
                                                        </cc1:CalendarExtender>
                                                      
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 180px;">
                                                        <asp:Label ID="lblSal" runat="server" Text="Salary (CTC per Annum)"></asp:Label><span
                                                            style="color: #ff0000">*</span> :
                                                    </td>
                                                    <td style="width: 300px;">
                                                        <asp:TextBox ID="txtSal" runat="server" TabIndex="10" AccessKey="8" ToolTip="[Alt+8]"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender
                                                                                ID="FilteredTextBoxExtender8" runat="server" Enabled="True" FilterType="Custom, Numbers"
                                                                                TargetControlID="txtSal">
                                                                            </cc1:FilteredTextBoxExtender>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 180px;">
                                                        <asp:Label ID="lblShift" runat="server" Text="Shift"></asp:Label><span style="color: #ff0000">*</span>
                                                        :
                                                    </td>
                                                    <td style="width: 300px;">
                                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="droplist" TabIndex="11">
                                                           
                                                        </asp:DropDownList>
                                                        &#160;* Day Shift is default
                                                    </td>
                                                    <td style="vertical-align: top; text-align: justify">
                                                    </td>
                                                </tr>
                                                 <tr>
                                                
                                                   <td style="vertical-align:top" >
                                                   
                                                   Is Contract to Hire [CTH]?:
                                                   </td>

                                                   <td>
                                                     <asp:CheckBox ID="chkCTH" runat="server" onchange="chkCTH_SelectedIndexChanged();" />
                                                      <asp:Panel  runat="server" ID="pnlCTH" style="display:none"  >
                                                          <table width="100%" style ="border-style:dotted ;border-width:1px;border-color:#0094ff;border-radius:15px">
                                                              <tr>
                                                                  <td><asp:TextBox  runat="server" ID="txtyearCTH" Width ="30px" Text ="0" ToolTip ="CTH years!"   onkeypress="return OnlyNumeric(event);" ></asp:TextBox>[Years]</td>
                                                                  <td><asp:TextBox  runat="server" ID="txtmonthCTH" Width ="30px" Text ="0" ToolTip ="CTH months!"    onkeypress="return OnlyNumeric(event);"></asp:TextBox>[Months]</td>
                                                                   <td><asp:TextBox  runat="server" ID="txtdaysCTH" Width ="50px" Text ="0" ToolTip ="CTH days!"  onkeypress="return OnlyNumeric(event);"></asp:TextBox>[Days]</td>
                                                                  
                                                              </tr>
                                                              <tr>
                                                                  <td colspan="3" style="vertical-align:middle "><asp:TextBox  runat="server" ID="txtEOC_CTH" Width ="80px" ReadOnly="true"   ToolTip ="DOC will be auto calculated from DOJ as per input Years,Month & days" ></asp:TextBox><asp:ImageButton  runat="server" ID="btnrefresh" ImageUrl="~/Images/refresh.gif" Width ="20px" Height="20px" OnClick ="Calculate_dt"  OnClientClick ="javascript:return Validdoj();" ToolTip ="calculate the EOC Date"  /><span style="background-color:yellow " >[Date Of End Contract]</span></td>
                                                                   
                                                                    
                                                              </tr>
                                                          </table>
                                                      </asp:Panel>
<%--<script type="text/javascript" >
    function autofill_DOC()
    {
       var someDate = new Date();
        alert(someDate);
        var dd = someDate.getDate() + document.getElementById('<%# txtdaysCTH.ClientID  %>').value ;
        var mm = someDate.getMonth() + document.getElementById('<%#  txtmonthCTH.ClientID %>').value;
        var y = someDate.getFullYear() + document.getElementById('<%# txtyearCTH.ClientID  %>').value;
        var someFormattedDate = dd + '/' + mm + '/' + y;
        alert(someFormattedDate);
        document.getElementById('<%#  txtEOC_CTH.ClientID %>').value = someFormattedDate;
    }
</script>--%>
                                                   </td>
                                                </tr>
                                                <tr>
                                                
                                                   <td>
                                                   
                                                   Is Labor Force?(Direct Job Costing)
                                                   </td>

                                                   <td>
                                                     <asp:CheckBox ID="chkNMR" runat="server" />
                                                   
                                                   </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                     
                                                         <asp:HyperLink ID="lnkFileView" runat="server" Text="View Job Responsibilities"  Target="_blank" NavigateUrl="#"></asp:HyperLink>

                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="align:left; height:350px ">
                                                        <asp:Button ID="btnPre" runat="server" Text="Previous" CssClass="savebutton btn btn-primary" OnClick="btnPre_Click"
                                                            TabIndex="12" Width="70px" />
                                                        <asp:Button ID="btnJobNext" runat="server" Text="Next" CssClass="savebutton btn btn-success" OnClick="btnJobNext_Click"
                                                            OnClientClick="javascript:return ValidJobDetails();" TabIndex="13" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    
</ContentTemplate>
                                








</cc1:TabPanel>
                                <cc1:TabPanel runat="server" HeaderText=" Account & Authentication Details" ID="tabAccDetails"
                                    Enabled="false">
                                    <HeaderTemplate>
                                        Account &amp; Authentication Details
</HeaderTemplate>
                                    








<ContentTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel4" runat="server"  Height="160px" Width="500px">
                                                        <table style="width: 100%">


                                                            <tr>
                                                                <td class="subheader" colspan="3">
                                                                    Account Details
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td style="width: 180px;">
                                                                    Payment Mode: <span style="color: #ff0000">*</span>
                                                                </td>
                                                                <td style="width: 350px;">
                                                                   <asp:DropDownList ID="ddlPaymentMode" width="120px" runat="server" CssClass="droplist" onchange="ddlPaymentMode_SelectedIndexChanged_js();">
<asp:ListItem Text="--Select--" Value="0" Selected ="True"  ></asp:ListItem>
<asp:ListItem Text="By Cash" Value="1"   ></asp:ListItem>
<asp:ListItem Text="By Account" Value="2" ></asp:ListItem>
</asp:DropDownList>









                                                                </td>
                                                            </tr>

                                                            <tr id="trBankDts"  style="display:none " ><td colspan="2" style="width: 530px;" runat="server">
                                                                   <table>
                                                                        
                                                            <tr>
                                                                <td style="width: 180px;">
                                                                    <asp:Label ID="lblCompACNum" runat="server" Text="Employee A/C Number"></asp:Label>








:
                                                                </td>
                                                                <td style="width: 350px;">
                                                                    <asp:TextBox ID="txtAccNo" runat="server" Width="296px" TabIndex="1" AccessKey="1"
                                                                        ToolTip="[Alt+1]"></asp:TextBox>









                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 180px; height: 21px;">
                                                                    <asp:Label ID="lblBankName" runat="server" Text="Bank Name"></asp:Label>








:
                                                                </td>
                                                                <td style="width: 300px; height: 21px;">
                                                                    <asp:DropDownList ID="ddlBank" AutoPostBack="True" runat="server" CssClass="droplist"
                                                                        OnSelectedIndexChanged="ddlBank_SelectedIndexChanged" TabIndex="2" AccessKey="2"
                                                                        ToolTip="[Alt+2]"></asp:DropDownList>









                                                                    &#160;&#160;<asp:LinkButton ID="lnkBank" runat="server" Text="Add Bank" OnClick="lnkBank_Click"
                                                                        TabIndex="3"></asp:LinkButton>









                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 180px;">
                                                                    <asp:Label ID="lblBranchName" runat="server" Text="Branch Name"></asp:Label>








:
                                                                </td>
                                                                <td style="width: 368px;">
                                                                    <asp:DropDownList ID="ddlBranch" CssClass="droplist" runat="server" TabIndex="7"
                                                                        AccessKey="3" ToolTip="[Alt+3]"></asp:DropDownList>









                                                                    &#160;&#160;<asp:LinkButton ID="lnkBranch" runat="server" Text="Add Branch" OnClick="lnkBranch_Click"
                                                                        TabIndex="8"></asp:LinkButton>









                                                                </td>
                                                                <td style="vertical-align: top; text-align: justify">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 180px;">
                                                                    <asp:Label ID="lblPANNumber" runat="server" Text="IQAMA/ID CARD"></asp:Label>








:
                                                                </td>
                                                                <td style="width: 300px;">
                                                                    <asp:TextBox ID="txtPan" MaxLength="15" runat="server" TabIndex="15" AccessKey="4"
                                                                        ToolTip="[Alt+4]"></asp:TextBox>








&#160; Ex: ABCPD1234E
                                                                </td>
                                                            </tr>
                                                            
                                                                   
                                                                   
                                                                   </table>
                                                                
                                                                </td>
</tr>











                                                             
                                                    

                                                        </table>
                                                    </asp:Panel>









                                                </td>
                                                <td>
                                                    <asp:Panel ID="pnlBank" runat="server"  Height="145px" Width="300px"
                                                        Visible="False">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblNewBankName" runat="server" Text="BankName"></asp:Label>








<span style="color: #ff0000">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNewBankName" runat="server" TabIndex="4"></asp:TextBox>









                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Button ID="btnNewBankSub" runat="server" Text="Submit" CssClass="savebutton"
                                                                        TabIndex="5" OnClick="btnNewBankSub_Click" OnClientClick="javascript:return ValidBank();" />









                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="savebutton" OnClick="btnCancel_Click"
                                                                        TabIndex="6" />









                                                                </td>
                                                            </tr>

                                                            

                                                        </table>
                                                    </asp:Panel>









                                                    <asp:Panel ID="pnlBranch1" runat="server"  Height="110px"
                                                        Width="300px" Visible="False">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblBranch" runat="server" Text="Branch Name"></asp:Label>








<span style="color: #ff0000">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBranchName" runat="server" TabIndex="9"></asp:TextBox>









                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblCou" runat="server" Text="Country"></asp:Label>








<span style="color: #ff0000">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlCou" runat="server" OnSelectedIndexChanged="ddlCou_SelectedIndexChanged"
                                                                        TabIndex="10" AutoPostBack="True"></asp:DropDownList>









                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblSta" runat="server" Text="State"></asp:Label>








<span style="color: #ff0000">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlState1" runat="server" OnSelectedIndexChanged="ddlState1_SelectedIndexChanged"
                                                                        TabIndex="11" AutoPostBack="True"></asp:DropDownList>









                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblLoc" runat="server" Text="Location"></asp:Label>








<span style="color: #ff0000">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlLoc" runat="server" TabIndex="12"></asp:DropDownList>









                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Button ID="btnSubBranch" runat="server" Text="Submit" CssClass="savebutton"
                                                                        TabIndex="13" OnClientClick="javascript:return ValidBranch();" OnClick="btnSubBranch_Click" />









                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnCanBranch" runat="server" Text="Cancel" CssClass="savebutton"
                                                                        TabIndex="14" OnClick="btnCanBranch_Click" />









                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>









                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel9" runat="server"  Height="145px" Width="500px">
                                                        <table>
                                                            <tr>
                                                                <td class="subheader" colspan="3">
                                                                    Authentication Details
                                                                    <asp:CheckBox ID="chkPWD" runat="server" Font-Bold="False" Font-Italic="True"  AutoPostBack="True" 
                                                                        Text="If you Check, UserName and Password are mandatory" OnCheckedChanged="chkPWD_CheckedChanged"
                                                                        TabIndex="16" AccessKey="5" ToolTip="[Alt+5]" />
                                                              </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 180px;">
                                                                    <asp:Label ID="lblUserName" runat="server" Text="Username"></asp:Label>
                                                                  <span style="color: #ff0000">*</span>
                                                                </td>
                                                                <td>
                                                                    
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:TextBox ID="txtUsername" runat="server" AutoPostBack="true" MaxLength="20" Width="175"
                                                                                            OnTextChanged="txtUsername_TextChanged" onchange="NameAutoFill(this)" TabIndex="17"
                                                                                            AccessKey="6" ToolTip="[Alt+6]" >
                                                                                            </asp:TextBox>
                                                                                           <%-- <asp:RangeValidator ID="RngValdUsername" runat="server" MinimumValue="6" MaximumValue="12" ControlToValidate="txtUsername"  ErrorMessage="Minmum 6 and Maximum 12 charrectors" ></asp:RangeValidator>
                                                                                            <asp:RegularExpressionValidator ID="regvalUsername" runat="server" ValidationExpression="([a-z]|[A-Z]|[0-9]|[]|[-]|[_])*" ErrorMessage="Accepts a-z,A-Z,0-9 only" ControlToValidate="txtUsername" ></asp:RegularExpressionValidator>--%>
                                                                                            <asp:Label ID="lblUserAvailable" runat="server"
                                                                                                Font-Bold="True" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                                                                                                
                                                                             </ContentTemplate>
                                                                          </asp:UpdatePanel>
                                                                         </td>
                                                                            <td width="300Px">
                                                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                                                    DisplayAfter="1" DynamicLayout="False"><ProgressTemplate>
                                                                                        <img id="Img1" src="Images/ajax-loader.gif" runat="server" style="text-align: left;"
                                                                                            alt="0" />Checking ...</ProgressTemplate>
                                                                                    </asp:UpdateProgress>
                                                                           </td>
                                                                        </tr>
                                                                    </table>
                                                                 
                                                                </td>
                                                                <td style="vertical-align: top; text-align: justify">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 150px">
                                                                    <asp:Label ID="lblPWD" runat="server" Text="Password"></asp:Label>
                                                                <span style="color: #ff0000">*</span>
                                                                </td>
                                                                <td style="width: 300px;">
                                                               
                                                                 <asp:TextBox ID="txtPassword" runat="server" Width="175px" Height="17px" TextMode="Password" MaxLength="20" 
                                                                        TabIndex="18" AccessKey="7" ToolTip="[Alt+7]"></asp:TextBox>
                                                              </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblConfPWD" runat="server" Text="Confirm Password"></asp:Label><span style="color: Red" id="spncnfpwd" runat="server">*</span>
                                                               </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtReenterPassword" runat="server" TextMode="Password" Width="175px" Height="17px"
                                                                        MaxLength="20" CssClass="text_black" TabIndex="19" AccessKey="8" ToolTip="[Alt+8]"></asp:TextBox>
                                                              </td>
                                                            </tr>
                                                      </table>
                                                    </asp:Panel> </td>
                                            </tr>
 <tr>
                                                <td>
                                                    <asp:Panel ID="Panel7" runat="server"  Height="65px" Width="500px">
                                                        <table>
                                                        <tr>
                                                                <td class="subheader" colspan="3">
                                                                    Image
                                                                    </td>
                                                                    </tr>

                                                        <tr>
                                                        <td style="width: 180px;">
                                                            <asp:Label ID="lblPhotoUpLoad" runat="server" Text="Photo Upload :"></asp:Label>
</td>
                                                        <td style="width: 300px;">
                                                        
                                                            <asp:FileUpload ID="fudPhoto" runat="server" Width="100%" TabIndex="1" AccessKey="1"
                                                                ToolTip="[Alt+1]" />

 </td>
                                                        <td style="vertical-align: top; text-align: justify">
                                                        </td>
                                                    </tr>
</table>
                                                    </asp:Panel>
 </td>
                                            </tr>
                                            <tr>
                                                <td align="left" height="120">
                                                    <asp:Button ID="btnPreAccDetails" runat="server" Text="Previous" CssClass="savebutton btn btn-primary"
                                                        OnClick="btnPreAccDetails_Click" TabIndex="20" Width="60px" />
                                                 <asp:Button ID="btnSubmit"  runat="server" Text="Submit" OnClientClick="javascript:return valids();" CssClass="savebutton btn btn-success"
                                                            Height="21px" OnClick="btnSubmit_Click" TabIndex="21" Width="60px" AccessKey="s"
                                                            ToolTip="[Alt+s OR Alt+s+Enter]" /> </td>
                                            </tr>
                                        </table>
                                    
</ContentTemplate></cc1:TabPanel>
                            </cc1:TabContainer>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        <%--</ContentTemplate>
          
    </asp:UpdatePanel>--%>

    <asp:HiddenField ID="lblDomain" runat="server" OnValueChanged="lblDomain_ValueChanged" />
    <asp:HiddenField ID="hdnCountry" runat="server" />
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
            <ProgressTemplate>
                <img src="IMAGES/updateProgress.gif" alt="update is in progress" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:content>
