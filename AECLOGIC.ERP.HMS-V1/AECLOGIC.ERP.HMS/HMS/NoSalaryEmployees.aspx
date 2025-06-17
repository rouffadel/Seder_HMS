<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoSalaryEmployees.aspx.cs" Inherits="AECLOGIC.ERP.HMSV1.NoSalaryEmployeesV1" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table style="width:70%">
                <tr>
                            <td align="left">
                                <asp:GridView ID="gvEmployees" runat="server" HeaderStyle-CssClass="gv" EmptyDataText="No records Found"
                                    AlternatingRowStyle-BackColor="GhostWhite" AutoGenerateColumns="false" CssClass="gridview" Width="80%">
                                    <Columns>
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="WorkSite" HeaderText="WorkSite" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Reason" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                <tr>
                                <td style="height: 17px">
                                    <uc1:Paging ID="EmpListPaging" runat="server" />
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
    </asp:content>
