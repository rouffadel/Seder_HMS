<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OPtionNew.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.OPtionNew" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <script language="javascript" type="text/javascript">

      function GetID(source, eventArgs) {
          var HdnKey = eventArgs.get_value();
          document.getElementById('<%=txtpurpose_hid.ClientID %>').value = HdnKey;
      }
      function prcid(source, eventArgs) {
          var HdnKey = eventArgs.get_value();
          document.getElementById('<%=txtprocessnamr_hid.ClientID %>').value = HdnKey;
      }
      function aspxid(source, eventArgs) {
          var HdnKey = eventArgs.get_value();
          document.getElementById('<%=txtaspx_hid.ClientID %>').value = HdnKey;
      }
      </script>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
<tr> <td>   </td> </tr>
<tr> <td> <div id="NewView" runat="server" visible="false">
<table width="100%"><tr> <td colspan="2" class="pageheader"></td> </tr>
<tr>
<td style="width: 124px"> <asp:Label ID="lblPurpose" runat="server">Purpose<span style="color: #ff0000">*</span></asp:Label> </td>
<td> <asp:Textbox ID="txtPurpose1" runat="server" Width="200px" MaxLength="30" TabIndex="2"></asp:Textbox><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqPurpose" ValidationGroup="save" ControlToValidate="txtPurpose1" SetFocusOnError="true" InitialValue="" ></asp:RequiredFieldValidator><asp:HiddenField ID="hdOptionID"   runat="server" Value="0"></asp:HiddenField>  </td>
</tr>

<tr>
<td style="width: 124px"> <asp:Label ID="lblValue" runat="server">Value<span style="color: #ff0000">*</span></asp:Label> </td>
<td> <asp:Textbox ID="txtValue" runat="server" Width="200px" MaxLength="30" TabIndex="3"></asp:Textbox><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqValue" ValidationGroup="save" ControlToValidate="txtValue" SetFocusOnError="true" InitialValue="" ></asp:RequiredFieldValidator></td>
</tr>

<tr>
<td style="width: 124px"> <asp:Label ID="lblUpdateBy" runat="server">UpdateBy<span style="color: #ff0000">*</span></asp:Label> </td>
<td> <asp:DropDownList ID="ddlUpdateBy" runat="server" Width="200px" MaxLength="30" TabIndex="4"></asp:DropDownList><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqUpdateBy" ValidationGroup="save" ControlToValidate="ddlUpdateBy" SetFocusOnError="true" InitialValue="" ></asp:RequiredFieldValidator></td>
</tr>

<tr>
<td style="width: 124px"> <asp:Label ID="lblUpdatedOn" runat="server">UpdatedOn<span style="color: #ff0000">*</span></asp:Label> </td>
<td> <asp:Textbox ID="txtUpdatedOn" runat="server" Width="200px" MaxLength="30" TabIndex="5"></asp:Textbox><cc1:CalendarExtender ID="calUpdatedOn" runat="server" TargetControlID="txtUpdatedOn" PopupButtonID="txtUpdatedOn"  Format="dd MMM yyyy" Enabled="true"> </cc1:CalendarExtender>
<asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqUpdatedOn" ValidationGroup="save" ControlToValidate="txtUpdatedOn" SetFocusOnError="true" InitialValue="" ></asp:RequiredFieldValidator></td>
</tr>



<tr>
<td style="width: 124px"> <asp:Label ID="lbldependencyDesc" runat="server">dependencyDesc<span style="color: #ff0000">*</span></asp:Label> </td>
<td> <asp:Textbox ID="txtdependencyDesc" runat="server" Width="200px" MaxLength="30" TabIndex="8"></asp:Textbox><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqdependencyDesc" ValidationGroup="save" ControlToValidate="txtdependencyDesc" SetFocusOnError="true" InitialValue="" ></asp:RequiredFieldValidator></td>
</tr>

<tr>
<td style="padding-left: 125Px" colspan="2"> <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-success"  Width="100px" AccessKey="s" TabIndex="5" OnClick="btnSubmit_Click"  ToolTip="[Alt+s OR Alt+s+Enter]"  ValidationGroup="save" /> </td>
</tr>
</table>
</div>
<div id="EditView" runat="server">

<table width="100%">
<tr>
<td>
<cc1:Accordion ID="EdtViewAccordion" runat="server" HeaderCssClass="accordionHeader" 
    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" AutoSize="None"
     FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="80%">
<panes>
<cc1:AccordionPane ID="EdtViewAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
ContentCssClass="accordionContent">
<Header>
Search Criteria</Header>
<Content>

<table cellpadding="0" cellspacing="0" style="width: 100%">
<tr>
<td>
    <asp:HiddenField ID="txtpurpose_hid" runat="server" />
                                             <asp:TextBox ID="txtpurpose" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtpurpose"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtpurpose"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Purpose Name]">
                                           </cc1:TextBoxWatermarkExtender>    
                                             <asp:TextBox ID="txtval" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                            
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtval"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Value ]">
                                           </cc1:TextBoxWatermarkExtender>
     <asp:HiddenField ID="txtprocessnamr_hid" runat="server" />
                                             <asp:TextBox ID="txtprcs" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="txtprcsname" ServicePath="" TargetControlID="txtpurpose"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="prcid">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtprcs"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Process Name]">
                                           </cc1:TextBoxWatermarkExtender>
         <asp:HiddenField ID="txtaspx_hid" runat="server" />
                                             <asp:TextBox ID="txtaspx" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="txtaspxservice" ServicePath="" TargetControlID="txtaspx"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="aspxid">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtaspx"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Page Reference Name]">
                                           </cc1:TextBoxWatermarkExtender><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"  OnClick="btnSearch_Click" />
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
<asp:GridView ID="gvEV" onrowcommand ="gvEV_RowCommand" runat="server"
     OnClientClick="return confirm('Are you sure to Delete Record!')" AutoGenerateColumns="false" Width="80%" CssClass="gridview" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BorderColor="GhostWhite">
<Columns>
<asp:BoundField HeaderText ="OptionID"  DataField ="OptionID" ItemStyle-Width="40px" HeaderStyle-Width="20px"  />
<asp:BoundField HeaderText ="Purpose"  DataField ="Purpose" ItemStyle-Width="400px" HeaderStyle-Width="30px" />
<asp:BoundField HeaderText ="Value"  DataField ="Value" ItemStyle-Width="60px" HeaderStyle-Width="20px" />
<asp:BoundField HeaderText ="IsModify"  DataField ="IsModify" visible="false" />
<asp:BoundField HeaderText ="Process Name"  DataField ="dependencyDesc" />
<asp:BoundField HeaderText ="AspxPageReference"  DataField ="AspxPageReference" />
<%--<asp:TemplateField>
<ItemTemplate>
<asp:LinkButton ID="lnkEdit" Visible="false" runat="server" CssClass="anchor__grd edit_grd" Text="Edit" CommandArgument='<%#Eval("OptionID")%>' CommandName="Edt"></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField>
<ItemTemplate>
<asp:LinkButton ID="lnkDelete" runat="server" CssClass="anchor__grd dlt" Text="Delete" CommandArgument='<%#Eval("OptionID")%>' CommandName="Del"></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>--%>
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
	</td> </tr>
	</table>
</asp:Content>
