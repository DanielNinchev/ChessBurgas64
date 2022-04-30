$(document).ready(function () {
    $("#paymentsTable").DataTable({
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
            "url": "/Users/GetUserPayments",
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
            {
                "targets": 4,
                "orderable": false,
            },
        ],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "dateOfPayment", "name": "DateOfPayment", "autoWidth": true },
            { "data": "description", "name": "Description", "autoWidth": true },
            { "data": "amount", "name": "Amount", "autoWidth": true },
            {
                "render": function (data, type, full, meta) {
                    return "<a class='btn btn-secondary border border-white' onclick=GoToPaymentEditView('" + full.id + "'); >Промяна</a> <a class='btn btn-danger border border-white' onclick=DeletePaymentData('" + full.id + "'); >Изтриване</a>";
                },
            },
        ]
    });
});

function GoToPaymentEditView(id) {
    window.location.href = 'https://localhost:44319/Payments/Edit/' + id;
}

function DeletePaymentData(id) {
    if (confirm("Наистина ли желаете да изтриете данните за това разплащане?")) {
        DeletePayment(id);
    } else {
        return false;
    }
}

function DeletePayment(id) {
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
