//$(".progress-bar").animate({
//    width: "70%"
//}, 2500);

function OnEditSuccessOperations() {
    $('#editReservationModal').modal('hide');
    var table = $('#reservationDataTable').DataTable();
    table.ajax.reload();
}

function OnSuccessOperations() {
    $('#addReservationModal').modal('hide');
    var table = $('#reservationDataTable').DataTable();
    table.ajax.reload();
}

function OnSendEmailSuccessOperations() {
    $('#sendEmailModal').modal('hide');
}

$(document).ready(function () {

    $('#addReservationModal').on('shown.bs.modal', function () {
        $('#FirstName').focus();        
    });  

    $("#newPosition").click(function () {
        $('#addReservationModal').modal('show');        
    });

    $("#editReservation").click(function () {
        $('#editReservationModal').modal('show');
    });

    $(document).on('click', ".removeReservation", function () {
        var table = $('#reservationDataTable').DataTable();
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        $.ajax(
            {
                type: "GET",
                url: "/Reservation/RemoveReservation",
                data: { "Id": id }
            }).success(function () {
                table.ajax.reload();
                newAlert('success', 'Sukces', 'Pozycja została usunięta');
            }).error(function () {
                table.ajax.reload();
                newAlert('danger', 'Błąd', 'Nie można usunąć danej pozycji. (0x14321445)');
            });
    });

    $(document).on('click', ".editReservation", function () {
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: '/Reservation/EditReservation',
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {
                $('#editReservationModalContent').html(data);
                $('#editReservationModal').modal(options);
                $('#editReservationModal').modal('show');
            },
            error: function () {
                alert("Błąd ładowania danych.");
            }
        });
    });

    $(document).on('click', ".sendEmail", function () {
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: '/Reservation/SendEmail',
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {

                $('#sendEmailModalContent').html(data);
                $('#sendEmailModal').modal(options);
                $('#sendEmailModal').modal('show');

            },
            error: function () {
                alert("Błąd ładowania danych.");
            }
        });
    });

    $(document).on('click', ".reverseReadStatus", function () {
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: '/Reservation/ReverseReadStatus',
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {                
                var table = $('#reservationDataTable').DataTable();
                table.ajax.reload();
            },
            error: function () {
                alert("Błąd ładowania danych.");
            }
        });
    });

    $(document).on('click', ".reverseClosedStatus", function () {
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: '/Reservation/ReverseClosedStatus',
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {
                var table = $('#reservationDataTable').DataTable();
                table.ajax.reload();
                var tableClosed = $('#reservationDataTableClosed').DataTable();
                tableClosed.ajax.reload();
            },
            error: function () {
                alert("Błąd ładowania danych.");
            }
        });
    });
});