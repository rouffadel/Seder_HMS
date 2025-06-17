<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="EmpContributions.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpContributions" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript">
        function SelectAll(hChkBox, grid, tCtrl) { var oGrid = document.getElementById(grid); var IPs = oGrid.getElementsByTagName("input"); for (var iCount = 0; iCount < IPs.length; ++iCount) { if (IPs[iCount].type == 'checkbox' && IPs[iCount].id.indexOf(tCtrl, 0) >= 0) IPs[iCount].checked = hChkBox.checked; } }
    </script>
    <table width="100%">
       
        <tr>
            <td colspan="2">
                &nbsp;<b>Employee:</b>&nbsp;<asp:DropDownList ID="ddlEmp" runat="server" CssClass="droplist">
                </asp:DropDownList>
                &nbsp;&nbsp;<cc1:ListSearchExtender ID="ListSearchExtender1" IsSorted="true" PromptText="Type Here To Search"
                    PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                    TargetControlID="ddlEmp" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <b>Month:</b>&nbsp;
                <asp:DropDownList ID="ddlMonth" CssClass="droplist" runat="server" Height="16px">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <asp:ListItem Value="1">January</asp:ListItem>
                    <asp:ListItem Value="2">February</asp:ListItem>
                    <asp:ListItem Value="3">March</asp:ListItem>
                    <asp:ListItem Value="4">April</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">June</asp:ListItem>
                    <asp:ListItem Value="7">July</asp:ListItem>
                    <asp:ListItem Value="8">August</asp:ListItem>
                    <asp:ListItem Value="9">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
                <b>&nbsp;Year:</b>&nbsp;
                <asp:DropDownList ID="ddlYear" CssClass="droplist" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 18px">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <b>Item:</b>&nbsp;<asp:DropDownList ID="ddlItem" CssClass="droplist" runat="server">
                </asp:DropDownList>
                &nbsp;<b>UOM: </b>
                <asp:DropDownList ID="ddlUnit" CssClass="droplist" runat="server">
                </asp:DropDownList>
                &nbsp; <b>Quantity:</b>&nbsp;<asp:TextBox ID="txtQuantity" Width="50Px" runat="server"
                    AutoPostBack="True" OnTextChanged="txtQuantity_TextChanged"></asp:TextBox>
                <b>&nbsp; Unit Rate:</b>&nbsp;<asp:TextBox ID="txtRate" AutoPostBack="true" Width="80Px"
                    runat="server" OnTextChanged="txtRate_TextChanged"></asp:TextBox><b>&nbsp; Amount:&nbsp;
                    </b>
                <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style3" style="width: 91px">
                <b>Explanation:</b>
            </td>
            <td>
                <asp:TextBox ID="txtExplnation" Width="250Px" runat="server" Rows="4" TextMode="MultiLine"
                    BorderStyle="Outset" BorderColor="#CC6600"></asp:TextBox>
                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtExplnation"
                    WatermarkCssClass="Watermarktxtbox" WatermarkText="[Enter Explnation here..]">
                </cc1:TextBoxWatermarkExtender>
            </td>
        </tr>
        <tr>
            <td class="style3" style="width: 91px">
                <b>Upload Proof:</b>
            </td>
            <td>
                <asp:FileUpload ID="fileUpload" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style3" style="width: 91px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style3" style="width: 91px; height: 27px;">
            </td>
            <td style="height: 27px">
                &nbsp;<asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Text="Submit"
                    OnClick="btnSubmit_Click" />&nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" CssClass="savebutton" Text="Cancel" OnClick="btnCancel_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="tblSelectEmp" runat="server" width="100%">
                    <tr>
                        <td style="padding-left: 200Px">
                            <b>Select Employee:&nbsp;&nbsp; </b>
                            <asp:DropDownList ID="ddlSelectEmp" CssClass="droplist" runat="server">
                            </asp:DropDownList>
                            <cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search"
                                PromptPosition="Top" PromptCssClass="PromptText" QueryPattern="Contains" runat="server"
                                TargetControlID="ddlSelectEmp" />
                            &nbsp; <b>Enter EmpID:</b>&nbsp;
                            <asp:TextBox ID="txtEmp" runat="server"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnShow" runat="server" CssClass="savebutton" OnClick="btnShow_Click"
                                Text="Show" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvImburse" AutoGenerateColumns="false" Width="100%" runat="server"
                    ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                    HeaderStyle-CssClass="tableHead" OnRowCommand="gvImburse_RowCommand" 
                    OnRowDataBound="gvImburse_RowDataBound" CssClass="gridview">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAll" Text="Select All" runat="server" /></HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelectOne" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false" HeaderText="RID">
                            <ItemTemplate>
                                <asp:Label ID="lblRID" runat="server" Text='<%#Eval("RID")%>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EmpID">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("EmpID")%>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="EmpName" DataField="Name" />
                        <asp:BoundField HeaderText="Month" Visible="false" DataField="Month" />
                        <asp:BoundField HeaderText="Year" Visible="false" DataField="Year" />
                        <asp:TemplateField HeaderText="ReimburseType">
                            <ItemTemplate>
                                <asp:Label ID="lblReimburseType" runat="server" Text='<%#Eval("ReimburseType")%>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
                        <asp:BoundField HeaderText="UOM" DataField="UnitType" />
                        <asp:BoundField HeaderText="UnitRate" DataField="UnitRate" />
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Explanation" DataField="Explanation" />
                        <asp:BoundField HeaderText="Reported On" DataField="Date" />
                        <asp:BoundField Visible="false" DataField="Proof" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="hlnkView" NavigateUrl='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "Proof").ToString())%>'
                                    runat="server">Proof</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton CommandName="edt" CommandArgument='<%#Eval("RID")%>' runat="server">Edit</asp:LinkButton></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton CommandName="Del" CommandArgument='<%#Eval("RID")%>' runat="server">Delete</asp:LinkButton></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
                <asp:Button ID="btnApprove" CssClass="savebutton" runat="server" Text="Approve" OnClick="btnApprove_Click" />
            </td>
        </tr>
    </table>
    <table id="tblApprove" runat="server" visible="false" width="100%">
        <tr>
            <td>
                <asp:GridView ID="gvApprove" AutoGenerateColumns="false" Width="100%" runat="server"
                    ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                    HeaderStyle-CssClass="tableHead" OnRowCommand="gvImburse_RowCommand" OnRowEditing="gvImburse_RowEditing"
                    OnRowDataBound="gvImburse_RowDataBound" CssClass="gridview">
                    <Columns>
                        <asp:BoundField HeaderText="TransID" DataField="TransID" />
                        <asp:BoundField HeaderText="RID" Visible="false" DataField="RIDs" />
                        <asp:BoundField HeaderText="EmpID" DataField="EmpID" />
                        <asp:BoundField HeaderText="ReimburseType" DataField="ReimburseTypes" />
                        <asp:BoundField HeaderText="TotalAmount" DataField="TotAmount" />
                        <asp:BoundField HeaderText="Approved On" DataField="ApprovedDate" />
                      
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
