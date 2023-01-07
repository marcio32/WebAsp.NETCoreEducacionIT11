var tablaRoles;

$(document).ready(function () {
    var token = getCookie("Token");
    var ajaxUrl = getCookie("AjaxUrl");
    tablaRoles = $('#roles').DataTable({
        ajax: {
            //url: 'https://localhost:7059/api/Roles/BuscarRoles',
            url: `${ajaxUrl}Roles/BuscarRoles`,
            dataSrc: "",
            headers: { "Authorization": "Bearer " + token }
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
                        `<td><a href='javascript:EditarRol(${JSON.stringify(data)})'><i class="fa-solid fa-pen-to-square editarRol"></i></td>` +
                        `<td><a href='javascript:EliminarRol(${JSON.stringify(data)})'><i class="fa-solid fa-trash eliminarRol"></i></td>`
                    return botones;
                }
            }
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.13.1/i18n/es-ES.json"
        },
    });
});

function GuardarRol() {
    $("#rolesAddPartial").html("");

    $.ajax({
        type: "GET",
        url: "/Roles/RolesAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            $("#rolesAddPartial").html(resultado);
            $("#rolModal").modal('show');

        }
    });
}

function EditarRol(data) {
    $("#rolesAddPartial").html("");

    $.ajax({
        type: "POST",
        url: "/Roles/RolesAddPartial",
        data: JSON.stringify(data),
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            $("#rolesAddPartial").html(resultado);
            $("#rolModal").modal('show');

        }
    });
}

function EliminarRol(data) {
    Swal.fire({
        title: 'Estas Seguro?',
        text: "Estas seguro que deseas eliminar el rol?",
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
                url: "/Roles/EliminarRol",
                data: JSON.stringify(data),
                contentType: "application/json",
                dataType: "html",
                success: function (resultado) {
                    Swal.fire(
                        'Eliminado!',
                        'El rol fue eliminado.',
                        'success'
                    )
                    tablaRoles.ajax.reload();
                }
            });
        }
    })
}