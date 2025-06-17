<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="EmpPayRoleConfig.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.EmpPayRoleConfig" Title="EmpPayRoleConfig.aspx" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>  
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function UpdateWages(ctrl, wageid, empid, Status, CentageID, UserID, Centage, EmpSalID) {
            var Cent = $get(Centage).value;
            if (Cent == "" || Cent == null) {
                Cent = "0";
            }

            var chkStatus = $get(Status).checked;
            var Result = AjaxDAL.UpdateWagesPercent(empid, wageid, chkStatus, CentageID, UserID, Cent, EmpSalID);
            alert(Result.value);
        }

        function UpdateAllowances(ctrl, allowid, empid, Status, CentageID, UserID, Centage, EmpSalID, ITStatus) {
            var Cent = $get(Centage).value;
            var chkStatus = $get(Status).checked;
            var chkITStatus = $get(ITStatus).checked;

            if (Cent == "" || Cent == null) {
                var Result = AjaxDAL.UpdateAllowancesPercent(empid, allowid, chkStatus, CentageID, UserID, Cent, EmpSalID, chkITStatus);
                alert(Result.value);
            } else {
                if (Cent == 0) {
                    alert('Limit Exceed');
                }
                else {
                    var Result = AjaxDAL.UpdateAllowancesPercent(empid, allowid, chkStatus, CentageID, UserID, Cent, EmpSalID, chkITStatus);
                    alert(Result.value);
                }
            }
        }

        function UpdateEmpContribution(ctrl, allowid, empid, Status, CentageID, UserID, Centage, EmpSalID, ITStatus) {

            var Cent = $get(Centage).value;
            var chkStatus = $get(Status).checked;
            var chkITStatus = $get(ITStatus).checked;
            if (Cent == "" || Cent == null) {

                var Result = AjaxDAL.UpdateEmpContributionPercent(empid, allowid, chkStatus, CentageID, UserID, Cent, EmpSalID, chkITStatus);
                alert(Result.value);
            }
            else {

                if (Cent == 0) {
                    alert('Limit Exceed');
                }
                else {
                    var Result = AjaxDAL.UpdateEmpContributionPercent(empid, allowid, chkStatus, CentageID, UserID, Cent, EmpSalID, chkITStatus);
                    alert(Result.value);
                }
            }
        }

        function UpdateEmpDeductions(ctrl, allowid, empid, Status, CentageID, UserID, Centage, EmpSalID, ITStatus) 
        {

            var Cent = $get(Centage).value;
            var chkStatus = $get(Status).checked;
            var chkITStatus = $get(ITStatus).checked;
            if (Cent == "" || Cent == null) {

                var Result = AjaxDAL.UpdateEmpDeductionsPercent(empid, allowid, chkStatus, CentageID, UserID, Cent, EmpSalID, chkITStatus);
                alert(Result.value);
            } else {
                if (Cent == 0) {
                    alert('Limit Exceed');
                }
                else {
                    var Result = AjaxDAL.UpdateEmpDeductionsPercent(empid, allowid, chkStatus, CentageID, UserID, Cent, EmpSalID, chkITStatus);
                    alert(Result.value);
                }
            }
        }


        function UpdateEmpNonCTCComp(ctrl, allowid, empid, Status, Centage, EmpSalID) {

            var Cent = $get(Centage).value;
            var chkStatus = $get(Status).checked;
            if (Cent != "" && Cent != null) {
                var Result = AjaxDAL.UpdateNOnCTCComponents(empid, allowid, chkStatus, EmpSalID, Cent);
                alert(Result.value);
            }
            else {
                alert('Enter Value');
            }
        }

        function WagesPercentageCal(ctrl, Centage, CentageValue, EmpSal) {
            var vrCent = $get(CentageValue).value;
            if (vrCent != "" || vrCent != null) {
                var Result = AjaxDAL.WagesPercentCalculation(vrCent, EmpSal);
                $get(Centage).value = Result.value;
            }
        }

    function AllowancesPercentageCal(ctrl, allowid, Centage, CentageValue, EmpSal, empid, EmpSalID) {
        var vrCent = $get(CentageValue).value;
        if (vrCent != "" || vrCent != null) {
            var Result = AjaxDAL.AllowancesPercentCalculation(vrCent, EmpSal, allowid, empid, EmpSalID);
            $get(Centage).value = Result.value;
        }
    }

    function ContrPercentageCal(ctrl, ItemID, Centage, CentageValue, EmpSal, empid, EmpSalID) {
        var vrCent = $get(CentageValue).value;
        if (vrCent != "" || vrCent != null) {
            var Result = AjaxDAL.ContrPercentageCal(vrCent, EmpSal, ItemID, empid, EmpSalID);
            $get(Centage).value = Result.value;
        }
    }

    function DedudPercentageCal(ctrl, ItemID, Centage, CentageValue, EmpSal, empid, EmpSalID) {
        var vrCent = $get(CentageValue).value;
        if (vrCent != "" || vrCent != null) {
            var Result = AjaxDAL.DedudPercentageCal(vrCent, EmpSal, ItemID, empid, EmpSalID);
            $get(Centage).value = Result.value;
        }
    }
    </script>
    <asp:HiddenField ID="hdnEMPId" runat="server" />
    <table style="width: 100%">
        <tr><td><asp:Button ID="Button2" runat="server" CssClass="savebutton" 
                                onclick="Button2_Click" Text="Back" />
                        </td></tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="width: 108px">
                            Emp No
                        </td>
                        <td>
                            :
                            <asp:Label ID="lblEmpNo" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="width: 108px">
                            Worksite
                        </td>
                        <td>
                            :
                            <asp:Label ID="lblWorkSite" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 108px">
                            Name
                        </td>
                        <td>
                            :
                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            Designation
                        </td>
                        <td>
                            :
                            <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 108px">
                            Trades
                        </td>
                        <td>
                            :
                            <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            Department
                        </td>
                        <td>
                            :
                            <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
                           
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 108px">
                            Salary(Gross)
                        </td>
                        <td>
                            :
                            <asp:Label ID="lblSal" runat="server" ></asp:Label>
                        </td>
                        <td style="width: 108px">
                            
                        </td>
                        <td>
                            
                            <asp:TextBox ID="txtFromDate" runat="server" Visible="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFromDate" PopupButtonID="txtFromDate" Format="dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                </table>
            </td>
      
            <td style="vertical-align: top;">
                <ajaxToolkit:Accordion ID="MyAccordion" runat="Server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    <Panes>
                        <ajaxToolkit:AccordionPane ID="paneWages" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent">
                            <Header>
                                <b>Wages</b></Header>
                            <Content>
                              <table width="100%">
                             
                                 <tr>
                                    <td>
                                       <asp:GridView ID="grdWages" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdWages_RowDataBound"  ForeColor="#333333" GridLines="Both" HeaderStyle-CssClass="tableHead">
                                        <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                     <asp:CheckBox ID="chkWages" runat="server" Text='<%#Eval("Name")%>' Checked='<%#Eval("Access")%>' >
                                            </asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="% on Gross" DataField="CentageOnCTC" DataFormatString="{0:P2}" />
                                              
                                                <asp:BoundField HeaderText="Pre Value" DataField="Value" DataFormatString="{0:N2}" />
                                             
                                                 <asp:TemplateField HeaderText="Value[Monthly]">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txtCentageValue" runat="server" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Centage">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txtwagepercent" runat="server" Enabled="false" Width="60px" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdt" runat="server" Text="Save" CommandName="edt" CommandArgument='<%#Eval("WagesId")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        </Columns>
                                       </asp:GridView>
                                    </td>
                                 </tr>
                              </table>
                            </Content>
                        </ajaxToolkit:AccordionPane>
                        <ajaxToolkit:AccordionPane ID="paneAllowences" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent">
                            <Header>
                                <b>Allowances</b></Header>
                            <Content>
                                <table width="100%">
                                 <tr>
                                    <td>
                                       <asp:GridView ID="grdAllowances" runat="server" AutoGenerateColumns="false"  ForeColor="#333333" GridLines="Both" HeaderStyle-CssClass="tableHead" OnRowDataBound="grdAllowances_RowDataBound" >
                                        <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                     <asp:CheckBox ID="chkAllowances" runat="server" Text='<%#Eval("Name")%>' Checked='<%#Eval("Access")%>' ></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="IT Exempted">
                                                    <ItemTemplate>
                                                     <asp:CheckBox ID="chkAllowancesIT" runat="server"  Checked='<%#Eval("ITExemptions")%>' ></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Calculated on">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblAllowanceBased" runat="server" Text='<%#Eval("CalculatedOn")%>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField HeaderText="Pre Centage" DataField="CentageOnCTC" DataFormatString="{0:P2}" />
                                                <asp:BoundField HeaderText="Pre Value" DataField="Value" DataFormatString="{0:N2}" />

                                               
                                                 <asp:TemplateField HeaderText="Max Value">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblMaxAllowancevalue" runat="server" Text='<%#Eval("Limit")%>'> </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Value[Monthly]">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txtCentageValue" runat="server" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Centage">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txtAllowancepercent" runat="server"  Enabled="false" Width="60px" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdt" runat="server" Text="Save" CommandName="edt" CommandArgument='<%#Eval("AllowId")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        </Columns>
                                       </asp:GridView>
                                    </td>
                                 </tr>
                              </table>
                            </Content>
                        </ajaxToolkit:AccordionPane>
                        <ajaxToolkit:AccordionPane ID="paneContribution" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                            <Header>
                                <b>Company Contribution</b></Header>
                            <Content>
                               <table width="100%">
                                 <tr>
                                    <td>
                                       <asp:GridView ID="grdCContribution" runat="server" AutoGenerateColumns="false"  ForeColor="#333333" GridLines="Both" HeaderStyle-CssClass="tableHead" OnRowDataBound="grdCContribution_RowDataBound">
                                        <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                     <asp:CheckBox ID="chkCContribution" runat="server" Text='<%#Eval("Name")%>' Checked='<%#Eval("Access")%>' >
                                            </asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="IT Exempted">
                                                    <ItemTemplate>
                                                     <asp:CheckBox ID="chkCContributionIT" runat="server"  Checked='<%#Eval("ITExemptions")%>' ></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:BoundField HeaderText="Pre Centage" DataField="CentageOnCTC" DataFormatString="{0:P2}" />
                                                <asp:BoundField HeaderText="Pre Value" DataField="Value" DataFormatString="{0:N2}" />
                                              
                                                 <asp:TemplateField HeaderText="Max Value">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblMaxCContributionvalue" runat="server" Text='<%#Eval("Limit")%>'> </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Value[Monthly]">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txtCentageValue" runat="server" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Centage">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txtCContributionpercent" runat="server"  Enabled="false" Width="60px" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdt" runat="server" Text="Save" CommandName="edt" CommandArgument='<%#Eval("Itemid")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        </Columns>
                                       </asp:GridView>
                                    </td>
                                 </tr>
                              </table>
                            </Content>
                        </ajaxToolkit:AccordionPane>
                        <ajaxToolkit:AccordionPane ID="paneDeduct" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent">
                            <Header>
                                <b>Deduct Statutory</b></Header>
                            <Content>
                                <table width="100%">
                                 <tr>
                                    <td>
                                       <asp:GridView ID="grdStatutory" runat="server" AutoGenerateColumns="false" 
                                       ForeColor="#333333" GridLines="Both" HeaderStyle-CssClass="tableHead" OnRowDataBound="grdStatutory_RowDataBound">
                                        <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDStatutory" runat="server" Text='<%#Eval("Name")%>' Checked='<%#Eval("Access")%>' >
                                                        </asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="IT Exempted">
                                                    <ItemTemplate>
                                                     <asp:CheckBox ID="chkDStatutoryIT" runat="server"  Checked='<%#Eval("ITExemptions")%>' ></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField HeaderText="Pre Centage" DataField="CentageOnCTC" DataFormatString="{0:P}" />
                                                <asp:BoundField HeaderText="Pre Value" DataField="Value" DataFormatString="{0:N2}" />
                                                
                                                 <asp:TemplateField HeaderText="Max Value">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblMaxDStatutoryvalue" runat="server" Text='<%#Eval("Limit")%>'> </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Value[Monthly]">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txtCentageValue" runat="server" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Centage">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txtDStatutorypercent" runat="server"  Enabled="false" Width="60px" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdt" runat="server" Text="Save" CommandName="edt" CommandArgument='<%#Eval("Itemid")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        </Columns>
                                       </asp:GridView>
                                    </td>
                                 </tr>
                              </table>
                            </Content>
                        </ajaxToolkit:AccordionPane>



                        <ajaxToolkit:AccordionPane ID="PaneNonctcComponents" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent">
                            <Header>
                                <b>Non CTC Components</b></Header>
                            <Content>
                                <table width="100%">
                                 <tr>
                                    <td>
                                       <asp:GridView ID="grdNonCTCComponts" runat="server" AutoGenerateColumns="false" 
                                       ForeColor="#333333" GridLines="Both" HeaderStyle-CssClass="tableHead" OnRowDataBound="grdNonCTCComponts_RowDataBound" >
                                        <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                   <asp:CheckBox ID="chkNonCTCComp" runat="server" Text='<%#Eval("LongName")%>' Checked='<%#Eval("Access")%>' > </asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                
                                                 <asp:TemplateField HeaderText="Value">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txtCentageValue" runat="server" Text='<%#Eval("Amount")%>' ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdt" runat="server" Text="Save" CommandName="edt" CommandArgument='<%#Eval("CompID")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        </Columns>
                                       </asp:GridView>
                                    </td>
                                 </tr>
                              </table>
                            </Content>
                        </ajaxToolkit:AccordionPane>

                    </Panes>
                </ajaxToolkit:Accordion>
            </td>
        </tr>
    </table>
</asp:Content>
