function ValidateWorkSite(txt) {

    if (txt.value.length != 0) {
        if (txt.value.length < 5) {
            alert('Enter valid WorkSite Name')
            txt.value = '';
            txt.focus();
            return false;
        }
        else {
            var i;
            var txt1 = txt.value;

            if (txt1.charAt(0) != 'C' && txt1.charAt(0) != 'c') {
                alert("Enter valid WorkSite Name, Suggested to select WorkSite from the populating list");
                txt.value = '';
                txt.focus();
                return false;
            }
            for (i = 2; i < 6; i++) {

                var c = txt1.charAt(i);
                if (isNaN(c)) {
                    alert("Enter valid WorkSite Name");
                    txt.value = '';
                    txt.focus();
                    return false;
                }
            }
            return true;
        }
    }
}

function ValidateEmployee(txt) {

    if (txt.value.length != 0) {
        if (txt.value.length < 5) {
            alert('Enter valid Employee Name')
            txt.value = '';
            txt.focus();
            return false;
        }
        else {
            var i;
            var txt1 = txt.value;

            if (txt1.charAt(0) != 'E' && txt1.charAt(0) != 'e') {
                alert("Enter valid Employee Name, Suggested to select Employee from the populating list");
                txt.value = '';
                txt.focus();
                return false;
            }
            for (i = 1; i < 5; i++) {

                var c = txt1.charAt(i);
                if (isNaN(c)) {
                    alert("Enter valid Employee Name");
                    txt.value = '';
                    txt.focus();
                    return false;
                }
            }
            return true;
        }
    }
}
function ValidateVendor(txt) {

    if (txt.value.length != 0) {
        if (txt.value.length < 5) {
            alert('Enter valid Vendor Name')
            txt.value = '';
            txt.focus();
            return false;
        }
        else {
            var i;
            var txt1 = txt.value;

            if (txt1.charAt(0) != 'V' && txt1.charAt(0) != 'v') {
                alert("Enter valid Vendor Name, Suggested to select Vendor from the populating list");
                txt.value = '';
                txt.focus();
                return false;
            }
            for (i = 1; i < 5; i++) {

                var c = txt1.charAt(i);
                if (isNaN(c)) {
                    alert("Enter valid Vendor Name");
                    txt.value = '';
                    txt.focus();
                    return false;
                }
            }
            return true;
        }
    }
}

function ValidateMaterial(txt) {


    if (txt.value.length != 0) {
        if (txt.value.length < 5) {
            alert('Enter Material Name')
            txt.value = '';
            txt.focus();
            return false;
        }
        else {
            var i;
            var txt1 = txt.value;


            for (i = 1; i < 5; i++) {

                var c = txt1.charAt(i);
                if (isNaN(c)) {
                    alert("Enter Material Name");
                    txt.value = '';
                    txt.focus();
                    return false;
                }
            }
            return true;
        }
    }
}

function ValidatePOs(txt) {

    if (txt.value.length != 0) {
        if (txt.value.length > 6) {

            alert('Enter PO NO')
            txt.value = '';
            txt.focus();
            return false;

        }
        else {
            var i;
            var txt1 = txt.value;

            if (txt1.charAt(5) != '' && txt1.charAt(5) != '') {
                alert("Enter valid  Number ");
                txt.value = '';
                txt.focus();
                return false;
            }


            for (i = 1; i < 6; i++) {

                var c = txt1.charAt(i);
                if (isNaN(c)) {
                    alert("Enter PONO");
                    txt.value = '';
                    txt.focus();
                    return false;
                }
            }
            return true;
        }
    }

}

function ValidateGoodsGroups(txt) {

    if (txt.value.length != 0) {
        if (txt.value.length < 5) {
            alert('Enter Group Name')
            txt.value = '';
            txt.focus();
            return false;
        }
        else {
            var i;
            var txt1 = txt.value;


            for (i = 1; i < 5; i++) {

                var c = txt1.charAt(i);
                if (isNaN(c)) {
                    alert("Enter Group Name");
                    txt.value = '';
                    txt.focus();
                    return false;
                }
            }
            return true;
        }
    }

}


function ValidateProject(txt) {

    if (txt.value.length != 0) {
        if (txt.value.length < 5) {
            alert('Enter valid Project Name')
            txt.value = '';
            txt.focus();
            return false;
        }
        else {
            var i;
            var txt1 = txt.value;

            if (txt1.charAt(0) != 'C' && txt1.charAt(0) != 'c') {
                alert("Enter valid Project Name, Suggested to select projects from the populating list");
                txt.value = '';
                txt.focus();
                return false;
            }
            for (i = 1; i < 5; i++) {

                var c = txt1.charAt(i);
                if (isNaN(c)) {
                    alert("Enter valid Project Name");
                    txt.value = '';
                    txt.focus();
                    return false;
                }
            }
            return true;
        }
    }
}





