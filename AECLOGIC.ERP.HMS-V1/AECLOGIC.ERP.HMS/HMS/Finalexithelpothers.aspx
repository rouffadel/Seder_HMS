<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Finalexithelpothers.aspx.cs" Inherits="AECLOGIC.ERP.HMSV1.FinalexithelpothersV1" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
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

        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=empid_hd.ClientID %>').value = HdnKey;
        }
        function validateCheckBox() {
            var isValid = false;
            var gvChk = document.getElementById('<%= gdvAttend.ClientID %>');
            for (var i = 1; i < gvChk.rows.length; i++) {
                var chkInput = gvChk.rows[i].getElementsByTagName('input');
                if (chkInput != null) {
                    if (chkInput[0].type == "checkbox") {
                        if (chkInput[0].checked) {
                            isValid = true;
                            return true;
                        }

                    }
                }
            }
            if (isValid == false) {
                alert("Please select atleast one Employee.");
                return false;
            }
        }
    </script>

    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>


            <table id="tblProcess" runat="server" width="100%" height="80%">
                <tr>
                    <td>
                        <cc1:Accordion ID="gvViewAccordion" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                            AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                            RequireOpenedPane="false" SuppressHeaderPostbacks="true" Height="106px" Width="50%">
                            <Panes>
                                <cc1:AccordionPane ID="gvViewAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                    ContentCssClass="accordionContent">
                                    <Header>
                                Search Criteria</Header>
                                    <Content>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <b>Employee Name:</b>
                                                    <asp:HiddenField ID="empid_hd" runat="server" />
                                                    <asp:TextBox ID="txtempid" Width="150" runat="server"></asp:TextBox>

                                                    <cc1:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="true"
                                                        MinimumPrefixLength="1" ServiceMethod="GetEmpidList" ServicePath="" TargetControlID="txtEmpid" UseContextKey="true" OnClientItemSelected="GetID"
                                                        CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ShowOnlyCurrentWordInCompletionListItem="true"
                                                        FirstRowSelected="True">
                                                    </cc1:AutoCompleteExtender>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtempid"
                                                        WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name]"></cc1:TextBoxWatermarkExtender>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </Content>
                                </cc1:AccordionPane>
                            </Panes>
                        </cc1:Accordion>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gdvAttend" runat="server" AutoGenerateColumns="False"
                            EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                            CssClass="gridview" Width="50%" OnRowCommand="gdvAttend_RowCommand" OnRowDataBound="ddlEmpDeAct_RowDataBound">
                            <Columns>

                                <asp:TemplateField>
                                    <ItemStyle />
                                    <HeaderStyle />
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" onclick="SelectAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="15px" />
                                    <ItemStyle Width="15px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpID" runat="server" Text='<%#Bind("EmpId")%>' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="EmpName" HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle Width="150px" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Exit From">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtExitFrom" runat="server" Width="70px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtExitFrom" CssClass="MyCalendar"
                                            TargetControlID="txtExitFrom" Format="dd MMM yyyy"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />

                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Exit Type" HeaderStyle-HorizontalAlign="Left">
                              <ItemStyle Width="130" />
                              <HeaderStyle Width="130" />
                              <ItemTemplate>
                                  <asp:DropDownList Width="130" ID="ddlEmpDeAct" CssClass="droplist" runat="server" DataTextField="Name"
                                      AutoPostBack="true" DataValueField="Id" >
                                  </asp:DropDownList>
                              </ItemTemplate>
                          </asp:TemplateField>

                                <asp:TemplateField HeaderText="Reason" ItemStyle-Width="300px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtReason" runat="server" Width="300px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Upload Proof" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:FileUpload ID="fuUploadProof" runat="server"></asp:FileUpload>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkUpload" runat="server" Text="Save" CommandName="Upld" CommandArgument='<%#Eval("EmpId")%>'
                                            CssClass="anchor__grd edit_grd"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                    </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                        <uc1:Paging ID="EmpReimbursementAprovedPaging" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnsave" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnsave_Click"
                            OnClientClick="javascript:return validateCheckBox()" ToolTip="[Alt+s OR Alt+s+Enter]" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnsave" />
            <asp:PostBackTrigger ControlID="gdvAttend" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

