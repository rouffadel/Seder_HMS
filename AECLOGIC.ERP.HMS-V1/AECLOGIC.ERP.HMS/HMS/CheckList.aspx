<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="CheckList.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.CheckList" Title="" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

    <script type="text/javascript" language="javascript">
function validatesave()
{
     if(document.getElementById('<%=txtListItem.ClientID%>').value=="")
      {    
         alert("Please Enter  TaskName ");
         return false;
      }
       if(document.getElementById('<%=txtAuthority.ClientID%>').value=="")
      {    
         alert("Please Enter  Authority ");
         return false;
      }
}

    </script>

    <table width="100%">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:MultiView ID="mainview" runat="server">
                                <asp:View ID="Newvieew" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" class="pageheader">
                                                To-Do List Item
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 103px">
                                                Task</td>
                                            <td>
                                                <asp:TextBox ID="txtListItem" runat="server" Width="406px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 103px">
                                                Authority
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAuthority" runat="server" Width="405px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 103px">
                                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="savebutton" Width="100px"
                                                    OnClick="btnsubmit_Click" /></td>
                                            <td style="text-align: left">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="EditView" runat="server">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                        <tr>
                                            <td class="pageheader">
                                                To-Do List
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="lnknew" CssClass="savebutton" Width="100" runat="server" Text="Add"  OnClick="lnknew_Click"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width: 100%">
                                                <asp:GridView ID="gvToddoList" Width="100%" runat="server" AutoGenerateColumns="False"
                                                    CellPadding="4" ForeColor="#333333" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                                    HeaderStyle-CssClass="tableHead" OnRowCommand="gvToddoList_RowCommand" CssClass="gridview">
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Task">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblListItem" runat="server" Text='<%#Eval("ListItem")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Authority">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAuthority" runat="server" Text='<%#Eval("Authority")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkedt" runat="server" CommandArgument='<%#Eval("ListID")%>'
                                                                    CommandName="Edt" Text="Edit"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="60px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkdel" runat="server" CommandArgument='<%#Eval("ListID") %>'
                                                                    CommandName="Del" Text="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="60px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <EditRowStyle BackColor="#999999" />
                                                    <EmptyDataRowStyle CssClass="EmptyRowData" />
                                                    <HeaderStyle CssClass="tableHead" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="ChklistView" runat="server">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                        <tr>
                                            <td class="pageheader" >
                                                To-Do List
                                            </td>
                                            <td align="right">
                                               </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                Worksite<span style="color: red">*</span>&nbsp;&nbsp;
                                                 <asp:DropDownList ID="ddlWS" runat="server" Width="200" AutoPostBack="true" CssClass="droplist"  OnSelectedIndexChanged="ddlWS_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                           <td colspan="2">
                                              <div id="dvchecklist" runat="server" visible="false">
                                                  <table width="100%">
                                                   <tr>
                                            <td  style="width: 100%">
                                                <asp:GridView ID="gvCheckList" Width="100%" runat="server" AutoGenerateColumns="False"
                                                    CellPadding="4" ForeColor="#333333" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                                    HeaderStyle-CssClass="tableHead" OnRowCommand="gvToddoList_RowCommand" CssClass="gridview">
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCheckListID" runat="server" Text='<%#Eval("CheckListID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                       
                                                        <asp:TemplateField HeaderText="Task">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblListItem" runat="server" Text='<%#Eval("ListItem")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Authority">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAuthority" runat="server" Text='<%#Eval("Authority")%>' Width="140px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="StartDate">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtStartDate" runat="server" Text='<%#Eval("StartDate")%>'></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate"
                                                                    PopupButtonID="txtStartDate" Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FinishDate">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtFinishDate" runat="server" Text='<%#Eval("EndDate")%>'></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFinishDate"
                                                                    PopupButtonID="txtFinishDate" Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:DropDownList  ID="ddlStatus"  runat="server"  SelectedIndex='<%# GetStatusIndex(Eval("StatusID").ToString())%>'  DataTextField = "StatusName" DataValueField = "StatusID" DataSource='<%# BindStatus()%>'  AutoPostBack="false" CssClass="droplist"  ></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTodoListID" runat="server" Text='<%#Eval("ListID")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <EditRowStyle BackColor="#999999" />
                                                    <EmptyDataRowStyle CssClass="EmptyRowData" />
                                                    <HeaderStyle CssClass="tableHead" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnCheckSave" runat="server" Text="Save" CssClass="savebutton" Width="100px" OnClick="btnCheckSave_Click" />
                                            </td>
                                        </tr>
                                                  </table>
                                              
                                              
                                              </div>
                                           
                                           </td>
                                        
                                        
                                        </tr>
                                       
                                    </table>
                                </asp:View>
                            </asp:MultiView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
