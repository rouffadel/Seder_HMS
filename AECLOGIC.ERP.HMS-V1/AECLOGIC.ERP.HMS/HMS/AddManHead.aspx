 <%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="AddManHead.aspx.cs" 
    Inherits="AECLOGIC.ERP.HMS.CMS_AddManHead" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel runat="server">
       <ContentTemplate>

       
    <script language="javascript" type="text/javascript">
        function Confirm(msg) {
            var con = confirm(msg);
            if (con == true) {
                return true;
            }
            else {
                return false;
            }
        }

        function Validate() {


            //for Worksite
            if (!chkDropDownList('<%=ddlWS.ClientID%>', 'Worksite'))
                return false;
            //for department
            if (!chkDropDownList('<%=ddlDept.ClientID %>', 'Department'))
                return false;
            //for Heads
            if (!chkDropDownList('<%=ddlHead.ClientID %>', 'Heads'))
                return false;



            return true;

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
    
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
           <td style="width: 662px">     
                <asp:Label runat="server" id="lblStatus" Text="" Font-Size="14px"></asp:Label>
            

                            
                <asp:MultiView ID="mainview" runat="server">
                    <asp:View ID="Newvieew" runat="server">
                        <table>
                            <tr>
                                <td colspan="2" class="pageheader">
                                    Assign Department Head &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Worksite<span style="color: red">*</span>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlWS" Visible="false" runat="server" Width="200" CssClass="droplist" 
                                        OnSelectedIndexChanged="ddlWS_SelectedIndexChanged" TabIndex="1">
                                    </asp:DropDownList>
                                    
                             
                                    <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
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
                                <td align="left">
                                    Department<span style="color: red">*</span>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDept" Visible="false" runat="server" Width="200" CssClass="droplist" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" TabIndex="2">
                                    </asp:DropDownList>
                                    <cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search..." PromptPosition="Top" 
                                     PromptCssClass="PromptText" QueryPattern="Contains" runat="server" TargetControlID="ddlDept"></cc1:ListSearchExtender>
                                    <asp:TextBox ID="txtdept" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListDep" ServicePath="" TargetControlID="txtdept"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtdept"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Head<span style="color: red">*</span>
                                </td>
                                <td align="left">
                                    <asp:DropDownList Visible="false" ID="ddlHead" CssClass="droplist" runat="server" Width="200" 
                                        TabIndex="3">
                                    </asp:DropDownList>
                                     <cc1:ListSearchExtender ID="ListSearchExtender1" IsSorted="true" PromptText="Type Here To Search..." PromptPosition="Top" 
                                     PromptCssClass="PromptText" QueryPattern="Contains" runat="server" TargetControlID="ddlHead"></cc1:ListSearchExtender>
                                     <asp:TextBox ID="txtSearchEmp" OnTextChanged="GetHeadEmp" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtSearchEmp"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchEmp"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                </td>
                              
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" OnClick="btnSubmit_Click"
                                        OnClientClick="javascript:return Validate()" Text="Assign" Width="100px" 
                                        AccessKey="s" TabIndex="4" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="70%">
                            <tr>
                                <td align="center" style="text-align: left" class="pageheader">
                                    Edit Department Head
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Worksite<span style="color: red">*</span> &nbsp;<asp:DropDownList ID="ddlWSSearch"
                                        runat="server" Width="200" CssClass="droplist" 
                                        OnSelectedIndexChanged="ddlWS_SelectedIndexChanged" AccessKey="w" TabIndex="5" 
                                        ToolTip="[Alt+w OR Alt+w+Enter]">
                                    </asp:DropDownList>
                                    <asp:Button ID="btnSearch" runat="server" Text="Show" CssClass="btn btn-primary" Width="100px"
                                        OnClientClick="javascript:return ShowValidate()" OnClick="btnSearch_Click" 
                                        AccessKey="i" TabIndex="6" ToolTip="[Alt+i OR Alt+i+Enter]" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gdvWS" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                                        GridLines="Both" Width="100%" CellPadding="4" OnRowCommand="gdvWS_RowCommand"
                                        AlternatingRowStyle-BackColor="GhostWhite" OnRowDataBound="gdvWS_RowDataBound"
                                        HeaderStyle-CssClass="tableHead" AllowSorting="true" OnSorting="gdvWS_Sorting1"
                                        EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData">
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrjID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"RowNumber")%>'   ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblwsid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PrjID")%>'   ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Worksite" SortExpression="Site_Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategary" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Site_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeptID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"DepartmentUId")%>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department" SortExpression="DepartmentName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDept" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"DepartmentName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHeadID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"HeadId")%>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Head" SortExpression="name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHead" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd" Text="Edit" CommandName="Edt"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkVac" runat="server" Text="Vacate" CssClass="btn btn-success" CommandName="Vac"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                             <tr>
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
    </asp:UpdatePanel>
      <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
            <ProgressTemplate>
                <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                    ID="imgs" />
                please wait...
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
