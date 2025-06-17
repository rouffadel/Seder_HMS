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
 <div class="col-md-12 nogaps" id="sidenavigation">
            <script src="../Includes/JS/Tooltip.js" type="text/javascript" language="javascript"></script>
            <script type="text/javascript">
                var t1 = null;
                addLoadEvent(function ToolTipInit() {
                    t1 = new ToolTip("dvToolTip", false);
                });
            </script>
     <div class="sidebar-form">
        <%-- <div class="input-group" runat="server" id="prjDiv" visible="false">
                <asp:Label ID="lblPrjName" CssClass="salutation" runat="server"></asp:Label>[
                                <asp:LinkButton ID="lnkChangeProject" runat="server" AccessKey="P" Text="Change"
                                    OnClick="lnkChangeProject_Click" CssClass="salutation" ForeColor="Yellow"></asp:LinkButton>]
             </div>--%>
            <div class="input-group">
               <asp:TextBox ID="txtsearch" CssClass="form-control" AccessKey="q" runat="server" placeholder="Search..." ></asp:TextBox>
                <span class="input-group-btn">
                  <asp:Button ID="SearchButton" ToolTip="Search"  runat="server" class="btn btn-flat" onclick="SearchButton_Click" />    
              </span>
            </div></div>
             
            <div class="navbar" id="navbar" runat="server">
            </div>
            <div id="dvToolTip" class="selected" style="background-color: #fffff0; padding: 3px 3px 3px 3px;
                width: 100px; height: 50px; top: -60px; position: absolute; border: solid 1px gray;
                text-align: left;">
            </div>
            <script type="text/javascript" src="../Includes/JS/xpmenuv21.js"></script> 
</div>
 