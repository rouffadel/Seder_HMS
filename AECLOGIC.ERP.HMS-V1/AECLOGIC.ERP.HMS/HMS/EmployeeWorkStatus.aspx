<%@ Page Title="" Language="C#"  AutoEventWireup="True"
    CodeBehind="EmployeeWorkStatus.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmployeeWorkStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function showApp(EmpID) {
            window.showModalDialog("../HMS/empdocuments.aspx?Eid=" + EmpID, "", "dialogheight:900px;dialogwidth:900px;status:no;edge:sunken;unadorned:no;resizable:yes;");
        }

        //Check Number
        function chkNumber(object, msg, isRequired, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (isRequired) {
                if (!Reval(object, msg, waterMark))
                { return false; }
            }
            if (val != '') {
                var rx = new RegExp("[0-9]*");
                var matches = rx.exec(val);
                if (matches == null || val != matches[0]) {
                    alert(msg + " can take numbers only!!!");
                    //elm.value='';
                    elm.focus();
                    return false;
                }
            } return true;
        }

        //For Dropdown list
        function chkDropDownList(object, msg) {

            var elm = getObj(object);

            if (elm.selectedIndex == 0) {
                alert("Select " + msg + "!!!");
                elm.focus();
                return false;
            } return true;
        }

        function getObj(the_id) {
            if (typeof (the_id) == "object") {
                return the_id;
            }
            if (typeof document.getElementById != 'undefined') {
                return document.getElementById(the_id);
            } else if (typeof document.all != 'undefined') {
                return document.all[the_id];
            } else if (typeof document.layers != 'undefined') {
                return document.layers[the_id];
            } else {
                return null;
            }
        }
        //Required Validation
        function Reval(object, msg, waterMark) {
            var elm = getObj(object);
            var val = elm.value;
            if (val == '' || val.length == 0 || val == waterMark) {
                alert(msg + " should not be empty!!! ");
                //elm.value = waterMark;
                elm.focus();
                return false;
            }
            return true;
        }

        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
         //   alert(HdnKey);
            document.getElementById('<%=empname_hid.ClientID %>').value = HdnKey;
        }
    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td align="right">
                <asp:HyperLink ID="HyperLink1" NavigateUrl="http://humanresources.about.com/od/managementandleadership/u/manage_people.htm#s8"
                    runat="server" ForeColor="Blue"><u> Employee Management in the Workplace</u></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td>
                            <cc1:Accordion ID="SimAlloListAccordion" runat="server" HeaderCssClass="accordionHeader"
                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                RequireOpenedPane="false" SuppressHeaderPostbacks="true" Height="158px">
                                <Panes>
                                    <cc1:AccordionPane ID="SimAlloListAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                        ContentCssClass="accordionContent">
                                        <Header>
                                            Search Criteria</Header>
                                        <Content>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td colspan="2">
                                                        Worksite:
                                                        <asp:DropDownList ID="ddlworksites" CssClass="droplist" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlworksites_SelectedIndexChanged" AccessKey="w"
                                                            TabIndex="1" ToolTip="[Alt+w OR Alt+w+Enter]">
                                                        </asp:DropDownList>
                                                        &nbsp;&nbsp;Department:
                                                        <asp:DropDownList ID="ddldepartments" CssClass="droplist" runat="server" AccessKey="1"
                                                            TabIndex="2" ToolTip="[Alt+1]">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;&nbsp;Filter Employee:
                                                          <asp:HiddenField ID="empname_hid" runat="server" />
                                                        <asp:TextBox ID="txtusername" runat="server" MaxLength="50" OnTextChanged="btnSearch_Click"
                                                            AccessKey="2" TabIndex="3" ToolTip="[Alt+2]"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtusername"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                                        <cc1:TextBoxWatermarkExtender ID="txtWMETexName" runat="server" TargetControlID="txtusername"
                                                            WatermarkText="[Filter EmpName]">
                                                        </cc1:TextBoxWatermarkExtender>
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                            CssClass="btn btn-primary" Width="80px" AccessKey="i" TabIndex="4" ToolTip="[Alt+i OR Alt+i+Enter]" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lblDate" runat="server" CssClass="pageheader"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;<asp:RadioButton ID="rbActive" runat="server" AutoPostBack="True" Visible="false"
                                                            Checked="True" GroupName="Emp" Text="Active EmployeeList" OnCheckedChanged="rbActive_CheckedChanged"
                                                            TabIndex="5" />
                                                        <asp:RadioButton ID="rbInActive" runat="server" Visible="false" AutoPostBack="True"
                                                            GroupName="Emp" Text="Inactive EmployeeList" OnCheckedChanged="rbInActive_CheckedChanged"
                                                            TabIndex="6" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
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
                            <asp:GridView ID="gveditkbipl" runat="server" AutoGenerateColumns="False" DataKeyNames="EmpId"
                                  HeaderStyle-CssClass="tableHead"
                                 EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                CssClass="gridview" Width="100%">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="EmpID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpId" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" Text='<%#String.Format("{0} {1} {2}{3}", DataBinder.Eval(Container.DataItem, "EmpId"),DataBinder.Eval(Container.DataItem, "FName"), DataBinder.Eval(Container.DataItem, "MName"), DataBinder.Eval(Container.DataItem, "LName")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Design" HeaderText="Designation" HeaderStyle-HorizontalAlign="Left">
                                    </asp:BoundField>
                                   
                                    <asp:TemplateField HeaderText="Worksite" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorksite" runat="server" Text='<%# GetWorkSite(DataBinder.Eval(Container.DataItem, "Categary").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" runat="server" Text='<%# GetDepartment(DataBinder.Eval(Container.DataItem, "DeptNo").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Mobile1" HeaderText="Personal Mobile" HeaderStyle-HorizontalAlign="Left">
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Date" HeaderStyle-Width="6%" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Working Task" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkTask" runat="server" Text='<%#Bind("Task") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                </Columns>
                                <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 17px">
                            <uc1:Paging ID="EmpListPaging" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
