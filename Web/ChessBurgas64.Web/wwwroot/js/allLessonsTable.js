$(document).ready(function () {
    $("#allLessonsTable").DataTable({
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
            "url": "/Lessons/GetLessons",
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
                "targets": 1,
                "render": function (data, type, row) {
                    return (data)
                        ? moment(data, "YYYY-MM-DD H:mm").format("DD/MM/YYYY H:mm")
                        : null;
                }
            },
            {
                "targets": 6,
                "orderable": false,
            },
        ],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "startingTime", "name": "StartingTime", "autoWidth": true },
            { "data": "topic", "name": "Topic", "autoWidth": true },
            { "data": "groupName", "name": "Group.Name", "autoWidth": true },
            { "data": "trainerName", "name": "Group.Trainer.User.FirstName", "autoWidth": true },
            { "data": "membersCount", "name": "Members.Count", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    return "<a class='btn btn-secondary' onclick=GoToByIdView('" + full.id + "'); >Преглед</a> <a class='btn btn-danger' onclick=DeleteData('" + full.id + "'); >Изтриване</a>";
                },
            },
        ]
    });
});

function GoToByIdView(id) {
    window.location.href = '/Lessons/ById/' + id;
}

function DeleteData(id) {
    if (confirm("Наистина ли желаете да изтриете данните за това занятие?")) {
        Delete(id);
    } else {
        return false;
    }
}

function Delete(id) {
    $.ajax({
        type: 'POST',
        url: "/Lessons/DeleteUserLesson",
        "beforeSend": function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { id: id },
    });

    window.location.reload();
}
