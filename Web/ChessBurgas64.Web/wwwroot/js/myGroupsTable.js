$(document).ready(function () {
    $("#myGroupsTable").DataTable({
        "processing": true,
        "responsive": true,
        "serverSide": true,
        "filter": true,
        "language": {
            "lengthMenu": "Показване на най-много _MENU_ реда на страница",
            "zeroRecords": "Няма съвпадения",
            "info": "Страница _PAGE_ от _PAGES_",
            "infoEmpty": "Няма данни",
            "infoFiltered": "(претърсено от _MAX_ потребители общо)",
            "search": "Търсене:",
            "paginate": {
                "first": "Първа",
                "last": "Последна",
                "next": "Следваща",
                "previous": "Предишна"
            },
        },
        "ajax": {
            "url": '/Identity/Account/Manage/MySchedule?Groups',
            "beforeSend": function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            "type": "POST",
            "datatype": "json",
        },
        "columnDefs": [
            {
                "targets": 0,
                "visible": false,
                "searchable": false,
                "className": "dt-body-center",
            },
            {
                "targets": 4,
                "render": function (data, type, row) {
                    return (data)
                        ? moment(data, "YYYY-MM-DD, H:mm").format("H:mm")
                        : null;
                }
            },
        ],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "name", "name": "Name", "autoWidth": true },
            { "data": "membersCount", "name": "Members.Count", "autoWidth": true, },
            { "data": "trainingDay", "name": "TrainingDay", "autoWidth": true },
            { "data": "trainingHour", "name": "TrainingHour", "autoWidth": true },
            { "data": "trainerName", "name": "Trainer.User.FirstName", "autoWidth": true },
            { "data": "lowestRating", "name": "LowestRating", "autoWidth": true },
            { "data": "highestRating", "name": "HighestRating", "autoWidth": true },
        ],
        "order": [[3, "asc"], [5, "asc"]],
    });
});