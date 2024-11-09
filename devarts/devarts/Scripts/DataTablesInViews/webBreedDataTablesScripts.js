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

    $("a[href='#sectionG']").on('shown.bs.tab', function () {
        $('#calendar').fullCalendar('render');
    });

    $('#tabs').tabs({
        show: function (event, ui) {
            $('#calendar').fullCalendar('render');
        }
    });

    $('#accordion').on('shown.bs.collapse', function () {
        $('#calendar').fullCalendar('render');
    });

    // WSZYSTKIE POSTY (data[type] = All - wszystkie posty )

    $('#postDataTable tfoot th').each(function () {
        var title = $('#postDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#postDataTable").DataTable({
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
            "url": "/AjaxPost/PostsAjaxList",
            "type": "POST",
            "datatype": "json",
            // filtorwanie po kategorii
            "data": function (d) {
                d.type = "All";
                // d.custom = $('#myInput').val();
                // etc
            }
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
                    "width": "20px",
                    "targets": [2],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "20px",
                    "targets": [4],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "20px",
                    "targets": [5],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "20px",
                    "targets": [6],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "40px",
                    "targets": [7],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "40px",
                    "targets": [8],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
            ],
        "columns": [
            {
                "data": "Id", "name": "Id", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<a href="/Admin/EditPost/' + full.PostLink + '"><img style="width:70px;" class="dataTableImg" src="/Posts/' + full.PostLink + '/' + full.MainPicture + '"></a>';
                }
            },
            { "data": "PostName", "name": "Tytuł postu", "autoWidth": true },
            { "data": "Type", "name": "Typ", "autoWidth": true },
            {
                "data": "PostLink", "name": "PostLink", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<b><a href="/Admin/EditPost/' + full.PostLink + '">' + full.PostLink + '</a></b>';
                }
            },
            { "data": "PostDate", "name": "Data dodania", "autoWidth": true },
            { "data": "PostRate", "name": "Ocena", "autoWidth": true },
            { "data": "PostShow", "name": "Wyświetleń", "autoWidth": true },            
            { "data": "AllowComments", "name": "Włączone komentarze", "autoWidth": true },
            { "data": "IsPublished", "name": "Opublikowany", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    if (full.IsPublished) {
                        return '<a class="btn btn-xs btn-info" href="/Admin/EditPost/' + full.PostLink + '">Przejdź</a><a class="btn btn-xs btn-warning setPostVisibility" data-id="' + full.Id + '" href="javascript:void(0)">Ukryj</a><a class="btn btn-xs btn-danger">Ustawienia</a><a href="/Remove/RemovePost/' + full.Id + '" class="btn btn-xs btn-danger">Usuń</a>';
                    }
                    else {
                        return '<a class="btn btn-xs btn-info" href="/Admin/EditPost/' + full.PostLink + '">Przejdź</a><a class="btn btn-xs btn-success setPostVisibility" data-id="' + full.Id + '" href="javascript:void(0)">Publikuj</a><a class="btn btn-xs btn-danger">Ustawienia</a><a href="/Remove/RemovePost/' + full.Id + '" class="btn btn-xs btn-danger">Usuń</a>';
                    }
                }
            }
        ]

    });

    // DataTable
    var table = $('#postDataTable').DataTable();

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

    // NEWSY

    $('#postNewsDataTable tfoot th').each(function () {
        var title = $('#postNewsDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#postNewsDataTable").DataTable({
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
            "url": "/AjaxPost/PostsAjaxList",
            "type": "POST",
            "datatype": "json",
            // filtorwanie po kategorii
            "data": function (d) {
                d.type = "News";
                // d.custom = $('#myInput').val();
                // etc
            }
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
                    "width": "20px",
                    "targets": [2],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "20px",
                    "targets": [4],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "20px",
                    "targets": [5],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "20px",
                    "targets": [6],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "40px",
                    "targets": [7],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "40px",
                    "targets": [8],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
            ],
        "columns": [
            {
                "data": "Id", "name": "Id", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<a href="/Admin/EditPost/' + full.PostLink + '"><img style="width:70px;" class="dataTableImg" src="/Posts/' + full.PostLink + '/' + full.MainPicture + '"></a>';
                }
            },
            { "data": "PostName", "name": "Tytuł postu", "autoWidth": true },
            { "data": "Type", "name": "Typ", "autoWidth": true },
            {
                "data": "PostLink", "name": "PostLink", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<b><a href="/Admin/EditPost/' + full.PostLink + '">' + full.PostLink + '</a></b>';
                }
            },
            { "data": "PostDate", "name": "Data dodania", "autoWidth": true },
            { "data": "PostRate", "name": "Ocena", "autoWidth": true },
            { "data": "PostShow", "name": "Wyświetleń", "autoWidth": true },
            { "data": "AllowComments", "name": "Włączone komentarze", "autoWidth": true },
            { "data": "IsPublished", "name": "Opublikowany", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    if (full.IsPublished) {
                        return '<a class="btn btn-xs btn-info" href="/Admin/EditPost/' + full.PostLink + '">Przejdź</a><a class="btn btn-xs btn-warning setPostVisibility" data-id="' + full.Id + '" href="javascript:void(0)">Ukryj</a><a href="javascript:void(0)" class="previewPost btn btn-xs btn-danger" data-id="' + full.Id + '"><i class="fa fa-cog"></i> Ustawienia</a>';
                    }
                    else {
                        return '<a class="btn btn-xs btn-info" href="/Admin/EditPost/' + full.PostLink + '">Przejdź</a><a class="btn btn-xs btn-success setPostVisibility" data-id="' + full.Id + '" href="javascript:void(0)">Publikuj</a><a href="javascript:void(0)" class="previewPost btn btn-xs btn-danger" data-id="' + full.Id + '"><i class="fa fa-cog"></i> Ustawienia</a>';
                    }
                }
            }
        ]
    });

    // DataTable
    var tableNews = $('#postNewsDataTable').DataTable();

    // Apply the search
    tableNews.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });

    // BLOG (WYFILTORWANY)
    $('#postBlogDataTable tfoot th').each(function () {
        var title = $('#postBlogDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#postBlogDataTable").DataTable({
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
            "url": "/AjaxPost/PostsAjaxList",
            "type": "POST",
            "datatype": "json",
            // filtorwanie po kategorii
            "data": function (d) {
                d.type = "Blog";
                // d.custom = $('#myInput').val();
                // etc
            }
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
                    "width": "20px",
                    "targets": [2],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "20px",
                    "targets": [4],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "20px",
                    "targets": [5],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "20px",
                    "targets": [6],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "40px",
                    "targets": [7],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "40px",
                    "targets": [8],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
            ],
        "columns": [
            {
                "data": "Id", "name": "Id", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<a href="/Admin/EditPost/' + full.PostLink + '"><img style="width:70px;" class="dataTableImg" src="/Posts/' + full.PostLink + '/' + full.MainPicture + '"></a>';
                }
            },
            { "data": "PostName", "name": "Tytuł postu", "autoWidth": true },
            { "data": "Type", "name": "Typ", "autoWidth": true },
            {
                "data": "PostLink", "name": "PostLink", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<b><a href="/Admin/EditPost/' + full.PostLink + '">' + full.PostLink + '</a></b>';
                }
            },
            { "data": "PostDate", "name": "Data dodania", "autoWidth": true },
            { "data": "PostRate", "name": "Ocena", "autoWidth": true },
            { "data": "PostShow", "name": "Wyświetleń", "autoWidth": true },
            { "data": "AllowComments", "name": "Włączone komentarze", "autoWidth": true },
            { "data": "IsPublished", "name": "Opublikowany", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    if (full.IsPublished) {
                        return '<a class="btn btn-xs btn-info" href="/Admin/EditPost/' + full.PostLink + '">Przejdź</a><a class="btn btn-xs btn-warning setPostVisibility" data-id="' + full.Id + '" href="javascript:void(0)">Ukryj</a><a class="btn btn-xs btn-danger">Ustawienia</a>';
                    }
                    else {
                        return '<a class="btn btn-xs btn-info" href="/Admin/EditPost/' + full.PostLink + '">Przejdź</a><a class="btn btn-xs btn-success setPostVisibility" data-id="' + full.Id + '" href="javascript:void(0)">Publikuj</a><a class="btn btn-xs btn-danger">Ustawienia</a>';
                    }
                }
            }
        ]

    });

    // DataTable
    var tableBlog = $('#postBlogDataTable').DataTable();

    // Apply the search
    tableBlog.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });

    // PORTFOLIO (WYFILTORWANE)
    $('#postPortfolioDataTable tfoot th').each(function () {
        var title = $('#postPortfolioDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#postPortfolioDataTable").DataTable({
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
            "url": "/AjaxPost/PostsAjaxList",
            "type": "POST",
            "datatype": "json",
            // filtorwanie po kategorii
            "data": function (d) {
                d.type = "Portfolio";
                // d.custom = $('#myInput').val();
                // etc
            }
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
                    "width": "20px",
                    "targets": [2],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "20px",
                    "targets": [4],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "20px",
                    "targets": [5],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "20px",
                    "targets": [6],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "40px",
                    "targets": [7],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "40px",
                    "targets": [8],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
            ],
        "columns": [
            {
                "data": "Id", "name": "Id", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<a href="/Admin/EditPost/' + full.PostLink + '"><img style="width:70px;" class="dataTableImg" src="/Posts/' + full.PostLink + '/' + full.MainPicture + '"></a>';
                }
            },
            { "data": "PostName", "name": "Tytuł postu", "autoWidth": true },
            { "data": "Type", "name": "Typ", "autoWidth": true },
            {
                "data": "PostLink", "name": "PostLink", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<b><a href="/Admin/EditPost/' + full.PostLink + '">' + full.PostLink + '</a></b>';
                }
            },
            { "data": "PostDate", "name": "Data dodania", "autoWidth": true },
            { "data": "PostRate", "name": "Ocena", "autoWidth": true },
            { "data": "PostShow", "name": "Wyświetleń", "autoWidth": true },
            { "data": "AllowComments", "name": "Włączone komentarze", "autoWidth": true },
            { "data": "IsPublished", "name": "Opublikowany", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    if (full.IsPublished) {
                        return '<a class="btn btn-xs btn-info" href="/Admin/EditPost/' + full.PostLink + '">Przejdź</a><a class="btn btn-xs btn-warning setPostVisibility" data-id="' + full.Id + '" href="javascript:void(0)">Ukryj</a><a class="btn btn-xs btn-danger">Usuń</a>';
                    }
                    else {
                        return '<a class="btn btn-xs btn-info" href="/Admin/EditPost/' + full.PostLink + '">Przejdź</a><a class="btn btn-xs btn-success setPostVisibility" data-id="' + full.Id + '" href="javascript:void(0)">Publikuj</a><a class="btn btn-xs btn-danger">Usuń</a>';
                    }
                }
            }
        ]

    });

    // DataTable
    var tablePortfolio = $('#postPortfolioDataTable').DataTable();

    // Apply the search
    tablePortfolio.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });

    // WYWIADY (WYFILTORWANE)
    $('#postWywiadyDataTable tfoot th').each(function () {
        var title = $('#postWywiadyDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#postWywiadyDataTable").DataTable({
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
            "url": "/AjaxPost/PostsAjaxList",
            "type": "POST",
            "datatype": "json",
            // filtorwanie po kategorii
            "data": function (d) {
                d.type = "Wywiady";
                // d.custom = $('#myInput').val();
                // etc
            }
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
                    "width": "20px",
                    "targets": [2],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "20px",
                    "targets": [4],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "20px",
                    "targets": [5],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "20px",
                    "targets": [6],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "40px",
                    "targets": [7],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "40px",
                    "targets": [8],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
            ],
        "columns": [
            {
                "data": "Id", "name": "Id", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<a href="/Admin/EditPost/' + full.PostLink + '"><img style="width:70px;" class="dataTableImg" src="/Posts/' + full.PostLink + '/' + full.MainPicture + '"></a>';
                }
            },
            { "data": "PostName", "name": "Tytuł postu", "autoWidth": true },
            { "data": "Type", "name": "Typ", "autoWidth": true },
            {
                "data": "PostLink", "name": "PostLink", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<b><a href="/Admin/EditPost/' + full.PostLink + '">' + full.PostLink + '</a></b>';
                }
            },
            { "data": "PostDate", "name": "Data dodania", "autoWidth": true },
            { "data": "PostRate", "name": "Ocena", "autoWidth": true },
            { "data": "PostShow", "name": "Wyświetleń", "autoWidth": true },
            { "data": "AllowComments", "name": "Włączone komentarze", "autoWidth": true },
            { "data": "IsPublished", "name": "Opublikowany", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    if (full.IsPublished) {
                        return '<a class="btn btn-xs btn-info" href="/Admin/EditPost/' + full.PostLink + '">Przejdź</a><a class="btn btn-xs btn-warning setPostVisibility" data-id="' + full.Id + '" href="javascript:void(0)">Ukryj</a><a class="btn btn-xs btn-danger">Usuń</a>';
                    }
                    else {
                        return '<a class="btn btn-xs btn-info" href="/Admin/EditPost/' + full.PostLink + '">Przejdź</a><a class="btn btn-xs btn-success setPostVisibility" data-id="' + full.Id + '" href="javascript:void(0)">Publikuj</a><a class="btn btn-xs btn-danger">Usuń</a>';
                    }
                }
            }
        ]

    });

    // DataTable
    var tableWywiady = $('#postWywiadyDataTable').DataTable();

    // Apply the search
    tableWywiady.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });

    // POKAZYWANIE I UKRYWANIE POSTA

    var TeamDetailPostBackURL = '/Admin/SetPostVisibility';
    $(document).on('click', ".setPostVisibility", function () {
        //debugger;
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {
                table.ajax.reload();
                newAlert('success', 'Sukces', 'Status postu został ustawiony.');
            },
            error: function () {
                newAlert('error', 'Błąd', 'Coś poszło nie tak (0x0001fsd');
            }
        });
    });
});