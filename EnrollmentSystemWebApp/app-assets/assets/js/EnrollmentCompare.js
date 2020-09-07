var IdMember = 0;
var FilePDFChangeEnrollment = '';
$(function () {
    //LoadPage();

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
                        title: "Info!",
                        text: "No se encontraron datos",
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

    $(".cbPmg").select2({
        placeholder: i18next.t('AllCboSelectAll'),
        minimumInputLength: 0,
        allowClear: true,
        ajax: {
            delay: 150,
            url: urlGetAllPmg,
            dataType: 'json',
            async: true,
            processResults: function (data, params) {
                var results = [];
                if (data.recordsTotal == 0)
                    swal({
                        title: "Info!",
                        text: "No se encontraron datos",
                        type: "info",
                        confirmButtonText: "OK"
                    });
                else {
                    $.each(data.records, function (index, item) {
                        results.push({
                            id: item.Id,
                            text: item.PmgName
                        });
                    });
                }
                return {
                    results: results
                };
                //return {
                //    results: $.map(data.records, function (obj) {
                //        return { id: obj.PmgId, text: obj.Name };
                //    })
                //};
            }
        }
    });

    $(".cbPhName").select2({
        placeholder: i18next.t('AllCboSelectAll'),
        minimumInputLength: 0,
        allowClear: true,
        ajax: {
            delay: 150,
            url: urlGetAllPcp + "/true",
            dataType: 'json',
            async: true,
            processResults: function (data, params) {
                var results = [];
                if (data.recordsTotal == 0)
                    swal({
                        title: "Info!",
                        text: "No se encontraron datos",
                        type: "info",
                        confirmButtonText: "OK"
                    });
                else {
                    $.each(data.records, function (index, item) {
                        results.push({
                            id: item.Id,
                            text: item.FullName
                        });
                    });
                }
                return {
                    results: results
                };
            }
        }
    });

    $(".cbSpeciality").select2({
        placeholder: i18next.t('AllCboSelectAll'),
        minimumInputLength: 0,
        allowClear: true,
        ajax: {
            delay: 150,
            url: urlGetAllSpeciality + "/true",
            dataType: 'json',
            async: true,
            processResults: function (data) {
                var results = [];
                if (data.recordsTotal == 0)
                    swal({
                        title: "Info!",
                        text: "No se encontraron datos",
                        type: "info",
                        confirmButtonText: "OK"
                    });
                else {
                    $.each(data.records, function (index, item) {
                        results.push({
                            id: item.Id,
                            text: i18next.language === 'es' ? item.Nombre : item.Name
                        });
                    });
                }
                return {
                    results: results
                };
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
    //Send Email
    $('#chkSendEmail').click(function (e) {
        if ($(this).is(':checked'))
            $('#txtEmail').removeAttr("disabled");
        else {
            $("#txtEmail").attr("disabled", "disabled");
            $('#txtEmail').val('');
        }
    });
    //Show/Hide section
    $('#btnDisplaySelection').click(function (e) {
        event.preventDefault();
        var query = $('#cardSelection');
        var isVisible = query.is(':visible');
        var height = query.css('height');

        if (isVisible === true) {
            $("#txt_btn_displat_Selection").attr({ "data-i18n": "Enrollment_btnDisplaySelection" });
            query.hide();
        }
        else {
            //$(this).html(i18next.t('Enrollment_btnhideselection'));
            $("#txt_btn_displat_Selection").attr({ "data-i18n": "Enrollment_btnhideselection" });
            query.show();
        }
        i18next.changeLanguage("@Session[SessionHelper.LANGUAGE_SESSION_KEY]");
        //query.toggle("fast");
        $('html, body').animate({
            scrollTop: $(this).offset().top
        }, 100);
    });

  

    //click button btnSearch
    $('body').on('click', '#btnSearch', function (e) {
        $("#results-div").html('');
        $.ajax({
            url: urlGetPcpWithFilters,
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
                var divContainChange = $('#results-div');
                divContainChange.html('');
                $('#pagination').empty();
                $('#pagination').removeData("twbs-pagination");
                $('#pagination').unbind("page");

                if (data.code == 0) {

                    if (data.recordsTotal > 0) {
                        jsondatamemory = data.records;
                        if (data.records.length >= 1000) {
                            $("#mensajedatos").text("Para mejora de la aplicación se truncaron los datos a 100 registros.");
                        }

                        data.records = data.records.sort(function (a, b) {
                            if (a.PersonId < b.PersonId)
                                return -1;
                            if (a.PersonId > b.PersonId)
                                return 1;
                            return 0;
                        });


                        var totalRows = parseInt(data.records.length);
                        var maxRow_x_page = 10;
                        var numTotal_pages = (totalRows % maxRow_x_page == 0) ? parseInt(totalRows / maxRow_x_page) : parseInt(totalRows / maxRow_x_page) + 1;
                        if (totalRows > 0) {
                            var obj = $('#pagination').twbsPagination({
                                totalPages: numTotal_pages,
                                visiblePages: 10,
                                onPageClick: function (event, page) {
                                    var htmlConcat = '';
                                    var page = parseInt(page);
                                    var min = maxRow_x_page * (page - 1) + 1;
                                    var max = maxRow_x_page * (page);

                                    $.each(data.records, function (i, item) {
                                        if ((i + 1) >= min && (i + 1) <= max) {
                                            //Build Html Section

                                            var PMGName = '';
                                            //PMGName = resultsPmg.filter(x => x.Id == item.AvailableManagedCareOrganizations[0].PmgId).PmgName;
                                            $.each(resultsPmg, function (i, el) {
                                                if (el.Id == item.AvailableManagedCareOrganizations[0].PmgId)
                                                    PMGName = el.PmgName;
                                            });

                                            console.log(ActualMcoId + ' - ' + item.AvailableManagedCareOrganizations[0].McoId);
                                            var strDisabled = '';//(ActualMcoId == item.AvailableManagedCareOrganizations[0].McoId ? 'disabled' : '');
                                            htmlConcat += $('#tmpCard').html()
                                                .replace("style='display: none'", "")
                                                .replace("labelsearchId", "labelsearch" + item.Id)
                                                .replace("chkCompare", "chkCompare" + item.Id)
                                                .replace(/PcpNameChange/g, item.Person.FullName)

                                                .replace(/AdressLineOneChange/g, item.AddressLineOne)
                                                .replace(/AdressLineTwoChange/g, item.AddressLineTwo)

                                                .replace(/NPIChange/g, item.Person.NPI)
                                                .replace(/PmgChange/g, PMGName)
                                                .replace(/McoChange/g, devuelvemco(item.AvailableManagedCareOrganizations[0].McoId));
                                            /*
                                            .replace(/toggleId/g, "toggle" + item.Id)
                                            .replace("Person", item.Person.NPI)

                                            .replace("SpecialityChange", item.Speciality.Name)
                                            .replace("expandId", "expand" + item.Id)
                                            .replace(/CityChange/g, item.City)
                                            .replace(/StateChange/g, item.State)
                                            .replace(/ZipCodeChange/g, item.ZipCode)


                                            .replace("PmgId", "Pmg" + item.AvailableManagedCareOrganizations[0].PmgId)
                                            .replace("PcpId", "Pcp" + item.Details[0].PrimaryCarePhysicianId)
                                            .replace("McoId", "Mco" + item.AvailableManagedCareOrganizations[0].McoId)

                                            .replace("CapacityMco", item.Capacity)
                                            .replace("btnApplyChangeId", "btnApplyChange" + item.Id)
                                            .replace("disabled", strDisabled);
                                        */
                                            //GPchange
                                            if ((item.AvailableManagedCareOrganizations.find(a => a.McoId == 2)) != undefined) {
                                                htmlConcat = htmlConcat.replace("GPchange", "fa fa-check fa-1x");
                                                htmlConcat = htmlConcat.replace("GPstyle", "color: green");
                                            } else {
                                                htmlConcat = htmlConcat.replace("GPchange", "fa fa-times fa-1x");
                                                htmlConcat = htmlConcat.replace("GPstyle", "color: red");
                                            };
                                            //FPchange
                                            if ((item.AvailableManagedCareOrganizations.find(a => a.McoId == 1)) != undefined) {
                                                htmlConcat = htmlConcat.replace("FPchange", "fa fa-check fa-1x");
                                                htmlConcat = htmlConcat.replace("FPstyle", "color: green");
                                            } else {
                                                htmlConcat = htmlConcat.replace("FPchange", "fa fa-times fa-1x");
                                                htmlConcat = htmlConcat.replace("FPstyle", "color: red");
                                            };
                                            //IMchange
                                            if ((item.AvailableManagedCareOrganizations.find(a => a.McoId == 3)) != undefined) {
                                                htmlConcat = htmlConcat.replace("IMchange", "fa fa-check fa-1x");
                                                htmlConcat = htmlConcat.replace("IMstyle", "color: green");
                                            } else {
                                                htmlConcat = htmlConcat.replace("IMchange", "fa fa-times fa-1x");
                                                htmlConcat = htmlConcat.replace("IMstyle", "color: red");
                                            };
                                            //PMchange
                                            if ((item.AvailableManagedCareOrganizations.find(a => a.McoId == 4)) != undefined) {
                                                htmlConcat = htmlConcat.replace("PMchange", "fa fa-check fa-1x");
                                                htmlConcat = htmlConcat.replace("PMstyle", "color: green");
                                            } else {
                                                htmlConcat = htmlConcat.replace("PMchange", "fa fa-times fa-1x");
                                                htmlConcat = htmlConcat.replace("PMstyle", "color: red");
                                            };
                                            //GMchange
                                            if ((item.AvailableManagedCareOrganizations.find(a => a.McoId == 5)) != undefined) {
                                                htmlConcat = htmlConcat.replace("GMchange", "fa fa-check fa-1x");
                                                htmlConcat = htmlConcat.replace("GMstyle", "color: green");
                                            } else {
                                                htmlConcat = htmlConcat.replace("GMchange", "fa fa-times fa-1x");
                                                htmlConcat = htmlConcat.replace("GMstyle", "color: red");
                                            };
                                            //$('#results-div').append(htmlConcat);
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
                } else {
                    swal({
                        title: "Error!",
                        text: data.message,
                        type: "error",
                        confirmButtonText: "OK"
                    });
                }

                $("#pagination .first a").attr({ "data-i18n": "datatable_first" });
                $("#pagination .prev a").attr({ "data-i18n": "datatable_previous" });
                $("#pagination .next a").attr({ "data-i18n": "datatable_next" });
                $("#pagination .last a").attr({ "data-i18n": "datatable_last" });
                i18next.changeLanguage("@Session[SessionHelper.LANGUAGE_SESSION_KEY]");

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus);
                swal({
                    title: "Error!",
                    text: textStatus,
                    type: "error",
                    confirmButtonText: "OK"
                });
            }
        });
    });
    //section html for searchPcp
    function getHtmlForSearchPcp() {
        //var htmlConcat = '<input class="toggle" id="toggleId" type="checkbox"> <label id="labelsearchId" class="label-search" for="toggleId">PcpChange <span>SpecialityChange</span></label><div class="expand" id="expandId" style="height: 0;"> <section class="expandable"><div class="row"><div class="form-group col-md-4"> <label id="NPIChange" class="NPI label-columns"><span>NPI:</span> NPIChange</label> <label id="AddressLineOneChange" class="NPI label-columns"> AdressLineOneChange</label> <label id="AddressLineTwoChange" class="AdressLineOne label-columns"> AdressLineTwoChange</label> <label id="OtherAddressGroupChange" class="OtherAddress label-columns"> CityChange</label> <label id="OtherAddressGroupChange" class="OtherAddress label-columns"> StateChange ZipCodeChange </label> <label id="PcpId" class="Pcp label-columns" style="display:none;"><span>PCP:</span> PcpChange</label> <label id="PmgId" class="Pmg label-columns" style="display:none;"><span>PMG:</span> PmgChange</label> <label id="McoId" class="Mco label-columns" style="display:none;"><span>MCO:</span> McoChange</label> <label id="CapacityMco" class="CapacityMco label-columns" style="display:none;"><span>CapacityMco:</span> CapacityMco</label></div><div id="results-available-div" class="form-group col-md-4"> <label id="GP" class="GP label-columns"><span class="GPchange" style="GPstyle"></span> MMM Multi Health</label> <label id="FP" class="FP label-columns"><span class="FPchange" style="FPstyle"></span> First Medical Plan</label> <label id="IM" class="IM label-columns"><span class="IMchange" style="IMstyle"></span> Molina Healthcare of PR</label> <label id="PM" class="PM label-columns"><span class="PMchange" style="PMstyle"></span> Plan Menorita</label> <label id="GM" class="GM label-columns"><span class="GMchange" style="GMstyle"></span> Triple S</label></div><div class="form-group col-md-4"><div class="position-relative has-icon-left" style="text-align:center;"> <button type="submit" class="btn btn-primary center-button btnApplyChangeCls" id="btnApplyChangeId" data-i18n="Enrollment_Seleccion" data-toggle="modal" data-target="#modalCart" disabled> <i class="icon-shuffle"></i> ' + i18next.t("Enrollment_Seleccion") + ' </button></div></div></div> </section></div>';
        var htmlConcat = '<input class="toggle" id="toggleId" type="checkbox"><label id="labelsearchId" class="label-search" for="toggleId">PcpChange <span>SpecialityChange</span></label><div class="expand" id="expandId" style="height: 0;"> <section class="expandable"> <div class="row"> <div class="form-group col-md-4"> <label id="NPIChange" class="NPI label-columns"><span>NPI:</span> NPIChange</label> <label id="AddressLineOneChange" class="NPI label-columns" style="display:none;"> AdressLineOneChange</label> <label id="AddressLineTwoChange" class="AdressLineOne label-columns" style="display:none;"> AdressLineTwoChange</label> <label id="OtherAddressGroupChange" class="OtherAddress label-columns" style="display:none;"> CityChange</label> <label id="OtherAddressGroupChange" class="OtherAddress label-columns" style="display:none;"> StateChange ZipCodeChange </label> <label id="PcpId" class="Pcp label-columns"><span>PCP:</span> PcpChange</label> <label id="PmgId" class="Pmg label-columns"><span>PMG:</span> PmgChange</label> <label id="McoId" class="Mco label-columns" style="display:none;"><span>MCO:</span> McoChange</label> <label id="CapacityMco" class="CapacityMco label-columns" style="display:none;"><span>CapacityMco:</span> CapacityMco</label> </div><div id="results-available-div" class="form-group col-md-4"> <label id="GP" class="GP label-columns"><span class="GPchange" style="GPstyle"></span> MMM Multi Health</label> <label id="FP" class="FP label-columns"><span class="FPchange" style="FPstyle"></span> First Medical Plan</label> <label id="IM" class="IM label-columns"><span class="IMchange" style="IMstyle"></span> Molina Healthcare of PR</label> <label id="PM" class="PM label-columns"><span class="PMchange" style="PMstyle"></span> Plan Menorita</label> <label id="GM" class="GM label-columns"><span class="GMchange" style="GMstyle"></span> Triple S</label> </div><div class="form-group col-md-4"> <div class="position-relative has-icon-left" style="text-align:center;"> <button type="submit" class="btn btn-primary center-button btnApplyChangeCls" id="btnApplyChangeId"  data-toggle="modal" data-target="#modalCart" disabled> <i class="icon-shuffle"></i> <span data-i18n="Enrollment_Seleccion">Enrollment_Seleccion</span></button> </div></div></div></section></div>';
        return htmlConcat;
    }
    //request for searchPcp
    function BuildRequestPcp() {
        var lst_McoId = null;
        var PmgId = null;
        var PcpFullName = null;
        var SpecialityId = null;
        var NPI = null;

        //lst_McoId = $("#cbMco").val() === null ? [] : $("#cbMco").val();
        lst_McoId = $("#txtPhName").val() === null ? [] : $("#cbMco").val();
        PmgId = "";// $("#cbPmg").val();
        PcpFullName = $("#txtPhName").val();
        SpecialityId = "";// $("#cbSpeciality").val();
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

    $('body').on('change', '.chkCompareCls', function (e) {
        e.preventDefault();
        if ($(this).attr('id').substring(0, 10) === "chkCompare") {
            $('#footCompare').show();
            $('#mmnCompare').show();
            var compareId = $(this).attr('id').replace('chkCompare', '');
            $('#lblCompare1').val($('#labelsearch' + compareId).val());
        }
    });

    //Click in button for popup
    $('body').on('click', '.btnApplyChangeCls', function (e) {
        e.preventDefault();

        //Get Section expand
        var pcpPmgProviderId = $(this).attr('id').replace('btnApplyChange', '');
        var expandableSection = $('#expand' + pcpPmgProviderId);

        var resultado = jsondatamemory.find(objeto => objeto.Id == pcpPmgProviderId);
        //conseguir los ids del objeto seleccionado

        UpdateMcoId = resultado.AvailableManagedCareOrganizations[0].McoId;
        UpdatePmgId = resultado.AvailableManagedCareOrganizations[0].PmgId;
        UpdatePcpId = resultado.AvailableManagedCareOrganizations[0].PrimaryCarePhysicianId;

        //Get nodes to updated

        var nodePcp = expandableSection.find('.Pcp').text();
        var PcpName = nodePcp.replace('PCP: ', '');
        //UPDATEPCP
        UpdateNPI = expandableSection.find('.Pcp').attr('id').replace('Pcp', '');

        var nodeMco = expandableSection.find('.Mco').text();
        var McoName = nodeMco.replace('MCO: ', '');
        UpdateMcoId = expandableSection.find('.Mco').attr('id').replace('Mco', '');

        var nodePmg = expandableSection.find('.Pmg').text();
        var PmgName = nodePmg.replace('PMG: ', '');
        UpdatePmgId = expandableSection.find('.Pmg').attr('id').replace('Pmg', '');


        var onlyOne = 0;
        var bMCOFirstMedical = false;
        var bMCOMMM = false;
        var bMCOMolinaHealthCare = false;
        var bMCOPlanMenonita = false;
        var bMCOTripleS = false;
        //document.getElementById('UpdateMco').innerHTML = McoName;
        if (resultado.AvailableManagedCareOrganizations.find(a => a.McoId === 1) !== undefined) {
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
        if (resultado.AvailableManagedCareOrganizations.find(a => a.McoId === 2) !== undefined) {
            $("#dMCOMMM").show();
            onlyOne += 1;
            bMCOMMM = true;
        } else {
            $("#dMCOMMM").hide();
        }
        if (resultado.AvailableManagedCareOrganizations.find(a => a.McoId === 3) !== undefined) {
            $("#dMCOMolinaHealthCare").show();
            onlyOne += 1;
            bMCOMolinaHealthCare = true;
        } else {
            $("#dMCOMolinaHealthCare").hide();
        }
        if (resultado.AvailableManagedCareOrganizations.find(a => a.McoId === 4) !== undefined) {
            $("#dMCOPlanMenonita").show();
            onlyOne += 1;
            bMCOPlanMenonita = true;
        } else {
            $("#dMCOPlanMenonita").hide();
        }
        if (resultado.AvailableManagedCareOrganizations.find(a => a.McoId === 5) !== undefined) {
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
            if (bMCOMolinaHealthCare) $("#MCOMolinaHealthCare").attr('checked', 'checked');
            if (bMCOPlanMenonita) $("#MCOPlanMenonita").attr('checked', 'checked');
            if (bMCOTripleS) $("#MCOTripleS").attr('checked', 'checked');
        }

        document.getElementById('UpdatePmg').innerHTML = PmgName;
        document.getElementById('UpdatePcp').innerHTML = PcpName;
    });
    //click button btnApplyPcp
    $('body').on('click', '#btnApplyPcp', function (e) {
        if ($('#chkSendEmail').is(':checked'))
            if (validarEmail($('#txtEmail').val()) === false) {
                swal({
                    title: i18next.t('common_warning'),
                    text: "La dirección de email es incorrecta!.",
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

        //validar Periodo Erollment
        var bool = true;

        swal({
            title: i18next.t('common_Sure'),
            text: i18next.t('common_Confirm'),
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: i18next.t('common_ButtonYes'),
            cancelButtonText: i18next.t('common_ButtonNo'),
            closeOnConfirm: false,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        url: urlGetEnrollmentPeriod,
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
                        async: false,
                        success: function (data) {
                            console.log(data);

                            if (data.code === -300) {
                                bool = false;
                            } else {
                                bool = data.records.Enabled;
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            swal({
                                title: "Error!",
                                text: "No se pudo registrar la transacción",
                                type: "error",
                                confirmButtonText: "OK"
                            });
                        }
                    });

                    //TODO: HOMAR Quitar
                    //bool = true;
                    if (!bool) {
                        swal({
                            title: "Advertencia!",
                            text: "Esta Fuera del Periodo de Enrollment",
                            type: "warning",
                            confirmButtonText: "OK"
                        });
                        return;
                    }

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
                        async: false,
                        success: function (data) {
                            if (data.code == 0) {
                                //Send Email if checkEmail
                                // if ($('#chkSendEmail').is(':checked')) {
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
                                            }, function () {
                                                $.redirect(UrlThisView,
                                                    {
                                                        "ApplicationMemberID": ApplicationMemberID
                                                    });
                                            });
                                        } else {
                                            swal({
                                                title: "Exito!",
                                                text: "Se registro correctamente el cambio",
                                                type: "success",
                                                confirmButtonText: "OK"
                                            }, function () {
                                                $.redirect(UrlThisView,
                                                    {
                                                        "ApplicationMemberID": ApplicationMemberID
                                                    });
                                            });
                                            //swal({
                                            //    title: "Advertencia!",
                                            //    text: "Se realizó el registro. No se pudo enviar el mail",
                                            //    type: "warning",
                                            //    confirmButtonText: "OK"
                                            //}, function () {
                                            //    $.redirect(UrlThisView,
                                            //        {
                                            //            "ApplicationMemberID": ApplicationMemberID
                                            //        });
                                            //});
                                        }
                                    },
                                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                                        swal({
                                            title: "Exito!",
                                            text: "Se registro correctamente el cambio",
                                            type: "success",
                                            confirmButtonText: "OK"
                                        });
                                        //swal({
                                        //    title: "Error!",
                                        //    text: textStatus,
                                        //    type: "error",
                                        //    confirmButtonText: "OK"
                                        //});
                                    }
                                });
                            }
                            //else {
                            //        swal({
                            //            title: "Exito!",
                            //            text: "Se registro correctamente el cambio.",
                            //            type: "success",
                            //            confirmButtonText: "OK"
                            //        }, function () {
                            //            $.redirect(UrlThisView,
                            //                {
                            //                    "ApplicationMemberID": ApplicationMemberID
                            //                });
                            //        });
                            //    }
                            else {


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
                                        text: "No se pudo registrar la transacción",
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
            }
        );
    });

    //request for PcpApply
    function BuildRequestPcpApply() {
        justCause = $('#txtCommentJustCausa').val();
        //console.log("hola")
        var request = new Object();

        request.MemberId = ApplicationMemberID;
        request.McoId = UpdateMcoId;
        request.PcpId = UpdateNPI;
        request.PmgId = UpdatePmgId;
        request.Origin = 40;
        request.UserName = 'Admin';
        request.IgnoreValidationRules = false;
        request.FilePDFChangeEnrollment = FilePDFChangeEnrollment;
        request.Permission = permission;
        request.JustCause = justCause;
        return request;
    }
});

function LoadPage() {
    //Fill general data

    $.ajax({
        url: urlGetMembersByApplicationMemberID,
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
            "ApplicationMemberID": ApplicationMemberID
        },
        type: 'POST',
        async: true,
        success: function (data) {
            if (data.code == 0) {
                if (data.recordsTotal > 0) {
                    var obj = data.records[0];
                    PersonId = obj.Id;
                    IdMember = PersonId;
                    ActualMcoId = obj.MCOId;
                    ActualNPI = obj.PCPId;
                    ActualPmgId = obj.PMGId;
                    $("#txtContactMPI").val(obj.Family.ContactFullName);
                    $("#txtBeneficiary").val(obj.MemberFullName);
                    $("#txtMPIDatosGenerales").val(obj.MPIShort);
                    $("#txtActualPcp").val((obj.PCP === null ? '' : (obj.PCP === null ? '' : (obj.PCP.Person === null ? '' : obj.PCP.Person.FullName))));
                    $("#txtActualPmg").val((obj.PMG === null ? '' : obj.PMG.PmgName));
                    $("#txtActualMco").val((obj.MCO === null ? '' : obj.MCO.CarrierName));
                    $("#txtEligibility").val(obj.Elegibility);
                    $("#txtEfectivityDate").val(formatDateEN(obj.Family.EffectiveDate));
                    //$("#txtExpirationDate").val(formatDateEN(obj.ExpirationDate));
                    $("#txtComment").val(obj.Reason);
                    $("#txtContactMPITab2").val(obj.Family.ContactFullName);
                    $("#txtContactMPITab3").val(obj.Family.ContactFullName);
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



                    document.getElementById('ActualMco').innerHTML = $("#txtActualMco").val();
                    document.getElementById('ActualPmg').innerHTML = obj.PMG.PmgName;
                    document.getElementById('ActualPcp').innerHTML = obj.PCP.Person.FullName;
                    //Section for autocomplete Phisycian Name
                    $.ajax({
                        url: urlGetAllPcp,
                        dataType: 'json',
                        type: 'POST',
                        async: true,
                        success: function (data) {
                            if (data.code == 0) {
                                if (data.recordsTotal > 0) {
                                    $.each(data.records, function (index, item) {
                                        AvailablePcpName.push({ label: item.FullName });
                                    });
                                }
                                //autocomplete
                                $("#txtPhName").autocomplete({
                                    source: AvailablePcpName,
                                    minLength: 1
                                });
                            }
                        }
                    });

                    listFile(PersonId);
                    configValidateFiles();

                    //Section for load table History
                    $.ajax({
                        url: urlGetEnrollmentHistoryByPersonId,
                        dataType: 'json',
                        data: {
                            "PersonId": PersonId
                        },
                        type: 'POST',
                        async: true,
                        success: function (data) {
                            //console.log(data.recordsTotal);
                            if (data.code == 0) {
                                if (data.recordsTotal > 0) {
                                    //LoadTable
                                    LoadTableSearchHistory('#tbHistorySearch', data.records);
                                    //Scroll into table only show if table width is bigger than div container
                                    PutCustomScrollIntoDataTable('#tbHistorySearch');
                                }
                            }
                        }
                    });

                    //Section for OverCapacities
                    $.ajax({
                        url: urlGetAllOvercapacity,
                        dataType: 'json',
                        async: true,
                        success: function (data) {
                            //console.log(data);
                            if (data.code == 0) {
                                jsonover = data;
                                //if (data.recordsTotal > 0) {
                                //    $.each(data.records, function (index, item) {
                                //        AvailablePcpName.push({ label: item.FullName });
                                //    });
                                //}
                                ////autocomplete
                                //$("#txtPhName").autocomplete({
                                //    source: AvailablePcpName,
                                //    minLength: 1
                                //});
                            }
                        }
                    });

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
    //Section for Pmg's
    $.ajax({
        url: urlGetAllPmg,
        dataType: 'json',
        type: 'POST',
        async: true,
        success: function (data) {
            $.each(data.records, function (index, item) {
                resultsPmg.push({
                    Id: item.Id,
                    PmgName: item.PmgName
                });
            });
        }
    });
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
    //$(tableSection).DataTable().row(tableSectin).remove().draw();
    i18next.changeLanguage("@Session[SessionHelper.LANGUAGE_SESSION_KEY]");

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
        responsive: {
            details: {
                renderer: function (api, rowIdx, columns) {
                    var data = $.map(columns, function (col, i) {
                        return col.hidden ?
                            '<tr  data-dt-row="' + col.rowIndex + '" data-dt-column="' + col.columnIndex + '">' +
                            '<td><b>' + col.title + ':' + '</b></td> ' +
                            '<td>' + col.data + '</td>' +
                            '</tr>' :
                            '';
                    }).join('');

                    return data ?
                        $('<table/>').append(data) :
                        false;
                }
            }
        },
        "autoWidth": true,
        "scrollX": true,
        "paging": false,
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
            { "title": i18next.t('Enrollment_User'), "data": "MCOModifiedBy", "name": "MCOModifiedBy" },
            { "title": i18next.t('Enrollment_ChangeSource'), "data": "MCOModifiedSource", "name": "MCOModifiedSource" },
            {
                title: i18next.t('Enrollment_ChangeDate'), data: null, render: function (data, type, row) {
                    return formatDateEN(row.CreatedOn);
                }
            },
            {
                title: i18next.t('Enrollment_MCO'), data: null, class: "bolder", render: function (data, type, row) {
                    if (row.MCO.CarrierName !== null) {
                        var check = '<div class="inline-Block"> <img class="size12" src="/Enrollment/app-assets/assets/images/CheckMark.svg"/> </div>';
                        return check + row.MCO.CarrierName;
                    } else
                        return '';
                }
            },
            {
                title: i18next.t('Enrollment_PMG'), data: null, class: "bolder", render: function (data, type, row) {
                    console.log(row.PMG.PmgName);
                    if (row.PMG.PmgName !== null) {
                        var check = '<div class="inline-Block"> <img class="size12" src="/Enrollment/app-assets/assets/images/CheckMark.svg"/> </div>';
                        return check + row.PMG.PmgName;
                    } else
                        return '';
                }
            },
            {
                title: i18next.t('Enrollment_PCP'), data: null, class: "bolder", render: function (data, type, row) {
                    if (row.PCP.Person.FullName !== null) {
                        var check = '<div class="inline-Block"> <img class="size12" src="/Enrollment/app-assets/assets/images/CheckMark.svg"/> </div>';
                        return check + row.PCP.Person.FullName;
                    } else
                        return '';
                }
            },
            {
                title: 'PDF', data: null, class: "bolder", render: function (data, type, row) {
                    if (row.filePDF !== '' && row.filePDF !== null) {
                        return '<a style="line-height:0.1" data-i18n="SearchPerson_Header_View" class="btn btn-info GoEnrollmentPage" href="#" id="SeePDF' + row.Id + '" onClick="seePDF(\'' + row.filePDF + '\');"></a>';
                        //return "<a style='line-height:0.1' data-i18n='SearchPerson_Header_View' class='btn btn-info GoEnrollmentPage' href='#' id='SeePDF" + row.Id + "' onClick='seePDF(\'" + row.filePDF + "\');\'></a>";
                        //return "<a style='line-height:0.1' class='btn btn-info GoEnrollmentPage' href='#' id='SeePDF" + row.Id + "' onClick='seePDF('" + row.filePDF + "');>" + i18next.t('SearchPerson_Header_View') + "</a>";
                    } else
                        return '';
                }
            }
        ]
    });
    i18next.changeLanguage("@Session[SessionHelper.LANGUAGE_SESSION_KEY]");
}

function LoadTableFiles(tableSection, JSonData, PersonId) {
    //$(tableSection).DataTable().destroy();
    //$(tableSection).DataTable().clear().draw();
    //$(tableSection).DataTable().row().remove().draw();
    //$(tableSection).DataTable().row().delete();
    //$(tableSection).DataTable().row().delete();
    //alert($(tableSection)[0].tagname);
    i18next.changeLanguage("@Session[SessionHelper.LANGUAGE_SESSION_KEY]");

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
        responsive: {
            details: {
                renderer: function (api, rowIdx, columns) {
                    var data = $.map(columns, function (col, i) {
                        return col.hidden ?
                            '<tr  data-dt-row="' + col.rowIndex + '" data-dt-column="' + col.columnIndex + '">' +
                            '<td><b>' + col.title + ':' + '</b></td> ' +
                            '<td>' + col.data + '</td>' +
                            '</tr>' :
                            '';
                    }).join('');

                    return data ?
                        $('<table/>').append(data) :
                        false;
                }
            }
        },
        "autoWidth": true,
        //"scrollX": true,
        "paging": false,
        "ordering": true,
        "processing": false, // for show progress bar
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

    i18next.changeLanguage("@Session[SessionHelper.LANGUAGE_SESSION_KEY]");

}
function SendEnrollmentPeriod() {

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
                title: "Error!",
                text: textStatus,
                type: "error",
                confirmButtonText: "OK"
            });
        }
    });
}

function configValidateFiles() {
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
            if (data.code == 0) {
                if (data.recordsTotal > 0) {
                    //LoadTable
                    LoadTableFiles('#tbFiles', data.records, PersonId);
                    //Scroll into table only show if table width is bigger than div container
                    PutCustomScrollIntoDataTable('#tbFiles');
                }
            }
        }
    });
}

function seeFile(FileId) {
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
                listFile(PersonId);
            }
        }
    });
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

