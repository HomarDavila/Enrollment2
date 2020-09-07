$(document).ready(function () {

    InitializeDatePicker();

    $("#btnSearch").click(function () {
        let startDate = $("#StartDate").val();
        let endDate = $("#EndDate").val();
        let validateDates = ValidateDates(startDate, endDate);
        if (validateDates) {
            GetReportsWeb();
        } else {
            swal({
                title: i18next.t('common_warning'),
                text: i18next.t('Report_Validation_Fecha'),
                type: "warning",
                confirmButtonText: "OK"
            });
        }
    });
});

function InitializeDatePicker() {
    $('#StartDate').pickadate({
        max: true,
        selectMonths: true,
        selectYears: 70,
        closeOnClear: true,
        format: 'mm/dd/yyyy',
        formatSubmit: 'yyyy-mm-dd',
        hiddenPrefix: 'standardDate__',
    });

    $('#EndDate').pickadate({
        max: true,
        selectMonths: true,
        selectYears: 70,
        closeOnClear: true,
        format: 'mm/dd/yyyy',
        formatSubmit: 'yyyy-mm-dd',
        hiddenPrefix: 'standardDate__',
    });
}

function ValidateDates(startDate, endDate) {
    let validate = false;
    let empty = false;
    let range = false;
    let sd = Date.parse(startDate);
    let ed = Date.parse(endDate);

    (startDate && endDate) ? empty = true : empty = false;
    (ed > sd) ? range = true : range = false;
    (empty && range) ? validate = true : validate = false;

    return validate;
};

function GetReportsWeb() {

    $.ajax({
        url: urlGetReportsWeb,
        dataType: 'json',
        data: BuildRequestReportsWeb(),
        type: 'POST',
        async: true,
        success: function (data) {
            console.log(data);

            if (data.Code === 0) {
                //swal({
                //    title: i18next.t('common_success'),
                //    text: data.message,
                //    type: "success",
                //    confirmButtonText: "OK"
                //});
                let reports = GetArrayOfTypes(data.listado);

                let report1 = google.visualization.arrayToDataTable(reports[0]);
                let report2 = google.visualization.arrayToDataTable(reports[1]);
                let report3 = google.visualization.arrayToDataTable(reports[2]);
                let report4 = google.visualization.arrayToDataTable(reports[3]);

                let line1 = new google.visualization.LineChart(document.getElementById('line-chart1'));
                let line2 = new google.visualization.LineChart(document.getElementById('line-chart2'));
                let line3 = new google.visualization.LineChart(document.getElementById('line-chart3'));
                let line4 = new google.visualization.LineChart(document.getElementById('line-chart4'));

                let options_line = {
                    height: 400,
                    fontSize: 12,
                    curveType: 'function',
                    colors: ['#37BC9B', '#DA4453'],
                    pointSize: 5,
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

                line1.draw(report1, options_line);
                line2.draw(report2, options_line);
                line3.draw(report3, options_line);
                line4.draw(report4, options_line);
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

function BuildRequestReportsWeb() {

    var request = new Object();
    var StartDate = null;
    var EndDate = null;

    StartDate = $("#StartDate").val();
    EndDate = $("#EndDate").val();

    request.StartDate = StartDate;
    request.EndDate = EndDate;

    return request;
}

function GetArrayOfTypes(list) {
    let ArrType1 = [];
    let ArrType2 = [];
    let ArrType3 = [];
    let ArrType4 = [];

    ArrType1.push(['Year', '']);
    ArrType2.push(['Year', '']);
    ArrType3.push(['Year', '']);
    ArrType4.push(['Year', '']);

    list.forEach(function (item, index) {
        let date = new Date(item.CreatedOnFormated);
        let day = date.getDate();
        let month = date.getMonth() + 1;
        let dateFormat = day + '-' + GetMonthName(month).substr(0, 3);

        let arr1 = [dateFormat, item.Type1Count];
        let arr2 = [dateFormat, item.Type2Count];
        let arr3 = [dateFormat, item.Type3Count];
        let arr4 = [dateFormat, item.Type4Count];

        ArrType1.push(arr1);
        ArrType2.push(arr2);
        ArrType3.push(arr3);
        ArrType4.push(arr4);
    });

    return [ArrType1, ArrType2, ArrType3, ArrType4];
}

function GetMonthName(monthNumber) {
    let monthName = '';
    switch (monthNumber) {
        case 1:
            monthName = i18next.t('enero');
            break;
        case 2:
            monthName = i18next.t('febrero');
            break;
        case 3:
            monthName = i18next.t('marzo');
            break;
        case 4:
            monthName = i18next.t('abril');
            break;
        case 5:
            monthName = i18next.t('mayo');
            break;
        case 6:
            monthName = i18next.t('junio');
            break;
        case 7:
            monthName = i18next.t('julio');
            break;
        case 8:
            monthName = i18next.t('agosto');
            break;
        case 9:
            monthName = i18next.t('setiembre');
            break;
        case 10:
            monthName = i18next.t('octubre');
            break;
        case 11:
            monthName = i18next.t('noviembre');
            break;
        case 12:
            monthName = i18next.t('diciembre');
            break;
        default:
    }
    return monthName;
}


// Line chart
// ------------------------------

// Load the Visualization API and the corechart package.
google.load('visualization', '1.0', { 'packages': ['corechart'] });

// Set a callback to run when the Google Visualization API is loaded.
google.setOnLoadCallback(drawLine);

// Callback that creates and populates a data table, instantiates the line chart, passes in the data and draws it.
function drawLine() {

    // Create the data table.
    var data = google.visualization.arrayToDataTable([
        ['Year', ''],
        ['8-Jul', 20],
        ['9-Jul', 50],
        ['10-Jul', 40],
        ['11-Jul', 55]
    ]);
    var data2 = google.visualization.arrayToDataTable([
        ['Year', ''],
        ['8-Jul', 40],
        ['9-Jul', 50],
        ['10-Jul', 60],
        ['11-Jul', 75]
    ]);
    var data3 = google.visualization.arrayToDataTable([
        ['Year', ''],
        ['8-Jul', 20],
        ['9-Jul', 20],
        ['10-Jul', 30],
        ['11-Jul', 45]
    ]);
    var data4 = google.visualization.arrayToDataTable([
        ['Year', ''],
        ['8-Jul', 10],
        ['9-Jul', 10],
        ['10-Jul', 20],
        ['11-Jul', 25]
    ]);


    // Set chart options
    var options_line = {
        height: 400,
        fontSize: 12,
        curveType: 'function',
        colors: ['#37BC9B', '#DA4453'],
        pointSize: 5,
        chartArea: {
            left: '5%',
            width: '90%',
            height: 350
        },
        vAxis: {
            format: '0',
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

    //Instantiate and draw our chart, passing in some options.
    var line = new google.visualization.LineChart(document.getElementById('line-chart1'));
    var line2 = new google.visualization.LineChart(document.getElementById('line-chart2'));
    var line3 = new google.visualization.LineChart(document.getElementById('line-chart3'));
    var line4 = new google.visualization.LineChart(document.getElementById('line-chart4'));

    line.draw(data, options_line);
    line2.draw(data2, options_line);
    line3.draw(data3, options_line);
    line4.draw(data4, options_line);

}


// Resize chart
// ------------------------------

$(function () {

    // Resize chart on menu width change and window resize
    $(window).on('resize', resize);
    $(".menu-toggle").on('click', resize);

    // Resize function
    function resize() {
        drawLine();
    }
});