function checkearlierDate(sender, args) {
    if (sender._selectedDate < new Date()) {
        alert("You cannot select a day earlier than today!");
        sender._selectedDate = new Date();
        // set the date back to the current date
        sender._textbox.set_Value(sender._selectedDate.format(sender._format))
    }
}




////for date formate dd/MMM/yyyy

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
 { alert("Invalid Date,The required format is(dd/MMM/yyyy)!!! ");
   elm.focus(); return false; 
 }
 if (!datechk(val))
 { elm.focus(); return false; } return true;
}

//Date of Birth
function chkDOB(object, msg, isRequired, waterMark) {
    var elm = getObj(object);
    var val = elm.value;

    if (isRequired) {
        if (!Reval(object, msg, waterMark))
        { return false; } 
    }

    var rx = new RegExp("^\\s*(\\d{2})([/])(\\d{2})([/])(\\d{4})\\s*$");
    var matches = rx.exec(val);
    if (matches == null || val != matches[0]) {
        alert("Invalid Date,The required format is(dd/MMM/yyyy)!!! ");
        elm.focus(); return false;
    }
    if (!datechk(val))
    { elm.focus(); return false; }

    var year = val.substring(6, val.length);
    var month = val.substring(3, 5); var day = val.substring(0, 2);
    var curDate = new Date();
    var DOB = new Date();
    DOB.setDate(day);
    DOB.setMonth(month);
    DOB.setYear(year);

    if( DOB>=curDate)
    {
        alert("Date of Birth should not accept future date!!! ");
        elm.focus(); return false;
    }
     return true;
}   


//Date Checking

