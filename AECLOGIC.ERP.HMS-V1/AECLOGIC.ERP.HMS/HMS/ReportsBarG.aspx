<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportsBarG.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMSV1.reports_barV1" MasterPageFile="~/Templates/CommonMaster.master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%--<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <table border="1" width="100%">
        <tr>
            <td>
                <asp:Chart ID="Chart1" runat="server" ToolTip="My loans">
                    <Titles>
                        <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Title1"
                            Text="My Loans Payble">
                        </asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" Color="#8AD4EB" IsValueShownAsLabel="true"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY>
                                <MajorGrid Enabled="False" />
                            </AxisY>
                            <AxisX>
                                <MajorGrid Enabled="false" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </td>
            <td>
                <asp:Chart ID="Chart2" runat="server" ToolTip="Absents">
                    <Titles>
                        <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Title1"
                            Text="My Absents">
                        </asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" Color="#13BDB0" IsValueShownAsLabel="true"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY>
                                <MajorGrid Enabled="False" />
                            </AxisY>
                            <AxisX>
                                <MajorGrid Enabled="false" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </td>
            <td>
                <asp:Chart ID="Chart3" runat="server" ToolTip="My OTs">
                    <Titles>
                        <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Title1"
                            Text="My OTs">
                        </asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" Color="#FADC86" IsValueShownAsLabel="true" LabelToolTip="OT in Hours " ToolTip="OT in Hours "></asp:Series>
                        <asp:Series Name="Series2" Color="#FD817E" IsValueShownAsLabel="true" LabelToolTip="OT Amount" ToolTip="OT Amount"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY>
                                <MajorGrid Enabled="False" />
                            </AxisY>
                            <AxisX>
                                <MajorGrid Enabled="false" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Chart ID="Chart4" runat="server">
                    <Titles>
                        <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Title1"
                            Text="My Leaves ">
                        </asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" IsValueShownAsLabel="true" Color="#E6AB82">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY>
                                <MajorGrid Enabled="False" />
                            </AxisY>
                            <AxisX>
                                <MajorGrid Enabled="false" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
              
            </td>
            <td>
                <asp:Chart ID="Chart5" runat="server">
                    <Titles>
                        <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Title1"
                            Text="My Performance ">
                        </asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" XValueMember="Process" YValueMembers="Waiting" IsValueShownAsLabel="true" Color="#99CA9A" PostBackValue="W"
                            LabelToolTip="Waiting" ToolTip="Waiting">
                        </asp:Series>
                      
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY>
                                <MajorGrid Enabled="False" />
                            </AxisY>
                            <AxisX>
                                <MajorGrid Enabled="false" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </td>
            <td>
                <asp:Chart ID="Chart6" runat="server" OnClick="Chart2_Click">
                    <Titles>
                        <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Title1"
                            Text="My Gratuity  ">
                        </asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" XValueMember="Process" YValueMembers="Waiting" IsValueShownAsLabel="true" Color="#888888" PostBackValue="W"
                            LabelToolTip="Waiting" ToolTip="Waiting">
                        </asp:Series>
                      
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY>
                                <MajorGrid Enabled="False" />
                            </AxisY>
                            <AxisX>
                                <MajorGrid Enabled="false" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Chart ID="Chart7" runat="server" ToolTip="My loans">
                    <Titles>
                        <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Title1"
                            Text="My Attendance">
                        </asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" Color="#8AD4EB" IsValueShownAsLabel="true"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY>
                                <MajorGrid Enabled="False" />
                            </AxisY>
                            <AxisX>
                                <MajorGrid Enabled="false" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </td>
            </tr>
        <tr>
              <td>
                <asp:GridView ID="gvDocs" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                    GridLines="Both" Width="40%" CellPadding="4" HeaderStyle-CssClass="tableHead"
                    CssClass="gridview">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpDocID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "EmpDocID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDocID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DocID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="5%" HeaderText="Document Name">
                            <ItemTemplate>
                                <asp:Label ID="lblDocName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DocName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Document Type" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDocType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DocType") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="3%">
                            <ItemTemplate>
                                <a id="A2" visible='<%#IsEditable(DataBinder.Eval(Container.DataItem, "DocType").ToString()) %>'
                                    href='<%# String.Format("Appointment.aspx?id={0}&DocID={1}&DocName={2}&EmpDocID={3}",
                                    DataBinder.Eval(Container.DataItem, "EmpId"),
                                    DataBinder.Eval(Container.DataItem, "DocID"),
                                    DataBinder.Eval(Container.DataItem, "DocName"),
                                    DataBinder.Eval(Container.DataItem, "EmpDocID")) %>'
                                    runat="server" class="btn btn-success">Edit </a>
                               <a id="A1" target="_blank" href='<%# DocNavigateUrl(DataBinder.Eval(Container.DataItem, "DocType").ToString(),
                                DataBinder.Eval(Container.DataItem, "EmpId").ToString(),DataBinder.Eval(Container.DataItem, "DocID").ToString(),
                                DataBinder.Eval(Container.DataItem, "EmpDocID").ToString(),DataBinder.Eval(Container.DataItem, "value").ToString()) %>'
                                        runat="server" class="btn btn-primary">View </a>
                                <%--  <a id="A2" href ='<%# String.Format("Appointment.aspx?id=" + <%#Eval("")%> + "&DocID=" +  <%#Eval("")%>') %>  runat="server" >Edit</a>
                                <a id="A1" target="_blank"  href ='<%# String.Format("AppointmentPreview.aspx?id={0}",DataBinder.Eval(Container.DataItem, "EmpId")) %>'  runat="server" >View</a>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDel" CommandName="Del" runat="server" CssClass="btn btn-danger" CommandArgument='<%# (DataBinder.Eval(Container.DataItem,"EmpDocID")) %>'> Delete</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#F7F6F3" HorizontalAlign="Left" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
