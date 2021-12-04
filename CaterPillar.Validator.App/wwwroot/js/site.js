// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: '/Home/GetMonthlyStatistics', // <-- Where should this point?
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (xhr, status, errorThrown) {
            var err = "Status: " + status + " " + errorThrown;
            console.log(err);
        }
    }).done(function (monthlyData) {
        if (monthlyData) {
            var monthlyDataArray = [['Date', 'Total Records', 'Valid Records', 'Invalid Records']];
            $.each(monthlyData, function (index, dailyData) {
                console.log(dailyData);
                var dailyDataArray = [dailyData.date, dailyData.dailyStatistics.countOfRecords, dailyData.dailyStatistics.countOfValidRecords, dailyData.dailyStatistics.countOfInvalidRecords];
                monthlyDataArray.push(dailyDataArray);
            });
            google.charts.load('current', { 'packages': ['bar'] });
            google.charts.setOnLoadCallback(drawChart);
            console.log(monthlyDataArray);
            function drawChart() {
                var data = google.visualization.arrayToDataTable(monthlyDataArray);

                var options = {
                    chart: {
                        title: 'Sales Validation',
                        subtitle: 'Total, Valid and Invalid Records',
                    },
                    bars: 'vertical',
                    vAxis: { format: 'decimal' },
                    height: 400,
                    colors: ['#1b9e77', '#d95f02', '#7570b3']
                };

                var chart = new google.charts.Bar(document.getElementById('chart_div'));

                chart.draw(data, google.charts.Bar.convertOptions(options));
            }
        }
    })
});
