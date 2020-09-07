/*=========================================================================================
    File Name: pie.js
    Description: google pie chart
    ----------------------------------------------------------------------------------------
    Item Name: Robust - Responsive Admin Theme
    Version: 1.2
    Author: PIXINVENT
    Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/

// Pie chart
// ------------------------------

// Load the Visualization API and the corechart package.
google.load('visualization', '1.0', { 'packages': ['corechart'] });

// Set a callback to run when the Google Visualization API is loaded.
google.setOnLoadCallback(drawPie);

// Callback that creates and populates a data table, instantiates the pie chart, passes in the data and draws it.
function drawPie() {

    // Create the data table.
    var data = google.visualization.arrayToDataTable([
        ['Task', 'Hours per Day'],
        ['Uptime Percentage', 11],
        ['Downtime Percentage', 2]
    ]);


    // Set chart options
    var options_bar = {
        title: '',
        height: 400,
        fontSize: 12,
        colors: ['#02B8AB', '#364548'],
        chartArea: {
            left: '5%',
            width: '90%',
            height: 350
        },
    };

     //Instantiate and draw our chart, passing in some options.
    var bar = new google.visualization.PieChart(document.getElementById('donut-chart'));
    bar.draw(data, options_bar);

    //var bar2 = new google.visualization.PieChart(document.getElementById('pie-chart2'));
    //bar2.draw(data, options_bar);

}


// Resize chart
// ------------------------------

$(function () {

    // Resize chart on menu width change and window resize
    $(window).on('resize', resize);
    $(".menu-toggle").on('click', resize);

    // Resize function
    function resize() {
        drawPie();
    }
});