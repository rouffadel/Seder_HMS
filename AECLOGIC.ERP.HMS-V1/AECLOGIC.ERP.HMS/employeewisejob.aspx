<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="employeewisejob.aspx.cs" Inherits="AECLOGIC.ERP.HMS.employeewisejob"  MasterPageFile="~/Templates/CommonMaster.master"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

       <script language="javascript" type="text/javascript">
 function GETEmp_ID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
             //alert(HdnKey);
            document.getElementById('<%=txtEmployeeName_hid.ClientID %>').value = HdnKey;
        }
           function GETdes_ID(source, eventArgs) {
               var HdnKey = eventArgs.get_value();
               //  alert(HdnKey);
               document.getElementById('<%=txtJobDescr_hid.ClientID %>').value = HdnKey;
           }
           function GETres_ID(source, eventArgs) {
               var HdnKey = eventArgs.get_value();
               //  alert(HdnKey);
               document.getElementById('<%=ddlEmp_hid.ClientID %>').value = HdnKey;
           }
           function GETwor_ID(source, eventArgs) {
               var HdnKey = eventArgs.get_value();
               //  alert(HdnKey);
               document.getElementById('<%=txtResponsibilities_hid.ClientID %>').value = HdnKey;
           }
   
        </script>
 <AEC:Topmenu ID="topmenu" runat="server" />
      <div id="dvadd" runat="server">

        <table>
            
            <tr>
                <td>Employee Name:
                        <asp:HiddenField ID="txtEmployeeName_hid" runat="server"  />
                <asp:TextBox ID="txtEmployeeName" runat="server" Width="100%"></asp:TextBox>
                         <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListEmployee" ServicePath="" TargetControlID="txtEmployeeName"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"  OnClientItemSelected="GETEmp_ID">
                                            </cc1:AutoCompleteExtender>

                     <cc1:TextBoxWatermarkExtender ID="txtwmeEmployeeName" runat="server" WatermarkText="[Employee Name]"
                        TargetControlID="txtEmployeeName">
                    </cc1:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr>
                <td>JobDescription :
                   <asp:HiddenField ID="txtJobDescr_hid" runat="server" />
    <asp:TextBox ID="txtJobDescr" runat="server" Width="50%"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionjobdespList" ServicePath="" TargetControlID="txtJobDescr"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"  OnClientItemSelected="GETdes_ID">
                                            </cc1:AutoCompleteExtender>
                    
                    <cc1:TextBoxWatermarkExtender ID="txtwmeJobDescription" runat="server" WatermarkText="[Job Description]"
                        TargetControlID="txtJobDescr">
                    </cc1:TextBoxWatermarkExtender>
                </td>
            </tr>
           <tr>
               <td>
                   Responsibilities:
                   <asp:HiddenField ID="txtResponsibilities_hid" runat="server" />
                   <asp:TextBox ID="txtResponsibilities" runat="server" Width="50%"></asp:TextBox>
                     <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="[Responsibilities ]"
                        TargetControlID="txtResponsibilities"  >
                    </cc1:TextBoxWatermarkExtender>
                    <asp:HiddenField ID="ddlEmp_hid" runat="server" />
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionjobRespList" ServicePath="" TargetControlID="txtResponsibilities"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"  OnClientItemSelected="GETres_ID">
                                            </cc1:AutoCompleteExtender>
               </td>
           </tr>
          
            <tr>
               <td>
                   Worksite:
                   <asp:HiddenField ID="txtworksite_hid" runat="server" />
                   <asp:TextBox ID="txtWorksite" runat="server" Width="50%"></asp:TextBox>
                       <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" WatermarkText="[Worksites ]"
                        TargetControlID="txtWorksite"  >
                                      </cc1:TextBoxWatermarkExtender>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionjobWorksiteList" ServicePath="" TargetControlID="txtWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"  OnClientItemSelected="GETwor_ID">
                                            </cc1:AutoCompleteExtender>
               </td>
           </tr>
              <tr>
                <td>
       <asp:Button ID="btnSub" runat="server" Text="Save"  OnClick="btnSub_Click" />
                </td>
            </tr>
            
        </table>
    </div>
        <div id="dvView" runat="server">
             <tr>

                <td style="height: 17px">
                    <uc1:Paging ID="Pagetax" Visible="true" runat="server" />
                </td>
            </tr>
            </div>
    </asp:Content>