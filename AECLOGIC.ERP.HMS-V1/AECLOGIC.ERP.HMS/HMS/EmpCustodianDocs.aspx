<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpCustodianDocs.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EmpCustodianDocs"  %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
     <script language="javascript" type="text/javascript">
       <%--  function GetID(source, eventArgs) {
             var HdnKey = eventArgs.get_value();
             document.getElementById('<%=ddlEmpid_hid.ClientID %>').value = HdnKey;
         }
         function GetWSID(source, eventArgs) {
             var HdnKey = eventArgs.get_value();
             document.getElementById('<%=ddlWorkSite_hid.ClientID %>').value = HdnKey;
         }--%>

    </script>
   
          <table width="100%">
              <tr>
                  <td>
                      
                        <table id="tblAllView"  width="100%" runat="server">
                        
                            <tr>
                                <td>
                                                      
                                <asp:GridView ID="gvEmpList" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead" GridLines="Both"
                                EmptyDataText="No Records Found" OnRowCommand="gvEmpList_RowCommand" Width="100%" CssClass="gridview">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Emp ID" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpId" runat="server" Text='<%#Eval("EmpId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Select" CommandName="Sel" CommandArgument='<%#Eval("EmpId")%>' CssClass="anchor__grd edit_grd "></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td> <uc1:Paging ID="EmpListPaging" runat="server" Visible="false" /></td>
                            </tr>
                        </table>
                                     
                  </td>
              </tr>

              <tr>
                  <td>
                      <table id="tblEmpView" runat="server" width="100%" visible="false">
                          <tr>
                              <td>
                                   <b>Employee Name: </b>&nbsp<asp:Label ID="lblname" runat="server"></asp:Label>
                                 
                              </td>
                          </tr>
                          <tr>
                              <td>
                                   <asp:GridView ID="GvEmpDocs" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="tableHead" CssClass="gridview"
                                            EmptyDataText="No Records Found" Width="100%" OnRowCommand="GvEmpDocs_RowCommand" GridLines="Both">
                                       <Columns>
                                           <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocID" runat="server" Text='<%#Eval("DocID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Doc Name" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("DocumentName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Worksite" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="180" />
                                                    <HeaderStyle Width="180" />
                                                    <ItemTemplate>
                                                        <asp:DropDownList Width="180" ID="grdddlworksites" runat="server" DataTextField="Site_Name"
                                                            CssClass="droplist" AutoPostBack="true" DataValueField="Site_ID" DataSource='<%# BindSites()%>'
                                                            OnSelectedIndexChanged="grdddlworksites_SelectedIndexChanged" SelectedIndex='<%# GetSiteIndex(Eval("SiteID").ToString())%>'></asp:DropDownList>
                                                         </ItemTemplate>
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Custody Holder" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle />
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="grdddlHeads" runat="server" DataTextField="name" AutoPostBack="true"
                                                            CssClass="droplist" DataValueField="EmpId" DataSource='<%# BindHeads()%>'  SelectedIndex='<%# GetInvHolderIndex(Eval("InvHolderID").ToString())%>'></asp:DropDownList>
                                                         </asp:DropDownList>
                                                         <asp:DropDownList ID="grdNewddlHeads" runat="server" DataTextField="name" AutoPostBack="true"
                                                            DataValueField="EmpId" DataSource='<%# BindHeads()%>' Visible="false">
                                                        </asp:DropDownList>
                                                          </ItemTemplate>
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Upload Proof" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:FileUpload ID="fuUploadProof" runat="server"></asp:FileUpload>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkUpload" runat="server" Text="Assign" CommandName="Upld" CommandArgument='<%#Eval("DocID")%>' CssClass="btn btn-primary"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Proof" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <a id="A1" target="_blank" href='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "ProofID").ToString(),DataBinder.Eval(Container.DataItem, "Ext").ToString().ToString()) %>'
                                                            runat="server" visible='<%# Visble(DataBinder.Eval(Container.DataItem, "Ext").ToString().ToString()) %>' class="anchor__grd vw_grd">
                                                            View</a>
                                                        <asp:Label ID="lblProofID" runat="server" Text='<%#Eval("Ext")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                       </Columns>
                                   </asp:GridView>
                              </td>
                          </tr>

                      </table>
                  </td>
              </tr>
          </table>
      
 </asp:Content>
