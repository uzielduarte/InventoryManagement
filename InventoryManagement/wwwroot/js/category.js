let datatable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Category/GetAll"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "description", "width": "40%" },
            {
                "data": "status",
                "render": function (data) {
                    return data === true ? "Activo" : "Inactivo"
                },
                "width": "20%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Category/Upsert/${data}" class="btn btn-success text-white cursor"><i class="bi bi-pencil-square"></i></a>
                            <a onclick=Delete("/Admin/Category/Delete/${data}") class="btn btn-danger text-white cursor"><i class="bi bi-trash-fill"></i></a>
                        </div>
                    `
                },
                "width": "20%"
            }
        ],
        "scrollX": true,
        "language": {
            "lengthMenu": "Mostrar _MENU_ Registros Por Página",
            "zeroRecords": "Ningún Registro",
            "info": "Mostrando página _PAGE_ de _PAGES_",
            "infoEmpty": "No hay registros",
            "infoFiltered": "(filtrado de _MAX_ registros totales)",
            "search": "Buscar",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    })
}

function Delete(url) {
    swal({
        title: "¿Esta seguro de eliminar la categoría?",
        text: "Este registro no podrá ser recuperado.",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((remove) => {
        if(remove) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success == true) {
                        toastr.success(data.message)
                        datatable.ajax.reload()
                    } else {
                        toastr.error(data.message)
                    }
                }
            })
        }
    })
}