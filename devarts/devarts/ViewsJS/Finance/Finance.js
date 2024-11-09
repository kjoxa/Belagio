
//$("#testDiv").click(function () {
//    $.getJSON("@Url.Action("ShowText","Finance")", { json: "jakis string" }, function (res) {
//        let objects = JSON.parse(res);
//        $("#testDiv").html("<h1>" + objects.monthsExpense + "</h1>");
//    });
//});

function OnEditSuccessOperations() {
    $('#editFinanceModal').modal('hide');
    var table = $('#financeDataTable').DataTable();
    table.ajax.reload();
}

function OnSuccessOperations() {
    $('#addFinanceModal').modal('hide');
    var table = $('#financeDataTable').DataTable();
    table.ajax.reload();
    UpdateBarData();
}

$(document).ready(function () {

    var barChartLabelsJS = $('#barChartLabelsJS').data("barchartekjs"); barChartLabelsJS = barChartLabelsJS.replace(/'/g, "");
    var barChartExpense = $('#barchartexpense').data("barchartexpense");
    var barChartIncome = $('#barchartincome').data("barchartincome");
    var urlBarChartUpdate = $('#urlbarchartupdate').data("urlbarchartupdate");
    var pieChartData = $('#piechartdata').data("piechartdata");

    console.log(barChartExpense);
    console.log(barChartIncome);

    $("#addPosition").click(function () {
        $('#addFinanceModal').modal('show');
    });

    $("#editFinance").click(function () {
        $('#editFinanceModal').modal('show');
    });

    $(document).on('click', ".removeFinance", function () {
        var table = $('#financeDataTable').DataTable();
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        $.ajax(
            {
                type: "GET",
                url: "/Finance/RemoveFinance",
                data: { "Id": id }
            }).success(function () {
                table.ajax.reload();
                newAlert('success', 'Sukces', 'Pozycja została usunięta');
                UpdateBarData();
            }).error(function () {
                table.ajax.reload();
                newAlert('danger', 'Błąd', 'Nie można usunąć danej pozycji.');
            });
    });

    var editFinanceURL = '/Finance/EditFinance';
    $(document).on('click', ".editFinance", function () {

        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: editFinanceURL,
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {

                $('#editFinanceModalContent').html(data);
                $('#editFinanceModal').modal(options);
                $('#editFinanceModal').modal('show');

            },
            error: function () {
                alert("Błąd ładowania danych.");
            }
        });
    });

    // WYKRESY
    if ($('#mybarChartek').length) {
        var ctx = document.getElementById("mybarChartek");
        var mybarChartek = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: Array.from(barChartLabelsJS.split(",")),
                datasets: [{
                    label: 'Wydatki',
                    backgroundColor: "#03586A",
                    data: barChartExpense
                },
                {
                    label: 'Przychody',
                    backgroundColor: "#26B99A",
                    data: barChartIncome
                }]
            },

            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
    }

    UpdateBarData = function () {
        $.getJSON(urlBarChartUpdate, { year: 2022 }, function (res) {
            var objects = JSON.parse(res);
            //$("#testDiv").html("<h1>" + objects.monthsExpense + "</h1>");
            mybarChartek.data.datasets[0].data = objects.monthsExpense;
            mybarChartek.data.datasets[1].data = objects.monthsIncome;
            mybarChartek.update();
            console.log("Zaktualizowano BarChart");
        });
    }

    $('#testDiv').bind('click', function () {
        UpdateBarData(data);
    });

    if ($('#lineChartek').length) {

        ctx = document.getElementById("lineChartek");
        var lineChartek = new Chart(ctx, {
            type: 'line',
            data: {
                labels: Array.from(barChartLabelsJS.split(",")),
                datasets: [{
                    label: "Wydatki",
                    backgroundColor: "rgba(38, 185, 154, 0.31)",
                    borderColor: "rgba(38, 185, 154, 0.7)",
                    pointBorderColor: "rgba(38, 185, 154, 0.7)",
                    pointBackgroundColor: "rgba(38, 185, 154, 0.7)",
                    pointHoverBackgroundColor: "#fff",
                    pointHoverBorderColor: "rgba(220,220,220,1)",
                    pointBorderWidth: 1,
                    data: barChartExpense
                }, {
                    label: "Przychody",
                    backgroundColor: "rgba(3, 88, 106, 0.3)",
                    borderColor: "rgba(3, 88, 106, 0.70)",
                    pointBorderColor: "rgba(3, 88, 106, 0.70)",
                    pointBackgroundColor: "rgba(3, 88, 106, 0.70)",
                    pointHoverBackgroundColor: "#fff",
                    pointHoverBorderColor: "rgba(151,187,205,1)",
                    pointBorderWidth: 1,
                    data: barChartIncome
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false
            }
        });

    }

    // Pie chart
    if ($('#pieChartek').length) {
        var ctx = document.getElementById("pieChartek");
        var data = {
            datasets: [{
                data: pieChartData,
                backgroundColor: [
                    "#455C73",
                    "#9B59B6",
                    "#BDC3C7",
                    "#26B99A",
                    "#3498DB",
                    "#9B59B6",
                    "#BDC3C7",
                    "#26B99A",
                    "#3498DB"
                ],
                label: 'My dataset' // for legend
            }],
            labels: [
                "Odżywianie",
                "Leczenie",
                "Profilaktyka",
                "Krycia",
                "Sprzedaż",
                "Akcesoria",
                "Wystawy",
                "Sport",
                "Inne"
            ]
        };

        var pieChart = new Chart(ctx, {
            data: data,
            type: 'pie',
            otpions: {
                legend: false
            }
        });
    }

    if ($('#donut').length) {

        var ctx = document.getElementById("donut");
        var data = {
            labels: [
                "Odżywianie",
                "Leczenie",
                "Profilaktyka",
                "Krycia",
                "Sprzedaż",
                "Akcesoria",
                "Wystawy",
                "Sport",
                "Inne"
            ],
            datasets: [{
                data: pieChartData,
                backgroundColor: [
                    "#455C73",
                    "#9B59B6",
                    "#BDC3C7",
                    "#26B99A",
                    "#3498DB",
                    "#9B59B6",
                    "#BDC3C7",
                    "#26B99A",
                    "#3498DB"
                ],
                hoverBackgroundColor: [
                    "#34495E",
                    "#B370CF",
                    "#CFD4D8",
                    "#36CAAB",
                    "#49A9EA"
                ]

            }]
        };

        var donut = new Chart(ctx, {
            type: 'doughnut',
            tooltipFillColor: "rgba(51, 51, 51, 0.55)",
            data: data
        });
    }
});