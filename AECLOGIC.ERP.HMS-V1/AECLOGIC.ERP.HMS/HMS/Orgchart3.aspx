<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Orgchart3.aspx.cs" Inherits="AECLOGIC.ERP.HMS.HMS.Orgchart3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<!DOCTYPE html>
<html>
<head>

<meta charset="UTF-8">
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
    function initEdit() {
       // init for these samples -- you don't need to call this
        var $ = go.GraphObject.make;  // for conciseness in defining templates
        myDiagram =
          $(go.Diagram, "myDiagramDivEdit", // must be the ID or reference to div
            {
                initialContentAlignment: go.Spot.Center,
                // make sure users can only create trees
                validCycle: go.Diagram.CycleDestinationTree,
                // users can select only one part at a time
                maxSelectionCount: 1,
                layout:
                  $(go.TreeLayout,
                    {
                        treeStyle: go.TreeLayout.StyleLastParents,
                        arrangement: go.TreeLayout.ArrangementHorizontal,
                        // properties for most of the tree:
                        angle: 90,
                        layerSpacing: 35,
                        // properties for the "last parents":
                        alternateAngle: 90,
                        alternateLayerSpacing: 35,
                        alternateAlignment: go.TreeLayout.AlignmentBus,
                        alternateNodeSpacing: 20
                    }),
                // support editing the properties of the selected person in HTML
                "ChangedSelection": onSelectionChanged,
                "TextEdited": onTextEdited,
                // enable undo & redo
                "undoManager.isEnabled": true
            });
        // when the document is modified, add a "*" to the title and enable the "Save" button
        myDiagram.addDiagramListener("Modified", function (e) {
            var button = document.getElementById("SaveButton");
            if (button) button.disabled = !myDiagram.isModified;
            var idx = document.title.indexOf("*");
            if (myDiagram.isModified) {
                if (idx < 0) document.title += "*";
            } else {
                if (idx >= 0) document.title = document.title.substr(0, idx);
            }
        });
        var levelColors = ["#AC193D/#BF1E4B", "#2672EC/#2E8DEF", "#8C0095/#A700AE", "#5133AB/#643EBF",
                           "#008299/#00A0B1", "#D24726/#DC572E", "#008A00/#00A600", "#094AB2/#0A5BC4"];
        // override TreeLayout.commitNodes to also modify the background brush based on the tree depth level
        myDiagram.layout.commitNodes = function () {
            go.TreeLayout.prototype.commitNodes.call(myDiagram.layout);  // do the standard behavior
            // then go through all of the vertexes and set their corresponding node's Shape.fill
            // to a brush dependent on the TreeVertex.level value
            myDiagram.layout.network.vertexes.each(function (v) {
                if (v.node) {
                    var level = v.level % (levelColors.length);
                    var colors = levelColors[level].split("/");
                    var shape = v.node.findObject("SHAPE");
                    if (shape) shape.fill = $(go.Brush, "Linear", { 0: colors[0], 1: colors[1], start: go.Spot.Left, end: go.Spot.Right });
                }
            });
        }
        // when a node is double-clicked, add a child to it
        function nodeDoubleClick(e, obj) {
            var clicked = obj.part;
            if (clicked !== null) {
                var thisemp = clicked.data;
                myDiagram.startTransaction("add employee");
                var nextkey = (myDiagram.model.nodeDataArray.length + 1).toString();
                var newemp = { key: nextkey, name: "(new person)", title: "", parent: thisemp.key };
                myDiagram.model.addNodeData(newemp);
                myDiagram.commitTransaction("add employee");
            }
        }
        // this is used to determine feedback during drags
        function mayWorkFor(node1, node2) {
            if (!(node1 instanceof go.Node)) return false;  // must be a Node
            if (node1 === node2) return false;  // cannot work for yourself
            if (node2.isInTreeOf(node1)) return false;  // cannot work for someone who works for you
            return true;
        }
        // This function provides a common style for most of the TextBlocks.
        // Some of these values may be overridden in a particular TextBlock.
        function textStyle() {
            return { font: "9pt  Segoe UI,sans-serif", stroke: "white" };
        }
        // This converter is used by the Picture.
        function findHeadShot(key) {
            // There are only 16 images on the server
            return "../EmpImages/" + key + ".jpg";
        };
        // define the Node template
        myDiagram.nodeTemplate =
          $(go.Node, "Auto",
            { doubleClick: nodeDoubleClick },
            { // handle dragging a Node onto a Node to (maybe) change the reporting relationship
                mouseDragEnter: function (e, node, prev) {
                    var diagram = node.diagram;
                    var selnode = diagram.selection.first();
                    if (!mayWorkFor(selnode, node)) return;
                    var shape = node.findObject("SHAPE");
                    if (shape) {
                        shape._prevFill = shape.fill;  // remember the original brush
                        shape.fill = "darkred";
                    }
                },
                mouseDragLeave: function (e, node, next) {
                    var shape = node.findObject("SHAPE");
                    if (shape && shape._prevFill) {
                        shape.fill = shape._prevFill;  // restore the original brush
                    }
                },
                mouseDrop: function (e, node) {
                    var diagram = node.diagram;
                    var selnode = diagram.selection.first();  // assume just one Node in selection
                    if (mayWorkFor(selnode, node)) {
                        // find any existing link into the selected node
                        var link = selnode.findTreeParentLink();
                        if (link !== null) {  // reconnect any existing link
                            link.fromNode = node;
                        } else {  // else create a new link
                            diagram.toolManager.linkingTool.insertLink(node, node.port, selnode, selnode.port);
                        }
                    }
                }
            },
            // for sorting, have the Node.text be the data.name
            new go.Binding("text", "name"),
            // bind the Part.layerName to control the Node's layer depending on whether it isSelected
            new go.Binding("layerName", "isSelected", function (sel) { return sel ? "Foreground" : ""; }).ofObject(),
            // define the node's outer shape
            $(go.Shape, "Rectangle",
              {
                  name: "SHAPE", fill: "white", stroke: null,
                  // set the port properties:
                  portId: "", fromLinkable: true, toLinkable: true, cursor: "pointer"
              }),
            $(go.Panel, "Horizontal",
              $(go.Picture,
                {
                    name: 'Picture',
                    desiredSize: new go.Size(50, 50),
                    margin: new go.Margin(6, 8, 6, 10),
                },
                new go.Binding("source", "key", findHeadShot)),
              // define the panel where the text will appear
              $(go.Panel, "Table",
                {
                    maxSize: new go.Size(150, 999),
                    margin: new go.Margin(6, 10, 0, 3),
                    defaultAlignment: go.Spot.Left
                },
                $(go.RowColumnDefinition, { column: 2, width: 4 }),
                $(go.TextBlock, textStyle(),  // the name
                  {
                      row: 0, column: 0, columnSpan: 5,
                      font: "12pt Segoe UI,sans-serif",
                      editable: true, isMultiline: false,
                      minSize: new go.Size(10, 16)
                  },
                  new go.Binding("text", "name").makeTwoWay()),
                $(go.TextBlock, "Title: ", textStyle(),
                  { row: 1, column: 0 }),
                $(go.TextBlock, textStyle(),
                  {
                      row: 1, column: 1, columnSpan: 4,
                      editable: true, isMultiline: false,
                      minSize: new go.Size(10, 14),
                      margin: new go.Margin(0, 0, 0, 3)
                  },
                  new go.Binding("text", "title").makeTwoWay()),
                $(go.TextBlock, textStyle(),
                  { row: 2, column: 0 },
                  new go.Binding("text", "key", function (v) { return "ID: " + v; })),
                $(go.TextBlock, textStyle(),
                  { row: 2, column: 3, },
                  new go.Binding("text", "parent", function (v) { return "Boss: " + v; })),
                $(go.TextBlock, textStyle(),  // the comments
                  {
                      row: 3, column: 0, columnSpan: 5,
                      font: "italic 9pt sans-serif",
                      wrap: go.TextBlock.WrapFit,
                      editable: true,  // by default newlines are allowed
                      minSize: new go.Size(10, 14)
                  },
                  new go.Binding("text", "comments").makeTwoWay())
              )  // end Table Panel
            ) // end Horizontal Panel
          );  // end Node
        // define the Link template
        myDiagram.linkTemplate =
          $(go.Link, go.Link.Orthogonal,
            { corner: 5, relinkableFrom: true, relinkableTo: true },
            $(go.Shape, { strokeWidth: 4, stroke: "#00a4a4" }));  // the link shape
        // read in the JSON-format data from the "mySavedModel" element
        load();
    }
    // Allow the user to edit text when a single node is selected
    function onSelectionChanged(e) {
        var node = e.diagram.selection.first();
        if (node instanceof go.Node) {
            updateProperties(node.data);
        } else {
            updateProperties(null);
        }
    }
    // Update the HTML elements for editing the properties of the currently selected node, if any
    function updateProperties(data) {
        if (data === null) {
            document.getElementById("propertiesPanel").style.display = "none";
            document.getElementById("name").value = "";
            document.getElementById("title").value = "";
            document.getElementById("comments").value = "";
        } else {
            document.getElementById("propertiesPanel").style.display = "block";
            document.getElementById("name").value = data.name || "";
            document.getElementById("title").value = data.title || "";
            document.getElementById("comments").value = data.comments || "";
        }
    }
    // This is called when the user has finished inline text-editing
    function onTextEdited(e) {
        var tb = e.subject;
        if (tb === null || !tb.name) return;
        var node = tb.part;
        if (node instanceof go.Node) {
            updateProperties(node.data);
        }
    }
    // Update the data fields when the text is changed
    function updateData(text, field) {
        var node = myDiagram.selection.first();
        // maxSelectionCount = 1, so there can only be one Part in this collection
        var data = node.data;
        if (node instanceof go.Node && data !== null) {
            var model = myDiagram.model;
            model.startTransaction("modified " + field);
            if (field === "name") {
                model.setDataProperty(data, "name", text);
            } else if (field === "title") {
                model.setDataProperty(data, "title", text);
            } else if (field === "comments") {
                model.setDataProperty(data, "comments", text);
            }
            model.commitTransaction("modified " + field);
        }
    }
    // Show the diagram's model in JSON format
    //function save() {
    //    //document.getElementById("mySavedModel").value = myDiagram.model.toJson();
    //    myDiagram.isModified = false;
    //}
    function load() {
        //myDiagram.model = go.Model.fromJson();
       
        var WsID = document.getElementById('<%=ddlWS_hid.ClientID%>').value;
        var DeptID = document.getElementById('<%=ddlWS_hid.ClientID%>').value;
        var Resultset = AjaxDAL.TestEmpOrgChart(WsID, DeptID);
        var jsonObject = [];
      
        for (i = 0; i < Resultset.value.Rows.length; i++) {
            var jsonItem = {};
            if (i == 0) {
                jsonItem.key = Resultset.value.Rows[i].key;
                jsonItem.name = Resultset.value.Rows[i].name;             
                jsonItem.title = Resultset.value.Rows[i].title;
                jsonItem.DeptName = Resultset.value.Rows[i].DeptName;
            }

            else {
                jsonItem.key = Resultset.value.Rows[i].key;
               
                jsonItem.name = Resultset.value.Rows[i].name;
               
                jsonItem.title = Resultset.value.Rows[i].title;
                jsonItem.DeptName = Resultset.value.Rows[i].DeptName;
                jsonItem.parent = Resultset.value.Rows[i].boss;
            }

            jsonObject.push(jsonItem);
        }
        myDiagram.model = go.Model.fromJson({ "class": "go.TreeModel", "nodeDataArray": jsonObject });          
 
         
    }
