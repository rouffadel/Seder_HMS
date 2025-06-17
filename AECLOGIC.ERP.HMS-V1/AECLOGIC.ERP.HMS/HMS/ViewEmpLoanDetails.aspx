<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="AECLOGIC.ERP.HMSV1.ViewEmpLoanDetailsV1" Codebehind="ViewEmpLoanDetails.aspx.cs"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="TopMenu" TagPrefix="AEC" %>
<%--<%@ Register Src="MenuStauts.ascx" TagName="ViewStatus" TagPrefix="AEC" %>--%>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Exit Details</title>
    <%--<h1>Final Exit Request</h1>--%>
    <base target="_self" />
    <style type="text/css">
        .style1
        {
            width: 50;
        }
        .style4
        {
            width: 78px;
        }
        .style5
        {
            width: 81px;
        }
         /*body {
    background-color : #484848;
    margin: 0;
    padding: 0;
}*/
h1 {
    color : #000000;
    text-align : center;
    font-family: "SIMPSON";
}
/*form {
    width: 300px;
    margin: 0 auto;
}*/
    </style>
    <link rel="stylesheet" type="text/css" href="Includes/CSS/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="Includes/CSS/base.css" />
    <link rel="stylesheet" type="text/css" href="Includes/CSS/sddm.css" />
    <script src="Includes/JS/Validation.js" type="text/javascript" language="javascript"></script>
    <script type="text/javascript" src="Includes/JS/onload.js" language="javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <table>
             <tr>
                 
                            <td align="left" >
                                <%--<b><asp:Image ID="imgseder" runat="server" ImageUrl="../Images/SCC.jpg" /></b>--%>
                                <b><asp:Image ID="Image1" runat="server" ImageUrl="../Images/logo%20-%20Copy%20(2).png" /></b>
                            </td>
                            
                            <td align="center" >
                                &nbsp;&nbsp;
                               <h1> <b>Advance Request</b></h1>
                            </td>
                           <%--  <td align="center" ><b>Bussiness Trip</b> </td>--%>
                            
                             <td align="right" >
                                <b> <asp:Image ID="imgBarcodeFE" runat="server" Visible="false" Width="400px"/>
                                <asp:PlaceHolder ID="plBarCodeFE" runat="server" /></b>
                            </td>
                        

            </tr>
        </table>
        <table width="100%" border="1">
            <tr>
                <td>
          
                    <table width="100%">
                        <tr >
                            <td align="left" valign="top">
                                <b>Request No</b>
                            </td>
                            <td align="left" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblLID" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="right" valign="top">
                                <b>Worksite</b>
                            </td>
                            <td  align="right" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblWSite" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                         <tr >
                            <td align="left" valign="top">
                                <b>Project</b>
                            </td>
                            <td align="left" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblProj" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="right" valign="top">
                                <b>Designation</b>
                            </td>
                            <td  align="right" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lbldesig" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <b>Request By</b>
                            </td>
                            <td  align="left" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="LblReqBy" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="right" valign="top">
                                <b>Project Head Approved By</b>
                            </td>
                            <td  align="right" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblPHApp" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                           
                        </tr>
                        <tr>
                             <td  align="left" valign="top">
                                <b>Department Head Approved By</b>
                            </td>
                            <td  align="left" valign="top">
                                <b>:</b>
                            </td>
                             <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblDHApp" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            
                            <td  align="right" valign="top">
                                <b>HR Approved By</b>
                            </td>
                            <td align="right" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblHRApp" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td  align="left" valign="top">
                                <b>GM Approved By</b>
                            </td>
                            <td  align="left" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblGMApp" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td  align="right" valign="top">
                                <b>CFO Approved By</b>
                            </td>
                            <td  align="right" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblCfoApp" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style1" align="left" valign="top">
                    <br />
                    <asp:GridView ID="gdvIndent" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                        ShowFooter="true">
                        <Columns>
                            <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemID" runat="server" Text='<%#Bind("LoanId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblItem" runat="server" Text='<%#Bind("Name")%>' Width="200"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Loan Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                <ItemTemplate>
                                    <asp:Label ID="lblAU1" runat="server" Text='<%#Bind("LoanAmount")%>' Width="100"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Recover Month" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lblAU" runat="server" Text='<%#Bind("RecoverMonth")%>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Recover Year" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lblAU" runat="server" Text='<%#Bind("RecoverYear")%>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IssueedOn" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Label ID="lblAU" runat="server" Text='<%#Bind("IssueedOn")%>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr> <td align="left" valign="top">
                                <b>Note: This Document is System Generated and No Signature is Required.</b>
                            </td></tr>
        </table>
    </div>
    </form>
</body>
</html>