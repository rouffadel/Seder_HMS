<%@ Page Language="C#"   AutoEventWireup="True" CodeBehind="OrgChartold.aspx.cs"
    Inherits="AECLOGIC.ERP.HMS.OrgChart1" Title="" %>
 <%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">  
    <html>
 <head>
           <style type="text/css">
             #myOverviewDiv {
  position:absolute;
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
 <script src="../Includes/JS/goSamples.js"></script>
<script language="javascript" type="text/javascript">
    

  

     function RightClick(event)
      {
            var obj = event.srcElement || event.target;
            var seltreeNode = obj;
            var EmpID=new String(seltreeNode.title); 
            if(EmpID!="")
            test(EmpID,obj);
      }
        
    function showWindowDailog(url,width,height)
    {
        var newWindow = window.showModalDialog(url, '','dialogHeight: '+ height +'px; dialogWidth: ' + width +'px; edge: Raised; center: Yes; resizable: Yes; status: No;');
    }
  function test(EmpID,TVId)
 {
 
  var vEmployee = AjaxDAL.GetEmployee(EmpID);
  
  
    if(vEmployee.erorr ==null)
    {
        //asign info
        document.getElementById('lblID').innerText=EmpID;
        document.getElementById('lblName').innerText=vEmployee.value.Name;
        document.getElementById('lblMobile1').innerText=vEmployee.value.Mobile1;
        document.getElementById('lblMobile2').innerText=vEmployee.value.Mobile2;
        document.getElementById('ancMail').innerText=vEmployee.value.Mailid;
        document.getElementById('ancMail').href="mailto:" +vEmployee.value.Mailid;
        document.getElementById('ancAltMail').innerText=vEmployee.value.AltMail;
        document.getElementById('ancAltMail').href="mailto:" +vEmployee.value.AltMail;
        document.getElementById('lblCategory').innerText=vEmployee.value.Category;
        lnkShowImage.style.visibility="visible";
        document.getElementById('imgPhoto').style.visibility="hidden";
        document.getElementById('imgPhoto').style.width="1px";
        document.getElementById('imgPhoto').style.height="1px";
        document.getElementById('linkAttendence').title="OrgViewAttendance.aspx?Empid=" + EmpID;
        document.getElementById('linkResponsibilities').title="Responsibility.aspx?Empid=" + EmpID;
        document.getElementById('linkHistory').title="History.aspx?Empid=" + EmpID;             
        //document.getElementById('lblHdnImg').value= vEmployee.value.Image;
        document.getElementById('lblHdnImg').value= "./EmpImages/"+ vEmployee.value.Image;
        
        //Table Display
        var top = document.body.scrollTop
            ? document.body.scrollTop
            : (window.pageYOffset
                ? window.pageYOffset
                : (document.body.parentElement
                    ? document.body.parentElement.scrollTop
                    : 0
                )
            );
    
        if(tempY+top+420 >=(top+window.screen.availHeight)){
                document.getElementById('tblDisplay').style.top=(tempY+ top)- ((tempY+ 440)-(window.screen.availHeight)) ;}
                else{
                    document.getElementById('tblDisplay').style.top=(tempY+ top);}
          
                document.getElementById('tblDisplay').style.left=tempX+200;
                document.getElementById('tblDisplay').style.position="absolute";
//                document.getElementById('tblDisplay').style.visibility="visible";     
       ShowDisplay();
    }
    else
    {
        document.getElementById('tblDisplay').style.visibility="hidden";
        document.getElementById('imgPhoto').style.visibility="hidden";     
    }
  

//   var StrEmp=new String(document.getElementById('<%=hdn.ClientID%>').value);
//   var Det;
//   
//   for(i=0;i<StrEmp.split('^').length;i++)
//   {
//     Det=new String(StrEmp.split('^')[i]);
//    
//     if(EmpID==Det.split('~')[0])
//     {
//       document.getElementById('lblID').innerText=EmpID;
//       document.getElementById('lblName').innerText=Det.split('~')[1];
//       document.getElementById('lblMobile1').innerText=Det.split('~')[2];
//       document.getElementById('lblMobile2').innerText=Det.split('~')[3];
//       document.getElementById('ancMail').innerText=Det.split('~')[4];
//       document.getElementById('ancMail').href="mailto:" +Det.split('~')[4];
//       document.getElementById('ancAltMail').innerText=Det.split('~')[6];
//       document.getElementById('ancAltMail').href="mailto:" +Det.split('~')[6];
//       document.getElementById('lblCategory').innerText=Det.split('~')[7];
//        lnkShowImage.style.visibility="visible";
//        document.getElementById('imgPhoto').style.visibility="hidden";
//        document.getElementById('imgPhoto').style.width="1px";
//        document.getElementById('imgPhoto').style.height="1px";
//       document.getElementById('linkAttendence').title="OrgViewAttendance.aspx?Empid=" + EmpID;
//       document.getElementById('linkResponsibilities').title="Responsibility.aspx?Empid=" + EmpID;
//       document.getElementById('linkHistory').title="History.aspx?Empid=" + EmpID;
//       
//       if(Det.split('~')[5]!="")
//       document.getElementById('lblHdnImg').value="./EmpImages/"+Det.split('~')[5];
//       else
//       document.getElementById('lblHdnImg').value="./EmpImages/0.jpg";
//       //document.getElementById('imgPhoto').src="../HMS/EmpImages/0.jpg";
//       var top = document.body.scrollTop
//    ? document.body.scrollTop
//    : (window.pageYOffset
//        ? window.pageYOffset
//        : (document.body.parentElement
//            ? document.body.parentElement.scrollTop
//            : 0
//        )
//    );
//    
//     if(tempY+top+420 >=(top+window.screen.availHeight)){
//        document.getElementById('tblDisplay').style.top=(tempY+ top)- ((tempY+ 440)-(window.screen.availHeight)) ;}
//      else{
//          document.getElementById('tblDisplay').style.top=(tempY+ top);}
//          
//      document.getElementById('tblDisplay').style.left=tempX+200;
//      document.getElementById('tblDisplay').style.position="absolute";
//      document.getElementById('tblDisplay').style.visibility="visible";     
//       
//       
//       break;
//     }
//    else
//    {
//        document.getElementById('tblDisplay').style.visibility="hidden";
//        document.getElementById('imgPhoto').style.visibility="hidden";     
//    }
  // } 
   return false;
 }


