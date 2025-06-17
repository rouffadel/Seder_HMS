<%@ Page Language="C#"   AutoEventWireup="True" ValidateRequest="false" CodeBehind="EditOfferLetter.aspx.cs" Inherits="AECLOGIC.ERP.HMS.EditOfferLetter" Title="Edit Offer Letter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
<script language="javascript" type="text/javascript">
    window.onload = "doInit";

    // For EmotIcon Menu
    var isViewEmotIconMenu = false;

    function doInit() {
        var elm = null;
        elm = getObj("<%=TextEditor.ClientID %>");
        for (i = 0; i < document.all.length; i++)
            document.all(i).unselectable = "on";
        elm.unselectable = "off";
        elm.focus();
    }

    var isHTMLMode = false;
    var bShow = false;
    var sPersistValue;

    // button over effect
    function button_over(eButton) {
        eButton.style.backgroundColor = "#B5BDD6";
        eButton.style.border = "1";
        eButton.style.borderColor = "Navy Navy Navy Navy";
    }

    // go back to normal
    function button_out(eButton) {
        eButton.style.backgroundColor = "threedface";
        eButton.style.borderColor = "threedface";
    }

    function ValidOffer() {
        if (!chkDropDownList('<%= ddlDesig.ClientID%>', 'Designation'))
            return false;
        if (!chkDropDownList('<%= ddlCategory.ClientID%>', 'Trade'))
            return false;
    }
    // button down effect
    function button_down(eButton) {
        eButton.style.backgroundColor = "#8494B5";
        eButton.style.borderColor = "darkblue darkblue darkblue darkblue";
    }

    // back to normal
    function button_up(eButton) {
        eButton.style.backgroundColor = "#B5BDD6";
        eButton.style.borderColor = "darkblue darkblue darkblue darkblue";
        eButton = null;
    }

    // Resets Style to default after selection
    function EditorOnStyle(select) {
        cmdExec("formatBlock", select[select.selectedIndex].value);
        select.selectedIndex = 0;
    }

    // Resets Font to default after selection
    function EditorOnFont(select) {
        cmdExec("fontname", select[select.selectedIndex].value);
        select.selectedIndex = 0;
    }

    // Resets Size to default after selection
    function EditorOnSize(select) {
        cmdExec("fontsize", select[select.selectedIndex].value);
        select.selectedIndex = 0;
    }

    // execute command and enter the HTML in the RTB
    function cmdExec(cmd, opt) {
        if (isHTMLMode) { alert("Please uncheck 'Edit HTML'"); return; }
        var elm = null;
        elm = getObj("<%=TextEditor.ClientID %>");
        elm.focus();
        elm.document.execCommand(cmd, bShow, opt);
        bShow = false;
    }

    // sets the mode for HTML or Text
    function setMode(bMode) {
        var elm = null;
        elm = getObj("<%=TextEditor.ClientID %>");
        var sTmp;
        isHTMLMode = bMode;
        if (isHTMLMode) {
            sTmp = elm.innerHTML;
            elm.innerText = sTmp;
        }
        else {
            sTmp = elm.innerText;
            elm.innerHTML = sTmp;
        }

        elm.focus();
    }

    // Insert Image
    function insertImage() {
        if (isHTMLMode) { alert("Please uncheck 'Edit HTML'"); return; }
        bShow = true;
        cmdExec("InsertImage");
    }

    // Insert Horizontal Rule
    function insertRuler() {
        if (isHTMLMode) { alert("Please uncheck 'Edit HTML'"); return; }
        cmdExec("InsertHorizontalRule", "");
    }

    // sets everything to vertical mode
    function VerticalMode() {
        var elm = null;
        elm = getObj("<%=TextEditor.ClientID %>");
        if (elm.style.writingMode == 'tb-rl') elm.style.writingMode = 'lr-tb';
        else elm.style.writingMode = 'tb-rl';
    }

    // calls the color object
    function callColorDlg(sColorType) {


        var sColor = dlgHelper.ChooseColorDlg(); sColor = sColor.toString(16);
        if (sColor.length < 6) {
            var sTempString = "000000".substring(0, 6 - sColor.length);
            sColor = sTempString.concat(sColor);
        }
        cmdExec(sColorType, sColor);
    }

    // sets the text in the Div to a textbox which you can pull the 
    // data from to save in the database
    function getHTML() {
        var String = "<%=TextEditor.ClientID %>";
        var elm = null;
        elm = getObj("<%=txtRTB.ClientID %>");
        // The ctl00_ContentPlaceHolder1_TextEditor DIV can not be in a form or the Java error out
        elm.value = String.innerHTML;   //you can't make the text box invisible or the Java will error out


    }

    // Load RTB info from the database into the DIV field on the web.
    // so one can edit the database info in the Rich Text Box.
    function LoadDiv() {
        //			var String = frmRTB.txtRTB.value;
        //			ctl00_ContentPlaceHolder1_TextEditor.innerHTML = String;   // set the innerHTML of the DIV to the text of the textbox


        var elm1 = null;
        elm1 = getObj("<%=TextEditor.ClientID %>");
        var elm = null;
        elm = getObj("<%=txtRTB.ClientID %>");
        var String = elm.value;
        elm1.innerHTML = String;
    }
    function IMG1_onclick() {

    }

    function TextEditor_onclick() {

        var sTmp;
        var elm1 = getObj("<%=TextEditor.ClientID %>");
        sTmp = elm1.innerHTML;
        //''ctl00_cp_TextEditor.innerText=sTmp;	
        var elm = getObj("<%=txtRTB.ClientID %>");
        elm.value = sTmp;

        elm1.focus();
    }
    function TextEditor_edit() {

        var sTmp;
        var elm1 = getObj("<%=TextEditor.ClientID %>");
        sTmp = elm1.innerHTML;
        //''ctl00_cp_TextEditor.innerText=sTmp;	
        var elm = getObj("<%=txtRTB.ClientID %>");
        elm.value = sTmp;
        elm1.focus();
    }

    function getObj(object) {
        var p_elm = object;
        var elm;
        if (typeof (p_elm) == "object")
        { elm = p_elm; } else { elm = document.getElementById(p_elm); }
        return elm;
    }
    function ChangeAppDate() {
        getObj("<%=lblDate.ClientID %>").innerText = getObj("<%=txtAppDate.ClientID %>").value;
        return;

    }
    </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td class="pageheaderprintcenter">
               
                Offer Letter</td>
        </tr>
        <tr><td><asp:Button ID="Button3" runat="server" CssClass="savebutton" 
                                onclick="Button1_Click" Text="Back" /></td></tr>
        <tr>
            <td >
            
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr><td><b>
                            <asp:Label ID="lblDate" runat="server"></asp:Label></b></td></tr>
                    <tr>
                        <td>
                        <b>
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                            
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblAddress" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 20px">
                                            <asp:Label ID="lblCity" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblState" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCountry" runat="server"></asp:Label>&nbsp;<asp:Label ID="lblPin" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPhone" runat="server"></asp:Label></td>
                                    </tr>
                                </table></b></td>
                        <td style="text-align: right; vertical-align: top;"></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 1439px;">
                            </b>
                        </td>
                       
                    </tr>
                    <tr>
                        <td colspan="2" style="vertical-align: top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                               <tr>
                                    <td style="height: 20px; width: 190px;">
                                        Designation<span style="color: #ff0000">*</span> 
                                    </td>
                                    <td style="height: 20px; text-align: left">
                                        <asp:DropDownList ID="ddlDesig" runat="server" CssClass="droplist">
                                        </asp:DropDownList>
                                        <%--<asp:TextBox ID="txtDesig" runat="server"></asp:TextBox>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTrade" runat="server" Text="Trades(Expertise)"></asp:Label><span
                                            style="color: #ff0000">*</span> 
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="droplist" TabIndex="3"
                                            AccessKey="3" ToolTip="[Alt+3]">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 190px">
                                        Salary
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtSalary" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 190px">
                                        Date of Joining</td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtDOJ" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender
                                TargetControlID="txtDOJ" PopupButtonID="txtDOJ" ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                                </tr>
                                <tr>
                                    <td>
                                     Job Type
                                    
                                    </td>
                                    <td>
                                    <asp:DropDownList ID="ddljobtype" CssClass="droplist"  runat="server" >
                                       <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                       <asp:ListItem Text="Perminant" Value="1"></asp:ListItem>
                                       <asp:ListItem Text="Temporary" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                    
                                    </td>
                                
                                </tr>
                                
                            </table><br />Help: [designation] Designation; [salary] : Salary; [doj] : Date Of Joining; [company]:
                            Company;&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table id="TABLEmails1" runat="server" align="center" border="1"  style="background-color: white"
                                width="100%">
                                <tr bgcolor="buttonface" class="tblToolbar">
                                    <td align="left">
                                        <table id="TopToolBar" align="left" cellpadding="0" cellspacing="3" class="raiseme">
                                            <tr>
                                                
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('bold')" onmousedown="button_down(this);" onmouseout="button_out(this);"
                                                        onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Bold" hspace="1" src="Images/Bold.GIF" vspace="0" /></div>
                                                </td>
                                                <td style="width: 26px">
                                                    <div class="cbtn" onclick="cmdExec('italic')" onmousedown="button_down(this);" onmouseout="button_out(this);"
                                                        onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Italic" hspace="1" src="Images/Italic.GIF" vspace="0" /></div>
                                                </td>
                                                <td style="width: 26px">
                                                    <div class="cbtn" onclick="cmdExec('underline')" onmousedown="button_down(this);"
                                                        onmouseout="button_out(this);" onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Underline" hspace="1" src="Images/under.GIF" vspace="0" /></div>
                                                </td>
                                                <td>
                                                    <img align="middle" src="Images/spacer.gif" /></td>
                                                <td>
                                                    <select class="Selects" onchange="EditorOnStyle(this);">
                                                        <option selected="selected">Style</option>
                                                        <option value="Normal">Normal</option>
                                                        <option value="Formatted">Formatted</option>
                                                        <option value="Address">Address</option>
                                                        <option value="Heading 1">Heading 1</option>
                                                        <option value="Heading 2">Heading 2</option>
                                                        <option value="Heading 3">Heading 3</option>
                                                        <option value="Heading 4">Heading 4</option>
                                                        <option value="Heading 5">Heading 5</option>
                                                        <option value="Heading 6">Heading 6</option>
                                                        <option value="Numbered List">Numbered List</option>
                                                        <option value="Bulleted List">Bulleted List</option>
                                                        <option value="Directory List">Directory List</option>
                                                        <option value="Menu List">Menu List</option>
                                                        <option value="Definition Term">Definition Term</option>
                                                        <option value="Definition">Definition</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <select class="Selects" onchange="EditorOnFont(this);">
                                                        <option selected="selected">Font</option>
                                                        <option value="Arial">Arial</option>
                                                        <option value="Arial Black">Arial Black</option>
                                                        <option value="Arial Narrow">Arial Narrow</option>
                                                        <option value="Comic Sans MS">Comic Sans MS</option>
                                                        <option value="Courier New">Courier New</option>
                                                        <option value="System">System</option>
                                                        <option value="Tahoma">Tahoma</option>
                                                        <option value="Times New Roman">Times New Roman</option>
                                                        <option value="Verdana">Verdana</option>
                                                        <option value="Wingdings">Wingdings</option>
                                                    </select>
                                                </td>
                                                <td>
                                                    <select class="Selects" onchange="EditorOnSize(this);">
                                                        <option selected="selected">Size</option>
                                                        <option value="1">1, 10px, 7pt</option>
                                                        <option value="2">2, 12px, 10pt</option>
                                                        <option value="3">3, 16px, 12pt</option>
                                                        <option value="4">4, 18px, 14pt</option>
                                                        <option value="5">5, 24px, 18pt</option>
                                                        <option value="6">6, 32px, 24pt</option>
                                                        <option value="7">7, 48px, 36pt</option>
                                                    </select>
                                                </td>
                                                <td style="width: 3px">
                                                    <img align="middle" src="Images/spacer.gif" /></td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('strikethrough')" onmousedown="button_down(this);"
                                                        onmouseout="button_out(this);" onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Strike Through" hspace="1" src="Images/Strikethrough.gif"
                                                            vspace="0" /></div>
                                                </td>
                                                <td>
                                                    <img align="middle" src="Images/spacer.gif" /></td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('superscript')" onmousedown="button_down(this);"
                                                        onmouseout="button_out(this);" onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Superscript" hspace="1" src="Images/superscript.gif"
                                                            vspace="0" /></div>
                                                </td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('subscript')" onmousedown="button_down(this);"
                                                        onmouseout="button_out(this);" onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Subscript" hspace="1" src="Images/subscript.gif" vspace="0" /></div>
                                                </td>
                                                <td>
                                                    <img align="middle" src="Images/spacer.gif" /></td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('undo')" onmousedown="button_down(this);" onmouseout="button_out(this);"
                                                        onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Undo" hspace="1" src="Images/Undo.gif" vspace="0" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('redo')" onmousedown="button_down(this);" onmouseout="button_out(this);"
                                                        onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Redo" hspace="1" src="Images/Redo.gif" vspace="0" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <img align="middle" src="Images/spacer.gif" /></td>
                                                <td valign="middle">
                                                    <input id="checkbox2" name="checkbox2" onclick="setMode(this.checked)" type="checkbox" /></td>
                                                <td nowrap="nowrap" style="font: 8pt verdana,arial,sans-serif" valign="middle">
                                                    Edit HTML
                                                </td>
                                                <td style="width: 5px">
                                                    <img align="middle" src="Images/leader.gif" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr bgcolor="buttonface" class="tblToolbar">
                                    <td align="left">
                                        <table id="BottomToolBar" cellpadding="0" cellspacing="3" class="raiseme" width="700">
                                            <tr>
                                                <td width="5">
                                                    <img align="middle" src="Images/leader.gif" /></td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('cut')" onmousedown="button_down(this);" onmouseout="button_out(this);"
                                                        onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Cut" hspace="1" src="Images/Cut.GIF" vspace="0" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('copy')" onmousedown="button_down(this);" onmouseout="button_out(this);"
                                                        onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Copy" hspace="1" src="Images/Copy.GIF" vspace="0" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('paste')" onmousedown="button_down(this);" onmouseout="button_out(this);"
                                                        onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Paste" hspace="1" src="Images/Paste.GIF" vspace="0" />
                                                    </div>
                                                </td>
                                                <td style="width: 4px">
                                                    <img align="middle" src="Images/spacer.gif" /></td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('justifyleft')" onmousedown="button_down(this);"
                                                        onmouseout="button_out(this);" onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Left Align" hspace="1" src="Images/left.GIF" vspace="0" /></div>
                                                </td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('justifycenter')" onmousedown="button_down(this);"
                                                        onmouseout="button_out(this);" onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Center" hspace="1" src="Images/Center.GIF" vspace="0" /></div>
                                                </td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('justifyright')" onmousedown="button_down(this);"
                                                        onmouseout="button_out(this);" onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Right Align" hspace="1" src="Images/right.GIF" vspace="0" /></div>
                                                </td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('justifyfull')" onmousedown="button_down(this);"
                                                        onmouseout="button_out(this);" onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Justify" hspace="1" src="Images/justify.gif" vspace="0" /></div>
                                                </td>
                                                <td>
                                                    <div class="cbtn" onclick="VerticalMode()" onmousedown="button_down(this);" onmouseout="button_out(this);"
                                                        onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Change Text Direction" hspace="1" src="Images/Vertical.gif"
                                                            vspace="0" /></div>
                                                </td>
                                                <td>
                                                    <img align="middle" src="Images/spacer.gif" /></td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('insertorderedlist')" onmousedown="button_down(this);"
                                                        onmouseout="button_out(this);" onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Ordered List" hspace="2" src="Images/numlist.GIF"
                                                            vspace="0" /></div>
                                                </td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('insertunorderedlist')" onmousedown="button_down(this);"
                                                        onmouseout="button_out(this);" onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Unordered List" hspace="2" src="Images/bullist.GIF"
                                                            vspace="0" /></div>
                                                </td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('outdent')" onmousedown="button_down(this);" onmouseout="button_out(this);"
                                                        onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Decrease Indent" hspace="2" src="Images/DeIndent.GIF"
                                                            vspace="0" /></div>
                                                </td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('indent')" onmousedown="button_down(this);" onmouseout="button_out(this);"
                                                        onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Increase Indent" hspace="2" src="Images/inindent.GIF"
                                                            vspace="0" /></div>
                                                </td>
                                                <td>
                                                    <img align="middle" src="Images/spacer.gif" /></td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('createLink')" onmousedown="button_down(this);"
                                                        onmouseout="button_out(this);" onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Insert Link" hspace="2" src="Images/wlink.gif" vspace="0" /></div>
                                                </td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('unlink')" onmousedown="button_down(this);" onmouseout="button_out(this);"
                                                        onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Remove Link" hspace="2" src="Images/unlink.gif" vspace="0" /></div>
                                                </td>
                                                <td>
                                                    <div class="cbtn" onclick="cmdExec('inserthorizontalrule')" onmousedown="button_down(this);"
                                                        onmouseout="button_out(this);" onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img align="middle" alt="Horizontal Rule" hspace="2" src="Images/HR.gif" vspace="0" /></div>
                                                </td>
                                                <td>
                                                    <img align="middle" src="Images/spacer.gif" /></td>
                                                <td>
                                                    <div class="cbtn" onclick="insertImage()" onmousedown="button_down(this);" onmouseout="button_out(this);"
                                                        onmouseover="button_over(this);" onmouseup="button_up(this);">
                                                        <img id="IMG1" align="middle" alt="Insert Image" hspace="2" language="javascript"
                                                            onclick="return IMG1_onclick()" src="Images/image.GIF" vspace="0" /></div>
                                                </td>
                                                <td width="5">
                                                    <img align="middle" src="Images/leader.gif" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="TextEditor" contenteditable="true" style="overflow: auto; width: 100%; height: auto;"
                                             language="javascript" onclick="return TextEditor_onclick()"
                                            onkeyup="return TextEditor_edit()" runat="server">
                                            &nbsp;&nbsp;<br />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 0px">
                                    Note: Press <strong>Shift + Enter</strong> to break line; Press <strong>Enter</strong>
                                        key to Next Paragraph;<object classid="clsid:5220cb21-c88d-11cf-b347-00aa00a28331"></object><object id="dlgHelper" classid="clsid:3050f819-98b5-11cf-bb82-00aa00bdce0b" height="0px"
                                            width="0px"></object><object id="cDialog" classid="CLSID:F9043C85-F6F2-101A-A3C9-08002B2F49FB" codebase="http://activex.microsoft.com/controls/vb5/comdlg32.cab"
                                            height="0px" width="0px"><param name="_ExtentX" value="847"><param name="_ExtentY" value="847"><param name="_Version" value="393216"><param name="CancelError" value="0"><param name="Color" value="0"><param name="Copies" value="1"><param name="DefaultExt" value=""><param name="DialogTitle" value=""><param name="FileName" value=""><param name="Filter" value=""><param name="FilterIndex" value="0"><param name="Flags" value="0"><param name="FontBold" value="0"><param name="FontItalic" value="0"><param name="FontName" value=""><param name="FontSize" value="8"><param name="FontStrikeThru" value="0"><param name="FontUnderLine" value="0"><param name="FromPage" value="0"><param name="HelpCommand" value="0"><param name="HelpContext" value="0"><param name="HelpFile" value=""><param name="HelpKey" value=""><param name="InitDir" value=""><param name="Max" value="0"><param name="Min" value="0"><param name="MaxFileSize" value="260"><param name="PrinterDefault" value="1"><param name="ToPage" value="0"><param name="Orientation" value="1"></object></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 20px">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 1439px;"><strong>
                            For
                            <asp:Label ID="Label1" runat="server" Text="<%$ AppSettings:Company %>"></asp:Label></strong></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; line-height: 50px; width: 1439px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; height: 20px; width: 1439px;">
                            <strong>Authorised Signatory.</strong></td>
                        <td style="height: 20px">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; line-height: 20px; width: 1439px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            I accept the above terms and conditions of my offer of appointment with the organization.
                            I shall join duty on [doj] .
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; line-height: 20px; width: 1439px;">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 1439px;">
                            Posting At:
                            <asp:DropDownList ID="ddlProject" CssClass="droplist"  runat="server" Width="261px">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 1439px;">
                            Date:
                            <asp:TextBox ID="txtAppDate" runat="server"  Width="75px"></asp:TextBox><cc1:CalendarExtender
                                    TargetControlID="txtAppDate" PopupButtonID="txtAppDate" ID="CalendarExtender2"
                                runat="server" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 1439px;">
                            <asp:TextBox ID="txtRTB" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                                BorderWidth="0px" ForeColor="White" Height="1px" scroll="false" TextMode="MultiLine"
                                Width="1px"></asp:TextBox><br />
                            <asp:Button ID="btnSubmit" runat="server"  Text="Submit" OnClick="btnSubmit_Click" 
                            OnClientClick="javascript:return ValidOffer();" CssClass="savebutton" Width="100px"/><asp:Button ID="Button1" runat="server" CssClass="savebutton" 
                                onclick="Button1_Click" Text="Back" /></td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            <input type="hidden" value="Site Manager" id="Designation" runat="server" /> 
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>

