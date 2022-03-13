$(document).ready(function () {
    $("#lessonsTable").DataTable({
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
                        ? moment(data, "YYYY-MM-DD").format("DD/MM/YYYY")
                        : null;
                }
            },
        ],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "startingTime", "name": "StartingTime", "autoWidth": true },
            { "data": "topic", "name": "Topic", "autoWidth": true },
            { "data": "groupName", "name": "GroupName", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    return "<a class='btn btn-secondary border border-white' onclick=GoToEditView('" + full.id + "'); >Промяна</a> <a class='btn btn-danger border border-white' onclick=DeleteData('" + full.id + "'); >Изтриване</a>";
                },
            },
        ]
    });
});

function GoToEditView(id) {
    window.location.href = 'https://localhost:44319/Payments/Edit/' + id;
}

function DeleteData(id) {
    if (confirm("Наистина ли желаете да изтриете данните за това разплащане?")) {
        Delete(id);
    } else {
        return false;
    }
}

function Delete(id) {
    $.ajax({
        type: 'POST',
        url: "/Payments/Delete",
        "beforeSend": function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { id: id },
    });

    window.location.reload();
}