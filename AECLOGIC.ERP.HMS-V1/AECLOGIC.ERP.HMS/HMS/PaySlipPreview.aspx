<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="PaySlipPreview.aspx.cs"    Inherits="AECLOGIC.ERP.HMS.PaySlipPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script language="javascript" type="text/javascript">
     function SetTarget() {
            document.forms[0].target = "_blank";
        }
 </script>

    <title>Pay-Slip</title>
    <link href="Includes/CSS/StyleSheet.css" type="text/css" rel="Stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 162px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divMenu" style="height: 25px; position: absolute; top: 60px; visibility: visible;
        width: 100px">
        <table border="0" cellpadding="0" cellspacing="0">
            <tbody>
                <tr>
                    <td>
                        <img border="0" alt="Print" onclick="javascript: divMenu.style.display='none'; window.print();  window.close();"
                            class="right" src="Images/print.png" /><img border="0" alt="Close" onclick="javascript: divMenu.style.display='none'; window.close();"
                                class="right" src="Images/close.png" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div align="center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" align="left">
            <tr>
                <td align="center" colspan="2">
                    <b>
                        <asp:Label ID="lblCompanyName" runat="server"></asp:Label>
                    </b>
                    <br />
                    <b>Pay-Slip for the month of
                        <asp:Label ID="lblmonthslip" runat="server"></asp:Label>
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <table align="left" width="100%">
                        <tr>
                            <td align="left" class="style1">
                                Employee ID:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                                Name:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                                Department:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblDept" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                                Designation:
                            </td>
                            <td align="left">
                                <asp:Label ID="lbldesig" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                                Date of Joining:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblDOJ" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                                Date of Last Increment:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblDoLI" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                                Monthly Salary:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblSalary" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                                Number of Days Worked:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblNODW" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                           
                            
                            <td>
                                <asp:LinkButton ID="lnkViewAttendance" runat="server" Text=" Number of Payable Days:" ToolTip="View Attendance" OnClientClick="SetTarget();" OnClick="lnkViewAttendance_Click"></asp:LinkButton>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblNoofDays" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                                Ledger Closing Balance:
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLBalance" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="style1">
                                Bank A/c No
                            </td>
                            <td align="left">
                                <asp:Label ID="lblbankACno" runat="server"></asp:Label>
                            </td>
                        </tr>
                       <%-- <tr>
                            <td align="left" class="style1">
                                Pancard No
                            </td>
                            <td align="left">
                                <asp:Label ID="lblPancardNo" runat="server"></asp:Label>
                            </td>
                        </tr>--%>
                       <%-- <tr>
                            <td align="left" class="style1">
                                PF A/c No
                            </td>
                            <td align="left">
                                <asp:Label ID="lblPFACNO" runat="server"></asp:Label>
                            </td>
                        </tr>--%>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <table align="left">
                        <tr>
                            <td align="left" colspan="3">
                                <table align="left">
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:GridView ID="grdWages" runat="server" AutoGenerateColumns="false" ForeColor="#333333"
                                                ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Earnings: Salaries" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLongName" runat="server" Text='<%#Eval("LongName")%>' Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                        FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# GetAmtWages(decimal.Parse(Eval("Value").ToString()==string.Empty?"0":Eval("Value").ToString())).ToString()%>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="lblWagesTot" runat="server" Text='<%# GetWages().ToString()%>'></asp:Label></b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="grdAllowances" runat="server" AutoGenerateColumns="false" ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="LongName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="280px"
                                                        HeaderText="Earnings: Allowances" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                        FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# GetAmtAllowances(decimal.Parse(Eval("Value").ToString())).ToString()%>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="lblAllowancesTot" runat="server" Text='<%# GetAllowances().ToString()%>'></asp:Label>
                                                            </b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:GridView ID="grdCoyContrybutions" runat="server" AutoGenerateColumns="false"
                                                ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deductions: Company  Contributions" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLongName" runat="server" Text='<%#Eval("LongName")%>' Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                        FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# GetAmtCoyContrybutions(decimal.Parse(Eval("Value").ToString())).ToString()%>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="lblCoyContrybutions" runat="server" Text='<%# GetCoyContrybutions().ToString()%>'></asp:Label>
                                                            </b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="grdDuductSatatutory" runat="server" AutoGenerateColumns="false"
                                                ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="LongName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="280px"
                                                        HeaderText="Deductions: Statutory" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                        FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# GetAmtDuductSatatutory(decimal.Parse(Eval("Value").ToString())).ToString()%>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="lblDuductSatatutory" runat="server" Text='<%# GetDuductSatatutory().ToString()%>'></asp:Label></b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            </br>
                                              <asp:GridView ID="grdNonCTC" runat="server" AutoGenerateColumns="false" ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="LongName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="280px"
                                                        HeaderText="Others" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                        FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNonAmount" runat="server" Text='<%# GetAmtNonCTC(decimal.Parse(Eval("Amount").ToString())).ToString()%>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="lblNonCTc" runat="server" Text='<%# GetNonCTC().ToString()%>'></asp:Label></b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left" style ="display:none ">
                                            <asp:GridView ID="grdITExmention" runat="server" AutoGenerateColumns="false" ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="E" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IT Exemptions" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLongName" runat="server" Text='<%#Eval("LongName")%>' Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                        FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# GetAmtExemptions(decimal.Parse(Eval("Value").ToString())).ToString()%>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="lblExemptions" runat="server" Text='<%# GetExemptions().ToString()%>'></asp:Label>
                                                            </b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <asp:GridView ID="grdITSavings" runat="server" AutoGenerateColumns="false" ShowFooter="true" Visible ="false" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="F" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="SectionName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="280px"
                                                        HeaderText="IT-Savings" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                        FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# GetAmtSavings(decimal.Parse(Eval("Amount").ToString())).ToString()%>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="lblSavings" runat="server" Text='<%# GetSavings().ToString()%>'></asp:Label></b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <asp:GridView ID="gvTDS" runat="server" AutoGenerateColumns="false" ShowFooter="true" Visible="false" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="G" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="280px"
                                                        HeaderText="Tax Deducted at Source" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="right"
                                                        FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# GetAmtTDS(decimal.Parse(Eval("value").ToString())).ToString()%>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="lblSavings" runat="server" Text='<%# GetTDS().ToString()%>'></asp:Label></b>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                         




                                             <br />
                                          

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td>
                                <b></b>
                            </td>
                            <td align="left" style="width: 280px;">
                                <b>Gross Salary :</b>
                            </td>
                            <td align="right">
                                <b>
                                    <asp:Label ID="lblGross" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td>
                                <b></b>
                            </td>
                            <td align="left" style="width: 280px;">
                                <b>Total Deductions : </b>
                            </td>
                            <td align="right">
                                <b>
                                    <asp:Label ID="lblDeductions" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td>
                                <b></b>
                            </td>
                            <td align="left" style="width: 280px;">
                                <b>Salary : </b>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblSalbfIT" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;display:none ">
                            <td>
                                <b></b>
                            </td>
                            <td align="left" style="width: 280px;">
                                <b>Income Tax Exemptions : </b>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblITExemption" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;display:none ">
                            <td>
                                <b></b>
                            </td>
                            <td align="left" style="width: 280px;">
                                <b>Tax Deducted at Source (TDS):</b>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblTDS" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;display:none ">
                            <td>
                                
                            </td>
                            <td align="left" style="width: 280px;">
                                <b>Education Cess On TDS:</b>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblEducess" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-weight: bold; "  >
                            <td>
                                
                            </td>
                            <td align="left" style="width: 280px;">
                                <b>Net Payable: </b>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblNetPayable" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td>
                                
                            </td>
                            <td align="left" style="width: 280px;">
                                <b>Loans/Advance Recovery:</b>
                            </td>
                            <td align="right">
                                <b>
                                    <asp:Label ID="lblLRecovery" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;display:none">
                            <td>
                                <b></b>
                            </td>
                            <td align="left" style="width: 280px;">
                                <b>Mobile Bill:</b>
                            </td>
                            <td align="right">
                                <b>
                                    <asp:Label ID="lblMobile" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;display:none">
                            <td>
                                <b></b>
                            </td>
                            <td align="left" style="width: 280px;">
                                <b>Mess Bill:</b>
                            </td>
                            <td align="right">
                                <b>
                                    <asp:Label ID="lblMess" runat="server"></asp:Label></b>
                            </td>
                        </tr>

                        <tr style="font-weight: bold;">
                            <td>
                                <b></b>
                            </td>
                            <td align="left" style="width: 280px;">
                                <b>Non CTC Components: </b>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblNonCTC" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td>
                                <b></b>
                            </td>
                            <td align="left" style="width: 280px;">
                                <b>OT : </b>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblOT" runat="server"></asp:Label>
                            </td>
                        </tr>
                         <tr style="font-weight: bold;">
                            <td>
                                <b></b>
                            </td>
                            <td align="left" style="width: 280px;">
                                <b>Special : </b>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblSpecial" runat="server"></asp:Label>
                            </td>
                        </tr>

                         <tr style="font-weight: bold;">
                            <td>
                                <b></b>
                            </td>
                            <td align="left" style="width: 280px;">
                                <b>Absent Penalities: </b>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblAbsAmt" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td>
                                <b></b>
                            </td>
                            <td align="left" style="width: 280px;">
                                <b>Employee Penalities: </b>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblEmpPenalities" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="font-weight: bold;">
                            <td>
                                <b></b>
                            </td>
                            <td align="left" style="width: 280px;">
                                <b>Take Home Pay: (Negative total is equated to ZERO)</b>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblTakeHome" runat="server"></asp:Label>
                            </td>
                        </tr>
                         


                        <tr>
                            <td colspan="4">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
           <%-- <tr>
                <td colspan="4" align="left">
                    * All the Salary Calculations are done with respect to Cost-To-Company(Gross) of an
                    Employee
                </td>
            </tr>
            <tr>
                <td colspan="4" align="left">
                    * If any Reimbursement, Then the amount will be paid separately
                </td>
            </tr>--%>
            <tr>
                <td colspan="4" align="left">
                    Note: This is a system generated payslip and does not require authorization
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
