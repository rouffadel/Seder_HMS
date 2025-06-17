<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="OtherDetails.aspx.cs" Inherits="AECLOGIC.ERP.HMS.OtherDetails" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">

<script language="javascript" type="text/javascript">
function ValidEmergency() 
        {

            // E-MailID
            if (!chkEmail('<%=txtEmail.ClientID %>', 'Email', false, '')) {
                return false;
            }
            
        }
        function ValidPassport() {

            //For Issue
            if (!chkDate('<%=txtIssueDate.ClientID%>', "Passport issue date", true, "")) {
                return false;
            }

            //For Expiry
            if (!chkDate('<%=txtExpiryDate.ClientID%>', "Expiry issue date", true, "")) {
                return false;
            }

        }

        function ValidInsurance() {



            if (!chkFloatNumber('<%=txtMonthlyPrem.ClientID%>', " Percentage", "false", ""))
                return false;
            //For Issue
            if (!chkDate('<%=txtInsIssueDate.ClientID%>', "Passport issue date", true, "")) {
                return false;
            }

            //For Expiry
            if (!chkDate('<%=txtInsExpDate.ClientID%>', "Expiry issue date", true, "")) {
                return false;
            }
          
        }

        function redircet() { window.history.back(-1);
        }
        function show_image() {
            window.open('Passport/1219.jpg');
        }
         </script>

