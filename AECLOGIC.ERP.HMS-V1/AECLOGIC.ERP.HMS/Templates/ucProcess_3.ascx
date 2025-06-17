<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ucProcess_3.ascx.cs" Inherits="AECLOGIC.ERP.COMMON.ucProcess_3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<style type="text/css">
   
   
     .Submit {
        text-align: center;
        color: Red;
        padding: 0 18px;
        height: 29px;
        font-size: 12px;
        font-weight: bold;
        color: #527881;
        text-shadow: 0 1px #e3f1f1;
        background: #cde5ef;
        border: 1px solid;
        border-color: #b4ccce #b3c0c8 #9eb9c2;
        border-radius: 16px;
        outline: 0;
        -webkit-box-sizing: content-box;
        -moz-box-sizing: content-box;
        box-sizing: content-box;
    }

    .droplist {
        padding-left: 0px;
        margin-left: 0px;
        font-family: Tahoma, Arial, Sans-Serif;
        font-size: 1.00em;
        color: #666666;
    }

    .HelpTable {
        text-shadow: 0 1px #e3f1f1;
        border: 1px solid #ffaa00;
        -webkit-box-sizing: content-box;
        -moz-box-sizing: content-box;
        box-sizing: content-box;
        background-color: transparent;
    }

    .up {
        vertical-align: top;
    }

    .down {
        vertical-align: top;
    }
</style>
 <style type="text/css">
        #crumbs {
            width:100%;
            text-align: center;
            margin: 0;
            padding: 0;
            border: 0;
        }

            #crumbs ul {
                list-style: none;
                display: inline-table;
     margin: 14px 0 0;
            }

                #crumbs ul li {
                    display: inline;
                }

                    #crumbs ul li a {
                        display: block;
                        float: left;                     
                        background: #3498db;
                        text-align: center;
                        padding: 13px 10px 10px 19px;
                        position: relative;
                        margin: 0 3px 1px 0;
                        font-size: 10px;
                        font-weight: normal;
                        text-decoration: none;
                        color: #fff;
                    }

                        #crumbs ul li a:after {
                            content: "";
                            border-top: 20px solid transparent;
                            border-bottom: 20px solid transparent;
                            border-left: 20px solid #3498db;
                            position: absolute;
                            right: -19px;
                            top: 0;
                            z-index: 1;
                        }

                        #crumbs ul li a:before {
                            content: "";
                            border-top: 20px solid transparent;
                            border-bottom: 20px solid transparent;
                            border-left: 20px solid #d4f2ff;
                            position: absolute;
                            left: 0;
                            top: 0;
                        }
                        .content li
                        {
                            margin-left: 0px !important;
                        }

                    #crumbs ul li:first-child a {
                        border-top-left-radius: 10px;
                        border-bottom-left-radius: 10px;
                    }

                        #crumbs ul li:first-child a:before {
                            display: none;
                        }

                    #crumbs ul li:last-child a {
                        padding-right: 10px;
                        border-top-right-radius: 10px;
                        border-bottom-right-radius: 10px;
                    }

                        #crumbs ul li:last-child a:after {
                            display: none;
                        }

                    #crumbs ul li a:hover {
                        background: #fa5ba5;
                    }

                        #crumbs ul li a:hover:after {
                            border-left-color: #fa5ba5;
                        }
    
                      
                           
    
                    .wfactive{
                        background: #ffff00 !important;
                        border-left-color: #ffff00 !important;
                        color: Black  !important;
                    }
                       
                .wfactive::after {
                           
                            border-top: 20px solid transparent;
                            border-bottom: 20px solid transparent;
                            border-left: 20px solid #ffff00 !important;
                             
                            
                        }

                        .wfactive::before {
                           
                            border-top: 20px solid transparent;
                            border-bottom: 20px solid transparent;
                            border-left: 20px solid #ffff00 !important;
                           
                        }

                          .wfactiveonclicl{
                        background: #D3AC8B !important;
                        border-left-color: #D3AC8B !important;
                        color: Black  !important;
                    }
                       
                .wfactiveonclicl::after {
                           
                            border-top: 20px solid transparent;
                            border-bottom: 20px solid transparent;
                            border-left: 20px solid #D3AC8B !important;
                             
                            
                        }

                        .wfactiveonclicl::before {
                           
                            border-top: 20px solid transparent;
                            border-bottom: 20px solid transparent;
                            border-left: 20px solid #d4f2ff !important;
                           
                        }
    
    </style>
