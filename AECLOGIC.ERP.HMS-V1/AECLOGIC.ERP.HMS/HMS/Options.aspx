<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="Options.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Options" MasterPageFile="~/Templates/CommonMaster.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <table id="tblEdit" runat="server" visible="false" width="100%">
        <tr>
            <td style="width: 170Px">
                <b>Option:</b>
            </td>
            <td>
                <asp:TextBox ID="txtOption" Width="170px" runat="server" TabIndex="1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 170Px">
                <b>Value:</b>
            </td>
            <td>
                <asp:TextBox ID="txtvalue" Width="170px" runat="server" TabIndex="2"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 170Px">
                <b></b>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 170Px">
                <b></b>
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="savebutton" 
                    OnClick="btnSave_Click" AccessKey="s" TabIndex="3" 
                    ToolTip="[Alt+s OR Alt+s+Enter]" />
            </td>
        </tr>
        <tr>
            <td style="padding-left: 150Px">
                &nbsp;
            </td>
        </tr>
    </table>
    <table id="tblView" runat="server" visible="false">
        <tr>
            <td>
                <asp:GridView ID="gvView" AutoGenerateColumns="false" Width="100%" CssClass="gridview"
                    runat="server" OnRowDataBound="gvView_RowDataBound"  >
                    <Columns>
                        <asp:BoundField HeaderText="OptionID" Visible ="true"    DataField="OptionID" />
                       
                       <%-- <asp:TemplateField HeaderText="Purpose">
                        <ItemTemplate>
                          <asp:TextBox ID="txtpur" runat ="server" Text='<%# Eval("Purpose") %>'  Style="border-style:none; background-color:transparent"     ></asp:TextBox>
                        <cc1:HoverMenuExtender ID="HoverMenuExtender2" runat="server" PopupPosition="Left"
                            TargetControlID="txtpur" PopupControlID="panelPopUp1"
                            PopDelay="20" OffsetX="-5" OffsetY="-5"> </cc1:HoverMenuExtender>
                         <asp:Panel ID="panelPopUp1" style="display:none" runat="server" BackColor="White" ForeColor="Black" BorderStyle="Solid" BorderWidth="1px">
                         <span>click to edit purpose!</span> 
                          </asp:Panel>
                         
                        </ItemTemplate>
                        </asp:TemplateField>--%>
                         
                          <asp:BoundField HeaderText="Purpose" Visible ="true"    DataField="Purpose" />
                         <asp:BoundField HeaderText="DependencyDesc" Visible ="true"    DataField="dependencyDesc" />
                        <asp:BoundField HeaderText="Value" Visible ="true"    DataField="Value" />
                        <asp:TemplateField HeaderText="Value" Visible="false">
                        <ItemTemplate>
                          <asp:TextBox ID="txtval" runat ="server"  Text='<%# Eval("Value") %>'  Style="border-style:none; background-color:	white"     ></asp:TextBox>
                            <asp:HiddenField  runat ="server"  ID="hdval" Value='<%# Eval("OptionID") %>' />
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions" Visible="false" >
                        <ItemTemplate>
                        <asp:ImageButton runat ="server"  ID="imgupdate"  ImageUrl="~/Images/editk.ico" ToolTip="Update" OnClientClick ="return confirm('Are you sure want to update?');"     OnClick ="btnupdt_Click"  Width="16px" Height="16px" />&nbsp;&nbsp;|&nbsp;&nbsp;<asp:ImageButton runat ="server"  ID="imgbtnDel"  ImageUrl="~/Images/Delete.ico" Width="15px" Height="16px" OnClientClick ="return confirm('Are you sure want to delete?');"     OnClick ="btnupdt_Click"  ToolTip="Delete" />
                        <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" PopupPosition="Left"
                            TargetControlID="txtval" PopupControlID="panelPopUp"
                            PopDelay="20" OffsetX="-5" OffsetY="-5"> </cc1:HoverMenuExtender>
                         <asp:Panel ID="panelPopUp" runat="server" style="display:none" BackColor="white" ForeColor="Black" BorderStyle="Solid" BorderWidth="1px">
                         <span>click to edit value!</span> 
                          </asp:Panel>
                        </ItemTemplate>
                        </asp:TemplateField>
                  
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 17px">
                <uc1:Paging ID="OptionsPaging" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
