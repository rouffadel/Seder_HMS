<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="BarCodeView.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
    Inherits="AECLOGIC.ERP.HMSV1.BarCodeViewV1" Title="" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript" language="javascript">

        function Validate() {
            if (document.getElementById('<%=txtBarCode.ClientID%>').value == "") {
                alert("Please Enter BarCode");
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
                                                <td style="width: 100px;">
                                                    Bar Code <span style="color: Red">*:</span>
                                                </td>
                                                <td style="width: 120px;">
                                                    <asp:TextBox ID="txtBarCode" runat="server"  Width="150" MaxLength="14"
                                                        CssClass="text_black" TabIndex="1" ></asp:TextBox>
                                                   
                                                </td>
                                                 <td >
                                                     <asp:Button ID="btnsubmit" runat="server" Text="View" OnClick="btnsubmit_Click"
                                                        OnClientClick="javascript:return Validate()" CssClass="btn btn-success" 
                                                        AccessKey="s" TabIndex="2" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
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
