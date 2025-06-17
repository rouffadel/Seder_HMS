<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="EmployeeContacts.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmployeeContacts" Title="Employee Contacts" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
      <tr>
          <td>
             <asp:UpdatePanel ID="updmaintable" runat="server">
                 <ContentTemplate>
                 
                 <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td >
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                 <tr>
                       <td class="pageheader">
                       Contacts List
                       
                       </td>
                    
                    </tr>
                <tr>
                       <td>
                       &nbsp;
                       
                       </td>
                    
                    </tr>
                    
                     <tr>
                        <td colspan="2">
                            Worksite &nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlworksites" runat="server"  CssClass="droplist" >
                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;Departments&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList
                                ID="ddldepartments" runat="server"  CssClass="droplist" >
                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;Employee Name &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox
                                ID="txtusername" runat="server" MaxLength="50">&nbsp;&nbsp;&nbsp;&nbsp;</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                    ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="savebutton"
                                    Width="100px" />
                        </td>
                    </tr>
                    
                    <tr>
                       <td>
                       &nbsp;
                       
                       </td>
                    
                    </tr>
                   <tr>
                        <td>
                            &nbsp;<asp:RadioButton ID="rbActive" runat="server" AutoPostBack="True" Checked="True"
                                GroupName="Emp" Text="Active EmployeeList" OnCheckedChanged="rbActive_CheckedChanged" />
                            <asp:RadioButton ID="rbInActive" runat="server" AutoPostBack="True" GroupName="Emp"
                                Text="Inactive EmployeeList" OnCheckedChanged="rbInActive_CheckedChanged" />
                        </td>
                    </tr>
                      <tr>
                       <td>
                       &nbsp;
                       
                       </td>
                    
                    </tr>
                    <tr>
                        <td>
                        
                        
                            <asp:GridView ID="grdEmpcontacts" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                                GridLines="Both" Width="100%" CellPadding="4"  OnPageIndexChanging="grdEmpcontacts_PageIndexChanging" PageSize="20" 
                                HeaderStyle-CssClass="tableHead" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Designation" HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Mobile1" HeaderText="Mobile" HeaderStyle-HorizontalAlign="Left">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Mobile2" HeaderText="Alternate Mobile" HeaderStyle-HorizontalAlign="Left">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Mailid" HeaderText="Mail Id" HeaderStyle-HorizontalAlign="Left">
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="skypeid" HeaderText="Skype Id" HeaderStyle-HorizontalAlign="Left">
                                        
                                    </asp:BoundField>
                                    
                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333"/>
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                               
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                            </asp:GridView>
                        </td>
                    </tr>
                     <tr>
                        <td style="height: 17px">
                            <uc1:Paging ID="EmpListPaging" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
                 
                 </ContentTemplate>
             
             
             </asp:UpdatePanel>
          
          </td>
      
      </tr>

</table>


</asp:Content>

