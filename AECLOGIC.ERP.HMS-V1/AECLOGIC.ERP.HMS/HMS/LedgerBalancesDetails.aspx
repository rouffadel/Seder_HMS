<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="LedgerBalancesDetails.aspx.cs"
    ValidateRequest="false" Inherits="AECLOGIC.ERP.HMS.LedgerBalancesDetails" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Namespace="AjaxControlToolkit" TagPrefix="Ajax" Assembly="AjaxControlToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title>Untitled Page</title>
    <link href="Includes/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="Includes/CSS/sddm.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript"> 

        function dateConfig(objTransPeriod, from, to, list) 
        {
            var objtxtFrom = document.getElementById(from);
            var objtxtTo = document.getElementById(to);
            var objddlFinYear = document.getElementById(list);
            objddlFinYear.disabled = true;
            objtxtFrom.disabled = true;
            objtxtTo.disabled = true;
            if (eval(objTransPeriod) > 0) {//transaction predefined periods
                var result = AjaxDAL.SetUpDates(objTransPeriod);
                objtxtFrom.value = result.value[0];
                objtxtTo.value = result.value[1];
            }
            if (eval(objTransPeriod) == 0) {//Custom Periods
                var resultC = AjaxDAL.getLedgerReportDateSetUp();
                objtxtTo.value = resultC.value[1];
                objtxtFrom.value = resultC.value[0];
                objtxtTo.disabled = false;
                objtxtFrom.disabled = false;
            }
            if (eval(objTransPeriod) == -1) {// Financial Year Periods
                objddlFinYear.disabled = false;
                var resultF = AjaxDAL.getFinanceYear(objddlFinYear.value);
                objtxtTo.value = resultF.value[1];
                objtxtFrom.value = resultF.value[0];

            }
        }
    
        function ChangefinYear(from,to,list) 
         {
             var vFrom = document.getElementById(from);
             var vTo = document.getElementById(to);
             var vFinYear = document.getElementById(list);
             var resultF = AjaxDAL.getFinanceYear(vFinYear.value);
             vTo.value = resultF.value[1];
             vFrom.value = resultF.value[0];
         }
         
        </script>
