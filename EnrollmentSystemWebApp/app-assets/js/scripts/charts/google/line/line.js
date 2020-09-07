/*=========================================================================================
    File Name: line.js
    Description: google line chart
    ----------------------------------------------------------------------------------------
    Item Name: Robust - Responsive Admin Theme
    Version: 1.2
    Author: PIXINVENT
    Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/

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
    var line = new google.visualization.LineChart(document.getElementById('line-chart'));
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