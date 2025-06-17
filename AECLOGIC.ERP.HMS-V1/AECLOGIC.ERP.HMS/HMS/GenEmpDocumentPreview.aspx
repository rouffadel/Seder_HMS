<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="GenEmpDocumentPreview.aspx.cs" Inherits="AECLOGIC.ERP.HMS.GenEmpDocumentPreview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

    <title>Documets Preview</title>
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

<div>
   <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td class="pageheaderprintcenter" style="width: 1199px">
        </tr>
        
        <tr>
                        <td>
                            <b>
                                <asp:Label ID="lblName" runat="server"></asp:Label></b>
                        </td>
                        
                    </tr>

                     <tr>
                                        <td>
                                            <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                        </td>
                     </tr>

                     <tr>
                                        <td style="height: 20px">
                                            <asp:Label ID="lblCity" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblState" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCountry" runat="server"></asp:Label><asp:Label ID="lblPin" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                        </td>
                                    </tr>

        <tr>
                        <td colspan="2"><br />
                                        <div id="TextEditor" contenteditable="true" style="width: 100%; height: auto; text-align:justify" runat="server">
                                            <br />
                                        </div>
                        </td>
                    </tr>

</div>

</asp:Content>