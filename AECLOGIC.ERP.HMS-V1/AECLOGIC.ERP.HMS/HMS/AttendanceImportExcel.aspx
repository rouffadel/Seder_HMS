<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="AttendanceImportExcel.aspx.cs" Inherits="AECLOGIC.ERP.HMS.AttendanceImportExcel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">

<table cellpadding="1" cellspacing="1" width="100%">
        <tr>
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4" valign="top">
                <asp:FileUpload ID="fileupload" runat="server" Width="400" />
                <asp:Button ID="Button1" runat="server" Text="Import" OnClick="btnImport_Click" CssClass="savebutton" />
                <asp:HiddenField ID="hdFileName" runat="server" />
                <asp:HiddenField ID="hdFile" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="1" align="left">
                <div id="dvTUQD" runat="server" visible="false">
                    <asp:UpdatePanel ID="upMain" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlOuterTop" runat="server" BackColor="#DCDCDC" Width="750px">
                                <asp:Panel ID="panel1" runat="server" BackColor="#DCDCDC" Width="740px" Style="margin-left: auto;
                                    margin-right: auto; margin-bottom: auto; margin-top: auto;">
                                    <table>
                                        
                                       
                                        <tr>
                                            <td>
                                                EmpID
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEmpID" runat="server" Width="25px" MaxLength="1">A</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                               Employee Name
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEmpName" runat="server" Width="25px" MaxLength="1">B</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Date
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDate" runat="server" Width="25px" MaxLength="1">c</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                InTime
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInTime" runat="server" Width="25px" MaxLength="1">D</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                               OutTime
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOutTime" runat="server" Width="25px" MaxLength="1">E</asp:TextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>
                                               Remarks
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRemarks" runat="server" Width="25px" MaxLength="1">F</asp:TextBox>
                                            </td>
                                        </tr>
                                        
                                       
                                    </table>
                                </asp:Panel>
                            </asp:Panel>
                        </ContentTemplate>
                       
                    </asp:UpdatePanel>
                </div>
            </td>
            <td colspan="3" align="left">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div id="DVMAP" style="overflow: auto; height: 300px" runat="server">
                    <asp:GridView ID="GridViewExcel" runat="server" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="Vertical" ForeColor="Black">
                        <FooterStyle BackColor="#CCCC99" />
                        <RowStyle BackColor="#F7F7DE" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
         <tr>
            <td colspan="4">
                <asp:Button ID="btnSave" runat="server" Text="Save" Visible="false" 
                    CssClass="savebutton" onclick="btnSave_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvMapping" runat="server" BackColor="White" BorderColor="#DEDFDE"
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="Vertical" ForeColor="Black"
                    AutoGenerateColumns="false" CssClass="gridview">
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundField HeaderText="SR.NO" DataField="SrNo" />
                        <asp:BoundField HeaderText="EmpID" DataField="EmpID" />
                        <asp:BoundField HeaderText="EmpName" DataField="EmpName" />
                        <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                        <asp:BoundField HeaderText="InTime" DataField="InTime" />
                        <asp:BoundField HeaderText="OutTime" DataField="OutTime" />
                        <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
       
    </table>
</asp:Content>

