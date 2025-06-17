<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="LTAConfig.aspx.cs" Inherits="AECLOGIC.ERP.HMS.LTAConfig" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function InsUpdLTAConfig(chkid, EmpID) {
            var targlist = "";
            var rbList = document.getElementById(chkid);
            var rbCount = rbList.getElementsByTagName("input");
            for (var i = 0; i < rbCount.length; i++) {
                if (rbCount[i].checked == true) {
                    targlist = rbCount[i].value;
                }
            }
            var CompID = document.getElementById('<%=ddlNCTCComponent.ClientID%>').options[document.getElementById('<%=ddlNCTCComponent.ClientID%>').selectedIndex].value;
            var Result = AjaxDAL.HR_InsUpdLTAConfig(targlist, EmpID, CompID);
            //alert('Done');
        }
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlworksites_hid.ClientID %>').value = HdnKey;
        }
        function GETDEPT_ID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddldepartments_hid.ClientID %>').value = HdnKey;
        }
        //chaitanya:for validation purpose
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td align="left" valign="top">
                        <table width="100%">
                            <tr>
                                <td colspan="2">
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
                                                    <table width="1000%">
                                                        <tr>
                                                            <strong><b>Worksite</b></strong>&nbsp;
                                                          <asp:HiddenField ID="ddlworksites_hid" runat="server" />
                                                            <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                            </cc1:AutoCompleteExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                            &nbsp;<strong><b>Department</b>&nbsp;
                                                        <asp:HiddenField ID="ddldepartments_hid" runat="server" />
                                                                <asp:TextBox ID="txtdepartment" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Department" ServicePath="" TargetControlID="txtdepartment"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETDEPT_ID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtdepartment"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                            </strong>&nbsp;
                                                        <strong><b>EmpId</b></strong>&nbsp;<asp:TextBox ID="txtEmpid" runat="server" Text="" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="true"
                                                                MinimumPrefixLength="1" ServiceMethod="GetEmpidList" ServicePath="" TargetControlID="txtEmpid" UseContextKey="true"
                                                                CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ShowOnlyCurrentWordInCompletionListItem="true"
                                                                FirstRowSelected="True">
                                                            </cc1:AutoCompleteExtender>
                                                            &nbsp; <strong>Non CTC Component : </strong>
                                                            <asp:DropDownList ID="ddlNCTCComponent" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNCTCComponent_SelectedIndexChanged">
                                                            </asp:DropDownList>&nbsp;
                                                        <asp:Button ID="btnSearch" ToolTip="Search selected worksite & Department peoples"
                                                            runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
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
                    <td style="text-align: left; vertical-align: top;" colspan="2">
                        <table width="80%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvAttend" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                        CellSpacing="1" DataKeyNames="EmpId" ForeColor="#333333" GridLines="None" EmptyDataText="No Records Found"
                                        EmptyDataRowStyle-CssClass="EmptyRowData" OnRowDataBound="gdvAttend_RowDataBound" CssClass="gridview">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="White" ForeColor="#333333" CssClass="gridview" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpID" runat="server" Text='<%#Bind("EmpId")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="worksite" HeaderText="WorkSite" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Department" HeaderText="Department" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rbtlstType" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Monthly"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Quarterly"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Half Yearly"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Annually"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:Label ID="lblChkvalue" runat="server" Text='<%#Bind("LTATYpe")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#D56511" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#999999" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 17px">
                        <uc1:Paging ID="EmpListPaging" runat="server" />
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
