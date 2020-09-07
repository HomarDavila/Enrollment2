var IdMember = 0;
var fileEnrollment;
var FilePDFChangeEnrollment = '';
var blnChangeEnabled = true;
var IdHistories = 0;
var IdStatus = 0;
var McoCancel = 0;
var PcpCancel = 0;
var PmgCancel = 0;

$(function () {
    $('.js-example-basic-multiple').select2();

    $('#btnBuscarDireccion').on('click', function (e) {
        UpdatePmg = $('#cbPmgPCP option:selected').text();
        UpdatePmgId = parseInt($('#cbPmgPCP').val());
        SpecialityId = parseInt($('#cbSpecialityPCP').val());
        LoadAddress();
    });
    $('#rtbFirstMedicalPlan').on('click', function (e) {
        $("#rtbFirstMedicalPlan").prop("checked", false);
        ShowmodalCart(1);
    });

    $('#rtbMMM').on('click', function (e) {
        $("#rtbMMM").prop("checked", false);
        ShowmodalCart(2);
    });

    //$('#rtbMolinaHealthCare').on('click', function (e) {
    //    $("#rtbMolinaHealthCare").prop("checked", false);
    //    ShowmodalCart(3);
    //});

    $('#rtbPlanMenonita').on('click', function (e) {
        $("#rtbPlanMenonita").prop("checked", false);
        ShowmodalCart(4);
    });

    $('#rtbTripleS').on('click', function (e) {
        $("#rtbTripleS").prop("checked", false);
        ShowmodalCart(5);
    });

    $('#divReasonJustCause').hide();
    $('#justCauseCheck').change(function () {
        cancelJustCause();
    });

    $('#cbJustCausa').change(function () {
       // debugger;
        var request = new Object();
        request.ReasonJustCauseId = $(this).val();

        if ($(this).val() !== "") {
            $.ajax({
                url: urlGetReasonJustCauseByID,
                dataType: 'json',
                data: request,
                type: 'POST',
                async: true,
                success: function (data) {
                    console.log(data);
                    if (data.code === 0) {
                        $('#txtCommentJustCausa').val(i18next.language === 'es' ? data.objeto.Descripcion : data.objeto.Description);
                    }
                }
                ,
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    swal({
                        title: "Error     ",
                        text: textStatus,
                        type: "error",
                        confirmButtonText: "OK"
                    });
                }
            });
        } else {
            $('#txtCommentJustCausa').val("");
        }
    });

    $('#cbJustCausa').empty().append('<option value="" selected>(SELECCIONE)</option>');
    $.ajax({
        url: urlGetReasonJustCause,
        dataType: 'json',
        type: 'GET',
        async: true,
        success: function (response) {
            if (response.recordsTotal === 0)
                swal({
                    title: i18next.t('common_info'),
                    text: i18next.t('NoData'),
                    type: "info",
                    confirmButtonText: "OK"
                });
            else {
                var data = response.records;

                if (i18next.language === 'es') {
                    data = $.map(data, function (obj) {
                        obj.id = obj.id || obj.Id;
                        obj.text = obj.text || obj.Razon;

                        return obj;
                    });

                    $("#cbJustCausa").select2({
                        placeholder: i18next.t('AllCboSelect'),
                        allowClear: true,
                        selectOnClose: false,
                        data: data
                    });
                } else {
                    data = $.map(data, function (obj) {
                        obj.id = obj.id || obj.Id;
                        obj.text = obj.text || obj.Reason;

                        return obj;
                    });

                    $("#cbJustCausa").select2({
                        placeholder: i18next.t('AllCboSelect'),
                        allowClear: true,
                        selectOnClose: false,
                        data: data
                    });
                }
                
            }
        }
    });

    //$("#cbJustCausa").select2({
    //    placeholder: i18next.t('SelectCboSelectSelect'),
    //    minimumInputLength: 0,
    //    allowClear: true,
    //    ajax: {
    //        url: urlGetReasonJustCause,
    //        dataType: 'json',
    //        async: true,
    //        processResults: function (data) {
    //            var results = [];
    //            if (data.recordsTotal == 0)
    //                swal({
    //                    title: "Info!",
    //                    text: "No se encontraron datos",
    //                    type: "info",
    //                    confirmButtonText: "OK"
    //                });
    //            else {
    //                $.each(data.records, function (index, item) {
    //                    results.push({
    //                        id: item.Id,
    //                        text: i18next.language === 'es' ? item.Razon : item.Reason
    //                    });
    //                });
    //            }
    //            return {
    //                results: results
    //            };
    //        }
    //    }
    //});

    LoadPage();
    //ChangeEnrollmentEnabled();

    //Send Email
    $('#chkSendEmail').click(function (e) {
        if ($(this).is(':checked'))
            $('#txtEmail').removeAttr("disabled");
        else {
            $("#txtEmail").attr("disabled", "disabled");
            $('#txtEmail').val('');
        }
    });

    //Send Phone
    $('#chkSendPhone').click(function (e) {
        if ($(this).is(':checked'))
            $('#txtPhone').removeAttr("disabled");
        else {
            $("#txtPhone").attr("disabled", "disabled");
            $('#txtPhone').val('');
        }
    });

    //$("#btnSave").click(function () {

    //    SendEnrollmentPeriod();

    //});
    /** Begin Fill Cbo's **/
    $(".cbMco").select2({
        minimumResultsForSearch: -1,
        placeholder: function () {
            console.log('sss');
            $(this).data('placeholder');
        },
        //placeholder: "Select a state",//i18next.t('AllCboSelectAll'),
        minimumInputLength: 0,
        allowClear: true,
        templateSelection: function (data) {
            //console.log(data);
            if (data.id === '') { // adjust for custom placeholder values
                return 'Custom styled placeholder text';
            }
            return data.text;
        },
        ajax: {
            delay: 150,
            url: urlGetAllMco,
            dataType: 'json',
            async: true,
            processResults: function (data, params) {
                var results = [];
                if (data.recordsTotal == 0)
                    swal({
                        title: i18next.t('common_info'),
                        text: i18next.t('NoData'),
                        type: "info",
                        confirmButtonText: "OK"
                    });
                else {
                    $.each(data.records, function (index, item) {
                        results.push({
                            id: item.Id,
                            text: item.CarrierName
                        });
                    });
                }

                return {
                    results: results
                };
            }
        }
    });

    //$(".cbPmg").select2({
    //    placeholder: i18next.t('AllCboSelectAll'),
    //    minimumInputLength: 0,
    //    allowClear: true,
    //    ajax: {
    //        delay: 150,
    //        url: urlGetAllPmg,
    //        dataType: 'json',
    //        async: true,
    //        processResults: function (data, params) {
    //            var results = [];
    //            if (data.recordsTotal == 0)
    //                swal({
    //                    title: "Info!",
    //                    text: "No se encontraron datos",
    //                    type: "info",
    //                    confirmButtonText: "OK"
    //                });
    //            else {
    //                $.each(data.records, function (index, item) {
    //                    results.push({
    //                        id: item.Id,
    //                        text: item.PmgName
    //                    });
    //                });
    //            }
    //            return {
    //                results: results
    //            };
    //            //return {
    //            //    results: $.map(data.records, function (obj) {
    //            //        return { id: obj.PmgId, text: obj.Name };
    //            //    })
    //            //};
    //        }
    //    }
    //});

    $('#cbPmg').empty().append('<option value="" selected>(SELECCIONE)</option>');
    $.ajax({
        url: urlGetAllPmg,
        dataType: 'json',
        type: 'GET',
        async: true,
        success: function (response) {
            if (response.recordsTotal == 0)
                swal({
                    title: i18next.t('common_info'),
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
                    data: data
                });
            }
        }
    });

    //$(".cbPhName").select2({
    //    placeholder: i18next.t('AllCboSelectAll'),
    //    minimumInputLength: 0,
    //    allowClear: true,
    //    ajax: {
    //        delay: 150,
    //        url: urlGetAllPcp + "/true",
    //        dataType: 'json',
    //        async: true,
    //        processResults: function (data, params) {
    //            var results = [];
    //            if (data.recordsTotal == 0)
    //                swal({
    //                    title: "Info!",
    //                    text: "No se encontraron datos",
    //                    type: "info",
    //                    confirmButtonText: "OK"
    //                });
    //            else {
    //                $.each(data.records, function (index, item) {
    //                    results.push({
    //                        id: item.Id,
    //                        text: item.FullName
    //                    });
    //                });
    //            }
    //            return {
    //                results: results
    //            };
    //        }
    //    }
    //});

    //$(".cbSpeciality").select2({
    //    placeholder: i18next.t('AllCboSelectAll'),
    //    minimumInputLength: 0,
    //    allowClear: true,
    //    ajax: {
    //        delay: 150,
    //        url: urlGetAllSpeciality + "/true",
    //        dataType: 'json',
    //        async: true,
    //        processResults: function (data) {
    //            var results = [];
    //            if (data.recordsTotal == 0)
    //                swal({
    //                    title: "Info!",
    //                    text: "No se encontraron datos",
    //                    type: "info",
    //                    confirmButtonText: "OK"
    //                });
    //            else {
    //                $.each(data.records, function (index, item) {
    //                    results.push({
    //                        id: item.Id,
    //                        text: i18next.language === 'es' ? item.Nombre : item.Name
    //                    });
    //                });
    //            }
    //            return {
    //                results: results
    //            };
    //        }
    //    }
    //});

    $('#cbSpeciality').empty().append('<option value="" selected>(SELECCIONE)</option>');
    $.ajax({
        url: urlGetAllSpeciality,
        dataType: 'json',
        type: 'GET',
        data: {
            ShowForChangeEnrollmentProcess: true
        },
        async: true,
        success: function (response) {
            if (response.recordsTotal == 0)
                swal({
                    title: i18next.t('common_info'),
                    text: i18next.t('NoData'),
                    type: "info",
                    confirmButtonText: "OK"
                });
            else {
                var data = response.records;

                //data = $.map(data, function (obj) {
                //    obj.id = obj.id || obj.Id;
                //    obj.text = obj.text || obj.Name;

                //    return obj;
                //});
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
                    data: data
                });
            }
        }
    });



    $(document).on("click", ".toggle", function () {
        var PcpPmgProviderId = $(this).attr('id').replace('toggle', '');
        var expandable = '#expand' + PcpPmgProviderId;
        var labelExpandable = '#labelsearch' + PcpPmgProviderId;
        if ($(this).is(':checked')) {
            $(expandable).css("height", "auto");
            $(expandable).css("border-bottom", "1px solid #ccc");
            $(labelExpandable).addClass('clickeable');
        } else {
            $(expandable).css("height", "0");
            $(expandable).css("border-bottom", "none");
            $(labelExpandable).removeClass('clickeable');
        }
    });

    $('body').on('click', '.SelectDirections', function (e) {
        $('#PersonTBody').html('');
        //$('#PCPDNPINumber').html('');

        PersonPcpId = CleanNumber($(this).attr('id'));
        UpdatePcpId = CleanNumber($(this).closest('.expand').attr('id'));
        var person = jsondatamemory.find(x => x.Id == UpdatePcpId);
        PersonPcp = person.Person.FullName;
        $('#PCPDFullName').html(PersonPcp);
        UpdatePmgId = person.AvailablePrimaryMedicalGroups[0].Id;
        SpecialityId = person.SpecialityId;
        $('#cbSpecialityPCP').empty();
        $.ajax({
            url: urlGetAllSpecialityPCP,
            dataType: 'json',
            type: 'GET',
            data: {
                PCPId: PersonPcpId
            },
            async: true,
            success: function (response) {
                console.log(response);
                var data = response.records;

                //data = $.map(data, function (obj) {
                //    obj.id = obj.id || obj.Id;
                //    obj.text = obj.text || obj.Name;

                //    return obj;
                //});
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
                    data: data
                });

                $("#cbSpecialityPCP").val(SpecialityId).trigger('change');
            }
        });

        $('#cbPmgPCP').empty();
        $.ajax({
            url: urlGetAllPmgPCP,
            dataType: 'json',
            type: 'GET',
            data: {
                PCPId: PersonPcpId
            },
            async: true,
            success: function (response) {
                //debugger;
                console.log(response);
                var data = response.records;

                data = $.map(data, function (obj) {
                    obj.id = obj.id || obj.Id;
                    obj.text = obj.text || obj.PmgName;

                    return obj;
                });

                $("#cbPmgPCP").select2({
                    selectOnClose: false,
                    data: data
                });
                $("#cbPmgPCP").val(UpdatePmgId).trigger('change');
                UpdatePmg = $('#cbPmgPCP :selected').text();
            }
        });
        $('#DirectionsModal').modal('show');
        LoadAddress();
    });

    $('body').on('click', '.btnApplyChange', function (e) {
        trSelected = $(this).closest('tr');
        dirIndex = trSelected.find('.dirIndex').val();

        $("#divrtbFirstMedicalPlan").hide();
        $("#divrtbMMM").hide();
        //$("#divrtbMolinaHealthCare").hide();
        $("#divrtbPlanMenonita").hide();
        $("#divrtbTripleS").hide();

        var solouno = 0;
        var plan = 0;
        CurrentPersonAddressSelected = CurrentPersonSelected.find(objeto => objeto.Id == dirIndex);
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
            //if (elem2.McoId == 3) {
            //    $("#divrtbMolinaHealthCare").show();
            //    solouno += 1;
            //}
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

        switch (Member.MCOId) {
            case 1:
                $("#divrtbFirstMedicalPlan").hide();
                break;
            case 2:
                $("#divrtbMMM").hide();
                break;
            //case 3:
            //    $("#divrtbMolinaHealthCare").hide();
            //    break;
            case 4:
                $("#divrtbPlanMenonita").hide();
                break;
            case 5:
                $("#divrtbTripleS").hide();
                break;
            default:
                break;
        }

        if (solouno === 1) {
            ShowmodalCart(plan);
        } else {
            $('#modalSelectAseguradora').modal('show');
        }

    });

    function LoadAddress() {
        //debugger;
        var entityRequest = new Object();
        entityRequest.PersonId = PersonPcpId;
        //entityRequest.SpecialityId = parseInt($("#cbSpecialityPCP").val());
        //entityRequest.PmgId = parseInt($("#cbPmgPCP").val());
        entityRequest.SpecialityId = SpecialityId;
        entityRequest.PmgId = UpdatePmgId;
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
                //console.log(data);
                if (data.code == 0) {
                    if (data.recordsTotal > 0) {
                        let tbody = ($('#PersonTBody'));
                        let htmlConcat = '';

                        let faTimesCheck1 = '<i class="fa fa-times fa-2x"></i>';
                        let faTimesCheck2 = '<i class="fa fa-times fa-2x"></i>';
                        //let faTimesCheck3 = '<i class="fa fa-times fa-2x"></i>';
                        let faTimesCheck4 = '<i class="fa fa-times fa-2x"></i>';
                        let faTimesCheck5 = '<i class="fa fa-times fa-2x"></i>';

                        CurrentPersonSelected = data.records;
                        $.each(data.records, function (i, elem) {
                            faTimesCheck1 = '<i class="fa fa-times fa-2x"></i>';
                            faTimesCheck2 = '<i class="fa fa-times fa-2x"></i>';
                            //let faTimesCheck3 = '<i class="fa fa-times fa-2x"></i>';
                            faTimesCheck4 = '<i class="fa fa-times fa-2x"></i>';
                            faTimesCheck5 = '<i class="fa fa-times fa-2x"></i>';

                            $.each(elem.AvailableManagedCareOrganizations, function (j, elem2) {
                                if (elem2.Id == 1) faTimesCheck1 = '<i class="fa fa-check fa-2x"></i>';
                                if (elem2.Id == 2) faTimesCheck2 = '<i class="fa fa-check fa-2x"></i>';
                                //if (elem2.McoId == 3) faTimesCheck3 = '<i class="fa fa-check fa-2x"></i>';
                                if (elem2.Id == 4) faTimesCheck4 = '<i class="fa fa-check fa-2x"></i>';
                                if (elem2.Id == 5) faTimesCheck5 = '<i class="fa fa-check fa-2x"></i>';
                            });

                            if (elem?.Phone?.length > 0) {
                                elem.Phone = formatPhoneNumber(elem.Phone);
                            } else {
                                elem.Phone = '';
                            }

                            if (elem.AddressLineTwo != null || elem.AddressLineTwo > 0) {
                                elem.AddressLineOne = elem.AddressLineOne + ', ' + elem.AddressLineTwo
                            }

                            let justMolina = false;
                            if (elem.AvailableManagedCareOrganizations.length === 1 && elem.AvailableManagedCareOrganizations[0].Id === 3) {
                                justMolina = true;
                            }
                            let mcoIds = elem.AvailableManagedCareOrganizations.map(x => x.Id);
                            let SelectButton = '<button type="button" class="btn btn-info SelectedDirection btnApplyChange" data-i18n="SelectCboSelectSelect">' + i18next.t('SelectCboSelectSelect') + '</button>';
                            if ((elem.AvailableManagedCareOrganizations.length === 1 && elem.AvailableManagedCareOrganizations[0].Id === Member.MCOId)
                                || (elem.AvailableManagedCareOrganizations.length === 2 && mcoIds.includes(Member.MCOId) && mcoIds.includes(3))
                                || parseInt(entityRequest.PmgId) === 3962
                                || justMolina == true) {
                                SelectButton = '<button type="button" class="btn btn-previous" disabled  data-i18n="SelectCboSelectSelect">' + i18next.t('SelectCboSelectSelect') + '</button>';
                            }

                            htmlConcat += GetHtmlForDirections()
                                .replace("dirIndexValue", elem.Id)
                                .replace("AddressLineOneAddressLineTwo", elem.AddressLineOne)
                                //.replace("AddressLineTwo", elem.AddressLineTwo)                      
                                .replace("MunicipalityName", elem.Municipality === null ? "" : elem.Municipality.Name)
                                .replace("State", elem.State)
                                .replace("ZipCode", elem.ZipCode)
                                .replace("Phone", elem.Phone)
                                .replace("faTimesCheck1", faTimesCheck1)
                                .replace("faTimesCheck2", faTimesCheck2)
                                //.replace("faTimesCheck3", faTimesCheck3)
                                .replace("faTimesCheck4", faTimesCheck4)
                                .replace("faTimesCheck5", faTimesCheck5)
                                .replace("SelectButton", SelectButton);

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
                            title: i18next.t('common_info'),
                            text: data.message,
                            type: "info",
                            confirmButtonText: "OK"
                        }).then(function () {
                            //$('#btnBackToFet4').click();
                            $('.icon-spinner9').hide();
                            $('.blockUI').hide();
                            $('#PersonDirections tbody').empty();
                        });
                    }
                    else {
                        swal({
                            title: "Error     ",
                            text: "Error",
                            type: "error",
                            confirmButtonText: "OK"
                        });
                    }
                }
            }
            ,
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //console.log(textStatus);
                swal({
                    title: "Error     ",
                    text: textStatus,
                    type: "error",
                    confirmButtonText: "OK"
                });
            }
        });
    }

    function ShowmodalCart(pMCOId) {
        //debugger
        $('#modalSelectAseguradora').modal('hide');
        var oMCO = CurrentPersonAddressSelected.AvailableManagedCareOrganizations.find(objeto => objeto.Id == pMCOId);
        UpdateMcoId = oMCO.Id;
        UpdateMco = oMCO.Name;

        UpdatePmgId = $('#cbPmgPCP').val();

        UpdatePcp = PersonPcp; //$('#PCPDFullName').html();

        document.getElementById('ActualMco').innerHTML = ActualMco;
        document.getElementById('ActualPmg').innerHTML = ActualPmg;
        document.getElementById('ActualPcp').innerHTML = ActualPcp;

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

        //if (3 === pMCOId) {
        //    $("#dMCOMolinaHealthCare").show();
        //    $("#MCOMolinaHealthCare").attr('checked', 'checked');
        //} else {
        //    $("#MCOMolinaHealthCare").removeAttr('checked');
        //    $("#dMCOMolinaHealthCare").hide();
        //}

        if (4 === pMCOId) {
            $("#dMCOPlanMenonita").show();
            $("#MCOPlanMenonita").attr('checked', 'checked');
        } else {
            $("#MCOPlanMenonita").removeAttr('checked');
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

    ////Send Email
    //$('#chkSendEmail').click(function (e) {
    //    if ($(this).is(':checked'))
    //        $('#txtEmail').removeAttr("disabled");
    //    else {
    //        $("#txtEmail").attr("disabled", "disabled");
    //        $('#txtEmail').val('');
    //    }
    //});
    //Show/Hide section
    $('#btnDisplaySelection').click(function (e) {
        event.preventDefault();
        var query = $('#cardSelection');
        var isVisible = query.is(':visible');
        var height = query.css('height');

        if (isVisible === true) {
            $("#txt_btn_displat_Selection").attr({ "data-i18n": "Enrollment_btnDisplaySelection" });
            query.hide();
            $("#justCauseCheck").prop("checked", false);
            cancelJustCause();
            if (blnChangeEnabled) {
                $('#btnDisplaySelection').prop('disabled', false);
            } else {
                $('#btnDisplaySelection').prop('disabled', true);
            }
            //$('#cardJustCause').hide();
        }
        else {
            //$(this).html(i18next.t('Enrollment_btnhideselection'));
            $("#txt_btn_displat_Selection").attr({ "data-i18n": "Enrollment_btnhideselection" });
            query.show();
            //$("#justCauseCheck").prop("checked", false);
            //cancelJustCause();
            //$('#cardJustCause').show();
        }
        i18next.changeLanguage(currentLanguage);
        //query.toggle("fast");
        $('html, body').animate({
            scrollTop: $(this).offset().top
        }, 100);
    });
    //click button btnSearch
    $('body').on('click', '#btnSearch', function (e) {
       // debugger;
        if ($("#justCauseCheck").is(':checked') && $("#cbJustCausa").val() === "") {
            swal({
                title: i18next.t('common_warning'),
                text: i18next.t('Enrollment_SelectReasonJustCause'),
                type: "warning",
                confirmButtonText: "OK"
            });
            return;
        }

        $("#results-div").html('');
        $.ajax({
            url: urlGetPcpWithFiltersToList,
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
                entityRequest: BuildRequestPcp()
            },
            type: 'POST',
            async: true,
            success: function (data) {
                //debugger:
                console.log(data);
                var divContainChange = $('#results-div');
                divContainChange.html('');
                $('#pagination').empty();
                $('#pagination').removeData("twbs-pagination");
                $('#pagination').unbind("page");

                if (data.code === 0) {
                    if (data.recordsTotal >= 199) {
                        swal({
                            title: i18next.t('common_info'),
                            text: i18next.t('common_ExcessData'),
                            type: "info",
                            confirmButtonText: "OK"
                        });
                    }

                    if (data.recordsTotal > 0) {
                        jsondatamemory = data.records;

                        //data.records = data.records.sort(function (a, b) {
                        //    if (a.PersonId < b.PersonId)
                        //        return -1;
                        //    if (a.PersonId > b.PersonId)
                        //        return 1;
                        //    return 0;
                        //});


                        var totalRows = parseInt(data.records.length);
                        var maxRow_x_page = 10;
                        var numTotal_pages = (totalRows % maxRow_x_page == 0) ? parseInt(totalRows / maxRow_x_page) : parseInt(totalRows / maxRow_x_page) + 1;
                        if (totalRows > 0) {
                            var obj = $('#pagination').twbsPagination({
                                totalPages: numTotal_pages,
                                visiblePages: 10,
                                first: '<span class="FirstPage"><i class="fa fa-angle-double-left"></i></span>',
                                next: '<span class="NextPage"><i class="fa fa-angle-right"></i></span>',
                                prev: '<span class="BackPage"><i class="fa fa-angle-left"></i></span>',
                                last: '<span class="LastPage"><i class="fa fa-angle-double-right"></i></span>',
                                onPageClick: function (event, page) {
                                    var htmlConcat = '';
                                    var page = parseInt(page);
                                    var min = maxRow_x_page * (page - 1) + 1;
                                    var max = maxRow_x_page * (page);

                                    $.each(data.records, function (i, item) {
                                        if ((i + 1) >= min && (i + 1) <= max) {
                                            //Build Html Section

                                            //////var PMGName = '';
                                            ////////PMGName = resultsPmg.filter(x => x.Id == item.AvailableManagedCareOrganizations[0].PmgId).PmgName;
                                            //////$.each(resultsPmg, function (i, el) {
                                            //////    if (el.Id == item.AvailableManagedCareOrganizations[0].PmgId)
                                            //////        PMGName = el.PmgName;
                                            //////});
                                            var PMGName = '';
                                            var firstPMGId = 0;
                                            //PMGName = resultsPmg.filter(x => x.Id == item.AvailableManagedCareOrganizations[0].PmgId).PmgName;
                                            $.each(item.AvailablePrimaryMedicalGroups, function (i, el) {
                                                //$.each(item.Data, function (i2, el2) {
                                                //if (el.Id == el2.AvailableManagedCareOrganizations[0].PmgId)
                                                if (el.PmgName != 'N/A') {
                                                    PMGName += el.PmgName + ' - ';
                                                }
                                                if (firstPMGId === 0) {
                                                    firstPMGId = el.Id;
                                                }
                                                //});
                                            });

                                            PMGName = PMGName.substr(0, PMGName.lastIndexOf('-'));

                                            if (PMGName.length > 50) {
                                                PMGName = PMGName.substr(0, 45);
                                                PMGName += ' ...';
                                            }

                                            //console.log(ActualMcoId + ' - ' + item.AvailableManagedCareOrganizations[0].McoId);
                                            var strDisabled = '';//(ActualMcoId == item.AvailableManagedCareOrganizations[0].McoId ? 'disabled' : '');
                                            htmlConcat += getHtmlForSearchPcp()
                                                .replace("labelsearchId", "labelsearch" + item.Id)

                                                .replace(/toggleId/g, "toggle" + item.Id)
                                                .replace("Person", item.Person.NPI)

                                                .replace("SpecialityChange", item.Speciality.Name)
                                                .replace("expandId", "expand" + item.Id)
                                                .replace(/NPIChange/g, item.Person.NPI)
                                                .replace(/AdressLineOneChange/g, item.AddressLineOne)
                                                .replace(/AdressLineTwoChange/g, item.AddressLineTwo)
                                                .replace(/CityChange/g, item.City)
                                                .replace(/StateChange/g, item.State)
                                                .replace(/ZipCodeChange/g, item.ZipCode)

                                                .replace(/PmgChange/g, PMGName)
                                                .replace(/McoChange/g, devuelvemco(item.AvailableManagedCareOrganizations[0].McoId))
                                                .replace(/PcpChange/g, item.Person.FullName)

                                                .replace("PmgId", "Pmg" + item.AvailablePrimaryMedicalGroups[0].Id)
                                                .replace("PcpId", "Pcp" + item.Id)
                                                .replace("McoId", "Mco" + item.AvailableManagedCareOrganizations[0].Id)

                                                .replace("CapacityMco", item.Capacity)
                                                .replace("btnApplyChangeId", "btnApplyChange" + item.PersonId)
                                                .replace("disabled", strDisabled);
                                            //GPchange
                                            if ((item.AvailableManagedCareOrganizations.find(a => a.Id == 2)) != undefined) {
                                                htmlConcat = htmlConcat.replace("GPchange", "fa fa-check fa-1x");
                                                htmlConcat = htmlConcat.replace("GPstyle", "color: green");
                                            } else {
                                                htmlConcat = htmlConcat.replace("GPchange", "fa fa-times fa-1x");
                                                htmlConcat = htmlConcat.replace("GPstyle", "color: red");
                                            };
                                            //FPchange
                                            if ((item.AvailableManagedCareOrganizations.find(a => a.Id == 1)) != undefined) {
                                                htmlConcat = htmlConcat.replace("FPchange", "fa fa-check fa-1x");
                                                htmlConcat = htmlConcat.replace("FPstyle", "color: green");
                                            } else {
                                                htmlConcat = htmlConcat.replace("FPchange", "fa fa-times fa-1x");
                                                htmlConcat = htmlConcat.replace("FPstyle", "color: red");
                                            };
                                            //IMchange
                                            if ((item.AvailableManagedCareOrganizations.find(a => a.Id == 3)) != undefined) {
                                                htmlConcat = htmlConcat.replace("IMchange", "fa fa-check fa-1x");
                                                htmlConcat = htmlConcat.replace("IMstyle", "color: green");
                                            } else {
                                                htmlConcat = htmlConcat.replace("IMchange", "fa fa-times fa-1x");
                                                htmlConcat = htmlConcat.replace("IMstyle", "color: red");
                                            };
                                            //PMchange
                                            if ((item.AvailableManagedCareOrganizations.find(a => a.Id == 4)) != undefined) {
                                                htmlConcat = htmlConcat.replace("PMchange", "fa fa-check fa-1x");
                                                htmlConcat = htmlConcat.replace("PMstyle", "color: green");
                                            } else {
                                                htmlConcat = htmlConcat.replace("PMchange", "fa fa-times fa-1x");
                                                htmlConcat = htmlConcat.replace("PMstyle", "color: red");
                                            };
                                            //GMchange
                                            if ((item.AvailableManagedCareOrganizations.find(a => a.Id == 5)) != undefined) {
                                                htmlConcat = htmlConcat.replace("GMchange", "fa fa-check fa-1x");
                                                htmlConcat = htmlConcat.replace("GMstyle", "color: green");
                                            } else {
                                                htmlConcat = htmlConcat.replace("GMchange", "fa fa-times fa-1x");
                                                htmlConcat = htmlConcat.replace("GMstyle", "color: red");
                                            };
                                        }
                                    });
                                    divContainChange.html(htmlConcat);
                                }
                            });
                        }
                    } else {
                        swal({
                            title: i18next.t('common_success'),
                            text: data.message,
                            type: "success",
                            confirmButtonText: "OK"
                        });
                    }
                }
                else {
                    swal({
                        title: "Error     ",
                        text: data.message,
                        type: "error",
                        confirmButtonText: "OK"
                    });
                }

                //$("#pagination .first a").attr({ "data-i18n": "datatable_first" });
                //$("#pagination .prev a").attr({ "data-i18n": "datatable_previous" });
                //$("#pagination .next a").attr({ "data-i18n": "datatable_next" });
                //$("#pagination .last a").attr({ "data-i18n": "datatable_last" });
                i18next.changeLanguage(currentLanguage);

            }
            ,
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus);
                swal({
                    title: "Error     ",
                    text: textStatus,
                    type: "error",
                    confirmButtonText: "OK"
                });
            }
        });
    });

    function cancelJustCause() {
        if ($('#justCauseCheck').is(":checked")) {
            $('#divReasonJustCause').show();
            $('#cbJustCausa').val('');

            permission = true;
            $('#btnDisplaySelection').prop('disabled', false);
        } else {
            $('#divReasonJustCause').hide();
            $('#txtCommentJustCausa').val("");
            $('#cbJustCausa').val('');

            if (blnChangeEnabled) {
                $('#btnDisplaySelection').prop('disabled', false);
            } else {
                $('#btnDisplaySelection').prop('disabled', true);
            }
            $("#txt_btn_displat_Selection").attr({ "data-i18n": "Enrollment_btnDisplaySelection" });
            $('#cardSelection').hide();

            i18next.changeLanguage(currentLanguage);
            //query.toggle("fast");
            $('html, body').animate({
                scrollTop: $(this).offset().top
            }, 100);
        }
    }
    //section html for searchPcp
    function getHtmlForSearchPcp() {
        //var htmlConcat = '<input class="toggle" id="toggleId" type="checkbox"> <label id="labelsearchId" class="label-search" for="toggleId">PcpChange <span>SpecialityChange</span></label><div class="expand" id="expandId" style="height: 0;"> <section class="expandable"><div class="row"><div class="form-group col-md-4"> <label id="NPIChange" class="NPI label-columns"><span>NPI:</span> NPIChange</label> <label id="AddressLineOneChange" class="NPI label-columns"> AdressLineOneChange</label> <label id="AddressLineTwoChange" class="AdressLineOne label-columns"> AdressLineTwoChange</label> <label id="OtherAddressGroupChange" class="OtherAddress label-columns"> CityChange</label> <label id="OtherAddressGroupChange" class="OtherAddress label-columns"> StateChange ZipCodeChange </label> <label id="PcpId" class="Pcp label-columns" style="display:none;"><span>PCP:</span> PcpChange</label> <label id="PmgId" class="Pmg label-columns" style="display:none;"><span>PMG:</span> PmgChange</label> <label id="McoId" class="Mco label-columns" style="display:none;"><span>MCO:</span> McoChange</label> <label id="CapacityMco" class="CapacityMco label-columns" style="display:none;"><span>CapacityMco:</span> CapacityMco</label></div><div id="results-available-div" class="form-group col-md-4"> <label id="GP" class="GP label-columns"><span class="GPchange" style="GPstyle"></span> MMM Multi Health</label> <label id="FP" class="FP label-columns"><span class="FPchange" style="FPstyle"></span> First Medical Plan</label> <label id="IM" class="IM label-columns"><span class="IMchange" style="IMstyle"></span> Molina Healthcare of PR</label> <label id="PM" class="PM label-columns"><span class="PMchange" style="PMstyle"></span> Plan Menorita</label> <label id="GM" class="GM label-columns"><span class="GMchange" style="GMstyle"></span> Triple S</label></div><div class="form-group col-md-4"><div class="position-relative has-icon-left" style="text-align:center;"> <button type="submit" class="btn btn-primary center-button 
        //" id="btnApplyChangeId" data-i18n="Enrollment_Seleccion" data-toggle="modal" data-target="#modalCart" disabled> <i class="icon - shuffle"></i> ' + i18next.t("Enrollment_Seleccion") + ' </button></div></div></div> </section></div>';
        //var htmlConcat = '<input class="toggle" id="toggleId" type="checkbox"><label id="labelsearchId" class="label-search" for="toggleId">PcpChange <span>SpecialityChange</span></label><div class="expand" id="expandId" style="height: 0;"> <section class="expandable"> <div class="row"> <div class="form-group col-md-4"> <label id="NPIChange" class="NPI label-columns"><span>NPI:</span> NPIChange</label> <label id="AddressLineOneChange" class="NPI label-columns" style="display:none;"> AdressLineOneChange</label> <label id="AddressLineTwoChange" class="AdressLineOne label-columns" style="display:none;"> AdressLineTwoChange</label> <label id="OtherAddressGroupChange" class="OtherAddress label-columns" style="display:none;"> CityChange</label> <label id="OtherAddressGroupChange" class="OtherAddress label-columns" style="display:none;"> StateChange ZipCodeChange </label> <label id="PcpId" class="Pcp label-columns"><span>PCP:</span> PcpChange</label> <label id="PmgId" class="Pmg label-columns"><span>PMG:</span> PmgChange</label> <label id="McoId" class="Mco label-columns" style="display:none;"><span>MCO:</span> McoChange</label> <label id="CapacityMco" class="CapacityMco label-columns" style="display:none;"><span>CapacityMco:</span> CapacityMco</label> </div><div id="results-available-div" class="form-group col-md-4"> <label id="GP" class="GP label-columns"><span class="GPchange" style="GPstyle"></span> MMM Multi Health</label> <label id="FP" class="FP label-columns"><span class="FPchange" style="FPstyle"></span> First Medical Plan</label> <label id="IM" class="IM label-columns"><span class="IMchange" style="IMstyle"></span> Molina Healthcare of PR</label> <label id="PM" class="PM label-columns"><span class="PMchange" style="PMstyle"></span> Plan Menorita</label> <label id="GM" class="GM label-columns"><span class="GMchange" style="GMstyle"></span> Triple S</label> </div><div class="form-group col-md-4"> <div class="position-relative has-icon-left" style="text-align:center;"> <button type="submit" class="btn btn-primary center-button SelectDirections" id="btnApplyChangeId" disabled> <i class="icon-shuffle"></i> <span data-i18n="Enrollment_Seleccion">' + i18next.t('Enrollment_Seleccion') + '</span></button> </div></div></div></section></div>';
        var htmlConcat = '<input class="toggle" id="toggleId" type="checkbox"><label id="labelsearchId" class="label-search" for="toggleId">PcpChange <span>SpecialityChange</span></label><div class="expand" id="expandId" style="height: 0;"> <section class="expandable"> <div class="row"> <div class="form-group col-md-4"> <label id="NPIChange" class="NPI label-columns"><span>NPI:</span> NPIChange</label> <label id="AddressLineOneChange" class="NPI label-columns" style="display:none;"> AdressLineOneChange</label> <label id="AddressLineTwoChange" class="AdressLineOne label-columns" style="display:none;"> AdressLineTwoChange</label> <label id="OtherAddressGroupChange" class="OtherAddress label-columns" style="display:none;"> CityChange</label> <label id="OtherAddressGroupChange" class="OtherAddress label-columns" style="display:none;"> StateChange ZipCodeChange </label> <label id="PcpId" class="Pcp label-columns"><span>PCP:</span> PcpChange</label> <label id="PmgId" class="Pmg label-columns"><span>PMG:</span> PmgChange</label> <label id="McoId" class="Mco label-columns" style="display:none;"><span>MCO:</span> McoChange</label> <label id="CapacityMco" class="CapacityMco label-columns" style="display:none;"><span>CapacityMco:</span> CapacityMco</label> </div><div id="results-available-div" class="form-group col-md-4"> <label id="GP" class="GP label-columns"><span class="GPchange" style="GPstyle"></span> MMM Multi Health</label> <label id="FP" class="FP label-columns"><span class="FPchange" style="FPstyle"></span> First Medical Plan</label>  <label id="PM" class="PM label-columns"><span class="PMchange" style="PMstyle"></span> Plan Menorita</label> <label id="GM" class="GM label-columns"><span class="GMchange" style="GMstyle"></span> Triple S</label> </div><div class="form-group col-md-4"> <div class="position-relative has-icon-left" style="text-align:center;"> <button type="submit" class="btn btn-primary center-button SelectDirections" id="btnApplyChangeId" disabled> <i class="icon-shuffle"></i> <span data-i18n="Enrollment_Seleccion">' + i18next.t('Enrollment_Seleccion') + '</span></button> </div></div></div></section></div>';
        return htmlConcat;
    }

    function GetHtmlForDirections() {
        return '<tr>'
            + '	<td style="display: none;">'
            + '     <input type="hidden" class="dirIndex" value="dirIndexValue">'
            + '	</td>'
            + '	<td>'
            + '		<br />'
            + '		<i class="fa fa-map-marker fa-2x" style="color:red"></i>'
            + '     <br />'
            + '	</td>'
            + '	<td class="dontCenter">'
            + '		<span>AddressLineOneAddressLineTwo</span><br />'
            + '		<span>MunicipalityName, State ZipCode</span><br />'
            + '		<span>Tel.: Phone</span>'
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
            //+ '	<td>'
            //+ '		<br />'
            //+ '		faTimesCheck3'
            //+ '     <br /> '
            //+ '	</td>'
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
            + '	<td>'
            + '		<br />'
            + '		SelectButton'
            + '     <br /> '
            + '	</td>'
            + '</tr>';
    };

    //request for searchPcp
    function BuildRequestPcp() {
        var lst_McoId = null;
        var PmgId = null;
        var PcpFullName = null;
        var SpecialityId = null;
        var NPI = null;

        lst_McoId = $("#cbMco").val() === null ? [] : $("#cbMco").val();
        PmgId = $("#cbPmg").val();
        PcpFullName = $("#txtPhName").val();
        SpecialityId = $("#cbSpeciality").val();
        NPI = $("#txtNPI").val();

        var entityRequest = new Object();
        entityRequest.lst_McoId = lst_McoId;
        entityRequest.PmgId = PmgId;
        entityRequest.PcpFullName = PcpFullName;
        entityRequest.SpecialityId = SpecialityId;
        entityRequest.NPI = NPI;
        entityRequest.ShowForChangeEnrollmentProcess = true;

        return entityRequest;
    }

    //Click in button for popup
    $('body').on('click', '.btnApplyChangeCls', function (e) {
        //debugger;
        e.preventDefault();

        //Get Section expand
        var pcpPmgProviderId = $(this).attr('id').replace('btnApplyChange', '');
        var expandableSection = $('#expand' + pcpPmgProviderId);

        var resultado = jsondatamemory.find(objeto => objeto.Id == pcpPmgProviderId);
        //conseguir los ids del objeto seleccionado
        //var varmcoid = resultado.AvailableManagedCareOrganizations[0].McoId;
        //var varpmgid = resultado.AvailableManagedCareOrganizations[0].PmgId;
        //var varpcpid = resultado.AvailableManagedCareOrganizations[0].PrimaryCarePhysicianId;

        UpdateMcoId = parseInt(resultado.AvailableManagedCareOrganizations[0].Id);
        UpdatePmgId = parseInt(resultado.AvailablePrimaryMedicalGroups[0].Id);
        UpdatePcpId = parseInt(resultado.Id);

        //verificar si existe en el array original

        //var existemco = jsonover.recordsMco.find(objeto => objeto.IdMco == varmcoid);

        //var existepmg = jsonover.recordsPmg.find(objeto => objeto.IdPmg == varpmgid);

        //var existepcp = jsonover.recordsPcp.find(objeto => objeto.IdPcp == varpcpid);


        //if (existemco.OverCapacityMco === true)
        // $('#McoOverCapacity').css('display', 'block');
        //else
        // $('#McoOverCapacity').css('display', 'none');

        //if (existepcp.OverCapacityPcp === true)
        // $('#PcpOverCapacity').css('display', 'block');
        //else
        // $('#PcpOverCapacity').css('display', 'none');

        //if (existepmg.OverCapacityPmg === true)
        // $('#PmgOverCapacity').css('display', 'block');
        //else
        // $('#PmgOverCapacity').css('display', 'none');

        //Get nodes to updated

        var nodePcp = expandableSection.find('.Pcp').text();
        var PcpName = nodePcp.replace('PCP: ', '');
        //UPDATEPCP
        //UpdatePcpId = expandableSection.find('.Pcp').attr('id').replace('Pcp', '');

        var nodeMco = expandableSection.find('.Mco').text();
        var McoName = nodeMco.replace('MCO: ', '');
        //UpdateMcoId = expandableSection.find('.Mco').attr('id').replace('Mco', '');

        var nodePmg = expandableSection.find('.Pmg').text();
        var PmgName = nodePmg.replace('PMG: ', '');
        //UpdatePmgId = expandableSection.find('.Pmg').attr('id').replace('Pmg', '');


        var onlyOne = 0;
        var bMCOFirstMedical = false;
        var bMCOMMM = false;
        //var bMCOMolinaHealthCare = false;
        var bMCOPlanMenonita = false;
        var bMCOTripleS = false;
        //document.getElementById('UpdateMco').innerHTML = McoName;
        if (UpdateMcoId === 1) {
            $("#dMCOFirstMedical").show();
            onlyOne += 1;
            bMCOFirstMedical = true;
            //$("#MCOFirstMedical").css("display", "block");
            //$("#MCOFirstMedical").attr("visibility", "visible");
        } else {
            $("#dMCOFirstMedical").hide();
            //$("#MCOFirstMedical").css("display", "none");
            //$('#MCOFirstMedical').removeAttr('visibility');
        }
        if (UpdateMcoId === 2) {
            $("#dMCOMMM").show();
            onlyOne += 1;
            bMCOMMM = true;
        } else {
            $("#dMCOMMM").hide();
        }
        //if (UpdateMcoId === 3) {
        //    $("#dMCOMolinaHealthCare").show();
        //    onlyOne += 1;
        //    bMCOMolinaHealthCare = true;
        //} else {
        //    $("#dMCOMolinaHealthCare").hide();
        //}
        if (UpdateMcoId === 4) {
            $("#dMCOPlanMenonita").show();
            onlyOne += 1;
            bMCOPlanMenonita = true;
        } else {
            $("#dMCOPlanMenonita").hide();
        }
        if (UpdateMcoId === 5) {
            $("#dMCOTripleS").show();
            //$("#dMCOTripleS").css("display", "block");
            onlyOne += 1;
            bMCOTripleS = true;
        } else {
            $("#dMCOTripleS").hide();
        }

        if (onlyOne === 1) {
            if (bMCOFirstMedical) $("#MCOFirstMedical").attr('checked', 'checked');
            if (bMCOMMM) $("#MCOMMM").attr('checked', 'checked');
            //if (bMCOMolinaHealthCare) $("#MCOMolinaHealthCare").attr('checked', 'checked');
            if (bMCOPlanMenonita) $("#MCOPlanMenonita").attr('checked', 'checked');
            if (bMCOTripleS) $("#MCOTripleS").attr('checked', 'checked');
        }

        document.getElementById('UpdatePmg').innerHTML = UpdatePmg;
        document.getElementById('UpdatePcp').innerHTML = PcpName;
    });
    //click button btnApplyPcp
    $('body').on('click', '#btnApplyPcp', function (e) {
       // debugger;
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

        if ($('#chkSendPhone').is(':checked')) {
            //debugger;
            if ($('#txtPhone').val() == '') {
                swal({
                    title: i18next.t('common_info'),
                    text: i18next.t('Phone_valid'),
                    type: "info",
                    confirmButtonText: "OK"
                });
                return;
            }
            else if ($('#txtPhone').val().length != 10) {
                swal({
                    title: i18next.t('common_info'),
                    text: i18next.t('Phone_lengthInvalid'),
                    type: "info",
                    confirmButtonText: "OK"
                });
                return;
            }
            else if ($('#txtPhone').val().slice(0, 1) == '1') {
                swal({
                    title: i18next.t('common_info'),
                    text: i18next.t('Phone_startCountryCode'),
                    type: "info",
                    confirmButtonText: "OK"
                });
                return;
            }
        }

        var onlyOne = 0;
        if ($("#dMCOFirstMedical").is(":visible")) {
            onlyOne += 1;
            UpdateMcoId = $("#ActualMco").val();
            UpdateMcoId_Old = $("#MCOFirstMedical").val();
        }
        if ($("#dMCOMMM").is(":visible")) {
            onlyOne += 1;
            UpdateMcoId = $("#MCOMMM").val();
        }
        //if ($("#dMCOMolinaHealthCare").is(":visible") && $("#MCOMolinaHealthCare").is(":checked")) {
        //    onlyOne += 1;
        //    UpdateMcoId = $("#MCOMolinaHealthCare").val();
        //}
        if ($("#dMCOPlanMenonita").is(":visible")) {
            onlyOne += 1;
            UpdateMcoId = $("#MCOPlanMenonita").val();
        }
        if ($("#dMCOTripleS").is(":visible")) {
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
        //validar Periodo Erollment
        //var bool = true;

        swal({
            title: i18next.t('common_Sure'),
            text: i18next.t('common_Confirm'),
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: i18next.t('common_ButtonYes'),
            cancelButtonText: i18next.t('common_ButtonNo'),
            closeOnConfirm: true,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {

                    
                    if ($("#justCauseCheck").is(':checked') && $("#cbJustCausa").val() === "") {
                        swal({
                            title: i18next.t('common_warning'),
                            text: i18next.t('Enrollment_SelectReasonJustCause'),
                            type: "warning",
                            confirmButtonText: "OK"
                        });
                        return;
                    }

                    //debugger

                    //$.ajax({
                    //    url: urlCreatePDF,
                    //    dataType: 'json',
                    //    data: {
                    //        entityRequest: BuildRequestPDF(UpdateMcoId, UpdatePmgId, UpdatePcpId)
                    //    },
                    //    type: 'POST',
                    //    async: true,
                    //    success: function (data) {
                    //        if (data.code == 0) {
                    //            //$('btnApplyPcp').removeAttr('disabled');
                    //            FilePDFChangeEnrollment = data.filePDF;
                    //        }
                    //    }
                    //});


                    $.ajax({
                        url: urlChangePersonMcoReject,
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
                            request: BuildRequestPcpApplyReject()
                        },
                        type: 'POST',
                        async: true,
                        success: function (data) {
                            //debugger
                            if (data.code == 0) {
                                //Send SMS if checkSMS
                                if ($('#chkSendPhone').is(':checked')) {
                                    $.ajax({
                                        url: urlSendSms,
                                        dataType: 'json',
                                        type: 'POST',
                                        async: true,
                                        data: {
                                            "PersonId": ApplicationMemberID,
                                            "phone": $("#txtPhone").val()
                                        },
                                        success: function (data) {
                                            if (data.code == 0) {
                                                console.log('mensaje enviado');
                                            }
                                            else {
                                                console.log('error mensaje enviado api');
                                            }
                                        },
                                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                                            console.log(errorThrown);
                                        }
                                    });
                                }

                                $.ajax({
                                    url: urlSendLinkQuitz,
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
                                    async: false,
                                    data: {
                                        entityRequest: BuildRequestEmail()
                                    },
                                    success: function (data) {
                                    },
                                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                                        console.log(errorThrown);
                                    }
                                });

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
                                    async: false,
                                    data: {
                                        entityRequest: BuildRequestEmail()
                                    },
                                    success: function (data) {
                                        if (data.code === 0) {
                                            swal({
                                                title: i18next.t('common_succesfull'),
                                                text: i18next.t('Enrollment_sucessfull_register'),
                                                type: "success",
                                                confirmButtonText: "OK"
                                            }, function () {
                                                $.redirect(UrlThisView,
                                                    {
                                                        "ApplicationMemberID": ApplicationMemberID
                                                    });
                                            });
                                        } else {
                                            swal({
                                                title: i18next.t('common_warning'),
                                                text: i18next.t('Enrollment_sucessfull_register_error_email'),
                                                type: "success",
                                                confirmButtonText: "OK"
                                            }, function () {
                                                $.redirect(UrlThisView,
                                                    {
                                                        "ApplicationMemberID": ApplicationMemberID
                                                    });
                                            });
                                        }
                                    },
                                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                                        swal({
                                            title: i18next.t('common_warning'),
                                            text: i18next.t('Enrollment_sucessfull_register'),
                                            type: "success",
                                            confirmButtonText: "OK"
                                        }, function () {
                                            $.redirect(UrlThisView,
                                                {
                                                    "ApplicationMemberID": ApplicationMemberID
                                                });
                                        });
                                    }
                                });
                            }
                            else {
                                swal({
                                    title: i18next.t('common_alert'),
                                    text: i18next.t('common_alertMessage'),
                                    type: "info",
                                    confirmButtonText: "OK"
                                });

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
                                        title: "Error     ",
                                        text: "No se pudo registrar la transacción",
                                        type: "error",
                                        confirmButtonText: "OK"
                                    });
                                }
                            }
                        }
                        ,
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            swal({
                                title: "Error     ",
                                text: textStatus,
                                type: "error",
                                confirmButtonText: "OK"
                            });
                        }
                    });
                }
            }
        );
    });

    function BuildRequestEmail() {
        var request = new Object();

        if ($('#chkSendEmail').is(':checked')) {
            request.Contact = true;
        } else {
            request.Contact = false;
        }
        request.Email = $('#txtEmail').val();
        request.Phone = $("#txtPhone").val();
        request.NameTo = 'Admin';
        request.NameFile = FilePDFChangeEnrollment;
        request.MemberID = ApplicationMemberID;
        return request;
    }

    //request for PcpApply
    function BuildRequestPcpApplyReject() {
        //debugger
        //console.log("hola")
        var request = new Object();
        // alert(IdMember);
        request.MemberId = IdMember;
        request.McoId = UpdateMcoId;
        request.PcpId = UpdatePcpId;
        request.PpcpId = PersonPcpId;
        request.PmgId = UpdatePmgId;
        request.Origin = 40;
        request.UserName = 'Admin';
        request.IgnoreValidationRules = false;
        request.FilePDFChangeEnrollment = FilePDFChangeEnrollment;
        request.Permission = permission;
        request.JustCause = $("#justCauseCheck").is(':checked') ? $("#cbJustCausa").val() : null;
        request.IsJustCause = $("#justCauseCheck").is(':checked');
        request.IdHistories = IdHistories;
        request.IdStatus = IdStatus;
        return request;
    }

    $('#btnBrowse').on('change', function (e) {
        //debugger;
        var files = e.target.files;
        var file;
        var nameFile;
        var sizeFile;
        var extensionFile;
        var allowedExtensions = $('#ExtensionValidEnrollmentFile').val().split(",");
        if (files.length > 0) {
            file = e.target.files[0];
            nameFile = file.name;
            sizeFile = file.size / 1024 / 1024;
            extensionFile = nameFile.substring(nameFile.lastIndexOf('.') + 1);
            if ($.inArray(extensionFile, allowedExtensions) === -1) {
                swal({
                    title: i18next.t('common_info'),
                    text: i18next.t('common_FileType') + ': ' + $('#ExtensionValidEnrollmentFile').val().split(",").join(', '),
                    type: "warning",
                    confirmButtonText: "OK"
                });
                $('#txtFile').val('');
            } else if (sizeFile > $('#TamanioMaximoEnrollmentFile').val()) {
                swal({
                    title: i18next.t('common_info'),
                    text: i18next.t('common_FileSize') + ': ' + $('#TamanioMaximoEnrollmentFile').val() + 'MB',
                    type: "warning",
                    confirmButtonText: "OK"
                });
                $('#txtFile').val('');
            } else {
                fileEnrollment = new FileReader();
                fileEnrollment.readAsDataURL(file);
                $('#txtFile').val(nameFile);
            }
        }
    });

    $('#btnUploadFile').click(function (e) {
        if (fileEnrollment === undefined || $('#txtFile').val().length === 0) {
            swal({
                title: i18next.t('common_info'),
                text: i18next.t('common_FileSelect'),
                type: "warning",
                confirmButtonText: "OK"
            });
        } else {
            $.ajax({
                url: urlSetEnrollmentFiles,
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
                    entityRequest: BuildRequestFile(0)
                },
                type: 'POST',
                async: true,
                success: function (data) {
                    if (data.code == 0) {
                        listFile(IdMember);
                        //$('#tbFiles').DataTable().ajax.reload();
                    }
                }
            });
        }
    });
});

