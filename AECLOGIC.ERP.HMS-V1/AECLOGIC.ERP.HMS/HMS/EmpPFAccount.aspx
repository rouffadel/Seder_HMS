<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EmpPFAccount.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpPFAccount" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<script src="Includes/JS/validation.js" type="text/javascript"></script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <script language="javascript" type="text/javascript">

            function validate() {
                //for PFReg Code
                if (!chkDropDownList('<%=ddlPFRegCode.ClientID%>', 'PFRegCode')) {
                    return false;
                }
                //For A/C Number
                if (!chkNumber('<%=txtAccount.ClientID%>', "Account Number", true, "")) {
                    return false;
                }
            }

        </script>
        <div id="divAddPF" runat="server">
            <table width="100%">
                <tr>
                    <td colspan="2" class="pageheader">
                    </td>
                </tr>
                <tr>
                    <td style="width: 124px">
                        <asp:Label ID="lblPFReg" runat="server" Text="PFRegCode"></asp:Label>
                        <span style="color: #ff0000">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPFRegCode" CssClass="droplist" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 72px">
                        <asp:Label ID="lblPFAccount" runat="server" Text="Account"></asp:Label>
                        <span style="color: red">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAccount" runat="server" Width="316px" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 125Px" colspan="2">
                        <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClientClick="javascript:return validate();"
                            OnClick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
