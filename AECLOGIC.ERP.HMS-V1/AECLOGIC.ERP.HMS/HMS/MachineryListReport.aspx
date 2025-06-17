<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="MachineryListReport.aspx.cs" Inherits="AECLOGIC.ERP.HMS.MachineryListReport" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>    

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
    <table width="100%" >
<tr>
<td>
 <asp:CheckBoxList ID="chkListFields" runat="server" RepeatColumns="6" Width="100%">
     <asp:ListItem Selected="True" Value="0">Photo</asp:ListItem>
     <asp:ListItem Selected="True" Value="1">Machinery</asp:ListItem>
     <asp:ListItem Selected="True"  Value="2" >Basic Cost</asp:ListItem>
     <asp:ListItem Selected="True" Value="3">WorkSite</asp:ListItem>
     
    </asp:CheckBoxList>
</td>

</tr>
<tr>

<td>

    <asp:Button ID="btnSaveButton" runat="server" CssClass="savebutton" 
        onclick="btnSaveButton_Click" Text="Generate Report" />

    <asp:Button ID="btnExpExcel" runat="server" CssClass="savebutton" 
         Text="Export to Excel" onclick="btnExpExcel_Click" />

</td></tr>
<tr>
<td>
    <asp:GridView Visible="false" ID="gvReport" AutoGenerateColumns="False" 
        runat="server" Width="100%" CssClass="gridview">
        <Columns>
            <asp:ImageField DataImageUrlField="Photo" 
                DataImageUrlFormatString="http:\\www.bssprojects.com\MMS\MachinaryImages\{0}" HeaderText="Photo">
            </asp:ImageField>
            <asp:BoundField DataField="Machinery" HeaderText="Machinery" />
            <asp:BoundField DataField="BasicCost" HeaderText="Basic Cost" />
            <asp:BoundField DataField="WorkSite" HeaderText="Worksite" />
        </Columns>
    
    </asp:GridView>
    
    </td>
</tr>

<tr>

<td> </td>
</tr>
</table>
</asp:Content>

