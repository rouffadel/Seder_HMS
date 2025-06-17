var xmlhttp=false;
/*@cc_on @*/
/*@if (@_jscript_version >= 5)
// JScript gives us Conditional compilation, we can cope with old IE versions.
// and security blocked creation of the objects.
 try {
  xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
 } catch (e) {
  try {
   xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
  } catch (E) {
   xmlhttp = false;
  }
 }
@end @*/
if (!xmlhttp && typeof XMLHttpRequest!='undefined') {
	try {
		xmlhttp = new XMLHttpRequest();
	} catch (e) {
		xmlhttp=false;
	}
}
if (!xmlhttp && window.createRequest) {
	try {
		xmlhttp = window.createRequest();
	} catch (e) {
		xmlhttp=false;
	}
}

function ProblemSuggestions() {
    
}

ProblemSuggestions.prototype.requestSuggestions = function (oAutoSuggestControl /*:AutoSuggestControl*/,
                                                          bTypeAhead /*:boolean*/) {
    var aSuggestions = [];
    var sTextboxValue = oAutoSuggestControl.textbox.value;
    var CtrlName = oAutoSuggestControl.textbox.name;
    var Smatch1 = CtrlName.match("txtGDN");
    var Smatch2 = CtrlName.match("txtPono");
    var TypeID;
    if (Smatch1 != null) {
        TypeID = 1;
    }
    if (Smatch2 != null) {
        TypeID = 2;
    }
    if (sTextboxValue.length > 0) {
        xmlhttp.open("GET", "Data.aspx?val=" + sTextboxValue + "&TypeID=" + TypeID, true);
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                if (xmlhttp.responseText.length > 0) {
                    var strData = xmlhttp.responseText.split(",");
                    for (i = 0; i < strData.length; i++) {
                        aSuggestions.push(strData[i]);
                    }
                }
                oAutoSuggestControl.autosuggest(aSuggestions, false);
            }
        }
        xmlhttp.send(null)
    }
};