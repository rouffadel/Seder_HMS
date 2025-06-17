<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="DeptHeadView.aspx.cs" Inherits="AECLOGIC.ERP.HMS.DeptHeadView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title><link href="Includes/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1">
   
    <table width="70%" >
                            <tr>
                        <td align="center" style="text-align: left" class="pageheader">
                                                        Department Heads</td>
                    </tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td>
                            Worksite<span style="color: red">*</span> &nbsp;<asp:DropDownList ID="ddlWSSearch" runat="server" AutoPostBack="true" Width="200" OnSelectedIndexChanged="ddlWS_SelectedIndexChanged">
                            </asp:DropDownList>
                            <hr /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gdvWS" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                                GridLines="Both" Width="100%"   HeaderStyle-CssClass="tableHead" AllowSorting="true"  EmptyDataText="No Records Found"
                                 EmptyDataRowStyle-CssClass="EmptyRowData">
                                
                                <Columns>
                                <asp:BoundField DataField="Site_Name" HeaderText="Site_Name" />
                                <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                <asp:BoundField DataField="HeadId" HeaderText="EmpID" />
                                <asp:BoundField DataField="name" HeaderText="Name" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                      
                      
                      </table>
  
    </form>
</body>
</html>
    </asp:Content>
