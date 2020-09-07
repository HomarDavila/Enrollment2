$(document).ready(function () {
    $("#settings").attr({
        class: "active"
    });
    LoadPage();
    $("#periodForm").submit(function () {
        SendEnrollmentPeriod();
    });
});

function LoadPage() {
    GetEnrollmentPeriod();
}


function formatDateEN(value) {
    if (value === null) return "";
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    if (dt.getFullYear() == 9999) return "";
    var d = new Date(dt),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();
    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;
    return [month, day, year].join('/');
}

function GetEnrollmentPeriod() {
    $.ajax({
        url: urlGetEnrollmentPeriod,
        dataType: 'json',
        complete: function () {
            //end = (new Date()).getTime();
            //var total = end - start;
            //$.unblockUI();
        },
        data: BuildRequestPeriod(),
        type: 'POST',
        async: true,
        success: function (data) {
            $('#DateToPeriod').val(formatDateEN(data.records.PeriodFin));
            $('#DateFromPeriod').val(formatDateEN(data.records.PeriodIni));
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            swal({
                title: "Error!",
                text: textStatus,
                type: "error",
                confirmButtonText: "OK"
            });
        }
    });
}

function SendEnrollmentPeriod() {
    event.preventDefault();
    start = $("#DateFromPeriod").val();
    finish = $("#DateToPeriod").val();

    if (validarFormatoFecha(start) && validarFormatoFecha(finish)) {
        console.log("Correcto");
            $.ajax({
                url: urlSendEnrollmentPeriod,
                dataType: 'json',
                beforeSend: function () {
                    //start = (new Date()).getTime();
                    //$.blockUI({ message: '<div class="icon-spinner9 icon-spin icon-lg"></div>', timeout: 60000, overlayCSS: { backgroundColor: "#000000", opacity: .8, cursor: "wait" }, css: { border: 0, padding: 0, backgroundColor: "transparent" } });
                },
                complete: function () {
                    //end = (new Date()).getTime();
                    //var total = end - start;
                    //$.unblockUI();
                },
                data: BuildRequestPeriod(),
                type: 'POST',
                async: true,
                success: function (data) {
                    GetEnrollmentPeriod();
                    if (data.code === 0) {
                        swal({
                            title: i18next.t('common_success'),
                            text: data.message,
                            type: "success",
                            confirmButtonText: "OK"
                        });
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    swal({
                        title: "Error!",
                        text: textStatus,
                        type: "error",
                        confirmButtonText: "OK"
                    });
                }
            });
    } else {
        swal({
            title: "Error!",
            text: "Las fechas no cumplen el formato MM/dd/yyyy",
            type: "error",
            confirmButtonText: "OK"
        });
    }
}

function BuildRequestPeriod() {
    var request = new Object();

    var fecini = null;
    var fecfin = null;
    var MemberId = null;

    fecini = $("#DateFromPeriod").val();
    fecfin = $("#DateToPeriod").val();

    fechaInicial = fecini.split("/");
    fechaFinal = fecfin.split("/");

    request.fecini = fechaInicial[1] + "/" + fechaInicial[0] + "/" + fechaInicial[2];
    request.fecfin = fechaFinal[1] + "/" + fechaFinal[0] + "/" + fechaFinal[2];
    return request;
}

function validarFormatoFecha(campo) {
    var RegExPattern = /^\d{1,2}\/\d{1,2}\/\d{2,4}$/;
    if ((campo.match(RegExPattern)) && (campo != '')) {
        return true;
    } else {
        return false;
    }
}






