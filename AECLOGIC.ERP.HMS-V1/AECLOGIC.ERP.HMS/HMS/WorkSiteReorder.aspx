<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="WorkSiteReorder.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.WorkSiteReorder" Title="" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
    <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr><td colspan="4" width="100%"> <AEC:Topmenu ID="topmenu" runat="server" /></td></tr>
    
      <tr><td colspan="6"></td></tr>
        
        <tr align="left" >
           <td colspan="2" style="vertical-align:top;" width="20%">
               <asp:ListBox ID="lstWorkStes" runat="server" width="100%" CssClass="box box-primary" Height="400px" 
                   TabIndex="1"></asp:ListBox>
           
           </td>
           <td style="vertical-align:middle"; width="10%" align="left">
              <asp:Button ID="btnFirst" runat="server" CssClass="btn btn-success" Text="Move First" Width="80px" 
                   OnClick="btnFirst_Click" TabIndex="2" /><br /><br />
              <asp:Button ID="btnUp" runat="server" Text="Move Up" CssClass="btn btn-primary" Width="80px" 
                   OnClick="btnUp_Click" TabIndex="3"/><br /><br />
              <asp:Button ID="btnDown" runat="server" Text="Move Down" CssClass="btn btn-warning" Width="80px" 
                   OnClick="btnDown_Click" TabIndex="4"/><br /><br />
              <asp:Button ID="btnLast" runat="server" Text="Move Last" CssClass="btn btn-danger" Width="80px" 
                   OnClick="btnLast_Click" TabIndex="5"/><br /><br />
           </td>
           
                                 <td  style="vertical-align:middle">
                                     <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary"
                                         Width="100px" OnClick="btnSubmit_Click" AccessKey="s" TabIndex="6" 
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

