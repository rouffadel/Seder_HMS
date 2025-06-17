<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveAccountOpeningBalance.aspx.cs" Inherits="AECLOGIC.ERP.HMS.LeaveAccountOpeningBalance" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <style>
                .MyCalendar .ajax__calendar_container {
                    background-color: White;
                    color: black;
                    border: 1px solid #646464;
                }

                    .MyCalendar .ajax__calendar_container td {
                        background-color: White;
                        padding: 0px;
                    }
            </style>
            <script language="javascript" type="text/javascript">
                function GetEmpID(source, eventArgs) {
                    var HdnKey = eventArgs.get_value();
                    document.getElementById('<%=txtEmpNameHidden.ClientID %>').value = HdnKey;
        }
            </script>

            <table>
                <tr>
                    <td>
                        <strong>&nbsp;Name&nbsp;
                                                        <asp:HiddenField ID="txtEmpNameHidden" runat="server" />
                            <asp:TextBox ID="txtEmpName" runat="server" Height="22px" Width="300px" AccessKey="2" ToolTip="[Alt+2]"></asp:TextBox>&nbsp;
						                                         <cc1:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters="" Enabled="True"
                                                                     MinimumPrefixLength="1" ServiceMethod="GetCompletionList_EmpName" ServicePath="" TargetControlID="txtEmpName"
                                                                     UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                     CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                     OnClientItemSelected="GetEmpID">
                                                                 </cc1:AutoCompleteExtender>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtEmpName"
                                WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Name]"></cc1:TextBoxWatermarkExtender>
                        </strong>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" ToolTip="Search" runat="server" CssClass="btn btn-primary"
                            Text="Search" OnClick="btnSearch_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gdvLeaveOB" runat="server" AutoGenerateColumns="False"
                            EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                            CssClass="gridview" Width="90%" OnRowCommand="gdvLeaveOB_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="EmpID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" runat="server" Text='<%#Bind("EmpId")%>' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="400px" />
                                    <ItemStyle Width="400px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Cut Off Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCutOffDate" runat="server" Width="70px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtCutOffDate" CssClass="MyCalendar"
                                            TargetControlID="txtCutOffDate" Format="dd MMM yyyy"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PayableDays" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPayableDays" runat="server" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCalculate" runat="server" Text="Calc" CommandName="Calc" CommandArgument='<%#Eval("EmpId")%>'
                                            CssClass="anchor__grd edit_grd"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px" colspan="2">
                        <uc1:Paging ID="EmpListPaging" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gdvLeaveDetails" runat="server" AutoGenerateColumns="False"
                            EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                            CssClass="gridview" Width="50%" OnRowCommand="gdvLeaveDetails_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="LeaveTypeID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLTypesID" runat="server" Text='<%#Bind("LTypesID")%>' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="LeaveType" HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Credit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLeavesCr" runat="server" Text='<%#Bind("LeavesCr")%>' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSubmit" ToolTip="Submit" runat="server" CssClass="btn btn-primary"
                            Text="Submit" OnClick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
