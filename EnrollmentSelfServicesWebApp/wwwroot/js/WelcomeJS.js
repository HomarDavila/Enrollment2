$(document).ready(function () {
    //DataEnrollment();
    //EnrollmentPeriod();
    //var memberId = $("#hdnMemberId").val();
    //ChangeEnrollmentEnabled(memberId);
    GetEnrPeriod();

    $('#btnChangeMCO').on('click', function (e) {
        $.redirect(UrlChangeMCO);
    });
});





function ChangeEnrollmentEnabled(memberId) {
    $.ajax({
        type: 'GET',
        url: urlChangePersonMcoEnabled + '?memberId=' + memberId,
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        async: true,
        success: function (data) {
            if (data.code === 0) {
                $('#txtChangeMCOYes').removeAttr('hidden');
                $('#btnChangeMCO').prop('disabled', false);
                $('#btnChangeMCO').attr('class', 'btn btn-info');
            } else {               
                $('#txtChangeMCONo').removeAttr('hidden');
                $('#btnChangeMCO').prop('disabled', true);
                $('#btnChangeMCO').removeClass('btn-info');
                $('#btnChangeMCO').css('background-color', 'gray');
            }   
        },
        error: function (jqXhr, textStatus, errorThrown) {
            swal({
                title: "Error",
                text: textStatus,
                type: "error",
                confirmButtonText: "OK"
            });
        }
    });
}

function GetEnrPeriod() {
    $.ajax({
        type: 'POST',
        url: urlGetEnrPeriod,
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        async: true,
        success: function (data) {
            console.log(data);
            let start = new Date(parseInt(data.objeto.PeriodIni.substr(6)));
            let end = new Date(parseInt(data.objeto.PeriodFin.substr(6)));

            start = ParseDate(start);
            end = ParseDate(end);

            $('#DateStart').html(start);
            $('#DateEnd').html(end);
        },
        error: function (jqXhr, textStatus, errorThrown) {
            swal({
                title: "Error",
                text: textStatus,
                type: "error",
                confirmButtonText: "OK"
            });
        }
    });
}

function ParseDate(date) {
    let day = date.getDate();
    let month = date.getMonth() + 1;
    let year = date.getFullYear();

    return day + '/' + month + '/' + year;
}