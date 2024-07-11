
$(document).ready(function () {
    $('#addItemForm').on('submit', function (e) {
        e.preventDefault();
        var reponse = $.post("/TodoItem/Create", $('#addItemForm').serialize());

        reponse.done(function (data) {
            location.reload();
        });
    });
});