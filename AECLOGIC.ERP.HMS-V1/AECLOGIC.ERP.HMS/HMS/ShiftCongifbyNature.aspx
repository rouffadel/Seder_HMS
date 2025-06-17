<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShiftCongifbyNature.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMS.ShiftCongifbyNature" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <asp:Label runat="server" ID="lblStatus" Text="" Font-Size="14px"></asp:Label>
            <script type="text/javascript" language="javascript">
                function Valid() {
                    if (!chkAddress('<%=txtName.ClientID %>', 'Shit name', true, ''))
                return false;
        }

            </script>
            <%--<asp:Panel ID="Panel2" runat="server" CssClass="DivBorderOlive">--%>
            <table align="left" id="tblshiftEdit" runat="server" visible="true">
                <tr>
                    <td>DayName<span><span style="color: #ff0000">*</span></span> :
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>InTime:
                    </td>
                    <td width="80px">Hours<asp:DropDownList ID="ddlInTime" runat="server" CssClass="droplist" Width="40px">
                        <asp:ListItem Text="01" Value="01"></asp:ListItem>
                        <asp:ListItem Text="02" Value="02"></asp:ListItem>
                        <asp:ListItem Text="03" Value="03"></asp:ListItem>
                        <asp:ListItem Text="04" Value="04"></asp:ListItem>
                        <asp:ListItem Text="05" Value="05"></asp:ListItem>
                        <asp:ListItem Text="06" Value="06"></asp:ListItem>
                        <asp:ListItem Text="07" Value="07"></asp:ListItem>
                        <asp:ListItem Text="08" Value="08"></asp:ListItem>
                        <asp:ListItem Text="09" Value="09"></asp:ListItem>
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                    </asp:DropDownList>

                    </td>
                    <td>Minutes<asp:TextBox ID="txtITMinutes" runat="server" CssClass="droplist" Width="40px"></asp:TextBox></td>
                    <td>&nbsp;&nbsp;<asp:DropDownList ID="ddlInTimeFormat" runat="server" CssClass="droplist" Width="40px">
                        <asp:ListItem Text="AM" Value="1"></asp:ListItem>
                        <asp:ListItem Text="PM" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td>OutTime:
                    </td>
                    <td width="80px">Hours<asp:DropDownList ID="ddlOutTime" runat="server" CssClass="droplist" Width="40px">
                        <asp:ListItem Text="01" Value="01"></asp:ListItem>
                        <asp:ListItem Text="02" Value="02"></asp:ListItem>
                        <asp:ListItem Text="03" Value="03"></asp:ListItem>
                        <asp:ListItem Text="04" Value="04"></asp:ListItem>
                        <asp:ListItem Text="05" Value="05"></asp:ListItem>
                        <asp:ListItem Text="06" Value="06"></asp:ListItem>
                        <asp:ListItem Text="07" Value="07"></asp:ListItem>
                        <asp:ListItem Text="08" Value="08"></asp:ListItem>
                        <asp:ListItem Text="09" Value="09"></asp:ListItem>
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                    </asp:DropDownList></td>
                    <td>Minutes<asp:TextBox ID="txtOTMinutes" runat="server" CssClass="droplist" Width="40"></asp:TextBox></td>
                    <td>&nbsp;
                <asp:DropDownList ID="ddlOutTimeFormat" runat="server" CssClass="droplist" Width="40">
                    <asp:ListItem Text="AM" Value="1"></asp:ListItem>
                    <asp:ListItem Text="PM" Value="2"></asp:ListItem>
                </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSave_Click"
                            OnClientClick="javascript:return Valid();" AccessKey="s" ToolTip="[Alt+s OR Alt+s+Enter]" />
                    </td>
                </tr>
            </table>
            <%--</asp:Panel>--%>

            <table id="tblTimeView" runat="server" visible="false" width="100%">
                <tr>
                    <td>
                        <cc1:Accordion ID="LstOfHolidayConAccordion" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="83%">
                            <Panes>
                                <cc1:AccordionPane ID="LstOfHolidayConAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                            Search Criteria</Header>
                                    <Content>
                                        &nbsp;&nbsp;
                                         <b>
                                             <asp:Label ID="Label2" runat="server" Text="Shift"></asp:Label>:
                                                   <asp:DropDownList ID="ddlShift" runat="server" CssClass="droplist">
                                                   </asp:DropDownList>
                                         </b>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <b>
                                                <asp:Label ID="Label3" runat="server" Text="Weeks"></asp:Label>:
                                                   <asp:DropDownList ID="ddlWks" runat="server" CssClass="droplist">
                                                   </asp:DropDownList>

                                            </b>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <b>
                                                            <asp:Label ID="lblNature" runat="server" Text="Emp Nature"></asp:Label>:</b>

                                        <asp:DropDownList ID="ddlEmpNature" runat="server" CssClass="droplist">
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />




                                    </Content>
                                </cc1:AccordionPane>
                            </Panes>
                        </cc1:Accordion>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvTimeView" AutoGenerateColumns="false" runat="server" OnRowCommand="gvTimeView_RowCommand"
                            CssClass="gridview" Width="83%">
                            <Columns>
                                <asp:BoundField HeaderText="DayName" DataField="DayName" />
                                <asp:BoundField HeaderText="InTime" DataField="InTime" />
                                <asp:BoundField HeaderText="OutTime" DataField="OutTime" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" CommandName="Edt" CommandArgument='<%#Eval("ShiftConfig")%>'
                                            runat="server" CssClass="anchor__grd edit_grd ">Edit</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

