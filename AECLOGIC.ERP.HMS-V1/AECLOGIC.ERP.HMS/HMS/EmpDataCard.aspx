<%@ Page Title="EmployeeDataCard" Language="C#"   AutoEventWireup="True" CodeBehind="EmpDataCard.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpDataCard" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
 <script language="javascript" type="text/javascript">
        //Check Number
        function chkNumber(object, msg, isRequired, waterMark) 
        {
            var elm = getObj(object);
            var val = elm.value;
            if (isRequired) {
                if (!Reval(object, msg, waterMark))
                { return false; } 
            }
            if (val != '') {
                var rx = new RegExp("[0-9]*");
                var matches = rx.exec(val);
                if (matches == null || val != matches[0]) {
                    alert(msg + " can take numbers only!!!");
                    //elm.value='';
                    elm.focus();
                    return false;
                }
            } return true;
        }

        //For Dropdown list
        function chkDropDownList(object, msg) {

            var elm = getObj(object);

            if (elm.selectedIndex == 0) {
                alert("Select " + msg + "!!!");
                elm.focus();
                return false;
            } return true;
        }

        function getObj(the_id)
        {
            if (typeof (the_id) == "object") {
                return the_id;
            }
            if (typeof document.getElementById != 'undefined') {
                return document.getElementById(the_id);
            } else if (typeof document.all != 'undefined') {
                return document.all[the_id];
            } else if (typeof document.layers != 'undefined') {
                return document.layers[the_id];
            } else {
                return null;
            }
        }
        //Required Validation
        function Reval(object, msg, waterMark)
        
         {
            var elm = getObj(object);
            var val = elm.value;
            if (val == '' || val.length == 0 || val == waterMark) {
                alert(msg + " should not be empty!!! ");
                //elm.value = waterMark;
                elm.focus();
                return false;
            }
            return true;
        }

        //For  CheckBox

        function SelectAll(CheckBox) {
            TotalChkBx = parseInt('<%= this.gveditkbipl.Rows.Count %>');
            var TargetBaseControl = document.getElementById('<%= this.gveditkbipl.ClientID %>');
            var TargetChildControl = "chkSelect";
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            for (var iCount = 0; iCount < Inputs.length; ++iCount) {
                if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[iCount].checked = CheckBox.checked;
            }
        }
        function SelectDeSelectHeader(CheckBox) {
            TotalChkBx = parseInt('<%= this.gveditkbipl.Rows.Count %>');
            var TargetBaseControl = document.getElementById('<%= this.gveditkbipl.ClientID %>');
            var TargetChildControl = "chkSelect";
            var TargetHeaderControl = "chkSelectAll";
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            var flag = false;
            var HeaderCheckBox;
            for (var iCount = 0; iCount < Inputs.length; ++iCount) {
                if (Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetHeaderControl, 0) >= 0)
                    HeaderCheckBox = Inputs[iCount];
                if (Inputs[iCount] != CheckBox && Inputs[iCount].type == 'checkbox' && Inputs[iCount].id.indexOf(TargetChildControl, 0) >= 0 && Inputs[iCount].id.indexOf(TargetHeaderControl, 0) == -1) {
                    if (CheckBox.checked) {
                        if (!Inputs[iCount].checked) {
                            flag = false;
                            HeaderCheckBox.checked = false;
                            return;
                        }
                        else
                            flag = true;
                    }
                    else if (!CheckBox.checked)
                        HeaderCheckBox.checked = false;
                }
            }
            if (flag)
                HeaderCheckBox.checked = CheckBox.checked
        }

    </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
    <tr><td> <AEC:Topmenu ID="topmenu" runat="server" /></td></tr>
       <%-- <tr>
            <td class="pageheader" colspan="2">
               
            </td>
        </tr>--%>
       <%-- <tr>
            <td style="width: 100%;" colspan="2">
                <a class="linksunselected" href="CreateEmployee.aspx">Add</a> |
                <a href="#" class="lnkselected">Edit</a> </br></br>
            </td>
        </tr>--%>
        <tr>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            Worksite
                            <asp:DropDownList ID="ddlworksites" runat="server"  CssClass="droplist" >
                            </asp:DropDownList>
                            &nbsp;&nbsp;Department
                            <asp:DropDownList ID="ddldepartments"  CssClass="droplist" runat="server">
                            </asp:DropDownList>
                            &nbsp;&nbsp;Filter Employee
                            <asp:TextBox ID="txtusername" runat="server" MaxLength="50" OnTextChanged="btnSearch_Click"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                CssClass="savebutton" Width="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gveditkbipl" runat="server" AutoGenerateColumns="False" 
                                CssClass="gridview" DataKeyNames="EmpId" 
                                EmptyDataRowStyle-CssClass="EmptyRowData" EmptyDataText="No Records Found" 
                                OnRowCommand="GridView1_RowCommand" OnRowDataBound="gveditkbipl_RowDataBound" 
                                OnRowDeleting="gveditkbipl_RowDeleting" onrowediting="gveditkbipl_RowEditing">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle />
                                        <HeaderStyle />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="SelectAll(this);" 
                                                Text="All" />
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EmpID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpId" runat="server" Text='<%#Bind("EmpId")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" 
                                                Text='<%#String.Format("{0} {1} {2}", DataBinder.Eval(Container.DataItem, "FName"), DataBinder.Eval(Container.DataItem, "MName"), DataBinder.Eval(Container.DataItem, "LName")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Design" HeaderStyle-HorizontalAlign="Left" 
                                        HeaderText="Designation" />
                                    <asp:BoundField DataField="Category" HeaderStyle-HorizontalAlign="Left" 
                                        HeaderText="Trades" />
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" runat="server" 
                                                Text='<%# GetDepartment(DataBinder.Eval(Container.DataItem, "DeptNo").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Worksite">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorksite" runat="server" 
                                                Text='<%# GetWorkSite(DataBinder.Eval(Container.DataItem, "Categary").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" 
                                        HeaderText="Company Mobile">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMobile2" runat="server" Text='<%#Eval("Mobile2")%>' 
                                                Width="120px"></asp:TextBox>
                                           <%-- <asp:Label ID="lblMobile2" runat="server" Text='<%# Eval("Mobile2")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderText="Amount Limit">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAmountLimit" runat="server" Text='<%#Eval("AmountLimit")%>' 
                                                Width="120px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" 
                                                CommandArgument='<%#Eval("EmpId")%>' CommandName="Edt" Text="Update"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 17px">
                            <uc1:Paging ID="EmpListPaging"  runat="server" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                                        <td>
                                            <asp:Button ID="btnUpdateAll" runat="server" CssClass="savebutton" 
                                                onclick="btnUpdateAll_Click" Text="Update Checked Accounts" Width="190px" />
                                        </td>
                                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>


