♠<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Logon.aspx.cs" ClientIDMode="Static" Inherits="AECLOGIC.ERP.COMMON.Logon" title="Login"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %><%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/ucmsg.ascx" TagName="msgshow" TagPrefix="AECmsg" %>
<!DOCTYPE html><html><head id="Head1" runat="server"><title>Default</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
     <link href="~/Content/bootstrap.css" rel="stylesheet" />
     <link href="Login.css" rel="stylesheet" />
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>--%>
    <%--<script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>--%>
    <script type="text/javascript" language="javascript">

        var ComapanyName = "";
        function validatesave() {

            if (document.getElementById('txtUserName').value == "") {
                alert("Please Enter Username");
                document.getElementById('txtUserName').focus();
                return false;

            }
            if (document.getElementById('txtPasword').value == "") {
                alert("Please Enter Password");
                document.getElementById('txtPasword').focus();
                return false;

            }
            PageMethods.GetCurrentTime(document.getElementById('txtUserName').value, OnSuccess);
            //var Res = PageMethods.CheckMultipleLogin();
            //if (Res.value == "Already Logged in")
            //{
            //    setTimeout(function () {
            //        $("#dvlogout").fadeTo(2000, 500).slideUp(500, function () {
            //            $("#dvlogout").remove();
            //        });
            //    }, 5000);
            //    var Res = PageMethods.CheckMultipleLogout();
            //}
            //return true;
        }
        function OnSuccess(response, userContext, methodName) {
            alert(response);
        }
    </script>
    
    <style type="text/css">

        .has-feedback .Custom__login_input{
           padding-right: 42.5px; 
        }

        .login-logo a,.login-logo a b{
       color: black;
    font-size: 22px;
    font-weight: normal;
    text-decoration: none !important;
        }
        .form-control-feedback {   font-size: 19px;}


        .Custom__login_input {
   border-color: #d2d6de !important;
    color: #555 !important;
    display: block;
    font-size: 14px !important;
    height: 34px !important;
    max-width: 100% !important;
    width: 100% !important;
    border-radius: 0 !important;
     -moz-border-radius: 0 !important;
      -webkit-border-radius: 0 !important;
    box-shadow: none !important;
    -moz-box-shadow: none !important;
    -webkit-box-shadow: none !important;
        }

        .cust__login_btn{
        
    background-color: #3c8dbc;
    border-color: #367fa9;
    display: block; 
    float:right;border:none;
     font-size: 14px; 
     padding: 6px 12px; 
     text-align: center;
      vertical-align: middle;
    white-space: nowrap;
     border-radius: 0;
     -moz-border-radius: 0;   
     -webkit-border-radius: 0;
    box-shadow: none; 
    -moz-box-shadow: none; 
   -webkit-box-shadow: none; 
}
        label {
        font-size:13px !important}


    </style></head>
    <body  class="body_img ">
        <form id="form1" runat="server" defaultfocus="txtUserName" defaultbutton="btnLogin">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>

         <div class="login-pages" id="dvlogin" runat="server">

             
  <div class="row"> 
          
                 <asp:Label ID="lblErrorMsg" style="color:red;" runat="server"></asp:Label>
  

          </div>
             <div class="row">
                <div class="col-md-12">
                    <div class="login-page-inner">
					
					
                        <div class="login-page-title">
                             <a href="#"><b>AEC ERP for Construction Management</b></a>
							<h6>Enter your User Name & Password to log on</h6>
                        </div>
                        <div class="login-form">
                         
				  <div class="form-group">
	  <asp:DropDownList ID="ddlCompany"  class="form-control selc uname" runat="server">
                </asp:DropDownList>
                         
                
                        <%--<span class="help-block" id="error"></span>  --%>
				  </div>
				  <div class="form-group">
					<div class="input-group">
                        
                       

                        <div class="input-group-addon"><span class="glyphicon glyphicon-user"></span></div>
                         <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Username" TabIndex="2" Text=""></asp:TextBox>
            
                         
                        </div>  
                        <%--<span class="help-block" id="error"></span>  --%>
				  </div>
				   <div class="form-group">
					<div class="input-group">
                        <div class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></div> 
                         <asp:TextBox ID="txtPasword" runat="server" CssClass="form-control" placeholder="Password" TabIndex="3" Text="" TextMode="Password"></asp:TextBox>
                     </div>  
               
				  </div>   <div class="form-group"> 
				    <asp:Button ID="btnLogin" TabIndex="4" runat="server" class="btn btn-primary btnW" Text="Login"
                    OnClick="btnLogin_Click" OnClientClick="javascript:return validatesave();" />
                     <%-- <button type="submit" class="">Submit</button> --%></div>
				 
                               <div class="checkbox">
					<label><asp:CheckBox ID="chkRem" runat="server" Text="Remember Me" /> <%--<input type="checkbox"> Remember me--%></label>
				  </div>

		 
                        </div>
                    </div>
                </div>
            </div>


      
    </div>
            
            
            <!-- /.login-box -->
         <div class="col-xs-6" id="dvlogout" runat="server" visible="false" style="padding: 10px 0px 30px 30px;">
            <table style="width:80%">
                <tr ><td colspan="2" align="left" ><asp:Label ID="Label1" runat="server" style="color:red;font-size:medium;">
                    1. On the previous instance, you did not logout from the AEC ERP.<br />
                                        2. System suggests you to click <b>Logout</b> button to Force LOGOUT that instance. <br />
                    3. This is to enfore data security.
                                                    </asp:Label></td></tr>
                <tr><td align="left" style="width:20%"><asp:Button ID="btnLogout" TabIndex="4" runat="server"  Text="Logout" cssclass="cust__login_btn btn-flat"
                    OnClick="btnLogout_Click"  visible="false"/></td><td align="left" style="width:20%">
                        <asp:Button ID="btnHome" TabIndex="4" runat="server"  Text="Back" cssclass="cust__login_btn btn-flat"
                    OnClick="btnHome_Click"  Visible="false"/>
                                                      </td></tr>
            </table>
            
                 
             
            </div>

        <AECmsg:msgshow id="ucmsg1" runat="server"  />
        <!-- 
    <table class="page" style="width: 100%">
        <tr>
            <td class="ClientVew">
                <a href='<%$AppSettings:WebSiteID%>' target="_blank" style="text-decoration: none"
                    runat="server" id="img">
                    <table>
                        <tr>
                            <td>
                                <img alt="BSS Projects Logo" id="Img3" class="ClientVewImg" src="Images/Logo.jpg" />
                            </td>
                            <td>
                                <asp:Label class="ClientCompanyPart1" ID="Label2" runat="server" Text="<%$AppSettings:CompanyNamePart1%>">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" class="ClientCompanyPart2" Text="<%$AppSettings:CompanyNamePart2%>">
                                </asp:Label>
                            </td>
                        </tr>
                    </table>
                </a>
            </td>
            <td align="center" valign="middle" class="ModuleView">
                <span id="Span6" class="ModuleName"></span>
            </td>
            <td align="right" style="height: 50px; vertical-align: middle" valign="middle">
                <a href="http://www.aeclogic.com" target="_blank" style="border-width: 0px; text-decoration: none;">
                    <img alt="AEC Logic" id="Img4" class="ServiceProviderViewImg" src="Images/AECLogic.jpg" /></a>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="background-color: #8c8c8c; height: 2px;">
                &nbsp;
            </td>
        </tr>
    </table>-->
    
    </form>
</body>
</html>
