<%@ Page Title="" Language="C#" AutoEventWireup="True" CodeBehind="EditgenEmpDocuments.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EditgenEmpDocuments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceholder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function validate() {


            if (document.getElementById('<%=txtAppDate.ClientID%>').value == "") {
            alert("Please Enter Issued Date");
            return false;
        }
        if (document.getElementById('<%=DocEditor.ClientID%>').value == "") {
            alert("Please Enter Document Text");
            return false;
        }


    }



</script>
    <div>
         <table>
                <tr>
                    <td>
                        <table>
                         
                                <tr>
                                  <td colspan="2" >
                                   &nbsp;                   
                                  </td>
                                  
                               </tr>
                           <td style="vertical-align: top">
                            Issued Date:
                            <asp:TextBox ID="txtAppDate" runat="server"  Width="120px"></asp:TextBox><cc1:CalendarExtender
                                    TargetControlID="txtAppDate" PopupButtonID="txtAppDate" ID="CalendarExtender2"
                                runat="server" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                                <tr>
                                  <td colspan="2" >
                                <%--  <span onclick="cmdExec('paste');" >[Designation]</span>             --%>        
                                  </td>
                                  
                               </tr>
                               <tr>
                                  <td colspan="2" >
                                      <b> Note*</b>  Paste from Wordpad/Notepd (Don&#39;t use MS-Word ) to maintain stable format
                                  </td>
                                  
                               </tr>
                              <tr>
                                  <td colspan="2">

                                      <asp:TextBox runat="server" ID="DocEditor" TextMode="MultiLine" Columns="50" Rows="10" Text=" " Width="950px"  Height="600px"/><br />
                                   <ajax:HtmlEditorExtender ID="htmextDocEditor"  EnableSanitization="false"   TargetControlID="DocEditor" runat="server"/>

                                    <br />
                                     <asp:Button id="btnSubmit" Text="Submit" Runat="server"  
                                          OnClientClick="javascript:return validate();" onclick="btnSubmit_Click" 
                                          CssClass="savebutton btn btn-success" Font-Bold="True"/>
                                      <asp:Button ID="Button1" runat="server" CssClass="savebutton btn btn-primary" Font-Bold="True" 
                                          onclick="Button1_Click" Text="Back" />
                                  </td>
                              
                              </tr>
                        
                        </table>
                    
                    </td>
                
                </tr>
                </table>
       
        
       
    
       
    </div>
</asp:content>

