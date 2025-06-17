<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Contacts.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.Contacts" Title="" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function Validate() {
            if (document.getElementById('<%=txtOther.ClientID%>').value == "[New Organisation]") {
                alert("Enter Organisation!");
                return false;
            }
            if (document.getElementById('<%=txtNewGroup.ClientID%>').value == "[New Group]") {
                alert("Enter Group!");
                return false;
            }
        }
        function GETCName_ID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=hdCName_id.ClientID %>').value = HdnKey;
        }
        function Validatesave() {
            if (document.getElementById('<%=ddlCategory.ClientID%>').selectedIndex == 0) {
                alert("Select Organisation!");
                return false;
            }
            if (document.getElementById('<%=ddlContactGroup.ClientID%>').selectedIndex == 0) {
                alert("Select Contact Group!");
                return false;
            }
            //  First Name
            if (!chkName('<%=txtFName.ClientID %>', 'First Name', true, ''))
                return false;
            //  Middle Name
            if (!chkName('<%=txtMName.ClientID %>', 'Middle Name', false, ''))
                return false;
            //  Last Name
            if (!chkName('<%=txtLName.ClientID %>', 'Last Name', true, ''))
                return false;
            //  Designation
            if (!chkName('<%=txtDesig.ClientID %>', 'Designation', true, ''))
                return false;
            //Mobile1
            //Mobile2
            // EmailId
            if (!chkEmail('<%=txtEmail.ClientID %>', 'Email ID', false, '')) {
                return false;
            }
            // Website
            if (!chkWebSite('<%=txtSite.ClientID %>', 'Website', false, ''))
                return false;
            //IM Address
         <%-- if (!chkEmail('<%=txtIM.ClientID %>', 'Email', false, '')) {
                return false;
            }--%>
            //    fax
            if (!chkNumber('<%=txtFax.ClientID %>', 'Fax', false, ''))
                return false;
        }
        //geting the object
        function getObj(the_id) {
            if (typeof (the_id) == "object") {
                return the_id;
            }
            if (typeof document.getElementById != 'undefined') {
                return document.getElementById(the_id);
            }
            else if (typeof document.all != 'undefined') {
                return document.all[the_id];
            }
            else if (typeof document.layers != 'undefined') {
                return document.layers[the_id];
            }
            else {
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
        //for checking org
        function chkorg(object) {
            var elm = getObj(object);
            var val = elm.value;
            if (val == '' || val.length == 0) {
                return false;
            }
            return true;
        }
        function Trim(str) {
            return str.replace(/^\s*|\s*$/g, "");
        }
    </script>
    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <AEC:Topmenu ID="topmenu" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:MultiView ID="mainview" runat="server">
                            <asp:View ID="Newvieew" runat="server">
                                <asp:Panel ID="pnlContacts" runat="server" CssClass="box box-primary" Width="60%">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 94px">
                                                <span>Organisation <span style="color: #ff0000">*</span></span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCategory" CssClass="droplist" ToolTip="Choose Organisation/Contact Type"
                                                    runat="server" Width="250px" TabIndex="1">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="ListSearchExtender1" IsSorted="true" PromptText="Type Here To Search"
                                                    PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                                    TargetControlID="ddlCategory" />
                                                &nbsp;
                                                <asp:LinkButton ID="lnkAdd" runat="server" OnClick="lnkAdd_Click" CssClass="btn btn-primary">Add New</asp:LinkButton>
                                                &nbsp;
                                                <asp:TextBox ID="txtOther" runat="server" Visible="False" Width="186px"></asp:TextBox><cc1:TextBoxWatermarkExtender
                                                    ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtOther" WatermarkCssClass="Watermarktxtbox"
                                                    WatermarkText="[New Organisation]"></cc1:TextBoxWatermarkExtender>
                                                <asp:Button ID="btnaddnew" runat="server" Visible="false" CssClass="btn btn-primary" OnClientClick="javascript:return Validate();"
                                                    Text="Submit" OnClick="btnaddnew_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 94px">
                                                <span>Contact Group <span style="color: #ff0000">*</span></span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlContactGroup" CssClass="droplist" runat="server" TabIndex="2">
                                                    <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search"
                                                    PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                                    TargetControlID="ddlContactGroup" />
                                                <asp:LinkButton ID="lnkNewGroup" runat="server" OnClick="lnkNewGroup_Click" CssClass="btn btn-primary">Add New</asp:LinkButton>
                                                &nbsp;
                                                <asp:TextBox ID="txtNewGroup" Width="186px" Visible="False" runat="server"></asp:TextBox>
                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtNewGroup"
                                                    WatermarkCssClass="Watermarktxtbox" WatermarkText="[New Group]"></cc1:TextBoxWatermarkExtender>
                                                <asp:Button ID="btnaddGroup" CssClass="btn btn-primary" Visible="false" runat="server"
                                                    OnClientClick="javascript:return Validate();" Text="Submit" OnClick="btnaddGroup_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 600Px">
                                                <table width="80%">
                                                    <tr>
                                                        <td style="width: 91px">
                                                            <span>First Name <span style="color: #ff0000">*</span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFName" runat="server" Width="187px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 91px">
                                                            <span>Middle Name </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtMName" runat="server" Width="187px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 91px">
                                                            <span>Last Name <span style="color: #ff0000">*</span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLName" runat="server" Width="187px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 91px">
                                                            <span>Designation <span style="color: #ff0000">*</span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDesig" runat="server" Width="187px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 91px">
                                                            <span>Mobile1 <span style="color: #ff0000">*</span></span>
                                                        </td>
                                                        <td>
                                                            <asp:UpdatePanel ID="updPanel" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="txtphone1" AutoPostBack="true" runat="server" Width="189px" MaxLength="15"></asp:TextBox>
                                                                    <%--    <cc1:FilteredTextBoxExtender ID="fteSimno" TargetControlID="txtphone1" runat="server"
                                                            FilterType="Numbers" ValidChars="-">
                                                        </cc1:FilteredTextBoxExtender>--%>
                                                                    <cc1:FilteredTextBoxExtender FilterMode="ValidChars" FilterType="Numbers,Custom"
                                                                        ID="Fl4" runat="server" TargetControlID="txtphone1"></cc1:FilteredTextBoxExtender>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 91px">Phone
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtphone2" AutoPostBack="true" runat="server" Width="189px" MaxLength="15"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtphone2"
                                                                runat="server" FilterType="Numbers" ValidChars="-"></cc1:FilteredTextBoxExtender>
                                                            Ext : &nbsp;
                                                            <asp:TextBox ID="txtphExt" runat="server" Width="10%">
                                                            </asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtphExt"
                                                                runat="server" FilterType="Numbers" ValidChars="-"></cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 91px">E-MailID
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmail" runat="server" Width="188px"></asp:TextBox><cc1:TextBoxWatermarkExtender
                                                                ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtEmail" WatermarkCssClass="Watermarktxtbox"
                                                                WatermarkText="[Enter E-MailID Here]"></cc1:TextBoxWatermarkExtender>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Mail ID!"
                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 91px">Web Site
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSite" Width="188px" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 91px">IM Address
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtIM" Width="188px" runat="server"></asp:TextBox>* Ex:- Skype
                                                            ID
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 91px">Fax
                                                        </td>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtFax"
                                                            runat="server" FilterType="Numbers" ValidChars="-"></cc1:FilteredTextBoxExtender>
                                                        <td>
                                                            <asp:TextBox ID="txtFax" Width="188px" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 91px">Image
                                                        </td>
                                                        <td>
                                                            <asp:FileUpload ID="flImage" runat="server" Height="18px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" style="width: 91px">
                                                            <br />
                                                            <br />
                                                            Address
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAddress" runat="server" TextMode="multiline" Height="86px" Width="318px"
                                                                BorderColor="#CC6600" BorderStyle="Outset"></asp:TextBox>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtAddress"
                                                                WatermarkCssClass="Watermark" WatermarkText="[Enter Address Here!]"></cc1:TextBoxWatermarkExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" style="width: 91px">
                                                            <br />
                                                            <br />
                                                            Notes
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNotes" runat="server" TextMode="multiline" Height="86px" Width="317px"
                                                                BorderColor="#CC6600" BorderStyle="Outset"></asp:TextBox>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtNotes"
                                                                WatermarkCssClass="Watermark" WatermarkText="[Enter Note Here!]"></cc1:TextBoxWatermarkExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding-left: 200Px">
                                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="javascript:return Validatesave();"
                                                                CssClass="btn btn-primary" OnClick="btnSave_Click" AccessKey="s" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                                            &nbsp;
                                                            <asp:Button ID="btncancel" runat="server" Text="Cancel" AccessKey="b" ToolTip="[Alt+b OR Alt+b+Enter]"
                                                                CssClass="btn btn-danger" OnClick="btncancel_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td rowspan="5" style="text-align: left; vertical-align: top;">
                                                <asp:Image ID="imgemp" runat="server" Width="50%" Height="30%" BorderColor="#CC6600"
                                                    BorderStyle="Inset" BorderWidth="1px" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <table width="100%">
                                </table>
                            </asp:View>
                            <asp:View ID="EditView" runat="server">
                                <%--<asp:UpdatePanel ID="Updatepanle2" runat="server">
                <ContentTemplate>--%>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                    <tr>
                                        <td class="pageheader">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                                ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                                FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                                <Panes>
                                                    <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                        ContentCssClass="accordionContent">
                                                        <Header>
                                                            Search Criteria</Header>
                                                        <Content>
                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td>Organisation:&nbsp;<asp:DropDownList ID="ddlTrade" runat="server" Width="200px" CssClass="droplist"
                                                                        OnSelectedIndexChanged="ddlTrade_SelectedIndexChanged" TabIndex="1" AccessKey="1"
                                                                        ToolTip="[Alt+1]">
                                                                    </asp:DropDownList>
                                                                        <cc1:ListSearchExtender ID="ListSearchExtender3" IsSorted="true" PromptText="Type Here To Search"
                                                                            PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                                                            TargetControlID="ddlTrade" />
                                                                        &nbsp; Contact Group:
                                                                        <asp:DropDownList ID="ddlContGroup" runat="server" TabIndex="2" AccessKey="2" ToolTip="[Alt+2]"
                                                                            CssClass="droplist" OnSelectedIndexChanged="ddlContGroup_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <cc1:ListSearchExtender ID="ListSearchExtender4" IsSorted="true" PromptText="Type Here To Search"
                                                                            PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                                                            TargetControlID="ddlContGroup" />
                                                                        &nbsp; Contact Name:&nbsp;<asp:TextBox ID="txtRefName" runat="server" Width="140px"
                                                                            TabIndex="3" AccessKey="3" ToolTip="[Alt+3]" AutoPostBack="true"></asp:TextBox>
                                                                        <asp:HiddenField ID="hdCName_id" runat="server" />
                                                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22"
                                                                            runat="server" TargetControlID="txtRefName" WatermarkCssClass="Watermarktxtbox"
                                                                            WatermarkText="[Enter ContactName]"></cc1:TextBoxWatermarkExtender>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender21" runat="server" DelimiterCharacters="" Enabled="True"
                                                                            MinimumPrefixLength="1" ServiceMethod="GetConName" ServicePath="" TargetControlID="txtRefName"
                                                                            UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETCName_ID">
                                                                        </cc1:AutoCompleteExtender>
                                                                        <asp:Button ID="btnFind" runat="server" Text="Search" CssClass="btn btn-primary" Width="100px"
                                                                            OnClick="btnFind_Click" TabIndex="4" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" />
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
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:GridView ID="gvContacts" Width="100%" runat="server" CssClass="gridview" AutoGenerateColumns="False"
                                                CellPadding="4" ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found"
                                                EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="gvContacts_RowCommand"
                                                HeaderStyle-CssClass="tableHead" OnRowDataBound="gvContacts_RowDataBound">
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Org" DataField="Category" />
                                                    <asp:BoundField HeaderText="ContactName" DataField="Name" />
                                                    <asp:TemplateField HeaderText="Image">
                                                        <ItemTemplate>
                                                            <asp:Image ID="Image1" Width="25" Height="29" Visible='<%#ImgVisible(Eval("ext").ToString()) %>'
                                                                onmouseover='<%#ViewImg(Eval("ContactID").ToString(),Eval("ext").ToString()) %>'
                                                                ImageUrl='<%#BindImg(Eval("ContactID").ToString(),Eval("ext").ToString()) %>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Group" DataField="GroupName" />
                                                    <asp:TemplateField HeaderText="Phone">
                                                        <ItemTemplate>
                                                            <%#Eval("Phone1")%><br></br>
                                                            <%#Eval("Phone2")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:BoundField HeaderText="Contact1" DataField="Phone1" />
                                            <asp:BoundField HeaderText="Contact2" DataField="Phone2" />--%>
                                                    <asp:BoundField HeaderText="Address" DataField="Address" />
                                                    <asp:TemplateField HeaderText="E-MailID">
                                                        <ItemTemplate>
                                                            <a href='<%#Email(Eval("EMailID").ToString())%>'>
                                                                <%#Eval("EMailID") %></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="10Px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/IMAGES/web.bmp" OnClick='<%#Web(Eval("web").ToString()) %>'
                                                                ToolTip='<%#Eval("web")%>' Visible='<%#WebVisible(Eval("web").ToString())%>'
                                                                runat="server" />
                                                            <%--  <asp:LinkButton ID="LinkButton1"   runat="server">web</asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkNotes" Visible='<%#NotesVisible(Eval("Notes").ToString())%>'
                                                                ToolTip='<%#Eval("Notes")%>' runat="server" CssClass="btn btn-primary">Notes</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkedt" runat="server" CommandArgument='<%#Eval("ContactID") %>'
                                                                CommandName="Edt" Text="Edit" CssClass="anchor__grd edit_grd "></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkdel" runat="server" OnClientClick="return confirm('Are U Sure?');"
                                                                CommandArgument='<%#Eval("ContactID") %>' CommandName="Del" Text="Delete" CssClass="anchor__grd dlt"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
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
                                            <asp:HiddenField ID="hdnSearchChange" Value="0" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="updatepanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
