//$(".progress-bar").animate({
//    width: "70%"
//}, 2500);

function OnEditSuccessOperations() {
    $('#editReproductionModal').modal('hide');
    var table = $('#reproductionDataTable').DataTable();
    table.ajax.reload();
}

function OnSuccessOperations() {
    $('#addReproductionModal').modal('hide');
    var table = $('#reproductionDataTable').DataTable();
    table.ajax.reload();    
}

$(document).ready(function () {

    $("#newPosition").click(function () {
        $('#addReproductionModal').modal('show');
    });

    $("#editReproduction").click(function () {
        $('#editReproductionModal').modal('show');
    });

    $(document).on('click', ".removeReproduction", function () {
        var table = $('#reproductionDataTable').DataTable();
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        $.ajax(
            {
                type: "GET",
                url: "/Reproduction/RemoveReproduction",
                data: { "Id": id }
            }).success(function () {
                table.ajax.reload();
                newAlert('success', 'Sukces', 'Pozycja została usunięta');
            }).error(function () {
                table.ajax.reload();
                newAlert('danger', 'Błąd', 'Nie można usunąć danej pozycji. (0x1442345)');
            });
    });

    var editReproductionURL = '/Reproduction/EditReproduction';
    $(document).on('click', ".editReproduction", function () {

        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: editReproductionURL,
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {

                $('#editReproductionModalContent').html(data);
                $('#editReproductionModal').modal(options);
                $('#editReproductionModal').modal('show');

            },
            error: function () {
                alert("Błąd ładowania danych.");
            }
        });
    });
});