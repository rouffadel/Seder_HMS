<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="excelimport.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.Importexcel" MasterPageFile="~/Templates/CommonMaster.master"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
     <asp:updatepanel runat="server" ID="UpdatePanel1">
  <ContentTemplate>
    <script language="javascript" type="text/javascript">
 
    
    </script>

    <table cellpadding="1" cellspacing="1" width="100%">
        
        <tr>
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4" valign="top">
                <asp:FileUpload ID="fileupload" runat="server" Width="400" AccessKey="c" ToolTip="[Alt+c OR Alt+c+Enter]" />
                <asp:Button ID="Button1" runat="server" Text="Import" OnClick="btnImport_Click" CssClass="btn btn-success" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]"
                    Width="61px" />
                <asp:HyperLink ID="hyperlink1" runat="server" CssClass="btn btn-primary" Text="Download a sample Excel format..." Target="_blank"
                    NavigateUrl="~/Reports/Departments_Sample_Format.xls"></asp:HyperLink>
                <asp:HiddenField ID="hdFileName" runat="server" />
                <asp:HiddenField ID="hdFile" runat="server" />
                  <br />
                                            <span style="color:#FF8C00; font-size: 12px;">
                                            <b><u>Instructions to be complied before uploading
                                        EXCEL files!</u></b></span><br />
                                      
                                        <span style="color:#FF8C00; font-size: 12px;">
                                        <b>1.</b> Only Excel files with .xls extension shall be acceptable.
                                        <br />
                                        <b>2.</b> Save AS your other formats before choosing file to ino XLS format.
                                        <br />
                                        <b>3.</b> <b>EXCEL</b> file should not contain <b>BLANK ROWS</b>.
                                          
                                                </span>

            </td>
        </tr>
        <tr>
            <td colspan="4">
            <hr   />
            </td>
        </tr>
        <tr>
         <td colspan="4">
         <asp:Panel ID="pnlcolorshow" runat="server"  CssClass="box box-primary">
         <table>
         <tr><td>Inserted--> </td><td style="background-color:#90EE90;width:50px"></td ><td></td><td></td><td><span> Already Exists--></span></td><td style="background-color:#FFA07A; width:50px"></td></tr>
         </table>
         </asp:Panel>
         </td>
        </tr>
    
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvMapping" runat="server"  CssClass="gridview" HeaderStyle-CssClass="tableHead"
                    EmptyDataText="Data not found to Import" EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BorderColor="GhostWhite"
                    AutoGenerateColumns="false">
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True"   ForeColor="White" />
                    <Columns>
                        <asp:TemplateField >
                        <HeaderTemplate>
                        <asp:CheckBox ID="chkall"  Checked="true" AutoPostBack="true"  OnCheckedChanged="allcheck"  Font-Size="10px" runat="server" Text="All" />
                        </HeaderTemplate>
                        <ItemTemplate>
                          <asp:CheckBox ID="chkitem" Checked="true" runat="server"  />
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField >
                        <HeaderTemplate> <span style="font-size:10px; font-family:Verdana"> Import Departments from Excel...</span>  </HeaderTemplate>
                    
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblparti" Text='<% #Eval("DepartmentsName") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnSave" runat="server" Text="Save" Visible="false"  OnClick="BtnSave_Click" Width="100px" AccessKey="s" ToolTip="[Alt+s OR Alt+s+Enter]"
                    CssClass="savebutton" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" Text="Masters impoted with errors!" Visible="false"></asp:Label>
                <asp:LinkButton ID="lnkError" runat="server" Text="Click here to download" Visible="false"
                    OnClick="lnkError_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTSError" runat="server" Text="Duplicate TripSheets were found!"
                    Visible="false"></asp:Label>
                <asp:LinkButton ID="lnkTSError" runat="server" Text="Click here to download" Visible="false"
                    OnClick="lnkTSError_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnback" runat="server" Text="Back" OnClick="btnback_Click" Visible="false" />
            </td>
        </tr>
    </table>
      </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="Button1" />
        </Triggers>
</asp:updatepanel>
<asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
  <ProgressTemplate>
   <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
    please wait...
  </ProgressTemplate>

 </asp:UpdateProgress>
</asp:Content>
