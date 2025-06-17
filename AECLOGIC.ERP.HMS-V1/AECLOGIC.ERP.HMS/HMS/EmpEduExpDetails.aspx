<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="EmpEduExpDetails.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpEduExpDetails" %>
<%--<%@ PreviousPageType VirtualPath="~/EmployeeList.aspx" %>--%>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JS/common.js"></script>
    <script type="text/javascript" language="javascript">
        function ValidQualification() {

            if (!chkName('<%=txtQuli.ClientID%>', "Qualification", true, "")) {
                return false;
            }
            if (!chkName('<%=txtInstitute.ClientID%>', "Institute", true, "")) {
                return false;
            }
            if (!chkName('<%=txtSpecilization.ClientID%>', "Specilization", true, "")) {
                return false;
            }
            if (!chkFloatNumber('<%=txtPercentage.ClientID%>', "Percentage", true, "")) {
                return false;
            }
        }
        function ValidExpDetails() {

            if (document.getElementById('<%=txtOrg.ClientID%>').value == "") {
                alert("Please Enter Organization");
                document.getElementById('<%=txtOrg.ClientID%>').focus();
                return false;
            }
            <%--if (!chkName('<%=txtDesig.ClientID%>', "Designation", true, "")) {
                return false;}--%>
  if (document.getElementById('<%=txtDesig.ClientID%>').value == "") {
                alert("Please Enter DesigNation");
                document.getElementById('<%=txtDesig.ClientID%>').focus();
                return false;
            
            }
            if (!chkFloatNumber('<%=txtCTC.ClientID%>', "CTC", true, "")) {
                return false;
            }
            if (!chkDropDownList('<%=ddlCity.ClientID%>', "City")) {
                return false;
            }
            if (document.getElementById('<%=txtFromDate.ClientID%>').value == "") {
                alert("Please Enter Fromdate");
                document.getElementById('<%=txtFromDate.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtToDate.ClientID%>').value == "") {
                alert("Please Enter Todate");
                document.getElementById('<%=txtToDate.ClientID%>').focus();
                return false;
            }
        }
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("Select a day earlier than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>
   
    <table>
    <tr>
    <td>
    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="savebutton btn btn-primary" 
            onclick="btnBack_Click" />
    </td>
    </tr>
        <tr>
            <td class="pageheader">
                Employee Education and Experiance Details
                <br />
                <asp:LinkButton ID="lnkAdd" runat="server" Text="Add" OnClick="lnkAdd_Click"></asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="lnkEdit_Click"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <div id="dvAdd" runat="server" visible="false">
        <table cellpadding="2" cellspacing="0" border="0" width="100%"  >
            <tr>
                <td>
                   
                        <cc1:TabContainer ID="tb" runat="server" ActiveTabIndex="2" AutoPostBack="false"
                            Width="750px">
                            <cc1:TabPanel runat="server" HeaderText=" Qualification Details" ID="tabPerDetails"
                                Enabled="true">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel5" runat="server" CssClass="DivBorderOlive">
                                        <table>
                                            <tr>
                                                <td style="width: 65%">
                                                    <asp:Label ID="lblQualification" runat="server" Text="Qualification"></asp:Label>
                                                    <span style="color: #ff0000">*</span>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQuli" runat="server"></asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="txtwmextQui" runat="server" TargetControlID="txtQuli"
                                                        WatermarkText="[Enter Qualification]">
                                                    </cc1:TextBoxWatermarkExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 65%">
                                                    <asp:Label ID="lblInstitute" runat="server" Text="Institute"></asp:Label>
                                                    <span style="color: #ff0000">*</span>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInstitute" runat="server"></asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="txtwmextInstitute" runat="server" TargetControlID="txtInstitute"
                                                        WatermarkText="[Enter Institute]">
                                                    </cc1:TextBoxWatermarkExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblYOP" runat="server" Text="YOP"></asp:Label>
                                                    <span style="color: #ff0000">*</span>:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlYOP" runat="server" CssClass="droplist">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSpecilization" runat="server" Text="Specilization"></asp:Label>
                                                    <span style="color: #ff0000">*</span>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSpecilization" runat="server"></asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="txtwmextSpeci" runat="server" TargetControlID="txtSpecilization"
                                                        WatermarkText="[Enter Specilization]">
                                                    </cc1:TextBoxWatermarkExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPercentage" runat="server" Text="Percentage(%)"></asp:Label>
                                                    <span style="color: #ff0000">*</span>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPercentage" runat="server"></asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="txtwmextPer" runat="server" TargetControlID="txtPercentage"
                                                        WatermarkText="[Enter Percentage]">
                                                    </cc1:TextBoxWatermarkExtender>
                                                </td>
                                            </tr>
                                            <td>
                                                <asp:Label ID="lblEduType" runat="server" Text="Education Type"></asp:Label>:
                                                <%--<span style="color: #ff0000">*</span>--%>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlFullPartTime" CssClass="droplist" runat="server">
                                                    <asp:ListItem Selected="True" Value="1">Full Time</asp:ListItem>
                                                    <asp:ListItem Value="2">Part Time</asp:ListItem>
                                                    <asp:ListItem Value="3">Correspondence</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton btn btn-success" Text="Submit" OnClick="btnSubmit_Click"
                                                        OnClientClick="javascript:return ValidQualification();" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel runat="server" HeaderText=" Experiance Details" ID="TabPanel1" Enabled="true">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server" CssClass="DivBorderOlive">
                                        <table>
                                            <tr>
                                                <td style="width: 65%">
                                                    <asp:Label ID="lblOrg" runat="server" Text="Organization"></asp:Label>
                                                    <span style="color: #ff0000">*</span>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOrg" runat="server"></asp:TextBox>
                                                 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDesigNation" runat="server" Text="Designation"></asp:Label>
                                                    <span style="color: #ff0000">*</span>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDesig" runat="server"></asp:TextBox>
                                                  
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCTC" runat="server" Text="CTC"></asp:Label>
                                                    <span style="color: #ff0000">*</span>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCTC" runat="server"></asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="txtWMExtCTC" runat="server" TargetControlID="txtCTC"
                                                        WatermarkText="[Enter CTC]">
                                                    </cc1:TextBoxWatermarkExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                                                    <span style="color: #ff0000">*</span>:
                                                </td>
                                                <td>
                                                <asp:DropDownList ID="ddlCity" runat="server" ></asp:DropDownList>
                                                  
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblType" runat="server" Text="Type"></asp:Label>:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPermanent" runat="server" CssClass="droplist" TabIndex="35">
                                                        <asp:ListItem Selected="True" Value="1">Permanent</asp:ListItem>
                                                        <asp:ListItem Value="2">Contract</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFromDate" runat="server" Text="From Date"></asp:Label>
                                                    <span style="color: #ff0000">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox><cc1:CalendarExtender
                                                        ID="CalendarExtender1" runat="server" OnClientDateSelectionChanged="checkDate"
                                                        TargetControlID="txtFromDate" Format="dd/MM/yyyy" Enabled="True">
                                                    </cc1:CalendarExtender>
                                                    <cc1:FilteredTextBoxExtender FilterType="Custom, Numbers" ID="FilteredTextBoxExtender1" runat="server"
                                                    TargetControlID="txtFromDate" ValidChars="/" Enabled="True">
                                                </cc1:FilteredTextBoxExtender>
                                                  
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTodate" runat="server" Text="To Date"></asp:Label>
                                                    <span style="color: #ff0000">*</span>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender2" OnClientDateSelectionChanged="checkDate"
                                                        runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" Enabled="True">
                                                    </cc1:CalendarExtender>
                                                     <cc1:FilteredTextBoxExtender FilterType="Custom, Numbers" ID="Fl4" runat="server"
                                                    TargetControlID="txtToDate" ValidChars="/" Enabled="True">
                                                </cc1:FilteredTextBoxExtender>
                                                 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnExpSubmit" runat="server" Text="Submit" CssClass="savebutton btn btn-success"
                                                        OnClick="btnExpSubmit_Click" OnClientClick="javascript:return ValidExpDetails();" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                   <%-- </asp:Panel>--%>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvEditExp" runat="server" visible="false">
        <table cellpadding="2" cellspacing="0" border="0" width="100%">
            <tr>
                <td>
                  
                        <cc1:TabContainer ID="EmpQuaExpTab" runat="server" ActiveTabIndex="2"   Width="750px" >
                           
                            <cc1:TabPanel runat="server" HeaderText=" Qualification Details view" ID="TabPanel2">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel3" runat="server" CssClass="DivBorderOlive">
                                        <div id="dvQuagrd" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="grdQualifi" runat="server" CssClass="gridview" AutoGenerateColumns="false"
                                                            AlternatingRowStyle-BackColor="GhostWhite" EmptyDataRowStyle-CssClass="EmptyRowData"
                                                            OnRowCommand="grdQualifi_RowCommand" HeaderStyle-CssClass="tableHead" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="EduID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQEduID" runat="server" Text='<%#Eval("EduID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="EmpID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQEmpID" runat="server" Text='<%#Eval("EmpID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="EmpName">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQEmpName" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qualification">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQQua" runat="server" Text='<%#Eval("Qualification") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Specilization">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQSpeci" runat="server" Text='<%#Eval("Specialization") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Institute">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQIns" runat="server" Text='<%#Eval("Institute") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQMode" runat="server" Text='<%#Eval("Mode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Percentage">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQPer" runat="server" Text='<%#Eval("Percentage") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="YOP">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQYOP" runat="server" Text='<%#Eval("YOP") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edt" CommandArgument='<%#Eval("EduID") %>'></asp:LinkButton>
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
                            <cc1:TabPanel runat="server" HeaderText=" Experiance Details view" ID="TabPanel3"
                                Enabled="true">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel4" runat="server" CssClass="DivBorderOlive">
                                        <div id="dvExpgrd" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="grdEmpExp" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="GhostWhite"
                                                            CssClass="gridview" EmptyDataText="No Records Found" HeaderStyle-CssClass="tableHead"
                                                            OnRowCommand="grdEmpExp_RowCommand" EmptyDataRowStyle-CssClass="EmptyRowData"
                                                            Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="EmpID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("EmpID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="EmpName">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Organization">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpOrg" runat="server" Text='<%#Eval("Organization") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Designation">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmpDesig" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("type") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="City">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCity" runat="server" Text='<%#Eval("CItyName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="FromDate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFrmDate" runat="server" Text='<%#Eval("FromDate") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ToDate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblToDate" runat="server" Text='<%#Eval("ToDate") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edt" CommandArgument='<%#Eval("ExpID") %>'></asp:LinkButton>
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
                    <%--</asp:Panel>--%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
