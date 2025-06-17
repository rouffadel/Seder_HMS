<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="OfferConfirmation.aspx.cs" ValidateRequest="false" Inherits="AECLOGIC.ERP.HMS.OfferConfirmation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Offer Confirmation</title>
    <link href="Includes/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="Includes/CSS/sddm.css" rel="stylesheet" type="text/css" />
    <link href="Includes/CSS/base.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="divMenu" style="height: 25px; position: absolute; top: 60px; visibility: visible;
            width: 100px">
            <table border="0" cellpadding="0" cellspacing="0">
                <tbody>
                    <tr>
                        <td>
                            <img border="0" id="printSpan" alt="Print" onclick="window.open('PrintOfferLetter.aspx','PrintWindow','width=800,height=700,toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes');"
                                style="color: Blue; font-weight: bold; text-decoration: underline;" class="right" src="Images/print.png" />
                           <img border="0" alt="Close" onclick="javascript: divMenu.style.display='none'; window.close();"
                                    class="right" src="Images/close.png" /></td>
                    </tr>
                </tbody>
            </table>
        </div>

        <script type="text/javascript" language="javascript">
   

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

                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%" id="reviewContent">
        <tr>
            <td class="pageheaderprintcenter" style="width: 1199px" align="center">
                Offer Letter<br />
                <ajax:ScriptManager ID="ScriptManager1" runat="server">
                </ajax:ScriptManager>
            </td>
        </tr>
        <tr>
            <td style="width: 1199px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            <b>
                                <asp:Label ID="lblName" runat="server"></asp:Label></b></td>
                        <td style="text-align: right; vertical-align: top;">
                            <b>&nbsp;<asp:Label ID="lblDate" runat="server"></asp:Label></b></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <b>
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
                                </table>
                            </b>
                        </td>
                        <td style="text-align: right; vertical-align: top;">
                            <asp:Image ID="imgPhoto" runat="server" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                            <div id="TextEditor" style="width: 100%; height: auto; <%--word-wrap: break-word;--%> text-align: justify"
                                runat="server">
                                <br />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 20px">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <strong>For
                                <asp:Label ID="Label1" runat="server" Text="<%$ AppSettings:Company %>"></asp:Label><br />
                                
                            </strong>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            &nbsp;
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
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
                            I shall join duty on
                             <asp:TextBox ID="txtDOJ" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender
                                TargetControlID="txtDOJ" PopupButtonID="txtDOJ" ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>.
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; line-height: 30px">
                            &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <span style="float: left; display: block; width: 80px;">Posting At</span> :<asp:Label
                                ID="lblSite" runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; display: inline; height: 13px;">
                            <span style="float: left; display: block; width: 80px;">Date</span>:<asp:Label ID="lblAppDate"
                                runat="server"></asp:Label>
                        </td>
                        <td align="center" style="height: 13px; text-align: right">
                            <strong>Signature of Candidate</strong></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="vertical-align: top; text-align: center;">
                            <br />
                            <br />
                          
                            <asp:TextBox ID="txtRTB" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                                BorderWidth="0px" ForeColor="White" Height="1px" scroll="false" TextMode="MultiLine"
                                Width="1px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 20px; width: 1199px;">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="pageheader" id="tdaccept" runat="server">Click this link for your Acceptence::&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkAccept" runat="server" Text="I Accept" ></asp:LinkButton></td>
        
        </tr>
        <tr>
            <td style="height: 20px; width: 1199px;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 20px; width: 1199px;">
                <asp:Button ID="Button2" runat="server" CssClass="savebutton" 
                    onclick="Button2_Click" Text="Back" />
            </td>
        </tr>
        <tr>
            <td style="height: 20px; width: 1199px;">
                &nbsp;</td>
        </tr>
    </table>
    
    </form>
</body>
</html>
