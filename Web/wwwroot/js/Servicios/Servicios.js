var tablaServicios;

$(document).ready(function () {
    tablaServicios = $('#servicios').DataTable({
        ajax: {
            url: 'https://localhost:7059/api/Servicios/BuscarServicios',
            dataSrc: ""
        },
        columns: [
            { data: 'id', title: 'Id' },
            { data: 'nombre', title: 'Descripcion' },
            {
                data: function (data) {
                    return data.activo == true ? "Si" : "No";
                }, title: 'Activo'
            },
            {
                data: function (data) {
                    var botones =
                        `<td><a href='javascript:EditarServicio(${JSON.stringify(data)})'><i class="fa-solid fa-pen-to-square editarServicio"></i></td>` +
                        `<td><a href='javascript:EliminarServicio(${JSON.stringify(data)})'><i class="fa-solid fa-trash eliminarServicio"></i></td>`
                    return botones;
                }
            }
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.13.1/i18n/es-ES.json"
        },
    });
});

function GuardarServicio() {
    debugger
    $("#serviciosAddPartial").html("");

    $.ajax({
        type: "GET",
        url: "/Servicios/ServiciosAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            $("#serviciosAddPartial").html(resultado);
            $("#servicioModal").modal('show');

        }
    });
}

function EditarServicio(data) {
    debugger
    $("#serviciosAddPartial").html("");

    $.ajax({
        type: "POST",
        url: "/Servicios/ServiciosAddPartial",
        data: JSON.stringify(data),
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            $("#serviciosAddPartial").html(resultado);
            $("#servicioModal").modal('show');

        }
    });
}

function EliminarServicio(data) {
    Swal.fire({
        title: 'Estas Seguro?',
        text: "Estas seguro que deseas eliminar el servicio?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Eliminar!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/Servicios/EliminarServicio",
                data: JSON.stringify(data),
                contentType: "application/json",
                dataType: "html",
                success: function (resultado) {
                    Swal.fire(
                        'Eliminado!',
                        'El servicio fue eliminado.',
                        'success'
                    )
                    debugger
                    tablaServicios.ajax.reload();
                }
            });
        }
    })
}