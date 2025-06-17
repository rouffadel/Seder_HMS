<%@ Page Title="" Language="C#"   AutoEventWireup="True" CodeBehind="OrgChart.aspx.cs" Inherits="AECLOGIC.ERP.HMS.TestOrgChart" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">


 
    
<table width="100%">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
  <tr>
                                        <td style="height: 26px; text-align: left;">
                                            <cc1:Accordion ID="MyAccordion" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                                ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                                FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                                <Panes>
                                                    <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                                        ContentCssClass="accordionContent">
                                                        <Header>
                                                            Search Criteria</Header>
                                                        <Content>
                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <strong>Worksite&nbsp;<asp:DropDownList ID="ddlWorksite" runat="server" AutoPostBack="True"
                                                                             CssClass="droplist"
                                                                            AccessKey="w" ToolTip="[Alt+w OR Alt+w+Enter]">
                                                                        </asp:DropDownList>
                                                                        </strong>&nbsp;&nbsp;<strong> Department&nbsp;<asp:DropDownList ID="ddlDepartment"
                                                                            runat="server" AutoPostBack="True" CssClass="droplist" >
                                                                        </asp:DropDownList>
                                                                        </strong>
                                                                   
                                                                        <asp:Button ID="btnSearch" CssClass="savebutton" runat="server" Text="View"
                                                                            OnClick="btnSearch_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </Content>
                                                    </cc1:AccordionPane>
                                                </Panes>
                                            </cc1:Accordion>
                                        </td>
                                    </tr>

</ContentTemplate>
    </asp:UpdatePanel>
    <tr>
       <td >
                
  <div id="myDiagram" style="background-color: white; border: solid 1px black; width: 100%; height:900px;align:Left"></div>

       </td>
    </tr>

</table>
 
<script type="text/javascript" id="code" language="javascript">
    function init() {
       // if (window.goSamples) goSamples();  // init for these samples -- you don't need to call this
        var $ = go.GraphObject.make;  // for conciseness in defining templates  go.Spot.TopCenter,

        myDiagram =
      $(go.Diagram, "myDiagram",  // the DIV HTML element
        {initialDocumentSpot: go.Spot.TopCenter,
        initialViewportSpot: go.Spot.TopCenter
    });

    // define Converters to be used for Bindings
    function theNationFlagConverter(nation) 
    {
        var img = new Image();
        //img.src =  "http://www.nwoods.com/go/Flags/" + nation.toString().toLowerCase().replace(/\s/g, "-") + "-flag.Png";

          img.src = "./EmpImages/" + nation.toString();

        return img;
    }

    function theInfoTextConverter(info) {
        var str = "";
        if (info.title) str += "Title: " + info.title;
        if (info.DeptName) str += "\n DeptName: " + info.DeptName;
        if (typeof info.boss === "number") {
            var bossinfo = myDiagram.model.findNodeDataForKey(info.boss);
            if (bossinfo !== null) {
                str += "\nReporting to: " + bossinfo.name;
            }
        }
        return str;
    }

    // define the Node template
    myDiagram.nodeTemplate =
      $(go.Node, go.Panel.Auto,
        { isShadowed: true },
    // the outer shape for the node, surrounding the Table
        $(go.Shape, "Rectangle",
          { fill: "azure" }),
    // a table to contain the different parts of the node
        $(go.Panel, go.Panel.Table,
          { margin: 4, maxSize: new go.Size(160, NaN) },
    // the two TextBlocks in column 0 both stretch in width
    // but align on the left side
          $(go.RowColumnDefinition,
            { column: 0,
                stretch: go.GraphObject.Horizontal,
                alignment: go.Spot.Left
            }),
    // the name
          $(go.TextBlock,
            { row: 0, column: 0,
                maxSize: new go.Size(110, NaN),
                font: "bold 8pt sans-serif",
                alignment: go.Spot.Top
            },
            new go.Binding("text", "name")),
    // the country flag
          $(go.Picture,
            { row: 0, column: 1, margin: 2,
                desiredSize: new go.Size(40, 60),
                imageStretch: go.GraphObject.Uniform,
                alignment: go.Spot.TopRight
            },
            new go.Binding("element", "nation", theNationFlagConverter)),
    // the additional textual information
          $(go.TextBlock,
            { row: 1, column: 0, columnSpan: 2,
                font: "8pt sans-serif"
            },
            new go.Binding("text", "", theInfoTextConverter))));

    // define the Link template, a simple orthogonal line
    myDiagram.linkTemplate =
      $(go.Link, go.Link.Orthogonal,
        { selectable: false },
        $(go.Shape));  // the default black link shape

    // set up the nodeDataArray, describing each person/position

    var WsID = document.getElementById('<%=ddlWorksite.ClientID%>').value;
    var DeptID = document.getElementById('<%=ddlDepartment.ClientID%>').value;


    var Resultset = AjaxDAL.TestEmpOrgChart(WsID, DeptID);
    var jsonObject = [];
    var nodeDataArray = jsonObject;
    for (i = 0; i < Resultset.value.Rows.length; i++) 
    {
        var jsonItem = {};
        if (i == 0)
        {
            jsonItem.key = Resultset.value.Rows[i].key;
            jsonItem.name = Resultset.value.Rows[i].name;
            jsonItem.nation = Resultset.value.Rows[i].nation;
            jsonItem.title = Resultset.value.Rows[i].title;
            jsonItem.DeptName = Resultset.value.Rows[i].DeptName;
        }
      
        else {
            jsonItem.key = Resultset.value.Rows[i].key;
            jsonItem.boss = Resultset.value.Rows[i].boss;
            jsonItem.name = Resultset.value.Rows[i].name;
            jsonItem.nation = Resultset.value.Rows[i].nation;
            jsonItem.title = Resultset.value.Rows[i].title;
            jsonItem.DeptName = Resultset.value.Rows[i].DeptName;
        }

        jsonObject.push(jsonItem);
    }

    nodeDataArray =jsonObject;

    // create the Model with data for the tree, and assign to the Diagram
    myDiagram.model =
      $(go.TreeModel,
        { nodeParentKeyProperty: "boss",  // this property refers to the parent node data
            nodeDataArray: nodeDataArray
        });

    // create a TreeLayout
    myDiagram.layout =
      $(go.TreeLayout,
        { treeStyle: go.TreeLayout.StyleLastParents,
            angle: 90,
            layerSpacing: 80,
            alternateAngle: 0,
            alternateAlignment: go.TreeLayout.AlignmentStart,
            alternateNodeIndent: 20,
            alternateNodeIndentPastParent: 1,
            alternateNodeSpacing: 20,
            alternateLayerSpacing: 40,
            alternateLayerSpacingParentOverlap: 1,
            alternatePortSpot: new go.Spot(0, 0.999, 20, 0),
            alternateChildPortSpot: go.Spot.Left
        });

    // Overview
    myOverview =
      $(go.Overview, "myOverview",  // the HTML DIV element for the Overview
        {observed: myDiagram });   // tell it which Diagram to show and pan
}
</script>
</asp:Content>

