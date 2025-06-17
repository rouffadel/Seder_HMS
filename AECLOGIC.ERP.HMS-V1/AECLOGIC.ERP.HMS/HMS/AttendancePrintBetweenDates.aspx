<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendancePrintBetweenDates.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.AttendancePrintBetweenDates" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Attendance Preview</title>
    <link href="Includes/CSS/StyleSheet.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <div id="divMenu" style="height: 25px; position: absolute; top: 60px; visibility: visible; width: 100px">
        <table border="0" cellpadding="0" cellspacing="0">
            <tbody>
                <tr>
                    <td>
                        <img border="0" alt="Print" onclick="javascript: divMenu.style.display='none'; window.print();  window.close();"
                            class="right" src="Images/print.png" />
                        <br />
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
            var yMenuFrom, yMenuTo, yOffset, timeoutNextCheck;
            //jjjj = 805;
            if (isNS4) {
                yMenuFrom = divMenu.top;
                yMenuTo = windows.pageYOffset + 137;   // À§ÂÊ À§Ä¡
            } else if (isDOM) {
                yMenuFrom = parseInt(divMenu.style.top, 10);
                yMenuTo = (isNS ? window.pageYOffset : document.body.scrollTop) + 50; // À§ÂÊ À§Ä¡
            }
            timeoutNextCheck = 500;
            if (yMenuFrom != yMenuTo) {
                yOffset = Math.ceil(Math.abs(yMenuTo - yMenuFrom) / 20);
                if (yMenuTo < yMenuFrom)
                    yOffset = -yOffset;
                if (isNS4)
                    divMenu.top += yOffset;
                else if (isDOM)
                    divMenu.style.left = 50; //¹è³Ê ¿ÞÂÊ À§Ä¡
                //divMenu.style.left =  parseInt (divMenu.style.left, 10) - yOffset;
                divMenu.style.top = parseInt(divMenu.style.top, 10) + yOffset;
                timeoutNextCheck = 10;
            }
            setTimeout("moveRightEdge()", timeoutNextCheck);
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
        <table>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblAtrprt" runat="server" CssClass="pageheader" Text="Attendance Report"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">Worksite &nbsp; : &nbsp;<asp:Label ID="lblWS" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : &nbsp;<asp:Label ID="lblDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Table ID="tblAtt" runat="server" BorderWidth="2" GridLines="Both">
                    </asp:Table>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