</script>
</head>
<body>
   <table>
       <tr>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

                                        <td style="height: 26px; text-align: left;width:1000px;">
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
                                                                                                                       </asp:DropDownList>--%>
                            <asp:DropDownList ID="ddlWS" Visible="false" runat="server" Width="200" CssClass="droplist" 
                                         TabIndex="1">
                                    </asp:DropDownList>
                                <%--</td>
                                <td>--%>  <asp:HiddenField ID="ddlWS_hid" runat="server" />
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
                                    <asp:TextBox ID="txtdept"  Height="22px" Width="189px" runat="server"></asp:TextBox>
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
           <td>
   <div id="sample">
  <div id="myDiagramDivEdit" style="background-color: #696969; border: solid 1px black; height: 500px;width:1000px;"></div>
  <div>
    <div id="propertiesPanel" style="display: none; background-color: aliceblue; border: solid 1px black">
      <b>Properties</b><br />
      Name: <input type="text" id="name" value="" onchange="updateData(this.value, 'name')" /><br />
      Title: <input type="text" id="title" value="" onchange="updateData(this.value, 'title')" /><br />
      Comments: <input type="text" id="comments" value="" onchange="updateData(this.value, 'comments')" /><br />
    </div>
  </div>

  <div>
  </div>
</div>
           </td>
       </tr>
   </table>

</body>
</html>
    </asp:Content>