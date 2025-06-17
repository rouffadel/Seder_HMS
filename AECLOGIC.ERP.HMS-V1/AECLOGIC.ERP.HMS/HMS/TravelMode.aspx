<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TravelMode.aspx.cs" Inherits="AECLOGIC.ERP.HMSV1.TravelModeV1" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
<script>  function GetName(source, eventArgs) { var HdnKey = eventArgs.get_value();  document.getElementById('<%=hdSName .ClientID %>').value = HdnKey; } 
</script><!--THIS IS SAMPLE SEARCH FILTER--> 
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
<tr> <td>  <AEC:Topmenu ID="topmenu" runat="server" />  </td> </tr>
<tr> <td> <div id="NewView" runat="server">
<table width="100%"><tr> <td colspan="2" class="pageheader"></td> </tr>
<tr>
<td style="width: 124px"> <asp:Label ID="lblName" runat="server">Name<span style="color: #ff0000">*</span></asp:Label> </td>
<td> <asp:Textbox ID="txtName" runat="server" Width="200px" MaxLength="30" TabIndex="2"></asp:Textbox><asp:RequiredFieldValidator runat="server" ErrorMessage="Is Required" ID="rqName" ValidationGroup="save" ControlToValidate="txtName" SetFocusOnError="true" InitialValue="" ></asp:RequiredFieldValidator><asp:HiddenField ID="hdID"   runat="server" Value="0"></asp:HiddenField>  </td>
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
<cc1:Accordion ID="EdtViewAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="70%">
<panes>
<cc1:AccordionPane ID="EdtViewAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
ContentCssClass="accordionContent">
<Header>
Search Criteria</Header>
<Content>
<table cellpadding="0" cellspacing="0" style="width: 100%">
<tr>
<td>
 <asp:TextBox ID="txtSName" runat="server" Height="22px" Width="140px" AutoPostBack="True">  </asp:TextBox> 
 <asp:HiddenField ID="hdSName" runat="server"  Value="0" /> 
 <cc1:AutoCompleteExtender ID="AutoCompleteExtenderName" runat="server" DelimiterCharacters=""   Enabled="true" MinimumPrefixLength="1" ServiceMethod="GetCompletionName" ServicePath=""  TargetControlID="txtSName" UseContextKey="True" CompletionInterval="10"  CompletionListCssClass="autocomplete_completionListElement"  CompletionListItemCssClass="autocomplete_listItem" OnClientItemSelected="GetName"  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">  </cc1:AutoCompleteExtender> 
 <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderName" runat="server" TargetControlID="txtSName"  WatermarkCssClass="watermark" WatermarkText="[Enter Name]"></cc1:TextBoxWatermarkExtender> 
<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"  OnClick="btnSearch_Click" />
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
<asp:GridView ID="gvEV" onrowcommand ="gvEV_RowCommand" runat="server" OnClientClick="return confirm('Are you sure to Delete Record!')" AutoGenerateColumns="false" Width="50%" CssClass="gridview" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BorderColor="GhostWhite">
<Columns>
<asp:BoundField HeaderText ="ID"  DataField ="ID" />
<asp:BoundField HeaderText ="Name"  DataField ="Name" />
<asp:BoundField HeaderText ="IsActive"  DataField ="IsActive" />
<asp:TemplateField>
<ItemTemplate>
<asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd" Text="Edit" CommandArgument='<%#Eval("ID")%>' CommandName="Edt"></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField>
<ItemTemplate>
<asp:LinkButton ID="lnkDelete" runat="server" CssClass="anchor__grd dlt" Text="Delete" CommandArgument='<%#Eval("ID")%>' CommandName="Del"></asp:LinkButton>
</ItemTemplate>
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
	</td> </tr>
	</table>
</asp:content>
