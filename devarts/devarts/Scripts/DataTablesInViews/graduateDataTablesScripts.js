
function GetDepatmentByShort(department) {

    switch (department) {
        case "WA":
            return "Wydział Architektury";
            break;
        case "WCh":
            return "Wydział Chemii";
            break;
        case "WETI":
            return "Wydział Elektroniki, Telekomunikacji i Informatyki";
            break;
        case "WEiA":
            return "Wydział Elektrotechniki i Automatyki";
            break;
        case "WM":
            return "Wydział Mechaniczny";
            break;
        case "WFTiMS":
            return "Wydział Fizyki Technicznej i Matematyki Stosowanej";
            break;
        case "WOiO":
            return "Wydział Oceanotechniki i Okrętownictwa";
    }
}

$(document).ready(function () {

    // ----------------------------------------------------------- Start GraduateView -----------------------------------------------------------

    $('#allStudentProvidersList tfoot th').each(function () {
        var title = $('#allStudentProvidersList thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#allStudentProvidersList").DataTable({
        "language": {
            "sProcessing": "Ładowanie danych...",
            "sLengthMenu": "Pokaż _MENU_ pozycji",
            "sZeroRecords": "Nie znaleziono pasujących pozycji",
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

        "ajax": {
            "url": "/Graduate/LoadGraduateData",
            "type": "POST",
            "datatype": "json"
        },
            
        "columns": [            
            {
                "data": "FirstName", "name": "FirstName", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<b><a href="javascript:void(0);" class="anchorDetail" data-id=' + full.Id + '>' + full.FirstName + '</a></b>';
                }
            },
            {
                "data": "SurName", "name": "SurName", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<a href="/Graduate/ViewUser/' + full.Id + '">' + full.SurName + '</a>';
                }
            },
            { "data": "DepartmentId", "title": "Wydział", "name": "DepartmentId", "autoWidth": true },
            { "data": "DiplomaDate", "name": "DiplomaDate", "autoWidth": true },            
            {
                "data": "Email", "name": "Email", "autoWidth": true, "render": function (data, type, full, meta) {
                    if (full.ShowMyMail == false) {
                        return "Ukryty";
                    }
                    else {
                        return full.Email;
                    }
                }
            },
                
            {
                "data": "Email", "name": "Email", "autoWidth": true, "render": function (data, type, full, meta) {
                    if (full.ShowMyMail == false) {
                        return "Ukryty";
                    }
                    else {
                        return full.Email;
                    }
                }
            },

            {
                "render": function (data, type, full, meta) {
                    return '<a href="javascript:void(0);" class="btn btn-xs btn-primary anchorDetail" data-id=' + full.Id + '>Pokaż</a>&nbsp;<a class="btn btn-xs btn-default" href="/Graduate/SendMessage/"' + full.Id + '>Wiadomość</a>';
                }
            },

        ]

    });

    // DataTable
    var table = $('#allStudentProvidersList').DataTable();

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

    // ----------------------------------------------------------- End GraduateView -----------------------------------------------------------

    // ----------------------------------------------------------- Start RegistersGraduateView -----------------------------------------------------------

    $('#allRegistersStudentProvidersList tfoot th').each(function () {
        var title = $('#allRegistersStudentProvidersList thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#allRegistersStudentProvidersList").DataTable({
        "language": {
            "sProcessing": "Ładowanie danych...",
            "sLengthMenu": "Pokaż _MENU_ pozycji",
            "sZeroRecords": "Nie znaleziono pasujących pozycji",
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

        "ajax": {
            "url": "/Graduate/LoadRegistersGraduateData",
            "type": "POST",
            "datatype": "json"
        },

        "columns": [
            {
                "data": "FirstName", "name": "FirstName", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<b><a href="javascript:void(0);" class="anchorDetail" data-id=' + full.Id + '>' + full.FirstName + '</a></b>';
                }
            },
            {
                "data": "SurName", "name": "SurName", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<a href="/Graduate/ViewUser/' + full.Id + '">' + full.SurName + '</a>';
                }
            },
            { "data": "DepartmentId", "title": "Wydział", "name": "DepartmentId", "autoWidth": true },
            { "data": "DiplomaDate", "name": "DiplomaDate", "autoWidth": true },
            {
                "data": "Email", "name": "Email", "autoWidth": true, "render": function (data, type, full, meta) {
                    if (full.ShowMyMail == false) {
                        return "Ukryty";
                    }
                    else {
                        return full.Email;
                    }
                }
            },

            {
                "data": "Email", "name": "Email", "autoWidth": true, "render": function (data, type, full, meta) {
                    if (full.ShowMyMail == false) {
                        return "Ukryty";
                    }
                    else {
                        return full.Email;
                    }
                }
            },

            {
                "render": function (data, type, full, meta) {
                    return '<a href="javascript:void(0);" class="btn btn-xs btn-primary anchorDetail" data-id=' + full.Id + '>Pokaż</a>&nbsp;<a class="btn btn-xs btn-default" href="/Graduate/SendMessage/"' + full.Id + '>Wiadomość</a>';
                }
            },

        ]

    });

    // DataTable
    var table = $('#allRegistersStudentProvidersList').DataTable();

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

    // ----------------------------------------------------------- End RegistersGraduateView -----------------------------------------------------------

});