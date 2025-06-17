<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TerminationLetterPreview.aspx.cs" ValidateRequest="false" Inherits="AECLOGIC.ERP.HMS.TerminationLetterPreview" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
 <title>Offer Letter Preview</title>
 <link href="Includes/CSS/StyleSheet.css"type="text/css" rel="Stylesheet" />
    <style type="text/css">
        .style1
        {
            height: 36px;
        }
        .style2
        {
            height: 15px;
        }
        .style3
        {
            height: 13px;
        }
        .style4
        {
            width: 1199px;
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
     <div id="divMenu" style="height: 25px; position: absolute; top: 10px; visibility: visible;">
            <table border="0" cellpadding="0" cellspacing="0">
                <tbody>
                    <tr>
                        <td>
                           <img border="0" id="printSpan" alt="Print" onclick="javascript: divMenu.style.display='none';window.print();window.close();"
                                style="color: Blue; font-weight: bold; text-decoration: underline;" class="right" src="Images/print.png" />
                           <img border="0" alt="Close" onclick="javascript: divMenu.style.display='none'; window.close();"
                                    class="right" src="Images/close.png" />
                       </td>
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
		yMenuTo     = windows.pageYOffset + 137;   // À§ÂÊ À§Ä¡
	} else if (isDOM) {
		yMenuFrom   = parseInt (divMenu.style.top, 10);
		yMenuTo     = (isNS ? window.pageYOffset : document.body.scrollTop) + 50; // À§ÂÊ À§Ä¡
	}
	timeoutNextCheck = 500;

	if (yMenuFrom != yMenuTo) {
		yOffset = Math.ceil(Math.abs(yMenuTo - yMenuFrom) / 20);
		if (yMenuTo < yMenuFrom)
			yOffset = -yOffset;
		if (isNS4)
			divMenu.top += yOffset;
		else if (isDOM)
			divMenu.style.left =  50; //¹è³Ê ¿ÞÂÊ À§Ä¡
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
            <td class="style4">
                            <asp:Button ID="btnBack" runat="server" CssClass="savebutton" 
                                onclick="btnBack_Click" Text="Back" />
                            <asp:Button ID="btnEdit" runat="server" CssClass="savebutton" 
                                onclick="btnEdit_Click" Text="Edit" />
            </td>
        </tr> 
        <tr>
            <td class="pageheaderprintcenter"  align="center">
                Termination Letter<br />
            </td>
        </tr>
        
        <tr>
            <td style="width: 1199px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr><td align="right"> 
            <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
            </td></tr>
                <tr><td class="style3"><b>To:</b></td></tr>
                <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td>
                            <b>
                                <asp:Label ID="lblEmp" runat="server"></asp:Label>  &nbsp;<asp:Label ID="lblName" runat="server"></asp:Label></b></td>
                        <td style="text-align: right; vertical-align: top;">
                            </td>
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
                                        <td class="style2">
                                            <asp:Label ID="lblState" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCountry" runat="server"></asp:Label><asp:Label ID="lblPin" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPhone" runat="server"></asp:Label>.</td>
                                    </tr>
                                </table>
                            </b>
                        </td>
                        <td style="text-align: right; vertical-align: top;">
                            </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                            <div id="TextEditor" style="width: 100%; height: auto;  text-align: justify"
                                runat="server">
                                <br />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 20px">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            Dear
                            <b><asp:Label ID="lblEmpName" runat="server"></asp:Label></b>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            &nbsp;
                            <p style="MARGIN: 0px; TEXT-ALIGN: justify">
                                I regret to inform you that your services are terminated from date due to not 
                                delivering duties and responsibilities as desired by the Management.
                            </p>
                            <p style="MARGIN: 0px; TEXT-ALIGN: justify">
                                <br />
                            </p>
                            <p style="MARGIN: 0px; TEXT-ALIGN: justify">
                                Further you are hereby informed to submit all the assets belonging to the 
                                company.</p>
                        </td>
                        <td>
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
                            <strong>For
                                <asp:Label ID="Label1" runat="server" Text="<%$ AppSettings:Company %>"></asp:Label></strong></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; line-height: 30px" class="style1">
                            &nbsp;
                        </td>
                        <td class="style1">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            Authorized Signatory</td>
                        <td>
                        </td>
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
                           <b> CC to:Dy General Manager(Accounts)</b><td>&nbsp;</td>&nbsp;</td>
        </tr>
        
    </table>
            



 
 
  
     
            



 
 
    </form>
</body>
</html>

