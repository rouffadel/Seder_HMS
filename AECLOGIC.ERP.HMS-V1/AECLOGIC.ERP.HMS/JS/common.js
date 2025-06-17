// JScript File
function alphanumeric(object,objName)
{
if(!isValidText(object,objName,"ALNUM"))
	{return false;}
else
    {return true;}
}

//Validate mail id
function ValidEmail(object,objName)
{
if(!isValidText(object,objName,"EMAIL"))
	{return false;}
else
    {return true;}
}

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

            validExtensions[0] = 'doc';
            validExtensions[1] = 'docx';
            validExtensions[2] = 'rtf';
            validExtensions[3] = 'txt';
            validExtensions[4] = 'jpg';
            validExtensions[5] = 'tiff';
            validExtensions[6] = 'tif';
            validExtensions[7] = 'mdf';
            validExtensions[8] = 'pdf';

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
function trim(str)
{
    return str.replace(/^\s+|\s+$/g, '');

}
function validateDropdown(object,objName)
{
   if(object == null || object.selectedIndex==0)
    {
         alert("Please select " + objName );
         var elm=getObj(object);
         if(elm != null)
            elm.focus();
         return false;
    }
    return true;
}

function regExpChk(object,exp,msg)
{
	var elm=getObj(object);
    var val=object.value;	 
    if(val!='')
    {
 		var rx=new RegExp(exp);
		var matches=rx.exec(val);
		if (matches==null || val != matches[0])
		{
		    alert(msg);
			
			elm.focus();
			return false;
		}
	 }		return true;		
}

function isValidText(object,strMsg,validationType)
{
    var strValue = trim(object.value);
    object.value = strValue;
    if(strValue.length == 0)
       {
        alert("Please enter the " + strMsg) 
        var elm=getObj(object);
        elm.focus();
        return false;
       }
    else
    {
        if(validationType == "ALNUM")
        {
            if(!regExpChk(object,"[a-z A-Z 0-9]+",'Please enter alphanumerics only!!'))
                return false;
            else
                return true;
        }
        else if(validationType == "EMAIL")
        {
            if(!regExpChk(object,"\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*",'Please enter valid email address!'))
                return false;
            else
                return true;
        }
    }
    return true;
}
//function getObj(object)
//{
//	var p_elm = object;
//	var elm;
//    if(typeof(p_elm) == "object")
//    {elm = p_elm;} else {elm = document.getElementById(p_elm);}
//    
//    return elm; 
//}

    function redirectPage(fileName,type,sExtraparameters)
    {
    if(type == "DWN")
        window.location.href="download.aspx?file=" + fileName;
    else if(type == "TRAN")
        window.location.href="https://secure.shareit.com/shareit/checkout.html?PRODUCT[" + fileName +"]=1&currencies=USD";
        return false;
    
}

//for date formate dd/MMM/yyyy

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

//Date of Birth
function chkDOB(object, msg, isRequired, waterMark) 
{
    if (!chkDate(object, msg, isRequired, waterMark))
        return false;
    return isDOB(getObj(object).value);
      
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
return true; 
}

function isDOB(datevalue) 
{
    // checking for Year
    var year = datevalue.substring(6, datevalue.length);
    var month = datevalue.substring(3, 5); var day = datevalue.substring(0, 5);
    var mChk = 0; var dChk = 0; var curDate = new Date(); var curYear = curDate.getFullYear();
    var curMonth = curDate.getMonth(); var curDay = curDate.getDate();
    var flgDate = 0;
    //Same year
    curMonth = curDate.getMonth();
    if (eval(year) == eval(curYear)) {
        if (eval(month) > eval(curMonth) + 1)
        { flgDate = 1; }
        if (eval(month) == eval(curMonth) + 1) {
            if (eval(day) > eval(curDay))
            { flgDate = 1; }
        }
    }
    if (eval(year) <= 1900 || eval(year) > eval(curYear) || flgDate == 1) { alert("please enter the date >(01/01/1900) and < current date!!!"); return false; }
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
        var rx = new RegExp("[a-zA-Z][a-z A-Z]*");
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
			elm.value='';
			elm.focus();
			return false;
		}
	}		return true;
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
	{	alert("Select " + msg + "!!!");
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
 function chkFloatNumber(object,msg,isRequired,waterMark)
{ 
 var elm=getObj(object);
    var val=elm.value;  
    if(isRequired){ 
    if(!Reval(object,msg,waterMark))
    {return false;}}
    if(val!='')
    {
   var rx=new RegExp("[0-9 .]*");
  var matches=rx.exec(val);
  if (matches==null || val != matches[0])
  {alert(msg + " can take numbers only!!!");
   elm.value='';
   elm.focus();
   return false;
  }
 }  return true;
}



 