function ChangeEnrollmentEnabled() {
    $.ajax({
        type: 'GET',
        url: urlChangePersonMcoEnabled,
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        data: {
            MemberId: ApplicationMemberID
        },
        async: false,
        success: function (data) {
            //if (data.code === 0) {
            //console.log(data.records);
            if (data.code === 0) {
                //$('#txtChangeMCOYes').removeAttr('hidden');
                //$('#txtChangeMCONo').hide();
                blnChangeEnabled = true;
                $('#btnDisplaySelection').prop('disabled', false);
            } else {
                //$('#txtChangeMCOYes').hide();
                //$('#txtChangeMCONo').removeAttr('hidden');
                blnChangeEnabled = false;
                $('#btnDisplaySelection').prop('disabled', true);
                //$('#btnChangeMCO').removeClass('btn-info');
                //$('#btnChangeMCO').css('background-color', 'gray');
            }
            //}
        }
        ,
        error: function (jqXhr, textStatus, errorThrown) {
        //debugger;
            swal({
                title: "Error",
                text: textStatus,
                type: "error",
                confirmButtonText: "OK"
            });
        }
    });
}

function LoadPage() {
    //Fill general data
   // debugger;

    $.ajax({
        url: urlGetOnlyRejects,
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
        success: function (data) {
            console.log(data);
            //debugger;
            if (data.code == 0) {
                if (data.recordsTotal > 0) {
                    //LoadTable
                    LoadTableSearchHistory('#tbHistorySearch', data.records);
                        //Scroll into table only show if table width is bigger than div container
                        //PutCustomScrollIntoDataTable('#tbHistorySearch');







                    var obj = data.records[0];
                    Member = obj;
                    PersonId = obj.Id;
                    IdMember = PersonId;
                    ActualMcoId = obj.MCOId;
                    ActualMco = obj?.MCO?.CarrierName;
                    ActualPcp = obj?.PCP?.FirstName + ' ' + obj?.PCP?.FirstLastName + ' ' + obj?.PCP?.SecondLastName;
                    ActualPmg = obj?.PMG?.PmgName;
                    ActualNPI = obj.PCPId;
                    ActualPmgId = obj.PMGId;
                    //$("#txtContactMPI").val(obj.Family.ContactFullName); - DLM
                    $("#txtBeneficiary").val(obj.MemberFullName);
                    $("#txtMPIDatosGenerales").val(obj.MPIShort);
                    $("#txtActualPcp").val((obj.PCP === null ? '' : ActualPcp));
                    $("#txtActualPmg").val((obj.PMG === null ? '' : obj.PMG.PmgName));
                    $("#txtActualMco").val((obj.MCO === null ? '' : obj.MCO.CarrierName));
                    $("#txtEligibility").val(obj.Elegibility);
                    //$("#txtEfectivityDate").val(formatDateEN(obj.Family.EffectiveDate)); - DLM
                    //$("#txtExpirationDate").val(formatDateEN(obj.ExpirationDate));
                    $("#txtComment").val(obj.Reason);
                    //$("#txtContactMPITab3").val(obj.Family.ContactFullName); - DLM

                    document.getElementById('PCPDNPINumber').innerHTML = obj.PCP.NPI;
                    ////$("#imgPCP").attr("src", div.find('#PCPImageUrl').attr('src'));
                    var message = "";
                    if (!obj.IsAvailableForChange)
                        $('#btnDisplaySelection').attr("disabled", "disabled");
                    if (obj.MCOId == null) {
                        message = message + "El MCO no se puede mostrar \n";

                    };
                    if (obj.PCPId == null) {
                        message = message + "El PCP no se puede mostrar \n";

                    };
                    if (obj.PMGId == null) {
                        message = message + "El PMG no se puede mostrar \n";

                    };
                    if (message != "") {
                        message = message + "\n Por favor comunicarse con el administrador.";
                        swal({
                            title: "Adevertencia!",
                            text: message,
                            type: "warning",
                            confirmButtonText: "OK"
                        });
                    };



                    //document.getElementById('ActualMco').innerHTML = $("#txtActualMco").val();
                    //document.getElementById('ActualPmg').innerHTML = obj.PMG.PmgName;
                    //document.getElementById('ActualPcp').innerHTML = obj.PCP.Person.FullName;
                    //Section for autocomplete Phisycian Name
                    //$.ajax({
                    //    url: urlGetAllPcp,
                    //    dataType: 'json',
                    //    type: 'POST',
                    //    async: true,
                    //    success: function (data) {
                    //        if (data.code == 0) {
                    //            if (data.recordsTotal > 0) {
                    //                $.each(data.records, function (index, item) {
                    //                    AvailablePcpName.push({ label: item.FullName });
                    //                });
                    //            }
                    //            //autocomplete
                    //            $("#txtPhName").autocomplete({
                    //                source: AvailablePcpName,
                    //                minLength: 1
                    //            });
                    //        }
                    //    }
                    //});

                    listFile(PersonId);
                    configValidateFiles();
                   // debugger;
                } else {
                    swal({
                        title: i18next.t('common_success'),
                        text: data.message,
                        type: "success",
                        confirmButtonText: "OK"
                    });
                }
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
                   // debugger;
                    swal({
                        title: "Error     ",
                        text: data.message,
                        type: "error",
                        confirmButtonText: "OK"
                    });
                }
            }
        }
        ,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
          //  debugger;
            swal({
                title: "Error     ",
                text: textStatus,
                type: "error",
                confirmButtonText: "OK"
            });
        }
    });
    //Section for Pmg's
    //$.ajax({
    //    url: urlGetAllPmg,
    //    dataType: 'json',
    //    type: 'POST',
    //    async: true,
    //    success: function (data) {
    //        $.each(data.records, function (index, item) {
    //            resultsPmg.push({
    //                Id: item.Id,
    //                PmgName: item.PmgName
    //            });
    //        });
    //    }
    //});
}

