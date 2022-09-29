


function Validate() {
    var blnResult = false;
    debugger;
    if ($('#FromAccount').val() === "") {

        swal("Error!", "Please first select from account.", "error");

        blnResult = false;
    }
    else if ($('#ToAccount').val() === "") {

        swal("Error!", "Please first select to account", "error");


        blnResult = false;
    }
    else if ($('#FromAccount').val() === $('#ToAccount').val()) {

        swal("Error!", "Both account should never be same.", "error");


        blnResult = false;
    }
    else if ($('#Amount').val() <= 0) {

        swal("Error!", "Amount should be greater then zero.", "error");


        blnResult = false;
    }
    
    else {
        blnResult = true;
    }

    return blnResult;
}
