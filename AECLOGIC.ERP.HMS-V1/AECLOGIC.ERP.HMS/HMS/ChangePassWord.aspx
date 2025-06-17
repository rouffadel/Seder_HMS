<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="ChangePassWord.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
    Inherits="AECLOGIC.ERP.HMS.ChangePassWord" Title="" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript" language="javascript">

        function Validate() {
            if (document.getElementById('<%=txtOldPassword.ClientID%>').value == "") {
                alert("Please Enter OldPassword");
                return false;
            }
            if (document.getElementById('<%=txtNewPassword.ClientID%>').value == "") {
                alert("Please Enter NewPassword");
                return false;
            }
            if (document.getElementById('<%=txtReenterPassword.ClientID%>').value == "") {
                alert("Please Enter ConfirmPassWord");
                return false;
            }
            if (document.getElementById('<%=txtNewPassword.ClientID%>').value != document.getElementById('<%=txtReenterPassword.ClientID%>').value) {
                alert("NewPassword and ConfirmPassWord are not matched");
                return false;
            }

        }
    </script>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="5" cellspacing="0" border="1" width="100%" class="gridbdr">
                                <tr>
                                    <td>
                                        <table cellpadding="5" cellspacing="0" border="1" width="100%" class="gridbdr">
                                            <tr>
                                                <td style="width: 150px;">
                                                    Old Password <span style="color: Red">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" Width="155" MaxLength="20"
                                                        CssClass="text_black" TabIndex="1" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    New Password <span style="color: Red">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Width="155" MaxLength="20"
                                                        CssClass="text_black" 
                                                        TabIndex="2"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Confirm Password <span style="color: Red" id="spncnfpwd" runat="server">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtReenterPassword" runat="server" TextMode="Password" Width="155"
                                                        MaxLength="20" CssClass="text_black" TabIndex="3" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click"
                                                        OnClientClick="javascript:return Validate()" CssClass="btn btn-success" 
                                                        AccessKey="s" TabIndex="4" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="btncancel" runat="server" Text="Reset" OnClick="btncancel_Click"
                                                        CssClass="btn btn-danger" AccessKey="b" TabIndex="5" 
                                                        ToolTip="[Alt+b OR Alt+b+Enter]" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</ContentTemplate>
       
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
