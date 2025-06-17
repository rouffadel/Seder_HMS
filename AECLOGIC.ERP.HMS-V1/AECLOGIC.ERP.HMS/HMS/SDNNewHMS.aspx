<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Templates/CommonMaster.master" CodeBehind="SDNNewHMS.aspx.cs" Inherits="AECLOGIC.ERP.HMS.SDNNewHMS" %>

<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/Help.ascx" TagName="Help" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="server">
    <script src="JScript.js" language="javascript" type="text/javascript"></script> 

    <script language="javascript" type="text/javascript">

        function Valid() {
            //Date Check
            if (!chkDate("<%=txtDate.ClientID %>", "Vehicle Name", true, ""))
                return false;

            //Vendor
            if (!chkDropDownList("<%=ddlVendor.ClientID %>", "Vendor"))
                return false;
            //Origin
           <%-- if (!chkDropDownList("<%=ddlOrigin.ClientID %>", "Origin"))
                return false;--%>

            //Destination
            if (!chkDropDownList("<%=ddlDestination.ClientID %>", "Destination"))
                return false;


            //Destination Representative
            if (!chkDropDownList("<%=ddlDestRepre.ClientID %>", "Destination Representative"))
                return false;
            //routeDescription
            //            if (!Reval("<%=txtRouteDesrciption.ClientID %>", "Route", true, ''))
            //                return false;


            return true;
        }

        function CancelAsyncPostBack() {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            //alert(prm);
            if (prm.get_isInAsyncPostBack()) {
                prm.abortPostBack();
                }
        }

        function FilterEmployees(WorkSite, Origi, Desti)
        {
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
                var Res = SaleServices.SearchVendor(Vendor.value);
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
                var Res = SaleServices.SearchVehicle(Vehicle.value);
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

        function ChkSSN(SSNID) {
            var Res = SaleServices.SSNProofs(SSNID);
            if (Res.value != "") {
                if (Res.value == 1) {
                    var Res1 = SaleServices.SSNItems(SSNID);
                    if (Res1.value <= 0) {
                        alert('No Goods Found in GDN ' + SSNID);
                    } else
                        window.location = "GDNPreReq.aspx?ID=0&GDNID=" + SSNID;
                }
                else {
                    var answer = confirm("Click OK to upload Invoice/Proof OR Cancel to skip uploading.")
                    if (answer)
                        window.location = "GdnItemsProof.aspx?ID=0&GDNID=" + SSNID;
                    else
                        window.location = "GDNPreReq.aspx?ID=0&GDNID=" + SSNID;
                }
            }
        }

            function ViewPO() {
            var POList = $get('<%=lstPODetails.ClientID %>');
            if (POList.value != null && POList.value != "") {
                if (POList.value != "-1")
                    window.open("ProPurchaseOrderPrint.aspx?id=" + POList.value + "&PON=2&tot=True", null, 'height=700px,width=1000px,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no');
                // window.location = "ProPurchaseOrderPrint.aspx?id=" + POList.value + "&PON=2&tot=True" ; //.aspx?ID=0&GDNID=" + GDNID;
            }
            else
                alert('No WO selected !')
            return false;
        }

    </script>

    <asp:UpdatePanel ID="upGDN" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="DivBorderSilver" style="overflow: auto; height: inherit; width: inherit;">
                <asp:Panel ID="pnlOuterTop" runat="server" CssClass="box box-primary">
                    <asp:Panel ID="panel1" runat="server" Style="margin-left: auto; margin-right: auto;
                    margin-bottom: auto; margin-top: auto;" CssClass="box box-primary">
                        <div>
                            <table style="width: 100%">
                                
                      
                                                                <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" Visible="false" CssClass="btn btn-primary" />
                                                                <asp:Label ID="lblSRNID" runat="server" Visible="false"></asp:Label>
                                                         
                                
                                                            Worksite:<span style="color: #CC3300">*</span>
                                                          
                                                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:DropDownList ID="ddlWorkSite" runat="server" DataValueField="ID" DataTextField="Name"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="Vendor_SelectedIndexChanged" AppendDataBoundItems="true" ToolTip="[Alt+w OR Alt+w+Enter]" AccessKey="w"
                                                                    CssClass="droplist" Width="80"></asp:DropDownList>
                                                                    <cc1:ListSearchExtender QueryPattern="Contains" ID="LEWorkSite" runat="server" TargetControlID="ddlWorkSite"
                                                                    PromptText="Type to search" PromptCssClass="PromptText" PromptPosition="Top"
                                                                    IsSorted="true" />
                                

                                <br />
                                
                                                                Fill values from last:&nbsp;&nbsp;&nbsp;<%--</td>--%>
                                                                <asp:TextBox ID="txtNoOfRec" runat="server" Width="40" Text="100" TabIndex="1">
                                                                </asp:TextBox>
                                                           
                                                               &nbsp; GDNs&nbsp;

                                <br />
                                 
                                                            Date of PSRN: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                <asp:TextBox ID="txtDate" runat="server" Width="70" CssClass="droplist" AccessKey="t" ToolTip="[Alt+t OR Alt+t+Enter]"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" PopupPosition="Right"
                                                                Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate" runat="server">
                                                            </cc1:CalendarExtender>
                                                             
                                                                Hours
                                                            <asp:DropDownList ID="ddlstarttime" CssClass="droplist" runat="server" Width="40">
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
                                                                <asp:ListItem Text="12" Value="0"></asp:ListItem>
                                                                     </asp:DropDownList>
                                                               
                                                           
                                                                Minutes<asp:TextBox ID="txtMinutes" runat="server" Width="40" CssClass="droplist"></asp:TextBox>
                                                                <asp:DropDownList ID="ddlTimeFormat" CssClass="droplist" runat="server"  Width="40">
                                                                    <asp:ListItem Text="AM" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="PM" Value="2"></asp:ListItem>
                                                                </asp:DropDownList>
                                     
                                <br />
                               
                                                      
                                                                 Vehicle Reg No:<span style="color: #CC3300"></span>
                                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:DropDownList ID="ddlVehicle" runat="server" DataValueField="ID" DataTextField="Name"
                                                                AppendDataBoundItems="true" CssClass="droplist" AccessKey="h" ToolTip="[Alt+h OR Alt+h+Enter]"></asp:DropDownList>
                                                                 <cc1:ListSearchExtender QueryPattern="Contains" ID="LEVehicle" runat="server" TargetControlID="ddlVehicle"
                                                                PromptText="Type to search" PromptCssClass="PromptText" PromptPosition="Top"
                                                                IsSorted="true" />
                                                                <asp:LinkButton ID="lnkVehicle" Text="Add New" runat="server" OnClientClick="window.showModalDialog('AddVehicle.aspx','','dialogWidth:690px; dialogHeight:650px;scrollbars=yes, center:yes');" CssClass="btn btn-primary"></asp:LinkButton>
                                                                <asp:TextBox ID="txtAllVeh" Width="90" runat="server"></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtAllVeh"
                                                                WatermarkCssClass="watermarked" WatermarkText="[Type to filter]">
                                                            </cc1:TextBoxWatermarkExtender>
                                                            <asp:Button ID="btnAllVeh" runat="server" Text="Filter" CssClass="btn btn-primary" OnClick="btnAllVeh_Click" />
                                    
                                <br />
                                 
                                                           
                                                                Vendor:<span style="color: #CC3300">*</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                          
                                                                <asp:DropDownList ID="ddlVendor" runat="server"  DataValueField="ID" DataTextField="Name"
                                                                    AutoPostBack="true" AppendDataBoundItems="true" CssClass="droplist" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" AccessKey="v" ToolTip="[Alt+v OR Alt+v+Enter]"></asp:DropDownList>
                                                                 <cc1:ListSearchExtender QueryPattern="Contains" ID="LSE" runat="server" TargetControlID="ddlVendor"
                                                                PromptText="Type to search" PromptCssClass="PromptText" PromptPosition="Top"
                                                                IsSorted="true" />
                                                                <asp:TextBox ID="txtVendor" Width="90" runat="server"></asp:TextBox>
                                                                 <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtVendor"
                                                                WatermarkCssClass="watermarked" WatermarkText="[Type to filter]">
                                                            </cc1:TextBoxWatermarkExtender>
                                                                <asp:Button ID="lnkAllVendors" runat="server" Text="Filter" OnClick="lnkAllVendors_Click" CssClass="btn btn-primary" />
                                    
                                                        
                                <br />
                               
                                                     Trip Sheet:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                               
                                                     <asp:TextBox ID="txtTripSheet" runat="server" CssClass="droplist" Width="65px" ></asp:TextBox>
                                                     <cc1:TextBoxWatermarkExtender ID="txtextTS" WatermarkText="[Trip Sheet]" runat="server"
                                                    WatermarkCssClass="watermarked" TargetControlID="txtTripSheet">
                                                </cc1:TextBoxWatermarkExtender>
                                  
                                <br />
                               
                                              
                                                    Invoice No:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                              
                                                    <asp:TextBox ID="txtInvoice" runat="server"></asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="[Invoice No]" runat="server"
                                                    WatermarkCssClass="watermarked" TargetControlID="txtInvoice">
                                                    </cc1:TextBoxWatermarkExtender>
                                    
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trRC" visible="false">
                                                <td style="width: 233px">
                                                    Royalty challan
                                                </td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtroyaltychallan" runat="server"  CssClass="droplist" Width="130px" ></asp:TextBox>
                                                    <cc1:TextBoxWatermarkExtender ID="txtextRC" WatermarkText="[Enter Royalty Challan]"
                                                    runat="server" WatermarkCssClass="watermarked" TargetControlID="txtroyaltychallan">
                                                </cc1:TextBoxWatermarkExtender>
                                                </td>

                                            </tr>
                                        
                                <br />
                                                    Destination<span style="color: #CC3300">*</span>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                           
                                                <asp:DropDownList ID="ddlDestination" runat="server" AppendDataBoundItems="true"
                                                    CssClass="droplist" DataTextField="Name" DataValueField="ID" AccessKey="l" ToolTip="[Alt+l OR Alt+l+Enter]"></asp:DropDownList>
                                                <cc1:ListSearchExtender QueryPattern="Contains" ID="LEDestination" runat="server"
                                                    IsSorted="true" PromptCssClass="PromptText" PromptPosition="Top" PromptText="Type to search"
                                                    TargetControlID="ddlDestination" />
                                                <asp:TextBox ID="txtDest" runat="server" Width="90"></asp:TextBox>
                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtDest"
                                                    WatermarkCssClass="watermarked" WatermarkText="[Type to filter]">
                                                </cc1:TextBoxWatermarkExtender>
                                                <asp:Button ID="btnDest" runat="server" Text="Filter" OnClick="btnDest_Click"  CssClass="btn btn-primary"/>
                                            

                                        </table>
                                    </td>
                                </tr>
                            </table> 

                            <table class="Tablecontent" style="width: 30%">
                                <tr>
                                    <td class="InputHeaderLabel">
                                        Service Orders &nbsp;<asp:Button ID="btnViewPO" runat="server" Text="View WO" OnClientClick="javascript:return ViewPO();"
                                            ToolTip="[Alt+p or Alt+p+Enter]"  AccessKey="p"  CssClass="btn btn-primary" />
                                        <asp:Button ID="btnGetPO" runat="server" Text="Refresh" ToolTip="[Alt+q or Alt+q+Enter]"  AccessKey="q" Width="70" OnClick="btnGetPO_Click" CssClass="btn btn-primary" />
                                    </td>
                                      <td class="InputHeaderLabel" style="width: 250px">
                                        Goods
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lstPODetails" runat="server" AutoPostBack="true" CssClass="droplist"
                                        DataTextField="Name" DataValueField="ID" Height="90" OnSelectedIndexChanged="lstPODetails_IndexChanged"
                                            SelectionMode="Single" Width="250" AccessKey="j" ToolTip="[Alt+j OR Alt+j+Enter]" ></asp:ListBox>

                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstItemDetails" runat="server" CssClass="droplist" DataTextField="Name"
                                        DataValueField="ID" Height="90" SelectionMode="Multiple" Width="250" AccessKey="g" ToolTip="[Alt+g OR Alt+g+Enter]"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Button ID="btnAddDetails" runat="server" OnClick="btnAddDetails_Click" ext="Add"
                                        CssClass="btn btn-primary" AccessKey="a" ToolTip="[Alt+a OR Alt+a+Enter]" />
                                        (Select Goods from the list and click Add)<%-- <asp:HiddenField ID="hdnGDN" runat="server" />--%>
                                    </td>
                                </tr>
                            </table> 

                            <table>
                                 <tr>
                                    <td>
                                        <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="false" CssClass="gridview"
                                    DataKeyNames="PodetID,Itemid,POID,SRNItemID" OnRowCommand="Grid_OnRowCommand" >
                                            <Columns>
                                                <asp:BoundField DataField="PoName" HeaderText="WO No &amp; Name " />
                                                <asp:BoundField DataField="Item" HeaderText="Item" />
                                                <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                                <asp:BoundField DataField="Qty" HeaderText="WO_Qty" />
                                                <asp:BoundField DataField="BalQty" HeaderText="Balance_Qty" />
                                                <asp:TemplateField HeaderText="Exe Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRelQty" runat="server" Text='<% #Eval("RelQty") %>' Width="40"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FTERelQty" TargetControlID="txtRelQty" FilterType="Custom" ValidChars="0123456789." runat="server">
                                                       </cc1:FilteredTextBoxExtender>
                                                    </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="40" HeaderText="Parts" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkParts" runat="server" CommandArgument='<% #Eval("Itemid") %>'
                                                    CommandName="Edt" Text="Add" CssClass="btn btn-primary"></asp:LinkButton>
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
                                                        <asp:Label ID="lblBQty"  runat="server" Text='<%#Eval("BQty")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblState" runat="server" Text='<%#Eval("State") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPodetId" runat="server" Text='<%#Eval("PodetID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="AuIDs" runat="server" ext='<% #Eval("AuIDs") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hdFlag" runat="server" />
                                    </td>
                                 </tr>

                                <tr>
                                  <td>
                                    <table>
                                         <tr>
                                            <td style="width: 233px">
                                            <span style="display: block; text-align: left">Rep at Destination<span style="color: #CC3300">
                                                *</span></span>
                                               </td>
                                             <td>
                                                 <asp:DropDownList ID="ddlDestRepre" runat="server" AppendDataBoundItems="true" CssClass="droplist"
                                                DataTextField="Name" DataValueField="ID" AccessKey="k" ToolTip="[Alt+k or Alt+k+Enter]"></asp:DropDownList>
                                                 <cc1:ListSearchExtender ID="LEDestRepre" runat="server" IsSorted="true" PromptCssClass="PromptText"
                                                PromptPosition="Top" PromptText="Type to search" QueryPattern="Contains" TargetControlID="ddlDestRepre" />
                                                 <asp:TextBox ID="txtDestRep" runat="server" Width="90"></asp:TextBox>
                                                 <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtDestRep"
                                                    WatermarkCssClass="watermarked" WatermarkText="[Type to filter]">
                                                    </cc1:TextBoxWatermarkExtender>
                                                 <asp:Button ID="btnDestRep" runat="server" OnClick="btnDestRep_Click" Text="Filter" CssClass="btn btn-primary" />
                                             </td>
                                             </tr>
                                        <tr id="trRD" runat="server" visible="false">
                                             <td style="width: 233px">
                                             Route Details
                                            </td>
                                            <td style="width: 255px">
                                                <asp:TextBox ID="txtRouteDesrciption" runat="server" CssClass="droplist" TextMode="MultiLine"
                                                Width="500"></asp:TextBox>
                                                <cc1:TextBoxWatermarkExtender ID="txtWM3" runat="server" TargetControlID="txtRouteDesrciption"
                                                WatermarkCssClass="watermarked" WatermarkText="[Describe Route from the place of goods origin to the place of goods destination with prominent landmarks/road names/places for easy tracking.]">
                                            </cc1:TextBoxWatermarkExtender>
                                            </td>
                                        </tr>
                                        </table>
                                      </td>
                                </tr>

                                <tr>
                                    <div id="dvParts" runat="server" visible="false">
                                        <table>
                                            <tr>
                                                <td class="InputHeaderLabel" valign="top">
                                                    Accompanying Parts
                                                </td>
                                                <td>
                                                    <asp:CheckBoxList ID="chkList" runat="server" DataTextField="Name" DataValueField="ID"
                                                RepeatColumns="3"></asp:CheckBoxList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Parts"
                                                CssClass="btn btn-primary" />

                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </tr>

                                <tr>
                                    <td>
                                        <table class="Tablecontent">
                                            <tr>
                                                <td style="width: 70px">
                                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" OnClientClick="javascript:return Valid();"
                                                        Text="Save" Enabled="false" Width="82px" CssClass="btn btn-success" AccessKey="s" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                                </td>
                                                <td>
                                                    <span style="color: #CC3300">* Fields are Mandatory</span><asp:HiddenField ID="hdIncrement" runat="server" Value="0" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                            </table>

                        </div>
                        <div class="UpdateProgressCSS">
                            <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
                                <ProgressTemplate>
                                    <asp:Panel ID="pnlFirst" CssClass="box box-primary" runat="server">
                                        <asp:Panel ID="pnlSecond" CssClass="box box-primary" runat="server">
                                            <img src="IMAGES/Searching.gif" alt="update is in progress" />
                                            <input id="btnCacel" onclick="CancelAsyncPostBack()" type="button" value="Cancel"  class="btn btn-danger"/>
                                        </asp:Panel>
                                    </asp:Panel>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
