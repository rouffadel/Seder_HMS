<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="CreateNewEmployee.aspx.cs" Inherits="AECLOGIC.ERP.HMS.CreateNewEmployee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function CheckManager() {
            var SelVal = document.getElementById('<%=ddlEmpType.ClientID%>').options[document.getElementById('<%=ddlEmpType.ClientID%>').selectedIndex].text;

            if (SelVal == "General" || SelVal == "Section Head" || SelVal == "Project Manager" || SelVal == "Department Head") {
                document.getElementById('<%=ddlManager.ClientID%>').disabled = false;
            }
            else {
                document.getElementById('<%=ddlManager.ClientID%>').disabled = true;
            }
        }

        function FDOB() {
            if (!chkDOB('<%=txtDepndAge.ClientID%>', "DOB", false, "")) {
                return false;
            }
        }

        function DOB() {
            if (!chkDOB('<%=txtDOB.ClientID%>', "DOB", false, "")) {
                return false;
            }
        }

        function ValidFamily() {

            //For DOB
            if (!chkDOB('<%=txtDepndAge.ClientID%>', "DOB", false, "")) {
                return false;
            }
            if (!ChkBG('<%=txtDepndBGroup.ClientID%>', "Blood Group", false, "")) {
                return false;
            }
        }
        function CheckAddress() {
            var SelVal = document.getElementById('<%=chkAddress.ClientID%>').checked;

            document.getElementById('<%=txtPer_Address.ClientID%>').disabled = SelVal;
            document.getElementById('<%=txtPer_City.ClientID%>').disabled = SelVal;
            document.getElementById('<%=txtPer_State.ClientID%>').disabled = SelVal;
            document.getElementById('<%=txtPer_Country.ClientID%>').disabled = SelVal;
            document.getElementById('<%=txtPer_Phone.ClientID%>').disabled = SelVal;
            document.getElementById('<%=txtPer_PIN.ClientID%>').disabled = SelVal;

            if (SelVal) {
                document.getElementById('<%=txtPer_Address.ClientID%>').value = document.getElementById('<%=txtAddress.ClientID%>').value;
                document.getElementById('<%=txtPer_City.ClientID%>').value = document.getElementById('<%=txtCity.ClientID%>').value;
                document.getElementById('<%=txtPer_State.ClientID%>').value = document.getElementById('<%=txtState.ClientID%>').value;
                document.getElementById('<%=txtPer_Country.ClientID%>').value = document.getElementById('<%=txtCountry.ClientID%>').value;
                document.getElementById('<%=txtPer_Phone.ClientID%>').value = document.getElementById('<%=txtPhone.ClientID%>').value;
                document.getElementById('<%=txtPer_PIN.ClientID%>').value = document.getElementById('<%=txtPIN.ClientID%>').value;
            }
            else {
                document.getElementById('<%=txtPer_Address.ClientID%>').value = '';
                document.getElementById('<%=txtPer_City.ClientID%>').value = '';
                document.getElementById('<%=txtPer_State.ClientID%>').value = '';
                document.getElementById('<%=txtPer_Country.ClientID%>').value = '';
                document.getElementById('<%=txtPer_Phone.ClientID%>').value = '';
                document.getElementById('<%=txtPer_PIN.ClientID%>').value = '';
            }


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


            if (!chkDOB('<%=txtDOB.ClientID%>', "DOB", false, "")) {
                return false;
            }




            // Pernonal mobile1
            if (!chkMobile('<%=txtMobile1.ClientID%>', 'Mobile', false, '')) {
                return false;
            }

            //For Comp Mobile
            if (!chkMobile('<%=txtMobile2.ClientID %>', ' Phone', false, ' ')) {
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
            if (!chkEmail('<%=txtMailId.ClientID %>', 'Email', true, '')) {
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
            if (!chkPAN('<%=txtPan.ClientID %>', ' PAN', false, ' ')) {
                return false;
            }



            //For Address
            if (document.getElementById('<%=txtAddress.ClientID%>').value == "") {
                alert("Please Enter Address");
                document.getElementById('<%=txtAddress.ClientID%>').focus();
                return false;
            }
            //For City
            if (document.getElementById('<%=txtCity.ClientID%>').value == "") {
                alert("Please Enter Present City");
                document.getElementById('<%=txtCity.ClientID%>').focus();
                return false;
            }
            //For State
            if (document.getElementById('<%=txtState.ClientID%>').value == "") {
                alert("Please Enter Present State");
                document.getElementById('<%=txtState.ClientID%>').focus();
                return false;
            }
            //For Country
            if (document.getElementById('<%=txtCountry.ClientID%>').value == "") {
                alert("Please Enter Present Country");
                document.getElementById('<%=txtCountry.ClientID%>').focus();
                return false;
            }
            //For Phone
            if (!chkPhoneOrMobile('<%=txtPhone.ClientID %>', ' Phone', false, ' ')) {
                return false;
            }


            //            


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

        function NewLocation() {

            var Selectedtext = $get('<%=DdlLocation.ClientID %>').value;

            if (Selectedtext == "-1") {
                $get('<%=tblNewLocation.ClientID %>').style.display = 'block';
                // document.getElementById("tblNewLocation").style.display = 'block';
                $get('<%=txtNewLocation.ClientID %>').value = "";
                $get('<%=ddlCountry.ClientID %>').selectedIndex = 0;
                $get('<%=DdlState.ClientID %>').selectedIndex = 0;


            }
            else {
                $get('<%=tblNewLocation.ClientID %>').style.display = 'none';

                $get('<%=txtNewLocation.ClientID %>').value = "";

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
            //For DOB
            if (!chkDOB('<%=txtDOB.ClientID%>', "DOB", false, "")) {
                return false;
            }
            // E-MailID
            if (!chkEmail('<%=txtMailId.ClientID %>', 'Email', true, '')) {
                return false;
            }
            // Pernonal mobile1
            if (!chkMobile('<%=txtMobile1.ClientID%>', 'Mobile', false, '')) {
                return false;
            }
            //For Comp Mobile
            if (!chkMobile('<%=txtMobile2.ClientID %>', ' Phone', false, ' ')) {
                return false;
            }
            //For Alt Email
            if (!chkEmail('<%=txtAltvMail.ClientID %>', 'Alternate Email', false, ' ')) {
                return false;
            }
        }
        function ValidCommunication() {
            //For Address
            if (document.getElementById('<%=txtAddress.ClientID%>').value == "") {
                alert("Please Enter Address");
                document.getElementById('<%=txtAddress.ClientID%>').focus();
                return false;
            }
            //For City
            if (document.getElementById('<%=txtCity.ClientID%>').value == "") {
                alert("Please Enter Present City");
                document.getElementById('<%=txtCity.ClientID%>').focus();
                return false;
            }
            //For State
            if (document.getElementById('<%=txtState.ClientID%>').value == "") {
                alert("Please Enter Present State");
                document.getElementById('<%=txtState.ClientID%>').focus();
                return false;
            }
            //For Country
            if (document.getElementById('<%=txtCountry.ClientID%>').value == "") {
                alert("Please Enter Present Country");
                document.getElementById('<%=txtCountry.ClientID%>').focus();
                return false;
            }
            //PIN Code chkPinCode
            if (!chkPinCode('<%=txtPIN.ClientID %>', 'PIN', false, '')) {
                return false;
            }
            //For Phone
            if (!chkPhoneOrMobile('<%=txtPhone.ClientID %>', ' Phone', false, ' ')) {
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
            if (!chkDate('<%=txtDoj.ClientID%>', "Date of join", true, "")) {
                return false;
            }

            //For Sal
            if (!chkNumber('<%=txtSal.ClientID%>', "Salary", true, "")) {
                return false;
            }
            //For Shift
            if (!chkDropDownList('<%=ddlShift.ClientID%>', "Shift")) {
                return false;
            }
        }
        function ValidAccountDetails() {
            //For PAN  
            if (!chkPAN('<%=txtPan.ClientID %>', ' PAN', false, ' ')) {
                return false;
            }
        }
    </script>
   
    <table width="100%">
        <tr>
            <td>
                <AEC:Topmenu ID="topmenu" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
               
                <asp:Panel ID="pnl11" runat="server" CssClass="DivBorderOlive" Width="850px" Style="position: absolute;
                    font-size: 80%; top: 25%; left: 17%;">
                    <cc1:TabContainer ID="tb" runat="server" ActiveTabIndex="6" AutoPostBack="false"
                        Width="850px" BorderWidth="0">
                      
                        <cc1:TabPanel runat="server" HeaderText=" Personal Details" ID="tabPerDetails" Enabled="true">
                            <ContentTemplate>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFName" runat="server" Text="First Name"></asp:Label>
                                            <span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px">
                                            <asp:TextBox ID="txtName" runat="server" MaxLength="25" Width="175px" onchange="NameAutoFill(this);"
                                                OnTextChanged="txtName_TextChanged"></asp:TextBox>
                                        </td>
                                        <td rowspan="5" style="vertical-align: top; text-align: justify">
                                            <asp:Image ID="imgEmp" runat="server" Visible="False" />&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMName" runat="server" Text="Middle Name"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtMName" runat="server" MaxLength="25" Width="175px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px">
                                            <asp:Label ID="lblLName" runat="server" Text="Last Name"></asp:Label><span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtLName" runat="server" MaxLength="25" Width="175px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblDOB" runat="server" Text="DOB (dd/MM/yyyy)"></asp:Label>
                                            <span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtDOB" runat="server" Width="80px"></asp:TextBox>* 01/01/1981
                                            is Default
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDOB"
                                                PopupButtonID="txtDOB" Format="dd/MM/yyyy" Enabled="True">
                                            </cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender FilterType="Custom, Numbers" ID="Fl4" runat="server"
                                                TargetControlID="txtDOB" ValidChars="/" Enabled="True">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblPlaceOfBirth" runat="server" Text=" Place of Birth"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:UpdatePanel ID="upLocation" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList runat="server" ID="DdlLocation" CssClass="droplist" AutoPostBack="false"
                                                        onchange="NewLocation();" 
                                                        onselectedindexchanged="DdlLocation_SelectedIndexChanged" />
                                                    <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender5" runat="server"
                                                        TargetControlID="DdlLocation" PromptText="Type to search" PromptCssClass="PromptText"
                                                        PromptPosition="Top" IsSorted="true" />
                                                    <br />
                                                    <div id="tblNewLocation" runat="server" style='display: none;'>
                                                        <table>
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <tr>
                                                                        <td align="left" style="width: 196px">
                                                                            <asp:Label ID="lblCountry" runat="server" Text="Country:">
                                                                            </asp:Label><span style="color: #ff0000">*</span>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" CssClass="droplist">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="width: 196px">
                                                                            <asp:Label ID="lblState" runat="server" Text="State:"></asp:Label><span style="color: #ff0000">*</span>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="DdlState" CssClass="droplist" runat="server" AutoPostBack="false">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="width: 196px">
                                                                            <asp:Label ID="lblLocation" runat="server" Text="New Location:"></asp:Label>
                                                                            <span style="color: #ff0000">*</span>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtNewLocation" runat="server" MaxLength="50"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td align="left" style="width: 196px">
                                                                            <asp:Button ID="txtSaveLocatin" CssClass="savebutton" runat="server" Text="Save" OnClick="txtSaveLocatin_Click"
                                                                                OnClientClick="javascript:return validsCity();" />
                                                                        </td>
                                                                    </tr>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rdblstgender" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Text="Male" Value="M"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblMailID" runat="server" Text="Mail ID"></asp:Label><span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtMailId" runat="server" Width="98%" MaxLength="100"></asp:TextBox>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                            <asp:Label ID="lblMailWarn" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblAltMailID" runat="server" Text="Alt. Mail ID"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtAltvMail" runat="server" Width="98%" MaxLength="100"></asp:TextBox>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblPerMobile" runat="server" Text="Personal Mobile"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:UpdatePanel ID="updateMobile1" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtMobile1" runat="server" AutoPostBack="true" MaxLength="20" Width="98%"
                                                        ></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblCompMobile" runat="server" Text=" Company Mobile"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:UpdatePanel ID="updateMobile2" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtMobile2" runat="server" 
                                                        AutoPostBack="true" MaxLength="20" Width="98%"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblSkypeID" runat="server" Text="Skype ID"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtskypeid" runat="server" Width="98%" MaxLength="50"></asp:TextBox>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblQulification" runat="server" Text="Qualification"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtQualifcation" Width="98%" MaxLength="50" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px">
                                            <asp:Label ID="lblBG2" runat="server" Text="Blood Group"></asp:Label>
                                        </td>
                                        <td style="width: 300px">
                                            <asp:DropDownList ID="ddlBldGrp" CssClass="droplist" runat="server">
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
                                        <td style="width: 180px">
                                            <asp:Label ID="lblMOle1" runat="server" Text="Mole 1"></asp:Label>
                                        </td>
                                        <td colspan="2" style="vertical-align: top; text-align: justify">
                                            <asp:TextBox ID="txtMole1" runat="server" MaxLength="50" Width="600px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px">
                                            <asp:Label ID="lblMole2" runat="server" Text="Mole 2"></asp:Label>
                                        </td>
                                        <td colspan="2" style="vertical-align: top; text-align: justify">
                                            <asp:TextBox ID="txtMole2" runat="server" MaxLength="50" Width="600px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="center">
                                            <asp:Button ID="btnNextPer" runat="server" Text="Next" CssClass="savebutton" OnClientClick="javascript:return ValidPersonalDetails();"
                                                OnClick="btnNextPer_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        
                        <cc1:TabPanel runat="server" HeaderText="Upload Image" ID="tabUPDImage" Enabled="false">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblPhotoUpLoad" runat="server" Text="Photo Upload"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:FileUpload ID="fudPhoto" runat="server" Width="100%" />
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="center">
                                            <asp:Button ID="btnPretUPD" runat="server" Text="Previous" CssClass="savebutton"
                                                OnClick="btnPretUPD_Click" />
                                            <asp:Button ID="btnNextUPD" runat="server" Text="Next" CssClass="savebutton" OnClick="btnNextUPD_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel runat="server" HeaderText="Dependencies" ID="tabDepen" Enabled="false">
                            <ContentTemplate>
                                <table id="Table1" runat="server" width="100%">
                                    <tr runat="server">
                                        <td style="width: 180px;" runat="server">
                                            <asp:Label ID="lblAddDepen" runat="server" Text="Add Dependencies"></asp:Label>
                                        </td>
                                        <td style="width: 300px;" runat="server">
                                            <asp:DropDownList ID="ddlDependies" AutoPostBack="True" CssClass="droplist" runat="server"
                                                OnSelectedIndexChanged="ddlDependies_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Father" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Mother" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="spouse" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Son" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="Daughter" Value="5"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify" runat="server">
                                        </td>
                                    </tr>
                                    <tr id="trDepdName" runat="server">
                                        <td style="width: 180px;" runat="server">
                                            <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                                        </td>
                                        <td style="width: 300px;" runat="server">
                                            <asp:TextBox ID="txtDepndName" Width="175px" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify" runat="server">
                                        </td>
                                    </tr>
                                    <tr id="trDepdAge" runat="server">
                                        <td style="width: 180px;" runat="server">
                                            <asp:Label ID="lblDOB1" runat="server" Text="DOB"></asp:Label>
                                        </td>
                                        <td style="width: 300px;" runat="server">
                                            <asp:TextBox ID="txtDepndAge" Width="100px" runat="server"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDepndAge"
                                                PopupButtonID="txtDepndAge" Format="dd/MM/yyyy" Enabled="True">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify" runat="server">
                                        </td>
                                    </tr>
                                    <tr id="trDepdBGrp" runat="server">
                                        <td style="width: 180px;" runat="server">
                                            <asp:Label ID="lblBG" runat="server" Text="Blood Group"></asp:Label>
                                        </td>
                                        <td style="width: 300px;" runat="server">
                                            <asp:TextBox ID="txtDepndBGroup" Width="80px" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify" runat="server">
                                        </td>
                                    </tr>
                                    <tr id="trDepdGender" runat="server">
                                        <td style="width: 180px;" runat="server">
                                            <asp:Label ID="lblGender1" runat="server" Text="Gender"></asp:Label>
                                        </td>
                                        <td style="width: 300px;" runat="server">
                                            <asp:DropDownList ID="ddlDepGender" CssClass="droplist" runat="server">
                                                <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:Button ID="btnDepdAdd" CssClass="savebutton" runat="server" Text="Save" OnClientClick="javascript:return ValidFamily();"
                                                OnClick="btnDepdAdd_Click" />
                                        </td>
                                        <td style="vertical-align: top; text-align: justify" runat="server">
                                        </td>
                                    </tr>
                                    <tr id="trFamilyGrid" runat="server">
                                        <td colspan="2" runat="server">
                                            <asp:GridView ID="gvFamily" AutoGenerateColumns="False" CssClass="gridview" runat="server"  OnRowCommand="gvFamily_RowCommand">
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
                                                                runat="server">Edit</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" CommandName="del" runat="server">Delete</asp:LinkButton></ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify" runat="server">
                                        </td>
                                    </tr>
                                    <tr runat="server">
                                        <td runat="server">
                                        </td>
                                    </tr>
                                    <tr runat="server">
                                        <td runat="server">
                                        </td>
                                        <td align="center" runat="server">
                                            <asp:Button ID="btnPreDepen" runat="server" Text="Previous" CssClass="savebutton"
                                                OnClick="btnPreDepen_Click" />
                                            <asp:Button ID="btnNextDepen" runat="server" Text="Next" CssClass="savebutton" OnClick="btnNextDepen_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="tabJobDetails" runat="server" HeaderText="Job Details" Enabled="false">
                            <HeaderTemplate>
                                Job Details
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblHisID" runat="server" Text=" Historical ID"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtOldEmpID" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px; height: 21px;">
                                            <asp:Label ID="lblDesig" runat="server" Text="Designation"></asp:Label><span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px; height: 21px;">
                                            <asp:DropDownList ID="ddlDesignation" CssClass="droplist" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblTrade" runat="server" Text="Trades(Expertise)"></asp:Label><span
                                                style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="droplist">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblEmptype" runat="server" Text="Employee Type"></asp:Label><span
                                                style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:DropDownList ID="ddlEmpType" runat="server" CssClass="droplist" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlEmpType_SelectedIndexChanged">
                                                <asp:ListItem Text="Choose Employee Type" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Director" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="Executive Director" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="Project Manager" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Department Head" Value="2"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Section Head"></asp:ListItem>
                                                <asp:ListItem Selected="True" Value="4" Text="General"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblEmpNature" runat="server" Text=" Employee Nature"></asp:Label><span
                                                style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:DropDownList ID="ddlEmpnature" CssClass="droplist" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblWS" runat="server" Text="Worksite"></asp:Label><span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:DropDownList ID="ddlWorksite" CssClass="droplist" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlWorksite_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblDept" runat="server" Text="Department"></asp:Label><span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:DropDownList ID="ddldept" CssClass="droplist" AutoPostBack="True" runat="server"
                                                OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblRptTo" runat="server" Text="Reporting To"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:DropDownList ID="ddlManager" runat="server" CssClass="droplist">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 200px;">
                                            <asp:Label ID="lblDateOfJoin" runat="server" Text="Date Of Join(dd/MM/yyyy)"></asp:Label>
                                            <span style="color: #ff0000">*</span>
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtDoj" runat="server"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDoj"
                                                PopupButtonID="txtDoj" Format="dd/MM/yyyy" Enabled="True">
                                            </cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender FilterType="Custom, Numbers" ID="FTextBoxExtender" runat="server"
                                                TargetControlID="txtDoj" ValidChars="/" Enabled="True">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblSal" runat="server" Text="Salary (CTC per Annum)"></asp:Label>
                                            <span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtSal" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblShift" runat="server" Text="Shift"></asp:Label>
                                            <span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="droplist">
                                                <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Day" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Night" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Shift-A" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Shift-B" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="Shift-C" Value="5"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;* Day Shift is default
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="center">
                                            <asp:Button ID="btnPre" runat="server" Text="Previous" CssClass="savebutton" OnClick="btnPre_Click" />
                                            <asp:Button ID="btnJobNext" runat="server" Text="Next" CssClass="savebutton" OnClick="btnJobNext_Click"
                                                OnClientClick="javascript:return ValidJobDetails();" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel runat="server" HeaderText="Communication" ID="tabCommunication" Enabled="false">
                            <ContentTemplate>
                                <table style="width: 100%">
                                    <tr>
                                        <td class="subheader" colspan="3">
                                            <asp:Label ID="lblPrstAdd" runat="server" Text="Present Address"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                                            <span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtAddress" runat="server" Height="47px" Rows="6" Width="98%" MaxLength="100"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                                            <span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtCity" runat="server" Width="98%" MaxLength="50">Hyderabad</asp:TextBox>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblState1" runat="server" Text="State"></asp:Label>
                                            <span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtState" runat="server" Width="97%" MaxLength="50">Andhra Pradesh</asp:TextBox>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblCountry1" runat="server" Text="Country"></asp:Label>
                                            <span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtCountry" runat="server" Width="98%" MaxLength="50">INDIA</asp:TextBox>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify; height: 19px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblPIN" runat="server" Text="PIN"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:UpdatePanel ID="updPnlPIN" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtPIN" runat="server" AutoPostBack="true" MaxLength="6" Width="98%"
                                                        ></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTEPin" runat="server" FilterMode="ValidChars" FilterType="Numbers,Custom"
                                                        TargetControlID="txtPIN" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:UpdatePanel ID="updpnlPhone" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtPhone" runat="server" AutoPostBack="true" MaxLength="20" Width="98%"
                                                        ></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender FilterType="Custom, Numbers" ID="FilteredTextBoxExtender2"
                                                        runat="server" TargetControlID="txtPhone" Enabled="True">
                                                    </cc1:FilteredTextBoxExtender>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="subheader" colspan="3">
                                            <asp:Label ID="lblPermanentAdd" runat="server" Text="Permanent Address"></asp:Label>
                                            <asp:CheckBox ID="chkAddress" runat="server" Font-Bold="False" Font-Italic="True"
                                                Text="Check if permanent address same as present address" AutoPostBack="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblAddress1" runat="server" Text="Address"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtPer_Address" runat="server" Height="47px" Rows="6" Width="98%"
                                                MaxLength="100" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblCity1" runat="server" Text="City"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtPer_City" runat="server" Width="98%" MaxLength="50">Hyderabad</asp:TextBox>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblState2" runat="server" Text="State"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtPer_State" runat="server" Width="97%" MaxLength="50">Andhra Pradesh</asp:TextBox>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px; height: 23px;">
                                            <asp:Label ID="lblCountry2" runat="server" Text="Country"></asp:Label>
                                        </td>
                                        <td style="width: 300px; height: 23px;">
                                            <asp:TextBox ID="txtPer_Country" runat="server" Width="98%" MaxLength="50">INDIA</asp:TextBox>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify; height: 23px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblPIN1" runat="server" Text="PIN"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:UpdatePanel ID="updpnlPerPin" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtPer_PIN" runat="server" AutoPostBack="true" MaxLength="6" Width="98%"
                                                        ></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars"
                                                        FilterType="Numbers,Custom" TargetControlID="txtPer_PIN" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblPhone1" runat="server" Text="Phone"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:UpdatePanel ID="UpdtxtPer_Phone" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtPer_Phone" runat="server" AutoPostBack="true" MaxLength="20"
                                                        Width="98%" ></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="center">
                                            <asp:Button ID="btnPreAdd" runat="server" Text="Previous" CssClass="savebutton" OnClick="btnPreAdd_Click" />
                                            <asp:Button ID="btnNextAdd" runat="server" Text="Next" CssClass="savebutton" OnClick="btnNextAdd_Click"
                                                OnClientClick="javascript:return ValidCommunication();" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel runat="server" HeaderText=" Account Details" ID="tabAccDetails" Enabled="false">
                            <HeaderTemplate>
                                Account Details
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblCompACNum" runat="server" Text="Company A/C Number"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtAccNo" runat="server" Width="296px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblBankName" runat="server" Text="Bank Name"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:DropDownList ID="ddlBank" AutoPostBack="True" runat="server" CssClass="droplist"
                                                OnSelectedIndexChanged="ddlBank_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblBranchName" runat="server" Text="Branch Name"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:DropDownList ID="ddlBranch" CssClass="droplist" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblPANNumber" runat="server" Text="PAN Number"></asp:Label>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtPan" MaxLength="10" runat="server"></asp:TextBox>
                                            &nbsp; Ex: ABCPD1234E
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            <asp:Button ID="btnPreAccDetails" runat="server" Text="Previous" CssClass="savebutton"
                                                OnClick="btnPreAccDetails_Click" />
                                            <asp:Button ID="btnNextAccDetails" runat="server" Text="Next" CssClass="savebutton"
                                                OnClick="btnNextAccDetails_Click" OnClientClick="javascript:return ValidAccountDetails();" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel runat="server" HeaderText="Authentication Details" ID="tabAuthen" Enabled="false">
                            <HeaderTemplate>
                                Authentication Details
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table style="width: 100%">
                                    <tr>
                                        <td class="subheader" colspan="3">
                                            Authentication Details
                                            <asp:CheckBox ID="chkPWD" runat="server" Font-Bold="False" Font-Italic="True" AutoPostBack="True"
                                                Text="If you Check, UserName and Password are mandatory" OnCheckedChanged="chkPWD_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 180px;">
                                            <asp:Label ID="lblUserName" runat="server" Text="Username"></asp:Label><span style="color: #ff0000">*</span>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtUsername" runat="server" AutoPostBack="true" MaxLength="20" Width="175"
                                                                    OnTextChanged="txtUsername_TextChanged"></asp:TextBox>
                                                                <asp:Label ID="lblUserAvailable" runat="server" Font-Bold="True" ForeColor="Red"
                                                                    Text="Label" Visible="False"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td width="300Px">
                                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                            DisplayAfter="1" DynamicLayout="False">
                                                            <ProgressTemplate>
                                                                <img id="Img1" src="Images/ajax-loader.gif" runat="server" style="text-align: left;"
                                                                    alt="0" />
                                                                Checking ...
                                                            </ProgressTemplate>
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
                                            <asp:Label ID="lblPWD" runat="server" Text="Password"></asp:Label><span style="color: #ff0000">*</span>
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtPassword" runat="server" Width="175px" TextMode="Password" MaxLength="20"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblConfPWD" runat="server" Text="Confirm Password"></asp:Label><span
                                                style="color: Red" id="spncnfpwd" runat="server">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtReenterPassword" runat="server" TextMode="Password" Width="175px"
                                                MaxLength="20" CssClass="text_black"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 300px;">
                                            <asp:Button ID="btnPreAuethen" runat="server" Text="Previous" CssClass="savebutton"
                                                OnClick="btnPreAuethen_Click" />
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="javascript:return valids();"
                                                CssClass="savebutton" Height="21px" OnClick="btnSubmit_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 300px;">
                                            &nbsp;
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 150px;">
                                        </td>
                                        <td style="width: 300px;">
                                        </td>
                                        <td style="vertical-align: top; text-align: justify">
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="lblDomain" runat="server" />
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
            <ProgressTemplate>
                <img src="IMAGES/updateProgress.gif" alt="update is in progress" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
