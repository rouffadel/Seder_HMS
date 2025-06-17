<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="PaySlip.aspx.cs" Inherits="AECLOGIC.ERP.HMS.PaySlip" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>  
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
  
  <script language="javascript" type="text/javascript">
       

  </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        
       
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            Worksite
                            <asp:DropDownList ID="ddlworksites"  CssClass="droplist" runat="server">
                            </asp:DropDownList>
                            &nbsp;&nbsp;Department
                            <asp:DropDownList ID="ddldepartments" CssClass="droplist"  runat="server">
                            </asp:DropDownList>
                            &nbsp;&nbsp;Filter Employee
                            <asp:TextBox ID="txtusername" runat="server" MaxLength="50" OnTextChanged="btnSearch_Click"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                CssClass="savebutton" Width="80px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                             Month:  <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" 
                                CssClass="droplist" onselectedindexchanged="ddlMonth_SelectedIndexChanged" >
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                Year <asp:DropDownList ID="ddlYear" runat="server"  AutoPostBack="true" 
                                onselectedindexchanged="ddlYear_SelectedIndexChanged" CssClass="droplist" >
                                
                                </asp:DropDownList>
                                
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
                            <asp:GridView ID="gvPaySlip" runat="server" AutoGenerateColumns="False" 
                                 DataKeyNames="EmpId" HeaderStyle-CssClass="tableHead" 
                                EmptyDataRowStyle-CssClass="EmptyRowData" EmptyDataText="No Records Found" 
                                OnRowCommand="GridView1_RowCommand" Width="100%" CssClass="gridview">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="EmpID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("EmpId")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Design" HeaderStyle-HorizontalAlign="Left" 
                                        HeaderText="Designation" />
                                    <asp:BoundField DataField="Category" HeaderStyle-HorizontalAlign="Left" 
                                        HeaderText="Trades" />
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Worksite">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorksite" runat="server" Text='<%#Eval("Site_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("DepartmentName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Payslip">
                                        <ItemTemplate>
                                            <a ID="A1" target="_blank"
                                                href='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "EmpId").ToString()) %>' 
                                                 runat="server">View</a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 17px">
                            <uc1:paging ID="EmpListPaging"  runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

