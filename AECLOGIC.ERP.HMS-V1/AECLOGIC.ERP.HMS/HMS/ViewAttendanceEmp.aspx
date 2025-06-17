<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAttendanceEmp.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMS.ViewAttendanceEmp" MasterPageFile="~/Templates/CommonMaster.master"%>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
   
     <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td>
                <AEC:Topmenu ID="topmenu" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <div id="dvvDay">

                     <asp:Table ID="tblAtt" runat="server" CssClass="item-a" BorderWidth="2" GridLines="Both">
                     </asp:Table>
                    </div>
                </td>
        </tr>
    </table>
</asp:Content>
