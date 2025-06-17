//for date format DD/MM/YYYY
function fDate(object)
{
 var elm=getObj(object);
 var val=elm.value;
 alert(val);
    
 var Ary=val.split('/');
 var d,m,y;
 if(Ary[0].length==1)
  d="0"+Ary[0];
  if(Ary[1].length==1)
  m="0"+Ary[1];
  if(Ary[2].length==3)
  y=Ary[2]+"0";
  if(Ary[2].length==2)
  y=Ary[2]+"00";
  if(Ary[2].length==1)
  y=Ary[2]+"000";
  //elm.value=d+'/'+m+'/'+y;
}
//Time Checking
function chkTime(object, msg, isRequired, waterMark) {
    var elm = getObj(object);
    var val = elm.value;

    if (isRequired) {
        if (!Reval(object, msg, waterMark))
        { return false; }
    }
    var rx = new RegExp("^([0-1][0-9]|[2][0-3])(:([0-5][0-9])){1,2}[AP]M$");
    var matches = rx.exec(val);
    if (matches == null || val != matches[0]) {
        alert("Invalid Time,The required format is(HH:MM AM/PM)");
        elm.focus(); return false;

    }
    if (!timechk(val))
    { elm.focus(); return false; }
    return true;

}

//Date of Birth
function chkDate(object,msg,isRequired,waterMark)
{
 var elm=getObj(object);
 var val=elm.value;
 
   if(isRequired){
    if(!Reval(object,msg,waterMark))
    {return false;}}
   
 var rx=new RegExp("^\\s*(\\d{2})([/])(\\d{2})([/])(\\d{4})\\s*$");
 var matches=rx.exec(val);
 if (matches==null || val != matches[0])
 { alert("Invalid Date,The required format is(DD/MM/YYYY)!!! ");
   elm.focus(); return false; 
 }
 if (!datechk(val))
 { elm.focus(); return false; } return true;
}   

//Date Checking

function datechk(datevalue)
{

 var year = datevalue.substring(6,datevalue.length);
 var month=datevalue.substring(3,5); var day=datevalue.substring(0,2);
 var mChk=0; var dChk=0; var curDate=new Date(); var curYear=curDate.getFullYear();
 var curMonth=curDate.getMonth(); var curDay=curDate.getDate();
 //checking for month
 if(eval(month)>12 || eval(month)< 1){alert("Invalid Month,The required format is(DD/MM/YYYY) !!!  \n Eg: Date: 01/01/2009 " );
 return false;} 
 //checking for day
 if(eval(month)==01 || eval(month)==03 || eval(month)==05 || eval(month)==07 || eval(month)==08 || eval(month)==10 || eval(month)==12){
  if(eval(day)>31 || eval(day)<1){
  alert(" Invalid Day,The required format is(DD/MM/YYYY) !!!  \n Eg: Date: 01/01/2009  ");
  return false;}}
 else{
 if(eval(month)==02)
 { if(isLeap(year))
  { if(eval(day)>29 || eval(day)<1)
   {alert(" Invalid Day,The required format is(DD/MM/YYYY) !!!  \n Eg: Date: 01/01/2009 "); return false;}
  }
  else
  { if(eval(day)>28 || eval(day)<1)
   {alert(" Invalid Day,The required format is(DD/MM/YYYY) !!! \n Eg: Date: 01/01/2009  ");return false; }
  }
 }
 else{if(eval(day)>30 || eval(day)<1){alert(" Invalid Day,The required format is(DD/MM/YYYY) !!!  \n Eg: Date: 01/01/2009 ");return false;}}}
 // checking for Year
// var flgDate=0;
// //Same year
// curMonth=curDate.getMonth();
// if(eval(year)==eval(curYear))
// { if(eval(month)>eval(curMonth)+1) 
//  {flgDate=1;}
//  if(eval(month)==eval(curMonth)+1)
//  { if(eval(day)>eval(curDay)) 
//   {flgDate=1;}      
//  }
// }  
// if(eval(year)<=1900 || eval(year)>eval(curYear) || flgDate==1){alert("please enter the date >(01/01/1900) and < current date!!!");return false;}
 return true; 
}

