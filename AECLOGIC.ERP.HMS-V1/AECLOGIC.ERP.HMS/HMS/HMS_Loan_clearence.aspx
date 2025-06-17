<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HMS_Loan_clearence.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMS_Loan_clearence" MasterPageFile="~/Templates/CommonMaster.master" %>


<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">

                <tr>
                    <td>
                        <strong>Handing Over Emp:</strong>
                        <asp:Label ID="lblname" runat="server"></asp:Label>&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTakeoverEmp" runat="server" viasible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Loan View
                        <asp:LinkButton ID="lnkhms" runat="server" OnClick="lnkhms_search"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:GridView ID="gvhms" runat="server" AutoGenerateColumns="false" ToolTip="gvLoanView"
                            HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found" Width="100%"
                            CssClass="gridview">
                            <Columns>
                                <asp:TemplateField HeaderText="LoanID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblloanid" runat="server" Text='<%#Eval("LoanID")%>' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:BoundField HeaderText="TransID" DataField="TransID" />
                                <asp:BoundField DataField="EmpID" HeaderText="EmpID" Visible="false" />
                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                <asp:BoundField DataField="LoanAmount" HeaderText="LoanAmount" />
                                <asp:BoundField DataField="DueAmount" HeaderText="DueAmount" />
                                <asp:BoundField DataField="NoofEMIs" ItemStyle-HorizontalAlign="Center" HeaderText="NoofEMIs" />
                                <asp:TemplateField HeaderText="RecoveryFrom">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecoveryStartFrom" Text='<%#FormatMonth(Eval("RecoveryStartFrom"))%>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RecoveryYear" HeaderText="Year" />
                                <asp:BoundField DataField="IssuedOn" ItemStyle-HorizontalAlign="Center" HeaderText="ApprovedOn" />
                                <asp:BoundField DataField="IssuedBy" HeaderText="Approved By" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkheadmms" runat="server" AutoPostBack="false" Visible="false" onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkItemmms" AutoPostBack="false" Visible="false" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                        <uc1:Paging ID="EmpListPaging" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:Button ID="btnback" runat="server" CssClass="btn btn-primary" Text="Back"
                            OnClick="btnback_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