<script type="text/javascript">
    var Returnval = false;
    var GrpReval = false;
    var PageID = 0;
    var file, n;
    var session = '<%=Session["EncUserID"].ToString() %>';
    function URL() {
        file = window.location.pathname;
        n = file.lastIndexOf('/');
        if (n >= 0) {
            file = file.substring(n + 1);
        }
        return file;
    }
    function CloseHelpPopUp() {
        document.getElementById('<%=imgMarkFav.ClientID %>').alt = "Mark Favourite";
        $find('<%=mpeHelpView.ClientID%>').hide();
        $find('<%=MpepnlselfHelp.ClientID%>').hide();
        return false;
    }
    function pageLoad(sender, args) {
        if (!args.get_isPartialLoad()) {
            $addHandler(document, "keydown", onKeyDown);
        }
    }

    function onKeyDown(e) {
        if (e && e.keyCode == Sys.UI.Key.esc) {
            // if Escape key is Pressed Hide
            document.getElementById('<%=imgMarkFav.ClientID %>').alt = "Mark Favourite";
            $find('<%=mpeHelpView.ClientID%>').hide();
            $find('<%=MpepnlselfHelp.ClientID%>').hide();
            return false;
        }
    }

    function ValidateHelpMenu() {
        if (document.getElementById('<%=ddlGroup.ClientID %>').options[document.getElementById('<%=ddlGroup.ClientID %>').selectedIndex].text == "--Select--") {
            document.getElementById('<%=ddlGroup.ClientID%>').focus();
            document.getElementById('<%=ddlGroup.ClientID%>').style.border = "thin solid #FF0000";
            return false;
        }
        if (Returnval == false) {
            document.getElementById('<%=txtFavName.ClientID%>').focus();
            document.getElementById('<%=txtFavName.ClientID%>').style.border = "thin solid #FF0000";
            document.getElementById('<%=txtFavName.ClientID%>').alt = "Enter Favourite Name";
            return false;
        }
        else if (Returnval == true) {
            <%-- var ModuleID = '<%=Application["EncModuleID"].ToString() %>';--%>
            var ModuleID = document.getElementById('<%=hdnEncModuleID.ClientID %>').value;
            URL();
            var FavRes = Help.SaveasFavPage(session, file, ModuleID, document.getElementById('<%=txtFavName.ClientID %>').value, document.getElementById('<%=ddlGroup.ClientID %>').options[document.getElementById('<%=ddlGroup.ClientID %>').selectedIndex].value);
            if (FavRes.value != "0") {
                document.getElementById('<%=CurPageVal.ClientID %>').value = FavRes.value;
                $find('<%=mpeHelpView.ClientID%>').hide();
                $get('<%=imgMarkFav.ClientID %>').src = "./IMAGES/UnMarkFavIcon.ico";
            }
        }
}
function chkNameVal(e) {
    if (e.value != "") {
        document.getElementById('<%=txtFavName.ClientID%>').style.border = "thin solid #FFFFFF";
        Returnval = true;
    }
    else {
        Returnval = false;
    }
}
function chkGrpVal(e) {
    if (e.value != "") {
        GrpReval = true;
    }
    else {
        GrpReval = false;
    }
}
function ShowModalPop(e) {
    if (e != null && e.src == '<%=imgMarkFav.ClientID %>') {
        if (document.getElementById('<%=imgMarkFav.ClientID %>').alt == "Mark Favourite") {
            $find('<%=mpeHelpView.ClientID%>').show();
            document.getElementById('<%=ChkHiddenVal.ClientID %>').value = 1;
            document.getElementById('<%=imgMarkFav.ClientID %>').alt = "Un Mark Favourite";
        }
        else if (document.getElementById('<%=imgMarkFav.ClientID %>').alt == "Un Mark Favourite") {
            document.getElementById('<%=imgMarkFav.ClientID %>').alt = "Mark Favourite";
            var Confirm = confirm('Are you sure .! you want to remove from favourites');
            if (Confirm) {
                Help.DelRecord(document.getElementById('<%=CurPageVal.ClientID %>').value);
                $get('<%=imgMarkFav.ClientID %>').src = "./IMAGES/MarkFavIcon.png";
            }
        }
    return false;
}
}
function AddNewFavGrp() {
    document.getElementById('<%=txtNewGrp.ClientID%>').style.border = "thin solid #bbbbbb";
    if (document.getElementById('<%=ChkHiddenVal.ClientID %>').value == 1) {
        if (document.getElementById('<%=lnkAddNewGrp.ClientID %>').value == "Add New") {
            document.getElementById('<%=txtNewGrp.ClientID%>').style.display = "inherit";
            document.getElementById('<%=ChkHiddenVal.ClientID %>').value = 0;
            document.getElementById('<%=lnkAddNewGrp.ClientID %>').value = "Save";
        }
    }
    else if (document.getElementById('<%=ChkHiddenVal.ClientID %>').value == 0) {
        if (GrpReval) {
            var Result = Help.GenInsertNewGrp(session, document.getElementById('<%=txtNewGrp.ClientID%>').value);
            if (Result.value != "0") {
                AddNewFavGroupDropDown(document.getElementById('<%=txtNewGrp.ClientID%>'), Result.value);
                }
                document.getElementById('<%=txtNewGrp.ClientID%>').value = ""
            document.getElementById('<%=ChkHiddenVal.ClientID %>').value = 1;
            document.getElementById('<%=lnkAddNewGrp.ClientID %>').value = "Add New";
            document.getElementById('<%=txtNewGrp.ClientID%>').style.display = "none";
        }
        else {
            document.getElementById('<%=txtNewGrp.ClientID%>').focus();
            document.getElementById('<%=txtNewGrp.ClientID%>').style.border = "thin solid #FF0000";
            document.getElementById('<%=txtNewGrp.ClientID%>').alt = "Enter Group Name";
        }
    }
    return false;
}
function AddNewFavGroupDropDown(FavGrpName, Value) {
    var optn = document.createElement("OPTION");
    optn.text = FavGrpName.value;
    optn.value = Value;
    document.getElementById('<%=ddlGroup.ClientID %>').options.add(optn);
}
var ChkEditReval = true;
function EditGroupName(lnkEdit, txtEdit, Id, GrpNamEdit) {
    try {

        document.getElementById(txtEdit).style.border = "thin solid #bbbbbb";
        if (document.getElementById(lnkEdit).title == "Edit Group") {
            if (ChkEditReval) {
                try {
                    document.getElementById(GrpNamEdit).style.display = "none";
                    document.getElementById(GrpNamEdit).style.cssFloat = "left";
                }
                catch (e)
                { alert(e); }
                document.getElementById(txtEdit).value = document.getElementById(GrpNamEdit).innerText;
                document.getElementById(txtEdit).style.display = "inherit";
                document.getElementById(lnkEdit).src = "./IMAGES/Update.png";
                document.getElementById(lnkEdit).title = "update Group";
                document.getElementById(txtEdit).focus();
                ChkEditReval = false;
                return false;
            }
            else {
                document.getElementById(txtEdit).style.border = "thin solid #FF0000";
                document.getElementById(txtEdit).focus();
                document.getElementById(txtEdit).alt = "Enter Group Name";
                return false;
            }
        }
        else {
            if (document.getElementById(txtEdit).value != "" && ChkEditReval == false) {
                try {
                    document.getElementById(GrpNamEdit).style.display = "inherit";
                    document.getElementById(GrpNamEdit).style.cssFloat = "left";
                }
                catch (e) {
                    alert(e);
                }
                var Res = Help.UpdateGroupText(Id, session, document.getElementById(txtEdit).value, 1);
                document.getElementById(GrpNamEdit).innerText = document.getElementById(txtEdit).value;
                document.getElementById(txtEdit).style.display = "none";
                document.getElementById(lnkEdit).src = "./IMAGES/edit.jpg";
                document.getElementById(lnkEdit).title = "Edit Group";
                FindIsdropdownValue(Id, document.getElementById(txtEdit).value);
                ChkEditReval = true;
                return false;
            }
            else {
                document.getElementById(txtEdit).style.border = "thin solid #ff0000";
                document.getElementById(txtEdit).focus();
                document.getElementById(txtEdit).alt = "Enter Group Name";
                return false;
            }
            return false;
        }
    } catch (e) {
        alert(e);
        return false;
    }
    return false;
}
function FindIsdropdownValue(value, DropdownText) {
    var sel = document.getElementById('<%=ddlGroup.ClientID %>');
    sel.options[value].text = DropdownText;
}
function DeleteFavGroup(Id, TrRow) {
    var Res = Help.UpdateGroupText(Id, session, "hari", 3);
    $get(TrRow).style.display = "none";
    return false;
}
function UpdateGroupOrder(ctrl, GroupID, direction) {
    Help.UpdateGroupID(session, GroupID, direction);
    var CurID = eval(ctrl.replace("ctl00_ucHelp_rpFAVGrps_ctl", "").replace("_rptRow", ""));
    var NextID = CurID + eval(direction);
    var NextUID = "ctl00_ucHelp_rpFAVGrps_ctl";
    if (NextID.toString().length == 1) {
        NextUID = NextUID + 0 + NextID.toString() + "_rptRow";
    } else {
        NextUID = NextUID + NextID.toString() + "_rptRow";
    }
    var html = document.getElementById(ctrl).innerHTML;
    document.getElementById(ctrl).innerHTML = document.getElementById(NextUID).innerHTML;
    document.getElementById(NextUID).innerHTML = html;
    return false;
}
function ChangeForeColor() {
    document.getElementById('<%=ddlGroup.ClientID%>').style.border = "thin solid #FFFFFF";
}
function ChangeGrpColor() {
    document.getElementById('<%=txtNewGrp.ClientID%>').style.border = "thin solid #FFFFFF";
    }
    function ShowSelfHelp() {
        $find('<%=MpepnlselfHelp.ClientID%>').show();
    }

    function checklist(ctrl) {
        ctrl.style.background = '#D3AC8B';
        ctrl.className += "wfactiveonclicl";
        //ctrl.style.setAttribute("class", "wfactiveonclicl");
        //ctrl.NextID.style.background = '#D3AC8B';
        //alert(ctrl.title);
       // var list = document.getElementById("divPath").getElementsByTagName('li');
        //alert(list.length);
        //var firval = false;
        //for (i = 0; i < list.length; i++) {
            //list[i].style.background = '#D3AC8B';
            ////alert(list[i].innerHTML);
            ////alert(list[i].children[0].innerHTML);
            //alert(list[i].getElementsByTagName('a')[0].title);
            //if (firval == true)
            //{
            //    alert("call next")
            //    list[i].style.background = '#D3AC8B';//wfactiveonclicl //.attr("class","current");
            //    firval = false;
            //    break;
            //}
           // if (list[i].getElementsByTagName('a')[0].title == ctrl.title) {
          //      alert("raised")
            //    list[i].setAttribute("class", "wfactiveonclicl");
            //    alert("Applied")
                //list[i].style.background = '#D3AC8B';
                //firval = true;
            //}
           
       // }
       
    }
