$(function () {

    $('body').on('click', '.radio-group .radioDiv', function (e) {
        $('body').parent().find('.radioDiv').removeClass('selected');
        $(this).addClass('selected');
        var val = $(this).attr('data-value');
        CardIdSelect = val;
        $('#btnApplyChange').removeAttr('disabled');
        $('.StepThree').removeAttr('disabled');

        //get id's for change
        var div = $(this);
        CardUpdatePcpIdValue = div.find('#CardUpdatePcpId').val();
        PersonId = div.find('#PersonId').val();
        SpecialityId = div.find('#SpecialityId').val();
        PrimaryMedicalGroupId = div.find('#PrimaryMedicalGroupId').val();
        $('#PCPDFullName').html(div.find('#PCPNameFull').html());
        $("#imgPCP").attr("src", div.find('#PCPImageUrl').attr('src'));
        $('#PCPDNPINumber').html(div.find('#PCPNumberNPI').html());
    });

    //$("#btnPrint").click(function () {
    //    var ficha = document.getElementById('fet5');
    //    var ventimp = window.open(' ', 'popimpr');
    //    ventimp.document.write(ficha.innerHTML);
    //    ventimp.document.close();
    //    ventimp.print();
    //    ventimp.close();
    //});

    $('body').on('click', '#StepOne', function (e) {
        //alert('hola');
    });

    $('body').on('click', '#StepTwo', function (e) {
        //alert('two');
    });

    //$('#cbSpecialityPCP').on('change', function (e) {
    //    ChangeAddress();
    //});

    //$('#cbPmgPCP').on('change', function (e) {
    //    ChangeAddress();
    //});

    $('#btnBuscarDireccion').on('click', function (e) {
        ChangeAddress();
    });

    $('#rtbFirstMedicalPlan').on('click', function (e) {
        $("#rtbFirstMedicalPlan").prop("checked", false);
        ShowmodalCart(1);
    });

    $('#rtbMMM').on('click', function (e) {
        $("#rtbMMM").prop("checked", false);
        ShowmodalCart(2);
    });

    $('#rtbMolinaHealthCare').on('click', function (e) {
        $("#rtbMolinaHealthCare").prop("checked", false);
        ShowmodalCart(3);
    });

    $('#rtbPlanMenonita').on('click', function (e) {
        $("#rtbPlanMenonita").prop("checked", false);
        ShowmodalCart(4);
    });

    $('#rtbTripleS').on('click', function (e) {
        $("#rtbTripleS").prop("checked", false);
        ShowmodalCart(5);
    });

    $('body').on('click', '.btnApplyChange', function (e) {
        trSelected = $(this).closest('tr');
        dirIndex = trSelected.find('.dirIndex').val();

        $("#divrtbFirstMedicalPlan").hide();
        $("#divrtbMMM").hide();
        $("#divrtbMolinaHealthCare").hide();
        $("#divrtbPlanMenonita").hide();
        $("#divrtbTripleS").hide();

        var solouno = 0;
        var plan = 0;
        CurrentPersonAddressSelected = CurrentPersonSelected.find(function (objeto) { return objeto.Id == dirIndex });
        $.each(CurrentPersonAddressSelected.AvailableManagedCareOrganizations, function (j, elem2) {
            if (elem2.Id == 1) {
                $("#divrtbFirstMedicalPlan").show();
                solouno += 1;
                plan = 1;
            }
            if (elem2.Id == 2) {
                $("#divrtbMMM").show();
                solouno += 1;
                plan = 2;
            }
            if (elem2.Id == 3) {
                $("#divrtbMolinaHealthCare").show();
                solouno += 1;
            }
            if (elem2.Id == 4) {
                $("#divrtbPlanMenonita").show();
                solouno += 1;
                plan = 4;
            }
            if (elem2.Id == 5) {
                $("#divrtbTripleS").show();
                solouno += 1;
                plan = 5;
            }
        });

        if (solouno === 1) {
            ShowmodalCart(plan);
        }

    });

    LoadPage();

    $('#cbPmg').empty().append('<option value="" selected>(SELECCIONE)</option>');
    $.ajax({
        url: urlGetAllPmg,
        dataType: 'json',
        data: {
            ShowForChangeEnrollmentProcess: false
        },
        type: 'GET',
        async: true,
        success: function (response) {
            if (response.recordsTotal == 0)
                swal({
                    title: i18next.t('common_Info'),
                    text: i18next.t('NoData'),
                    type: "info",
                    confirmButtonText: "OK"
                });
            else {
                var data = response.records;

                data = $.map(data, function (obj) {
                    obj.id = obj.id || obj.Id;
                    obj.text = obj.text || obj.PmgName;

                    return obj;
                });

                $("#cbPmg").select2({
                    placeholder: i18next.t('AllCboSelectAll'),
                    allowClear: true,
                    selectOnClose: false,
                    data: data,
                    sorter: function (data) {
                        return data.sort(function (a, b) {
                            if (a.text > b.text) {
                                return 1;
                            }
                            if (a.text < b.text) {
                                return -1;
                            }
                            return 0;
                        });
                    }
                });
            }
        }
    });

    $('#cbMunicipality').empty().append('<option value="" selected>(SELECCIONE)</option>');
    $.ajax({
        url: urlGetMunicipality,
        dataType: 'json',
        type: 'GET',
        async: true,
        success: function (response) {
            if (response.recordsTotal == 0)
                swal({
                    title: i18next.t('common_Info'),
                    text: i18next.t('NoData'),
                    type: "info",
                    confirmButtonText: "OK"
                });
            else {
                var data = response.records;

                data = $.map(data, function (obj) {
                    obj.id = obj.id || obj.Id;
                    obj.text = obj.text || obj.Name;

                    return obj;
                });

                $("#cbMunicipality").select2({
                    placeholder: i18next.t('AllCboSelectAll'),
                    allowClear: true,
                    selectOnClose: false,
                    data: data,
                    sorter: function (data) {
                        return data.sort(function (a, b) {
                            if (a.text > b.text) {
                                return 1;
                            }
                            if (a.text < b.text) {
                                return -1;
                            }
                            return 0;
                        });
                    }
                });
            }
        }
    });

    $('#cbSpeciality').empty().append('<option value="" selected>(SELECCIONE)</option>');
    $.ajax({
        url: urlGetAllSpeciality,
        dataType: 'json',
        type: 'GET',
        data: {
            ShowForChangeEnrollmentProcess: false
        },
        async: true,
        success: function (response) {
            if (response.recordsTotal == 0)
                swal({
                    title: i18next.t('common_Info'),
                    text: i18next.t('NoData'),
                    type: "info",
                    confirmButtonText: "OK"
                });
            else {
                var data = response.records;

                if (i18next.language === 'es') {
                    data = $.map(data, function (obj) {
                        obj.id = obj.id || obj.Id;
                        obj.text = obj.text || obj.Name;

                        return obj;
                    });
                } else {
                    data = $.map(data, function (obj) {
                        obj.id = obj.id || obj.Id;
                        obj.text = obj.text || obj.Nombre;

                        return obj;
                    });
                }

                $("#cbSpeciality").select2({
                    placeholder: i18next.t('AllCboSelectAll'),
                    allowClear: true,
                    selectOnClose: false,
                    data: data,
                    sorter: function (data) {
                        return data.sort(function (a, b) {
                            if (a.text > b.text) {
                                return 1;
                            }
                            if (a.text < b.text) {
                                return -1;
                            }
                            return 0;
                        });
                    }
                });
            }
        }
    });

    $('body').on('click', '.MCO', function (e) {
        var checkBoxes = $("input[name=MCO\\[\\]]");
        checkBoxes.attr("checked", !checkBoxes.attr("checked"));
    });

    //click button btnSearch
    $('body').on('click', '#btnSearch', function (e) {
        $('.StepThree').prop('disabled', true);

        $("#results-div").html('');
        $('#DinamicSection').html('');
        $.ajax({
            url: urlGetPcpWithFiltersToList,
            dataType: 'json',
            beforeSend: function () {
                start = (new Date()).getTime();
                $.blockUI({ message: '<div class="icon-spinner9 icon-spin icon-lg"></div>', timeout: 60000000, overlayCSS: { backgroundColor: "#000000", opacity: .8, cursor: "wait" }, css: { border: 0, padding: 0, backgroundColor: "transparent" } });
            },
            complete: function () {
                end = (new Date()).getTime();
                var total = end - start;
                $.unblockUI();
            },
            data: {
                entityRequest: BuildRequestPcp()
            },
            type: 'POST',
            async: true,
            success: function (data) {
                console.log(data);
                var divContainChange = $('#DinamicSection');
                divContainChange.html('');
                $('.pagination').empty();
                $('.pagination').removeData("twbs-pagination");
                $('.pagination').unbind("page");
                if (data.code == 0) {
                    if (data.recordsTotal >= 199) {
                        swal({
                            title: i18next.t('common_Info'),
                            text: i18next.t('common_ExcessData'),
                            type: "info",
                            confirmButtonText: "OK"
                        }).then(function () {
                            //       $('#btnPreviusSerach').click();
                        });
                    }
                    if (data.recordsTotal > 0) {
                        jsondatamemory = data.records;
                        var totalRows = parseInt(data.records.length);
                        var maxRow_x_page = 10;
                        var numTotal_pages = (totalRows % maxRow_x_page == 0) ? parseInt(totalRows / maxRow_x_page) : parseInt(totalRows / maxRow_x_page) + 1;
                        if (totalRows > 0) {
                            var obj = $('.pagination').twbsPagination({
                                totalPages: numTotal_pages,
                                visiblePages: 5,
                                first: '<p class="FirstPage"><i class="fa fa-angle-double-left"></i></p>',
                                next: '<p class="NextPage"><i class="fa fa-angle-right"></i></p>',
                                prev: '<p class="BackPage"><i class="fa fa-angle-left"></i></p>',
                                last: '<p class="LastPage"><i class="fa fa-angle-double-right"></i></p>',
                                onPageClick: function (event, page) {
                                    var htmlConcat = '';
                                    var page = parseInt(page);
                                    var min = maxRow_x_page * (page - 1) + 1;
                                    var max = maxRow_x_page * (page);
                                    $.each(data.records, function (i, item) {
                                        if ((i + 1) >= min && (i + 1) <= max) {
                                            //Build Html Section
                                            var PMGName = '';
                                            var firstOMGId = 0;
                                            $.each(item.AvailablePrimaryMedicalGroups, function (i, el) {
                                                PMGName += el.PmgName + ' - ';
                                                if (firstOMGId === 0) {
                                                    firstOMGId = el.Id;
                                                }
                                            });
                                            //if (item.AvailablePrimaryMedicalGroups.length < 3) {
                                            //    $.each(item.AvailablePrimaryMedicalGroups, function (i, el) {
                                            //        PMGName += el.PmgName + ' - ';
                                            //    });
                                            //} else {
                                            //    for (var j = 0; j < 2; j++) {
                                            //        if (j === 1) {
                                            //            PMGName += item.AvailablePrimaryMedicalGroups[j].PmgName + ' ... - ';
                                            //        } else {
                                            //            PMGName += item.AvailablePrimaryMedicalGroups[j].PmgName + ' - ';
                                            //        }
                                            //    }
                                            //}

                                            PMGName = PMGName.substr(0, PMGName.lastIndexOf('-'));

                                            if (PMGName.length > 50) {
                                                PMGName = PMGName.substr(0, 45);
                                                PMGName += ' ...';
                                            }

                                            var dataValue = 'dataValue_' + ('0000' + (i + 1)).slice(-4);

                                            //var maleStr = 'Male';
                                            //var femaleStr = 'Female'
                                            //var MaleFemaleStr = '';
                                            //if (item.Person?.GenderId > 0 && item.Person?.GenderId < 3) {
                                            //    MaleFemaleStr = (item.Person.GenderId === 1) ? maleStr : femaleStr;
                                            //}

                                            var imageURL = '';
                                            var SpecialityStr = '';
                                            SpecialityStr = item.Speciality.Name.split("/")[1].replace(/\s/g, '');
                                            SpecialityStr = SpecialityStr.replace(/\//g, '');
                                            //if (item.Speciality.ShowItOnChangeEnrollmentProcess == true) {
                                            //    if (MaleFemaleStr) {
                                            //        imageURL = 'url' + MaleFemaleStr + SpecialityStr;
                                            //    } else {
                                            //        imageURL = 'urlGeneric' + SpecialityStr;
                                            //    }
                                            //} else {
                                            //    if (MaleFemaleStr) {
                                            //        imageURL = 'urlGeneric' + MaleFemaleStr + 'Medic';
                                            //    } else {
                                            //        imageURL = 'urlGenericMedic';
                                            //    }
                                            //}
                                            if (item.Speciality.ShowItOnChangeEnrollmentProcess == true) {
                                                imageURL = 'urlGeneric' + SpecialityStr;
                                            } else {
                                                imageURL = 'urlGenericMedic';
                                            }
                                            image = window[imageURL];

                                            var DinamicCheck1 = '<span class="checkmarkRed2"><a class="checkmarkClose2"></a></span>';
                                            var DinamicCheck2 = '<span class="checkmarkRed2"><a class="checkmarkClose2"></a></span>';
                                            var DinamicCheck3 = '<span class="checkmarkRed2"><a class="checkmarkClose2"></a></span>';
                                            var DinamicCheck4 = '<span class="checkmarkRed2"><a class="checkmarkClose2"></a></span>';
                                            var DinamicCheck5 = '<span class="checkmarkRed2"><a class="checkmarkClose2"></a></span>';

                                            $.each(item.AvailableManagedCareOrganizations, function (j, el2) {
                                                if (el2.Id == 1) DinamicCheck1 = '<span class="checkmarkGreen"></span>';
                                                if (el2.Id == 2) DinamicCheck2 = '<span class="checkmarkGreen"></span>';
                                                if (el2.Id == 3) DinamicCheck3 = '<span class="checkmarkGreen"></span>';
                                                if (el2.Id == 4) DinamicCheck4 = '<span class="checkmarkGreen"></span>';
                                                if (el2.Id == 5) DinamicCheck5 = '<span class="checkmarkGreen"></span>';
                                            });

                                            htmlConcat += GetHtmlForCards()
                                                .replace("UrlImage", image)
                                                .replace("FullName", item.Person.FullName)
                                                .replace("SpeciallityName", item.Speciality.Name)
                                                .replace("PMGName", PMGName)
                                                .replace("NPINumber", item.Person.NPI)
                                                .replace("DinamicCheck1", DinamicCheck1)
                                                .replace("DinamicCheck2", DinamicCheck2)
                                                .replace("DinamicCheck3", DinamicCheck3)
                                                .replace("DinamicCheck4", DinamicCheck4)
                                                .replace("DinamicCheck5", DinamicCheck5)
                                                .replace("DataValue", dataValue)
                                                .replace("PersonIdValue", item.PersonId)
                                                .replace("SpecialityIdValue", item.SpecialityId)
                                                .replace("PrimaryMedicalGroupIdValue", firstOMGId)
                                                .replace("CardUpdatePmgValue", PMGName);
                                        }
                                    });
                                    divContainChange.html(htmlConcat);
                                }
                            });
                        }
                    } else {
                        swal({
                            title: "Success!",
                            text: data.message,
                            type: "success",
                            confirmButtonText: "OK"
                        });
                    }
                } else {
                    if (data.code > 0) {
                        swal({
                            title: i18next.t('common_Info'),
                            text: data.message,
                            type: "info",
                            confirmButtonText: "OK"
                        }).then(function () {
                            $('#btnPreviusSerach').click();
                        });
                    }
                    else {
                        swal({
                            title: "Error!",
                            text: "Error",
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
    });


    $('body').on('click', '.StepThree', function (e) {
        var oRequestPCP = new Object();
        oRequestPCP = new Object();
        oRequestPCP.PCPId = PersonId;
        oRequestPCP.ShowForChangeEnrollmentProcess = false;

        $('#cbSpecialityPCP').empty();
        $.ajax({
            url: urlGetAllSpecialityPCP,
            dataType: 'json',
            type: 'POST',
            data: {
                request: oRequestPCP
            },
            async: true,
            success: function (response) {
                if (response.recordsTotal == 0)
                    swal({
                        title: i18next.t('common_Info'),
                        text: i18next.t('NoData'),
                        type: "info",
                        confirmButtonText: "OK"
                    });
                else {
                    var data = response.records;

                    if (i18next.language === 'es') {
                        data = $.map(data, function (obj) {
                            obj.id = obj.id || obj.Id;
                            obj.text = obj.text || obj.Name;

                            return obj;
                        });
                    } else {
                        data = $.map(data, function (obj) {
                            obj.id = obj.id || obj.Id;
                            obj.text = obj.text || obj.Nombre;

                            return obj;
                        });
                    }

                    $("#cbSpecialityPCP").select2({
                        selectOnClose: false,
                        data: data,
                        sorter: function (data) {
                            return data.sort(function (a, b) {
                                if (a.text > b.text) {
                                    return 1;
                                }
                                if (a.text < b.text) {
                                    return -1;
                                }
                                return 0;
                            });
                        }
                    });

                    $("#cbSpecialityPCP").val(SpecialityId);
                }
            }
        });

        $('#cbPmgPCP').empty();
        $.ajax({
            url: urlGetAllPmgPCP,
            dataType: 'json',
            type: 'POST',
            data: {
                request: oRequestPCP
            },
            async: true,
            success: function (response) {
                if (response.recordsTotal == 0)
                    swal({
                        title: i18next.t('common_Info'),
                        text: i18next.t('NoData'),
                        type: "info",
                        confirmButtonText: "OK"
                    });
                else {
                    var data = response.records;

                    data = $.map(data, function (obj) {
                        obj.id = obj.id || obj.Id;
                        obj.text = obj.text || obj.PmgName;

                        return obj;
                    });

                    $("#cbPmgPCP").select2({
                        selectOnClose: false,
                        data: data,
                        sorter: function (data) {
                            return data.sort(function (a, b) {
                                if (a.text > b.text) {
                                    return 1;
                                }
                                if (a.text < b.text) {
                                    return -1;
                                }
                                return 0;
                            });
                        }
                    });

                    $("#cbPmgPCP").val(PrimaryMedicalGroupId);
                }
            }
        });

        LoadAddress();
    });

    //Send Email
    $('#chkSendEmail').click(function (e) {
        if ($(this).is(':checked'))
            $('#txtEmail').removeAttr("disabled");
        else {
            $("#txtEmail").attr("disabled", "disabled");
            $('#txtEmail').val('');
        }
    });
    //click button btnApplyPcp
    $('body').on('click', '#btnApplyPcp', function (e) {
        if ($('#chkSendEmail').is(':checked'))
            if (validarEmail($('#txtEmail').val()) === false) {
                swal({
                    title: i18next.t('common_warning'),
                    text: i18next.t('Email_valid'),
                    type: "warning",
                    confirmButtonText: "OK"
                });
                return;
            }

        var onlyOne = 0;
        if ($("#dMCOFirstMedical").is(":visible") && $("#MCOFirstMedical").is(":checked")) {
            onlyOne += 1;
            UpdateMcoId = $("#MCOFirstMedical").val();
        }
        if ($("#dMCOMMM").is(":visible") && $("#MCOMMM").is(":checked")) {
            onlyOne += 1;
            UpdateMcoId = $("#MCOMMM").val();
        }
        if ($("#dMCOMolinaHealthCare").is(":visible") && $("#MCOMolinaHealthCare").is(":checked")) {
            onlyOne += 1;
            UpdateMcoId = $("#MCOMolinaHealthCare").val();
        }
        if ($("#dMCOPlanMenonita").is(":visible") && $("#MCOPlanMenonita").is(":checked")) {
            onlyOne += 1;
            UpdateMcoId = $("#MCOPlanMenonita").val();
        }
        if ($("#dMCOTripleS").is(":visible") && $("#MCOTripleS").is(":checked")) {
            onlyOne += 1;
            UpdateMcoId = $("#MCOTripleS").val();
        }

        if (onlyOne !== 1) {
            swal({
                title: i18next.t('common_warning'),
                text: i18next.t('Enrollment_SelectNewMCO'),
                type: "warning",
                confirmButtonText: "OK"
            });
            return;
        }

        swal({
            title: i18next.t('common_Sure'),
            text: i18next.t('common_Confirm'),
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: i18next.t('common_ButtonYes'),
            cancelButtonText: i18next.t('common_ButtonNo')
        }).then(function (isConfirm) {
            if (isConfirm.value) {
                $.ajax({
                    url: urlCreatePDF,
                    dataType: 'json',
                    data: {
                        entityRequest: BuildRequestPDF(UpdateMcoId, UpdatePmgId, UpdatePcpId)
                    },
                    type: 'POST',
                    async: true,
                    success: function (data) {
                        if (data.code == 0) {
                            FilePDFChangeEnrollment = data.filePDF;
                        }
                    }
                });

                $.ajax({
                    url: urlChangePersonMco,
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
                        request: BuildRequestPcpApply()
                    },
                    type: 'POST',
                    async: true,
                    success: function (data) {
                        if (data.code == 0) {
                            $.ajax({
                                url: urlSendConfirmationEmail,
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
                                type: 'POST',
                                async: true,
                                data: {
                                    entityRequest: BuildRequestEmail()
                                },
                                success: function (data) {
                                    if (data.code == 0) {
                                        swal({
                                            title: i18next.t('common_succesfull'),
                                            text: i18next.t('Enrollment_sucessfull_register'),
                                            type: "success",
                                            confirmButtonText: "OK"
                                        }).then(function () {
                                            $.redirect(UrlThisView);
                                        });
                                    } else {
                                        sweetAlert({
                                            title: i18next.t('common_succesfull'),
                                            text: i18next.t('Enrollment_sucessfull_register'),
                                            type: "success",
                                            confirmButtonText: "OK"
                                        }).then(function () {
                                            $.redirect(UrlThisView);
                                        });
                                    }
                                },
                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    swal({
                                        title: "Éxito",
                                        text: "Se registro correctamente el cambio.",
                                        type: "success",
                                        confirmButtonText: "OK"
                                    });
                                }
                            });
                        } else {
                            if (data.code > 0) {
                                swal({
                                    title: i18next.t('common_warning'),
                                    text: data.message,
                                    type: "warning",
                                    confirmButtonText: "OK"
                                });
                            }
                            else {
                                swal({
                                    title: "Error",
                                    text: "No se pudo registrar la transacci\u00f3n.",
                                    type: "error",
                                    confirmButtonText: "OK"
                                });
                            }
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        swal({
                            title: "Error",
                            text: textStatus,
                            type: "error",
                            confirmButtonText: "OK"
                        });
                    }
                });
            }
        });
    });
});

function LoadPage() {
    //CallMembers();
}

function ShowmodalCart(pMCOId) {
    var oMCO = CurrentPersonAddressSelected.AvailableManagedCareOrganizations.find(function (objeto) { return objeto.Id == pMCOId });
    UpdateMcoId = oMCO.Id;
    UpdateMco = oMCO.Name;
    var oPCP = CurrentPersonSelected.find(function (objeto) { return objeto.Id == dirIndex });

    UpdatePcpId = oPCP.Id;
    UpdatePcp = $('#PCPDFullName').html();

    document.getElementById('ActualMco').innerHTML = ActualMco;
    document.getElementById('ActualPmg').innerHTML = ActualPcp;
    document.getElementById('ActualPcp').innerHTML = ActualPmg;

    if (1 === pMCOId) {
        $("#dMCOFirstMedical").show();
        $("#MCOFirstMedical").attr('checked', 'checked');
    } else {
        $("#MCOFirstMedical").removeAttr('checked');
        $("#dMCOFirstMedical").hide();
    }

    if (2 === pMCOId) {
        $("#dMCOMMM").show();
        $("#MCOMMM").attr('checked', 'checked');
    } else {
        $("#MCOMMM").removeAttr('checked');
        $("#dMCOMMM").hide();
    }

    if (4 === pMCOId) {
        $("#dMCOPlanMenonita").show();
        $("#MCOPlanMenonita").attr('checked', 'checked');
    } else {
        $("#MCOMolinaHealthCare").removeAttr('checked');
        $("#dMCOPlanMenonita").hide();
    }

    if (5 === pMCOId) {
        $("#dMCOTripleS").show();
        $("#MCOTripleS").attr('checked', 'checked');
    } else {
        $("#MCOTripleS").removeAttr('checked');
        $("#dMCOTripleS").hide();
    }

    document.getElementById('UpdatePcp').innerHTML = UpdatePcp;
    document.getElementById('UpdatePmg').innerHTML = UpdatePmg;
    $("#btnCloseAseguradora").click();
    $("#btnOpenmodalCart").click();
}

function LoadAddress() {
    $('#PersonDirections tbody').empty();
    var entityRequest = new Object();
    entityRequest.PersonId = PersonId;
    entityRequest.SpecialityId = SpecialityId;
    entityRequest.PmgId = PrimaryMedicalGroupId;
    entityRequest.MunicipalityId = 0;

    $.ajax({
        url: urlGetPrimaryCarePhysicianDetailWithFiltersToList,
        dataType: 'json',
        beforeSend: function () {
            start = (new Date()).getTime();
            $.blockUI({ message: '<div class="icon-spinner9 icon-spin icon-lg"></div>', timeout: 600000, overlayCSS: { backgroundColor: "#000000", opacity: .8, cursor: "wait" }, css: { border: 0, padding: 0, backgroundColor: "transparent" } });
        },
        complete: function () {
            end = (new Date()).getTime();
            var total = end - start;
            $.unblockUI();
        },
        data: {
            entityRequest: entityRequest
        },
        type: 'POST',
        async: true,
        success: function (data) {
            if (data.code == 0) {
                if (data.recordsTotal > 0) {
                    let tbody = ($('#PersonTBody'));
                    let htmlConcat = '';

                    let faTimesCheck1 = '<i class="fa fa-times fa-2x"></i>';
                    let faTimesCheck2 = '<i class="fa fa-times fa-2x"></i>';
                    let faTimesCheck3 = '<i class="fa fa-times fa-2x"></i>';
                    let faTimesCheck4 = '<i class="fa fa-times fa-2x"></i>';
                    let faTimesCheck5 = '<i class="fa fa-times fa-2x"></i>';

                    CurrentPersonSelected = data.records;
                    $.each(data.records, function (i, elem) {
                        faTimesCheck1 = '<i class="fa fa-times fa-2x"></i>';
                        faTimesCheck2 = '<i class="fa fa-times fa-2x"></i>';
                        faTimesCheck3 = '<i class="fa fa-times fa-2x"></i>';
                        faTimesCheck4 = '<i class="fa fa-times fa-2x"></i>';
                        faTimesCheck5 = '<i class="fa fa-times fa-2x"></i>';

                        $.each(elem.AvailableManagedCareOrganizations, function (j, elem2) {
                            if (elem2.Id == 1) faTimesCheck1 = '<i class="fa fa-check fa-2x"></i>';
                            if (elem2.Id == 2) faTimesCheck2 = '<i class="fa fa-check fa-2x"></i>';
                            if (elem2.Id == 3) faTimesCheck3 = '<i class="fa fa-check fa-2x"></i>';
                            if (elem2.Id == 4) faTimesCheck4 = '<i class="fa fa-check fa-2x"></i>';
                            if (elem2.Id == 5) faTimesCheck5 = '<i class="fa fa-check fa-2x"></i>';
                        });

                        if (elem.Phone != null) {
                            elem.Phone = formatPhoneNumber(cleanPhoneNumber(elem.Phone));
                        } else {
                            elem.Phone = '';
                        }

                        if (elem.AddressLineTwo != null || elem.AddressLineTwo > 0) {
                            elem.AddressLineOne = elem.AddressLineOne + ', ' + elem.AddressLineTwo
                        }

                        htmlConcat += GetHtmlForDirections()
                            .replace("dirIndexValue", elem.Id)
                            .replace("AddressLineOneAddressLineTwo", elem.AddressLineOne)
                            .replace("MunicipalityName", elem.Municipality === null ? "" : elem.Municipality.Name)
                            .replace("State", elem.State)
                            .replace("ZipCode", elem.ZipCode)
                            .replace("Phone", elem.Phone)
                            .replace("faTimesCheck1", faTimesCheck1)
                            .replace("faTimesCheck2", faTimesCheck2)
                            .replace("faTimesCheck3", faTimesCheck3)
                            .replace("faTimesCheck4", faTimesCheck4)
                            .replace("faTimesCheck5", faTimesCheck5);

                    });
                    tbody.html(htmlConcat);
                } else {
                    swal({
                        title: "Success!",
                        text: data.message,
                        type: "success",
                        confirmButtonText: "OK"
                    });
                }
            } else {
                if (data.code > 0) {
                    swal({
                        title: i18next.t('common_Info'),
                        text: data.message,
                        type: "info",
                        confirmButtonText: "OK"
                    }).then(function () {
                        //$('#btnBackToFet4').click();
                        $('#PersonDirections tbody').empty();
                    });
                }
                else {
                    swal({
                        title: "Error!",
                        text: "Error",
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
}


function ChangeAddress() {
    SpecialityId = $('#cbSpecialityPCP').val();
    PrimaryMedicalGroupId = $('#cbPmgPCP').val();

    LoadAddress();
}

function CallMembers() {
    return $.ajax({
        type: 'GET',
        url: urlGetMembers,
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (data) {
            //LoadTable
            Member = data.records[0];
            //DisableCurrentMco(Member.MCOId);
            //console.log(data.records);
            //var table = '#tb_Members';
            //LoadTable(table, data.records);

            ActualMco = data.records[0].MCO.CarrierName;
            ActualMcoId = data.records[0].MCO.Id;
            ActualPcp = data.records[0].PCP.Person.FullName;
            ActualPcpId = data.records[0].PCP.Person.Id;
            ActualPmg = data.records[0].PMG.PmgName;
            ActualPmgId = data.records[0].PMG.Id;
            $('#MCO').text(ActualMco);
            $('#PCP').text(ActualPcp);
            $('#PMG').text(ActualPmg);
        },
        error: function (jqXhr, textStatus, errorThrown) {
            //console.log(jqXhr);
            //console.log(textStatus);
            //console.log(errorThrown);
        }
    });
}

function BuildRequestPcp() {
    var lst_McoId = [];
    var PmgId = null;
    var PcpFullName = null;
    var SpecialityId = null;
    var NPI = null;
    var MunicipalityId = null;

    $(".MCO").each(function (index, element) {
        var $element = jQuery(this);
        if ($element.is(':checked')) {
            lst_McoId.push($element.attr('id'));
        }
    });

    PmgId = $("#cbPmg").val();
    PcpFullName = $("#txtPhName").val();
    SpecialityId = $("#cbSpeciality").val();
    NPI = $("#txtNPI").val();
    MunicipalityId = $("#cbMunicipality").val();

    var entityRequest = new Object();
    entityRequest.lst_McoId = lst_McoId;
    entityRequest.PmgId = PmgId;
    entityRequest.PcpFullName = PcpFullName;
    entityRequest.SpecialityId = SpecialityId;
    entityRequest.NPI = NPI;
    entityRequest.MunicipalityId = MunicipalityId;
    entityRequest.ShowForChangeEnrollmentProcess = false;

    return entityRequest;
}

function BuildRequestEmail() {
    var request = new Object();

    if ($('#chkSendEmail').is(':checked')) {
        request.Contact = true;
    } else {
        request.Contact = false;
    }
    request.Email = $('#txtEmail').val();
    request.NameTo = 'Admin';
    request.NameFile = FilePDFChangeEnrollment;
    return request;
}

function BuildRequestPDF(UpdateMcoId, UpdatePmgId, UpdatePcpId) {
    var request = new Object();

    request.MemberId = MemberIdentify;
    request.McoId = UpdateMcoId;
    request.PmgId = UpdatePmgId;
    request.PcpId = UpdatePcpId;

    return request;

}
//request for PcpApply
function BuildRequestPcpApply() {
    var request = new Object();

    var McoId = UpdateMcoId;
    var PcpId = UpdatePcpId;
    var PmgId = UpdatePmgId;
    var Origin = 30;
    var UserName = "Admin";

    request.MemberId = MemberIdentify;
    request.McoId = McoId;
    request.PcpId = PcpId;
    request.PmgId = PmgId;
    request.Origin = Origin;
    request.UserName = UserName;
    //TODO: estaba true
    request.IgnoreValidationRules = false;
    request.IsJustCause = false;
    request.JustCause = null
    return request;
}

function GetHtmlForCards() {
    //return '<div class="col-sm-6"> <div class="panel"> <div class="panel-body p-t-10 CardBody radioDiv PersonCard" data-value="DataValue"> <div class="media-main"> <a class="pull-left"><img class="thumb-lg img-circle bx-s" src="UrlImage" id="PCPImageUrl" alt=""/> </a> <div class="info"> <h4 class="name" id="PCPNameFull">FullName</h4> <div class="row"> <div class="col-md-6"> <p class="text-muted">Speciality: <span class="bolder">SpeciallityName</span></p><p class="text-muted">PMG: <span class="bolder">PMGName</span></p></div><div class="col-md-6"> <label class="containercheck"> First Medical Plan <input id="1" type="checkbox" checked="checked" disabled="disabled"> DinamicCheck1 </label> <label class="containercheck"> MMM Multi health <input id="2" type="checkbox" checked="checked" disabled="disabled"> DinamicCheck2 </label> <label class="containercheck"> Molina HealthCare of PR <input id="3" type="checkbox" checked="checked" disabled="disabled"> DinamicCheck3 </label>  <label class="containercheck"> Plan Menonita <input id="4" type="checkbox" checked="checked" disabled="disabled"> DinamicCheck4 </label> <label class="containercheck"> TripleS <input id="5" type="checkbox" checked="checked" disabled="disabled"> DinamicCheck5 </label> </div></div></div></div><div class="clearfix"></div><hr style="margin-bottom: .2rem;"> <p class="text-muted">NPI: <span class="bolder" id="PCPNumberNPI">NPINumber</span></p><input type="hidden" id="CardUpdateMcoId" name="CardUpdateMcoId" value="CardUpdateMcoIdValue"><input type="hidden" id="CardUpdateMco" name="CardUpdateMco" value="CardUpdateMcoValue"><input type="hidden" id="CardUpdatePcpId" name="CardUpdatePcpId" value="CardUpdatePcpIdValue"><input type="hidden" id="CardUpdatePcp" name="CardUpdatePcp" value="CardUpdatePcpValue"><input type="hidden" id="CardUpdatePmgId" name="CardUpdatePmgId" value="CardUpdatePmgIdValue"><input type="hidden" id="CardUpdatePmg" name="CardUpdatePmg" value="CardUpdatePmgValue"><input type="hidden" id="PersonId" name="PersonId" value="PersonIdValue"><input type="hidden" id="SpecialityId" name="SpecialityId" value="SpecialityIdValue"><input type="hidden" id="PrimaryMedicalGroupId" name="PrimaryMedicalGroupId" value="PrimaryMedicalGroupIdValue"> </div></div></div>';
    //return '    <div class="col-md-12 col-lg-6 p-1">        <div class="card CardBody radioDiv">            <div class="card-header">                <h4 class="name" id="PCPNameFull">FullName</h4>            </div>            <div class="card-body" data-value="DataValue">                <div class="row">                    <div class="col-sm-5 col-md-4 col-lg-4">                        <div class="text-center">                            <a><img class="thumb-lg img-circle" src="UrlImage" id="PCPImageUrl" alt="" /> </a>                        </div>                    </div>                    <div class="col-sm-7 col-md-4 col-lg-4">                        <p class="text-muted">Speciality: <span class="bolder">SpeciallityName</span></p>                        <p class="text-muted">PMG: <span class="bolder">PMGName</span></p>                    </div>                    <div class="col-sm-12 col-md-4 col-lg-4 pt-3 pt-md-0">                        <label class="containercheck"> First Medical Plan <input id="1" type="checkbox" checked="checked" disabled="disabled"> DinamicCheck1 </label>                        <label class="containercheck"> MMM Multi health <input id="2" type="checkbox" checked="checked" disabled="disabled"> DinamicCheck2 </label>                        <label class="containercheck"> Molina HealthCare of PR <input id="3" type="checkbox" checked="checked" disabled="disabled"> DinamicCheck3 </label>                        <label class="containercheck"> Plan Menonita <input id="4" type="checkbox" checked="checked" disabled="disabled"> DinamicCheck4 </label>                        <label class="containercheck"> TripleS <input id="5" type="checkbox" checked="checked" disabled="disabled"> DinamicCheck5 </label>                    </div>                </div>                <div class="clearfix"></div>                <hr style="margin-bottom: .2rem;">                <p class="text-muted">                    NPI: <span class="bolder" id="PCPNumberNPI">NPINumber</span>                </p>                <input type="hidden" id="CardUpdateMcoId" name="CardUpdateMcoId" value="CardUpdateMcoIdValue">                <input type="hidden" id="CardUpdateMco" name="CardUpdateMco" value="CardUpdateMcoValue">                <input type="hidden" id="CardUpdatePcpId" name="CardUpdatePcpId" value="CardUpdatePcpIdValue">                <input type="hidden" id="CardUpdatePcp" name="CardUpdatePcp" value="CardUpdatePcpValue">                <input type="hidden" id="CardUpdatePmgId" name="CardUpdatePmgId" value="CardUpdatePmgIdValue">                <input type="hidden" id="CardUpdatePmg" name="CardUpdatePmg" value="CardUpdatePmgValue">                <input type="hidden" id="PersonId" name="PersonId" value="PersonIdValue">                <input type="hidden" id="SpecialityId" name="SpecialityId" value="SpecialityIdValue">                <input type="hidden" id="PrimaryMedicalGroupId" name="PrimaryMedicalGroupId" value="PrimaryMedicalGroupIdValue">            </div>        </div>    </div>';
    return '    <div class="col-md-12 col-lg-6 p-1">        <div class="card CardBody radioDiv">            <div class="card-header">                <h4 class="name" id="PCPNameFull">FullName</h4>            </div>            <div class="card-body" data-value="DataValue">                <div class="row">                    <div class="col-sm-5 col-md-4 col-lg-4">                        <div class="text-center">                            <a><img class="thumb-lg img-circle" src="UrlImage" id="PCPImageUrl" alt="" /> </a>                        </div>                    </div>                    <div class="col-sm-7 col-md-4 col-lg-4">                        <p class="text-muted">Speciality: <span class="bolder">SpeciallityName</span></p>                        <p class="text-muted">PMG: <span class="bolder">PMGName</span></p>                    </div>                    <div class="col-sm-12 col-md-4 col-lg-4 pt-3 pt-md-0">                        <label class="containercheck"> First Medical Plan <input id="1" type="checkbox" checked="checked" disabled="disabled"> DinamicCheck1 </label>                        <label class="containercheck"> MMM Multi Health <input id="2" type="checkbox" checked="checked" disabled="disabled"> DinamicCheck2 </label>                        <label class="containercheck"> Molina HealthCare of PR <input id="3" type="checkbox" checked="checked" disabled="disabled"> DinamicCheck3 </label>                        <label class="containercheck"> Plan Menonita <input id="4" type="checkbox" checked="checked" disabled="disabled"> DinamicCheck4 </label>                        <label class="containercheck"> TripleS <input id="5" type="checkbox" checked="checked" disabled="disabled"> DinamicCheck5 </label>                    </div>                </div>                <div class="clearfix"></div>                <hr style="margin-bottom: .2rem;">                <p class="text-muted" style="display: none;>                    NPI: <span class="bolder" id="PCPNumberNPI">NPINumber</span>                </p>                <input type="hidden" id="CardUpdateMcoId" name="CardUpdateMcoId" value="CardUpdateMcoIdValue">                <input type="hidden" id="CardUpdateMco" name="CardUpdateMco" value="CardUpdateMcoValue">                <input type="hidden" id="CardUpdatePcpId" name="CardUpdatePcpId" value="CardUpdatePcpIdValue">                <input type="hidden" id="CardUpdatePcp" name="CardUpdatePcp" value="CardUpdatePcpValue">                <input type="hidden" id="CardUpdatePmgId" name="CardUpdatePmgId" value="CardUpdatePmgIdValue">                <input type="hidden" id="CardUpdatePmg" name="CardUpdatePmg" value="CardUpdatePmgValue">                <input type="hidden" id="PersonId" name="PersonId" value="PersonIdValue">                <input type="hidden" id="SpecialityId" name="SpecialityId" value="SpecialityIdValue">                <input type="hidden" id="PrimaryMedicalGroupId" name="PrimaryMedicalGroupId" value="PrimaryMedicalGroupIdValue">            </div>        </div>    </div>';
}

function GetHtmlForDirections() {
    return '<tr>'
        + '	<td style="display: none;">'
        + '     <input type="hidden" class="dirIndex" value="dirIndexValue">'
        + '	</td>'
        + '	<td>'
        + '		<br />'
        + '		<i class="fa fa-map-marker-alt fa-2x"></i>'
        + '     <br />'
        + '	</td>'
        + '	<td class="dontCenter">'
        + '	<div class="text-nowrap">'
        + '		<span>AddressLineOneAddressLineTwo</span><br />'
        + '		<span>MunicipalityName, State ZipCode</span><br />'
        + '		<span>Tel.: Phone</span>'
        + '	</div>'
        + '	</td>'
        + '	<td>'
        + '		<br />'
        + '		faTimesCheck1'
        + '     <br /> '
        + '	</td>'
        + '	<td>'
        + '		<br />'
        + '		faTimesCheck2'
        + '     <br /> '
        + '	</td>'
        + '	<td>'
        + '		<br />'
        + '		faTimesCheck3'
        + '     <br /> '
        + '	</td>'
        + '	<td>'
        + '		<br />'
        + '		faTimesCheck4'
        + '     <br /> '
        + '	</td>'
        + '	<td>'
        + '		<br />'
        + '		faTimesCheck5'
        + '     <br /> '
        + '	</td>'
        + '</tr>';
};

function LoadTable(tableSection, JSonData) {
    $(tableSection).DataTable({
        "ordering": false,
        "processing": true, // for show progress bar
        "filter": false, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        data: JSonData,
        "columnDefs":
            [
                {
                    "targets": [0],
                    "visible": true,
                    "orderable": true
                }
            ],
        "columns": [
            {
                title: "SSN"/*i18next.t('Expense_ExpenseType_Tag')*/, data: null, render: function (data, type, row) {
                    var checked = row.Id.toString() === MemberIdentify ? 'checked' : '';
                    var radio = '<label class="containerRadio">' + row.SSN + '<input type="radio" name="Member" id="' + row.Id + '" value="' + row.Id + '" ' + checked + '><span class="checkmarkRadio"></span></label>';
                    return radio;
                }
            },
            { "title": "Beneficiary"/*i18next.t('Expense_Client_Tag')*/, "data": "MemberFullName", "name": "MemberFullName", "autoWidth": true },
            {
                title: "Current PCP"/*i18next.t('Expense_ExpenseType_Tag')*/, data: null, render: function (data, type, row) {
                    ActualPcpId = row.PcpId;
                    ActualPcp = (row.PCP === null ? '<i style="color:red">No existe data para mostrar</i>' : row.PCP.Person.FullName);
                    return (row.PCP === null ? '<i style="color:red">No existe data para mostrar</i>' : row.PCP.Person.FullName);
                }
            },
            {
                title: "Current MCO"/*i18next.t('Expense_ExpenseType_Tag')*/, data: null, render: function (data, type, row) {
                    ActualMcoId = row.McoId;
                    ActualMco = (row.MCO === null ? '<i style="color:red">No existe data para mostrar</i>' : row.MCO.CarrierName);
                    return (row.MCO === null ? '<i style="color:red">No existe data para mostrar</i>' : row.MCO.CarrierName);
                }
            },
            {
                title: "Current PMG"/*i18next.t('Expense_ExpenseType_Tag')*/, data: null, render: function (data, type, row) {
                    ActualPmgId = row.PmgId;
                    ActualPmg = (row.PMG === null ? '<i style="color:red">No existe data para mostrar</i>' : row.PMG.PmgName);
                    return (row.PMG === null ? '<i style="color:red">No existe data para mostrar</i>' : row.PMG.PmgName);
                }
            }
        ],
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3]
                }
            }
        ]
    });

    $(".buttons-html5").hide();
}

function devuelvemco(valor) {
    switch (valor) {
        case 1: return "First Medical";
        case 2: return "MMM";
        case 3: return "Molina Health Care";
        case 4: return "Plan Menonita";
        case 5: return "Triple S";
        default: return "";
    }

}

function formatPhoneNumber(value) {
    if (value != null && value != 'NULL') {
        let newStr = "(";
        for (var i = 0; i <= value.length - 1; i++) {
            newStr += value.charAt(i);
            if (i == 2) {
                newStr += ") ";
            }
            if (i == 5) {
                newStr += "-";
            }
        }
        return newStr;
    }
    return "";
}

function cleanPhoneNumber(phone) {
    phone = phone.replace(/\s/g, '');
    phone = phone.replace(/\(/g, '');
    phone = phone.replace(/\)/g, '');
    phone = phone.replace(/\-/g, '');
    return phone;
}

$('#cbSpecialityPCP').on('change', function (event) {
    $('#PersonTBody').html('');
});

$('#cbPmgPCP').on('change', function (event) {
    $('#PersonTBody').html('');
});