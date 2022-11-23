$(document).ready(function () {
    $('#productos').DataTable({
        ajax: {
            url: 'https://localhost:7059/api/Productos/BuscarProductos',
            dataSrc: ""
        },
        columns: [
            { data: 'id', title: 'Id' },
            { data: 'descripcion', title: 'Descripcion' },
            { data: 'precio', title: 'Precio' },
            { data: 'stock', title: 'Stock' },
            { data: 'imagen', title: 'Imagen' },
            { data: 'activo', title: 'Activo' }
        ],
        language: {
            url:"https://cdn.datatables.net/plug-ins/1.13.1/i18n/es-ES.json"
        },
    });
});