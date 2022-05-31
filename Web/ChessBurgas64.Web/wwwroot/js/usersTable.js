$(document).ready(function () {
    $("#usersTable").DataTable({
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
            {
                "targets": 9,
                "orderable": false,
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
                "render": function (data, type, full, meta) { return "<a class='btn btn-secondary border border-white' onclick=GoToProfile('" + full.id + "'); >Профил</a>" }
            },
        ]
    });
});

function GoToProfile(id) {
    window.location.href = '/Users/ById/' + id;
}