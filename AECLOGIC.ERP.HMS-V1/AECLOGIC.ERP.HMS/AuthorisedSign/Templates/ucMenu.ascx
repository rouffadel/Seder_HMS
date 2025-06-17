<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ucMenu.ascx.cs" Inherits="AECLOGIC.ERP.COMMON.ucMenu1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link rel="stylesheet" type="text/css" href="Includes/CSS/base.css" />
<link rel="stylesheet" type="text/css" href="Includes/CSS/sddm.css" />
<style type="text/css">
    .txtboxw
    {
        -moz-border-radius-topleft: 30px;
        -webkit-border-top-left-radius: 30px;
        -moz-border-radius-bottomleft: 30px;
        -webkit-border-bottom-left-radius: 30px;
        -moz-border-radius-topright: 30px;
        -webkit-border-top-right-radius: 30px;
        -moz-border-radius-bottomright: 30px;
        -webkit-border-bottom-right-radius: 30px;
    }
</style>
<table border="0" cellpadding="0" cellspacing="0" style="height: 600px">
    <tr style="height: 50px;">
        <td id="sidenavigation">
            <script src="Includes/JS/Tooltip.js" type="text/javascript" language="javascript"></script>
            <script type="text/javascript">
                var t1 = null;
                addLoadEvent(function ToolTipInit() {
                    t1 = new ToolTip("dvToolTip", false);
                });
            </script>
            <div style="text-align: left;">
                <asp:TextBox ID="txtsearch" CssClass="txtboxw" AccessKey="q" runat="server"
                    Width="136px" ></asp:TextBox>
                <asp:ImageButton ID="SearchButton" ToolTip="Search" ImageUrl="~/Images/seaching.jpg"
                    runat="server" Height="14px" onclick="SearchButton_Click" />              
            </div>
            <div class="navbar" id="navbar" runat="server">
            </div>
            <div id="dvToolTip" class="selected" style="background-color: #fffff0; padding: 3px 3px 3px 3px;
                width: 100px; height: 50px; top: -60px; position: absolute; border: solid 1px gray;
                text-align: left;">
            </div>
            <script type="text/javascript" src="Includes/JS/xpmenuv21.js"></script>
        </td>
    </tr>
</table>
