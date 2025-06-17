<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Finalexit.aspx.cs" Inherits="AECLOGIC.ERP.HMSV1.FinalexitV1" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function GetIDEmp(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=empid_hd.ClientID %>').value = HdnKey;
        }
        function GetID(source, eventArgs) {
            var HdnKey = eventArgs.get_value();
            document.getElementById('<%=ddlWs_hid.ClientID %>').value = HdnKey;
        }
    </script>




    <table id="tblNew" runat="server" width="40%">
        <tr>
            <td>
                <asp:Label ID="lbl1" runat="server" Text="FinalExit From"></asp:Label>
            </td>
            <td>

                <asp:TextBox ID="txtfrom" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtDatePlaceIssue"
                    TargetControlID="txtfrom" Format="dd MMM yyyy"></cc1:CalendarExtender>
            </td>
        </tr>
        <tr><td>&nbsp;&nbsp;</td></tr>
        <tr>
            <td>
     <asp:Label ID="Lbl2" runat="server" Text="Exit Type"></asp:Label></td>
            <td>     <asp:DropDownList ID="ddlEmpDeAct" runat="server">
     </asp:DropDownList>
 </td>
        </tr><tr><td>&nbsp;&nbsp;</td></tr>
        <tr>
            <td>
                <asp:Label ID="lblreason" runat="server" Text="Reason"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtreason" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr><td>&nbsp;&nbsp;</td></tr>
        <tr>
            <td>
                <asp:Label ID="lblUploadProof" runat="server" Text="Upload Proof"></asp:Label>
            </td>
            <td>
                <asp:FileUpload ID="fuUploadProof" runat="server"></asp:FileUpload>

            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
                <asp:Button ID="btnclear" runat="server" Text="Clear" CssClass="btn btn-danger" OnClick="btnclear_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table id="tblview" runat="server">
        <tr>
            <td>
                <asp:GridView ID="gvVeiw" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                    CssClass="gridview" Width="100%" OnRowCommand="gvVeiw_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="EmpName">
                            <ItemTemplate>
                                <asp:Label ID="lblID" Text='<%#Eval("EmpName")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Exit From">
                            <ItemTemplate>
                                <asp:Label ID="lblExitFrom" Text='<%#Eval("FEFrom")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate>
                                <asp:Label ID="Label1" Text='<%#Eval("Reason")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkedt" CommandName="Edt" CssClass="anchor__grd edit_grd" CommandArgument='<%#Eval("FEID")%>'
                                    runat="server">Edit</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnlDelete" CommandName="Del" CssClass="anchor__grd dlt" CommandArgument='<%#Eval("FEID")%>'
                                    runat="server">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>

    <table id="tblProcess" runat="server" width="100%" visible="false">
        <tr>
            <td>
                <cc1:Accordion ID="gvViewAccordion" runat="server" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true" Height="106px" Width="90%">
                    <Panes>
                        <cc1:AccordionPane ID="gvViewAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                            ContentCssClass="accordionContent">
                            <Header>
                                Search Criteria</Header>
                            <Content>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                            <b>WorkSite:</b>
                                            <asp:HiddenField ID="ddlWs_hid" runat="server" />
                                            <asp:TextBox ID="txtSearchWorksite" OnTextChanged="GetWork" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>

                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]"></cc1:TextBoxWatermarkExtender>
                                            <b>Employee Name:</b>
                                            <asp:HiddenField ID="empid_hd" runat="server" />
                                            <asp:TextBox ID="txtempid" Width="150" runat="server"></asp:TextBox>

                                            <cc1:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="true"
                                                MinimumPrefixLength="1" ServiceMethod="GetEmpidList" ServicePath="" TargetControlID="txtEmpid" UseContextKey="true" OnClientItemSelected="GetIDEmp"
                                                CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ShowOnlyCurrentWordInCompletionListItem="true"
                                                FirstRowSelected="True">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtempid"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Employee Name]"></cc1:TextBoxWatermarkExtender>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
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
                <asp:GridView ID="gvViewApproved" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                    EmptyDataText="No Records Found" Width="90%" OnRowCommand="gvViewApproved_RowCommand" CssClass="gridview"
                    OnRowDataBound="gvViewApproved_RowDataBound">
                    <Columns>

                        <asp:BoundField DataField="FEID" HeaderText="Req.Id" HeaderStyle-Width="30px" ItemStyle-Width="30px"></asp:BoundField>
                        <asp:TemplateField HeaderText="EmpName">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpName" Text='<%#Eval("EmpName") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exit From" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpID" Text='<%#Eval("FEFrom") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Width="100%" Height="40px"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="120px" />
                        </asp:TemplateField>



                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkRecommend" CommandName="Rec" CssClass="btn btn-success" CommandArgument='<%#Eval("FEID")%>'
                                    runat="server">Recommend</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>

                        <asp:TemplateField >
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkApprove" CommandName="App" CssClass="btn btn-success" CommandArgument='<%#Eval("FEID")%>'
                                    runat="server">Approve</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="25px" />
                            <ItemStyle Width="25px" />
                        </asp:TemplateField>

                        <asp:TemplateField >
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkReject" CommandName="Rej" CssClass="anchor__grd dlt" CommandArgument='<%#Eval("FEID")%>'
                                    runat="server">Reject</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="25px" />
                            <ItemStyle Width="25px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status" >
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="25px" />
                            <ItemStyle Width="25px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Approver Details" >
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkView" CommandName="view" CssClass="btn btn-primary" CommandArgument='<%#Eval("FEID")%>'
                                    runat="server">View</asp:LinkButton>
                                 <asp:HyperLink ID="lnkInd" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FEID") %>' CssClass="btn btn-danger link__cust__style" NavigateUrl='<%# String.Format("ViewEmpExitDetails.aspx?FEID={0}", DataBinder.Eval(Container.DataItem,"FEID").ToString()) %>' Target="_blank" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="25px" />
                            <ItemStyle Width="25px" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Proof">
                                                <ItemTemplate>
                                                    <a id="A6" target="_blank" href='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "Ext").ToString(),DataBinder.Eval(Container.DataItem, "EMPID").ToString()) %>'
                                                        runat="server" class="btn btn-primary">View</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkFS" CommandName="FS" CssClass="btn btn-success" CommandArgument='<%#Eval("Empid")%>'
                                    runat="server">Final Settlement</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EMP Id" ControlStyle-Width="15px" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpid1" Text='<%#Eval("Empid") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Raised By">
                            <ItemTemplate>
                                <asp:Label ID="lblRaisedBy" Text='<%#Eval("CreatedBy") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Raised Date">
                            <ItemTemplate>
                                <asp:Label ID="lblRaisedDate" Text='<%#Eval("CreatedOn") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
 			            <asp:TemplateField HeaderText="Req Arr Date">
                            <ItemTemplate>
                                <asp:Label ID="lblReqArrDate" Text='<%#Eval("ReqArrDate") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Work Site">
                            <ItemTemplate>
                                <asp:Label ID="lblWsSite" Text='<%#Eval("WorkSite") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvExited" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                    EmptyDataText="No Records Found" Width="70%" CssClass="gridview">
                    <Columns>
                        <asp:BoundField DataField="Empid" HeaderText="Empid" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>

                        <asp:BoundField DataField="FEFrom" HeaderText="FE From" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>

                        <asp:BoundField DataField="EmpName" HeaderText="EmpName" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>

                        <asp:BoundField DataField="Transid" HeaderText="Transid" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
						<asp:BoundField DataField="CreatedOn" HeaderText="Created On" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height: 17px">
                <uc1:Paging ID="EmpReimbursementAprovedPaging" runat="server" />
            </td>
        </tr>
    </table>
    <table id="tblremarks" runat="server" visible="false">
        <tr>
            <td>
                <asp:GridView ID="gvRemarks" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead"
                    EmptyDataText="No Records Found" Width="100%" CssClass="gridview">
                    <Columns>
                        <asp:BoundField DataField="status" HeaderText="Level" />

                        <asp:BoundField DataField="Name" HeaderText="Remarked By" />
                        <asp:BoundField DataField="remarks" HeaderText="Remarks" ItemStyle-Width="500px" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
