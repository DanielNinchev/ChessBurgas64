$(document).ready(function () {
    $("#myLessonsTable").DataTable({
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
            "url": '/Identity/Account/Manage/MyLessons?Lessons',
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
        ],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "startingTime", "name": "StartingTime", "autoWidth": true },
            { "data": "topic", "name": "Topic", "autoWidth": true },
            { "data": "groupName", "name": "Group.Name", "autoWidth": true },
        ],
        "order": [[1, "desc"], [3, "asc"]],
    });
});
