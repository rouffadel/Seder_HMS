<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VacationConfigDetails.aspx.cs" Inherits="AECLOGIC.ERP.HMS.VacationConfigDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/Paging.ascx" TagName="Paging" TagPrefix="uc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel ID="up11" runat="server">
            <ContentTemplate>
               
                <div id="dvsearch" runat="server">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <asp:RadioButtonList RepeatDirection="Horizontal" AutoPostBack="true" ID="rdAc" runat="server"
                                    OnSelectedIndexChanged="rdAc_SelectedIndexChanged">
                                    <asp:ListItem Text="Active" Selected="True" Value="1">
                                    </asp:ListItem>
                                    <asp:ListItem Text="In Active" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" id="TblSearch" runat="server">
                                <Ajax:Accordion runat="server" ID="ACC" SelectedIndex="0" AutoSize="None" FadeTransitions="false"
                                    TransitionDuration="50" FramesPerSecond="40" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                    RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                    <Panes>
                                        <Ajax:AccordionPane runat="server" ID="AP1">
                                            <Header>
                                                Search
                                            </Header>
                                            <Content>
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td style="width: 20%">Country:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlsCountry" CssClass="droplist" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlsCountry_SelectedIndexChanged" AccessKey="c" ToolTip="[Alt+c or Alt+c+Enter]" />
                                                            <Ajax:ListSearchExtender QueryPattern="Contains" ID="leddlsCountry" runat="server"
                                                                TargetControlID="ddlsCountry" PromptText="Type to search" PromptCssClass="PromptText"
                                                                PromptPosition="Top" IsSorted="true" />
                                                        </td>
                                                        <td style="width: 20%">State:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlSState" CssClass="droplist" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlSState_SelectedIndexChanged" AccessKey="s" ToolTip="[Alt+s or Alt+s+Enter]" />
                                                            <Ajax:ListSearchExtender QueryPattern="Contains" ID="ListSearchExtender1" runat="server"
                                                                TargetControlID="ddlSState" PromptText="Type to search" PromptCssClass="PromptText"
                                                                PromptPosition="Top" IsSorted="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" AccessKey="i" ToolTip="[Alt+i or Alt+i+Enter]" />
                                                            <asp:Button runat="server" ID="btnReset" Text="Reset" CssClass="btn btn-danger" OnClick="btnReset_Click" AccessKey="b" ToolTip="[Alt+b or Alt+b+Enter]" />
                                                        </td>
                                                        <td style="width: 20%">City:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlSCity" CssClass="droplist" AccessKey="c" ToolTip="[Alt+c or Alt+c+Enter]" />
                                                            <Ajax:ListSearchExtender QueryPattern="Contains" ID="ListSearch1" runat="server"
                                                                TargetControlID="ddlSCity" PromptText="Type to search" PromptCssClass="PromptText"
                                                                PromptPosition="Top" IsSorted="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </Content>
                                        </Ajax:AccordionPane>
                                    </Panes>
                                </Ajax:Accordion>
                            </td>
                        </tr>
                    </table>
                </div>
                <table id="T1" cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td width="50%" colspan="4" align="left">
                            <table id="Table1" cellpadding="2" cellspacing="2" width="40%" align="left" visible="false"
                                runat="server">
                                <tr>
                                    <td width="25%" colspan="4" height="5"></td>
                                </tr>
                                <tr>
                                    <td width="25%" align="left">Country:<sup class="Must" style="color: #FF0000">*</sup>
                                    </td>
                                    <td width="25%" align="left">
                                        <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                            CssClass="droplist">
                                        </asp:DropDownList>
                                        
                                    </td>
                                    <td><asp:LinkButton runat="server" ID="lnkBnkMaster"  OnClientClick="javascript:return AddNewCountry();" 
                                        Text="Add New Country" /></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td width="25%" align="left">State:<sup class="Must" style="color: #FF0000">*</sup>
                                    </td>
                                    <td width="25%" align="left">
                                        <asp:DropDownList ID="DdlState" runat="server" AutoPostBack="false" CssClass="droplist">
                                        </asp:DropDownList>
                                        
                                    </td>
                                    <td><asp:LinkButton runat="server" ID="LinkButton2"  OnClientClick="javascript:return AddNewState();" 
                                        Text="Add New State" /></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td width="25%" align="left">City Name:<sup class="Must" style="color: #FF0000">*</sup>
                                    </td>
                                    <td width="25%" align="left">
                                        <asp:TextBox ID="txtName" runat="server" MaxLength="50" CssClass="droplist"></asp:TextBox>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td width="25%" colspan="4" height="5"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="left" width="25%">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" AccessKey="s" ToolTip="[Alt+s or Alt+s+Enter]" />
                                        <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-danger" OnClick="BtnClear_Click" AccessKey="c" ToolTip="[Alt+c or Alt+c+Enter]" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" colspan="4" align="left">
                            <asp:GridView ID="GVDetails" runat="server" AutoGenerateColumns="False" CssClass="gridview" HeaderStyle-CssClass="tableHead"
                                HeaderStyle-Font-Size="XX-Small" OnRowDataBound="GVDetails_RowDataBound" OnRowCommand="GVDetails_RowCommand"
                                OnRowDeleting="GVDetails_RowDeleting" Width="100%">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="20" HeaderText="#" HeaderStyle-Width="20">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CItyName" HeaderText="City Name" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="StateName" HeaderText="State Name" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="CountryName" HeaderText="Country Name" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-HorizontalAlign="Left" />
                                    <asp:TemplateField ItemStyle-Width="40" HeaderText="Edit" HeaderStyle-Width="40">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk" runat="server" CssClass="anchor__grd edit_grd" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"CityID")%>'
                                                CommandName='editing' Text="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                               
                                      <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" CssClass="anchor__grd dlt"  runat="server" CommandArgument='<%#Eval("CityID") %>'
                                                    Text='<%#Eval("IsActive") %>' CommandName="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCountryID" runat="server"  CommandArgument='<%# DataBinder.Eval(Container.DataItem,"stateid")%>'
                                                CommandName="Delete" Text="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCityName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CItyName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <AEC:UcPaging ID="taskPaging" runat="server" Visible="false" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Content>