//Is Leapyear
function isLeap(year)
{
 if((year/4) != Math.floor(year/4)) return false;
 if((year/100) != Math.floor(year/100)) return true;
 if((year/400) != Math.floor(year/400)) return false;
 return true;
}

//For PIN Code

function chkPinCode(object,msg,isRequired,waterMark)
{
 var elm=getObj(object);
    var val=elm.value; 
    if(isRequired) {
    if(!Reval(object,msg,waterMark))
    {return false;}}
    if(val!='')
    {
 var rx=new RegExp("\\d{6}");
 var matches=rx.exec(val);
 if (matches==null || val != matches[0])
 {alert("Invalid PIN! \n\nEnter 6 Digits valid PIN." );
  //elm.value='';
  elm.focus();
  return false;}}return true;
}
//For Mobile

function chkMobile(object,msg,isRequired,waterMark)
{
 var elm=getObj(object);
    var val=elm.value; 
    if(isRequired) {
    if(!Reval(object,msg,waterMark))
    {return false;}}
    if(val!='')
    {
 var rx=new RegExp("\\d{10}");
 var matches=rx.exec(val);
 if (matches==null || val != matches[0])
 {alert("Invalid Mobile Number! \n\nEnter 10 digits valid Mobile Number." );
  //elm.value='';
  elm.focus();
  return false;}}return true;
}

//For Phone /Mobile

function chkPhoneOrMobile(object,msg,isRequired,waterMark)
{
 var elm=getObj(object);
    var val=elm.value; 
    if(isRequired) {
    if(!Reval(object,msg,waterMark))
    {return false;}}
    if(val!='')
    {
 var rx=new RegExp("\\d{10,11}");
 var matches=rx.exec(val);
 if (matches==null || val != matches[0])
 {alert("Invalid Phone\Mobile Number! \n\nEnter 11 digits Phone number.\nOr\nEnter 10 digits Mobile number" );
  //elm.value='';
  elm.focus();
  return false;}}return true;
}

