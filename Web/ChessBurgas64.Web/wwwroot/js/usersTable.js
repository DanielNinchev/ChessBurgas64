$(document).ready(function () {
    $("#usersTable").DataTable({
        "processing": true,
        "responsive": true,
        "serverSide": true,
        "filter": true,
        "language": {
            "lengthMenu": "Показване на _MENU_ потребители на страница",
            "zeroRecords": "Няма открити съвпадения",
            "info": "Открити са _PAGE_ от _PAGES_ потребители",
            "infoEmpty": "Няма потребители",
            "infoFiltered": "(претърсено от _MAX_ потребители общо)",
        },
        "ajax": {
            "url": "/Users/GetUsers",
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
                        ? moment(data, "YYYY-MM-DD").format("DD/MM/YYYY")
                        : null;
                }
            },
        ],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "firstName", "name": "FirstName", "autoWidth": true },
            { "data": "middleName", "name": "MiddleName", "autoWidth": true },
            { "data": "lastName", "name": "LastName", "autoWidth": true },
            { "data": "birthDate", "name": "BirthDate", "autoWidth": true,},
            { "data": "gender", "name": "Gender", "autoWidth": true },
            { "data": "phoneNumber", "name": "PhoneNumber", "autoWidth": true },
            { "data": "email", "name": "Email", "autoWidth": true },
            { "data": "clubStatus", "name": "ClubStatus", "autoWidth": true },
            {
                "render": function (data, type, full, meta) { return "<a class='btn btn-primary border border-white' onclick=GoToEditView('" + full.id + "'); >Профил</a>" }
            },
        ]
    });
});

function GoToEditView(id) {
    window.location.href = 'ById/' + id;
}

function DeleteData(studentId) {
    if (confirm("Are you sure you want to delete ...?")) {
        Delete(studentId);
    } else {
        return false;
    }
}

function ChangeStatus(studentId) {
    var url = '@Url.Content("~/")' + "Announcements/Create";
}

function Delete(studentId) {
    var url = '@Url.Content("~/")' + "Students/Delete";

    $.post(url, { ID: studentId }, function (data) {
        if (data) {
            oTable = $('#studentsDatatable').DataTable();
            oTable.draw();
        } else {
            alert("Something Went Wrong!");
        }
    });
}