function devuelvemco(valor) {
    switch (valor) {
        case 1: return "First Medical";
        case 2: return "MMM";
        //case 3: return "Molina Health Care";
        case 4: return "Plan Menonita";
        case 5: return "Triple S";
        default: return "";
    }

}

function EvalCheckIntoTableIsChecked(myTableId) {
    var bool = false;
    $(myTableId).find('input[type="checkbox"]:checked').each(function () {
        bool = true;
    });
    return bool;
}

function disabledButtonsApply(IsAvailableForChange) {
    if (IsAvailableForChange == false) {
        $("#noChangeApplyDiv").css("display", "block");
        textForDisabledButton = "disabled";
    }
}

function LoadTableSearchHistory(tableSection, JSonData) {
   // debugger;
    //$(tableSection).DataTable().row(tableSectin).remove().draw();
    i18next.changeLanguage(currentLanguage);

    $(tableSection).DataTable({
        destroy: true,
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
            //{ "title": '<span data-i18n="Enrollment_User">' + i18next.t('Enrollment_User') + '</span>', "data": "MCOModifiedBy", "name": "MCOModifiedBy" },
            { "title": '<span data-i18n="Enrollment_ChangeSource">' + i18next.t('Enrollment_ChangeSource') + '</span>', "data": "MPI", "name": "MPI" },
            {
                title: '<span data-i18n="Enrollment_ChangeDate">' + i18next.t('Enrollment_ChangeDate') + '</span>', data: null, render: function (data, type, row) {
                    return formatDateEN(row.CreatedOn);
                }
            },
            {
                title: '<span data-i18n="Enrollment_MCO_Old">' + i18next.t('Enrollment_MCO_Old') + '</span>', data: null, class: "bolder", render: function (data, type, row) {
                    if (row.Member.MCO.CarrierName !== null) {
                        //var check = '<div class="inline-Block"> <img class="size12" src="/Enrollment/app-assets/assets/images/CheckMark.svg"/> </div>';
                        //return check + row.MCO.CarrierName;
                        return row.Member.MCO.CarrierName;
                    } else
                        return '';
                }
            },
            {
                title: '<span data-i18n="Enrollment_PMG_Old">' + i18next.t('Enrollment_PMG_Old') + '</span>', data: null, class: "bolder", render: function (data, type, row) {
                    if (row.Member.PMG.PmgName !== null) {
                        //var check = '<div class="inline-Block"> <img class="size12" src="/Enrollment/app-assets/assets/images/CheckMark.svg"/> </div>';
                        //return check + row.MCO.CarrierName;
                        return row.Member.PMG.PmgName;
                    } else
                        return '';
                }
            },
            {
                title: '<span data-i18n="Enrollment_PCP_Old">' + i18next.t('Enrollment_PCP_Old') + '</span>', data: null, class: "bolder", render: function (data, type, row) {
                    if (row.Member.PCP.FullName !== null) {
                        //var check = '<div class="inline-Block"> <img class="size12" src="/Enrollment/app-assets/assets/images/CheckMark.svg"/> </div>';
                        //return check + row.MCO.CarrierName;
                        return row.Member.PCP.FullName;
                    } else
                        return '';
                }
            },
            {
                title: '<span data-i18n="Enrollment_MCO">' + i18next.t('Enrollment_MCO') + '</span>', data: null, class: "bolder", render: function (data, type, row) {
                    if (row.MCO.CarrierName !== null) {
                        //var check = '<div class="inline-Block"> <img class="size12" src="/Enrollment/app-assets/assets/images/CheckMark.svg"/> </div>';
                        //return check + row.MCO.CarrierName;
                        return row.MCO.CarrierName;
                    } else
                        return '';
                }
            },
            {
                title: '<span data-i18n="Enrollment_PMG">' + i18next.t('Enrollment_PMG') + '</span>', data: null, class: "bolder", render: function (data, type, row) {
                    console.log(row.PMG.PmgName);
                    if (row.PMG.PmgName !== null) {
                        //var check = '<div class="inline-Block"> <img class="size12" src="/Enrollment/app-assets/assets/images/CheckMark.svg"/> </div>';
                        //return check + row.PMG.PmgName;
                        return row.PMG.PmgName;

                    } else
                        return '';
                }
            },
            {
                title: '<span data-i18n="Enrollment_PCP">' + i18next.t('Enrollment_PCP') + '</span>', data: null, class: "bolder", render: function (data, type, row) {
                    if (row.PCP.FullName !== null) {
                        //var check = '<div class="inline-Block"> <img class="size12" src="/Enrollment/app-assets/assets/images/CheckMark.svg"/> </div>';
                        //return check + row.PCP.FullName;
                        return row.PCP.FullName;
                    } else
                        return '';
                }
            },
            {
                title: '<span data-i18n="Enrollment_Status">' + i18next.t('Enrollment_Status') + '</span>', data: null, class: "bolder", render: function (data, type, row) {
                    //if (row.Status !== '' && row.Status !== null) {
                    //    return row.Status.BusinessStatus;
                    //} else
                    //    return '';
                    return '<span  data-i18n="Enrollment_Rejected">Rechazado</span>';
                }
            },
            {
                title: '<span data-i18n="Enrollment_Reject_Select">' + i18next.t('Enrollment_Reject_Select') + '</span>', data: null, class: "bolder", render: function (data, type, row) {
                    //if (row.Status !== '' && row.Status !== null) {
                    //    return row.Status.BusinessStatus;
                    //} else
                    //    return '';
                    return '<button class="btn" data-i18n="Enrollment_Reject_Select_Descripcion" id="' + row.Id + '" onclick="MemberId(' + row.MemberId + ',' + row.Id + ',4);"></button > ';
                }
            },
            {
                title: '<span data-i18n="Enrollment_Reject_Cancel">' + i18next.t('Enrollment_Reject_Cancel') + '</span>', data: null, class: "bolder", render: function (data, type, row) {
                    //if (row.Status !== '' && row.Status !== null) {
                    //    return row.Status.BusinessStatus;
                    //} else
                    //    return '';
                    return '<button class="btn" data-i18n="Enrollment_Reject_Cancel_Description" id="' + row.Id + '" onclick="Reject(' + row.MemberId + ',' + row.Id + ',' + row.MCOId + ',' + row.PMG.Id + ',' + row.PCP.Id + ',5);">Cancelar</button > ';
                }
            }
        ]
    });
    i18next.changeLanguage(currentLanguage);
}

