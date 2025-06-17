<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="AppointmentPreview.aspx.cs" ValidateRequest="false" Inherits="AECLOGIC.ERP.HMS.AppointmentPreview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">



    <title>Appointment Preview</title>
     <link href="Includes/CSS/StyleSheet.css" type="text/css" rel="Stylesheet" />


     <div id="divMenu" style="height: 25px; position: absolute; top: 60px; visibility: visible;
            width: 100px">
            <table border="0" cellpadding="0" cellspacing="0">
                <tbody>
                
                    <tr>
                        <td>
                            <img border="0" alt="Print" onclick="javascript: divMenu.style.display='none'; window.print();  window.close();"
                                class="right" src="Images/print.png" /><img border="0" alt="Close" onclick="javascript: divMenu.style.display='none'; window.close();"
                                    class="right" src="Images/close.png" /></td>
                    </tr>
                </tbody>
            </table>
        </div>

        <script>
   

var isDOM = (document.getElementById ? true : false);
var isIE4 = ((document.all && !isDOM) ? true : false);
var isNS4 = (document.layers ? true : false);
var isNS = navigator.appName == "Netscape";

function getRef(id) {
	if (isDOM) return document.getElementById(id);
	if (isIE4) return document.all[id];
	if (isNS4) return document.layers[id];
}

function moveRightEdge() {
	var yMenuFrom, yMenuTo, yOffset, timeoutNextCheck ;

//jjjj = 805;
	if (isNS4) {
		yMenuFrom   = divMenu.top;
		yMenuTo     = windows.pageYOffset + 137;   // 위쪽 위치
	} else if (isDOM) {
		yMenuFrom   = parseInt (divMenu.style.top, 10);
		yMenuTo     = (isNS ? window.pageYOffset : document.body.scrollTop) + 50; // 위쪽 위치
	}
	timeoutNextCheck = 500;

	if (yMenuFrom != yMenuTo) {
		yOffset = Math.ceil(Math.abs(yMenuTo - yMenuFrom) / 20);
		if (yMenuTo < yMenuFrom)
			yOffset = -yOffset;
		if (isNS4)
			divMenu.top += yOffset;
		else if (isDOM)
			divMenu.style.left =  50; //배너 왼쪽 위치
			//divMenu.style.left =  parseInt (divMenu.style.left, 10) - yOffset;
			divMenu.style.top = parseInt (divMenu.style.top, 10) + yOffset;
			timeoutNextCheck = 10;
	}
	setTimeout ("moveRightEdge()", timeoutNextCheck);
}

if (isNS4) {
	var divMenu = document["divMenu"];
	divMenu.top = top.pageYOffset + 0;
	divMenu.visibility = "visible";
	moveRightEdge();
} else if (isDOM) {
	var divMenu = getRef('divMenu');
	divMenu.style.top = (isNS ? window.pageYOffset : document.body.scrollTop) + 0;
	divMenu.style.visibility = "visible";
	moveRightEdge();
}

</script>
        <div >
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
   
        <tr>
            <td class="pageheaderprintcenter" style="width: 1199px">
                Appointment Letter<br /></td>
        </tr>
        <tr>
            <td style="width: 1199px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr><td><b><asp:Label ID="lblDate" runat="server"></asp:Label></b> </td></tr>
                <tr><td></td></tr>
                <tr>
                        <td><b>
                           EmpID: <asp:Label ID="lblEmpID" runat="server"></asp:Label> &nbsp;</b></td>
                        <td style="text-align: right; vertical-align: top;"><b>
                            &nbsp;</b></td>
                    </tr>
                    <tr>
                        <td><b>
                            <asp:Label ID="lblName" runat="server"></asp:Label></b></td>
                        <td style="text-align: right; vertical-align: top;"><b>
                            &nbsp;</b></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top"><b>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAddress" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="height: 20px">
                                        <asp:Label ID="lblCity" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblState" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCountry" runat="server"></asp:Label><asp:Label ID="lblPin" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPhone" runat="server"></asp:Label></td>
                                </tr>
                            </table></b>
                        </td>
                        <td style="text-align: right; vertical-align: top;">
                            <asp:Image ID="imgPhoto" runat="server" /></td>
                    </tr>
                    <tr>
                        <td colspan="2"><br />
                                        <div id="TextEditor" contenteditable="true" style="width: 100%; height: auto;  text-align:justify" runat="server">
                                            <br />
                                        </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 20px">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top"><strong>
                            For
                            <asp:Label ID="Label1" runat="server" Text="<%$ AppSettings:Company %>"></asp:Label></strong></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">&nbsp;
                            <br />
                            <br />
                            <asp:ImageButton ID="ImgAuth" ImageUrl="~/AuthorisedSign/83.jpg" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; height: 20px">
                            <strong>Authorised Signatory.</strong></td>
                        <td style="height: 20px">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; line-height: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            I accept the above terms and conditions of my offer of appointment with the organization.
                            I shall join duty on <asp:Label ID="lblDoj" runat="server"></asp:Label>.
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; line-height: 30px"> &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;   ">
                            <span style="float: left; display:block; width:80px;">Posting At</span> :<asp:Label ID="lblSite" runat="server"></asp:Label>
                            </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; display:inline; height: 13px;">
                             <span style="float: left; display:block; width:80px;">Date</span>:<asp:Label ID="lblAppDate" runat="server"></asp:Label>
                        </td>
                        <td align="center" style="height: 13px; text-align: right">
                            <strong>Signature of Candidate</strong></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="vertical-align: top; text-align:center; "> <br />
                        <br />
                        
                            <asp:TextBox ID="txtRTB" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                                BorderWidth="0px" ForeColor="White" Height="1px" scroll="false" TextMode="MultiLine"
                                Width="1px"></asp:TextBox>
                            
                        
                        </td>
                    </tr>
                     <tr><td>
                         &nbsp;</td></tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 20px; width: 1199px;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 1199px">
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
