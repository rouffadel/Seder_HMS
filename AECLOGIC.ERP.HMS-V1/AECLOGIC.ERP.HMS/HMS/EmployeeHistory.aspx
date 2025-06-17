<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="EmployeeHistory.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmployeeHistory" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <table>
       
        <tr>
            <td>
                <asp:GridView ID="grvEmpHistory" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="grvEmpHistory_OnSelectedIndexChanged"
                    CssClass="gridview" AlternatingRowStyle-BackColor="GhostWhite" AllowSorting="True"
                    OnSorting="grvEmpHistory_Sorting">
                    <Columns>
                        <asp:BoundField DataField="EmpId" HeaderText="EmpID" />
                        <asp:TemplateField HeaderText="Employee" HeaderStyle-HorizontalAlign="Left" SortExpression="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Gender" HeaderText="Gender" />
                        <asp:BoundField DataField="Qualification" HeaderText="Qualification" />
                        <asp:BoundField DataField="Mailid" HeaderText="Mailid" />
                        <asp:BoundField DataField="AltMail" HeaderText="AltMail" />
                        <asp:BoundField DataField="skypeid" HeaderText="skypeid" />
                        <asp:BoundField DataField="PersonalMobile" HeaderText="PersonalMobile" />
                        <asp:BoundField DataField="CompMobile" HeaderText="CompMobile" />
                        <asp:BoundField DataField="Mole1" HeaderText="Mole1" />
                        <asp:BoundField DataField="Mole2" HeaderText="Mole2" />
                        <asp:BoundField DataField="OldEmpID" HeaderText="OldEmpID" />
                        <asp:BoundField DataField="Designation" HeaderText="Designation" />
                        <asp:BoundField DataField="Category" HeaderText="Category" />
                        <asp:BoundField DataField="Type" HeaderText="Type" />
                        <asp:BoundField DataField="EmpNature" HeaderText="EmpNature" />
                        <asp:BoundField DataField="WorkSite" HeaderText="WorkSite" />
                        <asp:BoundField DataField="DeptName" HeaderText="DeptName" />
                        <asp:BoundField DataField="Mgnr" HeaderText="Mgnr" />
                        <asp:BoundField DataField="DOJ" HeaderText="DOJ" />
                        <asp:BoundField DataField="Shift" HeaderText="Shift" />
                        <asp:BoundField DataField="CompAccNo" HeaderText="CompAccNo" />
                        <asp:BoundField DataField="BankName" HeaderText="BankName" />
                        <asp:BoundField DataField="BranchName" HeaderText="BranchName" />
                        <asp:TemplateField HeaderText="PAN Number" HeaderStyle-HorizontalAlign="Left" SortExpression="PAN">
                            <ItemTemplate>
                                <asp:Label ID="lblPan" runat="server" Text='<%#Eval("PANNumber") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CompProvSim" HeaderText="CompProvSim" />
                        <asp:BoundField DataField="Bloodgroup" HeaderText="Bloodgroup" />
                        <asp:BoundField DataField="ResAddress" HeaderText="ResAddress" />
                        <asp:BoundField DataField="ResCoun" HeaderText="ResCoun" />
                        <asp:BoundField DataField="ResState" HeaderText="ResState" />
                        <asp:BoundField DataField="ResCity" HeaderText="ResCity" />
                        <asp:BoundField DataField="PerCoun" HeaderText="PerCoun" />
                        <asp:BoundField DataField="PerState" HeaderText="PerState" />
                        <asp:BoundField DataField="PerCity" HeaderText="PerCity" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height: 17px">
                <uc1:Paging ID="EmpHisPaging" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
