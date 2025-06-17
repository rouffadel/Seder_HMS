<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="EmpSalHikes.aspx.cs" MasterPageFile="~/Templates/CommonMaster.master" Inherits="AECLOGIC.ERP.HMS.EmpSalHikes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            if (!chkNumber('<%=txtSalary.ClientID%>', "Salary", true, "")) {
                return false;
            }
            if (!chkDate('<%= txtDate.ClientID%>', "Date", true, "")) {
                return false;
            }
        }
        // making accessible all the  controls inside the Gridview by kaushal // 13/3/16
        function UpdateWages(ctrl, wageid, empid, Status, CentageID, UserID, Centage, EmpSalID) {
            //var Cent = $get(Centage).value;
            var obj = document.getElementById(ctrl.id.replace(ctrl.id, Centage)); // $get(Centage).value; 
            var Cent = $get(Centage).value;
            if (Cent == "" || Cent == null) {
                Cent = "0";
            }
            //to get the update checked value
            var row = ctrl.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var chkStatus = row.cells[0].getElementsByTagName("input")[0].checked;// $get(Status).checked;
            //end
            
            var Result = AjaxDAL.UpdateWagesPercent(empid, wageid, chkStatus, CentageID, UserID, Cent, EmpSalID);
            alert(Result.value);
        }

        function UpdateAllowances(ctrl, allowid, empid, Status, CentageID, UserID, Centage, EmpSalID, ITStatus) {
            debugger;
            var row = ctrl.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            //var obj = row.cells[7].getElementsByTagName("input")[0].value ;//document.getElementById(ctrl.id.replace(ctrl.id, Centage)); //document.getElementById(ctrl.id.replace('lnkEdt1', Centage)); // $get(Centage).value;
            //var cenvallfortest = $get(Centage).value;
            //alert(cenvallfortest);
            var Cent = row.cells[7].getElementsByTagName("input")[0].value; //obj.innerHTML;
            var chkStatus = row.cells[0].getElementsByTagName("input")[0].checked;//objchk.checked;//$get(Status).checked;
            var chkITStatus = row.cells[1].getElementsByTagName("input")[0].checked;// $get(ITStatus).checked;
           if (Cent == "" || Cent == null) {
                Cent = "0";
            }
           var Result = AjaxDAL.UpdateAllowancesPercent(empid, allowid, chkStatus, CentageID, UserID, Cent, EmpSalID, chkITStatus);
            alert(Result.value);
            //if (Cent == "" || Cent == null) {
              //  var Result = AjaxDAL.UpdateAllowancesPercent(empid, allowid, chkStatus, CentageID, UserID, Cent, EmpSalID, chkITStatus);
                //alert(Result.value);
            //} else {
             //   if (Cent == 0) {
               //     alert('Limit Exceed');
                //}
                //else {
                 //   var Result = AjaxDAL.UpdateAllowancesPercent(empid, allowid, chkStatus, CentageID, UserID, Cent, EmpSalID, chkITStatus);
                  //  alert(Result.value);
                //}
            //}
        }

        function UpdateEmpContribution(ctrl, allowid, empid, Status, CentageID, UserID, Centage, EmpSalID, ITStatus) {

           // var Cent = $get(Centage).value;
            var obj = document.getElementById(ctrl.id.replace(ctrl.id, Centage)); // $get(Centage).value; 
            var Cent = obj.innerHTML;
            //for checked continue value
            var row = ctrl.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            var chkStatus = row.cells[0].getElementsByTagName("input")[0].checked;//$get(Status).checked;
            var chkITStatus = row.cells[1].getElementsByTagName("input")[0].checked// $get(ITStatus).checked;


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

        function UpdateEmpDeductions(ctrl, allowid, empid, Status, CentageID, UserID, Centage, EmpSalID, ITStatus) {
            // var Cent = $get(Centage).value;
            var obj = document.getElementById(ctrl.id.replace(ctrl.id, Centage)); // $get(Centage).value; 
            var Cent = obj.innerHTML;
            //for checked continue value
            var row = ctrl.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            var chkStatus = row.cells[0].getElementsByTagName("input")[0].checked;//$get(Status).checked;
            var chkITStatus = row.cells[1].getElementsByTagName("input")[0].checked// $get(ITStatus).checked;
          
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
            // var Cent = $get(Centage).value;
            var row = ctrl.parentNode.parentNode;
           // var obj = document.getElementById(ctrl.id.replace(ctrl.id, Centage)); // $get(Centage).value; 
            var Cent = row.cells[1].getElementsByTagName("input")[0].value;//obj.innerHTML;
            //for checked continue value
          
        

            var chkStatus = row.cells[0].getElementsByTagName("input")[0].checked;//$get(Status).checked;
         
            if (Cent != "" && Cent != null) {
                var Result = AjaxDAL.UpdateNOnCTCComponents(empid, allowid, chkStatus, EmpSalID, Cent);
                alert(Result.value);
            }
            else {
                alert('Enter Value');
            }
        }

        function WagesPercentageCal(ctrl, Centage, CentageValue, EmpSal) {
           
            var vrCent = ctrl.value;// $get(txtvalid).value; //  var vrCent = $get(CentageValue).value; by kaushal:13/3/16
            if (vrCent != "" && vrCent != null) {
                var Result = AjaxDAL.WagesPercentCalculation(vrCent, EmpSal);
                var row = ctrl.parentNode.parentNode;
                row.cells[4].getElementsByTagName("input")[0].value = Result.value;// $get(Centage).value = Result.value;
            }
        }

        function AllowancesPercentageCal(ctrl, allowid, Centage, CentageValue, EmpSal, empid, EmpSalID) {
            debugger;
            var row = ctrl.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
           // var city = ;
            var vrCent = ctrl.value; //  var vrCent = $get(CentageValue).value;// by kaushal:13/3/16
             var centge = document.getElementById(ctrl.id.replace(ctrl.id, Centage))
            if (vrCent != "" && vrCent != null) {
                var Result = AjaxDAL.AllowancesPercentCalculation(vrCent, EmpSal, allowid, empid, EmpSalID);
           //$get(Centage).value = Result.value;
                row.cells[7].getElementsByTagName("input")[0].value = Result.value;// Result.value;
            }
        }

        function ContrPercentageCal(ctrl, ItemID, Centage, CentageValue, EmpSal, empid, EmpSalID) {
            var vrCent = ctrl.value; //$get(CentageValue).value;
            if (vrCent != "" || vrCent != null) {
                var Result = AjaxDAL.ContrPercentageCal(vrCent, EmpSal, ItemID, empid, EmpSalID);
                var row = ctrl.parentNode.parentNode;
                row.cells[6].getElementsByTagName("input")[0].value = Result.value;// $get(Centage).value = Result.value;
            }
        }

        function DedudPercentageCal(ctrl, ItemID, Centage, CentageValue, EmpSal, empid, EmpSalID) {
            debugger;
            var vrCent = ctrl.value;//$get(CentageValue).value;
            if (vrCent != "" || vrCent != null) {
                var Result = AjaxDAL.DedudPercentageCal(vrCent, EmpSal, ItemID, empid, EmpSalID);
                var row = ctrl.parentNode.parentNode;
                row.cells[6].getElementsByTagName("input")[0].value = Result.value;// $get(Centage).value = Result.value;
            }
        }

    </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
    
        <tr><td colspan ="2"  style="width:22%" >

            <table>
                 <tr><td> <a  href="EmployeeList.aspx"  target="_self"   > <span  class="savebutton" style="border:solid 1px Gray;"  > Back  </span> </a></td></tr>

        <tr><td colspan="2">
                <asp:LinkButton ID="lnkAdd" CssClass="selected" runat="server" Text="<b>Add</b>" OnClick="lnkAdd_Click"></asp:LinkButton>&nbsp;<asp:Label
                    ID="Label1" runat="server" Text="||"></asp:Label>&nbsp;
                <asp:LinkButton ID="lnkEdit" CssClass="selected" runat="server" Text="<b>View</b>" OnClick="lnkEdit_Click"></asp:LinkButton>
                
            </td>
        </tr>
        <tr>
            <td class="pageheader" colspan="2">
                 
               <b>CTC/Salary-Revisions</b>  
              </td></tr>
      
                 <tr>
             <td align="left"   colspan="2">
                   <strong><b>Name:</b> </strong> : <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
                 </tr>
            <tr>
                <td align="left"  colspan="2">
                    <strong><b>Department:</b> </strong>: <asp:Label ID="lblDept" runat="server"></asp:Label>
                </td>
               
                
                
            </tr>
            <tr>
            <td align="left"   colspan="2">
                    <strong><b>Designation:</b> </strong> : <asp:Label ID="lbldesig" runat="server"></asp:Label>
                </td>
               
               
            </tr>
            <tr>
                <td align="left"   colspan="2">
                    <strong><b>Date of Joining :</b></strong> <asp:Label ID="lblDOJ" runat="server"></asp:Label>
                </td>
                
               
               
            </tr>
             <td colspan="2" >

                 <table  id="tblEdit" runat ="server"  >

           
        <tr>
           
                        <td align="Left" style="vertical-align:top;width:20%;" colspan="2" >
                            <asp:GridView ID="gvAllowances" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                OnRowCommand="gvWages_RowCommand" HeaderStyle-CssClass="tableHead" CssClass="gridview">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Width="100%" />
                                <Columns>
                                    <asp:TemplateField HeaderText="FromDate" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFromDate" runat="server" Text='<%#Eval("FromDate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ToDate"  HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToDate" runat="server" Text='<%#Eval("ToDate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Salary"  HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSalary" runat="server" Text='<%#Eval("Salary")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAudited" runat="server" Text='<%#Eval("IsAudited")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="anchor__grd edit_grd" Text="Edit" CommandArgument='<%#Eval("EmpSalID")%>'
                                                CommandName="Edt"></asp:LinkButton></ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField>
                                        <ItemTemplate>
                                        
                                            <asp:LinkButton ID="lnkBreak" runat="server" CssClass="anchor__grd vw_grd" Text="Breakup" CommandArgument='<%#Eval("EmpSalID")%>'
                                                CommandName="brkup"></asp:LinkButton></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                            </asp:GridView>
                        </td>
            </tr>                       
                 </table>
             </td>
         </tr>

                <tr><td colspan ="2">            
                                 <table id="tblNew" runat="server" visible="false" style="border-top-color:#0094ff;border-top-width:1px;border-top-style :solid;border-top-left-radius:20px;border-top-color:#0094ff;border-top-width:1px;border-top-style :solid;border-top-left-radius :20px;" width="100%" >
                 
                    <tr>
                        <td>
                            Revised CTC/Salary(Per Annum)<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSalary" runat="server" Width="100px" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                          Effective Date<span style="color: #ff0000">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" Width="100px"></asp:TextBox>
                             <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                PopupButtonID="txtDOB" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"  style="border-top-color:#0094ff;border-top-width:1px;border-top-style :solid;border-top-left-radius:20px;border-top-color:#0094ff;border-top-width:1px;border-top-style :solid;border-top-left-radius :20px;" >
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="savebutton btn btn-success" Width="50px"
                                OnClick="btnSubmit_Click" OnClientClick="javascript:return Validate();"/>
                        </td>
                    </tr>
                </table >
                    </td>

                </tr>
            </table>
            </td>
        
         
<td align="Left" style="vertical-align: top;width:78%;">
    <table runat ="server"  id ="tblaccordin" width="100%" >
     
        <tr>
           
            <td>

                <ajaxToolkit:Accordion ID="MyAccordion" runat="Server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    <Panes>
                        

                        <ajaxToolkit:AccordionPane ID="paneWages" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent">
                            <Header>
                                <b>Salary</b></Header>
                            <Content>
                              <table width="100%">
                             
                                 <tr>
                                    <td>
                                       <asp:GridView ID="grdWages" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdWages_RowDataBound"  ForeColor="#333333" GridLines="Both" HeaderStyle-CssClass="tableHead">
                                        <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                     <asp:CheckBox ID="chkWages" runat="server" Text='<%#Eval("Name")%>' Checked="true" Enabled="false">
                                            </asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="% on CTC/Salary" DataField="CentageOnCTC" DataFormatString="{0:P2}" />
                                             
                                                <asp:BoundField HeaderText="Pre Value" DataField="Value" DataFormatString="{0:N2}" />
                                             
                                                 <asp:TemplateField HeaderText="Value[Monthly]">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txtCentageValue1" runat="server" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Centage">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txtwagepercent" runat="server" Enabled="false" Width="60px" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdt" runat="server" CssClass="anchor__grd vw_grd" Text="Save" CommandName="edt" CommandArgument='<%#Eval("WagesId")%>'></asp:LinkButton>
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
                                                    <asp:TextBox ID="txtCentageValue2" runat="server" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Centage">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txtAllowancepercent" runat="server"  Enabled="false" Width="60px" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdt1" runat="server" CssClass="anchor__grd vw_grd" Text="Save" CommandName="edt" CommandArgument='<%#Eval("AllowId")%>'></asp:LinkButton>
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
                                                    <asp:TextBox ID="txtCentageValue3" runat="server" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Centage">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txtCContributionpercent" runat="server"  Enabled="false" Width="60px" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdt2" runat="server" CssClass="anchor__grd vw_grd" Text="Save" CommandName="edt" CommandArgument='<%#Eval("Itemid")%>'></asp:LinkButton>
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
                                                    <asp:TextBox ID="txtCentageValue4" runat="server" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Centage">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txtDStatutorypercent" runat="server"  Enabled="false" Width="60px" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdt3" runat="server"  CssClass="anchor__grd vw_grd" Text="Save" CommandName="edt" CommandArgument='<%#Eval("Itemid")%>'></asp:LinkButton>
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
                                                   <asp:CheckBox ID="chkNonCTCComp" runat="server" Text='<%#Eval("LongName")%>'  Checked='<%#Eval("Access")%>' > </asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                
                                                 <asp:TemplateField HeaderText="Value">
                                                    <ItemTemplate>
                                                    <asp:TextBox ID="txtCentageValue" runat="server" Text='<%#Eval("Amount")%>' ></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdt4" runat="server"  CssClass="anchor__grd vw_grd" Text="Save" CommandName="edt" CommandArgument='<%#Eval("CompID")%>'></asp:LinkButton>
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
     
            </td>  
            </tr>
          
    </table>
</asp:Content>

