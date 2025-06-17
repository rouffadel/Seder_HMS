<%@ Page Title="" Language="C#" AutoEventWireup="True" MasterPageFile="~/Templates/CommonMaster.master"
    CodeBehind="CategoryRateConfig.aspx.cs" Inherits="AECLOGIC.ERP.HMS.CategoryRateConfig" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function SearchModified() {
            $get('<%=hdnSearchChange.ClientID %>').value = "1";
        }
        function Validate() {
            //for Worksite
            if (!chkDropDownList('<%=ddlWS.ClientID%>', 'Worksite'))
                return false;
            //for Category
            if (!chkDropDownList('<%=ddlCategory.ClientID %>', 'Trade'))
                return false;
            //for Heads
            if (!chkDropDownList('<%=ddlDesignation.ClientID %>', 'Designation'))
                return false;
            //for Rate
            if (!chkFloatNumber('<%=txtRate.ClientID %>', 'Rate', true, ''))
                return false;
            if (!chkFloatNumber('<%=txtWHS.ClientID %>', 'Rate', true, ''))
                return false;
            return true;
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
                    elm.value = '';
                    elm.focus();
                    return false;
                }
            } return true;
        }
        function ShowValidate() {
            //for Worksite
            if (!chkDropDownList('<%=ddlWSSearch.ClientID%>', 'Worksite'))
                return false;
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
    </script>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td style="width: 662px">
                        <table>
                            <tr>
                                <td colspan="2" class="pageheader">Category rate configuration by site &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">Worksite<span style="color: red">*</span>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlWS" runat="server" CssClass="droplist" Width="120">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">Specialization<span style="color: red">*</span>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCategory" CssClass="droplist" runat="server" Width="120">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">Designation<span style="color: red">*</span>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDesignation" CssClass="droplist" runat="server" Width="120">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">Rate<span style="color: red">*</span>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRate" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">Working Hours<span style="color: red">*</span>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtWHS" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton btn btn-success" OnClientClick="javascript:return Validate()"
                                        Text="Save" Width="100px" OnClick="btnSubmit_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table>
                            <tr>
                                <td align="center" style="text-align: left" class="pageheader">Edit Category rate configuration by site
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td valign="top">Worksite<span style="color: red">*</span> &nbsp;<asp:DropDownList ID="ddlWSSearch" CssClass="droplist"
                                                runat="server" Width="100">
                                            </asp:DropDownList></td>
                                            &nbsp;&nbsp;&nbsp;
                                    <td valign="top">&nbsp;Categories <span style="color: red">*</span>
                                    </td>
                                            <td>&nbsp;&nbsp;
                                        <div style="max-height: 100px; overflow: auto">
                                            <asp:CheckBox ID="chkallcat" runat="server" AutoPostBack="true" OnCheckedChanged="sel_all_cat" Text="--All--" />
                                            <asp:CheckBoxList ID="ddlShowCategory" runat="server"></asp:CheckBoxList>
                                                </div>
                                            </td>
                                            <td valign="bottom">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="savebutton btn btn-primary" ToolTip="click to search..."
                                                Width="100px" OnClientClick="javascript:return ShowValidate()" OnClick="btnSearch_Click" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:GridView ID="gdvWS" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                                GridLines="Both" Width="100%" CellPadding="4" HeaderStyle-CssClass="tableHead"
                                EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="gdvWS_RowCommand">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSiteID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SiteID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategaryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CategoryID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesgID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"DesignationID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Worksite">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategary" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Site_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDept" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Category")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHeadID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Designation")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Rate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Working Hrs">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkingHrs" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WorkingHrs")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd" Text="Edit" CommandName="Edt"></asp:LinkButton>
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
                                <td>
                                    <uc1:paging id="CategoryConfigPaging" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hdnSearchChange" Value="0" runat="server" />
                                </td>
                            </tr>
                        </table>
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
