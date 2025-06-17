<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="CriteriaofEmpfor_Evaluation.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master"
    Inherits="AECLOGIC.ERP.HMS.CriteriaofEmpfor_Evaluation" Title="" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td>
                <AEC:Topmenu ID="topmenu" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <div id="Newvieew" runat="server">
                    <table width="100%">
                        <tr>
                            <td colspan="2" class="pageheader"></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 72px">
                                <%--CriteriaID<span style="color: red">*</span>--%>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="CrtID" runat="server" Width="316px" MaxLength="50" TabIndex="1" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 124px"><b>Criteria</b>  <span style="color: #ff0000">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtCrt" runat="server" Width="200px" MaxLength="30" TabIndex="1"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 124px"><b>Weightage % </b><span style="color: #ff0000">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPercentage" runat="server" Width="200px" MaxLength="30" TabIndex="1"></asp:TextBox></td>
                        </tr>
                        <tr>
                            
                                <td style="width: 124px"><b>Is Mandatory</b><span style="color: #ff0000">*</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkMandatry" runat="server" TabIndex="1" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 125Px" colspan="2">
                                <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-success"
                                    Width="100px" AccessKey="s" TabIndex="5" OnClick="btnSubmit_Click"
                                    ToolTip="[Alt+s OR Alt+s+Enter]" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="EditView" runat="server">
                    <table width="100%">
                        <tr>
                            <td>
                                <cc1:Accordion ID="EdtViewAccordion" runat="server" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                    RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="70%">
                                    <panes>
                                            <cc1:AccordionPane ID="EdtViewAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                        <td>
                                                              <b>ID </b>&nbsp
                                                                <asp:TextBox ID="txSCrtID" runat="server" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                                                &nbsp 
                                                            <b> Criteria </b>&nbsp
                                                                <asp:TextBox ID="TxtSCriteria" Visible="false" runat="server"></asp:TextBox>
                                                            
                                                            <asp:TextBox ID="TxtScrteria" OnTextChanged="TxtScrteria_TextChanged" runat="server" Height="22px" Width="140px" AutoPostBack="True"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="true"
                                                               MinimumPrefixLength="1" ServiceMethod="GetCompletionCrtList" ServicePath="" TargetControlID="TxtScrteria"
                                                                  UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"></cc1:AutoCompleteExtender>
                                                           <cc1:TextBoxWatermarkExtender  ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="TxtScrteria"
                                                         WatermarkCssClass="watermark" WatermarkText="[Enter Criteria Name]"></cc1:TextBoxWatermarkExtender>
                                                                &nbsp 
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"  OnClick="btnSearch_Click" />
                                                            </td>
                                                        
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                 <asp:RadioButton ID="rbShowActive" AutoPostBack="true" runat="server" Checked="True" TabIndex="1" AccessKey="1" ToolTip="[Alt+1]"
                                                          GroupName="show" Text="Active" OnCheckedChanged="rbShow_CheckedChanged" Style="font-weight: bold" />
                                                            <asp:RadioButton ID="rbShowInactive" AutoPostBack="true" runat="server" GroupName="show"
                                                                Text="Deleted" OnCheckedChanged="rbShow_CheckedChanged" Style="font-weight: bold" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </Content>
                                            </cc1:AccordionPane>
                                        </panes>
                                </cc1:Accordion>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvEV" runat="server" AutoGenerateColumns="False" Width="50%"
                                    HeaderStyle-CssClass="tableHead" OnRowCommand="gvEV_RowCommand"
                                    CssClass="gridview" EmptyDataText="No Records Found"
                                    EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BorderColor="GhostWhite">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="CrtID" runat="server" Visible="true" Text='<%#Eval("CriteriaID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Criteria">
                                            <ItemTemplate>
                                                <asp:Label ID="Criteria" runat="server" Text='<%#Eval("Criteria") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Marks">
                                            <ItemTemplate>
                                                <asp:Label ID="Percentage" runat="server" Text='<%#Eval("WeightagePercent") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd" Text="Edit" CommandArgument='<%#Eval("CriteriaID")%>'
                                                        CommandName="Edt"></asp:LinkButton></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mandatory">
                                               <ItemTemplate>
                                                   <asp:Checkbox ID="chkselect" runat="server" AutoPostBack="true" Checked='<%# Eval("Optional") %>' oncheckedchanged="chkselect_CheckedChanged"  /> 
                                               </ItemTemplate>
                                            </asp:TemplateField>
                                         <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDel" runat="server" CssClass="anchor__grd dlt "  Text='<%#GetText()%>' CommandArgument='<%#Eval("CriteriaID")%>'
                                                    CommandName="Del"></asp:LinkButton></ItemTemplate>
                                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 17px">
                                <uc1:Paging ID="EmpListPaging" runat="server" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
      </table>
</asp:Content>


