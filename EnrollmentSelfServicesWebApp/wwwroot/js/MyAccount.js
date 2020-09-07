$(function () {
    $("#register-dateofbirth-field").datepicker();
    $("#register-dateofbirth-field").datepicker("option", "dateFormat", "mm/dd/yy");
    //var $startpicker = $('#register-dateofbirth-field').pickadate({
    //    max: true,
    //    selectMonths: true,
    //    selectYears: 70,
    //    closeOnClear: true,
    //    format: 'mm/dd/yyyy',
    //    formatSubmit: 'yyyy-mm-dd',
    //    hiddenPrefix: 'standardDate__'
    //});
    //$picker = $startpicker.pickadate('picker');

    LoadPage();


    $('#btnDisplaySelection').click(function (e) {
        $.blockUI({ message: '<div class="icon-spinner9 icon-spin icon-lg"></div>', timeout: 60000, overlayCSS: { backgroundColor: "#000000", opacity: .8, cursor: "wait" }, css: { border: 0, padding: 0, backgroundColor: "transparent" } });
        //if ($('#chkSendEmail').is(':checked'))
        //    if (validarEmail($('#txtEmail').val()) === false) {
        //        swal({
        //            title: "Advertencia!",
        //            text: "La dirección de email es incorrecta!.",
        //            type: "warning",
        //            confirmButtonText: "OK"
        //        });
        //        return;
        //    }

        //var bool = true;

        var request = new Object();
        request.Id = UserId;
        request.FirstName = $("#register-names-field").val();
        request.LastName1 = $("#register-lastnameOne-field").val();
        request.LastName2 = $("#register-lastNameTwo-field").val();
        var DateOfBirth = $("#register-dateofbirth-field").val();

        var parts = DateOfBirth.split('/');
        //var mydate = new Date(parts[0], parts[1] - 1, parts[2]);
        var mydate = new Date(parts[2], parts[0] - 1, parts[1]);
        request.DateOfBirth = mydate.toJSON();


        //var from = $("#register-dateofbirth-field").val().split("/");
        //request.DateOfBirth = new Date(from[2], from[1] - 1, from[0]);
        //request.DateOfBirth = new Date(1, 1, 2019);// new Date($("#register-dateofbirth-field").val());
        request.SSNLast4 = $("#register-ssn-field").val();
        request.Email = $("#register-email-field").val();
        request.Email2 = $("#register-emailalt-field").val();
        request.PhoneNumber = cleanPhoneNumber($("#register-phone-field").val());
        request.PhoneNumber2 = cleanPhoneNumber($("#register-phonealt-field").val());
        request.MPI = $("#register-mpi-field").val();
        if ($('#register-optin-field').is(':checked')) {
            request.OptIn = true;
        } else {
            request.OptIn = false;
        }

        $.ajax({
            url: urlSetUser,
            dataType: 'json',
            beforeSend: function () {
                start = (new Date()).getTime();
                $.blockUI({ message: '<div class="icon-spinner9 icon-spin icon-lg"></div>', timeout: 60000, overlayCSS: { backgroundColor: "#000000", opacity: .8, cursor: "wait" }, css: { border: 0, padding: 0, backgroundColor: "transparent" } });
            },
            complete: function () {
                end = (new Date()).getTime();
                var total = end - start;
                $.unblockUI();
            },
            data: request,
            type: 'POST',
            async: false,
            success: function (data) {
                console.log(data);
                if (data.code === 0) {
                    swal({
                        title: i18next.t('common_succesfull'),
                        text: i18next.t('Enrollment_sucessfull_register'),
                        type: "success",
                        confirmButtonText: "OK"
                    });
                }
                //else {
                //    bool = data.records.Enabled;
                //}


            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.unblockUI();
                swal({
                    title: "Error!",
                    text: "No se pudo registrar la transacci\u00f3n",
                    type: "error",
                    confirmButtonText: "OK"
                });
            }
        });
    });
});

