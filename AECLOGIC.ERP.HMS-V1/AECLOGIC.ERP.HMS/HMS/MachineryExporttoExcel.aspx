<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="MachineryExporttoExcel.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Reports_ExporttoExcel123" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" CssClass="gridview">
            <Columns>
            <asp:ImageField DataImageUrlField="Photo" 
                DataImageUrlFormatString="http:\\www.bssprojects.com\MMS\MachinaryImages\{0}" HeaderText="Photo">
            </asp:ImageField>
            <asp:BoundField DataField="Machinery" HeaderText="Machinery" />
            <asp:BoundField DataField="BasicCost" HeaderText="Basic Cost" />
            <asp:BoundField DataField="WorkSite" HeaderText="Worksite" />
        </Columns>
    
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
