<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="DepartmentOrder.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.DepartmentOrder" MasterPageFile="~/Templates/CommonMaster.master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">

                <tr>
                    <td style="vertical-align: top;" width="20%">
                        <asp:ListBox ID="lstDepartments" runat="server" CssClass="box box-primary" Width="100%" Height="400px"
                            TabIndex="1"></asp:ListBox>
                    </td>
                    <td style="vertical-align: middle;" width="10%" align="left">
                        <asp:Button ID="btnFirst" runat="server" CssClass="btn btn-success" Text="Move First" Width="80px"
                            OnClick="btnFirst_Click" TabIndex="2" /><br />
                        <br />
                        <asp:Button ID="btnUp" runat="server" CssClass="btn btn-primary" Text="Move Up" Width="80px"
                            OnClick="btnUp_Click" TabIndex="3" /><br />
                        <br />
                        <asp:Button ID="btnDown" runat="server" CssClass="btn btn-warning" Text="Move Down" Width="80px"
                            OnClick="btnDown_Click" TabIndex="4" /><br />
                        <br />
                        <asp:Button ID="btnLast" runat="server" CssClass="btn btn-danger" Text="Move Last" Width="80px"
                            OnClick="btnLast_Click" TabIndex="5" /><br />
                        <br />
                    </td>
                    <td style="vertical-align: middle">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Width="15%"
                            OnClick="btnSubmit_Click" AccessKey="s" TabIndex="6"
                            ToolTip="[Alt+s OR Alt+s+Enter]" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="SalariesUpdPanel">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
