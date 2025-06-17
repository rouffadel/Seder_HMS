<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Clearence.aspx.cs" Inherits="AECLOGIC.ERP.HMS.Clearence" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //   alert(HdnKey);
            document.getElementById('<%=ddlWorksite_hid.ClientID %>').value = HdnKey;
        }
        function GetDeptID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //   alert(HdnKey);
            document.getElementById('<%=TxtDept_hid.ClientID %>').value = HdnKey;
        }
        function GetWorkID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=Txtwrk_hid.ClientID %>').value = HdnKey;
           }
           function showApp(EmpID) {
               // window.showModalDialog("../HMS/Checkcustodi.aspx?Empid=" + EmpID, "", "dialogheight:500px;dialogwidth:500px;status:no;edge:sunken;unadorned:no;resizable:no;");
               window.showModalDialog("../HMS/Checkcustodi.aspx?Empid=" + EmpID, "", "dialogheight:500px;dialogwidth:500px;status:no;edge:sunken;unadorned:no;resizable:no;");
           }
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <table style="width: 100%; height: 100%">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="EdtViewAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%">
                                        <Panes>
                                            <cc1:AccordionPane ID="EdtViewAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td>Worksite 
                                                            </td>
                                                            <td>
                                                                <asp:HiddenField ID="Txtwrk_hid" runat="server" />
                                                                <asp:TextBox ID="Txtwrk" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletiondeptWorkList" ServicePath="" TargetControlID="Txtwrk"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetWorkID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="Txtwrk"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                                            </td>
                                                            <td>Department
                                                            </td>
                                                            <td>
                                                                <asp:HiddenField ID="TxtDept_hid" runat="server" />
                                                                <asp:TextBox ID="TxtDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletiondeptList" ServicePath="" TargetControlID="TxtDept"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetDeptID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="TxtDept"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]"></cc1:TextBoxWatermarkExtender>
                                                            </td>
                                                            <td>Employee
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSItemname" runat="server" Visible="false"></asp:TextBox>
                                                                <asp:HiddenField ID="ddlWorksite_hid" runat="server" />
                                                                <asp:TextBox ID="txtSearchemp" AutoPostBack="false" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchemp"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                                                </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchemp"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Employeee Name]"></cc1:TextBoxWatermarkExtender>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" Width="53" />
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
                                    <asp:GridView ID="gdvWS" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCommand="gdvWS_RowCommand"
                                        HeaderStyle-CssClass="tableHead" AllowSorting="True"
                                        CssClass="gridview" EmptyDataText="No Records Found"
                                        EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BorderColor="GhostWhite">
                                        <Columns>
                                            <asp:TemplateField HeaderText="EmpID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblem" runat="server" Text='<%#Bind("EmpID")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Worksite" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwork" runat="server" Text='<%#Bind("site_name")%>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="site_name" HeaderText="Worksite" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="departmentname" HeaderText="Department" ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="EMPLOYEE" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Bind("EmpID")%>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EMPLOYEE">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Bind("EmpName")%>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkIssue" runat="server" CommandArgument='<% #Eval("EmpID")%>'
                                                        CommandName="Receive" Text="Clearance" CssClass="btn btn-primary"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkcst" runat="server" Visible="false" CommandArgument='<% #Eval("EmpID")%>'
                                                        CommandName="incostod" Text="In Custody" CssClass="btn btn-primary"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 17px">
                                    <uc1:Paging ID="EmpListPaging" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <asp:HiddenField ID="hdn" runat="server" />
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
