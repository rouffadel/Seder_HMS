<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="SiteEmployeeWorkDetails.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
    Inherits="AECLOGIC.ERP.HMS.SiteEmployeeWorkDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <div style="width: 100%">
        <table width="100%">
            <tr>
                <td>
                    <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataList ID="RpteView" RepeatLayout="Table" RepeatColumns="2" RepeatDirection="Horizontal"
                        runat="server">
                        <ItemStyle Font-Names="arial" VerticalAlign="Top" Font-Size="XX-Small" />
                        <ItemTemplate>
                            <div style="vertical-align:top; margin:5px; padding:5px; page-break-inside:avoid">
                            <table width="300" style="border-style:solid; font-weight:bolder; vertical-align:top;   border-width:1px; border-color:Black;    font-size:8pt"  >
                                <tr >
                                    
                                    <td colspan="3" align="left" style="font-size:9pt;"  ><span style="float:right;font-size:9pt"><%#Eval("Intime")%></span>
                                        <asp:Label ID="Label1" runat="server" Text="<%$AppSettings:CompanyClient%>"></asp:Label>
                                         <hr/>
                                    </td>
                                    <tr>
                                    </tr>
                                    <td colspan="3" align="center"  style="font-size:9pt">Attendance Slip <hr/></td>
                                   
                                      <tr>
                                    </tr>
                                   
                                    <td style="width:50px; vertical-align:top;">
                                        Employee
                                    </td>
                                    <td style="width:1px; vertical-align:top;">
                                        :
                                    </td>
                                    <td>
                                        <%#Eval("Name")%>
                                    </td>
                                    <tr>
                                    </tr>
                                     <td style="width:50px;">
                                        Worksite
                                    </td>
                                    <td style="width:1px;">
                                        :
                                    </td>
                                    <td>
                                        <%#Eval("Site_Name")%>
                                    </td>
                                    <tr>
                                    </tr>
                                    <tr>
                                    </tr>
                                    <td style="width:50px;">
                                        Department
                                    </td>
                                    <td style="width:1px;">
                                        :
                                    </td>
                                    <td>
                                        <%#Eval("DepartmentName")%>
                                    </td>
                                    <tr>
                                    </tr>
                                   
                                  
                                     <td style="width:50px;">
                                        Attandace
                                    </td>
                                     <td style="width:1px;">
                                        :
                                    </td>
                                    <td>
                                        <%#Eval("Status")%>
                                    </td>
                                    <tr>
                                    </tr> 
                                     <td style="width:50px; vertical-align:top;">
                                        Location
                                    </td>
                                    <td style="width:1px; vertical-align:top;">
                                        :
                                    </td>
                                    <td>
                                        <%#Eval("Remarks")%>
                                    </td>
                                     <tr>
                                    </tr>
                                     <tr>
                                    </tr>
                                     <td style="width:50px;">
                                      Signature
                                    </td>
                                     <td style="width:1px;">
                                       
                                    </td>
                                    <td>
                                      
                                    </td>
                                    <tr></tr>
                                    <td style="width:50px;">
                                     &nbsp;
                                    </td>
                                     <td style="width:1px;">
                                       
                                    </td>
                                    <td>
                                      
                                    </td>
                                </tr>
                            </table></div>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
    </div>
 </asp:Content>