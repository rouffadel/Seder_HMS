<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobResponsibiliti.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.HMS.JobResponsibiliti"  %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
      <style>
        .tooltipCss {
            position: absolute;
            border: 1px solid gray;
            margin: 1em;
            padding: 3px;
            background: #A4D162;
            font-family: Trebuchet MS;
            font-weight: normal;
            color: black;
            font-size: 11px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function GetempID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlsresp_hid.ClientID %>').value = HdnKey;
        }
        function CheckTextLength(text, long) {
            var maxlength = new Number(long); // Change number to your max length.
            if (text.value.length > maxlength) {
                text.value = text.value.substring(0, maxlength);
                alert(" Only " + long + " characters allowed");
            }
        }
        function GetempTitleID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            //  alert(HdnKey);
            document.getElementById('<%=ddlTitle_hid.ClientID %>').value = HdnKey;
        }
        </script>
       <asp:updatepanel runat="server" ID="UpdatePanel1">
  <ContentTemplate>    
    <div id="dvadd" runat="server">
        <asp:Panel ID="pnltblEdit" runat="server" CssClass="box box-primary" Visible="true"
        Width="50%">
        <table>
            <asp:TextBox ID="txtRespID" runat="server" Width="150%" Visible="false"></asp:TextBox>
            <tr>
                <td><b>JobTitle With specialization:</b></td><td></td>
                    <td>
                <asp:DropDownList ID="ddDesignation" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td><b>JobResponsibilities:</b></td><td></td>
                    <td>
                        <asp:TextBox ID="txtJobresponce" runat="server" onKeyUp="CheckTextLength(this,250)" onChange="CheckTextLength(this,250)" TextMode="MultiLine" Width="150%" Height="80px" MaxLength="250"   Style="resize:none"></asp:TextBox>
 
                    <cc1:TextBoxWatermarkExtender ID="txtwmJobresponce" runat="server" WatermarkText="[Job Responsibilities [MAX 250 CHARACTERS ONLY] ]" 
                        TargetControlID="txtJobresponce">
                    </cc1:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr><td></td>
                <td></td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success"   OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
            </asp:Panel>
    </div>
     <div id="dvView" runat="server">
        <table width="100%">
               <tr>
            <td>
                <cc1:Accordion ID="ViewAppLstAccordion" runat="server" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="80%">
                    <Panes>
                        <cc1:AccordionPane ID="ViewAppLstAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td colspan="2">
                                            <b>
                                                <asp:Label ID="lblJobResponsibilities" runat="server" Text="Responsibility"></asp:Label>
                                 <asp:HiddenField ID="ddlsresp_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchWorksite"  Height="22px" Width="140px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionjobRespList" ServicePath="" TargetControlID="txtSearchWorksite"    
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"  OnClientItemSelected="GetempID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Responsibility]">
                                            </cc1:TextBoxWatermarkExtender>
                                                 &nbsp;&nbsp;&nbsp;&nbsp;
                                                  <asp:Label ID="lblTitle" runat="server" Text="Desigination/Specialization"></asp:Label>
                                  <asp:HiddenField ID="ddlTitle_hid" runat="server" />
                                             <asp:TextBox ID="txtTitles"  Height="22px" Width="140px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListitle" ServicePath="" TargetControlID="txtTitles"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetempTitleID">
                                            </cc1:AutoCompleteExtender>
                                         
                                              &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnSearch" runat="server" Text="Search"   OnClick="btnSearch_Click"
                                                TabIndex="5" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" CssClass="btn btn-primary" 
                                                Width="50px" />
                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </cc1:AccordionPane>
                    </Panes>
                </cc1:Accordion>
            </td>
        </tr>
            <table width="100%" >
             <tr>
                <td>
                   <asp:GridView ID="gvjobterm" AlternatingRowStyle-BackColor="GhostWhite" AutoGenerateColumns="false" runat="server" GridLines="Both" CssClass="gridview" Width="80%" OnRowCommand="gvjobterm_RowCommand">
                     <Columns>
                         <asp:BoundField DataField="Jobtitleid" HeaderText="Titleid" ItemStyle-Width="70px"/>
                             <asp:BoundField DataField="Name" HeaderText="Desigination/Specialization" ItemStyle-Width="160px"/>
                         <asp:BoundField DataField="RespDescription" HeaderText="Responsibility" ItemStyle-Width="950px"/>
                
                          <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="anchor__grd edit_grd" CommandArgument='<%#Eval("RespID")%>'
                                                        CommandName="Edt"></asp:LinkButton></ItemTemplate>
                                            </asp:TemplateField>
                     </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>

                  <table width="79%">
                     <td  style="height: 17px;width=10px">
                    <uc1:Paging ID="Pagetax" Visible="true" runat="server" />

                </td>
                </table>
            </tr>
                </table>
        </table>
    </div>
</ContentTemplate>
</asp:updatepanel>
<asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
  <ProgressTemplate>
   <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" 
/>
    please wait...
  </ProgressTemplate>

 </asp:UpdateProgress>
</asp:Content>

