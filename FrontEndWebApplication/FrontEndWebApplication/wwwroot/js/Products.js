document.addEventListener('DOMContentLoaded', function () {
    loadProducts();
});

function loadProducts() {
    fetch('/Products/GetAllProducts') // Asegúrate de reemplazar con la ruta correcta
        .then(response => response.json())
        .then(data => {
            initializeDataTable(data.data);
        })
        .catch(error => console.error('Error:', error));
}


function initializeDataTable(Products) {
    let table = document.getElementById('ProductsTable');
    if (!table) {
        table = document.createElement('table');
        table.id = 'ProductsTable';
        table.className = 'display'; // Clase necesaria para DataTables
        document.getElementById('ProductsContainer').appendChild(table);
    }

    $(table).DataTable({
        responsive: true,
        data: Products,
        columns: [
            { title: "ID", data: "id", className: "column-id" },
            { title: "Nombre del Producto", data: "productName", className: "column-name" },
            { title: "ID del Proveedor", data: "supplierId", className: "column-supplierId" },
            { title: "Precio Unitario", data: "unitPrice", className: "column-unitPrice" },
            { title: "Empaque", data: "package", className: "column-package" },
            { title: "¿Descontinuado?", data: "isDiscontinued", className: "column-isDiscontinued" },
            {
                title: "Acciones",
                data: "id",
                render: function (data) {
                    return `<div class="text-center">
                                <a href="/Products/Edit/${data}" class=""><i class="fa fa-edit"></i></a>
                                <a onclick="Delete('/Products/Delete/${data}')" class=""><i class="fa fa-trash"></i></a>
                            </div>`;
                },
                className: "column-actions"
            }
        ]
    });
}


function Delete(url) {
    Swal.fire({
        title: "¿Esta seguro de querer borrar el registro?",
        text: "¡Esta accion no puede ser revertida!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, borralo!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (response) {
                    if (response && response.success) {
                        toastr.success(response.message || "Registro eliminado con exito.");
                        // Recargar DataTables
                        $('#ProductsTable').DataTable().clear().destroy();
                        loadProducts();
                    } else {
                        toastr.error(response.message || "Ocurrio un error desconocido.");
                    }
                },
                error: function (error) {
                    toastr.error("Error al intentar eliminar el registro.");
                    console.error('Error:', error);
                }
            });
        }
    });
}