</head>
<body>

   
    <div style="width: 100%">
        <table style="width: 100%">
            <tr>
                <td>
                    <ajaxToolkit:Accordion runat="server" ID="ALedgerReport" AutoSize="None" FadeTransitions="false"
                        TransitionDuration="50" FramesPerSecond="40" HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                        <Panes>
                            <ajaxToolkit:AccordionPane runat="server" ID="APLedgerRporte">
                                <Header>
                                    Search
                                </Header>
                                <Content>
                                  
                                    <table id="t11" width="100%">
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:RadioButtonList ID="rbTransPeriod"  runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Today</asp:ListItem>
                                                    <asp:ListItem Value="7">Week</asp:ListItem>
                                                    <asp:ListItem Value="15">Fortnight</asp:ListItem>
                                                    <asp:ListItem Value="30">Month</asp:ListItem>
                                                    <asp:ListItem Value="90">Quarterly</asp:ListItem>
                                                    <asp:ListItem Value="180">Half Yearly</asp:ListItem>
                                                    <asp:ListItem Value="365">Yearly</asp:ListItem>
                                                    <asp:ListItem Value="0">Custom</asp:ListItem>
                                                    <asp:ListItem  Value="-1">Financial Year</asp:ListItem>
                                                </asp:RadioButtonList>
                                                Worksite
                                                <asp:DropDownList ID="ddlCostCenter" runat="server"  CssClass="droplist" >
                                                </asp:DropDownList>
                                                Voucher Type
                                                <asp:DropDownList ID="ddlVouchertype" runat="server" AutoPostBack="false"  CssClass="droplist" >
                                                </asp:DropDownList>
                                                Date From
                                                <asp:TextBox ID="txtFrom" runat="server" TabIndex="10" Width="79px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="C1" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged="function hideCalendar(cb) { cb.hide(); }"
                                                    PopupButtonID="txtFrom" TargetControlID="txtFrom" />
                                                &nbsp;&nbsp;&nbsp;To
                                                <asp:TextBox ID="txtTo" runat="server" TabIndex="10" Width="73px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="C2" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged="function hideCalendar(cb) { cb.hide(); }"
                                                    PopupButtonID="txtTo" TargetControlID="txtTo" />
                                                Financial Year
                                                <asp:DropDownList ID="ddlFinYear" Enabled="false" runat="server"  CssClass="droplist" >
                                                </asp:DropDownList>
                                                <asp:Button ID="BtnGet" runat="server" CssClass="savebutton" OnClick="BtnGet_Click"
                                                    Text="Get Details" />
                                            </td>
                                        </tr>
                                    </table>
                                </Content>
                            </ajaxToolkit:AccordionPane>
                        </Panes>
                    </ajaxToolkit:Accordion>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="width: 200px">
                    Ledger (Head of Account)
                </td>
                <td style="width: 2px;">
                    :
                </td>
                <td colspan="2">
                    <asp:Label ID="lblLedgerName" runat="server" Font-Bold="true" CssClass="Ledgers" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    Mobile
                </td>
                <td style="width: 2px;">
                    :
                </td>
                <td>
                    <b>
                        <asp:Label ID="lblMobile" runat="server" CssClass="lab"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    Opening Balance
                </td>
                <td style="width: 2px;">
                    :
                </td>
                <td>
                    <asp:Label ID="lblOpeningBalance" runat="server" Font-Bold="true" CssClass="openingBal" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    Closing Balance
                </td>
                <td style="width: 2px;">
                    :
                </td>
                <td>
                    <asp:Label ID="lblClosingBalance" runat="server" CssClass="ClosingBal" Font-Bold="true" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="GV" runat="server" ShowFooter="true" AutoGenerateColumns="false"
            AlternatingRowStyle-BackColor="GhostWhite" Width="100%" HeaderStyle-CssClass="tableHead"
            EmptyDataText="No Records Found!" 
            EmptyDataRowStyle-CssClass="EmptyRowData" 
            CssClass="gridview">
            <Columns>
                <asp:BoundField DataField="RowNumber" HeaderText="S.No" />
                <asp:BoundField DataField="vocdate" HeaderText="Date" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="70"
                    HeaderStyle-Width="70" ControlStyle-Width="70" />
                <asp:BoundField HeaderText="Trans ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                    DataField="TransID" />
                <asp:BoundField DataField="vouchername" HeaderText="Vch Type" ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="70" HeaderStyle-Width="70"
                    ControlStyle-Width="70" />
                <asp:TemplateField HeaderText="Particulars">
                    <ItemTemplate  >
                        <asp:Label  ID="lblParticulars"  runat="server" Text='<%#BindTransDetails(Eval("TransID").ToString(),Eval("REMARKS").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                    
                    <FooterTemplate>
                        <table>
                            <tr>
                                <td>
                                    <strong>
                                        <asp:Label ID="Label1" runat="server" Text="Page Total:"></asp:Label></strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>
                                        <asp:Label ID="lbltAmt" runat="server" Text="Grand Total:"></asp:Label></strong>
                                </td>
                            </tr>
                        </table>
                    </FooterTemplate>
                    <FooterStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Debit">
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <%# GetAmtDr(decimal.Parse(Eval("DEBIT").ToString())).ToString()%>
                    </ItemTemplate>
                    <FooterTemplate>
                        <table>
                            <tr>
                                <td>
                                    <b>
                                        <%# GetTotalDr().ToString()%>
                                    </b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>
                                        <asp:Label ID="lblGrandTotalDr" runat="server" CssClass="TotalDr" Text='<%#TotalAmountDr.ToString("#,#0.00") %>'></asp:Label>
                                    </b>
                                </td>
                            </tr>
                        </table>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Credit">
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <%# GetAmtCr(decimal.Parse(Eval("CREDIT").ToString())).ToString()%>
                    </ItemTemplate>
                    <FooterTemplate>
                        <table>
                            <tr>
                                <td>
                                    <b>
                                        <%# GetTotalCr().ToString()%></b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>
                                        <asp:Label ID="lblGrandTotalCr" runat="server" CssClass="TotalCr" Text='<%#TotalAmountCr.ToString("#,#0.00") %>'></asp:Label></b>
                                </td>
                            </tr>
                        </table>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton CommandName="View" Text="View" runat="server" ID="lnkView" ForeColor="BlueViolet"
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"transid")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <uc1:Paging ID="pgLedgerReport" runat="server" />
    </div>

</body>
</html>
    </asp:Content>
