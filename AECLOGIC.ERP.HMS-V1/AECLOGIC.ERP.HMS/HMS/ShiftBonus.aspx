<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="ShiftBonus.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ShiftBonus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
    <asp:UpdatePanel ID="updmaintable" runat="server">
        <ContentTemplate>
        <table width="100%">
        <tr><td>
            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click"><b>Add</b></asp:LinkButton><b>||</b><asp:LinkButton
                ID="LinkButton2" runat="server" onclick="LinkButton2_Click"><b>View</b></asp:LinkButton></td>
            <td align="right"> 
                    <asp:LinkButton ID="lnkSettings" runat="server" onclick="lnkSettings_Click"><b>Default Settings</b></asp:LinkButton></td></tr></table>
<table id="tblAdd" visible="false" runat="server" width="100%">
<tr><td class="pageheader">Bonus Calculation</td></tr>
<tr><td><b>Select Date:&nbsp; </b><asp:TextBox ID="txtDate" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate" PopupButtonID="txtDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; <b>
    Bonus Percent(%):&nbsp; </b><asp:TextBox ID="txtBonusPercent" runat="server" 
                AutoPostBack="True" ontextchanged="txtBonusPercent_TextChanged"></asp:TextBox>
            &nbsp; * Click Default Settings to Change Default values</td></tr>
            <tr><td> &nbsp;</td></tr>
<tr><td style="height: 24px"><b>Shift A:</b>&nbsp;<b>Target:&nbsp; </b><asp:TextBox ID="txtTrgtS1" runat="server"></asp:TextBox>&nbsp;&nbsp;<b>Collection:</b> 
    <asp:TextBox ID="txtCS1" runat="server" AutoPostBack="True" 
        ontextchanged="txtCS1_TextChanged"></asp:TextBox><b>&nbsp; Revenue: </b><asp:Label ID="lblRevnuS1"
        runat="server" Font-Bold="True" ForeColor="Blue" Width="100px"></asp:Label> <b>
    Bonus Amount: </b>
            <asp:Label
                ID="lblBS1" runat="server" Font-Bold="True" ForeColor="Blue" Width="100px" 
                ToolTip="Based on Bonus Percent"></asp:Label> 
   </td></tr>                            
<tr><td><b>Shift B:</b>&nbsp;<b>Target:&nbsp; </b><asp:TextBox ID="txtTrgtS2" runat="server"></asp:TextBox>&nbsp;&nbsp;<b>Collection: </b><asp:TextBox
        ID="txtCS2" runat="server" AutoPostBack="True" 
        ontextchanged="txtCS2_TextChanged"></asp:TextBox><b>&nbsp; Revenue: </b><asp:Label ID="lblRevnuS2"
        runat="server" Font-Bold="True" ForeColor="Blue" Width="100px"></asp:Label> <b>
    Bonus Amount: </b>
                      <asp:Label
                ID="lblBS2" runat="server" Font-Bold="True" ForeColor="Blue" Width="100px" 
                          ToolTip="Based on Bonus Percent"></asp:Label> &nbsp;</td></tr>                            
<tr><td><b>Shift C:</b>&nbsp;<b>Target:&nbsp; </b><asp:TextBox ID="txtTrgtS3" runat="server"></asp:TextBox>&nbsp;&nbsp;<b>Collection:</b> 
    <asp:TextBox ID="txtCS3" runat="server" AutoPostBack="True" 
        ontextchanged="txtCS3_TextChanged"></asp:TextBox><b>&nbsp; Revenue: </b><asp:Label ID="lblRevnuS3"
        runat="server" Font-Bold="True" ForeColor="Blue" Width="100px"></asp:Label><b>
    Bonus Amount: </b>
                      <asp:Label
                ID="lblBS3" runat="server" Font-Bold="True" ForeColor="Blue" Width="100px" 
                          ToolTip="Based on Bonus Percent"></asp:Label>  </td></tr> 
        <tr><td>&nbsp;</td></tr>
        <tr><td style="padding-left:250Px">
            <asp:Button ID="btnSave" CssClass="savebutton" runat="server" Text="Submit" 
                onclick="btnSave_Click" />
            &nbsp;
            <asp:Button ID="btnCancel" CssClass="savebutton"  runat="server" Text="Cancel" 
                onclick="btnCancel_Click" />
            </td></tr> 
</table>
<table id="tblView" runat="server" visible="false" width="100%">
<tr><td><asp:GridView ID="gvView"  Width="100%" runat="server" 
        AutoGenerateColumns="False" CellPadding="4"  ForeColor="#333333" 
        GridLines="Both"   EmptyDataText="No Records Found" 
        EmptyDataRowStyle-CssClass="EmptyRowData"  HeaderStyle-CssClass="tableHead" 
        onrowcommand="gvView_RowCommand" CssClass="gridview">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%"  />
                                <Columns>
                                <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("BID")%>'></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DateofWork" HeaderText="Date" />
                              
                                  <asp:BoundField DataField="BonusPercent" HeaderText="Bonus-Percent" /> 
                                  <asp:BoundField DataField="ShiftABonus" HeaderText="Shift-A:Bonus" />
                                <asp:BoundField DataField="ShiftBBonus" HeaderText="Shift-B:Bonus" />
                                <asp:BoundField DataField="ShiftCBonus" HeaderText="Shift-C:Bonus" />
                                <asp:BoundField DataField="SubmitedBy" HeaderText="Submitted By" />
                                <asp:BoundField DataField="SubmittedDay" HeaderText="Submitted On" />
                               <asp:TemplateField>
                               <ItemTemplate>
                                   <asp:LinkButton ID="lnkview" runat="server" CommandName="view" CommandArgument='<%#Eval("BID")%>'>View</asp:LinkButton>
                               </ItemTemplate>
                               </asp:TemplateField>
                                <asp:TemplateField>
                               <ItemTemplate>
                                   <asp:LinkButton ID="lnkDel" runat="server"  OnClientClick="return confirm('Are you Sure?');" ForeColor="Red" CommandName="Del" CommandArgument='<%Eval("BID")%>'>Delete</asp:LinkButton>
                               </ItemTemplate>
                               </asp:TemplateField>
                                </Columns></asp:GridView></td></tr>
</table>
<table id="tblSettings" runat="server" visible="false" width="100%">
<tr><td class="pageheader">Default Settings</td></tr>
<tr><td style="width:200Px"><b>Bonus Percent(%):</b></td><td>
    <asp:TextBox ID="txtDBonusPercent" runat="server"></asp:TextBox></td></tr>
    <tr><td style="width:200Px"><b>Shift-A Target:</b></td><td>
    <asp:TextBox ID="txtDTS1" runat="server"></asp:TextBox></td></tr>
    <tr><td style="width:200Px"><b>Shift-B Target:</b></td><td>
    <asp:TextBox ID="txtDTS2" runat="server"></asp:TextBox></td></tr>
    <tr><td style="width:200Px"><b>Shift-C Target:</b></td><td>
    <asp:TextBox ID="txtDTS3" runat="server"></asp:TextBox></td></tr>
    <tr><td></td><td>
        <asp:Button ID="btnDSave" CssClass="savebutton" runat="server" Text="Save" 
            ToolTip="Above values reflect in pages defaultly" onclick="btnDSave_Click" /></td></tr>
</table>
</ContentTemplate>
 </asp:UpdatePanel>
        
</asp:Content>

