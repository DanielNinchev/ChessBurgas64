$(document).ready(function () {
    $("#groupLessonsTable").DataTable({
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
            "url": "/Groups/GetGroupLessons",
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
                "targets": 5,
                "orderable": false,
            },
        ],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "startingTime", "name": "StartingTime", "autoWidth": true },
            { "data": "topic", "name": "Topic", "autoWidth": true },
            { "data": "groupTrainerUserFirstName", "name": "Group.Trainer.User.FirstName", "autoWidth": true },
            { "data": "membersCount", "name": "Members.Count", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    return "<button class='btn btn-secondary' id='groupLessonViewBtn' onclick=GoToLessonByIdView('" + full.id + "'); >Преглед</button> <button class='btn btn-danger' id='groupLessonDeleteBtn' onclick=DeleteLessonData('" + full.id + "'); >Изтриване</button>";
                },
            },
        ]
    });
});

function GoToLessonByIdView(id) {
    window.location.href = 'https://localhost:44319/Lessons/ById/' + id;
}

function DeleteLessonData(id) {
    if (confirm("Наистина ли желаете да изтриете данните за това занятие?")) {
        DeleteLesson(id);
    } else {
        return false;
    }
}

function DeleteLesson(id) {
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