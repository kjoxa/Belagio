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

    $('body').tooltip({ selector: '[data-toggle="tooltip"]' });

    //function checkImageIndexValue() {
    //    $(".setImageIndex").each(function (e) {
    //        var val = $(this);
    //        if (val.val() !== "0") {
    //            $(this).css("background-color", "green");
    //            $inputImageIndex.css("color", "White");
    //        } else {
    //            // keep message
    //        }
    //    });
    //}
    //checkImageIndexValue();

    // --------------------------------- Zapisywanie indeksu zdjęcia w galerii psa ---------------------------------
    var setImageIndex = '/AjaxKennel/SetImageIndex';
    $(document).on('keydown', ".setImageIndex", function (event) {

        if (event.keyCode === 13) {
            // Cancel the default action, if needed
            event.preventDefault();
            // Trigger the button element with a click
            var $inputImageIndex = $(this);
            var id = $inputImageIndex.attr('data-image-id');
            var number = $inputImageIndex.val();
            var options = { "backdrop": "static", keyboard: true };
            $.ajax({
                type: "GET",
                url: setImageIndex,
                contentType: "application/json; charset=utf-8",
                data: { "id": id, "number": number },
                datatype: "json",
                success: function (data) {                    
                    $inputImageIndex.css("background-color", "#337ab7");
                    $inputImageIndex.css("color", "White");
                    $inputImageIndex.css("font-weight", "bold");
                    $inputImageIndex.hide().fadeIn('slow');
                    //checkImageIndexValue();
                },
                error: function () {
                    newAlert('error', 'Błąd', 'Wystąpił błąd podczas zapisywania indeksu zdjęcia: #IXER004');
                }
            });
        }
        //return event.key != "Enter"; return false;
    });

    // --------------------------------- Zapisywanie tekstu alternatywnego dla zdjęcia w galerii psa ---------------------------------
    var setDogImageAlt = '/AjaxKennel/SetAltForDogImage';
    $(document).on('keydown', ".setAltValue", function (event) {

        if (event.keyCode === 13) {
            // Cancel the default action, if needed
            event.preventDefault();
            // Trigger the button element with a click
            var $inputImageIndex = $(this);
            var id = $inputImageIndex.attr('data-image-id');
            var altText = $inputImageIndex.val();
            var options = { "backdrop": "static", keyboard: true };
            $.ajax({
                type: "GET",
                url: setDogImageAlt,
                contentType: "application/json; charset=utf-8",
                data: { "id": id, "altText": altText },
                datatype: "json",
                success: function (data) {
                    $inputImageIndex.css("background-color", "#337ab7");
                    $inputImageIndex.css("color", "White");
                    $inputImageIndex.css("font-weight", "bold");
                    $inputImageIndex.hide().fadeIn('slow');
                    //checkImageIndexValue();
                },
                error: function () {
                    newAlert('error', 'Błąd', 'Wystąpił błąd podczas zapisywania indeksu zdjęcia: #IXER004');
                }
            });
        }
        //return event.key != "Enter"; return false;
    });

    // --------------------------------- Zapisywanie indeksu zdjęcia w galerii SZCZENIĘCIA ---------------------------------
    var setPuppyImageIndex = '/AjaxKennel/SetPuppyImageIndex';
    $(document).on('keydown', ".setPuppyImageIndex", function (event) {

        if (event.keyCode === 13) {
            // Cancel the default action, if needed
            event.preventDefault();
            // Trigger the button element with a click
            var $inputImageIndex = $(this);
            var id = $inputImageIndex.attr('data-image-id');
            var number = $inputImageIndex.val();
            var options = { "backdrop": "static", keyboard: true };
            $.ajax({
                type: "GET",
                url: setPuppyImageIndex,
                contentType: "application/json; charset=utf-8",
                data: { "id": id, "number": number },
                datatype: "json",
                success: function (data) {
                    //newAlert('success', 'Sukces', 'Indeks zdjęcia został zapisany');
                    $inputImageIndex.css("background-color", "#337ab7");
                    $inputImageIndex.css("color", "White");
                    $inputImageIndex.css("font-weight", "bold");
                    $inputImageIndex.hide().fadeIn('slow');
                },
                error: function () {
                    newAlert('error', 'Błąd', 'Wystąpił błąd podczas zapisywania indeksu zdjęcia: #IXE223R004');
                }
            });
        }
        //return event.key != "Enter"; return false;
    });

    // --------------------------------- Zapisywanie tekstu alternatywnego dla zdjęcia w galerii szczenięcia ---------------------------------
    var setPuppyImageAlt = '/AjaxKennel/SetAltForPuppyImage';
    $(document).on('keydown', ".setAltPuppyValue", function (event) {

        if (event.keyCode === 13) {
            // Cancel the default action, if needed
            event.preventDefault();
            // Trigger the button element with a click
            var $inputImageIndex = $(this);
            var id = $inputImageIndex.attr('data-image-id');
            var altText = $inputImageIndex.val();
            var options = { "backdrop": "static", keyboard: true };
            $.ajax({
                type: "GET",
                url: setPuppyImageAlt,
                contentType: "application/json; charset=utf-8",
                data: { "id": id, "altText": altText },
                datatype: "json",
                success: function (data) {
                    $inputImageIndex.css("background-color", "#337ab7");
                    $inputImageIndex.css("color", "White");
                    $inputImageIndex.css("font-weight", "bold");
                    $inputImageIndex.hide().fadeIn('slow');
                    //checkImageIndexValue();
                },
                error: function () {
                    newAlert('error', 'Błąd', 'Wystąpił błąd podczas zapisywania indeksu zdjęcia: #IXER004');
                }
            });
        }
        //return event.key != "Enter"; return false;
    });

    // --------------------------------- Statistic DataTable ---------------------------------------------

    $('#bitchDataTable tfoot th').each(function () {
        var title = $('#bitchDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#bitchDataTable").DataTable({
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
            "url": "/AjaxKennel/BitchesAjaxList",
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
                    "className": "dt-body-center"
                }
            ],
        "columns": [
            {
                "data": "Id", "name": "Id", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<b><a href="/Demo/Edit/' + full.Id + '">' + full.Id + '</a></b>';
                }
            },
            { "data": "DogName", "name": "Nazwa suki", "autoWidth": true },
            { "data": "EstrusStartDate", "name": "Rozpoczęcie cieczki", "autoWidth": true },
            { "data": "RutStartDate", "name": "Rozpoczęcie rui", "autoWidth": true },
            //{
            //    "data": "DeviceType", "name": "DeviceType", "autoWidth": true, "render": function (data, type, full, meta) {
            //        if (full.DeviceType == 0) {
            //            return "<i class='fa fa-desktop'></i>";
            //        }
            //        else {
            //            return "<i class='fa fa-2x fa-mobile'></i>";
            //        }
            //    }
            //},
            { "data": "ProgessteronBestDay", "name": "Progesteron dzień", "autoWidth": true },
            { "data": "ProgessteronBestVal", "name": "Progesteron wartość", "autoWidth": true },
            { "data": "Comment", "name": "Uwagi", "autoWidth": true },
            { "data": "IsSuccess", "name": "Udane zapłodnienie", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    return '<a class="btn btn-xs btn-info" href="/Demo/Edit/' + full.CustomerID + '">Zobacz na mapie</a>';
                }
            },
        ]

    });

    // DataTable
    var table = $('#bitchDataTable').DataTable();

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

    // --------------------------------- Litters List ---------------------------------------------

    $('#littersDataTable tfoot th').each(function () {
        var title = $('#littersDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#littersDataTable").DataTable({
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
            "url": "/AjaxKennel/LittersAjaxList",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs":
            [
                {
                    "targets": [0],
                    "visible": true,
                    "searchable": true,
                    "width": "20px"
                },
                {
                    "width": "30px",
                    "targets": [6],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "30px",
                    "targets": [7],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },                
                {
                    "width": "200px",
                    "targets": [9],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                }
            ],
        "columns": [
            {
                "data": "Id", "name": "Id", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<a href="/Kennel/EditLitter/' + full.Id + '"><img style="width:70px;" class="dataTableImg" src="/Litters/' + full.BreedLink + '/' + full.LitterLink + '/' + full.MainPicture + '"></a>';
                }
            },
            {
                "data": "LitterPresentationName", "name": "LitterPresentationName", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<a href="/Kennel/EditLitter/' + full.Id + '"><b>' + full.LitterPresentationName + '</b></a>';
                }
            },
            { "data": "BreedLink", "name": "BreedLink", "autoWidth": true },
            { "data": "DogLink", "name": "DogLink", "autoWidth": true },            
            { "data": "DogFather", "name": "DogFather", "autoWidth": true },
            { "data": "BornDate", "name": "BornDate", "autoWidth": true },
            { "data": "FemaleCount", "name": "FemaleCount", "autoWidth": true },
            { "data": "MaleCount", "name": "MaleCount", "autoWidth": true },
            { "data": "LitterLink", "name": "LitterLink", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    return '<a class="btn btn-xs btn-danger" href="/Kennel/EditLitter/' + full.Id + '">Edytuj</a>';
                }
            }
        ]

    });

    // DataTable
    var littersTable = $('#littersDataTable').DataTable();

    // Apply the search
    littersTable.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });

    // --------------------------------- Puppies List ---------------------------------------------

    $('#puppiesDataTable tfoot th').each(function () {
        var title = $('#puppiesDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#puppiesDataTable").DataTable({
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
        "order": [[3, "desc"]],       
        "ajax": {
            "url": "/AjaxKennel/PuppiesAjaxList",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs":
            [
                {
                    "targets": [0],
                    "visible": true,
                    "searchable": true,
                    "width": "20px"
                },
                {
                    "targets": [1],
                    "visible": true,
                    "searchable": true,
                    "width": "200px"
                },
                {
                    "width": "30px",
                    "targets": [6],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "30px",
                    "targets": [7],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "400px",
                    "targets": [10],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                }
            ],
        "columns": [
            {
                "data": "Id", "name": "Id", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<a href="/Kennel/EditPuppy/' + full.Id + '"><img style="width:70px;" class="dataTableImg" src="/Puppies/' + full.BreedLink + '/' + full.PuppyLink + '/' + full.MainPicture + '"></a>';
                }
            },
            {
                "render": function (data, type, full, meta) {
                    if (full.PuppySex)
                    {
                        return '<a href="/Kennel/EditPuppy/' + full.Id + '" target="_blank"><b><i class="fa fa-mars"></i> ' + full.PuppyName + '</b></a>';
                    } else
                    {
                        return '<a href="/Kennel/EditPuppy/' + full.Id + '" target="_blank"><b><i class="fa fa-venus"></i> ' + full.PuppyName + '</b></a>';
                    }
                    
                }
            },
            { "data": "BreedLink", "name": "BreedLink", "autoWidth": true },
            { "data": "LitterLink", "name": "LitterLink", "autoWidth": true },
            { "data": "DogLink", "name": "DogLink", "autoWidth": true },
            { "data": "BornWeight", "name": "BornWeight", "autoWidth": true },
            { "data": "BornDate", "name": "BornDate", "autoWidth": true },
            { "data": "PuppySex", "name": "PuppySex", "autoWidth": true },
            { "data": "PuppyColour", "name": "PuppyColour", "autoWidth": true },
            { "data": "PuppyLink", "name": "PuppyLink", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    return '<a class="btn btn-xs btn-danger" href="/Kennel/EditPuppy/' + full.Id + '">Edytuj</a>'
                }
            }
        ]
    });

    // DataTable
    var puppiesTable = $('#puppiesDataTable').DataTable();

    // Apply the search
    puppiesTable.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });

    // --------------------------------- Genealogic Trees Dogs List ---------------------------------------------

    $('#genealogicTreeDataTable tfoot th').each(function () {
        var title = $('#genealogicTreeDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#genealogicTreeDataTable").DataTable({
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
            "url": "/AjaxTree/TreesList",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs":
            [
                {
                    "targets": [0],
                    "visible": true,
                    "searchable": true,
                    "width": "10px"
                },
                {
                    "targets": [1],
                    "visible": true,
                    "searchable": true,
                    "width": "100px"
                },
                {
                    "width": "500px",
                    "targets": [2],
                    "visible": true,
                    "searchable": true,
                },
                {
                    "width": "100px",
                    "targets": [3],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "100px",
                    "targets": [4],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "100px",
                    "targets": [5],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                }
            ],
        "columns": [
            //{
            //    "data": "Id", "name": "Id", "autoWidth": true, "render": function (data, type, full, meta) {
            //        return '<a href="/Kennel/EditPuppy/' + full.Id + '"><img style="width:70px;" class="dataTableImg" src="/Puppies/' + full.BreedLink + '/' + full.PuppyLink + '/' + full.MainPicture + '"></a>';
            //    }</a>
            //},
            {
                "data": "Id", "name": "Id", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<a href="/Tree/EditTree/' + full.Id + '"><img style="width:70px;" class="dataTableImg" src="' + full.DogTreeTooltip_Image + '"></a>';
                }
            },
            {
                "data": "DogLink", "name": "DogLink", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<b><a href="/Tree/EditTree/' + full.Id + '">' + full.DogLink + '</a></b>';
                }
            },
            { "data": "DogTreeBox", "name": "DogTreeBox", "autoWidth": true },
            {
                "data": "CreateDate", "name": "CreateDate",
                "render": function (value) {
                    if (value === null) return "";
                    return moment(value).format('DD.MM.YYYY');
                },
            },  
            {
                "data": "EditDate", "name": "EditDate",
                "render": function (value) {
                    if (value === null) return "";
                    return moment(value).format('DD.MM.YYYY');
                },
            },    
            {
                "render": function (data, type, full, meta) {
                    return '<a class="btn btn-xs btn-danger" href="/Tree/EditTree/' + full.Id + '">Edycja</a>';
                }
            }
        ]
    });

    // DataTable
    var treesTable = $('#genealogicTreeDataTable').DataTable();

    // Apply the search
    treesTable.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });

    // --------------------------------- Genealogic Trees Litters List ---------------------------------------------

    $('#genealogicLittersTreeDataTable tfoot th').each(function () {
        var title = $('#genealogicLittersTreeDataTable thead th').eq($(this).index()).text();
        $(this).html('<input class="searchInput notEmpty" type="text" placeholder="szukaj" />');
    });

    $("#genealogicLittersTreeDataTable").DataTable({
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
            "url": "/AjaxTree/LittersTreesList",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs":
            [
                {
                    "targets": [0],
                    "visible": true,
                    "searchable": true,
                    "width": "10px"
                },
                {
                    "targets": [1],
                    "visible": true,
                    "searchable": true,
                    "width": "100px"
                },
                {
                    "width": "500px",
                    "targets": [2],
                    "visible": true,
                    "searchable": true,
                },
                {
                    "width": "100px",
                    "targets": [3],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "100px",
                    "targets": [4],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                },
                {
                    "width": "100px",
                    "targets": [5],
                    "visible": true,
                    "searchable": true,
                    "className": "dt-body-center"
                }
            ],
        "columns": [
            //{
            //    "data": "Id", "name": "Id", "autoWidth": true, "render": function (data, type, full, meta) {
            //        return '<a href="/Kennel/EditPuppy/' + full.Id + '"><img style="width:70px;" class="dataTableImg" src="/Puppies/' + full.BreedLink + '/' + full.PuppyLink + '/' + full.MainPicture + '"></a>';
            //    }</a>
            //},
            {
                "data": "Id", "name": "Id", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<a href="/Tree/EditLitterTree/' + full.Id + '"><img style="width:70px;" class="dataTableImg" src="' + full.DogTreeTooltip_Image + '"></a>';
                }
            },
            {
                "data": "LitterLink", "name": "LitterLink", "autoWidth": true, "render": function (data, type, full, meta) {
                    return '<b><a href="/Tree/EditTree/' + full.Id + '">' + full.LitterLink + '</a></b>';
                }
            },
            { "data": "DogTreeBox", "name": "DogTreeBox", "autoWidth": true },
            {
                "data": "CreateDate", "name": "CreateDate",
                "render": function (value) {
                    if (value === null) return "";
                    return moment(value).format('DD.MM.YYYY');
                },
            },
            {
                "data": "EditDate", "name": "EditDate",
                "render": function (value) {
                    if (value === null) return "";
                    return moment(value).format('DD.MM.YYYY');
                },
            },
            {
                "render": function (data, type, full, meta) {
                    return '<a class="btn btn-xs btn-danger" href="/Tree/EditLitterTree/' + full.Id + '">Edycja</a>';
                }
            }
        ]
    });

    // DataTable
    var treesTable = $('#genealogicLittersTreeDataTable').DataTable();

    // Apply the search
    treesTable.columns().every(function () {
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