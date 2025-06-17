<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VacationalPrint.aspx.cs" Inherits="AECLOGIC.ERP.HMSV1.VacationalPrintV1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
	 <script src="../Includes/JS/jquery-1.10.1.min.js" type="text/javascript"></script>
    <script src="../Includes/JS/jQuery-2.1.4.min.js"></script>
    <script src="../Includes/JS/bootstrap.min.js"></script>
    <script src="../Includes/JS/app.js" type="text/javascript"></script>
    <script src="../Includes/JS/demo.js" type="text/javascript"></script>
         <script type="text/javascript">
            $(document).ready(function () {
                 var Final = getUrlParameter('Checked');
                 if (Final == 'True') {
				 document.getElementById("dtlvacation_lblFromDate_0").innerHTML ='Last Working Day :';
				 document.getElementById("dtlvacation_lblToDate_0").innerHTML ='';
				 document.getElementById("dtlvacation_lblOT_0").innerHTML ='';

//document.getElementById("dtlvacation_lbllv_0").innerHTML ='';
//document.getElementById("dtlvacation_lbllvrd_0").innerHTML ='';
//document.getElementById("dtlvacation_lblrd_0").innerHTML ='';
//document.getElementById("dtlvacation_lblrdate_0").innerHTML ='';
                   
                 }
             });
                 var getUrlParameter = function getUrlParameter(sParam) {
                 var sPageURL = window.location.search.substring(1),
                     sURLVariables = sPageURL.split('&'),
                     sParameterName,
                     i;

                 for (i = 0; i < sURLVariables.length; i++) {
                     sParameterName = sURLVariables[i].split('=');

                     if (sParameterName[0] === sParam) {
                         return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
                     }
                 }
                 return false;
             };
         </script>
    </head>
    <body runat="server">
    <table>
        <tr>
             <td align="left" >
                                <%--<b><asp:Image ID="imgseder" runat="server" ImageUrl="../Images/SCC.jpg" /></b>--%>
                                <b><asp:Image ID="Image1" runat="server" ImageUrl="../Images/logo%20-%20Copy%20(2).png" /></b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                 &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                 <asp:Label ID="lblHeader" runat="server" Font-Size="30px" Font-Bold="true" ></asp:Label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                 &nbsp&nbsp&nbsp&nbsp&nbsp
                 <img border="0" alt="Print" onclick="javascript:window.print();  window.close();"
                                                                                class="right" src="Images/print.png" />
                            </td>
            <td align="left">
                                        
                                        
                                    </td>
           <%-- <td>
                 <img border="0" alt="Print" onclick="javascript:window.print();  window.close();"
                                                                                class="right" src="Images/print.png" />
                
            </td>--%>
        </tr>
        <tr>
            <td>
                <asp:DataList ID="dtlvacation" runat="server" HeaderStyle-CssClass="datalistHead"
                    Width="100%" OnItemDataBound="dtlvacation_ItemDataBound">
                    <ItemTemplate>
                        <div class="DivBorderOlive" style="margin-bottom: 20px">
                            <table style="width: 100%; background-color: #efefef;">
                                <tr>
                                    <td>
                                        <b>EmployeeName :</b>
                                        <asp:Label ID="lblempname" runat="server" Text='<%#Eval("name")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <b>EmployeeId :</b>
                                        <asp:Label ID="lblempid" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <b>DateOfJoin :</b>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("Dateofjoin")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <b>
                                        <asp:Label ID="lbllv" runat="server" Text='Last Vacation Date :'></asp:Label></b>
                                        <asp:Label ID="lbllvrd" runat="server" Text='<%#Eval("LVRD")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <b>
                                        <asp:Label ID="lblrd" runat="server" Text='Return Date :'></asp:Label></b>
                                        <asp:Label ID="lblrdate" runat="server" Text='<%#Eval("RDATE")%>'></asp:Label>
                                    </td>
                                </tr>
                                <tr >
                                    <td>
                                        <b>Job Title:</b>
                                        <asp:Label ID="lblAAL" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <b>Nationality :</b>
                                        <asp:Label ID="lblRAL" runat="server" Text='<%#Eval("Country")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <b>
                                        <asp:Label ID="lblFromDate" runat="server" Text='Start Date :'></asp:Label></b>
                                        <asp:Label ID="lblGAL" runat="server" Text='<%#Eval("LFrom")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <b>
                                        <asp:Label ID="lblToDate" runat="server" Text='End Date :'></asp:Label></b>
                                        <asp:Label ID="lblOT" runat="server" Text='<%#Eval("LTo")%>'></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <form runat="server">
                                <asp:GridView ID="GVVacation" Visible="true" runat="server" AlternatingRowStyle-BackColor="GhostWhite"
                                    AutoGenerateColumns="false" ShowFooter="true" OnRowCommand="GVVacation_RowCommand"
                                    DataSource='<%#BindTransdetails(Eval("vid").ToString())%>' OnRowDataBound="GVVacation_RowDataBound"
                                    HeaderStyle-CssClass="tableHead" Width="100%">
                                    <Columns>
                                        <%--<asp:BoundField DataField="Description" HeaderText="Credit/Debit"></asp:BoundField>--%>
                                         <asp:TemplateField HeaderText="Credit/Debit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesc" Style="text-align: left" Width="200px" runat="server" Text='<%#Bind("Description")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="DivBorderOlive1" style="margin-bottom: 20px; font: bold; font-size: 20px;text-align: left" >
                                                    <asp:Label ID="lbl1" runat="server" Style="text-align: right" Text="    Total" ></asp:Label>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtA1" BorderWidth="0" OnTextChanged="QtyChanged" Style="text-align: right" AutoPostBack="true" Width="120px" Height="20px" runat="server" Text='<%#Bind("Amount")%>' ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div class="DivBorderOlive1" style="margin-bottom: 20px; font: bold; font-size: 20px;text-align: right" >
                                                    <%--<asp:Label ID="lbl1" runat="server" Text="Total= "></asp:Label>--%>
                                                    <asp:Label ID="lblvalue" runat="server" Text="0.000" style="text-align: right"></asp:Label>
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtA6" BorderWidth="0" Style="text-align: right" runat="server" Width="120px" Height="20px" Visible="false" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Width="17%" ItemStyle-Wrap="true" ></asp:BoundField>
                                       <%-- <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="btnCal" Style="text-align: right" AutoPostBack="true" Width="10px" Height="20px" runat="server" Text='Get' Visible="false"></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton runat="server" ID="lnkatt_Viewk" AutoPostBack="true" Text="Attendance" Visible="false" ToolTip="Click to complete attendance details"></asp:LinkButton>--%>
                                                <a id="Doc" target="_blank" class="anchor__grd vw_grd" visible='<%#VDocVisible(Eval("LoanProof").ToString()) %>' 
                                                                   href='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "LoanProof").ToString()) %>' 
                                                                   runat="server">Document
                                                                 
                                                               </a> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Details" HeaderText="Details" />
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtA11" BorderWidth="0"  runat="server" Text='<%#Bind("Add")%>' ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle />
                                    <FooterStyle BackColor="#cccccc" ForeColor="Black" HorizontalAlign="left" />
                                </asp:GridView>
                            </form>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
       <%--  <tr> <td align="left" valign="top">
                                <b>Note: This Document is System Generated and No Signature is Required.</b>
                            </td></tr>--%>
         <tfoot>
                                    <tr>
                                        <td colspan="2">
                                             <table width="100%">
                                                    <tr>
                                                        <td style="text-align: center; width:20%;">
                                                            <br />
                                                            <br />
                                                            <asp:Label ID="lbl_HR_CO1" runat="server" Text="Tarfah Alhusain" Font-Size="9pt"
                                                                Font-Bold="true" ForeColor="Blue" Font-Underline="true"></asp:Label>
                                                            <br />
                                                            <b><asp:Label ID="lblHRCo1" runat="server" Font-Size="9pt" Text="HR Coordinator"
                                                                Font-Bold="true" ForeColor="Black" Font-Underline="false"></asp:Label></b>
                                                            <%--<b style="font-size: 7pt">Procurement Coordinator</b>--%>
                                                        </td>
                                                        
                                                       <%-- <td style="text-align: center; width:16.6%;">
                                                            <br />
                                                            <br />
                                                            <asp:Label ID="lbl_HR_CO2" runat="server" Text="Amjed Binsahman" Font-Size="7pt"
                                                                Font-Bold="true" ForeColor="Blue" Font-Underline="true"></asp:Label>
                                                            <br />
                                                            <b><asp:Label ID="lblHRCo2" runat="server" Font-Size="7pt" Text="HR Coordinator"
                                                                Font-Bold="true" ForeColor="Black" Font-Underline="false"></asp:Label></b>
                                                           </td>--%>
                                                        <td style="text-align: center; width:20%;">
                                                            <br />
                                                            <br />
                                                            <asp:Label ID="lbl_HR_Mgnr" runat="server" Text="Abeer Al Sibai" Font-Size="9pt"
                                                                Font-Bold="true" ForeColor="Blue" Font-Underline="true"></asp:Label>
                                                            <br />
                                                            <b><asp:Label ID="lblHRMgnr" runat="server" Font-Size="9pt" Text="HR Manager"
                                                                Font-Bold="true" ForeColor="Black" Font-Underline="false"></asp:Label></b>
                                                            <%--<b style="font-size: 7pt">Technical Manager</b>--%>
                                                        </td>
                                                         <td style="text-align: center; width:20%;">
                                                            <br />
                                                            <br />
                                                            <asp:Label ID="lbl_Acc" runat="server" Text="Aly Elhelaly Aly Youssef" Font-Size="9pt"
                                                                Font-Bold="true" ForeColor="Blue" Font-Underline="true"></asp:Label>
                                                            <br />
                                                            <b><asp:Label ID="lblAcc" runat="server" Font-Size="9pt" Text="Senior Accountant"
                                                                Font-Bold="true" ForeColor="Black" Font-Underline="false"></asp:Label></b>
                                                            <%--<b style="font-size: 7pt">Technical Manager</b>--%>
                                                        </td>
                                                        <td style="text-align: center; width:20%;">
                                                            <br />
                                                            <br />
                                                            <asp:Label ID="lbl_FM" runat="server" Text="Ahmed Odeh" Font-Size="9pt"
                                                                Font-Bold="true" ForeColor="Blue" Font-Underline="true"></asp:Label>
                                                            <br />
                                                            <b><asp:Label ID="lblFM" runat="server" Font-Size="9pt" Text="Finance Manager"
                                                                Font-Bold="true" ForeColor="Black" Font-Underline="false"></asp:Label></b>
                                                            <%--<b style="font-size: 7pt">Technical Manager</b>--%>
                                                        </td>
                                                        <td style="text-align: center; width:20%;">
                                                            <br />
                                                            <br />
                                                            <asp:Label ID="lbl_GM" runat="server" Text="Abdulhakim Al Sehli" Font-Size="9pt"
                                                                Font-Bold="true" ForeColor="Blue" Font-Underline="true"></asp:Label>
                                                            <br />
                                                            <b><asp:Label ID="lblGM" runat="server" Font-Size="9pt" Text="General Manager"
                                                                Font-Bold="true" ForeColor="Black" Font-Underline="false"></asp:Label></b>
                                                            <%--<b style="font-size: 7pt">Technical Manager</b>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                        </td>
                                    </tr>
                                     <tr>

                                         <td align="left" valign="top">
<br />
<br />
                                        I'm the above mentioned employee, herby declare on my own free and will that I have received all my rights from the
                                        salaries, vacation encashment, tickets, overtime & required dues from Seder Construction Co., In confirmation of the above,
                                        I have signed this clearance certificate.

                                        </td></tr>
		<tr>
                                         <td >
     <table width="100%">
            <tr>
                <td style="text-align: center; width:33.3%;">
                    <br />
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Employee Name :" Font-Size="10pt"
                        Font-Bold="true"  Font-Underline="true"></asp:Label>
                    <br />
                    
                </td>
                
                <td style="text-align: center; width:33.3%;">
                    <br />
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="Signature :" Font-Size="10pt"
                        Font-Bold="true" Font-Underline="true"></asp:Label>
                    <br />
                    
                </td>
                <td style="text-align: center; width:33.3%;">
                    <br />
                    <br />
                    <asp:Label ID="Label6" runat="server" Text="Date :" Font-Size="10pt"
                        Font-Bold="true"  Font-Underline="true"></asp:Label>
                    <br />
                    
                </td></tr></table></td>
                                     </tr>
                                </tfoot>
   </table>
</body>
</html>