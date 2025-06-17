<%@ Page Title="" Language="C#"  AutoEventWireup="True"
    CodeBehind="EmpMessAttendance.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpMessAttendance" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("Select a day earlier than today!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
        function SelectAll(CheckBox) {
            TotalChkBx = parseInt('<%= this.grdEmpMessAtt.Rows.Count %>');
            var TargetBaseControl = document.getElementById('<%= this.grdEmpMessAtt.ClientID %>');
            var TargetChildControl = "chkAll";
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            for (var iCount = 0; iCount < Inputs.length; ++iCount) {
                if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[iCount].checked = CheckBox.checked;
            }
        }
        function validateCheckBox() {
                var isValid = false;
                var gvChk = document.getElementById('<%= grdEmpMessAtt.ClientID %>');
                for (var i = 1; i < gvChk.rows.length; i++) {
                    var chkInput = gvChk.rows[i].getElementsByTagName('input');
                    if (chkInput != null) {
                        if (chkInput[0].type == "checkbox") {
                            if (chkInput[0].checked) {
                                isValid = true;
                                return true;
                            }
                        }
                    }
                }
                alert("Please select atleast one Employee.");
                return false;
        }
    </script>
    <table width="100%">
       
        <tr>
            <td>
                <table width="100%" id="tblSearch">
                    <tr>
                        <td>
                            <cc1:Accordion ID="LstOfHolidayConAccordion" runat="server" HeaderCssClass="accordionHeader"
                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%">
                                <Panes>
                                    <cc1:AccordionPane ID="LstOfHolidayConAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                        ContentCssClass="accordionContent">
                                        <Header>
                                            Search Criteria</Header>
                                        <Content>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td style="width: 175px">
                                                        <b>
                                                            <asp:Label ID="lblWorksite" runat="server" Text="Worksite"></asp:Label>:</b>
                                                        &nbsp;<asp:DropDownList ID="ddlWorkSite" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWorkSite_SelectedIndexChanged" CssClass="droplist">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 250px">
                                                        <b>
                                                            <asp:Label ID="lblDept" runat="server" Text="Department"></asp:Label>:</b> &nbsp;<asp:DropDownList
                                                                ID="ddlDept" runat="server" CssClass="droplist">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 100px">
                                                        <b>
                                                            <asp:Label ID="lblNature" runat="server" Text="Emp Nature"></asp:Label>:</b>
                                                    </td>
                                                    <td style="width: 100px">
                                                        <asp:DropDownList ID="ddlEmpNature" runat="server" CssClass="droplist">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 50px">
                                                        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txtdateTxtExt" runat="server" FilterType="Custom,Numbers"
                                                            TargetControlID="txtDate" ValidChars="/" />
                                                        <cc1:CalendarExtender ID="caltxtDate" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy"
                                                            OnClientDateSelectionChanged="checkDate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="savebutton" OnClick="btnSearch_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBoxList ID="ChkLstAll" runat="server" RepeatDirection="Horizontal">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </Content>
                                    </cc1:AccordionPane>
                                </Panes>
                            </cc1:Accordion>
                        </td>
                    </tr>
                </table>
              
                <table width="100%" id="tblgrid">
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="grdEmpMessAtt" runat="server" CssClass="gridview" EmptyDataText="No Records Found"
                                AutoGenerateColumns="false" OnRowDataBound="grdEmpMessAtt_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="EmpID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("EmpId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="SelectAll(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EmpName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBoxList ID="chkMessTypes" runat="server" DataTextField="Name" DataValueField="MID"
                                                OnSelectedIndexChanged="chkMessTypes_SelectedIndexChanged" AutoPostBack="true"
                                                RepeatDirection="Horizontal" DataSource='<%# BindEmpAttendanceType()%>'>
                                            </asp:CheckBoxList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWS" runat="server" Text='<%#Eval("WS")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnAll" runat="server" Text="Submit" CssClass="savebutton" 
                                OnClientClick="javascript:return validateCheckBox();" onclick="btnAll_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 17px">
                            <uc1:Paging ID="EmpMessAttPaging" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
