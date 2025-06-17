<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="SRN.aspx.cs"  Inherits="AECLOGIC.ERP.HMS.SRN"   
    Title   ="" MasterPageFile="~/Templates/CommonMaster.master" %>
<%@ OutputCache Location="None" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function Valid() {
            //Date Check
            if (!chkDate("<%=txtDate.ClientID %>", "Vehicle Name", true, ""))
                return false;
            //Vendor
            if (!chkDropDownList("<%=ddlVendor.ClientID %>", "Vendor"))
                return false;
            return true;
        }
        function CancelAsyncPostBack() {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            //alert(prm);
            if (prm.get_isInAsyncPostBack()) {
                prm.abortPostBack();
            }
        }
        function FilterEmployees(WorkSite, Origi, Desti) {
            var Work = document.getElementById(WorkSite).options[document.getElementById(WorkSite).selectedIndex].value;
            var Exec = document.getElementById(Origi);
            var dest = document.getElementById(Desti);
            addOption(Exec, 'Select Origin Representative ', '-1');
            addOption(dest, 'Select Destination Representative ', '-1');
            var len = Exec.options.length;
            for (i = len - 1; i >= 0; i--) {
                Exec.options.remove(i);
                dest.options.remove(i);
            }
            var Res = GDN.PopulateDDL(Work);
            var res1 = Res.value.split('|');
            var s = res1.length;
            for (var i = 0; i < s - 1; ++i) {
                var Result = res1[i].split('@');
                addOption(Exec, Result[0], Result[1]);
                addOption(dest, Result[0], Result[1]);
            }
        }
        function SearchVendorJS(DrVendor, txtVendor) {
            var Vendor = document.getElementById(txtVendor);
            //alert(imgSearch.style.visibility);
            //ImageSearch.style.visibility="visible";
            var DropVendor = document.getElementById(DrVendor);
            // alert(Materials.value.length);
            if (Vendor.value.length > 1) {
                var Res = GDN.SearchVendor(Vendor.value);
                var len = DropVendor.options.length;
                for (i = len - 1; i >= 0; i--) {
                    DropVendor.options.remove(i);
                }
                var res1 = Res.value.split('|');
                var s = res1.length;
                addOption(DropVendor, 'Select Vendor ', '-1');
                for (var i = 0; i < s - 1; ++i) {
                    var Result = res1[i].split('@');
                    addOption(DropVendor, Result[0], Result[1]);
                }
            }
        }
        function SearchVehicleJS(DrVehicle, txtVehicle) {
            var Vehicle = document.getElementById(txtVehicle);
            //alert(imgSearch.style.visibility);
            //ImageSearch.style.visibility="visible";
            var DropVehicle = document.getElementById(DrVehicle);
            // alert(Materials.value.length);
            if (Vehicle.value.length > 1) {
                var Res = GDN.SearchVehicle(Vehicle.value);
                var len = DropVehicle.options.length;
                for (i = len - 1; i >= 0; i--) {
                    DropVehicle.options.remove(i);
                }
                var res1 = Res.value.split('|');
                var s = res1.length;
                addOption(DropVehicle, 'Select Vehicle ', '-1');
                for (var i = 0; i < s - 1; ++i) {
                    var Result = res1[i].split('@');
                    addOption(DropVehicle, Result[0], Result[1]);
                }
            }
        }
        function addOption(selectbox, text, value) {
            var optn = document.createElement("OPTION");
            optn.text = text;
            optn.value = value;
            selectbox.options.add(optn);
        }
        function ChkGDN(EDNID) {
            var Res = GDN.GDNProofs(EDNID);
            if (Res.value != "") {
                if (Res.value == 1) {
                    var Res1 = GDN.GDNItems(EDNID);
                    if (Res1.value <= 0) {
                        alert('No Goods Found in GDN ' + EDNID);
                    } else
                        window.location = "GDNPreReq.aspx?ID=0&EDNID=" + EDNID;
                }
                else {
                    var answer = confirm("Click OK to upload Invoice/Proof OR Cancel to skip uploading.")
                    if (answer)
                        window.location = "GdnItemsProof.aspx?ID=0&EDNID=" + EDNID;
                    else
                        window.location = "GDNPreReq.aspx?ID=0&EDNID=" + EDNID;
                }
            }
        }
        //        function ViewPO() {
        //            var POList = $get('<%=lstPODetails.ClientID %>');
        //            if (POList.value != "-1")
        //                window.open("ProPurchaseOrderPrint.aspx?id=" + POList.value + "&PON=2&tot=True", null, 'height=700px,width=1000px,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no');
        //            // window.location = "ProPurchaseOrderPrint.aspx?id=" + POList.value + "&PON=2&tot=True" ; //.aspx?ID=0&GDNID=" + GDNID;
        //            return false;
        //        }
        function ViewPO() {
            var POList = $get('<%=lstPODetails.ClientID %>');
            if (POList.value != "-1" && POList.value != "")
                window.open("ProPurchaseOrderPrint.aspx?id=" + POList.value + "&PON=2&tot=True", null, 'height=700px,width=1000px,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no');
            // window.location = "ProPurchaseOrderPrint.aspx?id=" + POList.value + "&PON=2&tot=True" ; //.aspx?ID=0&GDNID=" + GDNID;
            else
                alert('Select PO');
            return false;
        }
    </script>
    <%--<asp:UpdatePanel ID="upGDN" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <asp:Panel ID="pnlOuterTop" runat="server" CssClass="box box-primary">
        <asp:Panel ID="panel1" runat="server" CssClass="box box-primary" Style="margin-left: auto; margin-right: auto;
            margin-bottom: auto; margin-top: auto;">
            <div>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 183px">
                                                    <asp:Button ID="btnback" runat="server" Text="Back" CssClass="savebutton btn btn-success" OnClick="btnback_Click"
                                                        TabIndex="1" />
                                                    <br />
                                                    <asp:Label ID="lblSDNId" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px">
                                                    Worksite<span style="color: #CC3300">* :</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlWorkSite" runat="server" DataValueField="ID" DataTextField="Name"
                                                        AutoPostBack="true" AppendDataBoundItems="true" width="130px" CssClass="droplist"
                                                        TabIndex="2">
                                                    </asp:DropDownList>
                                                    <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender1" runat="server"
                                                        TargetControlID="ddlWorkSite" PromptText="Type to search" PromptCssClass="PromptText"
                                                        PromptPosition="Top" IsSorted="true">
                                                    </cc1:ListSearchExtender>
                                                    Fill Values From Last
                                                    <asp:TextBox ID="txtNoOfRec" runat="server" Width="40" Text="100" TabIndex="3"></asp:TextBox>
                                                    EDNs 
                                                    <asp:Button ID="BtnGo" Text="Filter" runat="server" CssClass="btn btn-primary"   AccessKey="i"
                                                        TabIndex="4" ToolTip="[Alt+i OR Alt+i+Enter]" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 183px">
                                                    Date of SRN :
                                                </td>
                                                <td>
                                                    <%--OnClientDateSelectionChanged="checkDate"--%>
                                                    <asp:TextBox ID="txtDate" runat="server" Width="70" CssClass="droplist" TabIndex="5"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" PopupPosition="Right"
                                                        Format="dd/MM/yyyy" runat="server">
                                                    </cc1:CalendarExtender>
                                                    Hours<asp:DropDownList ID="ddlstarttime" CssClass="droplist" width="40px" runat="server" TabIndex="6">
                                                        <%--<asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>--%>
                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    Minutes<asp:TextBox ID="txtMinutes" runat="server" Width="40" CssClass="droplist"
                                                        TabIndex="7"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlTimeFormat" CssClass="droplist" width="40px" runat="server" TabIndex="8">
                                                        <asp:ListItem Text="AM" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="PM" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 183px">
                                                    Vendor<span style="color: #CC3300">* :</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlVendor" runat="server" DataValueField="ID" DataTextField="Name"
                                                        AppendDataBoundItems="true" CssClass="droplist" AutoPostBack="true" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged"
                                                        TabIndex="9">
                                                    </asp:DropDownList>
                                                    <cc1:ListSearchExtender QueryPattern="Contains" ID="LSE" runat="server" TargetControlID="ddlVendor"
                                                        PromptText="Type to search" PromptCssClass="PromptText" PromptPosition="Top"
                                                        IsSorted="true">
                                                    </cc1:ListSearchExtender>
                                                    <asp:TextBox ID="txtVendor" runat="server" width="110px" TabIndex="10"></asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" WatermarkText="[Type to filter]"
                                                        runat="server" WatermarkCssClass="watermarked" TargetControlID="txtVendor">
                                                    </cc1:TextBoxWatermarkExtender>
                                                    <asp:Button ID="lnkAllVendors" runat="server" CssClass="btn btn-primary" Text="Filter" OnClick="lnkAllVendors_Click"
                                                        TabIndex="11"></asp:Button>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 183px">
                                                    Destination<span style="color: #CC3300">* :</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDestination" runat="server" AppendDataBoundItems="true"
                                                    CssClass="droplist" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                                                     <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender2" runat="server" 
                                                         TargetControlID="ddlDestination"
                                                        PromptText="Type to search" PromptCssClass="PromptText" PromptPosition="Top"
                                                        IsSorted="true">
                                                    </cc1:ListSearchExtender>
                                                    <asp:TextBox ID="txtDest" width="110px" runat="server"></asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="[Type to filter]"
                                                        runat="server"   WatermarkCssClass="watermarked" TargetControlID="txtDest">
                                                    </cc1:TextBoxWatermarkExtender>
                                                   <asp:Button ID="btnDest" runat="server" CssClass="btn btn-primary" Text="Filter" OnClick="btnDest_Click1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 183px">
                                                    Reminder<asp:CheckBox ID="chkReminder" AutoPostBack="true" runat="server" OnCheckedChanged="chkReminder_CheckedChanged"
                                                        TabIndex="12" />
                                                </td>
                                                <td id="tdReminder" runat="server" visible="false">
                                                    Valid From:<asp:TextBox ID="txtValidFrom" Width="70" runat="server" TabIndex="13"></asp:TextBox>
                                                    &nbsp; Valid Upto:<asp:TextBox ID="txtValidUpto" Width="70" runat="server" TabIndex="14"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtValidFrom" PopupPosition="Right"
                                                        Format="dd/MM/yyyy" runat="server">
                                                    </cc1:CalendarExtender>
                                                    &nbsp;Reminder Start before
                                                    <asp:TextBox ID="txtRemindDays" Text="0" Width="40" runat="server" TabIndex="15"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender3" TargetControlID="txtValidUpto" PopupPosition="Right"
                                                        Format="dd/MM/yyyy" runat="server">
                                                    </cc1:CalendarExtender>
                                                    &nbsp;Days
                                                </td>
                                            </tr>
                                            <tr id="trRemNote" runat="server" visible="false">
                                                <td>
                                                    Reminder Note
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtReminder" Width="300" runat="server" TabIndex="16"></asp:TextBox>
                                                    &nbsp; Due Date:
                                                    <asp:TextBox ID="txtDueDate" runat="server" Width="70" TabIndex="17"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtDueDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                                        PopupPosition="Right" TargetControlID="txtDueDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                    </td>
                                </tr>
                            </table>
                            <table class="Tablecontent" style="width: 314px">
                                <tr>
                                    <td class="InputHeaderLabel">
                                        Work Orders
                                        <asp:Button ID="btnGetPO" runat="server" CssClass="btn btn-primary" Text="Refresh" OnClick="btnGetPO_Click"
                                            AccessKey="b" TabIndex="18" ToolTip="[Alt+b OR Alt+b+Enter]" />
                                        <asp:Button ID="btnViewPO" runat="server" CssClass="btn btn-success" Text="View WO" OnClientClick="javascript:return ViewPO();"
                                            ToolTip="Select one WO on below listed WOs"   TabIndex="19" />
                                    </td>
                                    <td class="InputHeaderLabel" style="width: 250px">
                                        &nbsp;Items
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lstPODetails" runat="server" AutoPostBack="true" CssClass="droplist"
                                            DataTextField="Name" DataValueField="ID" Height="90" OnSelectedIndexChanged="lstPODetails_IndexChanged"
                                            SelectionMode="Single" Width="250" TabIndex="20"></asp:ListBox>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstItemDetails" runat="server" CssClass="droplist" DataTextField="Name"
                                            DataValueField="ID" Height="90" SelectionMode="Multiple" Width="250" TabIndex="21">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Button ID="btnAddDetails" runat="server" OnClick="btnAddDetails_OnClick" Text="Add"
                                            CssClass="savebutton btn btn-success" TabIndex="22" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="false" CssClass="gridview"
                                DataKeyNames="PodetID,Itemid,POID,SRNItemID" OnRowCommand="Grid_OnRowCommand">
                                <Columns>
                                    <asp:BoundField DataField="PoName" HeaderText="SO No &amp; Name " />
                                    <asp:BoundField DataField="Item" HeaderText="Item" />
                                    <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                    <asp:BoundField DataField="Qty" HeaderText="PO_Qty" />
                                    <asp:BoundField DataField="BalQty" HeaderText="Balance_Qty" />
                                    <asp:TemplateField HeaderText="Exe Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRelQty" OnTextChanged="QtyChanged" AutoPostBack="true" runat="server"  Text='<% #Eval("RelQty") %>' Width="40"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="40" HeaderText="Parts" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkParts" runat="server" CommandArgument='<% #Eval("Itemid") %>'
                                                CommandName="Edt" Text="Add"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="40" HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<% #Eval("SRNItemId") %>'
                                                CommandName="Del" Text="Delete" CssClass="anchor__grd dlt"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="AuID" runat="server" Text='<% #Eval("AuID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="Rate" runat="server" Text='<% #Eval("Rate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBQty" runat="server" Text='<%#Eval("BQty")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblState" runat="server" Text='<%#Eval("State") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPodetId" runat="server" Text='<%#Eval("PodetID") %>'></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="AuIDs" Text='<% #Eval("AuIDs") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="hdFlag" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <div id="dvParts" runat="server" visible="false">
                        </div>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="width: 233px">
                            <span style="display: block; text-align: left">Rep at Destination<span style="color: #CC3300">
                                                *</span></span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDestRepre" runat="server" AppendDataBoundItems="true" CssClass="droplist"
                                                DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                            <cc1:ListSearchExtender ID="LEDestRepre" runat="server" IsSorted="true" PromptCssClass="PromptText"
                                                PromptPosition="Top" PromptText="Type to search" QueryPattern="Contains" TargetControlID="ddlDestRepre" />
                            <asp:TextBox ID="txtDestRep" runat="server" Width="90"></asp:TextBox>                                                 
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtDestRep"
                                                    WatermarkCssClass="watermarked" WatermarkText="[Type to filter]">
                                                    </cc1:TextBoxWatermarkExtender>
                            <asp:Button ID="btnDestRep" runat="server" CssClass="btn btn-primary" Text="Filter" OnClick="btnDestRep_Click1" />
                        </td>
                    </tr>
                </table>
            </div>
            </tr>
            <tr>
                <td>
                    <table class="Tablecontent">
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" runat="server" CssClass="savebutton btn btn-success" Enabled="false" OnClick="btnSave_OnClick"
                                    OnClientClick="javascript:return Valid();" Text="Save" AccessKey="s" TabIndex="23"
                                    ToolTip="[Alt+s OR Alt+s+Enter]" />
                            </td>
                            <td>
                                <span style="color: #CC3300">* Fields are Mandatory</span><asp:HiddenField ID="hdIncrement"
                                    runat="server" Value="0" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table> </td> </tr> </table> </div>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
