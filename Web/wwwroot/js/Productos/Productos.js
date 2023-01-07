var tablaProductos;

$(document).ready(function () {
    var token = getCookie("Token");
    var ajaxUrl = getCookie("AjaxUrl");
    tablaProductos = $('#productos').DataTable({
        ajax: {
            //url: 'https://localhost:7059/api/Productos/BuscarProductos',
            url: `${ajaxUrl}Productos/BuscarProductos`,
            dataSrc: "",
            headers: { "Authorization": "Bearer " + token }
        },
        columns: [
            { data: 'id', title: 'Id' },
            {
                data: 'imagen', render: function (data) {
                    if (data != null) {
                        return '<img src="data:image/jpeg;base64,' + data + '"width="100px" height="100px">';
                    } else {
                        return '<img src="/images/noexiste.png"width="100px" height="100px">';

                    }
                }, title: 'Imagen'
            },
            { data: 'descripcion', title: 'Descripcion' },
            { data: 'precio', title: 'Precio' },
            { data: 'stock', title: 'Stock' },
            {
                data: function (data) {
                    return data.activo == true ? "Si" : "No";
                }, title: 'Activo'
            },
            {
                data: function (data) {
                    var botones =
                        `<td><a href='javascript:EditarProducto(${JSON.stringify(data)})'><i class="fa-solid fa-pen-to-square editarProducto"></i></td>` +
                        `<td><a href='javascript:EliminarProducto(${JSON.stringify(data)})'><i class="fa-solid fa-trash eliminarProducto"></i></td>`
                    return botones;
                }
            }
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.13.1/i18n/es-ES.json"
        },
    });
});

function GuardarProducto() {
    $("#productosAddPartial").html("");

    $.ajax({
        type: "GET",
        url: "/Productos/ProductosAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            $("#productosAddPartial").html(resultado);
            $("#productoModal").modal('show');

        }
    });
}

function EditarProducto(data) {
    $("#productosAddPartial").html("");

    $.ajax({
        type: "POST",
        url: "/Productos/ProductosAddPartial",
        data: JSON.stringify(data),
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            $("#productosAddPartial").html(resultado);
            $("#productoModal").modal('show');

        }
    });
}

function EliminarProducto(data) {
    Swal.fire({
        title: 'Estas Seguro?',
        text: "Estas seguro que deseas eliminar el producto?",
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
                url: "/Productos/EliminarProducto",
                data: JSON.stringify(data),
                contentType: "application/json",
                dataType: "html",
                success: function (resultado) {
                    Swal.fire(
                        'Eliminado!',
                        'El producto fue eliminado.',
                        'success'
                    )
                    tablaProductos.ajax.reload();
                }
            });
        }
    })
}