function datechk(datevalue)
{
 var year = datevalue.substring(6,datevalue.length);
 var month=datevalue.substring(3,5); var day=datevalue.substring(0,2);
 var mChk=0; var dChk=0; var curDate=new Date(); var curYear=curDate.getFullYear();
 var curMonth=curDate.getMonth(); var curDay=curDate.getDate();
 //checking for month
 if(eval(month)>12 || eval(month)< 1){alert("Invalid Month,The required format is(dd/MMM/yyyy) !!!  \n Eg: Date: 01/01/2009 " );
 return false;} 
 //checking for day
 if(eval(month)==01 || eval(month)==03 || eval(month)==05 || eval(month)==07 || eval(month)==08 || eval(month)==10 || eval(month)==12){
  if(eval(day)>31 || eval(day)<1){
  alert(" Invalid Day,The required format is(dd/MMM/yyyy) !!!  \n Eg: Date: 01/01/2009  ");
  return false;}}
 else{
 if(eval(month)==02)
 { if(isLeap(year))
  { if(eval(day)>29 || eval(day)<1)
   {alert(" Invalid Day,The required format is(dd/MMM/yyyy) !!!  \n Eg: Date: 01/01/2009 "); return false;}
  }
  else
  { if(eval(day)>28 || eval(day)<1)
   {alert(" Invalid Day,The required format is(dd/MMM/yyyy) !!! \n Eg: Date: 01/01/2009  ");return false; }
  }
 }
 else{if(eval(day)>30 || eval(day)<1){alert(" Invalid Day,The required format is(dd/MMM/yyyy) !!!  \n Eg: Date: 01/01/2009 ");return false;}}}
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
   elm.value='';
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
 if (matches==null || val != matches[0]) {
     alert(msg + " can take alphabets only and numerics Only!!!");
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
    var elm = getObj(object);
    //elm.value = (elm.value).replace(/^\s*|\s*$/g,'');
    var val = (elm.value).replace(/^\s*|\s*$/g,'');
    if(val == '' || val.length == 0 || val == waterMark )
 { 
  alert(msg + " should not be empty!!! ");
  //elm.value = waterMark;
  elm.focus();
  return false;
 }
 return true;
}


//Check Decimal
function checkdecmial(control) {
    var a, i, s
    var count = 0
    a = control.value.length
    for (i = 0; i < a; i++) {
        s = control.value.substring(i, i + 1)
        if (s == ".") {
            count = count + 1;
        }
    }
    if (count > 1) {
        alert("Check the Entered Value.");
        control.focus();
        return false;
    }
}



//Check FillLength
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

            validExtensions[0] = 'pbd';
            validExtensions[1] = 'jpeg';
            validExtensions[2] = 'bmp';
            validExtensions[3] = 'gif';
            validExtensions[4] = 'png';
            validExtensions[5] = 'xml';
            validExtensions[6] = 'tif';
            validExtensions[7] = 'mdf';
            validExtensions[8] = 'pdf';
            validExtensions[9] = 'jpg';

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


//Check File
function checkFile()
{
     var elm=null;
     elm=getObj("ctl00_ContentPlaceholder1_fileItemImage");
     if(!checkFileLength(elm,elm))
        return false;
     else
        return true;
 }
 //Check website  
 function chkWebSite(object, msg, isRequired, waterMark) {
     var elm = getObj(object);
     var val = elm.value;
     if (isRequired) {
         if (!Reval(object, msg, waterMark))
         { return false; } 
     }
     if (val != '') {
         var rx = new RegExp("\\w+([-\w\.]+)*.\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
         var matches = rx.exec(val);
         if (matches == null || val != matches[0]) {
             alert("Invalid " + msg + "!!!"); //elm.value='';
             elm.focus();
             return false;
         } 
     } return true;
 }

 //For Blood    [A|B|AB|O][\+|\-]  ^(A|B|AB|O)[+-]$


 function ChkBG(object, msg, isRequired, watermark)
 {
    var elm=getObj(object);
    var val=elm.value;
    if (isRequired)
    {
        if(!Reval(object,msg,watermark))
        {return false;}
    }
    if (val != '')
 {
    var rx=new RegExp("^(a|A|B|b|AB|ab|O|o)[+-]$");
    var matches = rx.exec(val);
    if (matches == null || val != matches[0]) 
    {
     alert("Invalid Blood Group");
     elm.focus();
     return false;
    }
} 
 return true;
}


 //For Fax

function ChkFax(object, msg, isRequired,waterMark) {
     var elm = getObj(object);
     var val = elm.value;
     if (isRequired) {
         if (!Reval(object, msg, watermark))
         { return false; }
     }
     if (val != '') {
         var rx = new RegExp("\\d{11}");
         var matches = rx.exec(val);
         if (matches == null || val != matches[0]) {
             alert("Invalid Fax Number! \n\nEnter 11 digits Fax number.\n");
             //elm.value='';
             elm.focus();
             return false;
         } 
     } return true;
 }

  
 //For PAN

function chkPAN(object, msg, isRequired, waterMark) {
    var elm = getObj(object);
    var val = elm.value;
    if (isRequired) {
        if (!Reval(object, msg, waterMark))
        { return false; }
    }
    if (val != '') {
        var rx = new RegExp("[A-Z]{3}[PCHFATBLJG][A-Z]\\d{4}[A-Z]");
        var matches = rx.exec(val);
        if (matches == null || val != matches[0]) {
            alert("Invalid PAN Number! \n\nEg. AAAAA1234A");
            //elm.value='';
            elm.focus();
            return false;
        }
    } return true;
} 

 
 function AutoDateFill(objDateControl) {

             
           
            var arrMonths = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
            var arriMonths = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
            var objDate = $get(objDateControl);

            var delim = ["/", " ", ".", "-"]; // posible delimeters in date format

            var DateSplit;

            for (i = 0; i < delim.length; i++) {
                DateSplit = objDate.value.split(delim[i]);
                if (DateSplit.length > 1)
                    break;
            }
            if (DateSplit.length == 1 && objDate.value.length>4) {
                DateSplit=[objDate.value.substring(0,2),objDate.value.substring(2,4),objDate.value.substring(4, objDate.value.length)];
            }else if (DateSplit.length == 1 && objDate.value.length<=4){ 
                DateSplit=[objDate.value.substring(0,1),objDate.value.substring(1,2),objDate.value.substring(2, objDate.value.length)];
            } 

            if (DateSplit.length > 1) {



                var Di = 0;
                var Mi = 1;
                var Yi = 2;
                var DtString = ""; 
                
                var CurDate = new Date();
                var CurYear = CurDate.getFullYear();
                var flgM = false;
                if (DateSplit[Di].length == 1)
                { DtString = "0" + DateSplit[Di]; } else { DtString = DateSplit[Di].substring(0, 2); }

                try {
                    if(eval(DtString)>31)
                        DtString="31";
                } catch (e){}
                
                if (DateSplit[Mi].length == 1) {
                    for (i = 0; i < arriMonths.length; i++) {
                        if (arriMonths[i] == "0" + DateSplit[Mi]) {
                            DtString = DtString + " " + arrMonths[i] + " ";
                            flgM = true;
                            break;
                        }
                    }
                } else if (DateSplit[Mi].length == 2) {
                    for (i = 0; i < arriMonths.length; i++) {
                    var Mstring= DateSplit[Mi].substring(0, 2);
                        try {
                            if (eval(Mstring) > 12) {
                                Mstring = DateSplit[Mi].substring(0, 1);
                            }
                        } catch (e) {
                        }

                        if (arriMonths[i] ==Mstring) {
                            DtString = DtString + " " + arrMonths[i] + " ";
                            flgM = true;
                            break;
                        }
                    }
                } else {
                    for (i = 0; i < arrMonths.length; i++) {
                        if (arrMonths[i] == DateSplit[Mi].substring(0, 3)) {
                            DtString = DtString + " " + arrMonths[i] + " ";
                            flgM = true;
                            break;
                        }
                    }
                }

                if (!flgM) {
                    DtString = DtString + " " + arrMonths[0] + " ";
                }

                if (DateSplit.length == 2) {
                    DtString = DtString + CurYear.toString();
                } else {
                    if (DateSplit[Yi].length == 1) {
                        DtString = DtString + "200" + DateSplit[Yi];
                    } else if (DateSplit[Yi].length == 2) {
                        DtString = DtString + "20" + DateSplit[Yi];
                    } else if (DateSplit[Yi].length == 3) {
                        DtString = DtString + "2" + DateSplit[Yi];
                    } else if (DateSplit[Yi].length > 3) {
                        DtString = DtString + DateSplit[Yi].substring(0, 4);
                    }
                }
                objDate.value = DtString;
            }

        }


        
//Date of Birth
function chkDateDDMMMYYYY(object, msg, isRequired, waterMark) {
    var elm = getObj(object);
    var val = elm.value;

    if (isRequired) {
        if (!Reval(object, msg, waterMark)) {
            return false;
        }
    }

    var rx = new RegExp("^\\s*(\\d{2})([ ])([A-Z][a-z][a-z])([ ])(\\d{4})\\s*$");
    var matches = rx.exec(val);
    if (matches == null || val != matches[0]) {
        alert("Invalid Date,The required format is(dd MMM yyyy)!!! ");
        elm.focus(); return false;
    }
    var arrMonths = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var arriMonths = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
    var objSplt = val.split(" ");
   // val=val.replace(' ', '/');
    for (i = 0; i < arrMonths.length; i++) {
        if (objSplt[1] == arrMonths[i]) {
            val = val.replace(" "+arrMonths[i]+" ","/"+ arriMonths[i]+ "/");
            break;
        }
    }

    if (!datechkDDMMYYYY(val))
    { elm.focus(); return false; } return true;
}

//Date Checking

function datechkDDMMYYYY(datevalue) {

    var year = datevalue.substring(6, datevalue.length);

    var month = datevalue.substring(3, 5); var day = datevalue.substring(0, 2);
    var mChk = 0; var dChk = 0; var curDate = new Date(); var curYear = curDate.getFullYear();
    var curMonth = curDate.getMonth(); var curDay = curDate.getDate();
    //checking for month
    if (eval(month) > 12 || eval(month) < 1) {
        alert("Invalid Month,The required format is(dd MMM yyyy) !!!  \n Eg: Date: 01 Jan 2009 ");
        return false;
    }
    //checking for day
    if (eval(month) == 01 || eval(month) == 03 || eval(month) == 05 || eval(month) == 07 || eval(month) == 08 || eval(month) == 10 || eval(month) == 12) {
        if (eval(day) > 31 || eval(day) < 1) {
            alert(" Invalid Day,The required format is(dd MMM yyyy) !!!  \n Eg: Date: 01 Jan 2009  ");
            return false;
        } 
    }
    else {
        if (eval(month) == 02) {
            if (isLeap(year)) {
                if (eval(day) > 29 || eval(day) < 1)
                { alert(" Invalid Day,The required format is(dd MMM yyyy) !!!  \n Eg: Date: 01 Jan 2009 "); return false; }
            }
            else {
                if (eval(day) > 28 || eval(day) < 1)
                { alert(" Invalid Day,The required format is(dd MMM yyyy) !!! \n Eg: Date: 01 Jan 2009  "); return false; }
            }
        }
        else { if (eval(day) > 30 || eval(day) < 1) { alert(" Invalid Day,The required format is(dd MMM yyyy) !!!  \n Eg: Date: 01 Jan 2009 "); return false; } } 
    }
    return true;
}