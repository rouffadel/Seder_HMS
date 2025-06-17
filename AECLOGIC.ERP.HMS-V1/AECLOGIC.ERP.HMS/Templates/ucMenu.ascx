<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ucMenu.ascx.cs" Inherits="AECLOGIC.ERP.COMMON.ucMenu1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
<table border="0" cellpadding="0" cellspacing="0" style="height: 600px; width:110px;" >
    <tr style="height: 50px;">
        <td id="sidenavigation">
            <script src="../Includes/JS/Tooltip.js" type="text/javascript" language="javascript"></script>
            <script type="text/javascript">
                var t1 = null;
                addLoadEvent(function ToolTipInit() {
                    t1 = new ToolTip("dvToolTip", false);
                });

                function searchclick()
                {
                    document.getElementById('ucMenu1_SearchButton').click();
                }
            </script>
               <div class="sidebar-form">
            <div class="input-group">
                  <asp:TextBox ID="txtsearch" CssClass="form-control" AccessKey="q" runat="server"  ></asp:TextBox> 
              <span class="input-group-btn">
                 <button id="Search" title="Search" onclick="searchclick()"  class="btn btn-flat" >
                   <i class="fa fa-search"></i>
               </button>
                <asp:Button ID="SearchButton" runat="server" style="display:none" />
              </span>
            </div></div>

            <div style="text-align: left;">
              
             
            </div>
            <div class="navbar" id="navbar" runat="server">
            </div>
            <div id="dvToolTip" class="selected" style="background-color: #fffff0; padding: 3px 3px 3px 3px;
                width: 110px; height: 50px; top: -60px; position: absolute; border: solid 1px gray;
                text-align: left;">
            </div>
           <script type="text/javascript" src="../Includes/JS/xpmenuv21.js"></script>
        </td>
    </tr>
</table>
