// Load the Visualization API and the corechart package.
google.load('visualization', '1.0', { 'packages': ['corechart'] });

// Set a callback to run when the Google Visualization API is loaded.
//google.setOnLoadCallback(drawColumnStacked);

$(document).ready(function () {

    $("#btnBuscar").click(function () {
        if ($("#DateFilter").val() !== '') {
            GetReportJustaCausa();
        } else {
            swal({
                title: i18next.t('common_warning'),
                text: i18next.t('Report_Validation_Fecha'),
                type: "warning",
                confirmButtonText: "OK"
            });
        }
    });

    var $startpicker = $('#DateFilter').pickadate({
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

function GetReportJustaCausa() {

    var request = new Object();
    var parts = $("#DateFilter").val().split('/');
    var mydate = new Date(parts[2], parts[0] - 1, parts[1]);
    //obj.DateOfBirth = mydate.toJSON();
    request.fecha = mydate.toJSON();
    request.inscripcion = 'ASSIST';

    $.ajax({
        url: urlGetReportJustaCausa,
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
        data: request,
        type: 'POST',
        async: true,
        success: function (data) {
            console.log(data);

            if (data.code === 0) {
                // Resize chart on menu width change and window resize
                //$(window).on('resize', resize);
                //$(".menu-toggle").on('click', resize);

                //// Resize function
                //function resize() {
                //    drawColumnStacked(data.records);
                //}
                // Loop the array of objects

                // Create the data table.
                var datos = google.visualization.arrayToDataTable([
                    ['Genre', i18next.t('Report_PMG'), i18next.t('Report_MCO'), i18next.t('Report_PCP'), { role: 'annotation' }],
                    [$("#DateFilter").val(), data.records.TotalPMG, data.records.TotalMCO, data.records.TotalPCP, '']
                ]);

                // Set chart options
                var options_column_stacked = {
                    height: 400,
                    fontSize: 12,
                    colors: ['#02B8AB', '#02B8AB', '#02B8AB'],
                    chartArea: {
                        left: '5%',
                        width: '90%',
                        height: 350
                    },
                    vAxis: {
                        gridlines: {
                            color: '#e9e9e9',
                            count: 10
                        },
                        minValue: 0
                    },
                    legend: {
                        position: 'top',
                        alignment: 'center',
                        textStyle: {
                            fontSize: 12
                        }
                    }
                };

                // Instantiate and draw our chart, passing in some options.
                var bar = new google.visualization.ColumnChart(document.getElementById('stacked-column-chart'));
                bar.draw(datos, options_column_stacked);
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

    request.inscripcion = 'ASES';

    $.ajax({
        url: urlGetReportJustaCausa,
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
        data: request,
        type: 'POST',
        async: true,
        success: function (data) {
            console.log(data);

            if (data.code === 0) {
                // Resize chart on menu width change and window resize
                //$(window).on('resize', resize);
                //$(".menu-toggle").on('click', resize);

                //// Resize function
                //function resize() {
                //    drawColumnStacked(data.records);
                //}
                // Loop the array of objects

                // Create the data table.
                var datos = google.visualization.arrayToDataTable([
                    ['Genre', i18next.t('Report_PMG'), i18next.t('Report_MCO'), i18next.t('Report_PCP'), { role: 'annotation' }],
                    [$("#DateFilter").val(), data.records.TotalPMG, data.records.TotalMCO, data.records.TotalPCP, '']
                ]);

                // Set chart options
                var options_column_stacked = {
                    height: 400,
                    fontSize: 12,
                    colors: ['#374649', '#374649', '#374649'],
                    chartArea: {
                        left: '5%',
                        width: '90%',
                        height: 350
                    },
                    vAxis: {
                        gridlines: {
                            color: '#e9e9e9',
                            count: 10
                        },
                        minValue: 0
                    },
                    legend: {
                        position: 'top',
                        alignment: 'center',
                        textStyle: {
                            fontSize: 12
                        }
                    }
                };

                // Instantiate and draw our chart, passing in some options.
                var bar = new google.visualization.ColumnChart(document.getElementById('stacked-column-chart2'));
                bar.draw(datos, options_column_stacked);
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


    request.inscripcion = 'SELFSERVICE';

    $.ajax({
        url: urlGetReportJustaCausa,
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
        data: request,
        type: 'POST',
        async: true,
        success: function (data) {
            console.log(data);

            if (data.code === 0) {
                // Resize chart on menu width change and window resize
                //$(window).on('resize', resize);
                //$(".menu-toggle").on('click', resize);

                //// Resize function
                //function resize() {
                //    drawColumnStacked(data.records);
                //}
                // Loop the array of objects

                // Create the data table.
                var datos = google.visualization.arrayToDataTable([
                    ['Genre', i18next.t('Report_PMG'), i18next.t('Report_MCO'), i18next.t('Report_PCP'), { role: 'annotation' }],
                    [$("#DateFilter").val(), data.records.TotalPMG, data.records.TotalMCO, data.records.TotalPCP, '']
                ]);

                var options_column_stacked = {
                    height: 400,
                    fontSize: 12,
                    colors: ['#FF6160', '#FF6160', '#FF6160'],
                    chartArea: {
                        left: '5%',
                        width: '90%',
                        height: 350
                    },
                    vAxis: {
                        gridlines: {
                            color: '#e9e9e9',
                            count: 10
                        },
                        minValue: 0
                    },
                    legend: {
                        position: 'top',
                        alignment: 'center',
                        textStyle: {
                            fontSize: 12
                        }
                    }
                };

                // Instantiate and draw our chart, passing in some options.
                var bar = new google.visualization.ColumnChart(document.getElementById('stacked-column-chart3'));
                bar.draw(datos, options_column_stacked);
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