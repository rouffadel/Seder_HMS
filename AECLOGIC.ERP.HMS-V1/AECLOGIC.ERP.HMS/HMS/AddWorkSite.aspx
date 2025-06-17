<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="AddWorkSite.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.CMS_AddWorkSite" Title="ADD Worksite" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function ShowValidate() {


            //for Worksite
            if (!chkDropDownList('<%=ddlWSSearch.ClientID%>', 'Worksite'))
                return false;

        }
        function Validate() {


            //for Worksite
            if (!chkDropDownList('<%=ddlWS.ClientID%>', 'Worksite'))
                return false;
            //for Manager
            if (!chkDropDownList('<%=ddlManager.ClientID %>', 'Manager'))
                return false;




            return true;

        }
        function ValidAddnew() {
            //for AddNew
            if (!chkDropDownList('<%=ddlNewEmp.ClientID%>', 'Site Head'))
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
 <asp:updatepanel runat="server" ID="UpdatePanel1">
  <ContentTemplate>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td>
                <asp:MultiView ID="mainview" runat="server">
                    <asp:View ID="Newvieew" runat="server">
                        <table width="100%">
                            <tr>
                                <td colspan="2" class="pageheader">
                                    Assign Worksite Head
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 113px">
                                    Worksite<span style="color: red">*</span>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlWS" visible="false" runat="server" CssClass="droplist" Width="200" TabIndex="1">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWorkForSearch" AutoPostBack="true" Height="22px" Width="180px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                            </cc1:TextBoxWatermarkExtender>

                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 113px">
                                    Site Head<span style="color: #ff0000">*</span>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlManager" CssClass="droplist" runat="server" Width="200"
                                        TabIndex="2">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtSearchEmp" OnTextChanged="GetEmployeeSearch" Height="22px" Width="180px" runat="server" AutoPostBack="True" Visible="false"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtSearchEmp"
                                                                    UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                    </cc1:AutoCompleteExtender>
                                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtSearchEmp"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name]">
                                                                    </cc1:TextBoxWatermarkExtender>

                                    &nbsp;<asp:LinkButton ID="lnkNew" Font-Bold="true" CssClass="btn btn-primary" runat="server" OnClick="lnkNew_Click" Visible="false"
                                        TabIndex="3">Add New</asp:LinkButton>
                                    &nbsp;<asp:DropDownList ID="ddlNewEmp" CssClass="droplist" runat="server" TabIndex="4">
                                    </asp:DropDownList>
                                    <cc1:ListSearchExtender ID="ListSearchExtender1" IsSorted="true" PromptText="Type Here To Search..."
                                        PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                        TargetControlID="ddlNewEmp" />
                                    &nbsp;
                                    <asp:Button ID="btnAddEmp"  CssClass="btn btn-primary" runat="server" Text="Add" OnClick="btnAddEmp_Click"
                                        OnClientClick="javascript:return ValidAddnew()" TabIndex="5" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 160Px" colspan="2">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Assign"  CssClass="btn btn-success"  OnClick="btnSubmit_Click"
                                       Width="100px" OnClientClick="javascript:return Validate()"
                                        AccessKey="s" TabIndex="6" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="100%">
                            <tr>
                                <td class="pageheader">
                                    Edit Worksite Head
                                </td>
                            </tr>
                           <td>
                                    Worksite<span style="color: red">*</span> &nbsp;<asp:DropDownList ID="ddlWSSearch"
                                        runat="server" Width="100" CssClass="droplist" 
                                AccessKey="w" TabIndex="5"   ToolTip="[Alt+w OR Alt+w+Enter]" ></asp:DropDownList>
                                <asp:Button ID="btnSearch" runat="server" Text="Show" CssClass="btn btn-primary" Width="100px"
                                    OnClientClick="javascript:return ShowValidate()" OnClick="btnSearch_Click"  AccessKey="i" 
                                    TabIndex="6" ToolTip="[Alt+i OR Alt+i+Enter]" />
                                       
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvWS" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                                        GridLines="Both" Width="70%" CellPadding="4" OnRowCommand="gdvWS_RowCommand"
                                        CssClass="gridview" OnRowDataBound="gdvWS_RowDataBound" HeaderStyle-CssClass="tableHead"
                                        AllowSorting="true" OnSorting="gdvWS_Sorting" EmptyDataText="No Records Found"
                                        EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BackColor="GhostWhite">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrjID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"RowNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwsid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"prjid")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMgnrId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MgnrId")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                           
                                            <asp:BoundField DataField="Site_Name" HeaderStyle-ForeColor="Lime" HeaderText="WorkSite" SortExpression="Name">
                                                <HeaderStyle Width="100" />
                                            </asp:BoundField>
                                             <asp:TemplateField HeaderStyle-ForeColor="Lime" HeaderText="Head" SortExpression="Site_Name">
                                                <HeaderStyle Width="100" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategary" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="60" />
                                                <ItemStyle  HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd " Text="Edit" CommandName="Edt"></asp:LinkButton>
                                                    
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="60" />
                                                <ItemStyle  HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkVacant" OnClientClick="return confirm('Are you sure ?');"
                                                        runat="server" Text="Vacate"  CssClass="anchor__grd vw_grd "  CommandName="Vac"></asp:LinkButton>
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
                        </table>
                    </asp:View>
                    <asp:View ID="EditView" runat="server">
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
         <tr>
                <td style="height: 17px">
                    <uc1:Paging ID="AdvancedLeaveAppOthPaging" runat="server" />
                </td>
            </tr>
    </table>
    <asp:HiddenField ID="hdn" runat="server" />
</ContentTemplate>
</asp:updatepanel>
<asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
  <ProgressTemplate>
   <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
    please wait...
  </ProgressTemplate>
 </asp:UpdateProgress>
</asp:Content>