</script>


             <asp:GridView ID="gvEV" OnRowCommand="gvEV_RowCommand" runat="server" Font-Size="10px"
                     AutoGenerateColumns="false" Width="100%" 
                     CssClass="gridview" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="EmptyRowData" AlternatingRowStyle-BorderColor="GhostWhite">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" 
                                                     CommandArgument='<%#Eval("psID")%>'
                                                     CommandName="Edt"><%#Eval("Proces")%></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMenuID" runat="server" Text='<%#Eval("MID")%>' Visible="false"> 
                                                    </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                    </Columns>
                                </asp:GridView>
            <asp:Label runat="server" ID="lblError"></asp:Label>
<%--<asp:HiddenField ID="hdfMenuID" Value="0" runat="server" />--%>
                      
<%--<asp:UpdatePanel ID="up1" runat="server" UpdateMode="Always">
    <ContentTemplate>--%>

        <div id="tbHelp" runat="server" class="menu">
            <li style="display:none">
                <ul>

                    <li style="visibility: hidden;">
                        <i id="qlID" runat="server" visible="false">Quick Launch ID:&nbsp;<asp:Label ID="lblQLID"
                            Style="color: Red; font-weight: bolder" runat="server"></asp:Label>&nbsp;&nbsp;
                        <input id="imgMarkFav" type="image" runat="server" style="cursor: pointer; vertical-align: sub;"
                            onclick="javascript: return ShowModalPop();" /></i>
                        <input id="ChkHiddenVal" runat="server" value="0" type="hidden" />
                        <input id="CurPageVal" runat="server" value="0" type="hidden" />
                    </li>
                    <li><a id="btnselfHelp" runat="server" onclick="ShowSelfHelp();" accesskey="h" style="cursor: pointer">Help</a></li>
                    <li>
                        <a id="lnkWho" runat="server" onclick="javascript::" style="cursor: pointer; text-decoration: none">Who</a></li>
                    <li>
                        <a id="lnkWhat" runat="server" onclick="javascript::" style="cursor: pointer; text-decoration: none">What</a></li>
                    <li>
                        <a id="lnkWhen" runat="server" onclick="javascript::" style="cursor: pointer; text-decoration: none">When</a></li>
                    <li>
                        <a id="lnkWhere" runat="server" onclick="javascript::" style="cursor: pointer; text-decoration: none">Where</a></li>
                    <li><a id="lnkWhy" runat="server" onclick="javascript::" style="cursor: pointer; text-decoration: none">Why</a>
                    </li>
                    <li>
                        <a id="lnkHow" runat="server" onclick="javascript::" style="cursor: pointer; text-decoration: none">How</a>
                    </li>
                    <li>
                        <a id="lnkTutorials" runat="server" href="#">Tutorials </a></li>
                    <li>
                        <asp:HiddenField ClientIDMode="Static" ID="hdntsWho" runat="server" />
                        <asp:HiddenField ClientIDMode="Static" ID="hdntsWhat" runat="server" />
                        <asp:HiddenField ClientIDMode="Static" ID="hdntsWhen" runat="server" />
                        <asp:HiddenField ClientIDMode="Static" ID="hdntsWhy" runat="server" />
                        <asp:HiddenField ClientIDMode="Static" ID="hdntsWhere" runat="server" />
                        <asp:HiddenField ClientIDMode="Static" ID="hdntsHow" runat="server" />
                        <asp:HiddenField ClientIDMode="Static" ID="hdnEncModuleID" runat="server" />

                    </li>





                </ul>
            </li>
        </div>
        <asp:Button ID="Button1" runat="server" Style="display: none" meta:resourcekey="btnShowModalPopupResource1" />
        <cc1:ModalPopupExtender ID="MpepnlselfHelp" runat="server" TargetControlID="Button1"
            PopupControlID="pnlselfHelp" BackgroundCssClass="modalBackground" DropShadow="True"
            DynamicServicePath="" Enabled="True">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlselfHelp" runat="server" Style="display: none; background-color: #999; margin: 70px 50px 50px 50px;">
            <asp:ImageButton ID="ImageButton1" OnClick="btnClose_Click" OnClientClick="javascript:return CloseHelpPopUp();"
                ImageUrl="~/IMAGES/img-close-off.gif" ImageAlign="Right" runat="server" meta:resourcekey="btnCloseResource1" />
            <span style="color: #fff;"><b>Help:</b></span><br />
            <div id="tdselfContent" runat="server" style="background-color: #fff; padding: 20px 20px 10px 10px;">
            </div>
        </asp:Panel>
        <asp:Button ID="btnShowModalPopup" runat="server" Style="display: none" meta:resourcekey="btnShowModalPopupResource1" />
        <cc1:ModalPopupExtender ID="mpeHelpView" runat="server" TargetControlID="btnShowModalPopup"
            PopupControlID="pnlView" BackgroundCssClass="modalBackground" DropShadow="True"
            DynamicServicePath="" Enabled="True">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlView" runat="server" Style="display: none; background-color: #fff;">
            <asp:ImageButton ID="btnimgClose" OnClick="btnClose_Click" OnClientClick="javascript:return CloseHelpPopUp();"
                ImageUrl="~/IMAGES/img-close-off.gif" ImageAlign="Right" runat="server" meta:resourcekey="btnCloseResource1" />
            <br />
            <div>
               <%-- <asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                        <table style="width: 100%;display:none">
                            <tr>
                                <td>
                                    <strong style="color: #db511b">For Quickview of Pages or Reports you can make page as
                                        Favourite, Which will be available throughout the ERP</strong>
                                </td>
                            </tr>
                            <tr>
                                <td>Favourite Group&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlGroup" onchange="javascript:return ChangeForeColor();" CssClass="droplist"
                                        runat="server" AutoPostBack="false">
                                    </asp:DropDownList>
                                    <input id="txtNewGrp" onchange="javascript:return ChangeGrpColor();" onblur="javascript:return chkGrpVal(this);"
                                        runat="server" type="text" style="display: none" />
                                    <input id="lnkAddNewGrp" runat="server" type="button" value="Add New" title="Add New"
                                        style="color: Blue; font-size: 10pt; display: inherit;" onclick="javascript: return AddNewFavGrp();" />
                                </td>
                            </tr>
                            <tr>
                                <td>Favourite Page Name
                                    <asp:TextBox ID="txtFavName" CssClass="droplist" onblur="javascript:return chkNameVal(this);"
                                        AutoPostBack="false" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 100px">
                                    <input id="bntSaveFavPage" runat="server" title="Save" type="image" src="~/IMAGES/add.ico"
                                        onclick="javascript: return ValidateHelpMenu();" />
                                    <input id="btnClosePopUp" runat="server" type="image" title="Close" style="width: 32px; height: 32px"
                                        src="~/IMAGES/btnClosePop.ico" onclick="javascript: return CloseHelpPopUp();" />
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 100%;">
                                    <asp:Panel ID="pnlg" runat="server" ScrollBars="Both" Width="100%">

                                        
                                   <table style="width: 100%; display:none">
                                            <tr>
                                                <td style="table-layout: inherit; border-collapse: inherit; border-spacing: inherit; empty-cells: hide; position: inherit; z-index: inherit; border: 1px solid #800080; background-color: #E6E6E6; vertical-align: text-top; text-align: center; text-indent: inherit; white-space: nowrap; word-spacing: normal; letter-spacing: normal; line-height: normal; font-family: Cambria; font-size: medium; font-weight: normal; font-style: normal; font-variant: normal; text-transform: none; width: 100%;">
                                                    <asp:Repeater ID="rpFAVGrps" runat="server" OnItemDataBound="rpFAVGrps_ItemDataBound" Visible="false">
                                                        <HeaderTemplate>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table id="rptRow" runat="server" style="border: 1px solid #eee; width: 100%;">
                                                                <tr runat="server">
                                                                    <td>
                                                                        <asp:Label ID="lblGp" Style="float: left; font-weight: bold" runat="server" Text='<%#Bind("Group") %>'></asp:Label>
                                                                        <input id="txtGrpEditText" style="display: none; float: left;" runat="server" type="text" />
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                        <input id="imgup" type="image" style="width: 16px; position: static; height: 16px"
                                                                            title="Move Group up" src="~/IMAGES/UpIcon.ico" runat="server" text="Edit" />
                                                                        <input id="imgdown" type="image" title="Move group down" style="position: static; width: 16px; height: 16px"
                                                                            src="~/IMAGES/down.ico" runat="server" text="Edit" />
                                                                        <input id="hdnEdit" runat="server" type="hidden" value='<%#Bind("FavGroupID") %>' />
                                                                        <input id="lnkEdit" type="image" title="Edit Group" style="position: static;" src="~/IMAGES/edit.jpg"
                                                                            runat="server" text="Edit" />
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                        <input id="lnkDel" runat="server" style="position: static;" src="~/IMAGES/delete.png"
                                                                            type="image" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                   <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <div class="UpdateProgressCSS">
                <%--<asp:UpdateProgress ID="updateProgress1" AssociatedUpdatePanelID="up2" runat="server"
                    DisplayAfter="1">
                    <ProgressTemplate>
                        <img src="IMAGES/updateProgress.gif" alt="update is in progress" />
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>
            </div>
        </asp:Panel>
   <%-- </ContentTemplate>
</asp:UpdatePanel>--%>
