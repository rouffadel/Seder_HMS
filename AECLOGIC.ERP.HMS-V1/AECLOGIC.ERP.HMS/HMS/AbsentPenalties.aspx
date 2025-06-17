<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="AbsentPenalties.aspx.cs" Inherits="AECLOGIC.ERP.HMS.AbsentPenalties" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

        <style type="text/css">
    .rounded_corners
    {
       
        -webkit-border-radius: 8px;
        -moz-border-radius: 8px;
        border-radius: 8px;
        overflow: hidden;
    }
    .rounded_corners td
    {
       border: 0.5px solid #A1DCF2;
        font-family: Arial;
        font-size: 10pt;
        text-align:left;
         
    }
     .rounded_corners th
    {
        border: 0.5px solid #A1DCF2;
        font-family: Arial;
        font-size: 10pt;
        text-align:left;
        font-weight:bold ;
    }
     
    .rounded_corners table table td 
    {
        border-style:dotted ;
    }
    a {
    color:black ;
    font-family:Verdana ;color:black
}
            </style>
    <script language="javascript" type="text/javascript">
        function OnlyNumeric(evt) {
           
         var chCode = evt.keyCode ? evt.keyCode : evt.charCode ? evt.charCode : evt.which;

            if (chCode >= 48 && chCode <= 57 ||
             chCode == 46) {
                return true;
            }
            else
                return false;
        }

        function validatesave() {
            if (document.getElementById('<%= txtnoOfday.ClientID %>').value == 0) {
                alert("Enter the no. Of Days!");
                document.getElementById('<%= txtnoOfday.ClientID%>').focus();
                return false;
            }
           <%-- if (document.getElementById('<%= ddlToCity.ClientID %>').selectedIndex == 0) {
                alert("Select To City!");
                document.getElementById('<%=ddlToCity.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%= ddlToCity.ClientID %>').selectedIndex != 0 && document.getElementById('<%= ddlfromCity.ClientID %>').selectedIndex != 0)
            {
                if (document.getElementById('<%= ddlfromCity.ClientID %>').selectedIndex ==document.getElementById('<%= ddlToCity.ClientID %>').selectedIndex)
                {
                    alert("From City & To City can not be same! Please make genuine selection.");
                    document.getElementById('<%=ddlToCity.ClientID%>').focus();
                    return false;
                }
            }--%>

         
        }
     

    </script>
    <asp:UpdatePanel ID="uppnlk" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <asp:Panel ID="tblNewk"  runat="server" Visible="False"   CssClass="box box-primary" Width="80%">
                        <table  >
                          
                            <tr>
                                <td  >
                                    <b>No. Of Days:</b><span style="color: #ff0000">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtnoOfday" runat="server" width="100px" tooltip="No. Of Days!"  ></asp:TextBox>  
                                        
                                                                      
                               </td>
                            </tr>

                          
                              <tr><td></td>
                               <td   class="rounded_corners"  >
                                 
                                 <asp:gridview ID="grdaddDays" runat="server" CssClass="gridview" AutoGenerateColumns="false">
        <Columns>
       <asp:TemplateField HeaderText="S.No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
               <ItemStyle Width="5%" />
            </asp:TemplateField>
        <asp:TemplateField HeaderText="Occurance">
            <ItemTemplate>
                <asp:TextBox ID="txtoccurance" runat="server" Text=<%# Eval("occurance") %> width="50px" tooltip="occurance!" AutoPostBack="true" OnTextChanged="addrows"   ></asp:TextBox>
            </ItemTemplate>
              <ItemStyle Width="100px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Penality ++">
            <ItemTemplate>
                <asp:TextBox ID="txtpenality" runat="server"  width="50px" Text=<%# Eval("penality") %>  tooltip="Penality ++!" AutoPostBack="true" OnTextChanged="addrows"  ></asp:TextBox>
            </ItemTemplate>
            <ItemStyle Width="100px" />
        </asp:TemplateField>
             <asp:TemplateField >
            <ItemTemplate>
              <asp:ImageButton runat="server" ID="imgDel" src="../images/Delete.ico" width="16px" hieght="16px" tooltip="Remove this!" OnClick="DelRows" OnClientClick="javascript:return confirm('Are you sure want to remove..?')" ></asp:ImageButton>
            </ItemTemplate>
            <ItemStyle Width="16px" />
        </asp:TemplateField>
            </Columns> 
          </asp:gridview>  
                               </td>
                            </tr>
                              
                            <tr>
                                <td colspan="2"  align="right"  style="border-top-width :1px;border-top-color:#0094ff;border-top-style:dotted" >
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" Width="100px" OnClientClick="javascript:return validatesave();"
                                        OnClick="btnSubmit_Click"  ToolTip="[Alt+s OR Alt+s+Enter]" />
                                         
                                        
                                       <asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="btn btn-danger" Width="70px"
                                        OnClick="btn_reste_Click"   AccessKey="r" TabIndex="3"  ToolTip="[Alt+r OR Alt+r+Enter]" />
                                        
                                        
                                </td>
                            </tr>
                        </table>
                            </asp:Panel> 
                      
                        <table id="tblEdit" runat="server" visible="false">
                            <tr>
                                <td>
                                    <cc1:Accordion ID="DesigAccordion" runat="server" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                        AutoSize="None" FadeTransitions="false" TransitionDuration="50" FramesPerSecond="40"
                                        RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                        <Panes>
                                            <cc1:AccordionPane ID="DesigAccordionPane" runat="server" HeaderCssClass="accordionHeader"
                                                ContentCssClass="accordionContent">
                                                <Header>
                                                    Search Criteria</Header>
                                                <Content>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList ID="rblDesg" AutoPostBack="true" runat="server" Font-Bold="True" TabIndex="1"
                                                                    OnSelectedIndexChanged="rblDesg_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Active" Selected="True" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
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
                                <td style="width: 100%">
                                    <asp:GridView ID="gvRMItem" runat="server" AutoGenerateColumns="False"    
                                        ForeColor="#333333" GridLines="Both" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData"
                                        OnRowCommand="gvWages_RowCommand" HeaderStyle-CssClass="tableHead" Width="100%"
                                        CssClass="gridview" AlternatingRowStyle-BackColor="GhostWhite" >
                                      <Columns>
                                             <asp:TemplateField  HeaderText="No. Of Days"  >
                                                 <HeaderStyle width="100px" />
                                                 <ItemTemplate>
                                                     <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("noOfDays")%>' Text='<%#Eval("noOfDays")%>'  CommandName="Edt"  style="cursor:pointer"   ></asp:LinkButton>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Occurance">
                                                <HeaderStyle Width="150" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblkk" runat="server" Text='<%#Eval("occurance")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Penality++">
                                                <HeaderStyle Width="150" />
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("penality")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                     </Columns>
                                  </asp:GridView>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 17px">
                                    <uc1:Paging ID="EmpListPaging" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProg" runat="server" AssociatedUpdatePanelID="uppnlk">
        <ProgressTemplate>
            <asp:Image runat="server" ImageUrl="~/Images/updateProgress.gif" ImageAlign="AbsMiddle"
                ID="imgs" />
            please wait...
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
