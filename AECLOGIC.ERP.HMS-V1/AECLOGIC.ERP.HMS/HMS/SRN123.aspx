<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="SRN123.aspx.cs" Inherits="AECLOGIC.ERP.HMS.SRN123" %>
<%@ OutputCache Location="None" %>
<%@ Register Src="Help.ascx" TagName="Help" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
  <script language="javascript" type="text/javascript">

      function Valid() {

          //Date Check
          if (!chkDate("<%=txtDate.ClientID %>", "Vehicle Name", true, ""))
              return false;

          //Vendor
          if (!chkDropDownList("<%=ddlVendor.ClientID %>", "Vendor"))
              return false;
          //Origin
          if (!chkDropDownList("<%=ddlOrigin.ClientID %>", "Origin"))
              return false;

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
    <AEC:Topmenu ID="topmenu" runat="server" cssclass="topmenu" />
    <asp:UpdatePanel ID="upGDN" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlOuterTop" runat="server">
                <asp:Panel ID="panel1" runat="server" Style="margin-left: auto; margin-right: auto;
                    margin-bottom: auto; margin-top: auto;">
                    <div>
                        <tr>
                            <td>
                                <%--Service Dispatch Note(SDN)-> At Supplier Yard--%>
                                <asp:Label ID="lblSRNID" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnback" runat="server" Text="Back" CssClass="savebutton" OnClick="btnback_Click" />
                                                            <br />
                                                            <asp:Label ID="lblSDNId" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 233px">
                                                            Worksite<span style="color: #CC3300">*</span>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:DropDownList ID="ddlWorkSite" runat="server" DataValueField="ID" DataTextField="Name"
                                                                TabIndex="1" AutoPostBack="true" AppendDataBoundItems="true" CssClass="droplist">
                                                            </asp:DropDownList>
                                                            <cc1:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender1" runat="server"
                                                                TargetControlID="ddlWorkSite" PromptText="Type to search" PromptCssClass="PromptText"
                                                                PromptPosition="Top" IsSorted="true">
                                                            </cc1:ListSearchExtender>
                                                            Fill Values From Last
                                                            <asp:TextBox ID="txtNoOfRec" runat="server" Width="40" Text="100" TabIndex="2">
                                                            </asp:TextBox>
                                                            EDNs
                                                            <asp:Button ID="BtnGo" Text="Filter" runat="server" OnClick="BtnGo_Click" TabIndex="3" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Date of SRN
                                                        </td>
                                                        <td colspan="2">
                                                            <%--OnClientDateSelectionChanged="checkDate"--%>
                                                            <asp:TextBox ID="txtDate" runat="server" Width="70" CssClass="droplist" TabIndex="4"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" PopupPosition="Right"
                                                                Format="dd/MM/yyyy" runat="server">
                                                            </cc1:CalendarExtender>
                                                            Hours<asp:DropDownList ID="ddlstarttime" CssClass="droplist" runat="server" TabIndex="5">
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
                                                                TabIndex="6"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlTimeFormat" CssClass="droplist" runat="server" TabIndex="7">
                                                                <%-- <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>--%>
                                                                <asp:ListItem Text="AM" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="PM" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Carrier Vehicle Reg No<span style="color: #CC3300"></span>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:DropDownList ID="ddlVehicle" runat="server" DataValueField="ID" DataTextField="Name"
                                                                TabIndex="8" AppendDataBoundItems="true" CssClass="droplist">
                                                            </asp:DropDownList>
                                                            <cc1:ListSearchExtender QueryPattern="Contains" ID="LEVehicle" runat="server" TargetControlID="ddlVehicle"
                                                                PromptText="Type to search" PromptCssClass="PromptText" PromptPosition="Top">
                                                            </cc1:ListSearchExtender>
                                                            <asp:LinkButton ID="lnkVehicle" Text="Add New" runat="server" TabIndex="9" OnClientClick="window.showModalDialog('AddVehicle.aspx','','dialogWidth:690px; dialogHeight:650px;scrollbars=yes, center:yes');">
                                                            </asp:LinkButton>
                                                            <asp:TextBox ID="txtAllVeh" runat="server" TabIndex="10"></asp:TextBox>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="[Type to filter]"
                                                                runat="server" WatermarkCssClass="watermarked" TargetControlID="txtAllVeh">
                                                            </cc1:TextBoxWatermarkExtender>
                                                            <asp:Button ID="btnAllVeh" runat="server" Text="Filter" OnClick="btnAllVeh_Click"
                                                                TabIndex="11"></asp:Button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 233px">
                                                            Vendor<span style="color: #CC3300">*</span>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:DropDownList ID="ddlVendor" runat="server" DataValueField="ID" DataTextField="Name"
                                                                TabIndex="12" AutoPostBack="true" AppendDataBoundItems="true" CssClass="droplist"
                                                                OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <cc1:ListSearchExtender QueryPattern="Contains" ID="LSE" runat="server" TargetControlID="ddlVendor"
                                                                PromptText="Type to search" PromptCssClass="PromptText" PromptPosition="Top"
                                                                IsSorted="true">
                                                            </cc1:ListSearchExtender>
                                                            <asp:TextBox ID="txtVendor" runat="server" TabIndex="13"></asp:TextBox>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" WatermarkText="[Type to filter]"
                                                                runat="server" WatermarkCssClass="watermarked" TargetControlID="txtVendor">
                                                            </cc1:TextBoxWatermarkExtender>
                                                            <asp:Button ID="lnkAllVendors" runat="server" Text="Filter" OnClick="lnkAllVendors_Click"
                                                                TabIndex="14"></asp:Button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 233px">
                                                            Trip Sheet
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="txtTripSheet" runat="server" CssClass="droplist" Width="65px" TabIndex="15"></asp:TextBox>
                                                            <cc1:TextBoxWatermarkExtender ID="txtextTS" WatermarkText="[Trip Sheet]" runat="server"
                                                                WatermarkCssClass="watermarked" TargetControlID="txtTripSheet">
                                                            </cc1:TextBoxWatermarkExtender>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="trRC" visible="false">
                                                        <td style="width: 233px">
                                                            Royalty challana
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="txtroyaltychallan" runat="server" CssClass="droplist" Width="130px"
                                                                TabIndex="16"></asp:TextBox>
                                                            <cc1:TextBoxWatermarkExtender ID="txtextRC" WatermarkText="[Enter Royalty Challan]"
                                                                runat="server" WatermarkCssClass="watermarked" TargetControlID="txtroyaltychallan">
                                                            </cc1:TextBoxWatermarkExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 233px">
                                                            Origin<span style="color: #CC3300">*</span>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:DropDownList ID="ddlOrigin" runat="server" AppendDataBoundItems="true" TabIndex="17"
                                                                CssClass="droplist" DataTextField="Name" DataValueField="ID">
                                                            </asp:DropDownList>
                                                            <cc1:ListSearchExtender QueryPattern="Contains" ID="LEOrigin" runat="server" IsSorted="true"
                                                                PromptCssClass="PromptText" PromptPosition="Top" PromptText="Type to search"
                                                                TargetControlID="ddlOrigin">
                                                            </cc1:ListSearchExtender>
                                                            <a id="lnkNewSpaceType" href="#" tabindex="18" onclick="window.showModalDialog('NewDestination.aspx','','dialogWidth:380px; dialogHeight:250px; center:yes');">
                                                                Add New</a>
                                                            <asp:TextBox ID="txtOrigin" runat="server" Width="90" TabIndex="19"></asp:TextBox>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtOrigin"
                                                                WatermarkCssClass="watermarked" WatermarkText="[Type to filter]">
                                                            </cc1:TextBoxWatermarkExtender>
                                                            <asp:Button ID="lnkAllOrigin" runat="server" Text="Filter" OnClick="lnkAllOrigin_Click"
                                                                TabIndex="20" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 233px">
                                                            Destination<span style="color: #CC3300">*</span>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:DropDownList ID="ddlDestination" runat="server" AppendDataBoundItems="true"
                                                                TabIndex="21" CssClass="droplist" DataTextField="Name" DataValueField="ID">
                                                            </asp:DropDownList>
                                                            <cc1:ListSearchExtender QueryPattern="Contains" ID="LEDestination" runat="server"
                                                                IsSorted="true" PromptCssClass="PromptText" PromptPosition="Top" PromptText="Type to search"
                                                                TargetControlID="ddlDestination">
                                                            </cc1:ListSearchExtender>
                                                            <asp:TextBox ID="txtDest" runat="server" Width="90" TabIndex="22"></asp:TextBox>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtDest"
                                                                WatermarkCssClass="watermarked" WatermarkText="[Type to filter]">
                                                            </cc1:TextBoxWatermarkExtender>
                                                            <asp:Button ID="btnDest" runat="server" Text="Filter" OnClick="btnDest_Click" TabIndex="23" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 233px">
                                                            Rep at Origin
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:DropDownList ID="ddlOriginRepre" runat="server" AppendDataBoundItems="true"
                                                                TabIndex="24" CssClass="droplist" DataTextField="Name" DataValueField="ID">
                                                            </asp:DropDownList>
                                                            <cc1:ListSearchExtender QueryPattern="Contains" ID="LEOriginRepre" runat="server"
                                                                IsSorted="true" PromptCssClass="PromptText" PromptPosition="Top" PromptText="Type to search"
                                                                TargetControlID="ddlOriginRepre">
                                                            </cc1:ListSearchExtender>
                                                            <asp:TextBox ID="txtOriRep" runat="server" TabIndex="25"></asp:TextBox>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtOriRep"
                                                                WatermarkCssClass="watermarked" WatermarkText="[Type to filter]">
                                                            </cc1:TextBoxWatermarkExtender>
                                                            <asp:Button ID="btnOriRep" runat="server" Text="Filter" OnClick="btnOriRep_Click"
                                                                TabIndex="26" />
                                                        </td>
                                                    </tr>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="Tablecontent" style="width: 314px">
                                        <tr>
                                            <td class="InputHeaderLabel">
                                                Service Orders
                                                <asp:Button ID="btnGetPO" runat="server" Text="Refresh" OnClick="btnGetPO_Click"
                                                    TabIndex="27" />
                                                <asp:Button ID="btnViewPO" runat="server" Text="View WO" OnClientClick="javascript:return ViewPO();"
                                                    TabIndex="28" ToolTip="Select one WO on below listed WOs" OnClick="btnViewPO_Click" />
                                            </td>
                                            <td class="InputHeaderLabel" style="width: 250px">
                                                Service Items
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ListBox ID="lstPODetails" runat="server" AutoPostBack="true" CssClass="droplist"
                                                    TabIndex="29" DataTextField="Name" DataValueField="ID" Height="90" OnSelectedIndexChanged="lstPODetails_IndexChanged"
                                                    SelectionMode="Single" Width="250"></asp:ListBox>
                                            </td>
                                            <td>
                                                <asp:ListBox ID="lstItemDetails" runat="server" CssClass="droplist" DataTextField="Name"
                                                    TabIndex="30" DataValueField="ID" Height="90" SelectionMode="Multiple" Width="250">
                                                </asp:ListBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Button ID="btnAddDetails" runat="server" OnClick="btnAddDetails_OnClick" Text="Add"
                                                    TabIndex="31" CssClass="savebutton" />
                                                (Select Goods from the list and click Add)<%-- <asp:HiddenField ID="hdnGDN" runat="server" />--%></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="false" CssClass="gridview"
                                        DataKeyNames="PodetID,Itemid,POID,SRNItemID" OnRowCommand="Grid_OnRowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="PoName" HeaderText="WO No &amp; Name " />
                                            <asp:BoundField DataField="Item" HeaderText="Item" />
                                            <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                            <asp:BoundField DataField="Qty" HeaderText="WO_Qty" />
                                            <asp:BoundField DataField="BalQty" HeaderText="Balance_Qty" />
                                            <asp:TemplateField HeaderText="Exe Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRelQty" runat="server" Text='<% #Eval("RelQty") %>' Width="40"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="AjtxtRelQty" TargetControlID="txtRelQty" FilterType="Custom,Numbers"
                                                        ValidChars="." runat="server">
                                                    </cc1:FilteredTextBoxExtender>
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
                                                        CommandName="Del" Text="Delete"></asp:LinkButton>
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
                                <td>
                                    <table>
                                        <tr>
                                            <td style="width: 233px">
                                                <span style="display: block; text-align: left">Rep at Destn<span style="color: #CC3300">
                                                    *</span></span>
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="ddlDestRepre" runat="server" AppendDataBoundItems="true" CssClass="droplist"
                                                    TabIndex="32" DataTextField="Name" DataValueField="ID">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender QueryPattern="Contains" ID="LEDestRepre" runat="server" IsSorted="true"
                                                    PromptCssClass="PromptText" PromptPosition="Top" PromptText="Type to search"
                                                    TargetControlID="ddlDestRepre" />
                                                <asp:TextBox ID="txtDestRep" runat="server" Width="90" TabIndex="33"></asp:TextBox>
                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="txtDestRep"
                                                    WatermarkCssClass="watermarked" WatermarkText="[Type to filter]">
                                                </cc1:TextBoxWatermarkExtender>
                                                <asp:Button ID="btnDestRep" runat="server" OnClick="btnDestRep_Click" Text="Filter"
                                                    TabIndex="34" />
                                            </td>
                                        </tr>
                                        <tr id="trRD" visible="false" runat="server">
                                            <td style="width: 233px">
                                                Route Details
                                            </td>
                                            <td style="width: 255px">
                                                <asp:TextBox ID="txtRouteDesrciption" runat="server" CssClass="droplist" TextMode="MultiLine"
                                                    TabIndex="35" Width="500"></asp:TextBox>
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
                                                    RepeatColumns="3">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_OnClick" Text="Parts"
                                                    CssClass="savebutton" />
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
                                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_OnClick" OnClientClick="javascript:return Valid();"
                                                    TabIndex="36" Text="Save" Enabled="false" CssClass="savebutton" />
                                            </td>
                                            <td>
                                                <span style="color: #CC3300">* Fields are Mandatory</span>
                                                <asp:HiddenField ID="hdIncrement" runat="server" Value="0" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        </td> </tr> </table>
                    </div>
                    <div class="UpdateProgressCSS">
                        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
                            <ProgressTemplate>
                                <asp:Panel ID="pnlFirst" CssClass="overlay" runat="server">
                                    <asp:Panel ID="pnlSecond" CssClass="loader" runat="server">
                                        <img src="IMAGES/Loading.gif" alt="update is in progress" />
                                        <input id="btnCacel" onclick="CancelAsyncPostBack()" type="button" value="Cancel" /></asp:Panel>
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