function LoadPage() {
    $.ajax({
        url: urlGetId,
        dataType: 'json',
        beforeSend: function () {
            start = (new Date()).getTime();
            $.blockUI({ message: '<div class="icon-spinner9 icon-spin icon-lg"></div>', timeout: 60000, overlayCSS: { backgroundColor: "#000000", opacity: .8, cursor: "wait" }, css: { border: 0, padding: 0, backgroundColor: "transparent" } });
        },
        complete: function () {
            end = (new Date()).getTime();
            var total = end - start;
            $.unblockUI();
        },
        data: {
            "UserId": UserId
        },
        type: 'POST',
        async: true,
        success: function (data) {
            if (data.code == 0) {
                var obj = data.records;
                obj.PhoneNumber = formatPhoneNumber(obj.PhoneNumber);
                obj.PhoneNumber2 = formatPhoneNumber(obj.PhoneNumber2);
                $("#register-names-field").val(obj.FirstName);
                $("#register-lastnameOne-field").val(obj.LastName1);
                $("#register-lastNameTwo-field").val(obj.LastName2);
                $("#register-dateofbirth-field").val(formatDateEN(obj.DateOfBirth));
                //$("#register-mpi-field").val(obj.SSNLast4);
                $("#register-ssn-field").val(obj.SSNLast4);
                $("#register-phone-field").val(obj.PhoneNumber);
                //$("#register-phonealt-field").val(obj.SSNLast4);
                $("#register-email-field").val(obj.Email);
                //$("#register-emailalt-field").val(obj.SSNLast4);
                $("#register-optin-field").attr('checked', obj.OptIn);
                $("#register-emailalt-field").val(obj.Email2);
                $("#register-phonealt-field").val(obj.PhoneNumber2);                
                $("#register-mpi-field").val(obj.MPI);
            } else {

                if (data.code > 0) {
                    swal({
                        title: "Advertencia!",
                        text: data.message,
                        type: "warning",
                        confirmButtonText: "OK"
                    });
                }
                else {
                    swal({
                        title: "Error!",
                        text: data.message,
                        type: "error",
                        confirmButtonText: "OK"
                    });
                }


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

    //Section for load table History
    $.ajax({
        url: urlGetEnrollmentHistoryByPersonId,
        dataType: 'json',
        data: {
            "PersonId": MemberId
        },
        type: 'POST',
        async: true,
        success: function (data) {
            //console.log(data);
            if (data.recordsTotal == 0) $('#NoDataHistory').removeAttr('hidden');
            if (data.code === 0) {
                if (data.recordsTotal > 0) {
                    //LoadTable
                    LoadTableSearchHistory('#tbHistorySearch', data.records);
                    //Scroll into table only show if table width is bigger than div container
                    PutCustomScrollIntoDataTable('#tbHistorySearch');
                }
            }
        }
    });
}

function LoadTableSearchHistory(tableSection, JSonData) {
    //i18next.changeLanguage("@Session[SessionHelper.LANGUAGE_SESSION_KEY]");

    $(tableSection).DataTable({
        "language": {
            "decimal": ".",
            "sEmptyTable": i18next.t("datatable_empty"),
            "sInfo": i18next.t("datatable_info"),
            "sLengthMenu": i18next.t("datatable_LengthMenu"),
            "oPaginate": {
                "sFirst": i18next.t("datatable_first"),
                "sPrevious": i18next.t("datatable_previous"),
                "sNext": i18next.t("datatable_next"),
                "sLast": i18next.t("datatable_last")
            }
        },
        //responsive: {
        //    details: {
        //        renderer: function (api, rowIdx, columns) {
        //            var data = $.map(columns, function (col, i) {
        //                return col.hidden ?
        //                    '<tr  data-dt-row="' + col.rowIndex + '" data-dt-column="' + col.columnIndex + '">' +
        //                    '<td><b>' + col.title + ':' + '</b></td> ' +
        //                    '<td>' + col.data + '</td>' +
        //                    '</tr>' :
        //                    '';
        //            }).join('');

        //            return data ?
        //                $('<table/>').append(data) :
        //                false;
        //        }
        //    }
        //},
        //"autoWidth": true,
        //"scrollX": true,
        "paging": false,
        //"ordering": true,
        //"processing": true, // for show progress bar
        "filter": false, // this is for disable filter (search box)
        //"orderMulti": false, // for disable multiple column at once
        data: JSonData,
        //"columnDefs":
        //    [
        //        {
        //            "targets": [0],
        //            "visible": true,
        //            "orderable": false
        //        }
        //    ],
        "columns": [
            //{ "title": '<span  data-i18n="Enrollment_User">Usuario</span>', "data": "MCOModifiedBy", "name": "MCOModifiedBy" },
            { "title": '<span  data-i18n="Enrollment_ChangeSource">Origen del Cambio</span>', "data": "MCOModifiedSource", "name": "MCOModifiedSource" },
            {
                title: '<span  data-i18n="Enrollment_ChangeDate">Fecha del Cambio</span>', data: null, render: function (data, type, row) {
                    return formatDateEN(row.CreatedOn);
                }
            },
            {
                title: '<span  data-i18n="Enrollment_MCO">MCO</span>', data: null, class: "bolder", render: function (data, type, row) {
                    if (row.MCO.CarrierName !== null) {
                        return row.MCO.CarrierName;
                    } else
                        return '';
                }
            },
            {
                title: '<span  data-i18n="Enrollment_PMG">PMG</span>', data: null, class: "bolder", render: function (data, type, row) {
                    console.log(row.PMG.PmgName);
                    if (row.PMG.PmgName !== null) {
                        return row.PMG.PmgName;
                    } else
                        return '';
                }
            },
            {
                title: '<span  data-i18n="Enrollment_PCP">PCP</span>', data: null, class: "bolder", render: function (data, type, row) {
                    if (row.PCP.FullName !== null) {
                        return row.PCP.FullName;
                    } else
                        return '';
                }
            },
            //{
            //    title: 'PDF', data: null, class: "bolder", render: function (data, type, row) {
            //        if (row.filePDF !== '' && row.filePDF !== null) {
            //            return '<a style="line-height:0.1" data-i18n="SearchPerson_Header_View" class="btn btn-info GoEnrollmentPage" href="#" id="SeePDF' + row.Id + '" onClick="seePDF(\'' + row.filePDF + '\');"></a>';
            //            //return "<a style='line-height:0.1' data-i18n='SearchPerson_Header_View' class='btn btn-info GoEnrollmentPage' href='#' id='SeePDF" + row.Id + "' onClick='seePDF(\' " + row.filePDF + "\');\"></a>";
            //            //return "<a style='line-height:0.1' class='btn btn-info GoEnrollmentPage' href='#' id='SeePDF" + row.Id + "' onClick='seePDF('" + row.filePDF + "');>" + i18next.t('SearchPerson_Header_View') + "</a>";
            //        } else
            //            return '';
            //    }
            //},
            {
                title: '<span  data-i18n="Enrollment_Status">Estatus</span>', data: null, class: "bolder", render: function (data, type, row) {
                    //return '<span  data-i18n="Enrollment_Processing">En proceso</span>';
                    if (row.Status !== '' && row.Status !== null) {
                        return row.Status.BusinessStatus;
                    } else {
                        return '-';
                    }
                }
            }
        ]
    });
    i18next.changeLanguage(i18next.language);
}

function PutCustomScrollIntoDataTable(myTableId) {
    //if (window.innerWidth >= 1000)
    //    $(myTableId).parent().mCustomScrollbar({
    //        axis: "x",
    //        theme: "dark-thin",
    //        autoExpandScrollbar: true,
    //        advanced: { autoExpandHorizontalScroll: true }
    //    });
    //else
    //    $(myTableId).mCustomScrollbar({
    //        axis: "x",
    //        theme: "dark-thin",
    //        autoExpandScrollbar: true,
    //        advanced: { autoExpandHorizontalScroll: true }
    //    });
}

function seePDF(FilePDF) {
    var request = new Object();
    request.Name = FilePDF;

    $.ajax({
        url: urlGetPDF,
        dataType: 'json',
        data: request,
        type: 'POST',
        async: true,
        success: function (data) {
            if (data.code === 0) {
                $('#ifrFile').attr('src', 'data:application/pdf;base64,' + data.content);

                $('#modalFile').dialog({
                    modal: true,
                    title: 'Archivo',
                    width: 1024,
                    height: 800,
                    resizable: true,
                    show: true,
                    hide: true,
                    buttons: {
                        "Cerrar": function () {
                            $('#modalFile').dialog("close");
                        }
                    }
                });

                $('#modalFile').dialog("open");
            }
        }
    });
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

function formatPhoneNumber(input) {
    if (input) {
        input = input.replace(/\D/g, '');

        input = input.substring(0, 10);

        var size = input.length;
        if (size == 0) {
            input = input;
        } else if (size < 4) {
            input = '(' + input;
        } else if (size < 7) {
            input = '(' + input.substring(0, 3) + ') ' + input.substring(3, 6);
        } else {
            input = '(' + input.substring(0, 3) + ') ' + input.substring(3, 6) + '-' + input.substring(6, 10);
        }
        return input;
    }
    else {
        return "";
    }
}

function cleanPhoneNumber(phone) {
    phone = phone.replace(/\D/g, ''); 
    return phone;
}

$("#register-phone-field").on("keyup", function (event) {
    phone = $(this).val();
    phone = cleanPhoneNumber(phone);
    phone = formatPhoneNumber(phone);
    $(this).val(phone);

});

$("#register-phonealt-field").on("keyup", function (event) {
    phone = $(this).val();
    phone = cleanPhoneNumber(phone);
    phone = formatPhoneNumber(phone);
    $(this).val(phone);

});