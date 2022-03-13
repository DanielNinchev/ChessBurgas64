$(document).ready(function () {
    $("#groupsTable").DataTable({
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
            "url": "/Groups/GetGroups",
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
        ],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "name", "name": "Name", "autoWidth": true },
            { "data": "lowestRating", "name": "LowestRating", "autoWidth": true },
            { "data": "highestRating", "name": "HighestRating", "autoWidth": true },
            { "data": "membersCount", "name": "MembersCount", "autoWidth": true, },
            {
                "render": function (data, type, full, meta) { return "<a class='btn btn-secondary border border-white' onclick=GoToGroup('" + full.id + "'); >Преглед</a> <a class='btn btn-danger border border-white' onclick=GoToGroup('" + full.id + "'); >Изтриване</a>" }
            },
        ]
    });
});

function GoToGroup(id) {
    window.location.href = 'https://localhost:44319/Groups/ById/' + id;
}