function LoadTableFiles(tableSection, JSonData, PersonId) {
   // debugger;
    //$(tableSection).DataTable().destroy();
    //$(tableSection).DataTable().clear().draw();
    //$(tableSection).DataTable().row().remove().draw();
    //$(tableSection).DataTable().row().delete();
    //$(tableSection).DataTable().row().delete();
    //alert($(tableSection)[0].tagname);
    currentLanguage = i18next.language;
    i18next.changeLanguage(currentLanguage);

    $(tableSection).DataTable({
        destroy: true,
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
        //"autoWidth": true,
        //"scrollX": true,
        "paging": false,
        //"ordering": true,
        //"processing": false, // for show progress bar
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
            {
                title: "<span data-i18n='SearchPerson_Header_View'></span>", data: null, render: function (data, type, row) {
                    //var PersonId = tableSection.replace('#tbPeople', '');
                    return "<a style='line-height:0.1' data-i18n='SearchPerson_Header_View' class='btn btn-info GoEnrollmentPage' href='#' id='See'" + row.Id + "' onClick='seeFile(" + row.Id + ");'></a>";
                    //return "<a style='line-height:0.1' class='btn btn-info GoEnrollmentPage' href='#' id='See" + row.Id + "' onClick='seeFile(" + row.Path + ");'>" + i18next.t('SearchPerson_Header_View') + "</a>";
                }
            },
            {
                title: "<span data-i18n='common_ButtonEliminar'></span>", data: null, render: function (data, type, row) {
                    //var PersonId = tableSection.replace('#tbPeople', '');
                    return "<a style='line-height:0.1;' class='btn btn-info GoEnrollmentPage' data-i18n='common_ButtonEliminar' href='#' id='Del'" + row.Id + "' onClick='deleteFile(" + row.Id + "," + PersonId + ");'></a>";
                }
            },
            { "title": "<span data-i18n='common_File'></span>", "data": "Name", "name": "Name" }
        ],
    });

    i18next.changeLanguage(currentLanguage);

}
function SendEnrollmentPeriod() {
    //debugger;
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
            console.log(data);

            if (data.code === 0) {
                swal({
                    title: i18next.t('common_success'),
                    text: data.message,
                    type: "success",
                    confirmButtonText: "OK"
                });
            }

            // Loop the array of objects

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            swal({
                title: "Error     ",
                text: textStatus,
                type: "error",
                confirmButtonText: "OK"
            });
        }
    });
}