<table width="80%">
               
                <tr>
                <td>
                <asp:Button ID="Button1" runat="server" Text="Back" CssClass="savebutton btn btn-success"  Width ="50px" OnClick="Button1_Click" OnClientClick="redircet();" > </asp:Button>
            
                </td></tr>
                <tr>
                    <td>
                 <asp:LinkButton ID="lnkAdd" runat="server" Text="Add" OnClick="lnkAdd_Click"></asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="lnkEdit_Click"></asp:LinkButton>
                        </td>
                </tr>

                                <tr>
                    <td align="left" colspan="2">
                       
                        <asp:Panel ID="pnl11" runat="server" CssClass="box box-primary" Width="850px">

                             <cc1:TabContainer ID="tb" runat="server" ActiveTabIndex="0" AutoPostBack="false"
                                Width="850px" BorderWidth="0" Height="350px">
                                <cc1:TabPanel runat="server" HeaderText=" Passport" ID="tabPerDetails" Enabled="true">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel5" runat="server"  Height="350px">
                                              <table>
                                                   <tr>
                                                       <td>
                                                            Number  &nbsp;&nbsp;&nbsp;&nbsp;
                                                       </td>
                                                       <td>
                                                           <asp:TextBox ID="txtPassportNo" runat="server"></asp:TextBox>
                                                       </td>
                                                   </tr>
                                              <tr>
                                                       <td>
                                                       Issuer
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtIssuer" runat="server"></asp:TextBox>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                       Place of Issue 
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtIssuePlace" runat="server"></asp:TextBox>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                       Issue Date
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtIssueDate" runat="server"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtIssueDate"
                                PopupButtonID="txtIssueDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                       Expiry Date
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtExpiryDate" runat="server"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtExpiryDate"
                                PopupButtonID="txtExpiryDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                       Remarks
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                       
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                        
                                                       </td>
                                                       <td>
                                                       <asp:FileUpload ID="ImgUpload" runat="server" /><asp:ImageButton runat="server" ID="lnkdownload"  ImageUrl="~/Images/downloads.png"      OnClick="lnkdownload_img" ToolTip="Dwonload" />&nbsp;&nbsp;<asp:ImageButton runat="server" ID="lnkdel"  ImageUrl="~/Images/Delete.ico" Height="16px" Width="16px" OnClick="lnk_delimage"    ToolTip="Delete" />
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                           
                                                       </td>
                                                       <td>
                                                           <asp:Button ID="btnPassport" runat="server" Text="Save" OnClick="btnPassport_Click" CssClass="savebutton btn btn-success"  OnClientClick="javascript:return ValidPassport();"/>
                                                       </td>
                                                   </tr>
                                              </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </cc1:TabPanel>

                                 <cc1:TabPanel runat="server" HeaderText="Emergency" ID="TabPanel1" Enabled="true">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel1" runat="server" Height="350px">

                                        <table>

                                        <tr>
                                            <td colspan="2">
                                                       <asp:Label ID="lblLocal" Text="Local" runat="server"  CssClass="savebutton"></asp:Label>
                                            </td>
                                        </tr>
                                                   <tr>
                                                       <td>
                                                            Contact Name
                                                       </td>
                                                       <td>
                                                           <asp:TextBox ID="txtContactName" runat="server"></asp:TextBox>
                                                       </td>
                                                   </tr>
                                              <tr>
                                                       <td>
                                                       Relation
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtRelation" runat="server"></asp:TextBox>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                        Email
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                       Contact Mobile
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtContactMobile" runat="server"></asp:TextBox>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                       Contact Phone
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtContactPhone" runat="server"></asp:TextBox>
                                                       
                                                       </td>
                                                   </tr>

                                                   <tr>
                                                       <td>
                                                            Marital status
                                                       </td>
                                                       <td>
                                                              <asp:DropDownList ID="ddlMariSta" runat="server" OnSelectedIndexChanged="ddlMariSta_SelectedIndexChanged" AutoPostBack="true">
                                                           
                                                           <asp:ListItem Text="Single" Value="1"  ></asp:ListItem>
                                                           <asp:ListItem Text="Married" Value="2" ></asp:ListItem>
                                                           <asp:ListItem Text="Divorced" Value="3" ></asp:ListItem>
                                                           <asp:ListItem Text="Widowed" Value="4" ></asp:ListItem>
                                                           <asp:ListItem Text="Separated" Value="5" ></asp:ListItem>
                                                           <asp:ListItem Text="Living common law" Value="6" ></asp:ListItem>
                                                           <asp:ListItem Text="Other" Value="7" ></asp:ListItem>

                                                           </asp:DropDownList>
                                                       </td>
                                                   </tr>
                                                   <tr id="trMarriage" runat="server" visible="false">
                                                       <td>
                                                       Date of marriage
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtDateofmarriage" runat="server" ></asp:TextBox>
                                                         <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateofmarriage"
                                PopupButtonID="txtDateofmarriage" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                                                       </td>
                                                   </tr>
                                                  
                                                   <tr>
                                            <td colspan="2">
                                                       <asp:Label ID="lblCountryOrigin" Text="Country of Origin" runat="server" CssClass="savebutton"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                                       <td>
                                                            Contact Name : &nbsp;&nbsp;&nbsp;&nbsp;
                                                       </td>
                                                       <td>
                                                           <asp:TextBox ID="txtORContactName" runat="server"></asp:TextBox>
                                                       </td>
                                                   </tr>
                                              <tr>
                                                       <td>
                                                       Relation
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtORRelation" runat="server"></asp:TextBox>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                        Email
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtOREmail" runat="server"></asp:TextBox>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                       Contact Mobile
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtORContactMob" runat="server"></asp:TextBox>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                       Contact Phone
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtORContactPhone" runat="server"></asp:TextBox>
                                                       
                                                       </td>
                                                   </tr>

                                                   <tr>
                                                       <td>
                                                           
                                                       </td>
                                                       <td>
                                                           <asp:Button ID="btnEmargency" runat="server" Text="Save" OnClick="btnEmargency_Click"  CssClass="savebutton btn btn-success"  OnClientClick="javascript:return ValidEmergency();"/>
                                                       </td>
                                                   </tr>
                                              </table>

                                        </asp:Panel>
                                    </ContentTemplate>
                                </cc1:TabPanel>

                                 <cc1:TabPanel runat="server" HeaderText="Insurance" ID="TabPanel2" Enabled="true">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel2" runat="server" Height="350px">

                                        <table>
                                                   <tr>
                                                       <td>
                                                            Policy number  &nbsp;&nbsp;&nbsp;&nbsp;
                                                       </td>
                                                       <td>
                                                           <asp:TextBox ID="txtPolicyNo" runat="server"></asp:TextBox>
                                                       </td>
                                                   </tr>
                                              <tr>
                                                       <td>
                                                       Monthly Premium
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtMonthlyPrem" runat="server"></asp:TextBox>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                       Certificate No 
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtCertificateNo" runat="server"></asp:TextBox>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                       Issue Date
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtInsIssueDate" runat="server"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtInsIssueDate"
                                PopupButtonID="txtInsIssueDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                       Expiry Date
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtInsExpDate" runat="server"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtInsExpDate"
                                PopupButtonID="txtInsExpDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td>
                                                       Remarks
                                                       </td>
                                                       <td>
                                                       <asp:TextBox ID="txtInsRemarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                       
                                                       </td>
                                                   </tr>
                                                  
                                                   <tr>
                                                       <td>
                                                           
                                                       </td>
                                                       <td>
                                                           <asp:Button ID="btnInsurance" runat="server" Text="Save" OnClick="btnInsurancet_Click"  CssClass="savebutton btn btn-success"  OnClientClick="javascript:return ValidInsurance();"/>
                                                       </td>
                                                   </tr>
                                              </table>

                                        </asp:Panel>
                                    </ContentTemplate>
                                </cc1:TabPanel>

                               
                                </cc1:TabContainer>
                             </asp:Panel>

                    </td>
                </tr>
             <tr>
                 <td>
                     <div id="dvEditDocs" runat="server" visible="false">
                         <table cellpadding="2" cellspacing="0" border="0" width="100%">
                             <tr>
                                 <td>
                                     <cc1:TabContainer ID="ShowDocs" runat="server" ActiveTabIndex="0" Width="950px">

                                         <cc1:TabPanel ID="TabPaasport" HeaderText="PassPort Details view" runat="server">
                                             <ContentTemplate>
                                                 <asp:Panel ID="PanelPassPort" runat="server" CssClass="DivBorderOlive" Width="950px">
                                                     <div id="dvPassport" runat="server">
                                                         <table width="100%">
                                                             <tr>
                                                                 <td>
                                                                     <asp:GridView ID="GVPassPort" runat="server" CssClass="gridview" AutoGenerateColumns="false"
                                                            AlternatingRowStyle-BackColor="GhostWhite" EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="GVPassPort_RowCommand" HeaderStyle-CssClass="tableHead" Width="100%">
                                                                         <Columns>
                                                                             <asp:TemplateField HeaderText="PassportNumber">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblPassPortNumber" runat="server" Text='<%#Eval("PassportNo") %>'></asp:Label>
                                                                                 </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Issuer">
                                                                                   <ItemTemplate>
                                                                                      <asp:Label ID="lblIssuer" runat="server" Text='<%#Eval("Issuer") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Place of Issue">
                                                                                   <ItemTemplate>
                                                                                        <asp:Label ID="lblPlace" runat="server" Text='<%#Eval("IssuePlace") %>'></asp:Label>
                                                                                   </ItemTemplate>
                                                                                  </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Issue Date">
                                                                                   <ItemTemplate>
                                                                                        <asp:Label ID="lblIssueDate" runat="server" Text='<%#Eval("IssueDate") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Expiry Date">
                                                                                   <ItemTemplate>
                                                                                        <asp:Label ID="lblExpiryDate" runat="server" Text='<%#Eval("ExpiryDate") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Remarks">
                                                                                   <ItemTemplate>
                                                                                        <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("PasportRemarks") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                             </asp:TemplateField>

                                                                        

                                                                              <asp:TemplateField>
                                                                             <ItemTemplate>
                                                                              <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd" Text="Edit" CommandName="Edt"></asp:LinkButton>
                                                                             </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                         </Columns>
                                                                     </asp:GridView>
                                                                 </td>
                                                             </tr>
                                                         </table>
                                                     </div>
                                                 </asp:Panel>
                                             </ContentTemplate>
                                         </cc1:TabPanel>

                                         <cc1:TabPanel ID="TabEmergency" HeaderText="Emergency Details view" runat="server">
                                             <ContentTemplate>

                                                 <asp:Panel ID="PanelEmergency" runat="server" CssClass="DivBorderOlive">
                                                     <div id="dvEmergency" runat="server">
                                                         <table width="100%">
                                                             <tr>
                                                                <td>
                                                                    <asp:GridView ID="GVEmergency" runat="server" CssClass="gridview" AutoGenerateColumns="false"
                                                            AlternatingRowStyle-BackColor="GhostWhite" EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="GVEmergency_RowCommand" HeaderStyle-CssClass="tableHead" Width="100%">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Emergency ContactName">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblEmergContactName" runat="server" Text='<%#Eval("EmergContactName") %>'></asp:Label>
                                                                                 </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Relation">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblRelation" runat="server" Text='<%#Eval("EmergRelation") %>'></asp:Label>
                                                                                 </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Email">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("EmergEmail") %>'></asp:Label>
                                                                                 </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Contact Mobile">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblContactMobile" runat="server" Text='<%#Eval("EmergContact") %>'></asp:Label>
                                                                                 </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Contact Phone">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblContactPhone" runat="server" Text='<%#Eval("EmergResiPhone") %>'></asp:Label>
                                                                                 </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Origin Contact Name">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblOriginContactName" runat="server" Text='<%#Eval("OREmergContactName") %>'></asp:Label>
                                                                                 </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Relation">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblRelatn" runat="server" Text='<%#Eval("OREmergRelation") %>'></asp:Label>
                                                                                 </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText=" Email">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblemailOrgin" runat="server" Text='<%#Eval("OREmergEmail") %>'></asp:Label>
                                                                                 </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Contact Mobile">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblContactMobileOR" runat="server" Text='<%#Eval("OREmergContact") %>'></asp:Label>
                                                                                 </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                               <asp:TemplateField HeaderText="Contact Phone">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblContactPhoneOR" runat="server" Text='<%#Eval("OREmergResiPhone") %>'></asp:Label>
                                                                                 </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                              <asp:TemplateField>
                                                                             <ItemTemplate>
                                                                              <asp:LinkButton ID="lnkEdit" runat="server" CssClass="anchor__grd edit_grd" Text="Edit" CommandName="Edt"></asp:LinkButton>
                                                                             </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                             </tr>
                                                         </table>

                                                     </div>
                                                 </asp:Panel>
                                             </ContentTemplate>
                                         </cc1:TabPanel> 

                                         <cc1:TabPanel ID="TabInsurance" HeaderText="Isurance Details view"  runat="server">
                                             <ContentTemplate>
                                                 <asp:Panel ID="PanelInsurance" runat="server" CssClass="DivBorderOlive">
                                                    <div id="dvInsurance" runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="GVInsurance" runat="server" CssClass="gridview" AutoGenerateColumns="false"
                                                            AlternatingRowStyle-BackColor="GhostWhite" EmptyDataRowStyle-CssClass="EmptyRowData" OnRowCommand="GVInsurance_RowCommand" HeaderStyle-CssClass="tableHead" Width="100%">

                                                                        <Columns>
                                                                             <asp:TemplateField HeaderText="Policy number ">
                                                                                   <ItemTemplate>
                                                                                        <asp:Label ID="lblPolicynumber" runat="server" Text='<%#Eval("PolicyNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Monthly Premium">
                                                                                   <ItemTemplate>
                                                                                        <asp:Label ID="lblMonthlyPremium" runat="server" Text='<%#Eval("MonthlyPremium") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Certificate No">
                                                                                   <ItemTemplate>
                                                                                        <asp:Label ID="lblCertificateNo" runat="server" Text='<%#Eval("CertificateNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Issue Date ">
                                                                                   <ItemTemplate>
                                                                                        <asp:Label ID="lblIssueDate" runat="server" Text='<%#Eval("IssueDate") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Expiry Date">
                                                                                   <ItemTemplate>
                                                                                        <asp:Label ID="lblExpiryDate" runat="server" Text='<%#Eval("ExpiryDate") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Remarks">
                                                                                   <ItemTemplate>
                                                                                        <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                              <asp:TemplateField>
                                                                             <ItemTemplate>
                                                                              <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edt"></asp:LinkButton>
                                                                             </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                 </asp:Panel>
                                             </ContentTemplate>
                                         </cc1:TabPanel>
                                         

                                     </cc1:TabContainer>
                                 </td>
                             </tr>
                         </table>
                     </div>
                 </td>
             </tr>
               </table>


</asp:Content>

