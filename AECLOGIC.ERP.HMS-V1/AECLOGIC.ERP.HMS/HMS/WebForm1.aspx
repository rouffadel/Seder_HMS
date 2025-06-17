<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMS.WebForm1" 
    MasterPageFile="~/Templates/CommonMaster.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
      
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView runat="server" ID="grvEmp" AutoGenerateColumns="false" CssClass="gridview"
                    OnRowCancelingEdit="grvEmp_RowCancelingEdit" OnRowEditing="grvEmp_RowEditing" OnRowUpdating="grvEmp_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="ID"><%--<HeaderTemplate>Employee Name</HeaderTemplate>--%>
                            <ItemTemplate><%# Eval("DepartmentUId")%> </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("DepartmentUId")%>' ID="lblId"></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department Name"><%--<HeaderTemplate>Employee Name</HeaderTemplate>--%>
                            <ItemTemplate><%# Eval("DepartmentName")%> </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# Eval("DepartmentName")%>' ID="txtName"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" Text="Edit" runat="server" CommandName="Edit" CssClass="anchor__grd edit_grd" /><br />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" CssClass="anchor__grd edit_grd" /><br />
                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CommandName="Cancel" CssClass="anchor__grd dlt" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grvEmp" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Label runat="server" ID="lblWarning" ForeColor="Red"></asp:Label>
    </div>
   
</asp:Content>