// Detect if the browser is IE or not.
// If it is not IE, we assume that the browser is NS.
var IE = document.all?true:false

// If NS -- that is, !IE -- then set up for mouse capture
if (!IE) document.captureEvents(Event.MOUSEMOVE)

// Set-up to use getMouseXY function onMouseMove
        document.onmousemove = getMouseXY;

// Temporary variables to hold mouse x-y pos.s
var tempX = 0
var tempY = 0

// Main function to retrieve mouse x-y pos.s

function getMouseXY(e) {
  if (IE) { // grab the x-y pos.s if browser is IE
    tempX = event.clientX + document.body.scrollLeft
    tempY = event.clientY + document.body.scrollTop
  } else {  // grab the x-y pos.s if browser is NS
    tempX = e.pageX
    tempY = e.pageY
  }  
  // catch possible negative values in NS4
  if (tempX < 0){tempX = 0}
  if (tempY < 0){tempY = 0}  
  // show the position values in the form named Show
  // in the text fields named MouseX and MouseY
 
  return true
}
function HideDisplay()
{
    document.getElementById('imgPhoto').style.visibility="hidden";
 document.getElementById('tblDisplay').style.visibility="hidden";

}
function ShowDisplay()
{
   if(document.getElementById('chkDetails').checked)
    { 
  document.getElementById('tblDisplay').style.visibility="visible";
 }
 else{
 document.getElementById('tblDisplay').style.visibility="hidden";
 }

}
function ReLoadOrg()
{
    if(document.getElementById('chkimage').checked)
    {
       // alert(window.location);
        window.location.href="./OrgChartold.aspx?id=1";
    }
    else{
        window.location.href="./OrgChartold.aspx?id=0";
    
    }
}
function chkChange(flg)
{
    document.getElementById('chkimage').checked=flg;   
}
function Preload() {

    HideDisplay();
    var args = Preload.arguments; 
    document.imageArray = new Array(args.length);
    for(var i=0; i<args.length; i++) {
        document.imageArray[i] = new Image;
        document.imageArray[i].src = args[i];
    }
}
  
   function changeStyle(TvID)
   {
   
     var Tags= document.getElementById(TvID).getElementsByTagName("A");
     for(var i=0;i<Tags.length;i++)
     {
        var ObjEle=Tags[i];
        var str=new String(ObjEle.href);
        
        
//        if(ObjEle.href.substring(ObjEle.href.lastIndexOf('\\\\')+2,ObjEle.href.length-2)=="0")
        if(str.split('\\\\').length==3)
        {
            ObjEle.style.fontWeight="bold";     
            ObjEle.style.textDecoration="none";
            ObjEle.style.color="#339";
            ObjEle.href="#";     
            ObjEle.style.textDecoration="none";
            ObjEle.style.cursor="none";
        }
        if(str.split('\\\\').length==2)
        {
            ObjEle.style.fontWeight="bold";     
            ObjEle.style.textDecoration="none";
            ObjEle.style.color="#cc4a01";
            //ObjEle.href="#";     
            ObjEle.style.textDecoration="none";
            //ObjEle.style.cursor="none";
        }
     }
   }
