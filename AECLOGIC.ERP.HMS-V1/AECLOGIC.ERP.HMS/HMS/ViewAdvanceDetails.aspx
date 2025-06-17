<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ViewAdvanceDetails.aspx.cs"
    Inherits="AECLOGIC.ERP.HMSV1.ViewAdvanceDetailsV1" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">


    <link href="Includes/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1 {
            width: 159px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            if (<%=Server.HtmlEncode(Session["UserId"].ToString())%>==1) {
                $("#dvreverse").css("display", "block");
                $("#dvreverse1").css("display", "block");
                $("#dvreverse2").css("display", "block");
            }
            else {
                $("#dvreverse").css("display", "none");
                $("#dvreverse1").css("display", "none");
                $("#dvreverse2").css("display", "none");
            }
        });
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <div>
                <table width="100%">
                    <tr>
                        <td colspan="2" class="pageheader">Employee Loan/Advance EMI Details
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">&nbsp;
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <b>EmpID:</b>
                        </td>
                        <td width="300px">
                            <asp:Label ID="lblEmpID" runat="server" Font-Bold="True" ForeColor="#0000CC"></asp:Label>
                        </td>
                        <td class="style1">
                            <b>Request By:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblRequestBy" runat="server" Font-Bold="True" ForeColor="#0000CC"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <b>Name:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblName" runat="server" Font-Bold="True" ForeColor="#0000CC"></asp:Label>
                        </td>
                        <td class="style1">
                            <b>PM Approval By:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblPMApprovalBy" runat="server" Font-Bold="True" ForeColor="#0000CC"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <b>LoanID:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblLoanID" runat="server" Font-Bold="True" ForeColor="#0000CC"></asp:Label>
                        </td>
                        <td class="style1">
                            <b>Cheif Operating Officer:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblCOOBy" runat="server" Font-Bold="True" ForeColor="#0000CC"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <b>Advance/Loan Amount:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblloanAmt" runat="server" Font-Bold="True" ForeColor="#0000CC"></asp:Label>
                        </td>
                        <td class="style1">
                            <b>HR Approval By:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblHrApprovalBy" runat="server" Font-Bold="True" ForeColor="#0000CC"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <b>Issued On:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblIssuedOn" runat="server" Font-Bold="True" ForeColor="#0000CC"></asp:Label>
                        </td>
                        <td class="style1">
                            <b>GM Approval By:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblGMApprovalBy" runat="server" Font-Bold="True" ForeColor="#0000CC"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1"></td>
                        <td></td>
                        <td class="style1">
                            <b>CFO Approval By:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblCFOApprovalBy" runat="server" Font-Bold="True" ForeColor="#0000CC"></asp:Label>
                        </td>
                    </tr>
                  


                        <tr>
                            <td colspan="2">
                                  <div id="dvreverse">
                                <table>
                                    <tr>
                                        <td>
                                            <b>Reverse Amount : </b>&nbsp;  &nbsp;  &nbsp;  &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtReverseAmount" runat="server" Text="0"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtReverseAmount" FilterType="Custom,Numbers" ValidChars="."></cc1:FilteredTextBoxExtender>
                                            <cc1:TextBoxWatermarkExtender
                                                ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtReverseAmount" WatermarkCssClass="Watermarktxtbox"
                                                WatermarkText="[Enter Amount]"></cc1:TextBoxWatermarkExtender>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnReverseLoan" runat="server" ToolTip="Reverse loan" Text="Reverse Loan"
                                                CssClass="btn btn-primary" OnClick="btnReverseLoan_Click" />
                                        </td>
                                    </tr>
                                </table>
                                      </div>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                  <div id="dvreverse1">
                                <table>
                                    <tr>
                                        <td style="width: 100px;">
                                            <b>Advance Amount : </b>&nbsp;  &nbsp;  &nbsp;  &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLoanAmount" runat="server" Text="0"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FTBAdvAmt" runat="server" TargetControlID="txtLoanAmount" FilterType="Custom,Numbers" ValidChars="."></cc1:FilteredTextBoxExtender>
                                            <cc1:TextBoxWatermarkExtender
                                                ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtLoanAmount" WatermarkCssClass="Watermarktxtbox"
                                                WatermarkText="[Enter Loan]"></cc1:TextBoxWatermarkExtender>
                                        </td>
                                    </tr>
                                </table>
</div>


                            </td>
                            <td>
                                    <div id="dvreverse2">
                                <table>
                                    <tr>
                                        <td><b>No of Recovery Months</b>&nbsp;  &nbsp;  &nbsp;  &nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtNoEMI" runat="server" Text="0"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3"
                                                runat="server" TargetControlID="txtNoEMI" WatermarkCssClass="Watermarktxtbox"
                                                WatermarkText="[Enter EMI Months]"></cc1:TextBoxWatermarkExtender>
                                            &nbsp;  &nbsp;  &nbsp;  &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnNewLoan" runat="server" ToolTip="Issue New loan" Text="Issue New Loan"
                                                CssClass="btn btn-success" OnClick="btnNewLoan_Click" /></td>
                                    </tr>
                                </table>

                                        </div>

                            </td>
                        </tr>
                    </div>

                    <tr>
                        <td class="style1">&nbsp;
                        </td>
                        <td></td>
                    </tr>
                </table>
                <table runat="server" width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvLoanDetails" Width="90%" runat="server" AutoGenerateColumns="false"
                                HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found" CssClass="gridview">
                                <Columns>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoadID" runat="server" Text='<%#Eval("LoanID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="InstallmentNo" HeaderText="Installment" />
                                    <asp:BoundField DataField="PrincipalAmt" HeaderText="Principal Amt" />

                                    <asp:BoundField DataField="EMIAmount" HeaderText="EMI-Amount" />
                                    <asp:BoundField DataField="RedAmt" HeaderText="Recovered EMI" />
                                    <asp:BoundField DataField="Cfd" HeaderText="C/F Amount" />
                                    <asp:BoundField DataField="CumCfd" HeaderText="Cum C/F Amount" />
                                    <asp:BoundField DataField="TransID1" HeaderText="TransID" />
                                    <asp:BoundField DataField="RecoveredOn1" HeaderText="RecoveredOn" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
