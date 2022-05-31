$(document).ready(function () {
    $("#userGroupsTable").DataTable({
        "processing": true,
        "responsive": true,
        "serverSide": true,
        "filter": true,
        "language": {
            "lengthMenu": "Показване на най-много _MENU_ реда на страница",
            "zeroRecords": "Няма данни.",
            "info": "Страница _PAGE_ от _PAGES_",
            "infoEmpty": "Няма съвпадения.",
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
            "url": "/Users/GetUserGroups",
            "beforeSend": function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            {
                "targets": [0],
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
            {
                "targets": [7],
                "orderable": false,
            },
        ],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "name", "name": "Name", "autoWidth": true },
            { "data": "membersCount", "name": "Members.Count", "autoWidth": true, },
            { "data": "trainingDay", "name": "TrainingDay", "autoWidth": true },
            { "data": "trainingHour", "name": "TrainingHour", "autoWidth": true },
            { "data": "lowestRating", "name": "LowestRating", "autoWidth": true },
            { "data": "highestRating", "name": "HighestRating", "autoWidth": true },
            {
                "render": function (data, type, full, meta) { return "<a class='btn btn-secondary border border-white' onclick=GoToGroup('" + full.id + "'); >Преглед</a> <a class='btn btn-danger border border-white' onclick=DeleteGroupData('" + full.id + "'); >Премахване</a>" }
            },
        ]
    });
});

function GoToGroup(id) {
    window.location.href = '/Groups/ById/' + id;
}

function DeleteGroupData(id) {
    if (confirm("Наистина ли желаете да премахнете тази група от таблицата?")) {
        Delete(id);
    } else {
        return false;
    }
}

function Delete(id) {
    $.ajax({
        type: 'POST',
        url: "/Groups/Delete",
        "beforeSend": function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { id: id },
    });

    window.location.reload();
}