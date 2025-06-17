<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orgchart2.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMS.orgchart2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

    <!DOCTYPE html>
 
<html>
<head>
<title>Org Chart Static</title>
<style type="text/css">
#myOverviewDiv {
  position: absolute;
  top: 10px;
  left: 10px;
  background-color: aliceblue;
  z-index: 300; /* make sure its in front */
  border: solid 1px blue;
  width:200px;
  height:100px
}
</style>
    <script src="../JS/go.js"></script>
    <link href="../Includes/CSS/goSamples.css" rel="stylesheet" />
<%--<link href="../assets/css/goSamples.css" rel="stylesheet" type="text/css" />  <!-- you don't need to use this -->--%>
 <!-- this is only for the GoJS Samples framework --><script src="../Includes/JS/goSamples.js"></script>
<script id="code">
    function GetID(source, eventArgs) {
        var HdnKey = eventArgs.get_value();
        document.getElementById('<%=ddlWS_hid.ClientID %>').value = HdnKey;
    }
    function GetDepID(source, eventArgs) {
        var HdnKey = eventArgs.get_value();
        document.getElementById('<%=ddlDept_hid.ClientID %>').value = HdnKey;
        }
    function init() {
        //if (window.goSamples) goSamples();  // init for these samples -- you don't need to call this
        var $ = go.GraphObject.make;  // for conciseness in defining templates
        myDiagram =
          $(go.Diagram, "myDiagramDiv",  // the DIV HTML element
            {
                // Put the diagram contents at the top center of the viewport
                initialDocumentSpot: go.Spot.TopCenter,
                initialViewportSpot: go.Spot.TopCenter,
                // OR: Scroll to show a particular node, once the layout has determined where that node is
                //"InitialLayoutCompleted": function(e) {
                //  var node = e.diagram.findNodeForKey(28);
                //  if (node !== null) e.diagram.commandHandler.scrollToPart(node);
                //},
                layout:
                  $(go.TreeLayout,  // use a TreeLayout to position all of the nodes
                    {
                        treeStyle: go.TreeLayout.StyleLastParents,
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
                    })
            });
        // define Converters to be used for Bindings
        function theNationFlagConverter(nation) {
            // return "http://www.nwoods.com/go/Flags/" + nation.toLowerCase().replace(/\s/g, "-") + "-flag.Png";
            return "../EmpImages/" + nation + ".jpg";
        }
        <%--   var WsID = document.getElementById('<%=ddlWorksite.ClientID%>').value;
        var DeptID = document.getElementById('<%=ddlDepartment.ClientID%>').value;--%>
       
        //var Resultset = AjaxDAL.TestEmpOrgChart(WsID, DeptID);
      

        //for (i = 0; i < Resultset.value.Rows.length; i++) {
        //    var jsonItem = {};
        //    if (i == 0) {
        //        jsonItem.key = Resultset.value.Rows[i].key;
        //        jsonItem.name = Resultset.value.Rows[i].name;
        //        jsonItem.nation = Resultset.value.Rows[i].nation;
        //        jsonItem.title = Resultset.value.Rows[i].title;
        //        jsonItem.DeptName = Resultset.value.Rows[i].DeptName;
        //    }

        //    else {
        //        jsonItem.key = Resultset.value.Rows[i].key;
        //        jsonItem.boss = Resultset.value.Rows[i].boss;
        //        jsonItem.name = Resultset.value.Rows[i].name;
        //        jsonItem.nation = Resultset.value.Rows[i].nation;
        //        jsonItem.title = Resultset.value.Rows[i].title;
        //        jsonItem.DeptName = Resultset.value.Rows[i].DeptName;
        //    }

        //    jsonObject.push(jsonItem);
        //}
        var levelColors = ["#AC193D/#BF1E4B", "#2672EC/#2E8DEF", "#8C0095/#A700AE", "#5133AB/#643EBF",
                            "#008299/#00A0B1", "#D24726/#DC572E", "#008A00/#00A600", "#094AB2/#0A5BC4"];
        function theInfoTextConverter(info) {
            var str = "";
            if (info.title) str += "Title: " + info.title;
            if (info.headOf) str += "\n\nHead of: " + info.headOf;
            if (typeof info.boss === "number") {
                var bossinfo = myDiagram.model.findNodeDataForKey(info.boss);
                if (bossinfo !== null) {
                    str += "\n\nReporting to: " + bossinfo.name;
                }
            }
            return str;
        }
        // define the Node template
        myDiagram.nodeTemplate =
          $(go.Node, "Auto",
            { isShadowed: true },
            // the outer shape for the node, surrounding the Table
            $(go.Shape, "Rectangle",
              new go.Binding("fill", "isHighlighted", function(h) { return h ? "red" : "azure"; }).ofObject()),
            // a table to contain the different parts of the node
            $(go.Panel, "Table",
              { margin: 4, maxSize: new go.Size(150, NaN) },
              // the two TextBlocks in column 0 both stretch in width
              // but align on the left side
              $(go.RowColumnDefinition,
                {
                    column: 0,
                    stretch: go.GraphObject.Horizontal,
                    alignment: go.Spot.Left
                }),
              // the name
              $(go.TextBlock,
                {
                    row: 0, column: 0,
                    maxSize: new go.Size(120, NaN), margin: 2,
                    font: "bold 8pt sans-serif",
                    alignment: go.Spot.Top
                },
                new go.Binding("text", "name")),
              // the country flag
              $(go.Picture,
                {
                    row: 0, column: 1,
                    desiredSize: new go.Size(34, 26), margin: 2,
                    imageStretch: go.GraphObject.Uniform,
                    alignment: go.Spot.TopRight
                },
                new go.Binding("source", "nation", theNationFlagConverter)),
              // the additional textual information
              $(go.TextBlock,
                {
                    row: 1, column: 0, columnSpan: 2,
                    font: "8pt sans-serif"
                },
                new go.Binding("text", "", theInfoTextConverter))
            )  // end Table Panel
          );  // end Node
        // define the Link template, a simple orthogonal line
        myDiagram.linkTemplate =
          $(go.Link, go.Link.Orthogonal,
            { selectable: false },
            $(go.Shape, { stroke: '#222' } ));  // the default black link shape
        // set up the nodeDataArray, describing each person/position
        var WsID = document.getElementById('<%=ddlWS_hid.ClientID%>').value;
        var DeptID = document.getElementById('<%=ddlWS_hid.ClientID%>').value;
 <%--       var WsID = document.getElementById('<%=ddlWorksite.ClientID%>').value;
        var DeptID = document.getElementById('<%=ddlDepartment.ClientID%>').value;--%>
        var Resultset = AjaxDAL.TestEmpOrgChart(WsID, DeptID);
        var jsonObject = [];
        var nodeDataArray = jsonObject;
        for (i = 0; i < Resultset.value.Rows.length; i++) {
            var jsonItem = {};
            if (i == 0) {
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

        nodeDataArray = jsonObject;
     
        // create the Model with data for the tree, and assign to the Diagram
        myDiagram.model =
          $(go.TreeModel,
            { nodeParentKeyProperty: "boss",  // this property refers to the parent node data
                nodeDataArray: nodeDataArray });
        // Overview
        myOverview =
          $(go.Overview, "myOverviewDiv",  // the HTML DIV element for the Overview
            { observed: myDiagram, contentAlignment: go.Spot.Center });   // tell it which Diagram to show and pan
    }
    // the Search functionality highlights all of the nodes that have at least one data property match a RegExp
    function searchDiagram() {  // called by button
        var input = document.getElementById("mySearch");
        if (!input) return;
        input.focus();
        // create a case insensitive RegExp from what the user typed
        var regex = new RegExp(input.value, "i");
        myDiagram.startTransaction("highlight search");
        myDiagram.clearHighlighteds();
        // search four different data properties for the string, any of which may match for success
        if (input.value) {  // empty string only clears highlighteds collection
            var results = myDiagram.findNodesByExample({ name: regex },
                                                       { nation: regex },
                                                       { title: regex },
                                                       { headOf: regex });
            myDiagram.highlightCollection(results);
            // try to center the diagram at the first node that was found
            if (results.count > 0) myDiagram.centerRect(results.first().actualBounds);
        }
        myDiagram.commitTransaction("highlight search");
   
    }



</script>
</head>

   <body>
       <table>
           <tr>
              
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

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
                                                                      
                            <asp:DropDownList ID="ddlWS" Visible="false" runat="server" Width="200" CssClass="droplist" 
                                         TabIndex="1">
                                    </asp:DropDownList>
                                  <asp:HiddenField ID="ddlWS_hid" runat="server" />
                                    <asp:TextBox ID="txtSearchWorksite" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                                                        </strong>&nbsp;&nbsp;   <asp:HiddenField ID="ddlDept_hid" runat="server" />
                               <asp:DropDownList ID="ddlDept" Visible="false" runat="server" Width="200" CssClass="droplist"
                                        TabIndex="2">
                                    </asp:DropDownList>
                                    <cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search..." PromptPosition="Top" 
                                     PromptCssClass="PromptText" QueryPattern="Contains" runat="server" TargetControlID="ddlDept"></cc1:ListSearchExtender>
                                    <asp:TextBox ID="txtdept" OnTextChanged="GetDept" AutoPostBack="true" Height="22px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionListDep" ServicePath="" TargetControlID="txtdept"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetDepID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtdept"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Department Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                                                   
                                                                        <asp:Button ID="btnSearchToP" CssClass="savebutton" runat="server" Text="View"
                                                                            OnClick="btnSearchToP_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </Content>
                                                    </cc1:AccordionPane>
                                                </Panes>
                                            </cc1:Accordion>
                                        </td>
                         

</ContentTemplate>
                   </asp:UpdatePanel>
              
           </tr>
           <tr>
               <td style="width:1000px;">
<div id="sample" style="position:relative">
  <div id="myDiagramDiv" style="background-color: white; width: 100%; height: 700px"></div>
  <div id="myOverviewDiv"></div> <!-- Styled in a <style> tag at the top of the html page --> 
  <input type="search" id="mySearch" onkeypress="if (event.keyCode === 13) searchDiagram()" />
  <button onclick="searchDiagram();return false;">Search</button>
</div>
               </td>
           </tr>
       </table>

</body>

</html>

    </asp:Content>