<%@ Page Title="" Language="C#"  AutoEventWireup="True"
    CodeBehind="EmpRentalDocs.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpRentalDocs" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function UpdateApproval(lblHRDocID) {
            var UserId = '<%= Convert.ToInt32(Session["UserId"]).ToString()%>';
            var ResultVal = AjaxDAL.HR_SetUpApproval(lblHRDocID, UserId);
            if (ResultVal.erorr == null && ResultVal.value != null) {
                return true;
            }
        }
        function validsEmpRental() {


            //For employee
            if (document.getElementById('<%=ddlEmp.ClientID%>').selectedIndex == 0) {
                alert("Please select employee.!");
                document.getElementById('<%=ddlEmp.ClientID%>').focus();
                return false;
            }

            //For amount
            if (!Reval('<%=txtAmount.ClientID%>', "amount", true, "")) {
                return false;
            }

            //txtFrom

            if (!Reval('<%=txtFrom.ClientID%>', "From date", true, "")) {
                return false;
            }

        }


    </script>
   
    <asp:Panel ID="pnltblEdit" runat="server" CssClass="DivBorderOlive" Visible="false"
        Width="50%">
        <table id="tblEdit" runat="server" visible="false" width="100%">
            <tr id="trEmpDisp" runat="server">
                <td style="width: 170">
                    <b>
                        <asp:Label ID="EmpRenEmp" runat="server" Text="Employee:"></asp:Label>
                        <span style="color: #ff0000">*</span></b>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEmp" CssClass="droplist" runat="server" TabIndex="1">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 170">
                    <b>
                        <asp:Label ID="lblEmpRenAmt" runat="server" Text="Amount:"></asp:Label>
                        <span style="color: #ff0000">*</span> </b>
                </td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" TabIndex="2"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="fteoutQty" runat="server" TargetControlID="txtAmount"
                        ValidChars="." FilterType="Numbers,custom" FilterMode="ValidChars">
                    </cc1:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 170">
                    <b>Proof:</b>
                </td>
                <td>
                    <asp:FileUpload ID="FileProof" CssClass="savebutton" runat="server" TabIndex="3" />
                </td>
            </tr>
            <tr>
                <td style="width: 170">
                    <b>
                        <asp:Label ID="lblEmpFrm" runat="server" Text="From:"></asp:Label>
                        <span style="color: #ff0000">*</span> </b>
                </td>
                <td>
                    <asp:TextBox ID="txtFrom" runat="server" TabIndex="4"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFrom"
                        PopupButtonID="txtFrom" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:FilteredTextBoxExtender FilterMode="ValidChars" FilterType="Numbers,Custom"
                        ID="Fl7" runat="server" TargetControlID="txtFrom" ValidChars="/">
                    </cc1:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 170">
                    <b>Upto:</b>
                </td>
                <td>
                    <asp:TextBox ID="txtUpto" runat="server" TabIndex="5"></asp:TextBox><cc1:CalendarExtender
                        ID="CalendarExtender1" runat="server" TargetControlID="txtUpto" PopupButtonID="txtUpto"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 170">
                    <b></b>
                </td>
                <td>
                    <asp:Button ID="btnSave" CssClass="savebutton" runat="server" Text="Save" OnClick="btnSave_Click"
                        OnClientClick="javascript:return validsEmpRental();" AccessKey="s" TabIndex="6"
                        ToolTip="[Alt+s OR Alt+s+Enter]" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" CssClass="savebutton" runat="server" Text="Reset" OnClick="btnCancel_Click"
                        AccessKey="b" TabIndex="7" ToolTip="[Alt+b OR Alt+b+Enter]" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table id="tblView" runat="server" visible="false" width="100%">
        <tr>
            <td>
                <cc1:Accordion ID="EmpRntDocAccordion" runat="server" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    <Panes>
                        <cc1:AccordionPane ID="EmpRntDocAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr id="trEmpSearch" runat="server" visible="false">
                                        <td colspan="2">
                                            <b>WorkSite:</b><asp:DropDownList visible="false" ID="ddlWs" TabIndex="8" AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]"
                                                CssClass="droplist" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWs_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtSearchWorksite" OnTextChanged="Worksidechangewithdep" Height="22px" Width="140px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                            MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                            WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                                            </cc1:TextBoxWatermarkExtender>
                                            <b>&nbsp;&nbsp;&nbsp; Department:</b><asp:DropDownList ID="ddlDept" visible="false" CssClass="droplist"
                                                TabIndex="9" AccessKey="1" ToolTip="Alt+1" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtSearchdept" OnTextChanged="GetDepartment" Height="22px" Width="140px" runat="server" AutoPostBack="True"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListdept" ServicePath="" TargetControlID="txtSearchdept"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchdept"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                                                </cc1:TextBoxWatermarkExtender>

                                            <b>&nbsp;&nbsp;&nbsp;&nbsp; EmpName:</b><asp:TextBox ID="txtEmpName" TabIndex="10"
                                                AccessKey="2" ToolTip="[Alt+2]" runat="server"></asp:TextBox><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    EmpID:</b><asp:TextBox ID="txtEmpID" runat="server" TabIndex="11" ToolTip="[Alt+3]"
                                                        AccessKey="3"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FTBEmpID" runat="server" TargetControlID="txtEmpID" FilterType="Numbers"></cc1:FilteredTextBoxExtender>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="savebutton" OnClick="btnSearch_Click"
                                                TabIndex="12" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />
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
            <td colspan="2">
                <asp:GridView ID="gvView" Width="100%" CssClass="gridview" AutoGenerateColumns="false"
                    runat="server" OnRowCommand="gvView_RowCommand" OnRowDataBound="gvView_RowDataBound">
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDocID" runat="server" Text='<%#Eval("HRDocID")%>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="ID" DataField="EmpID" />
                        <asp:BoundField HeaderText="Name" DataField="Name" />
                        <asp:BoundField HeaderText="Disignation" DataField="Designation" />
                        <asp:BoundField HeaderText="Department" DataField="DepartmentName" />
                        <asp:BoundField HeaderText="Site" DataField="Site_Name" />
                        <asp:BoundField HeaderText="Amount" DataField="Amount" />
                        <asp:BoundField HeaderText="From" DataField="FromDate1" />
                        <asp:BoundField HeaderText="Upto" DataField="ToDate1" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                            HeaderText="Approve">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkApprove" Checked='<%#Eval("IsApproved")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hlnkproof" Enabled='<%#Visible(DataBinder.Eval(Container.DataItem, "Proof").ToString())%>'
                                    NavigateUrl="#" OnClick='<%#ProofView(DataBinder.Eval(Container.DataItem, "EmpID").ToString(),DataBinder.Eval(Container.DataItem, "Proof").ToString())%>'
                                    runat="server">Proof</asp:HyperLink></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" CommandName="Edt" CommandArgument='<%#Eval("HRDocID")%>'
                                    runat="server">Edit</asp:LinkButton></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 17px">
                <uc1:Paging ID="EmpRentalDocsPaging" runat="server" />
            </td>
        </tr>
    </table>
  
</asp:Content>
