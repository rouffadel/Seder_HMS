<%@ Page Language="C#"   AutoEventWireup="True" ValidateRequest="false"
    CodeBehind="DirectPO.aspx.cs" Inherits="AECLOGIC.ERP.HMS.DirectPO" MaintainScrollPositionOnPostback="true"
    Title="DirectPO" %>
<%@ Register Src="~/Templates/UploadControl.ascx" tagname="UploadControl" tagprefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">


        function SelectAll(id) {
            var frm = document.getElementById("<%=GVTERMS.ClientID %>");
            for (i = 0; i < frm.rows.length; i++) {
                cell = frm.rows[i].cells[0];
                //loop according to the number of childNodes in the cell
                for (j = 0; j < cell.childNodes.length; j++) {
                    //if childNode type is CheckBox                 
                    if (cell.childNodes[j].type == "checkbox") {
                        //assign the status of the Select All checkbox to the cell checkbox within the grid
                        cell.childNodes[j].checked = document.getElementById(id).checked;
                    }
                }
            }
        }  
    </script>
    
    <asp:UpdatePanel ID="u11" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%; vertical-align: top">
               
                <tr>
                    <td>
                        <table border="0" cellpadding="1" cellspacing="2" style="width: 100%">
                          
                            <tr>
                                <td align="left">
                                    <asp:UpdatePanel ID="updWS" runat="server">
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table>
                                <tr class="pageheader"><td colspan="3">New WO for Hire Accommodation</td></tr>         
                            <tr>
                                <td style="vertical-align: top; text-align: left; width: 257px;">
                                   <b>Indent Reference </b> <span style="color: #FF0000"><sup class="Must">*</sup></span>
                               <%-- </td>
                                <td style="vertical-align: top; text-align: left">--%>
                                   
                                </td>
                                <td style="width:650px">
                                
                                
                                    <asp:TextBox ID="txtReqNo" runat="server" MaxLength="100" Width="344px"></asp:TextBox>
                                 &nbsp;(Example: Hiring Accommodation)
                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtReqNo"
                                        WatermarkCssClass="watermark" WatermarkText="[Enter Indent Quick Reference]">
                                    </cc1:TextBoxWatermarkExtender>
                                
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left; width: 257px;">
                                 <b>PO For</b> <span style="color: #FF0000"><sup class="Must">*</sup></span>  </td>
                                  <td style="width:550px">
                                <asp:TextBox ID="txtpofor" runat="server" Width="344px" MaxLength="100"></asp:TextBox>
                              </td>
                            </tr>
                                                        
                                                            <tr>
                                                                <td style="vertical-align: top; text-align: left; width: 257px;">
                                                                    <b>For Project/Worksite/Cost Center</b> <span style="color: #FF0000"><sup class="Must">*</sup></span>
                                                                </td>
                                                                <td style="vertical-align: top; text-align: left">
                                                                    <asp:DropDownList ID="ddlForProject" runat="server" CssClass="droplist"  Width="250" AutoPostBack="True"
                                                                       >
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="10" style="width: 257px">
                                                                   <b>Services Group</b> </td><td>
                                                                    <asp:DropDownList ID="ddlService" CssClass="droplist"  runat="server" Width="250">
                                                                    <asp:ListItem Value="96" Text="Hiring Lands & Buildings"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="10" style="width: 257px">
                                                                    <b>Pupose</b></td><td>
                                                                    <asp:TextBox ID="txtPurpose" runat="server" BorderColor="#CC6600" 
                                                                        BorderStyle="Outset" Height="50px" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                    <td style="width: 257px">
                                                        <b>Payment Type<sup style="color: #FF0000">*
                                                        </sup></b>&nbsp;
                                                        
                                                    </td>
                                                    <td> <asp:DropDownList ID="ddlPayment" CssClass="droplist"  runat="server" Width="150px">
                                                        </asp:DropDownList></td>
                                                </tr>
                                                             <tr>
                 <td  
                     class="style3" style="width: 257px"><b>Required on&nbsp; </b></td><td>
                 <asp:TextBox ID="txtHLFromDay" runat="server"></asp:TextBox>  <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtHLFromDay" PopupButtonID="txtHLFromDay" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                                                                     * Default date is&nbsp; Today,Change if Required.</td></tr>
                                                            <tr>
                                                                <td style="vertical-align: top; text-align: left; width: 257px;" align="left">
                                                                    &nbsp;</td>
                                                                <td style="vertical-align: top; text-align: left">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            
                                                        </table>
                                                    </td>
                                                </tr>
                                              
                                               
                                              
                                               
                                               
                                               
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                               
                                <tr>
                                    <td style="width: 80%">
                                        Predefined Terms & Conditions Groups
                                    </td>
                                </tr>
                                <tr>
                                    <td bordercolor="#999999" style="border: 1px double #999999; padding-left: 10px;
                                        width: 80%;">
                                        <asp:RadioButtonList Font-Bold="true" ID="RbSetList" AutoPostBack="true" RepeatDirection="Vertical"
                                            runat="server" RepeatColumns="6" OnSelectedIndexChanged="RbSetList_SelectedIndexChanged">
                                            <asp:ListItem Text="Common Terms" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="WO Terms" Value="7"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 80%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 80%">
                                        <b>Upload quatations:</b>&nbsp;&nbsp;<asp:LinkButton ID="lnkHUpload" 
                                            runat="server" Font-Bold="True" 
                                            OnClientClick="javascript:return ShowUploadCtrl();">Upload Files</asp:LinkButton>
                                    </td>
                                </tr>
                               <%-- <tr>
                                    <td style="padding-left: 130px; width: 80%;">
                                        <asp:FileUpload ID="fup2" runat="server" />
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td style="width: 80%">
                                        <b>Terms &amp; Conditions:</b>
                                        <asp:GridView ID="GVTERMS" runat="server" Width="100%" HeaderStyle-CssClass="tableHead"
                                            AutoGenerateColumns="false" OnRowDataBound="GVTERMS_RowDataBound" CssClass="gridview">
                                            <RowStyle CssClass="gentext" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="cbSelectAll" runat="server" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" AutoPostBack="false" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="30" />
                                                    <ItemStyle Width="30" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Terms">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TXTTERMS" runat="server" Text='<%#Bind("Term")%>' Width="98%" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="termid" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblid" runat="server" Text='<%#Bind("Termid")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="3" style="width: 80%">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 80%">
                                        <asp:TextBox ID="TxtTerms" runat="server" Width="800" />
                                        <asp:Button ID="btnAddNewTerm" runat="server" Text="Add Terms" OnClick="btnAddNewTerm_Click"
                                            CssClass="savebutton" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 80%">
                                        <asp:GridView ID="GVAdditionalTerms" runat="server" Width="100%" HeaderStyle-CssClass="tableHead"
                                            AutoGenerateColumns="false" CssClass="gridview">
                                            <RowStyle CssClass="gentext" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Additional Terms">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TXTTERMS" runat="server" Text='<%#Bind("Remarks")%>' Width="98%" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="termid" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblid" runat="server" Text="0" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="10" style="width: 80%">
                                    </td>
                                </tr>
                                <tr>
                                    <td style=" padding-left:160Px">
                                        <asp:Button ID="btnSubmit" runat="server" Text=" Send " CssClass="savebutton">
                                        </asp:Button><br />
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td height="10" valign="top" style="width: 80%">
                                        Print:<asp:RadioButtonList ID="Rblist" runat="server" RepeatDirection="Horizontal"
                                            RepeatLayout="Flow">
                                            <asp:ListItem Text="Normal" Value="2" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Letter Head" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                        </table>
        </ContentTemplate>
        <Triggers>
<ajax:PostBackTrigger ControlID="btnSubmit"></ajax:PostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
     <uc1:UploadControl ID="UploadControl1" runat="server" />
</asp:Content>
