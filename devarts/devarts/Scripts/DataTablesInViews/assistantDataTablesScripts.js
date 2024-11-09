function newAlert(type, title, message) {
    $("#alert-area").css("display", "none");
    $("#alert-area").html($("<br><br><div class='alert-message alert alert-" + type + " fade in'>" + '<button type="button" class="close" data-dismiss="alert">&times;</button><strong>' + title + '</strong> ' + message + "</div>"));
    $("#alert-area").fadeIn();
    $(".alert-message").delay(3000).fadeOut("slow", function () { $(this).remove(); });
}

$(document).ready(function () {

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

    $('#tabs').tabs({
        show: function (event, ui) {
            $('#calendar').fullCalendar('render');
        }
    });

    $('#accordion').on('shown.bs.collapse', function () {
        $('#calendar').fullCalendar('render');
    });

    $('body').tooltip({ selector: '[data-toggle="tooltip"]' });

    function isEmpty(str) {
        return (!str || str.length === 0);
    }

    // -------------------------------------------------- ReservationDataTableOpen -----------------------------------

    $('#reservationDataTable tfoot th').each(function () {
        var title = $('#reservationDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#reservationDataTable").DataTable({
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
        "orderMulti": true, // for disable multiple column at once
        "pageLength": 25,
        //"order": [[0, "desc"]],
        "ajax": {
            "url": "/AjaxReservation/ReservationAjaxList",
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                d.status = "Open";
            }
        },
        "columnDefs":
            [
                {
                    "targets": [0],
                    "width": "50px",
                    "visible": true,
                    "searchable": true,
                    "className": "green"
                },
                {
                    "width": "100px",
                    "targets": [1],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                    "className": "green"
                },
                {
                    "width": "100px",
                    "targets": [2],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                },
                {
                    "width": "100px",
                    "targets": [3],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                },
                {
                    "width": "20px",
                    "targets": [4],
                    "visible": true,
                    "searchable": true,
                    "sortable": true
                    //"className": "dt-body-center"
                },
                {
                    "width": "40px",
                    "targets": [5],
                    "visible": true,
                    "searchable": true,
                    "sortable": true
                },
                {
                    "width": "30px",
                    "targets": [6],
                    "visible": true,
                    "searchable": true,
                    "sortable": true
                },
                {
                    "width": "400px",
                    "targets": [7],
                    "visible": true,
                    "searchable": true,
                    "sortable": true
                },
                {
                    "width": "60px",
                    "targets": [8],
                    "visible": true,
                    "searchable": true,
                    "sortable": true
                },
                {
                    "width": "40px",
                    "targets": [9],
                    "visible": true,
                    "searchable": true,
                    "sortable": true                    
                },
                {
                    "width": "130px",
                    "targets": [10],
                    "visible": true,
                    "searchable": true,
                    "sortable": true                    
                }
            ],
        "createdRow": function (row, data, dataIndex) {
            if (data.Priority === "Wysoki") {
                $(row).addClass('danger');
            }
            if (data.Priority === "Średni") {
                $(row).addClass('warning');
            }
            if (data.Priority === "Niski") {
                $(row).addClass('success');
            }
        },
        "columns": [
            { "data": "FirstName", "name": "FirstName", "autoWidth": false },
            { "data": "SurName", "name": "SurName", "autoWidth": false },
            { "data": "City", "name": "City", "autoWidth": false },
            { "data": "Colour", "name": "Colour", "autoWidth": false },
            { "data": "Sex", "name": "Sex", "autoWidth": false },
            {
                "data": "DogForKennel", "name": "DogForKennel",
                "render": function (value) {
                    if (value === true)
                    {
                        return "Tak";
                    } 
                    return "Nie";
                },
            },
            { "data": "DogForSport", "name": "DogForSport", "autoWidth": false },
            /*{ "data": "BackendComments", "name": "BackendComments", "autoWidth": false },*/
            {
                "render": function (data, type, full, meta) {
                    if (full.IsReaded == false) {
                        if (isEmpty(full.BackendComments)) {
                            return "<span class='badge badge-danger' style='background-color:#c9302c'>Nieprzeczytana</span>";
                        } else {
                            return "<span class='badge badge-danger' style='background-color:#c9302c'>Nieprzeczytana</span><br> " + full.BackendComments;
                        }                        
                    }
                    else {
                        return full.BackendComments;
                    }
                }
            },
            { "data": "PaymentStatus", "name": "PaymentStatus", "autoWidth": false },
            {
                "data": "CreateDate", "name": "CreateDate",
                "render": function (value) {
                    if (value === null) return "";
                    return moment(value).format('DD.MM.YYYY');
                },
            },
            {
                "render": function (data, type, full, meta) {
                    return '<button class="btn btn-xs btn-danger reverseClosedStatus" data-placement="top" data-toggle="tooltip" data-original-title="Zakończ rezerwację (przenosi na zakładkę zakończonych)" data-id="' + full.Id + '"><i class="glyphicon glyphicon-flag"></i></button>' +
                           '<button class="btn btn-xs btn-info reverseReadStatus" data-placement="top" data-toggle="tooltip" data-original-title="Oznacz jako przeczytaną/nieprzeczytaną" data-id="' + full.Id + '"><i class="glyphicon glyphicon-refresh"></i></button>' +
                           '<button class="btn btn-xs btn-warning sendEmail" data-placement="top" data-toggle="tooltip" data-original-title="Wysyłanie e-maila" data-id="' + full.Id + '"><i class="glyphicon glyphicon-envelope"></i></button>' +
                           '<button class="btn btn-xs btn-success editReservation" data-placement="top" data-toggle="tooltip" data-original-title="Edycja"" data-id="' + full.Id + '"><i class="glyphicon glyphicon-edit"></i></button>' +
                           '<button class="btn btn-xs btn-danger removeReservation" data-placement="top" data-toggle="tooltip" data-original-title="Usuwanie rezerwacji" data-id="' + full.Id + '"><i class="glyphicon glyphicon-trash"></i></button>';
                    //return '<button class="btn btn-xs btn-warning sendEmail" data-id="' + full.Id + '"><i class="glyphicon glyphicon-envelope"></i></button>' +
                    //    '<button class="btn btn-xs btn-success editReservation" data-id="' + full.Id + '"><i class="glyphicon glyphicon-edit"></i></button>' +
                    //    '<button class="btn btn-xs btn-danger removeReservation" data-id="' + full.Id + '"><i class="glyphicon glyphicon-trash"></i></button>';
                }
            }
        ]
    });

    // DataTable
    var reservationDT = $('#reservationDataTable').DataTable();

    // Apply the search
    reservationDT.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });

    // -------------------------------------------------- RESERVATIONS -----------------------------------
    $('#reservationDataTableClosed tfoot th').each(function () {
        var title = $('#reservationDataTableClosed thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#reservationDataTableClosed").DataTable({
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
        "orderMulti": true, // for disable multiple column at once
        "pageLength": 25,
        //"order": [[0, "desc"]],
        "ajax": {
            "url": "/AjaxReservation/ReservationAjaxList",
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                d.status = "Closed";
            }
        },
        "columnDefs":
            [
                {
                    "targets": [0],
                    "width": "50px",
                    "visible": true,
                    "searchable": true,
                    "className": "green"
                },
                {
                    "width": "100px",
                    "targets": [1],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                    "className": "green"
                },
                {
                    "width": "100px",
                    "targets": [2],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                },
                {
                    "width": "100px",
                    "targets": [3],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                },
                {
                    "width": "20px",
                    "targets": [4],
                    "visible": true,
                    "searchable": true,
                    "sortable": true
                    //"className": "dt-body-center"
                },
                {
                    "width": "40px",
                    "targets": [5],
                    "visible": true,
                    "searchable": true,
                    "sortable": true
                },
                {
                    "width": "30px",
                    "targets": [6],
                    "visible": true,
                    "searchable": true,
                    "sortable": true
                },
                {
                    "width": "400px",
                    "targets": [7],
                    "visible": true,
                    "searchable": true,
                    "sortable": true
                },
                {
                    "width": "60px",
                    "targets": [8],
                    "visible": true,
                    "searchable": true,
                    "sortable": true
                },
                {
                    "width": "40px",
                    "targets": [9],
                    "visible": true,
                    "searchable": true,
                    "sortable": true
                },
                {
                    "width": "130px",
                    "targets": [10],
                    "visible": true,
                    "searchable": true,
                    "sortable": true
                }
            ],
        "createdRow": function (row, data, dataIndex) {
            if (data.Priority === "Wysoki") {
                $(row).addClass('danger');
            }
            if (data.Priority === "Średni") {
                $(row).addClass('warning');
            }
            if (data.Priority === "Niski") {
                $(row).addClass('success');
            }
        },
        "columns": [
            { "data": "FirstName", "name": "FirstName", "autoWidth": false },
            { "data": "SurName", "name": "SurName", "autoWidth": false },
            { "data": "City", "name": "City", "autoWidth": false },
            { "data": "Colour", "name": "Colour", "autoWidth": false },
            { "data": "Sex", "name": "Sex", "autoWidth": false },
            {
                "data": "DogForKennel", "name": "DogForKennel",
                "render": function (value) {
                    if (value === true) {
                        return "Tak";
                    }
                    return "Nie";
                },
            },
            { "data": "DogForSport", "name": "DogForSport", "autoWidth": false },
            /*{ "data": "BackendComments", "name": "BackendComments", "autoWidth": false },*/
            {
                "render": function (data, type, full, meta) {
                    if (full.IsReaded == false) {
                        if (isEmpty(full.BackendComments)) {
                            return "<span class='badge badge-danger' style='background-color:#c9302c'>Nieprzeczytana</span>";
                        } else {
                            return "<span class='badge badge-danger' style='background-color:#c9302c'>Nieprzeczytana</span><br> " + full.BackendComments;
                        }
                    }
                    else {
                        return full.BackendComments;
                    }
                }
            },
            { "data": "PaymentStatus", "name": "PaymentStatus", "autoWidth": false },
            {
                "data": "CreateDate", "name": "CreateDate",
                "render": function (value) {
                    if (value === null) return "";
                    return moment(value).format('DD.MM.YYYY');
                },
            },
            {
                "render": function (data, type, full, meta) {
                    return '<button class="btn btn-xs btn-success reverseClosedStatus" data-placement="top" data-toggle="tooltip" data-original-title="Otwórz stan rezeracji" data-id="' + full.Id + '"><i class="glyphicon glyphicon-flag"></i></button>' +
                        '<button class="btn btn-xs btn-info reverseReadStatus" data-placement="top" data-toggle="tooltip" data-original-title="Oznacz jako przeczytaną/nieprzeczytaną" data-id="' + full.Id + '"><i class="glyphicon glyphicon-refresh"></i></button>' +
                        '<button class="btn btn-xs btn-warning sendEmail" data-placement="top" data-toggle="tooltip" data-original-title="Wysyłanie e-maila" data-id="' + full.Id + '"><i class="glyphicon glyphicon-envelope"></i></button>' +
                        '<button class="btn btn-xs btn-success editReservation" data-placement="top" data-toggle="tooltip" data-original-title="Edycja"" data-id="' + full.Id + '"><i class="glyphicon glyphicon-edit"></i></button>' +
                        '<button class="btn btn-xs btn-danger removeReservation" data-placement="top" data-toggle="tooltip" data-original-title="Usuwanie rezerwacji" data-id="' + full.Id + '"><i class="glyphicon glyphicon-trash"></i></button>';
                    //return '<button class="btn btn-xs btn-warning sendEmail" data-id="' + full.Id + '"><i class="glyphicon glyphicon-envelope"></i></button>' +
                    //    '<button class="btn btn-xs btn-success editReservation" data-id="' + full.Id + '"><i class="glyphicon glyphicon-edit"></i></button>' +
                    //    '<button class="btn btn-xs btn-danger removeReservation" data-id="' + full.Id + '"><i class="glyphicon glyphicon-trash"></i></button>';
                }
            }
        ]
    });

    // DataTable
    var reservationDTc = $('#reservationDataTableClosed').DataTable();

    // Apply the search
    reservationDTc.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });

    // --------------------------------- Reproduction DataTable ---------------------------------------------

    $('#reproductionDataTable tfoot th').each(function () {
        var title = $('#reproductionDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#reproductionDataTable").DataTable({
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
        "orderMulti": true, // for disable multiple column at once
        "pageLength": 10,
        //"order": [[0, "desc"]],
        "ajax": {
            "url": "/AjaxReproduction/ReproductionAjaxList",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs":
            [
                {
                    "targets": [0],
                    "width": "20px",
                    "visible": true,
                    "searchable": true
                },
                {
                    "width": "100px",
                    "targets": [1],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                    "className": "green"
                },
                {
                    "width": "100px",
                    "targets": [2],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                },
                {
                    "width": "100px",
                    "targets": [3],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                },
                {
                    "width": "20px",
                    "targets": [4],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                    "className": "dt-body-center bolded"
                },
                {
                    "width": "100px",
                    "targets": [5],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                    "className": "dt-body-center red-bolded"
                },
                {
                    "width": "100px",
                    "targets": [7],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                    "className": "dt-body-center"
                },
                {                    
                    "targets": [8],
                    "visible": false
                }
            ],
        "createdRow": function (row, data, dataIndex) {
            if (data.CalculateDone === true) {
                $(row).addClass('gainsboroClass');
            }
        },
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": false },            
            { "data": "DogName", "name": "DogName", "autoWidth": false },
            { "data": "FatherName", "name": "FatherName", "autoWidth": false },
            {
                "data": "EstrusStartDate", "name" : "EstrusStartDate",
                "render": function (value) {
                    if (value === null) return "";
                    return moment(value).format('DD.MM.YYYY');
                },
            },
            {
                "data": "MatingDate_First", "name": "MatingDate_First",
                "render": function (value) {
                    if (value === null) return "";
                    return moment(value).format('DD.MM.YYYY');
                },
            },
            {
                "data": "EstimationBornDate", "name": "EstimationBornDate",
                "render": function (value) {
                    if (value === null) return "";
                    return moment(value).format('DD.MM.YYYY');
                },
            },
            {
                "data": "Comment", "name": "Comment",
                "render": function (data, type, full, meta) {
                    return full.Comment;
                    //return '<p>' + full.Comment +'</p>'+'<div class="progress" style="height:10px;">' +
                    //    '<div class="progress-bar progress-bar-striped active" role="progressbar" style="width: 74%;" aria-valuenow="10" aria-valuemin="0" aria-valuemax="100"></div>'+
                    //'</div>';
                },
            },         
            {
                "render": function (data, type, full, meta) {
                    return '<button class="btn btn-xs btn-primary showInfo" data-id="' + full.Id + '"><i class="fa fa-info-circle"></i></button>' +
                        '<button class="btn btn-xs btn-success editReproduction" data-id="' + full.Id + '"><i class="glyphicon glyphicon-edit"></i></button>' +
                        '<button class="btn btn-xs btn-danger removeReproduction" data-id="' + full.Id + '"><i class="glyphicon glyphicon-trash"></i></button>';
                }
            },
            { "data": "CalculateDone", "name": "CalculateDone", "autoWidth": false }
        ]
    });

    //$('#reproductionDataTable').dataTable({
    //    "createdRow": function (row, data, dataIndex) {
    //        //if (data[]) {
    //            $(row).addClass('redClass');
    //        //}
    //    }
    //});

    // DataTable
    var reproductionDT = $('#reproductionDataTable').DataTable();

    // Apply the search
    reproductionDT.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });

    // --------------------------------- Finance DataTable ---------------------------------------------

    $('#financeDataTable tfoot th').each(function () {
        var title = $('#financeDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#financeDataTable").DataTable({
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
        "orderMulti": true, // for disable multiple column at once
        "pageLength": 10,
        //"order": [[0, "desc"]],
        "ajax": {
            "url": "/AjaxFinance/FinanceAjaxList",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs":
            [
                {
                    "targets": [0],
                    "width": "20px",
                    "visible": true,
                    "searchable": false
                },
                {
                    "width": "50px",
                    "targets": [1],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                },
                {
                    "width": "auto",
                    "targets": [2],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                    "className" : "green"
                },
                {
                    "width": "auto",
                    "targets": [3],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                },
                {
                    "width": "80px",
                    "targets": [4],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                    "className": "dt-body-center bolded"
                },
                {
                    "width": "50px",
                    "targets": [5],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                    "className": "dt-body-center bolded"
                },
                {
                    "width": "50px",
                    "targets": [6],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                    "className": "dt-body-center bolded"
                },
                {
                    "width": "50px",
                    "targets": [7],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                    "className": "dt-body-center bolded"
                },
                {
                    "width": "50px",
                    "targets": [8],
                    "visible": true,
                    "searchable": true,
                    "sortable": true,
                    "className": "dt-body-center bolded"
                }
            ],
        "columns": [
            {
                "data": "IsExpense", "name": "IsExpense", "autoWidth": true, "render": function (data, type, full, meta) {
                    if (full.IsExpense) {
                        //return '<p><b><i class=class="red fa fa-arrow-circle-down"></i></b></p>';
                        return '<a class="red fa fa-2x fa-arrow-circle-down" href="javascript:void(0);"></a>';
                    } else {
                        return '<a class="green fa fa-2x fa-arrow-circle-up" href="javascript:void(0);"></a>';
                    }
                }
            },
            {
                "data": "Amount", "name": "Amount", "autoWidth": false, "render": function (data, type, full, meta) {
                    if (full.IsExpense) {
                        return '<span class="red" style="font-size: 1.2em;"><b> - ' + full.Amount + '</b></span>';
                    } else {
                        return '<span class="green" style="font-size: 1.2em;"><b> + ' + full.Amount + '</b></span>';
                    }
                }
            },
            {
                "data": "Name", "name": "Name", "autoWidth": true, "render": function (data, type, full, meta) {
                    if (full.Cathegory === "Odżywianie") {
                        return '<i class="fa fa-3x fa-apple" style="padding:5px; color:orange"></i>' + '<span style="color: orange; vertical-align:text-bottom;">' + full.Name + '</span>';
                    }
                    if (full.Cathegory === "Zdrowie") {
                        return '<i class="fa fa-3x fa-heartbeat" style="padding:5px; color:red"></i>' + '<span style="color: red; vertical-align:text-bottom;">' + full.Name + '</span>';
                    }
                    if (full.Cathegory === "Hodowla") {
                        return '<i class="fa fa-3x fa-paw" style="padding:5px; color:#337ab7"></i>' + '<span style="color: #337ab7; vertical-align:text-bottom;">' + full.Name + '</span>';
                    }
                    if (full.Cathegory === "Sprzedaż") {
                        return '<i class="fa fa-3x fa-money" style="padding:5px; color:green"></i>' + '<span style="color: green; vertical-align:text-bottom;">' + full.Name + '</span>';
                    }
                    if (full.Cathegory === "Wystawy") {
                        return '<i class="fa fa-3x fa-trophy" style="padding:5px; color:gold"></i>' + '<span style="color: gold; vertical-align:text-bottom;">' + full.Name + '</span>';
                    }
                    if (full.Cathegory === "Ubranka") {
                        return '<i class="glyphicon fa-3x glyphicon-sunglasses" style="padding:5px; color:gray"></i>' + '<span style="color: gray; vertical-align:text-bottom;">' + full.Name + '</span>';
                    }
                    if (full.Cathegory === "Rozmnażanie") {
                        return '<i class="fa fa-3x fa-american-sign-language-interpreting" style="padding:5px; color:pelevioletred"></i>' + '<span style="color: pelevioletred; vertical-align:text-bottom;">' + full.Name + '</span>';
                    }
                    return full.Name;
                }
            },
            { "data": "Description", "name": "Description", "autoWidth": false },
            { "data": "Cathegory", "name": "Cathegory", "autoWidth": false },
            {
                "data": "DateFrom", "name": "DateFrom",
                "render": function (value) {
                    if (value === null) return "";
                    return moment(value).format('DD.MM.YYYY');
                },
            },
            {
                "data": "DateTo", "name": "DateTo",
                "render": function (value) {
                    if (value === null) return "";
                    return moment(value).format('DD.MM.YYYY');
                },
            },
            { "data": "CurrencyName", "name": "CurrencyName", "autoWidth": false },
            {
                "render": function (data, type, full, meta) {
                    return '<button class="btn btn-xs btn-success editFinance" data-id="' + full.Id + '"><i class="glyphicon glyphicon-edit"></i></button>' +
                    '<button class="btn btn-xs btn-danger removeFinance" data-id="' + full.Id + '"><i class="glyphicon glyphicon-trash"></i></button>';
                }
            }
        ]

    });

    // DataTable
    var financeDT = $('#financeDataTable').DataTable();

    // Apply the search
    financeDT.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });
});