<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewBusinessTrip.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMSV1.ViewBusinessTripV1" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="TopMenu" TagPrefix="AEC" %>
<%--<%@ Register Src="MenuStauts.ascx" TagName="ViewStatus" TagPrefix="AEC" %>--%>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Business Trip</title>
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
                               <h1> <b>Business Trip</b></h1>
                            </td>
                           <%--  <td align="center" ><b>Bussiness Trip</b> </td>--%>
                            
                             <td align="right" >
                                <b> <asp:Image ID="imgBarcodeBT" runat="server" Visible="false" Width="400px"/>
                                <asp:PlaceHolder ID="plBarCodeBT" runat="server" /></b>
                            </td>
                        

            </tr>
        </table>
        <table width="100%" border="1">
            <tr>
                <td>
          
                    <table width="100%">
                        <tr >
                            <td align="left" valign="top">
                                <b>Request No#</b>
                            </td>
                            <td align="left" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblIndentId" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="right" valign="top">
                                <b>Worksite</b>
                            </td>
                            <td  align="right" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblWorksite" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                      <%--  <tr>
                            <td  align="right" valign="top">
                                <b>Project</b>
                            </td>
                            <td  align="right" valign="top">
                                <b>:</b>
                            </td>
                             <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblProject" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                             <td align="left" valign="top">
                                <b>Designation</b>
                            </td>
                            <td  align="left" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblDesigination" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr> --%>
                         <tr >
                            <td align="left" valign="top">
                                <b>Project</b>
                            </td>
                            <td align="left" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblProject" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="right" valign="top">
                                <b>Designation</b>
                            </td>
                            <td  align="right" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblDesigination" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td  align="left" valign="top">
                                <b>Request By</b>
                            </td>
                            <td align="left" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblcreatedBy" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td  align="right" valign="top">
                                <b>Project Head Approval By</b>
                            </td>
                            <td  align="right" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblprojectHead" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>  
                            <td  align="left" valign="top">
                            
                                <asp:Label ID="lblRecmndDesc" Text="GM Approval By" runat="server" Font-Bold="True"></asp:Label>                                
                            </td>
                            <td align="left" valign="top">
                             
                                <asp:Label ID="lblGmApproval12" runat="server" Font-Bold="True" Text=":"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblGmApproval" runat="server" Font-Bold="True"></asp:Label>
                            </td> 
                              <td  align="right" valign="top">
                              
                                <asp:Label ID="lblApprvedByDesc" Text="HR Review By" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td  align="right" valign="top">
                              
                                <asp:Label ID="lblApprvedByCol" runat="server" Font-Bold="True" Text=":"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblHRReview" runat="server" Font-Bold="True"></asp:Label>
                            </td>                       
                         
                        </tr>
                        <tr>
                                 <td  align="left" valign="top">
                                <%--<b>CMApproved By[PMS]</b>--%>
                                <asp:Label ID="lblcmidDesc" Text="HR Approval" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                <%--<b>:</b>--%>
                                <asp:Label ID="lblcmidCol" runat="server" Font-Bold="True" Text=":"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblHRApproval" runat="server" Font-Bold="True"></asp:Label>
                            </td> 
                              <td  align="right" valign="top">
                                <%--<b>PMApproved By[PMS]</b>--%>
                                <asp:Label ID="lblpmidDesc" runat="server" Font-Bold="True" Text="Account Posting By"></asp:Label>
                            </td>
                            <td  align="right" valign="top">
                                <%--<b>:</b>--%>
                                <asp:Label ID="lblpmidCol" runat="server" Font-Bold="True" Text=":"></asp:Label>
                            </td>
                            <td align="left" valign="top">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblAccountPostingBy" runat="server" Font-Bold="True"></asp:Label>
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
                            <asp:TemplateField HeaderText="#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="30" />
                                <HeaderStyle HorizontalAlign="Left" Width="30" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblItem" runat="server" Text='<%#Bind("empname")%>' Width="200"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Travel Mode" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpec" runat="server" Text='<%#Bind("TravelMode")%>' Width="580"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Booking Class" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Label ID="lblAU" runat="server" Text='<%#Bind("BookingClass")%>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From City" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                <ItemTemplate>
                                    <asp:Label ID="lblAU1" runat="server" Text='<%#Bind("FCity")%>' Width="60"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To City" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblQty" runat="server" Text='<%#Bind("TCity")%>' Width="60"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblPurpose" runat="server" Text='<%#Bind("FromDate") %>' Width="100"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:Label ID="lblReq" runat="server" Text='<%#Bind("ToDate") %>'
                                        ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="style1" align="left" valign="top">
                    <br />
                      <asp:GridView ID="gvShow" CssClass="gridview" runat="server" AutoGenerateColumns="false"
                            Width="80%" >
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="30" />
                                    <HeaderStyle HorizontalAlign="Left" Width="30" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ReimburseItem">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRItem" Text='<%#Eval("RItem")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="txtRate" Text='<%#Eval("UnitRate") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQty" Text='<%#Eval("Qty") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="txtAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                                <div class="DivBorderOlive1" style="margin-bottom: 20px; font: bold; font-size: 20px;text-align: right" >
                                                    <%--<asp:Label ID="lbl1" runat="server" Text="Total= "></asp:Label>--%>
                                                    <asp:Label ID="lblvalue" runat="server" Text='<%#Eval("SumAmount")%>' style="text-align: right"></asp:Label>
                                                </div>
                                      </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRItemNo" runat="server" Text='<%#Eval("RItemID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                        </asp:GridView>
                </td>
            </tr>
           <tr> <td align="left" valign="top">
                                <b>Note: This Document is System Generated and No Signature is Required.</b>
                            </td></tr>
<%--            <tr>
                <td>
                    <div id="dvSendback" runat="server" visible="false"> 
                        <table>
                            <tr>
                                <td>
                                     <asp:GridView ID="gvSendBack" runat="server"
                             AutoGenerateColumns="true" CssClass="gridview"
                             EmptyDataRowStyle-CssClass="EmptyRowData" EmptyDataText="There are no Request(s) for Assignment" 
                            HeaderStyle-CssClass="tableHead"  Width="100%">
                                
                                        <Columns>
                                        </Columns>
                                     
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                    <td height="10">
                        <uc1:Paging ID="taskPaging" runat="server" Visible="false" />
                    </td>
                </tr>
                        </table>
                    </div>
                </td>
            </tr>--%>
         
        </table>
    </div>
    </form>
</body>
</html>