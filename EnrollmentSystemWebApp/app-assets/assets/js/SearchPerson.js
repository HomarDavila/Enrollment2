$(document).ready(function () {
    LoadPage();

    $(document).on("click", ".stv-radio-button", function () {
        var selected = $(this).attr('value');
        $('.search-form').css('display', 'none');
        $('#' + selected).css('display', 'block');
    });

    $("#btnSearchMPI").click(function () {
        if ($("#txtMPI").val()) {
            GetPeopleByFilters();
        }else{
            swal({
                title: i18next.t('common_warning'),
                text: i18next.t('SearchPerson_Validation_One'),
                type: "warning",
                confirmButtonText: "OK"
            });
        }
    });

    $("#btnSearch").click(function () {
        if (UnlessTwoOfFive()) {
            if ($("#txtSSN4").val().length == 0 || $("#txtSSN4").val().length == 4) {
                GetPeopleByFilters();
            } else {
                swal({
                title: i18next.t('common_warning'),
                text: i18next.t('SearchPerson_Validation_Two'),
                type: "warning",
                confirmButtonText: "OK"
            });
            }
        } else {
            swal({
                title: i18next.t('common_warning'),
                text: i18next.t('SearchPerson_Validation_Three'),
                type: "warning",
                confirmButtonText: "OK"
            });
        }
    });

    $("#btnClean").click(function () {
        $("#txtSSN4").val('');
        $("#txtBirthday").val('');
        $("#txtFirstName").val('');
        $("#txtLastName").val('');
        $("#txtSecondLastName").val('');
        $("#txtBirthday").val('');
    });

    // Evaluate if two of five fields are filled
    function UnlessTwoOfFive() {
        var count = 0;
        if ($("#txtSSN4").val())
            count += 1;
        if ($("#txtBirthday").val())
            count += 1;
        if ($("#txtFirstName").val())
            count += 1;
        if ($("#txtLastName").val())
            count += 1;
        if ($("#txtSecondLastName").val())
            count += 1;
        return count >= 2;
    }

    $(document).on("click", ".toggle", function () {
        var PersonId = $(this).attr('id').replace('toggle', '');
        var tableID = "#tbPeople" + PersonId;
        var expandable = '#expand' + PersonId;
        var labelExpandable = '#labelsearch' + PersonId;
        if ($(this).is(':checked')) {
            $(expandable).css("height", "auto");
            $(expandable).css("border-bottom", "1px solid #ccc");
            $(labelExpandable).addClass('clickeable');

            var rowCount = $(tableID + ' tr').length - 1;
            console.log(rowCount);
            //if (rowCount == 0) {
            //    $.ajax({
            //        url: urlGetApplicationMembers,
            //        dataType: 'json',
            //        beforeSend: function () {
            //            start = (new Date()).getTime();
            //            $.blockUI({ message: '<div class="icon-spinner9 icon-spin icon-lg"></div>', timeout: 60000, overlayCSS: { backgroundColor: "#000000", opacity: .8, cursor: "wait" }, css: { border: 0, padding: 0, backgroundColor: "transparent" } });
            //        },
            //        complete: function () {
            //            end = (new Date()).getTime();
            //            var total = end - start;
            //            $.unblockUI();
            //        },
            //        data: {
            //            PersonId: PersonId
            //        },
            //        type: 'POST',
            //        async: true,
            //        success: function (data) {
            //            if (data.code == 0) {
            //                if (data.recordsTotal > 0) {
            //                    //LoadTable
            //                    LoadTable(tableID, data.records);
            //                } else {
            //                    swal({
            //                        title: "Success!",
            //                        text: data.message,
            //                        type: "success",
            //                        confirmButtonText: "OK"
            //                    });
            //                }
            //            } else {
            //                swal({
            //                    title: "Error!",
            //                    text: data.message,
            //                    type: "error",
            //                    confirmButtonText: "OK"
            //                });
            //            }
            //        },
            //        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //            swal({
            //                title: "Error!",
            //                text: textStatus,
            //                type: "error",
            //                confirmButtonText: "OK"
            //            });
            //        }
            //    });
            //}
        } else {
            $(expandable).css("height", "0");
            $(expandable).css("border-bottom", "none");
            $(labelExpandable).removeClass('clickeable');
        }
    });

    $('body').on('click', '.GoEnrollmentPage', function () {
        var ApplicationMemberID = $(this).attr("id");
        pathSource = urlEnrollmentPage;
        $.redirect(pathSource,
            {
                "ApplicationMemberID": ApplicationMemberID
            });
    });

    var $startpicker = $('#txtBirthday').pickadate({
        max: true,
        selectMonths: true,
        selectYears: 70,
        closeOnClear: true,
        format: 'mm/dd/yyyy',
        formatSubmit: 'yyyy-mm-dd',
        hiddenPrefix: 'standardDate__',
    });
    $picker = $startpicker.pickadate('picker');

});