function configValidateFiles() {
    //debugger;
    $.ajax({
        url: urlGetValidateFiles,
        dataType: 'json',
        type: 'POST',
        async: true,
        success: function (data) {
            if (data.code == 0) {
                $('#TamanioMaximoEnrollmentFile').val(data.TamanioMaximo);
                $('#ExtensionValidEnrollmentFile').val(data.ExtensionValid);
                $('#PathEnrollmentFile').val(data.Path);
            }
        }
    });
}

function listFile(PersonId) {
   // debugger;
    //Section for load table Files
    $.ajax({
        url: urlGetEnrollmentFiles,
        dataType: 'json',
        data: {
            "MemberId": PersonId
        },
        type: 'POST',
        async: true,
        success: function (data) {
            $('#sectionFiles').html('');
            //document.getElementById('sectionFiles').innerHTML = '<table id="tbFiles" class="table table-hover table-bordered dt-responsive nowrap dataex-html5-export" width="100%" cellspacing="0"><thead><tr><th scope="col" class="all" width="15%" data-i18n="SearchPerson_Header_View">' + i18next.t('SearchPerson_Header_View') + '</th><th scope="col" class="none" width="15%" data-i18n="common_ButtonEliminar">' + i18next.t('common_ButtonEliminar') + '</th><th scope="col" class="desktop" width="70%" data-i18n="common_File">' + i18next.t('common_File') + '</th></tr></thead></table>'; - DLM
            if (data.code == 0) {
                //if (data.recordsTotal > 0) {
                //LoadTable
                LoadTableFiles('#tbFiles', data.records, PersonId);
                //Scroll into table only show if table width is bigger than div container
                PutCustomScrollIntoDataTable('#tbFiles');
            } else {
                LoadTableFiles('#tbFiles', null, PersonId);
                //Scroll into table only show if table width is bigger than div container
                PutCustomScrollIntoDataTable('#tbFiles');
            }
        }
    });
}

