<%@ Page Title="" Language="C#"  AutoEventWireup="True"
    CodeBehind="QuickTrans.aspx.cs" Inherits="AECLOGIC.ERP.HMS.QuickTrans" EnableEventValidation="false" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function ShowGroups(source) {
            // var obj = event.srcElement || event.target;
            var obj = $get(source);
            for (i = 0; i < obj.length; i++) {

                obj[i].title = AjaxDAL.GetGoodsGroup(obj[i].value).value;

            }
        } 
        function validateterms() {
            if (!Reval('<%=TxtTerms.ClientID %>', 'Term', ''))
                return false;
        }

        function validatetax() {
            if (!chkDropDownList('<%=ddltax.ClientID %>', 'Tax'))
                return false;

            if (!Reval('<%=txttax.ClientID %>', 'Value', ''))
                return false;
        }

        function ValidateWS() {
            if (document.getElementById('<%=ddlWorksite.ClientID %>').value == "0" && document.getElementById('<%=ddlWorksite.ClientID %>').value == "") {
                alert("Select Worksite");
                return false;
            }

        }

        function validatelumptax() {
            if (!chkDropDownList('<%=ddllumptax.ClientID %>', 'Lumpsum Amount'))
                return false;

            if (!Reval('<%=txtlumpsum.ClientID %>', 'Value', ''))
                return false;
        }


        function Multiply(contorl, oQty, oRate, oBudget) {
            if (checkdecmial(contorl) == false) {
                contorl.focus();
                return false;
            }

            var Qty = $get(oQty);
            var Rate = $get(oRate);
            var num = 0;
            num = eval(Qty.value) * eval(Rate.value);
            // obj.value = num.toFixed(2);

            $get(oBudget).innerHTML = addCommas(num.toFixed(2).toString());
            CalculatePOValue();
        }

    </script>
    <script language="javascript" type="text/javascript">


        function CalculatePOValue() {
            var Qty, Rate;
            var TOTPOVALUE = 0;

            var frm = document.getElementById("<%=dlIndents.ClientID %>");
            if (frm != null) {
                for (i = 1; i < frm.rows.length; i++) {
                    cellQty = frm.rows[i].cells[6];
                    cellRate = frm.rows[i].cells[7];
                    //loop according to the number of childNodes in the cell
                    for (k = 0; k < cellQty.childNodes.length; k++) {
                        if (cellQty.childNodes[k].type == "text") {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid
                            Qty = eval(cellQty.childNodes[k].value);
                        }
                    }
                    for (j = 0; j < cellRate.childNodes.length; j++) {
                        //if childNode type is CheckBox
                        if (cellRate.childNodes[j].type == "text") {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid
                            Rate = eval(cellRate.childNodes[j].value);
                        }
                    }
                    var tot = (Qty * Rate);
                    tot = tot.toFixed(2);
                    TOTPOVALUE = eval(TOTPOVALUE) + eval(tot);
                    TOTPOVALUE = TOTPOVALUE.toFixed(2);
                }

                var BasicValue = TOTPOVALUE;
                var ExciseDuty = 0;
                var gridFields = document.getElementById("<%=gvtax.ClientID %>");
                if (gridFields != null) {
                    var rowIndex;
                    var Value;
                    for (rowIndex = 1; rowIndex < gridFields.rows.length; rowIndex++) {
                        if (gridFields.rows[rowIndex].cells[0].innerHTML.toUpperCase().trim() == 'EXCISE DUTY') {
                            ExciseDuty = eval(gridFields.rows[rowIndex].cells[1].innerHTML);
                        }
                    }
                }
                var ED = (parseFloat(ExciseDuty) * parseFloat(BasicValue)) / 100;
                ED = ED.toFixed(2);

                var TAX = 0;
                var gridFields = document.getElementById("<%=gvtax.ClientID %>");
                if (gridFields != null) {
                    var rowIndex;
                    var Value;
                    for (rowIndex = 1; rowIndex < gridFields.rows.length; rowIndex++) {
                        if (gridFields.rows[rowIndex].cells[0].innerHTML.toUpperCase().trim() != 'EXCISE DUTY') {
                            Value = eval(gridFields.rows[rowIndex].cells[1].innerHTML);
                            TAX = parseFloat(TAX) + parseFloat(Value);
                        }
                    }
                }
                var Basic_Ed = parseFloat(BasicValue) + parseFloat(ED)

                TAX = (parseFloat(Basic_Ed) * parseFloat(TAX)) / 100;

                var LUMPSUM = 0;
                var gridFields = document.getElementById("<%=GVlumpsum.ClientID %>");
                if (gridFields != null) {
                    var rowIndex;
                    var Value;
                    for (rowIndex = 1; rowIndex < gridFields.rows.length; rowIndex++) {
                        Value = eval(gridFields.rows[rowIndex].cells[1].innerHTML);
                        LUMPSUM = parseFloat(LUMPSUM) + parseFloat(Value);
                    }
                }

                var POVALUE = parseFloat(TOTPOVALUE) + parseFloat(TAX) + parseFloat(LUMPSUM) + parseFloat(ED);
                //            tot = Math.round(tot * 100) / 100;
                var TotPoVal = POVALUE.toFixed(2).toString();

                document.getElementById("<%=lblrate.ClientID %>").innerHTML = addCommas(TotPoVal.toString());
                document.getElementById("<%=hfOfrVal.ClientID %>").value = addCommas(TotPoVal.toString());

            }
        }

        function addCommas(sValue) {
            var sRegExp = new RegExp('(-?[0-9]+)([0-9]{3})');

            while (sRegExp.test(sValue)) {
                sValue = sValue.replace(sRegExp, '$1,$2');
            }
            return sValue;
        }
        function CheckDateEalier(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("Future Date Cannot be Accepted!");
                sender._selectedDate = new Date();
                // set the date back to the today
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
                return false;
            }
        }

        function CheckDateOlder(sender, args) {
            if (sender._selectedDate <= new Date()) {
                alert("Older Date Cannot be Accepted!");
                sender._selectedDate = new Date();
                // set the date back to the today
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
                return false;
            }
        }  </script>
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
    <script language="javascript" type="text/javascript">

        function AddNewItem() {
            retval = window.showModalDialog("ProcNewItem.aspx?type=item&From=Enq", "", "dialogheight:500px;dialogwidth:400px;status:no;edge:sunken;unadorned:no;resizable:no;");
            if (retval == 1) {
                window.location.href = "Indent.aspx";
            }
            else {
                return false;
            }
        }

    </script>
    <script language="javascript" type="text/javascript">

        function validateMove() {
            var elSel = document.getElementById('<%=lbxItems.ClientID%>');

            var j = 0;
            for (i = 0; i < elSel.options.length; i++) {
                if (elSel.options[i].selected) {
                    if (elSel.options[i].value == 0) {
                        alert("Select Valid Item");
                        return false;
                    }
                    j++;
                }
            }
            if (j == 0) {
                alert("Select atleast one Item");
                return false;
            }
        }
    
    </script>
    <script language="javascript" type="text/javascript" >
        
         function validatesave() {
            if (!Reval('<%=txtReqNo.ClientID %>', 'Ref#', '[Enter Indent Quick Reference]'))
                return false;

            if (!Reval('<%=txtpofor.ClientID %>', 'PO For', ''))
                return false;

            if (document.getElementById('<%=ddlWorksite.ClientID %>').value == "0") {
                alert("Select Worksite");
                return false;
            }


            if (!chkDropDownList('<%=ddlProjects.ClientID %>', 'Project'))
                return false;



            var GV = document.getElementById("<%=dlIndents.ClientID %>");
            if (GV != null) {

                var totalRows = GV.rows.length;
                var rowIndex;
                for (rowIndex = 1; rowIndex < totalRows; rowIndex++) {
                    //                    if (GV.rows[rowIndex].cells[2].children[0].value == "") {
                    //                        alert("Enter Specfication");
                    //                        GV.rows[rowIndex].cells[2].children[0].focus();
                    //                        return false;
                    //                    }

                    if (GV.rows[rowIndex].cells[6].children[0].value == "") {
                        alert("Enter Required Date");
                        GV.rows[rowIndex].cells[6].children[0].focus();
                        return false;
                    }

                    if (GV.rows[rowIndex].cells[7].children[0].value == "") {
                        alert("Enter Quantity");
                        GV.rows[rowIndex].cells[7].children[0].focus();
                        return false;
                    }

                    if (GV.rows[rowIndex].cells[7].children[0].value == 0) {
                        alert("Quantity Cannot be Zero");
                        GV.rows[rowIndex].cells[7].children[0].focus();
                        return false;
                    }

                    if (GV.rows[rowIndex].cells[8].children[0].value == "") {
                        alert("Enter Rate");
                        GV.rows[rowIndex].cells[8].children[0].focus();
                        return false;
                    }

                    if (GV.rows[rowIndex].cells[8].children[0].value == 0) {
                        alert("Rate Cannot be Zero");
                        GV.rows[rowIndex].cells[8].children[0].focus();
                        return false;
                    }
                }
            }
            else {
                alert("No Indent Items Added");
                return false;
            }

            if (!chkDropDownList('<%=ddlVendor.ClientID %>', 'Vendor'))
                return false;

            if (!chkDropDownList('<%=ddlPayment.ClientID %>', 'Payment Type'))
                return false;
            if (!chkDropDownList('<%=ddlRcvr.ClientID%>', ' Receiver'))
                return false;
             <%-- if (!chkDropDownList('<%=ddlAltRcvr.ClientID%>', 'Alternate  Receiver'))
                return false;--%>
             if (document.getElementById('<%=ddlAltRcvr.ClientID %>').value == "0") {
                 alert("Select Alternate  Receiver");
                 return false;
             }
            if (!chkDropDownList('<%=ddlMnStaff.ClientID%>', 'Monitor Staff'))
                return false;
            if (!chkDropDownList('<%=ddlAltMnStaff.ClientID%>', 'Alternate Monitor Staff'))
                return false;

            /// var res = CheckStock();
            return true;
        }

        function CheckStock() {
            var ddl = document.getElementById('<%=listParentAcGroup.ClientID %>');
            if (ddl.value != "1" && ddl.value != "4") {
                var answer = window.confirm("These PAGG items will not effect in stock, would you like to proceed?");
                return answer;
            }
        }

        function ValidateRcvrs() {
            if (!chkDropDownList('<%=ddlNewRcvrsByDept.ClientID %>', 'New Receiver'))
                return false;
        }
        function ValidateAltRcvrs() {
            if (!chkDropDownList('<%=ddlNewAltRcvrByDept.ClientID %>', 'New Alternative Receiver'))
                return false;
        }
        function ValidateMntr() {
            if (!chkDropDownList('<%=ddlAddNewMntrByDept.ClientID %>', 'New Monitor'))
                return false;
        }
        function ValidateAltMntr() {
            if (!chkDropDownList('<%=ddlNewAltMntrByDept.ClientID %>', 'New Alternative Monitor'))
                return false;
        }
    </script>

   
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <asp:MultiView ID="MainView" runat="server">
                            <asp:View ID="VWGoods" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td colspan="3" valign="middle" style="padding-left: 150px">
                                            <asp:Image ID="imgProjects" runat="server" ImageUrl="~/IMAGES/Projects.PNG" AlternateText="Projects" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" valign="middle" style="padding-left: 165px">
                                            <asp:Image ID="imgArrow1" runat="server" ImageUrl="~/IMAGES/Arrow.PNG" AlternateText="Arrow" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" valign="middle" style="padding-left: 150px">
                                            <asp:Image ID="imgResources" runat="server" ImageUrl="~/IMAGES/Resources.PNG" AlternateText="Resources" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" valign="middle" style="padding-left: 165px">
                                            <asp:Image ID="imgArrow2" runat="server" ImageUrl="~/IMAGES/Arrow.PNG" AlternateText="Arrow" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-right: 15px">
                                            <asp:ImageButton ID="btnGoods" runat="server" OnClick="btnGoods_Click" AlternateText="Goods"
                                                ImageUrl="~/IMAGES/PO.PNG" />&nbsp;&nbsp;
                                            <asp:ImageButton ID="btnServices" runat="server" OnClick="btnService_Click" AlternateText="Services"
                                                ImageUrl="~/IMAGES/WO.PNG" />
                                        </td>
                                        <td align="left">
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="VWRaiseIndent" runat="server">
                                <table>
                                    <tr>
                                        <td colspan="3" align="left">
                                            <asp:Label ID="lblType" runat="server" CssClass="pageheader"></asp:Label>
                                        </td>
                                    </tr>
                               
                                    <tr>
                                        <td align="left">
                                            <asp:UpdatePanel ID="updWS" runat="server">
                                                <ContentTemplate>
                                                    <table>
                                                        <tr>
                                                            <td align="left" style="width: 592px">
                                                                <table style="width: 139%">
                                                                    <tr>
                                                                        <td style="vertical-align: top; text-align: left; width: 150px; height: 29px;">
                                                                            Indent Reference <span style="color: #FF0000"><sup class="Must">*</sup></span>
                                                                        </td>
                                                                        <td style="vertical-align: top; text-align: left; width: 251px; height: 29px;">
                                                                            <asp:TextBox ID="txtReqNo" runat="server" Width="300px" Text="Quick Trans"></asp:TextBox>&nbsp;<asp:Label
                                                                                ID="lbltext" runat="server" Text="" />
                                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtReqNo"
                                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Indent Quick Reference]">
                                                                            </cc1:TextBoxWatermarkExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                    <td style="vertical-align: top; text-align: left; width: 150px; height: 29px;">
                                                                           <asp:Label ID="lblPOFor" runat="server"></asp:Label>
                                                                               <span style="color: #FF0000"><sup class="Must">*</sup></span>
                                                                    </td>
                                                                     <td style="vertical-align: top; text-align: left; width: 251px; height: 29px;">
                                                                             <asp:TextBox ID="txtpofor" runat="server" Width="300px" MaxLength="100" Text="Quick Trans"></asp:TextBox>
                                                                          
                                                                        </td>
                                                                        </tr>
                                                                    <tr>
                                                                        <td style="vertical-align: top; text-align: left; width: 150px; height: 29px;">
                                                                            Work Site <span style="color: #FF0000"><sup class="Must">*</sup></span>
                                                                        </td>
                                                                        <td style="vertical-align: top; text-align: left; width: 251px; height: 29px;">
                                                                            <asp:DropDownList ID="ddlWorksite" runat="server" Width="250" AutoPostBack="True"
                                                                                CssClass="droplist" OnSelectedIndexChanged="ddlForProject_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="height: 29px">
                                                                            <asp:TextBox ID="txtSearchWorksite" runat="server" Width="90px"></asp:TextBox>
                                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtSearchWorksite"
                                                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite]">
                                                                            </cc1:TextBoxWatermarkExtender>
                                                                            <asp:Button ID="btnSearcWorksite" CssClass="savebutton btn btn-primary" runat="server" Text="Filter"
                                                                                OnClick="btnSearcWorksite_Click" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="vertical-align: top; text-align: left; width: 150px;">
                                                                            Project:<span style="color: #FF0000"><sup class="Must">*</sup></span>
                                                                        </td>
                                                                        <td style="vertical-align: top; text-align: left; width: 251px;">
                                                                            <asp:DropDownList ID="ddlProjects" runat="server" Width="250" CssClass="droplist">
                                                                            </asp:DropDownList>
                                                                            <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" IsSorted="true" QueryPattern="Contains"
                                                                                PromptCssClass="PromptText" PromptPosition="Top" PromptText="Type to search"
                                                                                TargetControlID="ddlProjects" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" visible="false">
                                                                        <td style="vertical-align: top; text-align: left; width: 150px;">
                                                                            PO Date:<span style="color: #FF0000"><sup class="Must">*</sup></span>
                                                                        </td>
                                                                        <td style="vertical-align: top; text-align: left; width: 251px;">
                                                                            <asp:TextBox ID="txtDate" runat="server" CssClass="droplist" Width="100" AccessKey="t"
                                                                                ToolTip="[Alt+t OR Alt+t+Enter]" TabIndex="4"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd MMM yyyy"
                                                                                PopupPosition="Right" TargetControlID="txtDate">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="vertical-align: top; text-align: left;" align="left" colspan="2">
                                                                            <asp:LinkButton ID="btnNewItem" runat="server" Text="Click to Create New Goods Item"
                                                                                CssClass="btn"></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="tableAddResources" runat="server">
                                                        <tr>
                                                            <td style="text-align: left; font-weight: bold; width: 592px;">
                                                                <asp:Label ID="lblParent" Text=" Parent Accounts Goods Group" runat="server"></asp:Label>&nbsp;
                                                            </td>
                                                            <td style="text-align: left; font-weight: bold;">
                                                                <asp:Label ID="lblGroupSearch" runat="server"></asp:Label>
                                                                <asp:TextBox ID="txtFilterGroups" runat="server" AutoPostBack="true" Width="100"></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtFilterGroups"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Goods/Services Groups to Search]">
                                                                </cc1:TextBoxWatermarkExtender>
                                                                <asp:Button ID="btnSearchGroups" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearchGroups_Click">
                                                                </asp:Button>
                                                            </td>
                                                            <td style="text-align: left; font-weight: bold;">
                                                                <asp:Label ID="lblItemsSearch" runat="server"></asp:Label>
                                                                <asp:TextBox ID="txtFilter" runat="server" AutoPostBack="false" Width="100"></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtFilter"
                                                                    WatermarkCssClass="watermark" WatermarkText="[Enter Item to Search]">
                                                                </cc1:TextBoxWatermarkExtender>
                                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" Text="Search" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" style="width: 592px">
                                                                <asp:ListBox ID="listParentAcGroup" runat="server" AutoPostBack="true" CssClass="droplist full__width"
                                                                    Height="200px" Width="100%" Rows="30" OnSelectedIndexChanged="listParentAcGroup_SelectedIndexChanged">
                                                                </asp:ListBox>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:ListBox ID="lbxGGroup" runat="server" AutoPostBack="true" CssClass="droplist full__width" 
                                                                    OnSelectedIndexChanged="lbxGGroup_SelectedIndexChanged" Rows="30" Width="100%"
                                                                    Height="200px"></asp:ListBox>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:ListBox ID="lbxItems" runat="server" CssClass="droplist full__width" Height="200px" Rows="30"
                                                                    SelectionMode="Multiple" Width="100%" OnSelectedIndexChanged="lbxItems_SelectedIndexChanged"
                                                                    AutoPostBack="True"></asp:ListBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 13px; padding-left: 450px">
                                            <asp:Button ID="btnMove" runat="server" CssClass="savebutton btn btn-success" Text="Add to Indent"
                                                OnClientClick="javascript:return ValidateWS();" OnClick="btnMove_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" align="left" colspan="2">
                                         
                                                    <asp:GridView ID="dlIndents" runat="server" CssClass="gridview" AutoGenerateColumns="false"
                                                        HeaderStyle-CssClass="tableHead" Width="70%" OnRowDeleting="dlIndents_RowDeleting"
                                                        OnRowDataBound="dlIndents_RowDataBound">
                                                        <RowStyle CssClass="gentext" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex + 1%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30" />
                                                                <HeaderStyle HorizontalAlign="Left" Width="30" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Goods/Services Name" DataField="Goods">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Specification">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSpec" Width="200" Height="50" runat="server" TextMode="MultiLine"
                                                                        Text='<%# DataBinder.Eval(Container.DataItem,"Specification") %>'></asp:TextBox>
                                                                    <asp:Label ID="itemid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"GoodsID") %>'
                                                                        Visible="false"></asp:Label>
                                                                    <cc1:FilteredTextBoxExtender FilterMode="InvalidChars" ID="F1" InvalidChars=":" runat="server"
                                                                        TargetControlID="txtSpec" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Units of Measure">
                                                                <ItemTemplate>
                                                                   
                                                                    <asp:DropDownList ID="ddlunits" runat="server" AutoPostBack="false" CssClass="droplist"
                                                                        DataSource='<%# BindResUnits(Eval("GoodsID").ToString())%>' DataTextField="Name"
                                                                        DataValueField="Id" SelectedIndex='<%# GetIndexUnits(Eval("GoodsID").ToString(),Eval("uom").ToString())%>'>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mileage" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlMunits" SelectedIndex='<%# GetRolesIndex(Eval("Mileage").ToString())%>'
                                                                        runat="server" AutoPostBack="false" DataTextField="Au_Name" DataValueField="Au_Id"
                                                                        CssClass="droplist" DataSource='<%# BindRoles()%>'>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Purpose">
                                                                <ItemTemplate>
                                                                    <asp:TextBox Width="200" Height="50" ID="txtPurpose" TextMode="MultiLine" runat="server"
                                                                        Text='<%# DataBinder.Eval(Container.DataItem,"Purpose") %>'></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender FilterMode="InvalidChars" ID="F2" InvalidChars=":" runat="server"
                                                                        TargetControlID="txtPurpose" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Required On">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtReq" ReadOnly="true" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"Requiredon")).ToString("dd/MM/yyyy") %>'
                                                                        Width="70"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="cc1" Format="dd/MM/yyyy" PopupButtonID="<%=txtReq.ClientID %>"
                                                                        runat="server" TargetControlID="txtReq" OnClientDateSelectionChanged="CheckDateEalier" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Right"
                                                                ItemStyle-Width="40">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Quantity") %>'
                                                                        Style="text-align: right;" Width="40"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender FilterMode="ValidChars" FilterType="Numbers,Custom"
                                                                        ID="Fl1" runat="server" TargetControlID="txtQty" ValidChars="." />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Basic Rate" HeaderStyle-Width="40" HeaderStyle-HorizontalAlign="Right"
                                                                ItemStyle-Width="40">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtrate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"basicrate") %>'
                                                                        Style="text-align: right;" Width="40"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender FilterMode="ValidChars" FilterType="Numbers,Custom"
                                                                        ID="Fl2" runat="server" TargetControlID="txtrate" ValidChars="." />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Budget" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                                                HeaderStyle-Width="40" ItemStyle-Width="40">
                                                                <ItemTemplate>
                                                                    <itemstyle horizontalalign="Right" />
                                                                    <asp:Label ID="lbl" runat="server" EnableViewState="true" Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem,"Quantity")) * Convert.ToDouble(DataBinder.Eval(Container.DataItem,"BasicRate")))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnk" runat="server" CssClass="anchor__grd dlt" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"GoodsID")%>'
                                                                        CommandName="Delete" Text="Delete" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                        </Columns>
                                                    </asp:GridView>
                                                    <br />
                                             
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="updVendors" runat="server">
                                                <ContentTemplate>
                                                    <b>Vendor Name: &nbsp;</b><asp:DropDownList ID="ddlVendor" runat="server" width="150px" AutoPostBack="false"
                                                        CssClass="droplist"  DataValueField="ID"
                                                        DataTextField="Name">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtvendorSearch" runat="server" width="100px" AccessKey="s" CssClass="droplist"></asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxVendors" runat="server" TargetControlID="txtvendorSearch"
                                                        WatermarkCssClass="watermark" WatermarkText="Filter Vendors">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:Button ID="btnVendorSearch" Text="Filter" runat="server" CssClass="savebutton btn btn-primary"
                                                        OnClick="btnVendorSearch_Click" />
                                                    <asp:LinkButton ID="lnkRefreshVendor" runat="server" CssClass="anchor__grd edit_grd" Text="Refresh Vendors" OnClick="lnkRefreshVendor_Click"></asp:LinkButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                           
                                            <b>&nbsp;Add Prorated:</b>
                                            <asp:DropDownList ID="ddltax" runat="server" AutoPostBack="false" Width="150" CssClass="droplist">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txttax" runat="server" width="100px" OnBlur="javascript:return checkdecmial(this);"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender FilterType="Custom, Numbers" FilterMode="ValidChars"
                                                ID="fl1" runat="server" ValidChars="." TargetControlID="txttax" />
                                            <asp:Button ID="BtnAddtax" runat="server" Text=" Add Tax" OnClick="BtnAddtax_Click"
                                                CssClass="savebutton btn btn-success"></asp:Button>
                                            <asp:LinkButton ID="lnkRefreshTaxs" runat="server" CssClass="anchor__grd edit_grd" Text="Refresh Prorates" OnClick="lnkRefreshTaxs_Click"></asp:LinkButton>
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:GridView ID="gvtax" runat="server" Width="30%" HeaderStyle-CssClass="tableHead"
                                                AutoGenerateColumns="false" OnRowCommand="gvtax_RowCommand" >
                                                <RowStyle CssClass="gentext" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Tax Name" DataField="tax_name">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Value" DataField="value">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltxid" runat="server" Text='<%#Bind("tax_id")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDel" runat="server" CssClass="anchor__grd dlt" CommandName="lnkDel" Text="Delete" CommandArgument='<%#Bind("tax_id")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                          
                                            <b>Add Lumpsum:</b>
                                            <asp:DropDownList ID="ddllumptax" runat="server" AutoPostBack="false" Width="300"
                                                CssClass="droplist">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtlumpsum" runat="server" width="130px" OnBlur="javascript:return checkdecmial(this);"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender FilterType="Custom, Numbers" FilterMode="ValidChars"
                                                ID="FilteredTextBoxExtender1" runat="server" ValidChars="." TargetControlID="txtlumpsum" />
                                            <asp:Button ID="BtnAddlumpsum" runat="server" Text=" Add Lumpsum Amount " OnClick="BtnAddlumpsum_Click"
                                                CssClass="savebutton btn btn-success"></asp:Button>
                                            <asp:LinkButton ID="lnkRefreshLumpsums" runat="server" CssClass="anchor__grd edit_grd" on Text="Refresh Lumpsums"
                                                OnClick="lnkRefreshLumpsums_Click"></asp:LinkButton>
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="10">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:GridView ID="GVlumpsum" runat="server" Width="30%" HeaderStyle-CssClass="tableHead"
                                                AutoGenerateColumns="false" OnRowCommand="GVlumpsum_RowCommand" >
                                                <RowStyle CssClass="gentext" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Lumptax Name" DataField="tax_name">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Value" DataField="value">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblltaxid" runat="server" Text='<%#Bind("tax_id")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDel" runat="server" CssClass="anchor__grd dlt" CommandName="lnkDel" Text="Delete" CommandArgument='<%#Bind("tax_id")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="10">
                                            <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox ID="chkTDS" runat="server" AutoPostBack="true" OnCheckedChanged="chkTDS_CheckedChanged"
                                                    Text="TDS Applicable?" /></b>
                                            <div id="dvTDS" runat="server" visible="false">
                                                <table>
                                                    <tr>
                                                        <td style="width: 70px">
                                                            <b>TDS(%): </b>
                                                        </td>
                                                        <td style="width: 113px" valign="top">
                                                            <asp:TextBox ID="txtTDS" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMsg" runat="server" Visible="false" Style="color: #FF0000; font-weight: 700"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Payment Type<sup style="color: #FF0000">*&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </sup>:</b>&nbsp;
                                            <asp:DropDownList ID="ddlPayment" runat="server" Width="80px" CssClass="droplist">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>Receivers<sup style="color: #FF0000">*&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp;&nbsp;&nbsp; </sup>:</b>
                                            <asp:DropDownList ID="ddlRcvr" runat="server" Width="150px" CssClass="droplist">
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lnkAddRcvrs" runat="server" CssClass="anchor__grd edit_grd" Text="AddNewReceiver" OnClick="lnkAddRcvrs_Click"></asp:LinkButton>
                                            &nbsp;&nbsp;
                                            <asp:CheckBox ID="chkEmp" Text="Head Office Emplyoees" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="chkEmp_CheckedChanged" />
                                            <div id="dvAddRcvrs" runat="server" visible="false">
                                                <table>
                                                    <tr>
                                                        <td valign="top" colspan="2" align="right">
                                                            Receiving Dept<sup style="color: #FF0000">*</sup>:
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlStrRcvrDept" runat="server" CssClass="droplist" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlStrRcvrDept_SelectedIndexChanged" Width="150px">
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblNewRcvr" runat="server" Text="New Receiver:" Visible="false"></asp:Label>
                                                            <asp:DropDownList ID="ddlNewRcvrsByDept" CssClass="droplist" runat="server" Visible="False"
                                                                Width="150px">
                                                            </asp:DropDownList>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnAddNewRcvr" runat="server" Text="Add" CssClass="savebutton" OnClick="btnAddNewRcvr_Click"
                                                                Visible="False" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>Alt Receivers<sup style="color: #FF0000">*&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </sup>:</b>
                                            <asp:DropDownList ID="ddlAltRcvr" runat="server" Width="150px" CssClass="droplist">
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lnkAddAltRcvr" runat="server" CssClass="anchor__grd edit_grd" Text="AddNewAltReceiver" OnClick="lnkAddAltRcvr_Click"></asp:LinkButton>
                                            <div id="dvAddAltRcvrs" runat="server" visible="false">
                                                <table>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            Receiving Dept<sup style="color: #FF0000">*</sup>:
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlAltRcvrDept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAltRcvrDept_SelectedIndexChanged"
                                                                Width="150px">
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblNewAltRcvr" runat="server" Text="New AltReceivers:" Visible="false"></asp:Label>
                                                            <asp:DropDownList ID="ddlNewAltRcvrByDept" CssClass="droplist" runat="server" Visible="False"
                                                                Width="150px">
                                                            </asp:DropDownList>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnAddNewAltRcvr" runat="server" Text="Add" CssClass="savebutton"
                                                                Visible="False" OnClick="btnAddNewAltRcvr_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Monitors<sup style="color: #FF0000">*&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </sup>:</b>
                                            <asp:DropDownList ID="ddlMnStaff" runat="server" Width="150px" CssClass="droplist">
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lnkAddMntrs" runat="server" CssClass="anchor__grd edit_grd" Text="AddNewMonitor" OnClick="lnkAddMntrs_Click"></asp:LinkButton>
                                            <div id="dvAddMntrs" runat="server" visible="false">
                                                <table>
                                                    <tr>
                                                        <td align="right">
                                                            Monitoring Dept <sup style="color: #FF0000">*</sup>:
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlMntrsDept" runat="server" AutoPostBack="True" CssClass="droplist"
                                                                OnSelectedIndexChanged="ddlMntrsDept_SelectedIndexChanged" Width="150px">
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblAddMntr" runat="server" Text="New Monitor:" Visible="false"></asp:Label>
                                                            <asp:DropDownList ID="ddlAddNewMntrByDept" runat="server" Visible="False" Width="150px"
                                                                CssClass="droplist">
                                                            </asp:DropDownList>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnAddMntr" runat="server" Text="Add" CssClass="savebutton" Visible="False"
                                                                OnClick="btnAddMntr_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Alt Monitors<sup style="color: #FF0000">*&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</sup>:</b>
                                            <asp:DropDownList ID="ddlAltMnStaff" runat="server" Width="150px" CssClass="droplist">
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lnkAddAltMntr" runat="server" CssClass="anchor__grd edit_grd" Text="AddNewAltMonitor" OnClick="lnkAddAltMntr_Click"></asp:LinkButton>
                                            <div id="dvAddAltMntr" runat="server" visible="false">
                                                <table>
                                                    <tr>
                                                        <td align="right">
                                                            Monitoring Dept <sup style="color: #FF0000">*</sup>:
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlAltMntrsDept" runat="server" AutoPostBack="True" Width="150px"
                                                                CssClass="droplist" OnSelectedIndexChanged="ddlAltMntrsDept_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblNewAltMntr" runat="server" Text="New AltMonitor:" Visible="false"></asp:Label>
                                                            <asp:DropDownList ID="ddlNewAltMntrByDept" runat="server" Visible="False" Width="150px"
                                                                CssClass="droplist">
                                                            </asp:DropDownList>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnAddAltMntr" runat="server" Text="Add" CssClass="savebutton" Visible="False"
                                                                OnClick="btnAddAltMntr_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="10">
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td style="width: 80%" colspan="2">
                                            <b>Offer value:</b>&nbsp;&nbsp;<asp:Label ID="lblrate" runat="server" Text="0" Font-Bold="true"
                                                ForeColor="brown" />
                                            <asp:HiddenField ID="hfOfrVal" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Delivery&nbsp;Terms&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtDelivery" runat="server" Width="250" TextMode="MultiLine" MaxLength="400"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Payment&nbsp;terms&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtPayment" runat="server" Width="250" TextMode="MultiLine" MaxLength="400"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Mode of Transport
                                            <asp:TextBox ID="txtMOT" runat="server" Width="250" TextMode="MultiLine" MaxLength="400"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%" colspan="2">
                                            Predefined Terms & Conditions Groups:&nbsp;
                                           
                                            <asp:DropDownList ID="ddlTermsList" runat="server" width="120px" OnSelectedIndexChanged="ddlTermsList_SelectedIndexChanged"
                                                AutoPostBack="true" CssClass="droplist">
                                            </asp:DropDownList>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%" colspan="2">
                                            <b>Upload quatations:</b>&nbsp;&nbsp;<asp:FileUpload ID="fupQuatations" runat="server"/>
                                 </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <cc1:Accordion ID="Accordion1" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                                AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                                RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="1054px">
                                                <Panes>
                                                    <cc1:AccordionPane ID="AccordionPane4" runat="server" HeaderCssClass="accordionHeader"
                                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                                        <Header>
                                                            <b>Offered Final Terms &amp; Conditions:</b>
                                                        </Header>
                                                        <Content>
                                                            <asp:GridView ID="GVTERMS" runat="server" Width="100%" HeaderStyle-CssClass="tableHead"
                                                                DataKeyNames="TermId" OnRowCommand="GVTERMS_RowCommand" EmptyDataRowStyle-CssClass="EmptyRowData"
                                                                EmptyDataText="No Terms found." AutoGenerateColumns="false" OnRowDataBound="GVTERMS_RowDataBound">
                                                                <RowStyle CssClass="gentext" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="cbSelectAll" runat="server" Visible="false" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chk" runat="server" AutoPostBack="false" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="30" />
                                                                        <ItemStyle Width="30" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sl NO">
                                                                        <HeaderStyle Width="15PX" />
                                                                        <ItemStyle Width="15PX" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTermID" runat="server" Width="99%" Text='<%#Eval("SLNO")%>' Height="20" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Terms">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TXTTERMS" runat="server" Text='<%#Bind("Term")%>' Width="99%" Height="20"
                                                                                OnTextChanged="TXTTERMS_TextChanged" AutoPostBack="true" TextMode="MultiLine" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="termid" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblid" runat="server" Text='<%#Bind("Termid")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkTermDel" runat="server" CssClass="anchor__grd dlt" Text="Delete" CommandArgument='<%#Bind("Termid")%>'
                                                                                CommandName="Del"></asp:LinkButton></ItemTemplate>
                                                                        <HeaderStyle Width="50" />
                                                                        <ItemStyle Width="50" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </Content>
                                                    </cc1:AccordionPane>
                                                </Panes>
                                                <Panes>
                                                    <cc1:AccordionPane ID="AccordionPane3" runat="server" HeaderCssClass="accordionHeader"
                                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                                        <Header>
                                                            Standard Master Terms &amp; Conditions:</Header>
                                                        <Content>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="width: 10%" valign="top">
                                                                        Remaining Terms:
                                                                    </td>
                                                                    <td style="width: 80%" valign="top">
                                                                        <asp:ListBox ID="lstTerms" runat="server" CssClass="droplist" SelectionMode="Multiple"
                                                                            Height="200"></asp:ListBox>
                                                                       
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnIncludeTerm" runat="server" CssClass="savebutton btn btn-success" Text="Include Term"
                                                                            OnClick="btnIncludeTerm_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </Content>
                                                    </cc1:AccordionPane>
                                                </Panes>
                                            </cc1:Accordion>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%" colspan="2">
                                            <cc1:Accordion ID="MyAccordion" runat="server" SelectedIndex="-1" HeaderCssClass="accordionHeader"
                                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                                AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                                RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="1056px">
                                                <Panes>
                                                    <cc1:AccordionPane ID="AccordionPane2" runat="server" HeaderCssClass="accordionHeader"
                                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                                        <Header>
                                                            Appended Additional Terms to the Offer</Header>
                                                        <Content>
                                                            <asp:GridView ID="GVAdditionalTerms" runat="server" Width="100%" HeaderStyle-CssClass="tableHead"
                                                                EmptyDataRowStyle-CssClass="EmptyRowData" EmptyDataText="No Additional Terms found."
                                                                AutoGenerateColumns="false"  OnRowCommand="GVAdditionalTerms_RowCommand">
                                                                <RowStyle CssClass="gentext" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl NO">
                                                                        <HeaderStyle Width="15PX" />
                                                                        <ItemStyle Width="15PX" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTermID" runat="server" Width="99%" Text='<%#Eval("SLNO")%>' Height="20" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Additional Terms">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TXTTERMS" runat="server" Text='<%#Bind("Remarks")%>' Width="99%"
                                                                                Height="20" TextMode="MultiLine" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="termid" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblid" runat="server" Text='<%#Bind("Termid")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkTermDel" runat="server" Text="Delete" CommandName="Del" CommandArgument='<%#Bind("Termid")%>'></asp:LinkButton></ItemTemplate>
                                                                        <HeaderStyle Width="50" />
                                                                        <ItemStyle Width="50" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </Content>
                                                    </cc1:AccordionPane>
                                                </Panes>
                                                <Panes>
                                                    <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                                        <Header>
                                                            Append to Additional Terms</Header>
                                                        <Content>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtTerms" runat="server" Width="99%" Height="20" TextMode="MultiLine" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnAddNewTerm" runat="server" Text="Add Additional Terms" OnClick="btnAddNewTerm_Click"
                                                                            CssClass="savebutton btn btn-success" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </Content>
                                                    </cc1:AccordionPane>
                                                </Panes>
                                            </cc1:Accordion>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="3" style="width: 80%" colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="10" style="width: 80%" colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%" colspan="2">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Post Transaction" CssClass="savebutton btn btn-success"
                                                OnClientClick="javascript:return validatesave();" OnClick="btnSubmit_Click">
                                            </asp:Button><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="15" style="width: 80%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="10" valign="top" style="width: 80%" colspan="2">
                                            Print:<asp:RadioButtonList ID="Rblist" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow">
                                                <asp:ListItem Text="Normal" Value="2" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Letter Head" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="onSuccess" runat="server">
                                <div style="text-align: center; width: 100%; height: 500px;">
                                    <div style="width: 350px; margin-top: 50px; height: 200px;" class="DivBorderGray">
                                        <br />
                                        <span style="font-size: 1.2em; font-weight: bold; color: Maroon;">Transaction sent to
                                            Accounts(AMS) Drafts</span>
                                        <table style="width: 280px; margin: 20px 5px 20px 5px; text-align: left;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblIndentType" Text="PO" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 3px">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPONo" Text="0000" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblRecievedNoteType" Text="PO" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 3px">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRecievedNote" Text="0000" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Bill No
                                                </td>
                                                <td style="width: 3px">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBillNo" Text="0" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Transaction ID
                                                </td>
                                                <td style="width: 3px">
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTransID" Text="0" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                       <asp:HyperLink ID="lnkPO" Target="_blank" Font-Bold="true" Text="Click here to view PO/WO"
                                            runat="server"></asp:HyperLink>
                                    </div>
                                </center>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
      
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
            <ProgressTemplate>
                <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif"  ImageAlign="AbsMiddle"
                    ID="imgs" />
                please wait...</ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
