<%@ Page Language="C#"  AutoEventWireup="True" CodeBehind="NMRChart.aspx.cs"  Inherits="AECLOGIC.ERP.HMS.NMRChart" Title="" %>
<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">


    <script language="javascript" type="text/javascript">
    
     function RightClick(event) {
            var obj = event.srcElement || event.target;
            var seltreeNode = obj;
            //alert(seltreeNode.innerHTML); //This will prompt selected Node Text
            //seltreeNode.innerHTML = ""; //This will change the selected node text 
            
            var EmpID=new String(seltreeNode.title); 
            //var EmpID =strID.split('\\\\')[strID.split('\\\\').length-1];
            if(EmpID!="")
            test(EmpID,obj);
            else
            HideDisplay();
        }
        
    function showWindowDailog(url,width,height)
    {
        var newWindow = window.showModalDialog(url, '','dialogHeight: '+ height +'px; dialogWidth: ' + width +'px; edge: Raised; center: Yes; resizable: Yes; status: No;');
    }
 function test(EmpID,TVId)
 {
  
   var StrEmp=new String(document.getElementById('<%=hdn.ClientID%>').value);
   var Det;
   
   for(i=0;i<StrEmp.split('^').length;i++)
   {
     Det=new String(StrEmp.split('^')[i]);
    
     if(EmpID==Det.split('~')[0])
     {
       document.getElementById('lblID').innerText=EmpID;
       document.getElementById('lblName').innerText=Det.split('~')[1];
       document.getElementById('lblMobile1').innerText=Det.split('~')[2];
       document.getElementById('lblMobile2').innerText=Det.split('~')[3];
       document.getElementById('ancMail').innerText=Det.split('~')[4];
       document.getElementById('ancMail').href="mailto:" +Det.split('~')[4];
       document.getElementById('ancAltMail').innerText=Det.split('~')[6];
       document.getElementById('ancAltMail').href="mailto:" +Det.split('~')[6];
      
       document.getElementById('linkAttendence').title="OrgViewAttendance.aspx?Empid=" + EmpID;
       document.getElementById('linkResponsibilities').title="Responsibility.aspx?Empid=" + EmpID;
       document.getElementById('linkHistory').title="History.aspx?Empid=" + EmpID;
       
       if(Det.split('~')[5]!="")
       document.getElementById('imgPhoto').src="../HMS/EmpImages/"+Det.split('~')[5];
       else
       document.getElementById('imgPhoto').src="../HMS/EmpImages/0.jpg";
       
       
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
          document.getElementById('tblDisplay').style.left=tempX+100;
          document.getElementById('tblDisplay').style.position="absolute";
          document.getElementById('tblDisplay').style.visibility="visible";     
          break;
          }
      else
          {
           document.getElementById('tblDisplay').style.visibility="hidden";
          }
   } return false;
 }
function testAdd


//    function test1(EmpID, TVId) {
//       
//        var EmpDetails = AjaxDAL.GetClientEmployee(EmpID);
//                if (EmpDetails.erorr == null && EmpDetails.value != null) {

//                    document.getElementById('lblID').innerText = EmpID;
//                         document.getElementById('lblName').innerText=EmpDetails.value.;
//                         document.getElementById('lblMobile1').innerText=EmpDetails.value.;
//                         document.getElementById('lblMobile2').innerText=EmpDetails.value.;
//                         document.getElementById('ancMail').innerText=EmpDetails.value.;
//                         document.getElementById('ancMail').href="mailto:" +EmpDetails.value.;
//                         document.getElementById('ancAltMail').innerText=EmpDetails.value.;
//                         document.getElementById('ancAltMail').href="mailto:" +EmpDetails.value.;
//                         document.getElementById('linkAttendence').title="OrgViewAttendance.aspx?Empid=" + EmpID;
//                         document.getElementById('linkResponsibilities').title="Responsibility.aspx?Empid=" + EmpID;
//                         document.getElementById('linkHistory').title="History.aspx?Empid=" + EmpID;
//                         
//                         if(EmpDetails.value.="")
//                         document.getElementById('imgPhoto').src="../HMS/EmpImages/"+EmpDetails.value.;
//                         else
//                         document.getElementById('imgPhoto').src="../HMS/EmpImages/0.jpg";

//        

//                var top = document.body.scrollTop
//    ? document.body.scrollTop
//    : (window.pageYOffset
//        ? window.pageYOffset
//        : (document.body.parentElement
//            ? document.body.parentElement.scrollTop
//            : 0
//        )
//    );

//                if (tempY + top + 420 >= (top + window.screen.availHeight)) {
//                    document.getElementById('tblDisplay').style.top = (tempY + top) - ((tempY + 440) - (window.screen.availHeight));
//                }
//                else {
//                    document.getElementById('tblDisplay').style.top = (tempY + top);
//                }

//                document.getElementById('tblDisplay').style.left = tempX + 100;
//                document.getElementById('tblDisplay').style.position = "absolute";
//                document.getElementById('tblDisplay').style.visibility = "visible";


//                
//            } else {
//                document.getElementById('tblDisplay').style.visibility = "hidden";

//            }
//            return false;
//        } 
   


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
 document.getElementById('tblDisplay').style.visibility="hidden";

}
function ReLoadOrg()
{
    if(document.getElementById('chkimage').checked)
    {
       // alert(window.location);
        window.location.href="./OrgChart.aspx?id=1";
    }
    else{
        window.location.href="./OrgChart.aspx?id=0";
    
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

</script>

    <div style="height: auto;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                
                    <tr>
                        <td colspan="2" class="pageheader">
                            Organization Chart Labor
                        </td>
                    </tr>
                    
                    <tr style="height: auto">
                        <td width="30%" valign="top" style="height: auto">
                            <asp:TreeView ID="tvProjects" ExpandDepth="1" runat="server" ImageSet="Simple" 
                                OnSelectedNodeChanged="tvProjects_SelectedNodeChanged" Font-Size="Large" LineImagesFolder="~/TreeLineImages"
                                ShowLines="True"  NodeIndent="40" EnableTheming="True">
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
                    
                    <tr>
                        <td class="pageheader">
                    Attendance
                </td>
                    </tr>
                    <tr>
                        <td align="left">
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
                       <td>
                         &nbsp;
                       </td> 
                       <td>
                         &nbsp;
                       </td> 
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <asp:HiddenField ID="hdn" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