function seeFile(FileId) {
   // debugger;
    $.ajax({
        url: urlGetFile,
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
            "FileId": FileId
        },
        type: 'POST',
        async: true,
        success: function (data) {
            if (data.code == 0) {
                //var fileBase64;
                //var filePreview = document.createElement('img');
                //filePreview.id = 'file-preview';

                $.each(data.records, function (index, item) {
                    //$('#ifrFile').val(decode64(item.Content));
                    $('#ifrFile').attr('src', BuildFormatExtension(item.Extension) + item.Content);
                    //fileBase64 = 'data:image/jpeg;base64,' + item.Content;
                });
                //filePreview.src = fileBase64;
                //$('#modalFile').append(filePreview);

                $('#modalFile').dialog({
                    modal: true,
                    title: 'Archivo',
                    width: 1024,
                    height: 800,
                    resizable: true,
                    show: true,
                    hide: true,
                    //beforeClose: function (event, ui) {
                    //    if (pFuncion1 != null) {
                    //        pFuncion1();
                    //    }
                    //},
                    buttons: {
                        "Cerrar": function () {
                            $('#modalFile').dialog("close");
                        }
                    }
                });

                $('#modalFile').dialog("open");

                //swal({
                //    title: "Archivo",
                //    html: 'img id="ifrFile2" width="200" height="200" src="' + fileBase64 + '" />',
                //    type: "info",
                //    confirmButtonText: "OK"
                //});
            }
        }
    });
}

