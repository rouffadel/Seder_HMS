<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="CreateGenEmpdocuments.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpdocumentsEditing" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">

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
                           <td style="vertical-align: top">
                            Issued Date:
                            <asp:TextBox ID="txtAppDate" runat="server"  Width="120px"></asp:TextBox><cc1:CalendarExtender
                                    TargetControlID="txtAppDate" PopupButtonID="txtAppDate" ID="CalendarExtender2"
                                runat="server" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                        </tr>   
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
                                       <asp:TextBox runat="server" ID="DocEditor" TextMode="MultiLine" Columns="50" Rows="10"  Width="1050px"  Height="600px" Text=" " /><br />
                                   <ajax:HtmlEditorExtender ID="htmlExtDocEditor"  EnableSanitization="false" TargetControlID="DocEditor"  runat="server"/>
                                    <br />
                                     <asp:Button id="btnSubmit" Text="Submit" CssClass="btn btn-success" Runat="server"  
                                          OnClientClick="javascript:return validate();" onclick="btnSubmit_Click"/>
                                  </td>
                              
                              </tr>
                        
                        </table>
                    
                    </td>
                
                </tr>
                </table>
       
        
       
    
       
    </div>
</asp:Content>