function LoadPage() {
    $('.toggle').attr('checked', false);
    $('#btn1').attr('checked', true);
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

function GetPeopleByFilters() {
    $("#results-div").html('');
    $.ajax({
        url: urlGetPeopleByFilters,
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
        data: BuildRequest(),
        type: 'POST',
        async: true,
        success: function (data) {
            console.log(data);
            if (data.code == 0) {
                if (data.recordsTotal > 0) {
                    if (data.recordsTotal >= 200) {
                        swal({
                            title: i18next.t('common_info'),
                            text: i18next.t('common_ExcessDataMember'),
                            type: "info",
                            confirmButtonText: "OK"
                        });
                    }
                    $.each(data.records, function (i, item) {
                        item.FirstName = (!item.FirstName) ? '' : item.FirstName;
                        item.FirstLastName = (!item.FirstName) ? '' : item.FirstLastName;
                        item.SecondLastName = (!item.SecondLastName) ? '' : item.SecondLastName;
                        //Build Html Section

                        if (data.recordsTotal >= 199) {
                            swal({
                                title: i18next.t('common_info'),
                                text: i18next.t('common_ExcessDataMember'),
                                type: "info",
                                confirmButtonText: "OK"
                            });
                        }

                        var concat = getResultHtml().split('-n').join(item.Id)
                            .replace("fullnameValue", item.FirstName + " " + item.FirstLastName + " " + item.SecondLastName)
                            .replace("mpiValue", item.MPIShort)
                            .replace("ssn4Value", item.Last4SSN)
                            .replace("birthdayValue", formatDateEN(item.DateOfBirth))
                            .replace("contactNameValue", item.Family.ContactFirstName + " " + item.Family.ContactFirstLastName + " " + item.Family.ContactSecondLastName)
                            .replace("memberNameValue", item.FirstName + " " + item.FirstLastName + " " + item.SecondLastName)
                            .replace("applicationNumberValue", item.Family.ApplicationNumber)
                            .replace("zipCodeValue", item.Family.ResidenceAddressZipCode)
                            .replace("MemberId",item.Id);
                        $("#results-div").append(concat);
                        //LoadTable
                        //var tableID = "#tbPeople" + item.Id;
                        //LoadTable(tableID, item.Family.Members);
                        //if (item.Applications !== null) {
                            
                        //}
                    });
                    i18next.changeLanguage(i18next.language);
                } else {
                    swal({
                        title: i18next.t('common_success'),
                        text: data.message,
                        type: "success",
                        confirmButtonText: "OK"
                    });
                }
            } else {
                swal({
                    title: i18next.t('common_warning'),
                    text: i18next.t('NoData'),
                    type: "warning",
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
}

function BuildRequest() {
    var MPI = null;
    var Last4SSN = null;
    var DateOfBirth = null;
    var FirstName = null;
    var FirtLastName = null;
    var SecondLastName = null;

    if ($("#btn2").is(':checked')) {
        MPI = $("#txtMPI").val();
    } else {
        Last4SSN = $("#txtSSN4").val();
        $("input[name=standardDate__birthdate_submit]").each(function () {
            DateOfBirth = this.value;
        });
        FirstName = $("#txtFirstName").val();
        FirtLastName = $("#txtLastName").val();
        SecondLastName = $("#txtSecondLastName").val();
    }
    
    var obj = new Object();
    obj.MPI = MPI;
    obj.Last4SSN = Last4SSN;        
    if (DateOfBirth !== null)
    {
        var parts = DateOfBirth.split('-');
        var mydate = new Date(parts[0], parts[1] - 1, parts[2]);
        obj.DateOfBirth = mydate.toJSON();
    };
    obj.FirstName = FirstName;
    obj.FirtLastName = FirtLastName;
    obj.SecondLastName = SecondLastName;

    return obj;
}

function getResultHtml() {
    //var htmlConcat = '<input id="toggle-n" class="toggle" type="checkbox"><label id="labelsearch-n" class="label-search" for="toggle-n">fullnameValue</label><div id="expand-n" class="expand"> <section class="expandable"> <div class="row"> <div class="form-group col-md-4"> <label for="txtMPI-n" data-i18n="SearchPerson_MPI">MPI:</label> <div class="position-relative has-icon-left"> <input id="txtMPI-n" class="form-control" placeholder="Enter Text" value="mpiValue" disabled/> <div class="form-control-position"> <i class="icon-qrcode"></i> </div></div></div><div class="form-group col-md-4"> <label for="txtSSN4-n" data-i18n="SearchPerson_SSN4">SSN (Últimos 4 dígitos):</label> <div class="position-relative has-icon-left"> <input id="txtSSN4-n" class="form-control" placeholder="Enter Text" value="ssn4Value" disabled/> <div class="form-control-position"> <i class="icon-qrcode"></i> </div></div></div><div class="form-group col-md-4"> <label for="txtBirthday-n" data-i18n="SearchPerson_Birthday">' + i18next.t("SearchPerson_Birthday") + ':</label> <div class="position-relative has-icon-left"> <input id="txtBirthday-n" class="form-control" placeholder="Mm/dd/yyyy" value="birthdayValue" disabled/> <div class="form-control-position"> <i class="icon-calendar"></i> </div></div></div></div><div class="row"  style="margin-bottom: 10px;"> <div class="col-md-12"> <table id="tbPeople-n" class="table table-hover table-bordered dt-responsive nowrap dataex-html5-export" width="100%" cellspacing="0"> <thead> <tr> <th data-i18n="SearchPerson_Header_View">Ver</th> <th data-i18n="SearchPerson_Header_Contact_Name">Nombre de Contacto</th> <th data-i18n="SearchPerson_Header_Member_Name">Beneficiario</th> <th data-i18n="SearchPerson_Header_Application_Number">N\u00famero de Aplicaci\u00f3n</th> </tr></thead> </table> </div></div></section></div>';
    //var htmlConcat = '<input id="toggle-n" class="toggle" type="checkbox"><label id="labelsearch-n" class="label-search" for="toggle-n">fullnameValue</label><div id="expand-n" class="expand"> <section class="expandable"> <div class="row"> <div class="form-group col-md-4"> <label for="txtMPI-n" data-i18n="SearchPerson_MPI">MPI:</label> <div class="position-relative has-icon-left"> <input id="txtMPI-n" class="form-control" placeholder="Enter Text" value="mpiValue" disabled/> <div class="form-control-position"> <i class="icon-qrcode"></i> </div></div></div><div class="form-group col-md-4"> <label for="txtSSN4-n" data-i18n="SearchPerson_SSN4">SSN (Últimos 4 dígitos):</label> <div class="position-relative has-icon-left"> <input id="txtSSN4-n" class="form-control" placeholder="Enter Text" value="ssn4Value" disabled/> <div class="form-control-position"> <i class="icon-qrcode"></i> </div></div></div><div class="form-group col-md-4"> <label for="txtBirthday-n" data-i18n="SearchPerson_Birthday">' + i18next.t("SearchPerson_Birthday") + ':</label> <div class="position-relative has-icon-left"> <input id="txtBirthday-n" class="form-control" placeholder="Mm/dd/yyyy" value="birthdayValue" disabled/> <div class="form-control-position"> <i class="icon-calendar"></i> </div></div></div></div><div class="row"><div class="form-group col-md-4"> <label for="txtContactName-n" data-i18n="SearchPerson_ContactName">Nombre de Contacto:</label> <div class="position-relative has-icon-left"> <input id="txtContactName-n" class="form-control" placeholder="Enter Text" value="contactNameValue" disabled/> <div class="form-control-position"> <i class="icon-qrcode"></i> </div></div></div><div class="form-group col-md-4"> <label for="txtMemberName-n" data-i18n="SearchPerson_MemberName">Nombre de Beneficiario:</label> <div class="position-relative has-icon-left"> <input id="txtMemberName-n" class="form-control" placeholder="Enter Text" value="memberNameValue" disabled/> <div class="form-control-position"> <i class="icon-qrcode"></i> </div></div></div><div class="form-group col-md-4"> <label for="txtApplicationNumber-n" data-i18n="SearchPerson_ApplicationNumber">Número de Aplicación:</label> <div class="position-relative has-icon-left"> <input id="txtApplicationNumber-n" class="form-control" placeholder="Enter Text" value="applicationNumberValue" disabled/> <div class="form-control-position"> <i class="icon-qrcode"></i> </div></div></div></div>' + "<a style='line-height:0.1; padding: 1rem 1.8rem;' data-i18n='SearchPerson_Header_View' class='btn btn-info GoEnrollmentPage' href='#' id='MemberId';>Ver</a>"+'</section></div>';
    var htmlConcat = '<input id="toggle-n" class="toggle" type="checkbox"><label id="labelsearch-n" class="label-search" for="toggle-n">fullnameValue</label><div id="expand-n" class="expand"> <section class="expandable"> <div class="row"> <div class="form-group col-md-4"> <label for="txtMPI-n" data-i18n="SearchPerson_MPI">MPI:</label> <div class="position-relative has-icon-left"> <input id="txtMPI-n" class="form-control" placeholder="Enter Text" value="mpiValue" disabled/> <div class="form-control-position"> <i class="icon-qrcode"></i> </div></div></div><div class="form-group col-md-4"> <label for="txtSSN4-n" data-i18n="SearchPerson_SSN4">SSN (Últimos 4 dígitos):</label> <div class="position-relative has-icon-left"> <input id="txtSSN4-n" class="form-control" placeholder="Enter Text" value="ssn4Value" disabled/> <div class="form-control-position"> <i class="icon-qrcode"></i> </div></div></div><div class="form-group col-md-4"> <label for="txtBirthday-n" data-i18n="SearchPerson_Birthday">' + i18next.t("SearchPerson_Birthday") + ':</label> <div class="position-relative has-icon-left"> <input id="txtBirthday-n" class="form-control" placeholder="Mm/dd/yyyy" value="birthdayValue" disabled/> <div class="form-control-position"> <i class="icon-calendar"></i> </div></div></div></div><div class="row"><div class="form-group col-md-4"> <label for="txtContactName-n" data-i18n="SearchPerson_ContactName">Nombre de Contacto:</label> <div class="position-relative has-icon-left"> <input id="txtContactName-n" class="form-control" placeholder="Enter Text" value="contactNameValue" disabled/> <div class="form-control-position"> <i class="icon-qrcode"></i> </div></div></div><div class="form-group col-md-4"> <label for="txtMemberName-n" data-i18n="SearchPerson_MemberName">Nombre de Beneficiario:</label> <div class="position-relative has-icon-left"> <input id="txtMemberName-n" class="form-control" placeholder="Enter Text" value="memberNameValue" disabled/> <div class="form-control-position"> <i class="icon-qrcode"></i> </div></div></div><div class="form-group col-md-4"> <label for="txtApplicationNumber-n" data-i18n="SearchPerson_ApplicationNumber">Número de Aplicación:</label> <div class="position-relative has-icon-left"> <input id="txtApplicationNumber-n" class="form-control" placeholder="Enter Text" value="applicationNumberValue" disabled/> <div class="form-control-position"> <i class="icon-qrcode"></i> </div></div></div></div> <div class="row"><div class="form-group col-md-4"> <label for="txtZipCode-n" data-i18n="SearchPerson_ZipCode">Código ZIP:</label> <div class="position-relative has-icon-left"> <input id="txtZipCode-n" class="form-control" placeholder="Enter Text" value="zipCodeValue" disabled/> <div class="form-control-position"> <i class="icon-qrcode"></i> </div></div></div></div><div>' + "<a style='line-height:0.1; padding: 1rem 1.8rem;' data-i18n='SearchPerson_Header_View' class='btn btn-info GoEnrollmentPage' href='#' id='MemberId';>Ver</a>" +'</section></div>';
    return htmlConcat;
}

function LoadTable(tableSection, JSonData) {
    i18next.changeLanguage("@Session[SessionHelper.LANGUAGE_SESSION_KEY]");
    $(tableSection).DataTable({
        "language": {
            "decimal": ".",
            "sEmptyTable": i18next.t("datatable_empty"),
            "sInfo": i18next.t("datatable_info"),
            "sLengthMenu": i18next.t("datatable_LengthMenu"),
            "oPaginate": {
                "sFirst":  i18next.t("datatable_first"),
                "sPrevious": i18next.t("datatable_previous"),
                "sNext": i18next.t("datatable_next"),
                "sLast": i18next.t("datatable_last")
            }
        },
        "ordering": true,
        "processing": true, // for show progress bar
        "filter": false, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        data: JSonData,
        "columnDefs":
            [
                {
                    "targets": [0],
                    "visible": true,
                    "orderable": false
                }
            ],
        "columns": [
            {
                title: i18next.t('SearchPerson_Header_View'), data: null, render: function (data, type, row) {
                    var PersonId = tableSection.replace('#tbPeople', '');
                    return "<a style='line-height:0.1;' data-i18n='SearchPerson_Header_View' class='btn btn-info GoEnrollmentPage' href='#' id='" + row.Id + "';></a>";
                }
            },
            { "title": i18next.t('SearchPerson_Header_Contact_Name'), "data": "Family.ContactFullName", "name": "Nombre de Contacto", "autoWidth": true },
            { "title": i18next.t('SearchPerson_Header_Member_Name'), "data": "MemberFullName", "name": "Beneficiario" },
            { "title": i18next.t('SearchPerson_Header_Application_Number'), "data": "Family.ApplicationNumber", "name": "N\u00famero de Aplicaci\u00f3n" }
        ],
        dom: 'lBfrtip',
        buttons: [
            //{
            //    extend: 'colvis',
            //    exportOptions: {
            //        columns: [1, 2, 3]
            //    }
            //},
            //{
            //    extend: 'copyHtml5',
            //    exportOptions: {
            //        columns: [1, 2, 3]
            //    }
            //},
            //{
            //    extend: 'excelHtml5',
            //    exportOptions: {
            //        columns: [1, 2, 3]
            //    }
            //},
            //{
            //    extend: 'csvHtml5',
            //    exportOptions: {
            //        columns: [1, 2, 3]
            //    }
            //},
            //{
            //    extend: 'pdfHtml5',
            //    exportOptions: {
            //        columns: [1, 2, 3]
            //    }
            //},
            //{
            //    extend: 'print',
            //    exportOptions: {
            //        columns: [1, 2, 3]
            //    }
            //}
        ]
    });

    //$(".buttons-html5").addClass("btn-primary");
    //$(".buttons-colvis").addClass("btn-primary");
    //$(".buttons-print").addClass("btn-primary");
    //$(".buttons-columnVisibility").addClass("btn-primary"); 
    i18next.changeLanguage("@Session[SessionHelper.LANGUAGE_SESSION_KEY]");

}