function deleteFile(fileId, PersonId) {
   // debugger;
    $.ajax({
        url: urlDisabledEnrollmentFiles,
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
            entityRequest: BuildRequestFile(fileId)
        },
        type: 'POST',
        async: true,
        success: function (data) {
            if (data.code == 0) {
                $('#txtFile').val('');
                listFile(IdMember);
                //listFile(PersonId);
                //$('#tbFiles').DataTable().ajax.reurlGetEnrollmentFilesload();
            }
        }
    });
}

function seePDF(FilePDF) {
   // debugger;
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
                $('#ifrFile').attr('src', BuildFormatExtension('pdf') + data.content);

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

function BuildFormatExtension(formatExtension) {
    var request;

    switch (formatExtension) {
        case "jpg":
            request = 'data:image/' + formatExtension + ';base64,';
            break;
        case "jpeg":
            request = 'data:image/' + formatExtension + ';base64,';
            break;
        case "pdf":
            request = 'data:application/' + formatExtension + ';base64,';
            break;
    }

    return request;
}

function BuildRequestFile(fileId) {
    var request = new Object();

    if (fileId === 0) {
        request.Id = fileId;
        request.MemberId = IdMember;
        request.Path = $('#PathEnrollmentFile').val();
        request.Name = $('#txtFile').val();
        request.Extension = $('#txtFile').val().substring($('#txtFile').val().lastIndexOf('.') + 1);
        request.CreatedBy = 'Admin';
        //request.CreatedOn = '17/06/2019';//new Date();
        request.Enabled = true;
        request.Content = fileEnrollment.result;
    } else {
        request.Id = fileId;
        request.Enabled = false;
        request.UpdatedBy = 'Admin';
    }
    return request;
}

function BuildRequestPDF(UpdateMcoId, UpdatePmgId, UpdatePcpId) {
    
    var request = new Object();

    request.MemberId = IdMember;
    request.McoId = UpdateMcoId;
    request.PmgId = UpdatePmgId;
    request.PcpId = UpdatePcpId;

    return request;
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

function PutCustomScrollIntoDataTable(myTableId) {
    if (window.innerWidth >= 1000)
        $(myTableId).parent().mCustomScrollbar({
            axis: "x",
            theme: "dark-thin",
            autoExpandScrollbar: true,
            advanced: { autoExpandHorizontalScroll: true }
        });
    else
        $(myTableId).mCustomScrollbar({
            axis: "x",
            theme: "dark-thin",
            autoExpandScrollbar: true,
            advanced: { autoExpandHorizontalScroll: true }
        });
}
function BuildRequestPeriod() {
    var request = new Object();

    var fecini = null;
    var fecfin = null;
    var MemberId = null;


    fecini = $("#DateFromPeriod").val();
    fecfin = $("#DateToPeriod").val();
    MemberId = ApplicationMemberID;

    request.fecini = fecini;
    request.fecfin = fecfin;
    request.MemberId = MemberId;
    return request;
}

function validarEmail(valor) {
    if (/^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i.test(valor))
        return true;
    else
        return false;
}

function CleanNumber(str) {
    str = str.replace(/\D/g, '');
    return parseInt(str);
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

function MemberId(IdMem, Id, Status) {
    //debugger;
    IdMember = IdMem;
    IdHistories = Id;
    IdStatus = Status;
    $("#cardSelection").show();
    event.preventDefault();
}
function BuildRequestPcpApplyRejectCancel() {
    //debugger
    //console.log("hola")
    var request = new Object();
    // alert(IdMember);
    request.MemberId = IdMember;
    request.McoId = McoCancel;
    request.PcpId = PcpCancel;
    request.PpcpId = PersonPcpId;
    request.PmgId = PmgCancel;
    request.Origin = 40;
    request.UserName = 'Admin';
    request.IgnoreValidationRules = false;
    request.FilePDFChangeEnrollment = FilePDFChangeEnrollment;
    request.Permission = permission;
    request.JustCause = $("#justCauseCheck").is(':checked') ? $("#cbJustCausa").val() : null;
    request.IsJustCause = $("#justCauseCheck").is(':checked');
    request.IdHistories = IdHistories;
    request.IdStatus = IdStatus;
    return request;
}
function Reject(IdMem, Id, mcoid, pmgid, pcpid, Status) {
    //debugger;
    IdMember = IdMem;
    IdHistories = Id;
    IdStatus = Status;
    McoCancel = mcoid;
    PmgCancel = pmgid;
    PcpCancel = pcpid;

    swal({
        title: i18next.t('common_Sure'),
        text: i18next.t('common_Confirm'),
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: i18next.t('common_ButtonYes'),
        cancelButtonText: i18next.t('common_ButtonNo'),
        closeOnConfirm: true,
        closeOnCancel: true
    },
        function (isConfirm) {
            if (isConfirm) {
                $.ajax({
                    url: urlChangePersonMcoReject,
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
                        request: BuildRequestPcpApplyRejectCancel()
                    },
                    type: 'POST',
                    async: true,
                    success: function (data) {
                        //debugger
                        if (data.code == 0) {
                            //Send SMS if checkSMS
                            //if ($('#chkSendPhone').is(':checked')) {
                            //    $.ajax({
                            //        url: urlSendSms,
                            //        dataType: 'json',
                            //        type: 'POST',
                            //        async: true,
                            //        data: {
                            //            "PersonId": ApplicationMemberID,
                            //            "phone": $("#txtPhone").val()
                            //        },
                            //        success: function (data) {
                            //            if (data.code == 0) {
                            //                console.log('mensaje enviado');
                            //            }
                            //            else {
                            //                console.log('error mensaje enviado api');
                            //            }
                            //        },
                            //        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //            console.log(errorThrown);
                            //        }
                            //    });
                            //}

                            //$.ajax({
                            //    url: urlSendLinkQuitz,
                            //    dataType: 'json',
                            //    beforeSend: function () {
                            //        start = (new Date()).getTime();
                            //        $.blockUI({ message: '<div class="icon-spinner9 icon-spin icon-lg"></div>', timeout: 60000, overlayCSS: { backgroundColor: "#000000", opacity: .8, cursor: "wait" }, css: { border: 0, padding: 0, backgroundColor: "transparent" } });
                            //    },
                            //    complete: function () {
                            //        end = (new Date()).getTime();
                            //        var total = end - start;
                            //        $.unblockUI();
                            //    },
                            //    type: 'POST',
                            //    async: false,
                            //    data: {
                            //        entityRequest: BuildRequestEmail()
                            //    },
                            //    success: function (data) {
                            //    },
                            //    error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //        console.log(errorThrown);
                            //    }
                            //});

                            //$.ajax({
                            //    url: urlSendConfirmationEmail,
                            //    dataType: 'json',
                            //    beforeSend: function () {
                            //        start = (new Date()).getTime();
                            //        $.blockUI({ message: '<div class="icon-spinner9 icon-spin icon-lg"></div>', timeout: 60000, overlayCSS: { backgroundColor: "#000000", opacity: .8, cursor: "wait" }, css: { border: 0, padding: 0, backgroundColor: "transparent" } });
                            //    },
                            //    complete: function () {
                            //        end = (new Date()).getTime();
                            //        var total = end - start;
                            //        $.unblockUI();
                            //    },
                            //    type: 'POST',
                            //    async: false,
                            //    data: {
                            //        entityRequest: BuildRequestEmail()
                            //    },
                            //    success: function (data) {
                            //        if (data.code === 0) {
                            //            swal({
                            //                title: i18next.t('common_succesfull'),
                            //                text: i18next.t('Enrollment_sucessfull_register'),
                            //                type: "success",
                            //                confirmButtonText: "OK"
                            //            }, function () {
                            //                $.redirect(UrlThisView,
                            //                    {
                            //                        "ApplicationMemberID": ApplicationMemberID
                            //                    });
                            //            });
                            //        } else {
                            //            swal({
                            //                title: i18next.t('common_warning'),
                            //                text: i18next.t('Enrollment_sucessfull_register_error_email'),
                            //                type: "success",
                            //                confirmButtonText: "OK"
                            //            }, function () {
                            //                $.redirect(UrlThisView,
                            //                    {
                            //                        "ApplicationMemberID": ApplicationMemberID
                            //                    });
                            //            });
                            //        }
                            //    },
                            //    error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //        swal({
                            //            title: i18next.t('common_warning'),
                            //            text: i18next.t('Enrollment_sucessfull_register'),
                            //            type: "success",
                            //            confirmButtonText: "OK"
                            //        }, function () {
                            //            $.redirect(UrlThisView,
                            //                {
                            //                    "ApplicationMemberID": ApplicationMemberID
                            //                });
                            //        });
                            //    }
                            //});

                            swal({
                                title: i18next.t('common_succesfull'),
                                text: i18next.t('Enrollment_sucessfull_register'),
                                type: "success",
                                confirmButtonText: "OK"
                            }, function () {
                                $.redirect(UrlThisView,
                                    {
                                        "ApplicationMemberID": ApplicationMemberID
                                    });
                            });
                        }
                        else {
                            swal({
                                title: i18next.t('common_alert'),
                                text: i18next.t('common_alertMessage'),
                                type: "info",
                                confirmButtonText: "OK"
                            });

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
                                    title: "Error     ",
                                    text: "No se pudo registrar la transacción",
                                    type: "error",
                                    confirmButtonText: "OK"
                                });
                            }
                        }
                    }
                    ,
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        swal({
                            title: "Error     ",
                            text: textStatus,
                            type: "error",
                            confirmButtonText: "OK"
                        });
                    }
                });
            }
        }
    );
}

$("#txtPhone").on("keyup", function (event) {
    phone = $(this).val();
    phone = formatPhoneNumber(phone);
    $(this).val(phone);

});