//--> 
function  ShowImg()
{
    lnkShowImage.style.visibility="hidden";
    document.getElementById('imgPhoto').src=document.getElementById('lblHdnImg').value;
    document.getElementById('imgPhoto').style.visibility="visible";
    document.getElementById('imgPhoto').style.width="128px";
    document.getElementById('imgPhoto').style.height="160px";
}
    </script>
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
            return "EmpImages/" + nation + ".jpg";
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
              new go.Binding("fill", "isHighlighted", function (h) { return h ? "red" : "azure"; }).ofObject()),
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
            $(go.Shape, { stroke: '#222' }));  // the default black link shape
        // set up the nodeDataArray, describing each person/position
        var WsID = document.getElementById('<%=ddlWS_hid.ClientID%>').value;
        var DeptID = document.getElementById('<%=ddlDept_hid.ClientID%>').value;
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
            {
                nodeParentKeyProperty: "boss",  // this property refers to the parent node data
                nodeDataArray: nodeDataArray
            });
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
   <script >
          
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
            return "EmpImages/" + key + ".jpg";
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
        var DeptID = document.getElementById('<%=ddlDept_hid.ClientID%>').value;
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
    <div style="height: auto;">
        <table width="100%">
            <tr>
                <td  width="100%">
                    <asp:RadioButtonList ID="rbTaxasion" Font-Bold="true" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rbTaxasion_SelectedIndexChanged" visible="true">
                            <asp:ListItem Text="Tree View" Selected="True" Value="1"></asp:ListItem>
                        
                        </asp:RadioButtonList>   
                </td>             
           <td style="height: 20px;  text-align: left;" id="searchCriteria" runat="server">
                                            <cc1:Accordion ID="MyAccordion" runat="server" Width="550px" 
                                                ContentCssClass="accordionContent" AutoSize="None" FadeTransitions="false" TransitionDuration="50"
                                                FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                                <Panes>
                                                    <cc1:AccordionPane ID="AccordionPane1" runat="server" 
                                                        ContentCssClass="accordionContent">
                                                       
                                                        <Content>
                                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                      
                            <asp:DropDownList ID="ddlWS" Visible="false" runat="server" Width="200" CssClass="droplist" 
                                        OnSelectedIndexChanged="ddlWS_SelectedIndexChanged" TabIndex="1">
                                    </asp:DropDownList>
                                <%--</td>
                                <td>--%>  <asp:HiddenField ID="ddlWS_hid" runat="server" />
                                    <asp:TextBox ID="txtSearchWorksite"  Height="16px" Width="189px" runat="server"></asp:TextBox>
                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="GetCompletionList" ServicePath="" TargetControlID="txtSearchWorksite"
                                                UseContextKey="True" CompletionInterval="10" CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="GetID">
                                            </cc1:AutoCompleteExtender>
                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtSearchWorksite"
                                                WatermarkCssClass="watermark" WatermarkText="[Enter Worksite Name]">
                                            </cc1:TextBoxWatermarkExtender>
                                                                        </strong>&nbsp;&nbsp;   <asp:HiddenField ID="ddlDept_hid" runat="server" />
                               <asp:DropDownList ID="ddlDept" Visible="false" runat="server" Width="200" CssClass="droplist" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" TabIndex="2">
                                    </asp:DropDownList>
                                    <cc1:ListSearchExtender ID="ListSearchExtender2" IsSorted="true" PromptText="Type Here To Search..." PromptPosition="Top" 
                                     PromptCssClass="PromptText" QueryPattern="Contains" runat="server" TargetControlID="ddlDept"></cc1:ListSearchExtender>
                                    <asp:TextBox ID="txtdept"  Height="16px" Width="189px" runat="server"></asp:TextBox>
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

            </tr>
           
            <tr>
                <td colspan="2" id="checkeditems" runat="server">
                    &nbsp;<input type="checkbox" id="chkimage" onclick="javascript:ReLoadOrg();" name="With Images" />With
                    Images&nbsp;<input type="checkbox"  id="chkDetails" onclick="javascript:ShowDisplay();" name="With Details" />With Details
                   
                    <br />
                </td>
            </tr>
            <tr style="height: auto">
                <td width="30%" valign="top" style="height: auto">
                    <asp:TreeView ID="tvDirectors" ExpandDepth="1" runat="server" ImageSet="Simple" Font-Size="Large"
                        LineImagesFolder="~/TreeLineImages" ShowLines="True" NodeIndent="40" 
                        EnableTheming="True" >
                        <ParentNodeStyle Font-Bold="False" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <NodeStyle Font-Names="Verdana" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px"
                            NodeSpacing="0px" VerticalPadding="0px" Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                    </asp:TreeView>
                </td>
                <td style="vertical-align: top; height: auto">
                </td>
            </tr>
            <tr style="height: auto">
                <td width="30%" id="OldOrgChart"  runat="server" valign="top" style="height: auto">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                       
                            <asp:TreeView ID="tvProjects" ExpandDepth="1" runat="server" ImageSet="Simple"
                                OnSelectedNodeChanged="tvProjects_SelectedNodeChanged" Font-Size="Large" LineImagesFolder="~/TreeLineImages"
                                ShowLines="True" NodeIndent="40" EnableTheming="True">
                                <ParentNodeStyle Font-Bold="False" />
                                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                                    VerticalPadding="0px" />
                                <NodeStyle Font-Names="Verdana" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px"
                                    NodeSpacing="0px" VerticalPadding="0px" Font-Bold="False" />
                                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                            </asp:TreeView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                   <div class="UpdateProgressCSS">
                <ajax:UpdateProgress ID="updateProgress" AssociatedUpdatePanelID="UpdatePanel1" runat="server" DisplayAfter="1">
                    <ProgressTemplate>
                        <asp:Panel ID="pnlFirst" CssClass="overlay" runat="server">
                            <asp:Panel ID="pnlSecond" CssClass="loader" runat="server">
                                <img src="Images/Loading.gif"   alt="update is in progress" />
                                <input id="btnCacel" onclick="CancelAsyncPostBack()" type="button" value="Cancel" /></asp:Panel>
                        </asp:Panel>
                    </ProgressTemplate>
                </ajax:UpdateProgress>
                
            </div>
                </td>
                <td style="vertical-align: top; height: auto">
                </td>
            </tr>
            <tr id="NewOrgChart" runat="server">
                <td colspan="5">
                    <div id="sample" style="position:relative;margin-left:0px;">
  <div id="myDiagramDiv" style="background-color: white;  width:1500px; height: 700px"></div>
  <div id="myOverviewDiv"></div> <!-- Styled in a <style> tag at the top of the html page --> 
  <input type="search" id="mySearch" onkeypress="if (event.keyCode === 13) searchDiagram()" />
  <button onclick="searchDiagram();return false;">Search</button>
                   </div>
                </td>
               
            </tr>   
            <tr id="NewOrgChartwithImages" runat="server">
                <td colspan="5">
                  <div id="samplewithImages">
                     <div id="myDiagramDivEdit" style="background-color: #696969; border: solid 1px black; height: 500px;width:1500px;"></div>                      
                          <div>
                               <div id="propertiesPanel" style="display: none; background-color: aliceblue; border: solid 1px black">
                                  <b>Properties</b><br />
                                    Name: <input type="text" id="name" value="" onchange="updateData(this.value, 'name')" /><br />
                                    Title: <input type="text" id="title" value="" onchange="updateData(this.value, 'title')" /><br />
                                    Comments: <input type="text" id="comments" value="" onchange="updateData(this.value, 'comments')" /><br />
                                   </div>
                                  
                              </div>
                      </div>
                </td>
               
            </tr>    
                                  
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="pageheader">
                    Legend
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="pageheader">
                    Attendance
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td>
                                Directors
                            </td>
                            <td>
                                Management
                            </td>
                            <td>
                                Manager
                            </td>
                            <td>
                                Department
                            </td>
                            <td>
                                Head
                            </td>
                            <td>
                                General
                            </td>
                            <td>
                                OD
                            </td>
                        </tr>
                        <tr>
                            <td class="tvOrgBOD_TD">
                                &nbsp;
                            </td>
                            <td class="tvOrgDirectors_TD">
                                &nbsp;
                            </td>
                            <td class="tvOrgPrjMan_TD">
                                &nbsp;
                            </td>
                            <td class="tvOrgDept_TD">
                                &nbsp;
                            </td>
                            <td class="tvOrgDeptHead_TD">
                                &nbsp;
                            </td>
                            <td class="tvOrgNode_TD">
                                &nbsp;
                            </td>
                            <td class="tvODNode_TD">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table border="1px" cellpadding="2" cellspacing="2">
                        <tr>
                            <td style="width: 100px;">
                                <b>Present(s)</b>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblpresent" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Absent(s)</b>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lblabsent" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Total</b>
                            </td>
                            <td style="width: 50px;">
                                <asp:Label ID="lbltot" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="Footnote">
                <td colspan="4">
                    <b>Note:</b> Department wise attendance may not show correct numbers when a person
                    belonging to one department holds additional charge(s) of other department(s). His
                    parent department shows attendance marked and the additional charge(s) holding department(s)
                    will not show attendance.
                </td>
            </tr>
        </table>
        <table style="border-bottom: black 1px solid; position: absolute; 
            border-left: black 1px solid;width: 500px; border-collapse: collapse;
            border-top: black 1px solid; top: 0px;
            border-right: black 1px solid; left: 500px; background-color: #ffffff" runat="server" id="tblDisplay" 
            border="0" cellspacing="0"  cellpadding="3">
            <tbody>
                <tr>
                    <td style="background-color: #d56511; color: white" colspan="4">
                        <strong>
                            <label id="lblID">
                            </label>
                            <label id="lblName">
                            </label>
                        </strong>
                    </td>
                    <td colspan="2" onclick="javascript:return tblDisplay.style.visibility='hidden';"
                        style="width: 20px; color: white; background-color: #d56511;">
                        <b style="cursor: pointer">X</b>
                    </td>
                </tr>
                <tr>
                    <td style="width:1px"  rowspan="7">
                        <img alt="" src="Images/" id="imgPhoto" >
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        <strong>Mobile1</strong>
                    </td>
                    <td style="width: 1px">
                        <strong>:</strong>
                    </td>
                    <td style="width: 60%">
                        <label id="lblMobile1">
                        </label>
                    </td>
                    <td style="width: 20px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        <strong>Mobile2</strong>
                    </td>
                    <td style="width: 1px">
                        <strong>:</strong>
                    </td>
                    <td style="width: 60%">
                        <label id="lblMobile2">
                        </label>
                    </td>
                    <td style="width: 20px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        <strong>Email</strong>
                    </td>
                    <td style="width: 1px">
                        <strong>:</strong>
                    </td>
                    <td style="width: 60%">
                        <a id="ancMail"></a>
                    </td>
                    <td style="width: 20px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        <strong>Alt Email</strong>
                    </td>
                    <td style="width: 1px">
                        <strong>:</strong>
                    </td>
                    <td style="width: 60%">
                        <a id="ancAltMail"></a>
                    </td>
                    <td style="width: 20px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        <strong> Category</strong></td>
                    <td style="width: 1px">
                         <strong>:</strong></td>
                    <td style="width: 60%"><label id="lblCategory">
                        </label>
                    </td>
                    <td style="width: 20px">
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <a id="linkAttendence" style="text-decoration: underline" title="Attendance" onclick="javascript:showWindowDailog(this.title,800,300);"
                            href="#">Attendance</a> &nbsp; | &nbsp; <a id="linkResponsibilities" href="#" style="text-decoration: underline"
                                onclick="javascript:showWindowDailog(this.title,850,410);" title="linkDuties">Responsibilities</a>
                        &nbsp; | &nbsp; <a id="linkHistory" href="#" style="text-decoration: underline" onclick="javascript:showWindowDailog(this.title,700,380);"
                            title="linkDuties">History</a>
                    </td>
                    <td style="width: 20px">
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #eeeeee; color: white; height: 10px;">
                    </td>
                    
                    <td colspan="3" style="background-color: #eeeeee; color: white; height: 10px">
                       <a id="lnkShowImage" style=" cursor: pointer; text-decoration:underline;" onclick="ShowImg();" >Click here to view photo</a><input type="hidden" id="lblHdnImg" value="" />
                    </td>
                    <td style="background-color: #eeeeee; color: white; height: 10px">
                        &nbsp;
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:HiddenField ID="hdn" runat="server" />
    </div>
                
    </html>
</asp:Content>
