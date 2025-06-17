<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportsBarGM.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMS.ReportsBarGM"  MasterPageFile="~/Templates/CommonMaster.master"%>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%--<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <table border="1" width="100%">
        <tr>
            <td>
                <asp:Chart ID="Chart1" runat="server" ToolTip="Transit Employees">
                    <Titles>
                        <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Title1"
                            Text="Transit Employees">
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
                  <asp:Chart ID="Chart2" runat="server" OnClick="Chart2_Click">
                    <Titles>
                        <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Title1"
                            Text="Transit Employees">
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
     
    </table>
</asp:Content>
