<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="EmptotDetails.aspx.cs"  Inherits="AECLOGIC.ERP.HMS.EmptotDetails" %>


<%@ Register Namespace="AjaxControlToolkit" TagPrefix="cc1" Assembly="AjaxControlToolkit" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">

<script language="javascript" type="text/javascript">
   
    function GetID(source, eventArgs) {
        var HdnKey = eventArgs.get_value();
        //  alert(HdnKey);
        document.getElementById('<%=ddlWorksite_hid.ClientID %>').value = HdnKey;
    }
    function GETDEPT_ID(source, eventArgs) {
        var HdnKey = eventArgs.get_value();
        //  alert(HdnKey);
        document.getElementById('<%=ddlDepartment_hid.ClientID %>').value = HdnKey;
    }
    </script>
    <AEC:Topmenu ID="topmenu" runat="server" cssclass="topmenu" />
    <table border="0" cellpadding="0" cellspacing="0" dir="ltr" width="100%">
        <tr>
            <td>
                <cc1:Accordion ID="SeMyAccordion" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true" Style="margin-bottom: 0px"
                    Width="100%">
                    <Panes>
                        <cc1:AccordionPane ID="SeAccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    
                                     <tr>
                                                                   <td>
                                                                   Worksite&nbsp;
                                                                   
                                                         <asp:HiddenField ID="ddlWorksite_hid" runat="server" />
                                             <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                           </cc1:TextBoxWatermarkExtender>
                                                                  
                                                                   Department&nbsp;
                                                                   
                                                                  <asp:HiddenField ID="ddlDepartment_hid" runat="server" />
                                                 <asp:TextBox ID="txtdepartment" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList_Department" ServicePath="" TargetControlID="txtdepartment"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GETDEPT_ID"  >
                                            </cc1:AutoCompleteExtender>        
                                                 <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtdepartment"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]">
                                           </cc1:TextBoxWatermarkExtender>
                                                                  
                                                                     EmpID &nbsp;<asp:DropDownList ID="ddlEmp" runat="server" CssClass="droplist">
                                                                            </asp:DropDownList>

                                                                             <cc1:ListSearchExtender ID="lseEmploees" runat="server" QueryPattern="Contains"
                                                PromptPosition="Top" PromptCssClass="PromptText" PromptText="Type to serch" TargetControlID="ddlEmp">
                                            </cc1:ListSearchExtender>

                                        <asp:TextBox ID="txtemp" runat="server"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtemp"
                                            WatermarkCssClass="watermarked" WatermarkText="[Type to filter]">
                                        </cc1:TextBoxWatermarkExtender>
                                        <asp:Button ID="btnfilter" Width="70" runat="server" CssClass="btn btn-primary" Text="Filter" OnClick="btnfilter_Click" />

                                                                 
                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button Width="70" ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" 
                                            TabIndex="2" AccessKey="i" ToolTip="[Alt+i OR Alt+i+Enter]" CssClass="btn btn-primary" />
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
            <td >
                <cc1:Accordion ID="MyAccordion" runat="Server"  BackColor="#DCDCDC " HeaderCssClass="accordionHeader" 
                    ContentCssClass="accordionContent" FadeTransitions="true" SelectedIndex="0" FramesPerSecond="40"
                    TransitionDuration="250">
                  
                       <Panes>
                                    <cc1:AccordionPane ID="AccordionPane9" runat="server" HeaderCssClass="accordionHeader">
                               
                                    <header>
                                        Personal Details
                                    </header>
                                    
                                    <Content >
                                            <table>
                                                 <tr>
                                                     <td>
                                        <asp:GridView ID="gvPersonaldets" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="GhostWhite"
                                        CssClass="gridview" EmptyDataText="No Records Found" HeaderStyle-CssClass="tableHead" EmptyDataRowStyle-CssClass="EmptyRowData">
                                        <Columns>
                                            <asp:BoundField DataField="EmpName" HeaderText="EmpName" />
                                            <asp:BoundField DataField="Gender" HeaderText="Gender" />
                                            <asp:BoundField DataField="DOB" HeaderText="DOB" />
                                            <asp:BoundField DataField="PlaceOfBirth" HeaderText="PlaceOfBirth" />
                                            <asp:BoundField DataField="ContactNo" HeaderText="ContactNo" />
                                            <asp:BoundField DataField="Mailid" HeaderText="Mailid" />
                                            <asp:BoundField DataField="Qualification" HeaderText="Qualification" />
                                            <asp:BoundField DataField="PerAddress" HeaderText="Permanent Address" />
                                            <asp:BoundField DataField="ResAddress" HeaderText="ResAddress" />
                                            <asp:BoundField DataField="Bloodgroup" HeaderText="B-Group" />

                                            <asp:BoundField DataField="EmergContactName" HeaderText="Emerg Res ContactName" />
                                            <asp:BoundField DataField="EmergRelation" HeaderText="Emerg Res Relation" />
                                            <asp:BoundField DataField="EmergContact" HeaderText="Emerg Res Contact" />

                                            <asp:BoundField DataField="OREmergContactName" HeaderText="Emerg Org ContactName" />
                                            <asp:BoundField DataField="OREmergRelation" HeaderText="Emerg Org Relation" />
                                            <asp:BoundField DataField="OREmergContact" HeaderText="Emerg Org Contact" />


                                            <asp:BoundField DataField="MaritalStat" HeaderText="MaritalStatus" />
                                           
                                           
                                            
                                          
                                           
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="EmptyRowData" />
                                        <HeaderStyle CssClass="tableHead" />
                                    </asp:GridView>
                                                     
                                                     </td>
                                                 
                                                 </tr>
                                            
                                            </table>
                                    
                                    
                                    </Content>
                                     </cc1:AccordionPane>
                        </Panes>
                       
                       <Panes>
                                    <cc1:AccordionPane ID="AccordionPane11" runat="server" HeaderCssClass="accordionHeader">
                               
                                    <header>
                                    Job Details
                                    </header>
                                    
                                    <Content>
                                    <table>
                                                 <tr>
                                                     <td>
                                        <asp:GridView ID="gvJobDetails" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="GhostWhite"
                                        CssClass="gridview" EmptyDataText="No Records Found" HeaderStyle-CssClass="tableHead" EmptyDataRowStyle-CssClass="EmptyRowData">
                                        <Columns>
                                            <asp:BoundField DataField="ReportingTo" HeaderText="ReportingTo" />
                                            <asp:BoundField DataField="DOJ" HeaderText="Date of Join" />
                                            <asp:BoundField DataField="Site_Name" HeaderText="Worksite" />
                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                            <asp:BoundField DataField="Category" HeaderText="Category" />
                                            <asp:BoundField DataField="Nature" HeaderText="Nature" />
                                            <asp:BoundField DataField="EmpType" HeaderText="EmpType" />
                                           
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="EmptyRowData" />
                                        <HeaderStyle CssClass="tableHead" />
                                    </asp:GridView>
                                                     
                                                     </td>
                                                 
                                                 </tr>
                                            
                                            </table>
                                    
                                    </Content>
                                     </cc1:AccordionPane>
                        </Panes>
                        <Panes>
                                    <cc1:AccordionPane ID="AccordionPane12" runat="server" HeaderCssClass="accordionHeader">
                               
                                    <header>
                                    Working Details
                                    </header>
                                    
                                    <Content>
                                    <table>
                                                 <tr>
                                                     <td>
                                        <asp:GridView ID="gvWorkingDetails" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="GhostWhite"
                                        CssClass="gridview" EmptyDataText="No Records Found" HeaderStyle-CssClass="tableHead" EmptyDataRowStyle-CssClass="EmptyRowData">
                                        <Columns>

                                             <asp:BoundField DataField="Site_Name" HeaderText="Worksite" />
                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                            <asp:BoundField DataField="Category" HeaderText="Category" />
                                            <asp:BoundField DataField="Nature" HeaderText="Nature" />
                                            <asp:BoundField DataField="EmpType" HeaderText="EmpType" />
                                            <asp:BoundField DataField="FromDate" HeaderText="From Date" />
                                            <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                                           
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="EmptyRowData" />
                                        <HeaderStyle CssClass="tableHead" />
                                    </asp:GridView>
                                                     
                                                     </td>
                                                 
                                                 </tr>
                                            
                                            </table>
                                    
                                    </Content>
                                     </cc1:AccordionPane>
                        </Panes>

                         <Panes>
                                    <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader">
                               
                                    <header>
                                    Leave History
                                    </header>
                                    
                                    <Content>
                                    <table>
                                                 <tr>
                                                     <td>
                                        <asp:GridView ID="gvLeavedetails" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="GhostWhite"
                                        CssClass="gridview" EmptyDataText="No Records Found" HeaderStyle-CssClass="tableHead" EmptyDataRowStyle-CssClass="EmptyRowData">
                                        <Columns>
                                            <asp:BoundField DataField="AppliedFrom" HeaderText="AppliedFrom" />
                                            <asp:BoundField DataField="AppliedTo" HeaderText="AppliedTo" />
                                            <asp:BoundField DataField="GrantedFrom" HeaderText="GrantedFrom" />
                                            <asp:BoundField DataField="Grantedto" HeaderText="Granted To" />
                                            <asp:BoundField DataField="LeaveStatus" HeaderText="Leave Status" />
                                            <asp:BoundField DataField="Name" HeaderText="Leave Type" />
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="EmptyRowData" />
                                        <HeaderStyle CssClass="tableHead" />
                                    </asp:GridView>
                                                     
                                                     </td>
                                                 
                                                 </tr>
                                            
                                            </table>
                                    
                                    </Content>
                                     </cc1:AccordionPane>
                        </Panes>

                         <Panes>
                                    <cc1:AccordionPane ID="AccordionPane2" runat="server" HeaderCssClass="accordionHeader">
                               
                                    <header>
                                    Salary Hike Details
                                    </header>
                                    
                                    <Content>
                                    <table>
                                                 <tr>
                                                     <td>
                                        <asp:GridView ID="gvSalHikes" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="GhostWhite"
                                        CssClass="gridview" EmptyDataText="No Records Found" HeaderStyle-CssClass="tableHead" EmptyDataRowStyle-CssClass="EmptyRowData">
                                        <Columns>
                                            <asp:BoundField DataField="FromDate" HeaderText="From Date" />
                                            <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                                            <asp:BoundField DataField="Salary" HeaderText="Salary" />
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="EmptyRowData" />
                                        <HeaderStyle CssClass="tableHead" />
                                    </asp:GridView>
                                                     
                                                     </td>
                                                 
                                                 </tr>
                                            
                                            </table>
                                    
                                    </Content>
                                     </cc1:AccordionPane>
                        </Panes>

                         <Panes>
                                    <cc1:AccordionPane ID="AccordionPane3" HeaderCssClass="accordionHeader" runat="server">
                               
                                    <header>
                                    Document Details
                                    </header>
                                    
                                    <Content>
                                    
                                    <table>
                                                 <tr>
                                                     <td>
                                        <asp:GridView ID="gvDocs" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="GhostWhite"
                                        CssClass="gridview" EmptyDataText="No Records Found" HeaderStyle-CssClass="tableHead" EmptyDataRowStyle-CssClass="EmptyRowData">
                                        <Columns>

                                            <asp:BoundField DataField="ResourceName" HeaderText="Doc Name" />
                                            <asp:BoundField DataField="Numeber" HeaderText="Numeber" />
                                            <asp:BoundField DataField="AltNumber" HeaderText="Alt Number" />
                                            <asp:BoundField DataField="IssuePlace" HeaderText="Issue Place" />
                                            <asp:BoundField DataField="Issuer" HeaderText="Issuer" />
                                            <asp:BoundField DataField="FromDate" HeaderText="Valid From" />
                                            <asp:BoundField DataField="ValidTo" HeaderText="Valid To" />
                                           
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="EmptyRowData" />
                                        <HeaderStyle CssClass="tableHead" />
                                    </asp:GridView>
                                                     
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
</asp:Content>