//Email Address 
function chkEmail(object,msg,isRequired,waterMark)
{   
 var elm=getObj(object);
    var val=elm.value; 
    if(isRequired){
    if(!Reval(object,msg,waterMark))
    {return false;}}
    if(val!='')
    {
 var rx=new RegExp("\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
 var matches=rx.exec(val);
 if (matches==null || val != matches[0])
 {alert("Invalid "+ msg + "!!!");//elm.value='';
  elm.focus();
  return false;}}return true;
}

//For First Name, Last Name and Middle Name

function chkName(object,msg,isRequired,waterMark)
{
 var elm=getObj(object);
    var val=elm.value;  
      if(isRequired){
    if(!Reval(object,msg,waterMark))
    {return false;}}
    if(val!='')
    {
     var rx=new RegExp("[a-z A-Z]*");
     var matches=rx.exec(val);
     if (matches==null || val != matches[0])
     {alert(msg + " can take alphabets only!!!" );
      //elm.value='';
      elm.focus();
      return false;}}return true;
}

//Check Number
function chkNumber(object,msg,isRequired,waterMark)
{ 
 var elm=getObj(object);
    var val=elm.value;  
    if(isRequired){ 
    if(!Reval(object,msg,waterMark))
    {return false;}}
    if(val!='')
    {
   var rx=new RegExp("[0-9]*");
  var matches=rx.exec(val);
  if (matches==null || val != matches[0])
  {alert(msg + " can take numbers only!!!");
   elm.focus();
   return false;
  }
 }  return true;
}

//Check Number
function chkDoubleNumber(object,msg,isRequired,waterMark)
{ 
 var elm=getObj(object);
    var val=elm.value;  
    if(isRequired){ 
    if(!Reval(object,msg,waterMark))
    {return false;}}
    if(val!='')
    {
   var rx=new RegExp("[0-9]*([.][0-9]+)?");
  var matches=rx.exec(val);
  if (matches==null || val != matches[0])
  {alert(msg + " can take numbers only!!!");
   elm.focus();
   return false;
  }
 }  return true;
}

//For Address, Street, Town

function chkAddress(object,msg,IsRequired,waterMark)
{
 var elm=getObj(object);
    var val=elm.value; 
      if(IsRequired){
    if(!Reval(object,msg,waterMark))
    {return false;}} 
    if(val!='')
    {
    var rx=new RegExp("[a-z A-Z 0-9]*");
 var matches=rx.exec(val);
 if (matches==null || val != matches[0])
 {alert(msg + " can take alphabets only!!!" );
  //elm.value='';
  elm.focus();
  return false;}}return true;
}

//For Dropdown list
function chkDropDownList(object,msg)
{

 var elm=getObj(object);
 
 if(elm.selectedIndex == 0)
 { alert("Select " + msg + "!!!");
   elm.focus();
   return false;
 }return true;
}

//geting the object
function getObj(the_id) 
{
    if (typeof(the_id) == "object") {
    return the_id;
    }
    if (typeof document.getElementById != 'undefined') 
    {
        return document.getElementById(the_id);
    } else if (typeof document.all != 'undefined') 
    {
        return document.all[the_id];
    } else if (typeof document.layers != 'undefined') 
    {
        return document.layers[the_id];
    } else 
    {
        return null;
    }
}
//Required Validation
function Reval(object,msg,waterMark)
{
 var elm=getObj(object);
    var val=elm.value;  
    if(val == '' || val.length == 0 || val == waterMark )
 { 
  alert(msg + " should not be empty!!! ");
  //elm.value = waterMark;
  elm.focus();
  return false;
 }
 return true;
}
// File Upload Validation

function checkFileLength(elem,obj)
{ 
    
        var filePath = elem.value;
        if(filePath == "") 
        {
            alert("Please select file to upload.");
            return false;
        }
        else
        {
            if(filePath.indexOf('.') == -1) 
            return false;

            var validExtensions = new Array(); 
            var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();

            validExtensions[0] = 'jpeg';
            validExtensions[1] = 'bmp';
            validExtensions[2] = 'gif';
            validExtensions[3] = 'png';
            validExtensions[4] = 'tif';
            validExtensions[5] = 'jpg';

            for(var i = 0; i < validExtensions.length; i++) { 
            if(ext == validExtensions[i])

            return true; 
            }

            alert('Invalid file format.'); 
            var who=obj;

            who.value="";var who2= who.cloneNode(false); 
            who2.onchange= who.onchange;

            who.parentNode.replaceChild(who2,who); 

            return false;  
        }           
}
function OnlyNumbers(e)
{
    var keynum;
    var keychar;
    var numcheck;

    if(window.event) // IE
    {
    keynum = e.keyCode;
    }
    else if(e.which) // Netscape/Firefox/Opera
    {
    keynum = e.which;
    }
    keychar = String.fromCharCode(keynum);
    numcheck = /\d/;
    return numcheck.test(keychar);
}

function trim(str, chars)
{
 return ltrim(rtrim(str, chars), chars);
}
 
function ltrim(str, chars) 
{
 chars = chars || "\\s";
 return str.replace(new RegExp("^[" + chars + "]+", "g"), "");
}
 
function rtrim(str, chars) 
{
 chars = chars || "\\s";
 return str.replace(new RegExp("[" + chars + "]+$", "g"), "");
}
//OnClientDateSelectionChanged="checkDate"
function checkDate(sender,args)
{
 if (sender._selectedDate > new Date())
            {
                alert("Select a day earlier than today!");
                sender._selectedDate = new Date(); 
                // set the date back to the current date
sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
}
//changeToUpper
function changeToUpper(objFrm,objTxt)
{	
	var objFrmName=objFrm.name;
	var objTxtName=objTxt.name;
	if(objTxt.value!='')
    {
         var rx=new RegExp("[, a-z A-Z]*");
         var matches=rx.exec(objTxt.value);
         if (matches==null || objTxt.value != matches[0])
         {
           alert("Alphabets only!!!" );
           objTxt.value='';
           objTxt.focus();
           return false;
         }
    }
	document.forms(objFrmName).elements(objTxtName).value=document.forms(objFrmName).elements(objTxtName).value.toUpperCase(); 
	if(objTxtName.length>1)
	    return false;
	return true;
}

//OnClientShown="ChangeCalendarView"
function ChangeCalendarView(sender,args)
{
   sender._switchMode("years", true);           
}
//CancelAsyncPostBack
function CancelAsyncPostBack()
{
// alert(prm);
var prm = Sys.WebForms.PageRequestManager.getInstance();
//alert(prm);
     if (prm.get_isInAsyncPostBack())
     {
      prm.abortPostBack();
     }
}
    
function ItemMouseOver(oRow, Color)
{
    oRow.originalBackgroundColor = oRow.style.backgroundColor
    oRow.style.backgroundColor = '#FF7F2A'; //Yellow    
}

function ItemMouseOut(oRow)
{
    oRow.style.backgroundColor = oRow.originalBackgroundColor;
}

function Resources(src,dest)
 {    
     var ctrl = document.getElementById(src);
      // call server side method
     PageMethods.GetResources(ctrl.value, CallResources, CallFailed, dest);
 }
 
 // set the destination textbox value with the CallResources
 function CallResources(res, destCtrl)
 {    
     var dest = document.getElementById(destCtrl);
     //var ResArray = new Array(res);
     
     var len = dest.options.length;
        for(i=len-1; i>=0; i--)
        {
          dest.options.remove(i);
        }
        addOption(dest,"Select Resource Type", "1");
     for (var i=0; i < res.length;++i)
     {
        var Result=res[i].split('~');
       // alert( Result[0]);
        //alert( Result[1]);
        addOption(dest, Result[0], Result[1]);
     }
 }
 
 // alert message on some failure
 function CallFailed(res, destCtrl)
 {
     alert(res.get_message());
 }
 
function addOption(selectbox,text,value )
{
    var optn = document.createElement("OPTION");
    optn.text = text;
    optn.value = value;
    selectbox.options.add(optn);
}


function ReverseString() {
    var originalString = document.getElementById('txtReverse').value;
    var reversedString = Reverse(originalString);
    RetrieveControl();
    // to handle in IE 7.0
    if (window.showModalDialog) {
        window.returnValue = reversedString;
        window.close();
    }
    // to handle in Firefox
    else {
        if ((window.opener != null) && (!window.opener.closed)) {
            // Access the control.       
            window.opener.document.getElementById(ctr[1]).value = reversedString;
        }
        window.close();
    }
}

function Reverse(str) {
    var revStr = "";
    for (i = str.length - 1; i > -1; i--) {
        revStr += str.substr(i, 1);
    }
    return revStr;
}

function RetrieveControl() {
    //Retrieve the query string
    queryStr = window.location.search.substring(1);
    // Seperate the control and its value
    ctrlArray = queryStr.split("&");
    ctrl1 = ctrlArray[0];
    //Retrieve the control passed via querystring
    ctr = ctrl1.split("=");
}

function InvokePop(fname) {
    val = document.getElementById(fname).value;
    // to handle in IE 7.0          
    if (window.showModalDialog) {
        retVal = window.showModalDialog("DSpaceManage.aspx?Control1=" + fname + "&ControlVal=" + val, 'Show Popup Window', "dialogHeight:90px,dialogWidth:250px,resizable:yes,center:yes,");
        document.getElementById(fname).value = retVal;
    }
    // to handle in Firefox
    else {
        retVal = window.open("DSpaceManage.aspx?Control1=" + fname + "&ControlVal=" + val, 'Show Popup Window', 'height=90,width=250,resizable=yes,modal=yes');
        retVal.focus();
    }
}
function GetDecimalDelimiter(countryCode) {

    switch (countryCode) {
        case 3:
            return '#';
        case 2:
            return ',';
        default:
            return '.';
    }
}

function GetCommaDelimiter(countryCode) {

    switch (countryCode) {
        case 3:
            return '*';
        case 2:
            return ',';
        default:
            return ',';
    }

}

function FormatClean(num) {
    var sVal = '';
    var nVal = num.length;
    var sChar = '';

    try {
        for (c = 0; c < nVal; c++) {
            sChar = num.charAt(c);
            nChar = sChar.charCodeAt(0);
            if ((nChar >= 48) && (nChar <= 57)) { sVal += num.charAt(c); }
        }
    }
    catch (exception) { AlertError("Format Clean", exception); }
    return sVal;
}
function FormatNumber(num, countryCode, decimalPlaces) {

    var minus = '';
    var comma = '';
    var dec = '';
    var preDecimal = '';
    var postDecimal = '';

    try {

        decimalPlaces = parseInt(decimalPlaces);
        comma = GetCommaDelimiter(countryCode);
        dec = GetDecimalDelimiter(countryCode);

        if (decimalPlaces < 1) { dec = ''; }
        if (num.lastIndexOf("-") == 0) { minus = '-'; }

        preDecimal = FormatClean(num);

        // preDecimal doesn't contain a number at all.
        // Return formatted zero representation.

        if (preDecimal.length < 1) {
            return minus + FormatEmptyNumber(dec, decimalPlaces);
        }

        // preDecimal is 0 or a series of 0's.
        // Return formatted zero representation.

        if (parseInt(preDecimal) < 1) {
            return minus + FormatEmptyNumber(dec, decimalPlaces);
        }

        // predecimal has no numbers to the left.
        // Return formatted zero representation.

        if (preDecimal.length == decimalPlaces) {
            return minus + '0' + dec + preDecimal;
        }

        // predecimal has fewer characters than the
        // specified number of decimal places.
        // Return formatted leading zero representation.

        if (preDecimal.length < decimalPlaces) {
            if (decimalPlaces == 2) {
                return minus + FormatEmptyNumber(dec, decimalPlaces - 1) + preDecimal;
            }
            return minus + FormatEmptyNumber(dec, decimalPlaces - 2) + preDecimal;
        }

        // predecimal contains enough characters to
        // qualify to need decimal points rendered.
        // Parse out the pre and post decimal values
        // for future formatting.

        if (preDecimal.length > decimalPlaces) {
            postDecimal = dec + preDecimal.substring(preDecimal.length - decimalPlaces,
                                               preDecimal.length);
            preDecimal = preDecimal.substring(0, preDecimal.length - decimalPlaces);
        }

        // Place comma oriented delimiter every 3 characters
        // against the numeric represenation of the "left" side
        // of the decimal representation.  When finished, return
        // both the left side comma formatted value together with
        // the right side decimal formatted value.

        var regex = new RegExp('(-?[0-9]+)([0-9]{3})');

        while (regex.test(preDecimal)) {
            preDecimal = preDecimal.replace(regex, '$1' + comma + '$2');
        }

    }
    catch (exception) { AlertError("Format Number", exception); }
    return minus + preDecimal + postDecimal;
}

function FormatEmptyNumber(decimalDelimiter, decimalPlaces) {
    var preDecimal = '0';
    var postDecimal = '';
    for (i = 0; i < decimalPlaces; i++) {
        if (i == 0) { postDecimal += decimalDelimiter; }
        postDecimal += '0';
    }
    return preDecimal + postDecimal;
}


function AlertError(methodName, e) {
    if (e.description == null) { alert(methodName + " Exception: " + e.Message.ToString(),AlertMsg.MessageType.Error); }
    else { alert(methodName + " Exception: " + e.description); }
}
