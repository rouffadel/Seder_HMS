<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignWorksite.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.AssignWorksite" %>


<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ MasterType VirtualPath="~/Templates/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function Multiply(contorl) {
            if (checkdecmial(contorl) == false) {
                contorl.focus();
                return false;
            }
        }

        function RadioCheck(rb) {
            var gv = document.getElementById("<%=GVIndent.ClientID%>");
            var rbs = gv.getElementsByTagName("input");

            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }



        //        function SelectAll(CheckBox) {

        //            alert('test1');
        //            TotalChkBx = parseInt('<%= this.ChkCostCenter.Items.Count %>');
        //            var TargetBaseControl = document.getElementById('<%= this.ChkCostCenter.ClientID %>');
        //            var TargetChildControl = "chkSelect";
        //            var Inputs = TargetBaseControl.getElementsByTagName("input");
        //            for (var iCount = 0; iCount < Inputs.length; ++iCount) {
        //                if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0)
        //                    Inputs[iCount].checked = CheckBox.checked;
        //            }
        //        }


    </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
                <ajax:Accordion ID="ACC" runat="server" Width="100%" TransitionDuration="50" FramesPerSecond="10"
                    HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                    ContentCssClass="accordionContent" RequireOpenedPane="False" SuppressHeaderPostbacks="True"
                    meta:resourcekey="ACCResource1">
                    <panes>
                                <Ajax:AccordionPane ID="Ap1" runat="server" ContentCssClass="" HeaderCssClass=""
                                    meta:resourcekey="Ap1Resource1">
                                    <Header>
                                        <b>
                                            <asp:Label ID="lblAssignCostCenter" runat="server" meta:resourcekey="lblAssignCostCenterResource1"
                                                Text="Assign WorkSite"></asp:Label>
                                        </b>
                                    </Header>
                                    <Content>
                                        <table id="t11" cellpadding="2" cellspacing="2" width="100%">
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                   
                                                                <asp:Label ID="LblWorkSite" runat="server" meta:resourcekey="LblWorkSiteResource1"
                                                                    Text="Worksite"></asp:Label>
                                                                : &nbsp;<asp:DropDownList ID="ddlDropWorkSite" runat="server" CssClass="droplist" AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]"
                                                                    meta:resourcekey="ddlDropWorkSiteResource1">
                                                                </asp:DropDownList>
                                                                <Ajax:ListSearchExtender ID="ListSearchExtender1" runat="server" Enabled="True" IsSorted="True" 
                                                                    PromptText="Type Here To Search" QueryPattern="Contains" TargetControlID="ddlDropWorkSite">
                                                                </Ajax:ListSearchExtender>
                                                     
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="lblDepartments" runat="server" meta:resourcekey="lblDepartmentsResource1"
                                                                    Text="Departments"></asp:Label>
                                                                : &nbsp;<asp:DropDownList ID="ddlDept" runat="server" CssClass="droplist" meta:resourcekey="ddlDeptResource1">
                                                                </asp:DropDownList>
                                                                <Ajax:ListSearchExtender ID="ListSearchExtender2" runat="server" Enabled="True" IsSorted="True"
                                                                    PromptText="Type Here To Search" QueryPattern="Contains" TargetControlID="ddlDept">
                                                                </Ajax:ListSearchExtender>
                                                  

                                                       &nbsp; &nbsp;<b>Employee Id/Name:-</b>&nbsp;
                                                        <asp:TextBox ID="txtSearchEmployee"  Height="22px" Width="140px" runat="server"></asp:TextBox>
                                                                <Ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmp" ServicePath="" TargetControlID="txtSearchEmployee"
                                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                                                </Ajax:AutoCompleteExtender>
                                                                <Ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearchEmployee"
                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Emp ID/Name]">
                                                                </Ajax:TextBoxWatermarkExtender>

                                                            
                                                            
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="BtnSearch" runat="server" CssClass="btn btn-primary" meta:resourcekey="BtnSearchResource1"
                                                                    OnClick="BtnSearch_Click" Text="Search" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]"></asp:Button>
                                                        
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </Content>
                                </Ajax:AccordionPane>
                            </panes>
                </ajax:Accordion>
            </td>
        </tr>
        <tr>
            <td>
                <table id="tbl1Grid" runat="server" visible="False" width="100%">
                    <tr id="Tr1" runat="server">
                        <td id="Td1" runat="server">
                            <asp:GridView ID="GVIndent" runat="server" Width="100%" HeaderStyle-CssClass="tableHead"
                                AutoGenerateColumns="false" OnSorting="GVIndent_Sorting" AllowSorting="true"
                                EmptyDataText="No Record(s) Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                OnRowCommand="GVIndent_RowCommand" AlternatingRowStyle-BackColor="GhostWhite"
                                OnRowDataBound="GVIndent_RowDataBound" CssClass="gridview">
                                <HeaderStyle CssClass="tableHead" />
                                <RowStyle CssClass="gentext" />
                                <AlternatingRowStyle BackColor="GhostWhite" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:RadioButton ID="RadioButton1" runat="server"
                                                onclick="RadioCheck(this);" OnCheckedChanged="rbtnSelect_CheckedChanged" AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblempid" runat="server" Text='<%#Eval("empid") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SiteName" HeaderText="Worksite" SortExpression="SiteName">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="departmentname" HeaderText="Department" SortExpression="departmentname">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="name" HeaderText="Employee Name" SortExpression="name">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="designation" HeaderText="Designation" SortExpression="designation">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                   
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <AEC:Paging ID="MPPaging" runat="server" />
                        </td>
                    </tr>
                    <tr id="Tr2" runat="server">
                        <td id="Td2" runat="server">
                            <br />
                            <asp:UpdatePanel ID="upgvAssignMaterials" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <asp:CheckBox ID="chkAll" Text="Select All" OnCheckedChanged="chkAll_CheckedChanged" runat="server" AutoPostBack="true" />
                                        <asp:CheckBoxList runat="server" ID="ChkCostCenter" CellSpacing="2" RepeatColumns="4"
                                            RepeatDirection="Horizontal" AccessKey="c" ToolTip="[Alt+c OR Alt+c+Enter]">
                                        </asp:CheckBoxList>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1">
                    <ProgressTemplate>
                        <asp:Panel ID="Panel1" CssClass="box box-primary" runat="server" meta:resourcekey="Panel1Resource1">
                            <asp:Panel ID="Panel2" CssClass="box box-primary" runat="server" meta:resourcekey="Panel2Resource1">
                                <asp:Image runat="server" ImageUrl="~/images/Loding.gif" ImageAlign="AbsMiddle" ID="imgs1"
                                    meta:resourcekey="imgs1Resource1" />
                                Please wait...
                            </asp:Panel>
                        </asp:Panel>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Save" OnClick="BtnSave_Click"
                    meta:resourcekey="BtnSaveResource1" />
            </td>
        </tr>
    </table>

</asp:Content>