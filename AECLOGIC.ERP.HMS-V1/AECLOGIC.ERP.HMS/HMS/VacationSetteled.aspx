<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VacationSetteled.aspx.cs" Inherits="AECLOGIC.ERP.HMS.VacationSetteled" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function GetEmpID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=txtEmpNameHidden.ClientID %>').value = HdnKey;
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <table id="tblView" width="100%" runat="server">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                        ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                        FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                                        SelectedIndex="0">
                                        <Panes>
                                            <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                              Search Criteria
                                          </Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td>&nbsp;Employee Name&nbsp;
                                                        <asp:TextBox ID="txtdate" Height="20" runat="server" CssClass="hiddencol" Visible="false"></asp:TextBox>
                                                                <asp:HiddenField ID="txtEmpNameHidden" runat="server" />
                                                                <asp:TextBox ID="txtEmpName" runat="server" Height="22px" Width="300px" AccessKey="2" ToolTip="[Alt+2]"></asp:TextBox>&nbsp;
						                                         <cc1:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters="" Enabled="True"
                                                                     MinimumPrefixLength="1" ServiceMethod="GetCompletionList_EmpName" ServicePath="" TargetControlID="txtEmpName"
                                                                     UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                     CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetEmpID">
                                                                 </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtEmpName"
                                                                    WatermarkCssClass="Watermarktxtbox" WatermarkText="[Filter Name]"></cc1:TextBoxWatermarkExtender>
                                                                <asp:CheckBox ID="chkFinal1" runat="server" Text="Final" />
                                                                <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </Content>
                                            </cc1:AccordionPane>
                                        </Panes>
                                    </cc1:Accordion>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvVacation" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead" GridLines="Both"
                            EmptyDataText="No Records Found" Width="100%" CssClass="gridview" OnRowDataBound="gvVacation_RowDataBound" OnRowCommand="gvVacation_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="EmpName" HeaderText="EmployeeName"></asp:BoundField>
                                <asp:BoundField DataField="SettlementDate" HeaderText="Settlement Date"></asp:BoundField>
                                <asp:BoundField DataField="AddedAmount" HeaderText="Added Amount"></asp:BoundField>
                                <asp:BoundField DataField="DeductedAmount" HeaderText="Deducted Amount"></asp:BoundField>
                                <asp:BoundField DataField="TotalAmt" HeaderText="Total Amount"></asp:BoundField>
                                <asp:BoundField DataField="TransId" HeaderText="Trans Id"></asp:BoundField>
                                <asp:BoundField DataField="TransDate" HeaderText="Trans Date"></asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPrint" runat="server" CssClass="anchor__grd vw_grd" Text="Print" CommandArgument='<%#Eval("Empid")%>' CommandName="Print"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" Text="View" CssClass="anchor__grd edit_grd" CommandArgument='<%#Eval("vid")%>'
                                    CommandName="Edt"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:BoundField DataField="Form" HeaderText="Form" Visible="false"></asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblcutdate" runat="server" CssClass="anchor__grd edit_grd" Text='<%#Eval("SettlementDate")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                        <uc1:Paging ID="AdvancedLeaveAppOthPaging" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
            </table>
            <table id="tblEdit" runat="server" visible="false" width="100%">
                <tr>
                    <td>
                        <asp:DataList ID="dtlvacationEdit" runat="server" HeaderStyle-CssClass="datalistHead"
                            Width="100%">
                            <ItemTemplate>
                                <div class="DivBorderOlive" style="margin-bottom: 20px">
                                    <table style="width: 100%; background-color: #efefef;">
                                        <tr>
                                            <td>
                                                <b>EmployeeName :</b>
                                                <asp:Label ID="lblempname" runat="server" Text='<%#Eval("name")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <b>EmployeeId :</b>
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("empid")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <b>DateOfJoin :</b>
                                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("Dateofjoin")%>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Balance Annual Leaves:</b>
                                                <asp:Label ID="lblAAL" runat="server" Text='<%#Eval("AvailableLeaves")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <b>Requested Leaves :</b>
                                                <asp:Label ID="lblRAL" runat="server" Text='<%#Eval("RequestedLeaves")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <b>Granted Leaves :</b>
                                                <asp:Label ID="lblGAL" runat="server" Text='<%#Eval("GrantedDays")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <b>OverTime Hours :</b>
                                                <asp:Label ID="lblOT" runat="server" Text='<%#Eval("OTHours")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="GVVacationedit" Visible="true" runat="server" AlternatingRowStyle-BackColor="GhostWhite"
                                        AutoGenerateColumns="false" ShowFooter="true"
                                        DataSource='<%#BindVacationDetails(Eval("vid").ToString())%>'
                                        HeaderStyle-CssClass="tableHead" Width="100%" CssClass="gridview">
                                        <Columns>
                                            <asp:BoundField DataField="Description" HeaderText="Credit/Debit"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Value">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtA1" OnTextChanged="QtyChangedEdit" Style="text-align: right" AutoPostBack="true" Width="120px" Height="20px" runat="server" Text='<%#Bind("Amount")%>'></asp:TextBox>
                                                    <br />
                                                    <%--<asp:LinkButton ID="lnkEAL" Text="Master data must be supplied to populate this field" runat="server" CssClass="btn btn-primary" Visible="false" OnClick="lnkEAL_Click"></asp:LinkButton>--%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div class="DivBorderOlive1" style="margin-bottom: 20px; font: bold; font-size: 17px">
                                                        <asp:Label ID="lbl1" runat="server" Text="Total= "></asp:Label>
                                                        <asp:Label ID="lblvalue" runat="server" Text="0.000"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtA6" Style="text-align: right" runat="server" Width="120px" Height="20px" Visible="false"></asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtA6"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Description]"></cc1:TextBoxWatermarkExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <%--<asp:LinkButton runat="server" ID="lnkatt_Viewk" AutoPostBack="true" Text="Attendance" Visible="false" OnClick="lnkatt_Viewk_click" CssClass="anchor__grd vw_grd" ToolTip="Click to complete attendance details"></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkatt_add" AutoPostBack="true" Text="Adv.Att" Visible="false" OnClick="lnkatt_add_click" CssClass="anchor__grd vw_grd" ToolTip="Click to complete advance attendance details"></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkALGross" AutoPostBack="true" Text="AL A/c" Visible="false" OnClick="lnkALGross_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Leave Details"></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkOT" AutoPostBack="true" Text="OT" Visible="false" OnClick="lnkOT_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Over Time Details"></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkAirTicket" AutoPostBack="true" Text="AirTicket" Visible="false" OnClick="lnkAirTicket_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Get AirTicket Details"></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkGratuity" AutoPostBack="true" Text="Gratuity" Visible="false" OnClick="lnkGratuity_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Get Gratuity Details"></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkLoans" AutoPostBack="true" Text="Loans" Visible="false" OnClick="lnkLoans_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Employee Advances"></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkAbsentPenalities" AutoPostBack="true" Text="Abs Pen" Visible="false" OnClick="lnkABS_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Absent Penalities Details"></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkEmpPen" AutoPostBack="true" Text="Emp Pen" Visible="false" OnClick="lnkEmpPen_click" CssClass="anchor__grd vw_grd" ToolTip="Click to Employee Penalities Details"></asp:LinkButton>
                                                        <asp:Button ID="btnCal" Style="text-align: right" AutoPostBack="true" Width="30px" Height="30px" runat="server" Text='Get' CssClass="btn btn-success" OnClick="btnCal_Click" Visible="false"></asp:Button>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Details" HeaderText="Details" />
                                        </Columns>
                                        <FooterStyle />
                                        <FooterStyle BackColor="#cccccc" ForeColor="Black" HorizontalAlign="left" />
                                    </asp:GridView>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
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
