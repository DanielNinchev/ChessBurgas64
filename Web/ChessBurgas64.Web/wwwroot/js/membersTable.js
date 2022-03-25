$(document).ready(function () {
    $("#membersTable").DataTable({
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
            "url": "/Groups/GetGroupMembers",
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
                "targets": 5,
                "render": function (data, type, row) {
                    return (data)
                        ? moment(data, "YYYY-MM-DD").format("DD/MM/YYYY")
                        : null;
                }
            },
            {
                "targets": 6,
                "render": function (data, type, row) {
                    return (data)
                        ? moment(data, "YYYY-MM-DD").format("DD/MM/YYYY")
                        : null;
                }
            },
        ],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "userFirstName", "name": "UserFirstName", "autoWidth": true },
            { "data": "userMiddleName", "name": "UserMiddleName", "autoWidth": true },
            { "data": "userLastName", "name": "UserLastName", "autoWidth": true },
            { "data": "clubRating", "name": "ClubRating", "autoWidth": true },
            { "data": "dateOfJoiningCurrentGroup", "name": "DateOfJoiningCurrentGroup", "autoWidth": true },
            { "data": "lastAttendance", "name": "LastAttendance", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    return "<a class='btn btn-info' onclick=GoToByIdView('" + full.id + "'); >Преглед</a> <a class='btn btn-danger' onclick=DeleteData('" + full.id + "'); >Премахване</a>";
                },
            },
        ]
    });
});

function GoToByIdView(id) {
    window.location.href = 'https://localhost:44319/Users/ByMemberId/' + id;
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
        url: "/Members/Delete",
        "beforeSend": function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { id: id },
    });

    window.location.reload();
}