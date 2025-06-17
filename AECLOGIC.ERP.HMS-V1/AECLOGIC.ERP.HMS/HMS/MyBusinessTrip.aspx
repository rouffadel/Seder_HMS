<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyBusinessTrip.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.MyBusinessTrip"  %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
     <style>
        .MyCalendar .ajax__calendar_container {
            background-color: White;
            color: black;
            border: 1px solid #646464;
        }

            .MyCalendar .ajax__calendar_container td {
                background-color: White;
                padding: 0px;
            }
    </style>
    <script language="javascript" type="text/javascript">

        function SelectAll(hChkBox, grid, tCtrl) { var oGrid = document.getElementById(grid); var IPs = oGrid.getElementsByTagName("input"); for (var iCount = 0; iCount < IPs.length; ++iCount) { if (IPs[iCount].type == 'checkbox' && IPs[iCount].id.indexOf(tCtrl, 0) >= 0) IPs[iCount].checked = hChkBox.checked; } }
        function CheckItem(ctrl, ID) {

            var ResultVal = AjaxDAL.HR_IsVerified(ctrl.checked, ID);
            if (ctrl.checked == true) {
                alert("Checked");
            }
            else {
                alert("UnChecked");
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
       
        //chaitanya:for validation below code
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
    <table id="tblAdd" visible="false" runat="server" width="100%">
    
        <tr>
            <td colspan="2">
                <b>Reimburse Items</b>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:ListBox ID="lstItems" Rows="6" Width="300Px" runat="server" SelectionMode="Multiple">
                </asp:ListBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td style="padding-left: 160Px">
                <asp:Button ID="btnSave" ToolTip="Add multiple Items at once" CssClass="btn btn-success"
                    runat="server" Text="Add" OnClick="btnSave_Click" AccessKey="s" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr> 


        <tr>
            <td>
                <asp:GridView ID="gvRemiItems" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                    Width="90%" OnRowCommand="gvRemiItems_RowCommand" CssClass="gridview">
                    <Columns>
                        <asp:TemplateField Visible="false" HeaderText="Sl.No">
                            <ItemTemplate>
                                <asp:Label ID="lblID" Text='<%#Eval("ID")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="30" />
                            <HeaderStyle HorizontalAlign="Left" Width="30" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sl.No">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="30" />
                            <HeaderStyle HorizontalAlign="Left" Width="30" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EmpID">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpID" Text='<%#Eval("EmpID")%>' runat="server"></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="80Px" HeaderText="ReimburseItem">
                            <ItemTemplate>
                                <asp:Label ID="lblRItem" Text='<%#Eval("RItem")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="30" />
                            <HeaderStyle HorizontalAlign="Left" Width="30" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Units of Measure">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlunits" DataSource='<%#GetAUDataSet() %>' DataTextField="AU_Name"
                                    DataValueField="AU_Id" SelectedIndex='<%#GetAUIndex(Eval("AUID").ToString())%>'
                                    runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Rate">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRate" Text='<%#Eval("UnitRate") %>' OnTextChanged="txtUnitrete_TextChanged"
                                    AutoPostBack="true" Width="40" runat="server"></asp:TextBox>
                                         <cc1:FilteredTextBoxExtender FilterMode="ValidChars" FilterType="Numbers,Custom"
                                    ID="Fl5" runat="server" TargetControlID="txtRate" ValidChars="." /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQty" runat="server" Text='<%#Eval("Qty") %>' Style="text-align: right;"
                                    OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" Width="40"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender FilterMode="ValidChars" FilterType="Numbers,Custom"
                                    ID="Fl6" runat="server" TargetControlID="txtQty" ValidChars="." />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                               <%-- <asp:Label ID="txtAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>--%>
                                <asp:Textbox ID="txtAmount" Text='<%#Eval("Amount")%>' AutoPostBack="true" Width="60" runat="server"></asp:Textbox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="100Px" HeaderText="Purpose">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPurpose" runat="server" Height="40" Text='<%# DataBinder.Eval(Container.DataItem,"Purpose") %>'
                                    TextMode="MultiLine" Width="200"></asp:TextBox><cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2"
                                        runat="server" TargetControlID="txtPurpose" WatermarkCssClass="Watermarktxtbox"
                                        WatermarkText="[Enter Purpose Here..]">
                                    </cc1:TextBoxWatermarkExtender>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblRItemNo" runat="server" Text='<%#Eval("RItemID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Spent On">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSpentOn" Text='<%#Eval("DateOfSpent")%>' runat="server" Width="70" AccessKey="t" ToolTip="[Alt+t OR Alt+t+Enter]"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtSpentOn" CssClass="MyCalendar"
                                    OnClientDateSelectionChanged="checkDate" TargetControlID="txtSpentOn" Format="dd MMM yyyy">
                                </cc1:CalendarExtender>
                               <%-- <cc1:FilteredTextBoxExtender FilterMode="ValidChars" FilterType="Numbers,Custom"
                                    ID="Fl7" runat="server" TargetControlID="txtSpentOn" ValidChars="/" />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Proof">
                            <ItemTemplate>
                                <asp:FileUpload ID="UploadProof" width="180px" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnlDelete" CommandName="Del" CommandArgument='<%#Eval("ID")%>'
                                    runat="server" CssClass="anchor__grd dlt">Delete</asp:LinkButton></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 200Px">
                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" OnClientClick="javascript:return CheckUnitSelect();"
                    CssClass="btn btn-success" Text="Save" />
            </td>
        </tr>
        <tr>
            <td style="padding-left: 200Px">
             <br />
            </td>
        </tr>
        <tr>
            <td style="padding-left: 200Px">
              <br />
            </td>
        </tr>
        <tr>
            <td style="padding-left: 200Px">
              <br />
            </td>
        </tr>
        <tr>
            <td style="padding-left: 200Px">
              <br />
                <br />

            </td>
        </tr>
    </table>
    </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle" ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>