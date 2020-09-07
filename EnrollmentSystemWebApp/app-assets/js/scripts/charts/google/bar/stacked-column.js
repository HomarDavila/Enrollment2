/*=========================================================================================
    File Name: column-stacked.js
    Description: google stacked column bar chart
    ----------------------------------------------------------------------------------------
    Item Name: Robust - Responsive Admin Theme
    Version: 1.2
    Author: PIXINVENT
    Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/

// Stacked column bar chart
// ------------------------------

// Load the Visualization API and the corechart package.
google.load('visualization', '1.0', {'packages':['corechart']});

// Set a callback to run when the Google Visualization API is loaded.
google.setOnLoadCallback(drawColumnStacked);

// Callback that creates and populates a data table, instantiates the pie chart, passes in the data and draws it.
function drawColumnStacked() {

    // Create the data table.
    var data = google.visualization.arrayToDataTable([
        ['Genre', 'PMG Changes', 'MCO Changes','PCP Changes' ,{ role: 'annotation' } ],
        ['12-Jul', 10, 15, 25,'']
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
            gridlines:{
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
    var options_column_stacked2 = {
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
    var options_column_stacked3 = {
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
    var bar = new google.visualization.ColumnChart(document.getElementById('stacked-column-chart'));
    var bar2 = new google.visualization.ColumnChart(document.getElementById('stacked-column-chart2'));
    var bar3 = new google.visualization.ColumnChart(document.getElementById('stacked-column-chart3'));
    bar.draw(data, options_column_stacked);
    bar2.draw(data, options_column_stacked2);
    bar3.draw(data, options_column_stacked3);

}


// Resize chart
// ------------------------------

$(function () {

    // Resize chart on menu width change and window resize
    $(window).on('resize', resize);
    $(".menu-toggle").on('click', resize);

    // Resize function
    function resize() {
        drawColumnStacked();
    }
});