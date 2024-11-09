function newAlert(type, title, message) {
    $("#alert-area").css("display", "none");
    $("#alert-area").html($("<br><br><div class='alert-message alert alert-" + type + " fade in'>" + '<button type="button" class="close" data-dismiss="alert">&times;</button><strong>' + title + '</strong> ' + message + "</div>"));
    $("#alert-area").fadeIn();
    $(".alert-message").delay(3000).fadeOut("slow", function () { $(this).remove(); });
}

$(document).ready(function () {

    //Highcharts.chart('ctnCh', {
    //    chart: {
    //        type: 'column'
    //    },
    //    renderTo: 'ctnCh',
    //    title: {
    //        text: 'Podsumowanie roczne'
    //    },
    //    subtitle: {
    //        text: 'Wydatki w 2017 roku'
    //    },
    //    xAxis: {
    //        categories: [
    //            'Styczeń',
    //            'Luty',
    //            'Marzec',
    //            'Kwiecień',
    //            'Maj',
    //            'Czerwiec',
    //            'Lipiec',
    //            'Sierpień',
    //            'Wrzesień',
    //            'Październik',
    //            'Listopad',
    //            'Grudzień'
    //        ],
    //        crosshair: true
    //    },
    //    yAxis: {
    //        min: 0,
    //        title: {
    //            text: 'Koszty (PLN)'
    //        }
    //    },
    //    tooltip: {
    //        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
    //        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
    //            '<td style="padding:0"><b>{point.y:.1f} mm</b></td></tr>',
    //        footerFormat: '</table>',
    //        shared: true,
    //        useHTML: true
    //    },
    //    plotOptions: {
    //        column: {
    //            pointPadding: 0.2,
    //            borderWidth: 0
    //        }
    //    },
    //    series: [{
    //        name: 'Pożywienie',
    //        data: [49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4]

    //    }, {
    //        name: 'Leczenie',
    //        data: [83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 105.0, 104.3, 91.2, 83.5, 106.6, 92.3]

    //    }, {
    //        name: 'Wystawy',
    //        data: [48.9, 38.8, 39.3, 41.4, 47.0, 48.3, 59.0, 59.6, 52.4, 65.2, 59.3, 51.2]

    //    }, {
    //        name: 'Wyścigi',
    //        data: [42.4, 33.2, 34.5, 39.7, 52.6, 75.5, 57.4, 60.4, 47.6, 39.1, 46.8, 51.1]

    //    }]
    //});

    if (location.hash) {
        $('a[href="' + location.hash + '"]').tab('show');
    }

    $(document.body).on("click", "a[data-toggle]", function (event) {
        location.hash = this.getAttribute("href");
    });

    $(window).on('popstate', function () {
        var anchor = location.hash || $("a[data-toggle=tab]").first().attr("href");
        $('.tab-pane').addClass('fade');
        $('a[href="' + anchor + '"]').tab('show');
    });


    // --------------------------------- Statistic DataTable ---------------------------------------------

    $('#statisticDataTable tfoot th').each(function () {
        var title = $('#statisticDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#statisticDataTable").DataTable({
        "language": {
            "sProcessing": "Ładowanie danych...",
            "sLengthMenu": "Pokaż _MENU_ pozycji",
            "sZeroRecords": "Nie znaleziono żadnych pozycji",
            "sInfoThousands": " ",
            "sInfo": "Pozycje od _START_ do _END_ z _TOTAL_ łącznie",
            "sInfoEmpty": "Pozycji 0 z 0 dostępnych",
            "sInfoFiltered": "(filtrowanie spośród _MAX_ dostępnych pozycji)",
            "sInfoPostFix": "",
            "sSearch": "Szybkie szukanie:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "Pierwsza",
                "sPrevious": "Poprzednia",
                "sNext": "Następna",
                "sLast": "Ostatnia"
            }
        },
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        "order": [[0, "desc"]],
        "ajax": {
            "url": "/AjaxStatistic/StatisticsAjaxList",
            "type": "POST",
            "datatype": "json"
        },        
        "columnDefs":
            [
                {
                    "targets": [0],
                    "visible": true,
                    "searchable": true
                },
                {
                    "width": "20px",
                    "targets": [4],
                    "visible": true,
                    "searchable": true,
                    "className" : "dt-body-center"
                }
            ],        
        "columns": [
            {
                "data": "Id", "name": "Id", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<b><a href="/Demo/Edit/' + full.Id + '">' + full.Id + '</a></b>';
                }
            },
            { "data": "DateTime", "name": "Czas", "autoWidth": true },
            { "data": "IpAddress", "name": "Adres IP", "autoWidth": true },
            { "data": "Name", "name": "Nazwa hosta", "autoWidth": true },
            {
                "data": "DeviceType", "name": "DeviceType", "autoWidth": true, "render": function (data, type, full, meta) {
                    if (full.DeviceType === 0) {
                        return "<i class='fa fa-desktop'></i>";
                    }
                    else {
                        return "<i class='fa fa-2x fa-mobile'></i>";
                    }
                }
            },
            { "data": "Continent", "name": "Kontynent", "autoWidth": true },
            { "data": "Country", "name": "Kraj", "autoWidth": true },
            { "data": "GeoLong", "name": "Długość Geo", "autoWidth": true },
            { "data": "GeoLat", "name": "Szerokość Geo", "autoWidth": true },

            {
                "render": function (data, type, full, meta) {
                    return '<a class="btn btn-xs btn-info" href="/Demo/Edit/' + full.CustomerID + '">Zobacz na mapie</a>';
                }
            },
        ]

    });

    // DataTable
    var table = $('#statisticDataTable').DataTable();

    // Apply the search
    table.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });

    // ----------------------------------------------- End Statistic Data Table ----------------------------------------------

    $('#tracingDataTable tfoot th').each(function () {
        var title = $('#tracingDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $('#tracingDataTable').dataTable({
        "language": {
            "sProcessing": "Ładowanie danych...",
            "sLengthMenu": "Pokaż _MENU_ pozycji",
            "sZeroRecords": "Nie znaleziono żadnych pozycji",
            "sInfoThousands": " ",
            "sInfo": "Pozycje od _START_ do _END_ z _TOTAL_ łącznie",
            "sInfoEmpty": "Pozycji 0 z 0 dostępnych",
            "sInfoFiltered": "(filtrowanie spośród _MAX_ dostępnych pozycji)",
            "sInfoPostFix": "",
            "sSearch": "Szybkie szukanie:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "Pierwsza",
                "sPrevious": "Poprzednia",
                "sNext": "Następna",
                "sLast": "Ostatnia"
            }
        },
        "bServerSide": true,
        "sAjaxSource": "/AjaxStatistic/TracingAjaxList",
        "bProcessing": true,
        "columnDefs":
            [
                {
                    "targets": [0],
                    "visible": true,
                    "searchable": true
                },
                {
                    "width": "20px",
                    "targets": [4],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                }
            ],        
        "aoColumns": [
            {
                "sName": "Id",
            },
            {
                "mData": null,
                className: "center",
                "mRender": function (data, type, row) {
                    return "<a href='" + row[1] + "'>" + row[1] + "</a>";
                    //return "<img style='width='100%'; height='10%'; box-shadow: 0px 0px 13px 1px black;'' src='/UploadImages/" + row[5] + "/" + row[6] + "'>"
                }
            },
            { "sName": "HostAndIp" },
            { "sName": "DateTime" },
            { "sName": "DeviceType" }
        ],
    });

    // DataTable
    var tracingTable = $('#tracingDataTable').DataTable();

    // Apply the search
    tracingTable.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });

    // ----------------------------------------------- Start Documents Data Table ----------------------------------------------

    $('#documentsDataTable tfoot th').each(function () {
        var title = $('#documentsDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $('#documentsDataTable').dataTable({
        "language": {
            "sProcessing": "Ładowanie danych...",
            "sLengthMenu": "Pokaż _MENU_ pozycji",
            "sZeroRecords": "Nie znaleziono żadnych pozycji",
            "sInfoThousands": " ",
            "sInfo": "Pozycje od _START_ do _END_ z _TOTAL_ łącznie",
            "sInfoEmpty": "Pozycji 0 z 0 dostępnych",
            "sInfoFiltered": "(filtrowanie spośród _MAX_ dostępnych pozycji)",
            "sInfoPostFix": "",
            "sSearch": "Szybkie szukanie:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "Pierwsza",
                "sPrevious": "Poprzednia",
                "sNext": "Następna",
                "sLast": "Ostatnia"
            }
        },
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        "order": [[0, "asc"]],
        "ajax": {
            "url": "/AjaxKennel/DocumentsAjaxList",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs":
            [
                {
                    "width": "10px",
                    "targets": [0],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "30px",
                    "targets": [8],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "100px",
                    "targets": [9],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                }
            ],
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    return '<b>' + full.DocumentName + '</b>';
                }
            },
            { "data": "Description", "name": "Opis", "autoWidth": true },
            { "data": "Type", "name": "Typ", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    return '<i class="fa fa-2x fa-file-pdf-o"></i> <a href="/Admin/Download?fileName=' + full.Url + '">' + full.Url + '</a>';
                }
            },
            { "data": "DocumentDate", "name": "Data dokumentu", "autoWidth": true },
            { "data": "DocumentVersion", "name": "wersja dokumentu", "autoWidth": true },
            { "data": "UploadDate", "name": "Data zamieszczenia", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    return '<input type="checkbox" class="editor-active" checked onClick="return false">';
                }
            },
            {
                "render": function (data, type, full, meta) {
                    return '<a class="btn btn-xs btn-info" href="/Admin/Download?fileName=' + full.Url + '">Pobierz</a><a class="btn btn-xs btn-success" href="' + full.Url + '">Drukuj</a>';
                }
            },
        ],
    });

    // DataTable
    var documentsTable = $('#documentsDataTable').DataTable();

    // Apply the search
    documentsTable.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });

    // -------------------------------------- WYSŁANE NEWSLETTERY

    $('#newsletterDataTable tfoot th').each(function () {
        var title = $('#newsletterDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $('#newsletterDataTable').dataTable({
        "language": {
            "sProcessing": "Ładowanie danych...",
            "sLengthMenu": "Pokaż _MENU_ pozycji",
            "sZeroRecords": "Nie znaleziono żadnych pozycji",
            "sInfoThousands": " ",
            "sInfo": "Pozycje od _START_ do _END_ z _TOTAL_ łącznie",
            "sInfoEmpty": "Pozycji 0 z 0 dostępnych",
            "sInfoFiltered": "(filtrowanie spośród _MAX_ dostępnych pozycji)",
            "sInfoPostFix": "",
            "sSearch": "Szybkie szukanie:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "Pierwsza",
                "sPrevious": "Poprzednia",
                "sNext": "Następna",
                "sLast": "Ostatnia"
            }
        },
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        "order": [[0, "asc"]],
        "ajax": {
            "url": "/AjaxNewsletter/NewsletterAjaxList",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs":
            [
                {
                    "width": "10px",
                    "targets": [0],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                }
            ],
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            { "data": "Email", "name": "E-mail", "autoWidth": true },
            { "data": "Subject", "name": "Subject", "autoWidth": true },
            { "data": "CreateDate", "name": "Data zapisania", "autoWidth": true },
            { "data": "SendDate", "name": "Data wysłania", "autoWidth": true },
            { "data": "IsActive", "name": "Aktywny", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    return '<a class="btn btn-xs btn-info" href="/Admin/Download?fileName=' + full.Id + '">Pobierz</a><a class="btn btn-xs btn-success" href="' + full.Id + '">Drukuj</a>';
                }
            }
        ],
    });

    // DataTable
    var newsletterTable = $('#newsletterDataTable').DataTable();

    // Apply the search
    newsletterTable.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });

    // ----------------------------- SUBSKRYBENCI NEWSLETTERA ----------------------------------------------

    $('#subscriberDataTable tfoot th').each(function () {
        var title = $('#subscriberDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $('#subscriberDataTable').dataTable({
        "language": {
            "sProcessing": "Ładowanie danych...",
            "sLengthMenu": "Pokaż _MENU_ pozycji",
            "sZeroRecords": "Nie znaleziono żadnych pozycji",
            "sInfoThousands": " ",
            "sInfo": "Pozycje od _START_ do _END_ z _TOTAL_ łącznie",
            "sInfoEmpty": "Pozycji 0 z 0 dostępnych",
            "sInfoFiltered": "(filtrowanie spośród _MAX_ dostępnych pozycji)",
            "sInfoPostFix": "",
            "sSearch": "Szybkie szukanie:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "Pierwsza",
                "sPrevious": "Poprzednia",
                "sNext": "Następna",
                "sLast": "Ostatnia"
            }
        },
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 10,
        "order": [[0, "asc"]],
        "ajax": {
            "url": "/AjaxNewsletter/SubscriberAjaxList",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs":
            [
                {
                    "width": "10px",
                    "targets": [0],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                }
            ],
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            { "data": "Email", "name": "E-mail", "autoWidth": true },            
            { "data": "CreateDate", "name": "Data zapisania", "autoWidth": true },            
            { "data": "IsActive", "name": "Aktywny", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    if (full.IsActive) {
                        return '<a class="btn btn-xs btn-warning setSubscriberStatus" data-id="' + full.Id + '" href="javascript:void(0)">Dezaktywuj</a><a href="javaScript:void(0)" data-id="' + full.Id + '" class="btn btn-xs btn-danger removeSubscriber">Usuń</a>';
                    }
                    else
                    {
                        return '<a class="btn btn-xs btn-success setSubscriberStatus" data-id="' + full.Id + '" href="javascript:void(0)">Aktywuj</a><a href="javaScript:void(0)" data-id="' + full.Id + '" class="btn btn-xs btn-danger removeSubscriber">Usuń</a>';
                    }
                }
            }
        ],
    });

    // DataTable
    var subscriberTable = $('#subscriberDataTable').DataTable();

    // Apply the search
    subscriberTable.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });
    
    $(document).on('click', ".setSubscriberStatus", function () {
        //debugger;
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: '/AjaxNewsletter/SetSubscriberStatus',
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {
                subscriberTable.ajax.reload();
                newAlert('success', 'Sukces', 'Status subskrybenta został ustawiony.');
            },
            error: function () {
                newAlert('error', 'Błąd', 'Coś poszło nie tak (0x000234fsd)');
            }
        });
    });

    $(document).on('click', ".removeSubscriber", function () {
        //debugger;
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: '/AjaxNewsletter/RemoveSubscriber',
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {
                subscriberTable.ajax.reload();
                newAlert('success', 'Sukces', 'E-mail subskrybenta został usunięty.');
            },
            error: function () {
                newAlert('error', 'Błąd', 'Coś poszło nie tak (0x0002qwrt34fsd)');
            }
        });
    });

    $('#littersDataTable').dataTable({
        "bServerSide": true,
        "sAjaxSource": "/Ajax/LittersAjaxList",
        "bProcessing": true,
        "aoColumns": [
            {
                "sName": "Id", "visible": false
            },
            {
                "sName": "LitterName", "visible": false
            },
            {
                "mData": null,
                className: "center",
                "mRender": function (data, type, row) {
                    return "<a href='/Litters/Selected/" + row[1] + "'> " + row[1] + "</a>";
                    //return "<img style='width='100%'; height='10%'; box-shadow: 0px 0px 13px 1px black;'' src='/UploadImages/" + row[5] + "/" + row[6] + "'>"
                }
            },
            { "sName": "LitterBreed" },
            { "sName": "DogMother" },
            { "sName": "DogFather" },
            { "sName": "BornDate" },
            { "sName": "ShowsCount" },
            { "sName": "ImgFileName" },
            {
                "mData": null,
                "title": "Zarządzaj",
                className: "center",
                "mRender": function (data, type, row) {
                    //return "<a class='btn btn-xs btn-default' href='/Admin/Index/" + row[0] + "'> Zobacz </a> / <a class='btn btn-xs btn-default' href='/Admin/Index/" + row[0] + "'> Usuń </a>";
                    return "<img class='dataTableImg' src='/UploadLitters/" + row[1] + "/" + row[8] + "'><br><br><a class='btn btn-xs btn-success' href='/Litters/LitterEdit/" + row[0] + "'> Edytuj </a> <a class='btn btn-xs btn-danger' href='/Admin/Index/" + row[0] + "'> Usuń </a>"
                }
            }
        ],
    });

    $.getScript("/Scripts/fullcalendar.js", function () {

        var date = new Date();

        var d = date.getDate();
        var m = date.getMonth();
        var y = date.getFullYear();

        $('#calendar').fullCalendar({
            monthNames: ['Styczeń', 'Luty', 'Marzec', 'Kwiecień', 'Maj', 'Czerwiec', 'Lipiec', 'Sierpień', 'Wrzesień', 'Październik', 'Listopad', 'Grudzień'],
            monthNamesShort: ['Stycz.', 'Luty', 'Mar.', 'Kwiec.', 'Maj', 'Czerw.', 'Lip.', 'Sierp.', 'Wrz.', 'Paźdź.', 'List.', 'Grudź.'],
            dayNames: ['Niedziela', 'Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek', 'Sobota'],
            dayNamesShort: ['Niedz.', 'Pon.', 'Wt.', 'Śr.', 'Czw.', 'Pt.', 'Sob.'],
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            allDaySlot: false,
            buttonText:
                {
                    prev: '<', // <
                    next: '>', // >
                    prevYear: '<<',  // <<
                    nextYear: '>>',  // >>
                    today: 'Dziś',
                    month: 'Miesiąc',
                    week: 'Tydzień',
                    day: 'Dzień'
                },
            editable: false,
            timeFormat: 'HH:mm',
            axisFormat: 'HH:mm',
            events:
                [
                    {
                        title: 'Lea - cieczka',
                        start: new Date(y, m, 1)
                    },
                    {
                        id: 999,
                        title: 'Mati - reprodukcja',
                        start: new Date(y, m, d - 3, 16, 0),
                        allDay: false,
                        className: 'info'
                    },
                    {
                        id: 999,
                        title: 'Mini - cieczka',
                        start: new Date(y, m, d + 4, 16, 0),
                        allDay: false,
                        className: 'info'
                    },
                    {
                        title: 'Skazka - wyjazd',
                        start: new Date(y, m, d, 10, 30),
                        allDay: false,
                        className: 'important'
                    },
                    {
                        title: 'Coursing (wszyscy)',
                        start: new Date(y, m, d, 12, 0),
                        end: new Date(y, m, d, 14, 0),
                        allDay: false,
                        className: 'important'
                    },
                    {
                        title: 'Lea - wystawa',
                        start: new Date(y, m, d + 1, 19, 0),
                        end: new Date(y, m, d + 1, 22, 30),
                        allDay: false,
                    },
                    {
                        title: 'Cuki - coursing',
                        start: new Date(y, m, 28),
                        end: new Date(y, m, 29),
                        url: 'http://google.com/',
                        className: 'success'
                    }
                ]
        });
    });
});

$(window).on('popstate', function () {
    var anchor = location.hash || $("a[data-toggle=tab]").first().attr("href");
    $('.tab-pane').addClass('fade');
    $('a[href="' + anchor + '"]').tab('show');
});