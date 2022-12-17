var tablaUsuarios;

$(document).ready(function () {
    debugger
    var token = getCookie("Token");
    tablaUsuarios = $('#usuarios').DataTable({
        ajax: {
            url: 'https://localhost:7059/api/Usuarios/BuscarUsuarios',
            dataSrc: "",
            headers: {"Authorization": "Bearer " + token}
        },
        columns: [
            { data: 'id', title: 'Id' },
            { data: 'nombre', title: 'Nombre' },
            { data: 'apellido', title: 'Apellido' },
            {
                data: function (data) {
                    return moment(data.fecha_Nacimiento).format("DD/MM/YYYY");
                }, title: 'Fecha nacimiento' },
            { data: 'mail', title: 'Mail' },
            { data: 'roles.nombre', title: 'Rol' },
            {
                data: function (data) {
                    console.log("roles", data)
                    return data.activo == true ? "Si" : "No";
                }, title: 'Activo'
            },
            {
                data: function (data) {
                    var botones =
                        `<td><a href='javascript:EditarUsuario(${JSON.stringify(data)})'><i class="fa-solid fa-pen-to-square editarUsuario"></i></td>` +
                        `<td><a href='javascript:EliminarUsuario(${JSON.stringify(data)})'><i class="fa-solid fa-trash eliminarUsuario"></i></td>`
                    return botones;
                }
            }
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.13.1/i18n/es-ES.json"
        },
    });
});

function GuardarUsuario() {
    debugger
    $("#usuariosAddPartial").html("");

    $.ajax({
        type: "GET",
        url: "/Usuarios/UsuariosAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            $("#usuariosAddPartial").html(resultado);
            $("#usuarioModal").modal('show');

        }
    });
}

function EditarUsuario(data) {
    debugger
    $("#usuariosAddPartial").html("");

    $.ajax({
        type: "POST",
        url: "/Usuarios/UsuariosAddPartial",
        data: JSON.stringify(data),
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            $("#usuariosAddPartial").html(resultado);
            $("#usuarioModal").modal('show');

        }
    });
}

function EliminarUsuario(data) {
    Swal.fire({
        title: 'Estas Seguro?',
        text: "Estas seguro que deseas eliminar el usuario?",
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
                url: "/Usuarios/EliminarUsuario",
                data: JSON.stringify(data),
                contentType: "application/json",
                dataType: "html",
                success: function (resultado) {
                    Swal.fire(
                        'Eliminado!',
                        'El usuario fue eliminado.',
                        'success'
                    )
                    debugger
                    tablaUsuarios.ajax.reload();
                }
            });
        }
    })
}