<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Templates/CommonMaster.master" CodeBehind="MonthlyPaymentsProcessedV4.aspx.cs"
     Inherits="AECLOGIC.ERP.HMSV1.MonthlyPaymentsProcessedV4V1"%>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="server">
     <script language="javascript" type="text/javascript">

        <%--  function controlEnter(event) {
              // alert('hello');
              var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
              if (keyCode == 13) {
                  // alert("Enter fired");
                  document.getElementById('<%= btnSearch.ClientID %>').click();
                  return false;
              }
              else {
                  return true;
              }
          }--%>
          function GetEmpID(source, eventArgs) {
              var HdnKey = eventArgs.get_value();
              document.getElementById('<%=txtEmpNameHidden.ClientID %>').value = HdnKey;
          }

        <%--function EnterEvent(e) {
            if (e.keyCode == 13) {
                __doPostBack('<%=btnSearch.UniqueID%>', "");
            }
        }--%>
          function DisplayMonthYear() {
              var Result = AjaxDAL.GetStartDate();
              //var ddl = document.getElementById("<%=ddlMonth.ClientID%>");
              //var SelVal = ddlMonth.options[ddl.selectedIndex].text;
              //var SelVal = ddlMonth.options[ddlMonth.selectedIndex].value;
              //alert(SelVal); //SelVal is the selected Value
              var frommonth = ddlMonth.options[ddlMonth.selectedIndex].text;
              var fromyear = ddlYear.options[ddlYear.selectedIndex].text;
              var fromdate = '21' + '/' + frommonth + '/' + fromyear;
              var todate;
              if (ddlMonth.options[ddlMonth.selectedIndex].value == 12) {
                  //todate=
              }
              else {

              }
              document.getElementById("txtToDate").value = fromdate;
          }
    </script>
     <asp:UpdatePanel ID="SalariesUpdPanel" runat="server">
        <ContentTemplate>
        
         <table  border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
                                <td>
                                    <cc1:Accordion ID="SalariesAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="SalariesAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        
                                                          
                                                               
                                                                <tr>
                                                                   <td> 
                                                              
                                                            
                                                         Month:
                                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="droplist" TabIndex="3" AccessKey="2"
                                                                    ToolTip="[Alt+2]" Width="90">
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
                                                              Year:
                                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="droplist" TabIndex="4" ToolTip="[Alt+3]"
                                                                    AccessKey="3"  Width="90">
                                                                </asp:DropDownList>
                                                                   
                                                                  
                                                                  <asp:Button ID="btnfetch" Width="70" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnfetch_Click" />
                                                                       <%--<asp:Button ID="btnmisngemp" Text="Missing Employees" Width="100" runat="server" CssClass="btn btn-danger" OnClick="btnmisngemp_Click" />--%>

                                                            </td>
                                                             </tr>
                                                        <tr>
                                                             <td class="pageheader" style="text-align:left">
                                                                     <asp:LinkButton ID="lnkExpandControlID" Style="vertical-align: middle; border: 0px;"
                                                                         runat="server">
                                                                         <asp:Image ID="imgImageControlID" ImageAlign="AbsBottom" runat="server"></asp:Image>
                                                                         <asp:Label ID="lblTextLabelID" runat="server"></asp:Label>
                                                            </asp:LinkButton>
                                                                <cc1:CollapsiblePanelExtender ID="cpe" runat="Server" SuppressPostBack="true" TargetControlID="pnlTaskDetails"
                                                                    CollapsedSize="0" ExpandedSize="250" Collapsed="True" ExpandControlID="lnkExpandControlID"
                                                                    CollapseControlID="lnkExpandControlID" AutoCollapse="false" AutoExpand="false"
                                                                    ScrollContents="True" TextLabelID="lblTextLabelID" CollapsedText="Instructions..."
                                                                    ExpandedText=" Hide Instructions" ImageControlID="imgImageControlID" ExpandedImage="~/Images/dashminus.gif"
                                                                    CollapsedImage="~/Images/dashplus.gif" ExpandDirection="Vertical" />
                                                                <asp:Panel ID="pnlTaskDetails" runat="server" CssClass="box box-primary">
                                                                            <table>
                                                                                <tr>
                                                                                    <td style="text-align:left">
                                                             1. Check you <asp:HyperLink ID="AbsentPenalties" runat="server" Text="Absent Penalties" Font-Size="12px" Font-Bold="true" Target="_blank" NavigateUrl="~/HMS/AbsPenalities.aspx"></asp:HyperLink>
                                                                                      , <asp:HyperLink ID="EmployeePenalties" runat="server" Text="Employee Penalties" Font-Size="12px" Font-Bold="true" Target="_blank" NavigateUrl="~/HMS/EmpPenalties.aspx"></asp:HyperLink> 
                                                                                         and <asp:HyperLink ID="OverTime" runat="server" Text="Over Time" Font-Size="12px" Font-Bold="true" Target="_blank" NavigateUrl="~/HMS/OTPayments.aspx"></asp:HyperLink>  
                                                                                         are SYNCED before you Generate Salary Calculations.<br />
                                                                                          2. The absent penalties are calculated based on days elapsed between 21st of Last Month to 20th of this Month. <br />
                                                                                          3. Salary is calculated based on Calendar Month days of present.

                                                                                    </td>
                                                                                    </tr>                                                                                                                                                                                                                                                            
                                                                                </table>
                                                                                  <table>
                                                                                    <tr>
                                                                                    <td>
                                                                                          <asp:LinkButton ID="lblAbC" Font-Underline="false" runat="server" Text="Absent Penalties,Employee Penalties & Over Time Month" OnClick="lblAbC_Click"></asp:LinkButton>
                                                                                        <br />
                                                                                         <asp:Table ID="tblAtt" Width="80%" runat="server" CssClass="item-a" BorderWidth="2" GridLines="Both">
                                                                                            </asp:Table>
                                                                                        <asp:LinkButton Visible="false" ID="lblCalenderstrip" Font-Underline="false" runat="server" Text="Payroll of Month" OnClick="lblCalenderstrip_Click"></asp:LinkButton>
                                                                                           
                                                                                        <asp:Table ID="tblPay" Width="80%" runat="server" CssClass="item-a" BorderWidth="2" GridLines="Both">
                                                                                            </asp:Table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                       </asp:Panel>
                                                                
                                                               
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

              <table id="tblunsync" runat="server" visible="false">
                        <tr>
                            <td>
                                <asp:GridView ID="gvunsync" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%"
                                    OnRowCommand="gvunsync_RowCommand" OnRowDataBound="gvunsync_RowDataBound" EmptyDataText="No Records Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ws" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblwsid" runat="server" Text='<%# Eval("Site_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Worksite">
                                            <ItemTemplate>
                                                <asp:Label ID="lblws" runat="server" Text='<%# Eval("Site_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Year">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlyeargrid" Enabled="false" runat="server" Width="60" DataTextField="Name" DataValueField="ID" CssClass="droplist" DataSource='<%# bindyear()%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Month">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlmonthgrid" runat="server" Width="100" CssClass="droplist" Enabled="false">
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
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Generated Datetime">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllastsyncgrid" runat="server" Text='<%# Eval("date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkCompute" runat="server" CssClass="btn btn-primary" Text="Generate" CommandName="com"
                                                    CommandArgument='<%#Eval("Site_ID")%>' Enabled='<%# !Convert.ToBoolean(Eval("CanLock")) %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Str Nos ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStrNos" runat="server" Text='<%# Eval("StrNos") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                         <asp:TemplateField HeaderText="MissNos">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkMissNo" Text='<%# Eval("MissNos") %>' CssClass="btn btn-primary" runat="server" CommandName="MissNo" CommandArgument='<%#Eval("Site_ID") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Reason">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FS">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFS" Text='<%# Eval("FS") %>' CssClass="btn btn-danger" runat="server" CommandName="FS" CommandArgument='<%#Eval("Site_ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Zero Payble Days">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkZeroPayableDays" Text='<%# Eval("ZeroPDays") %>' CssClass="btn btn-danger" runat="server" CommandName="ZPD" CommandArgument='<%#Eval("Site_ID") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Basic InActive">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkWages" Text='<%# Eval("Wages") %>' CssClass="btn btn-danger" runat="server" CommandName="Wages" CommandArgument='<%#Eval("Site_ID") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <uc1:Paging ID="Paging1" runat="server" NoOfPages="1" CurrentPage="1" Visible="False" />
                            </td>
                        </tr>

                    </table>
          <div id="msgempy" runat="server" visible="true">
                        <table>
                            <asp:Label ID="lblmsg1" runat="server" Text="Missing Employees" Font-Size="16px" ForeColor="red" Font-Bold="true" Visible="false"></asp:Label>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvmsngemp" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%"
                                        EmptyDataText="No Records Found">
                                        <Columns>
                                            <asp:BoundField DataField="Empid" HeaderText="EmpID" Visible="true" />
                                            <asp:BoundField DataField="empname" HeaderText="Employee Name" Visible="true" ItemStyle-Width="50%"/>
                                            <asp:BoundField DataField="site_name" HeaderText="Worksite" Visible="true" ItemStyle-Width="20%"/>
                                            <asp:BoundField DataField="Description" HeaderText="Description" Visible="true" ItemStyle-Width="30%"/>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <uc1:Paging ID="Pagingmsg" runat="server" NoOfPages="1" CurrentPage="1" Visible="False" />
                                </td>

                            </tr>
                        </table>
                    </div>
        <div id="fsemp" runat="server" visible="true">
                <table>
                    <asp:Label ID="lblsFS" runat="server" Text="FS Employees" Font-Size="16px" ForeColor="red" Font-Bold="true" Visible="false"></asp:Label>
                    <tr>
                        <td>
                            <asp:GridView ID="gvfsemp" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%"
                                EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:BoundField DataField="Empid" HeaderText="EmpID" Visible="true" />
                                    <asp:BoundField DataField="empname" HeaderText="Employee Name" Visible="true" ItemStyle-Width="50%" />
                                    <asp:BoundField DataField="site_name" HeaderText="Worksite" Visible="true" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" Visible="true" ItemStyle-Width="30%" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:Paging ID="Pagingfs" runat="server" NoOfPages="1" CurrentPage="1" Visible="False" />
                        </td>

                    </tr>
                </table>
            </div>
            <div id="zeroPayabledaysemp" runat="server" visible="true">
                <table>
                    <asp:Label ID="lblmsg" runat="server" Text="Zero Payable Days Employees" Font-Size="16px" ForeColor="red" Font-Bold="true" Visible="false"></asp:Label>
                    <tr>
                        <td>
                            <asp:GridView ID="gvzeroPayabledaysemp" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%"
                                EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:BoundField DataField="Empid" HeaderText="EmpID" Visible="true" />
                                    <asp:BoundField DataField="empname" HeaderText="Employee Name" Visible="true" ItemStyle-Width="50%"/>
                                    <asp:BoundField DataField="site_name" HeaderText="Worksite" Visible="true" ItemStyle-Width="20%"/>
                                    <asp:BoundField DataField="Description" HeaderText="Description" Visible="true" ItemStyle-Width="30%"/>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:Paging ID="PagingzeroPayabledaysemp" runat="server" Visible="False" />
                        </td>
                    </tr>
                </table>
            </div>
         <div id="Div1" runat="server" visible="true">
                <table>
                    <asp:Label ID="lblBasicInactive" runat="server" Text="Basic InActive Employee list" Font-Size="16px" ForeColor="red" Font-Bold="true" Visible="false"></asp:Label>
                    <tr>
                        <td>
                            <asp:GridView ID="gvBasicInactive" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%" Visible="false"
                                EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:BoundField DataField="Empid" HeaderText="EmpID" Visible="true" />
                                    <asp:BoundField DataField="empname" HeaderText="Employee Name" Visible="true" ItemStyle-Width="50%"/>
                                    <asp:BoundField DataField="site_name" HeaderText="Worksite" Visible="true" ItemStyle-Width="20%"/>
                                    <asp:BoundField DataField="Description" HeaderText="Description" Visible="true" ItemStyle-Width="30%"/>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:Paging ID="pngBasicInActive" runat="server" Visible="False" />
                        </td>
                    </tr>
                </table>
            </div>
       

       <div id="dvview" runat="server" visible="false">
                            <table style="width:800px;">

                        <tr>
                            <td>
                                <cc1:Accordion ID="Accordion2" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                    ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                    <Panes>
                                        <cc1:AccordionPane ID="AccordionPane2" runat="server" HeaderCssClass="accordionHeader"
                                            ContentCssClass="accordionContent">
                                            <Header>
                                            Search Criteria</Header>
                                            <Content>
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td>EmpName:
                                                                       <asp:HiddenField ID="txtEmpNameHidden" runat="server" />
                                                                       <asp:TextBox ID="txtEmpName" Height="20px" runat="server" TabIndex="6" AccessKey="5"  Width="150Px"
                                                                    ToolTip="[Alt+5]">                                                              
                                                                    </asp:TextBox>
                                                                       <cc1:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" DelimiterCharacters="" Enabled="True"
							                                        MinimumPrefixLength="1" ServiceMethod="GetCompletionList_EmpName" ServicePath="" TargetControlID="txtEmpName"
							                                        UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
							                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetEmpID" >
						                                        </cc1:AutoCompleteExtender>
                                                                <cc1:TextBoxWatermarkExtender ID="txtwmeEmpName" runat="server" WatermarkText="[Filter Name]"
                                                                    TargetControlID="txtEmpName">
                                                                </cc1:TextBoxWatermarkExtender>

                                                            <asp:Button ID="btnempsearch" Text="Search" runat="server" OnClick="btnempsearch_Click" CssClass="btn btn-primary" />

                                                             <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" Visible="false"
                                                                    TabIndex="7" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" CssClass="btn btn-success"
                                                                    Width="80px" />
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
                                <td>
                                    <div style="overflow-x: scroll;width:75%;">
                                    <asp:GridView ID="gvPaySlip" runat="server" AutoGenerateColumns="true" CssClass="gridview"
                                        Width="80%" CellPadding="4" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        HeaderStyle-CssClass="tableHead"
                                         AllowSorting="True" AlternatingRowStyle-BackColor="GhostWhite">
                                        <EmptyDataRowStyle CssClass="EmptyRowData" />
                                        <Columns>
                                           
                                           
                                        </Columns>
                                        <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle CssClass="tableHead" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />
                                    </asp:GridView>
                                        </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 17px">
                                    <uc1:Paging ID="EmpListPaging" runat="server" />
                                </td>
                            </tr>
    </table>
           </div>
     
         <table id="tblsync" runat="server" visible="false">
        <tr>
            <td>
                <asp:GridView ID="gvsync" runat="server" CssClass="gridview" AutoGenerateColumns="false" Width="100%" >
                       <Columns>
                           <asp:TemplateField  HeaderText="Synced Worksites">
                               <ItemTemplate>
                                   <asp:Label ID="lblws" runat="server" Text='<%# Eval("Site_Name") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>
                            </Columns>                      
                   </asp:GridView>
            </td>
        </tr>
    </table>
         <table id="tbAbsentPenalities" runat="server" visible="false">
        <tr>
            <td>
               <asp:GridView ID="gvViewApproved" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                    EmptyDataText="No Records Found" Width="100%" CssClass="gridview">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAll" Text="All" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkToTransfer" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EMP Id" ControlStyle-Width="15px">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpid" Text='<%#Eval("Empid") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="250px">
                            <ItemTemplate>
                                <asp:Label ID="lblempname" Text='<%#Eval("empname") %>' runat="server" Style="width: 250px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Previous Occurances">
                            <ItemTemplate>
                                <asp:Label ID="lblpreviousoccurances" runat="server" Text='<%#Eval("previousoccurances") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="This Occurance">
                            <ItemTemplate>
                                <asp:Label ID="lblOccurance" runat="server" Text='<%#Eval("Occurance") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Occurances">
                            <ItemTemplate>
                                <asp:Label ID="lbltotaloccurance" runat="server" Text='<%#Eval("totaloccurance") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Absents">
                            <ItemTemplate>
                                <asp:Label ID="lblAbsents" runat="server" Text='<%#Eval("Absents") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Penalities">
                            <ItemTemplate>
                                <asp:Label ID="lblpenalities" runat="server" Text='<%#Eval("Penalities") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="right">
                            <ItemTemplate>
                               <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                
                            </ItemTemplate>
                            
                            
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Month">
                            <ItemTemplate>
                                <asp:Label ID="lblMonth" runat="server" Text='<%#Eval("Month") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Year">
                            <ItemTemplate>
                                <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="UpdateProgressCSS">
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="1">
            <ProgressTemplate>
                <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                    ID="imgs" />
                please wait...</ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>

