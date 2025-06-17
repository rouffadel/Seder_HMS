<%@ Page Title="" Language="C#" AutoEventWireup="True" CodeBehind="Reminder.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Reminder" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table id="tblAdd" runat="server" width="100%">
                <tr>
                    <td style="width: 112px"><b>Valid From:</b></td>
                    <td>
                        <asp:TextBox
                            ID="txtValidFrom" Width="70" runat="server"></asp:TextBox>
                        &nbsp; 
                <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtValidFrom" PopupPosition="Right"
                    Format="dd/MM/yyyy" runat="server"></cc1:CalendarExtender>
                        &nbsp; 
         <cc1:CalendarExtender ID="CalendarExtender3" TargetControlID="txtValidUpto" PopupPosition="Right"
             Format="dd/MM/yyyy" runat="server"></cc1:CalendarExtender>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 112px"><b>Valid Upto:</b></td>
                    <td>
                        <asp:TextBox ID="txtValidUpto" Width="70" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 112px"><b>Due Date:</b></td>
                    <td>
                        <asp:TextBox ID="txtDueDate" runat="server" Width="70"></asp:TextBox>
                        <tr>
                            <td style="width: 112px; height: 24px;"><b>Reminder Days:</b></td>
                            <td style="height: 24px">
                                <asp:TextBox ID="txtRemindDays" Width="40" runat="server"></asp:TextBox>&nbsp;Alert 
            we want to show before Valid Upto</td>
                        </tr>
                        <cc1:CalendarExtender ID="txtDueDate_CalendarExtender" runat="server"
                            Format="dd/MM/yyyy" PopupPosition="Right" TargetControlID="txtDueDate"></cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 112px"><b>Reminder Note:</b></td>
                    <td>
                        <asp:TextBox ID="txtReminder" Width="300" runat="server"></asp:TextBox>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 112px"></td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save"
                            OnClick="btnSave_Click" />
                        &nbsp;
            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger" Text="Clear" />
                    </td>
                </tr>
            </table>
            <table id="tblView" runat="server" width="100%">
                <tr>
                    <td align="left">
                        <asp:GridView ID="gvView" AutoGenerateColumns="false" CssClass="gridview"
                            Width="70%" runat="server" OnRowCommand="gvView_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="WO NO">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnk" CssClass="btn btn-primary" runat="server" NavigateUrl="#" onclick='<%# PONavigateUrl(DataBinder.Eval(Container.DataItem,"POID").ToString())%>'
                                            CommandName="View" Text='<%#Eval("WONO") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Remind Note" DataField="RemindText" />
                                <asp:BoundField HeaderText="SRNID" DataField="SRNID" Visible="false" />
                                <asp:BoundField HeaderText="ValidFrom" DataField="ValidFrom" />
                                <asp:BoundField HeaderText="ValidUpto" DataField="ValidUpto" />
                                <asp:BoundField HeaderText="DueDate" DataField="DueDate" />
                                <asp:BoundField ItemStyle-ForeColor="Red" HeaderText="Status" DataField="Status" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkReg" CssClass="btn btn-success" OnClientClick="javascript:return confirm('Are you sure to Regularise WO?');" CommandName="Reg" CommandArgument='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem,"SRNID")).ToString() %>'  ToolTip="Re-Generate WO" runat="server">Re-Gen</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkReNew" CssClass="btn btn-primary" OnClientClick="javascript:return confirm('Are you sure to Re-Alloat New WO?');" CommandName="ReNew" CommandArgument='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem,"SRNID")).ToString() %>'  ToolTip="Re-Generate WO" runat="server">Re-New</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" CssClass="anchor__grd edit_grd" CommandName="Edt" CommandArgument='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem,"RID")).ToString() %>'  ToolTip="Edit Reminder" runat="server">Edit</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDel" CssClass="btn btn-danger" OnClientClick="javascript:return confirm('Are you sure to Delete?');" CommandName="Del" CommandArgument='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem,"RID")).ToString() %>' ToolTip="Re-Generate WO" runat="server">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
