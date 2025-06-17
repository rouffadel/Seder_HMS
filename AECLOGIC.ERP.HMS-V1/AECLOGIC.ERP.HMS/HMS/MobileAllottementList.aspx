<%@ Page Language="C#" AutoEventWireup="True"  CodeBehind="MobileAllottementList.aspx.cs" Inherits="AECLOGIC.ERP.HMS.MobileAllottementList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <link href="Includes/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table id="tblAlloted" runat="server" width="100%">
    <tr><td colspan="2" class="pageheader">Not Allotted List</td></tr>
    <tr><td colspan="2">
        <asp:GridView ID="gvSimView" Width="100%" AutoGenerateColumns="false" 
        CssClass="gridview" runat="server" EmptyDataText="No Records Found!">
    <Columns>
    <asp:BoundField HeaderText="ID" DataField="PID" />
    <asp:BoundField HeaderText="Provider" DataField="Provider" />
    <asp:BoundField HeaderText="Vendor" DataField="vendor_name" />
    <asp:BoundField HeaderText="Phone No" DataField="SimNo" />
     <asp:BoundField HeaderText="Category" DataField="Category1" />
    <asp:BoundField HeaderText="Status" DataField="Status1" />
    <asp:BoundField HeaderText="Worksite" DataField="Site_Name" />
    <asp:BoundField HeaderText="ServiceFrom" DataField="ServiceFrom1" />
    <asp:BoundField HeaderText="Upto" DataField="Upto1" />
    </Columns>
    </asp:GridView>
    </td></tr>
    </table>
    <table id="tblNotAlloted" runat="server" width="100%">
    <tr><td colspan="2" class="pageheader">&nbsp;Allotted List</td></tr>
    <tr><td colspan="2">
        <asp:GridView ID="gvRSimsView" Width="100%" EmptyDataText="No Records Found!" 
        AutoGenerateColumns="false" CssClass="gridview" runat="server"> 
        
    <Columns>
    <asp:BoundField DataField="SID" HeaderText="ID" />
    <asp:BoundField DataField="AllottedTo" HeaderText="EmpID" />
    <asp:BoundField DataField="Name" HeaderText="Name" />
   
    <asp:BoundField DataField="SIMNo" HeaderText="Phone No" />
    <asp:BoundField DataField="Category" HeaderText="Type" />
    <asp:BoundField DataField="AmountLimit" HeaderText="AmountLimit" />
    <asp:BoundField DataField="AllottedFrom1" HeaderText="From" />
    <asp:BoundField DataField="Upto1" HeaderText="Upto" />
         
     
    </Columns>
    </asp:GridView>
    </td></tr>
    </table>
    </div>
    </form>
</body>
</html>
