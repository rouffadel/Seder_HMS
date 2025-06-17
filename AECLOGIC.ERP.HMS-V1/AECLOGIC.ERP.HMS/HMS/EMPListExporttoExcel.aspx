<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EMPListExporttoExcel.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Reports_ExporttoExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Reports</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" CssClass="gridview">
          <Columns>
            <asp:ImageField DataImageUrlField="Photo" 
                DataImageUrlFormatString="http:\\www.delcoprojects.in\HMS\EmpImages\{0}" HeaderText="Photo">
            </asp:ImageField>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Designation" HeaderText="Designation" />
             <asp:BoundField DataField="Phone" HeaderText="Phone" />
              <asp:BoundField DataField="DOB" HeaderText="Date of Birth" />
              <asp:BoundField DataField="DOJ" HeaderText="Date of Joining" />
              <asp:BoundField DataField="PerAddress" HeaderText="PerAddress" />
              <asp:BoundField DataField="ResAddress" HeaderText="ResAddress" />
            <asp:BoundField DataField="WorkSite" HeaderText="Worksite" />
            <asp:BoundField DataField="Salary" HeaderText="Salary" />
        </Columns>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
