<%@ Page Title="" Language="C#"   AutoEventWireup="True"
    CodeBehind="ClientChart.aspx.cs" Inherits="AECLOGIC.ERP.HMS.ClientChart1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="~/Templates/topmenu.ascx" TagName="Topmenu" TagPrefix="AEC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function RightClick(event) {
            var obj = event.srcElement || event.target;
            var seltreeNode = obj;
            var EmpID = new String(seltreeNode.title);
            if (EmpID != "")
                test(EmpID, obj);
        }

        function showWindowDailog(url, width, height) {
            var newWindow = window.showModalDialog(url, '', 'dialogHeight: ' + height + 'px; dialogWidth: ' + width + 'px; edge: Raised; center: Yes; resizable: Yes; status: No;');
        }
        function HideDisplay() {

            document.getElementById('tblDisplay').style.visibility = "hidden";

        }

        function test(EmpID, TVId) {
            if (EmpID != -1) {
                var EmpDetails = AjaxDAL.GetClientEmployee(EmpID);
                if (EmpDetails.erorr == null && EmpDetails.value != null) {
                    document.getElementById('lblID').innerText = EmpID;
                    document.getElementById('lblName').innerText = EmpDetails.value.EmpName;
                    document.getElementById('lblMobile1').innerText = EmpDetails.value.ContactNo1;
                    document.getElementById('lblMobile2').innerText = EmpDetails.value.ContactNo2;
                    document.getElementById('lblDesignation').innerText = EmpDetails.value.Designation;
                    document.getElementById('lblMailID').innerText = EmpDetails.value.EmailID;

                    var top = document.body.scrollTop
                      ? document.body.scrollTop
                       : (window.pageYOffset
                       ? window.pageYOffset
                       : (document.body.parentElement
                       ? document.body.parentElement.scrollTop
                       : 0
                       )
                       );
                    if (tempY + top + 420 >= (top + window.screen.availHeight)) {
                        document.getElementById('tblDisplay').style.top = (tempY + top) - ((tempY + 440) - (window.screen.availHeight));
                    }
                    else {
                        document.getElementById('tblDisplay').style.top = (tempY + top);
                    }
                    document.getElementById('tblDisplay').style.left = tempX + 200;
                    document.getElementById('tblDisplay').style.position = "absolute";
                    document.getElementById('tblDisplay').style.visibility = "visible";
                }
                else {
                    document.getElementById('tblDisplay').style.visibility = "hidden";
                }
            }
            return false;
        }









        // function test1(EmpID,TVId)
        // {
        //    
        //   var StrEmp=new String(document.getElementById('<%=hdn.ClientID%>').value);
        //   var Det;
        //   for(i=0;i<StrEmp.split('^').length;i++)
        //   {
        //     Det=new String(StrEmp.split('^')[i]);
        //     if(EmpID==Det.split('~')[0])
        //     {
        //       document.getElementById('lblID').innerText=EmpID;
        //       document.getElementById('lblName').innerText=Det.split('~')[1];
        //       document.getElementById('lblMobile1').innerText=Det.split('~')[2];
        //       document.getElementById('lblMobile2').innerText=Det.split('~')[3];
        //       document.getElementById('lblDesignation').innerText=Det.split('~')[10];
        //       document.getElementById('lblMailID').innerText=Det.split('~')[11];
        //       
        //       
        //       var top = document.body.scrollTop
        //    ? document.body.scrollTop
        //    : (window.pageYOffset
        //        ? window.pageYOffset
        //        : (document.body.parentElement
        //            ? document.body.parentElement.scrollTop
        //            : 0
        //        )
        //    );
        //     if(tempY+top+420 >=(top+window.screen.availHeight)){
        //        document.getElementById('tblDisplay').style.top=(tempY+ top)- ((tempY+ 440)-(window.screen.availHeight)) ;}
        //      else{
        //          document.getElementById('tblDisplay').style.top=(tempY+ top);}
        //      document.getElementById('tblDisplay').style.left=tempX+200;
        //      document.getElementById('tblDisplay').style.position="absolute";
        //      document.getElementById('tblDisplay').style.visibility="visible";     
        //       break;
        //     }else
        //     {
        //      document.getElementById('tblDisplay').style.visibility="hidden";
        //      
        //     }
        //   } return false;
        // }


        // Detect if the browser is IE or not.
        // If it is not IE, we assume that the browser is NS.
        var IE = document.all ? true : false

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
            if (tempX < 0) { tempX = 0 }
            if (tempY < 0) { tempY = 0 }
            // show the position values in the form named Show
            // in the text fields named MouseX and MouseY

            return true
        }

        function changeStyle(TvID) {

            var Tags = document.getElementById(TvID).getElementsByTagName("A");
            for (var i = 0; i < Tags.length; i++) {
                var ObjEle = Tags[i];
                var str = new String(ObjEle.href);


                //        if(ObjEle.href.substring(ObjEle.href.lastIndexOf('\\\\')+2,ObjEle.href.length-2)=="0")
                if (str.split('\\\\').length == 3) {
                    ObjEle.style.fontWeight = "bold";
                    ObjEle.style.textDecoration = "none";
                    ObjEle.style.color = "#339";
                    ObjEle.href = "#";
                    ObjEle.style.textDecoration = "none";
                    ObjEle.style.cursor = "none";
                }
                if (str.split('\\\\').length == 2) {
                    ObjEle.style.fontWeight = "bold";
                    ObjEle.style.textDecoration = "none";
                    ObjEle.style.color = "#cc4a01";
                    //ObjEle.href="#";     
                    ObjEle.style.textDecoration = "none";
                    //ObjEle.style.cursor="none";
                }
            }
        }
        //--> 

    </script>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div style="height: auto;">
                <table width="100%">
                   
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cc1:accordion id="MyAccordion" runat="server" headercssclass="accordionHeader" headerselectedcssclass="accordionHeaderSelected"
                                contentcssclass="accordionContent" autosize="None" fadetransitions="false" transitionduration="50"
                                framespersecond="40" requireopenedpane="false" suppressheaderpostbacks="true">
                                    <Panes>
                                        <cc1:AccordionPane ID="AccordionPane1" runat="server" HeaderCssClass="accordionHeader"
                                            ContentCssClass="accordionContent">
                                            <Header>
                                                Search Criteria</Header>
                                            <Content>
                                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td>
                                    Organizations
                                    <asp:DropDownList ID="ddlorgs" runat="server" AutoPostBack="false" CssClass="droplist" >
                                    </asp:DropDownList>
                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="savebutton" OnClick="btnShow_Click" AccessKey="s" ToolTip="[Alt+s OR Alt+s+Enter]" />
                                </td>
                                </tr>
                        </td>
                    </tr>
                </table>
                </Content> </cc1:AccordionPane> </Panes> </cc1:Accordion> </tr>
                <tr style="height: auto">
                    <td valign="top" style="height: auto" id="tdtreeview" runat="server" visible="false">
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
                            <ajax:UpdateProgress ID="updateProgress" AssociatedUpdatePanelID="UpdatePanel1" runat="server"
                                DisplayAfter="1">
                                <ProgressTemplate>
                                    <asp:Panel ID="pnlFirst" CssClass="overlay" runat="server">
                                        <asp:Panel ID="pnlSecond" CssClass="loader" runat="server">
                                            <img src="Images/Loading.gif" alt="update is in progress" />
                                            <input id="btnCacel" onclick="CancelAsyncPostBack()" type="button" value="Cancel" /></asp:Panel>
                                    </asp:Panel>
                                </ProgressTemplate>
                            </ajax:UpdateProgress>
                        </div>
                    </td>
                    <td style="vertical-align: top; height: auto">
                    </td>
                </tr>
                </table>
                <table style="border-bottom: black 1px solid; position: absolute; border-left: black 1px solid;
                    width: 400px; border-collapse: collapse; border-top: black 1px solid; top: 0px;
                    border-right: black 1px solid; left: 400px; background-color: #ffffff" id="tblDisplay"
                    border="0" cellspacing="0" cellpadding="3">
                    <tbody>
                        <tr>
                            <td style="background-color: #d56511; color: white" colspan="3">
                                <strong>
                                    <label id="lblID">
                                    </label>
                                    <label id="lblName">
                                    </label>
                                </strong>
                            </td>
                            <td colspan="2" onclick="javascript:return tblDisplay.style.visibility='hidden';"
                                style="width: 20px; color: white; background-color: #d56511;">
                                <b style="cursor: hand">X</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px">
                                <strong>Designation</strong>
                            </td>
                            <td style="width: 1px">
                                <strong>:</strong>
                            </td>
                            <td style="width: 60%">
                                <label id="lblDesignation">
                                </label>
                            </td>
                            <td style="width: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px">
                                <strong>MailID</strong>
                            </td>
                            <td style="width: 1px">
                                <strong>:</strong>
                            </td>
                            <td style="width: 60%">
                                <label id="lblMailID">
                                </label>
                            </td>
                            <td style="width: 20px">
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
                    </tbody>
                </table>
                <asp:HiddenField ID="hdn" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
