<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="TodoListInfo.aspx.cs" Inherits="AECLOGIC.ERP.HMS.TodoListInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
<table id="tblNotStarted" runat="server" width="100%">
<tr><td colspan="2" class="pageheader">Not Started</td></tr>
<tr><td>
    <asp:GridView ID="gvNS" AutoGenerateColumns="false" CssClass="gridview" runat="server">
    <Columns>
   <asp:TemplateField><ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate></asp:TemplateField> 
    <asp:BoundField DataField="ToDoID"  />
    <asp:BoundField DataField="info"  />
    </Columns>
    </asp:GridView>
</td></tr>
</table>
<table id="tbldueFinish" runat="server" width="100%">
<tr><td colspan="2" class="pageheader">Due Date Finished</td></tr>
<tr><td>
    <asp:GridView ID="gvDF" AutoGenerateColumns="false" CssClass="gridview" runat="server">
    <Columns>
   <asp:TemplateField><ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate></asp:TemplateField> 
    <asp:BoundField DataField="ToDoID"  />
    <asp:BoundField DataField="info"  />
    </Columns>
    </asp:GridView>
</td></tr>
</table>
<table id="tblTaskCompleted" runat="server" width="100%">
<tr><td colspan="2" class="pageheader">Task Completed</td></tr>
<tr><td>
    <asp:GridView ID="gvTC" AutoGenerateColumns="false" CssClass="gridview" runat="server">
    <Columns>
   <asp:TemplateField><ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate></asp:TemplateField> 
    <asp:BoundField DataField="ToDoID"  />
    <asp:BoundField DataField="info"  />
    </Columns>
    </asp:GridView>
</td></tr>
</table>
<table id="tblNotUpd" runat="server" width="100%">
<tr><td colspan="2" class="pageheader">Not Updating</td></tr>
<tr><td>
    <asp:GridView ID="gvNU" AutoGenerateColumns="false" CssClass="gridview" runat="server">
    <Columns>
   <asp:TemplateField><ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate></asp:TemplateField> 
    <asp:BoundField DataField="ToDoID"  />
    <asp:BoundField DataField="info"  />
    </Columns>
    </asp:GridView>
</td></tr>
</table>
</asp:Content>

