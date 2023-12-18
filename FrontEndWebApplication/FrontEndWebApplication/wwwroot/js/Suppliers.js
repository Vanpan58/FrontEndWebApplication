document.addEventListener('DOMContentLoaded', function () {
    loadSuppliers();
});

function loadSuppliers() {
    fetch('/Suppliers/GetAllSuppliers') 
        .then(response => response.json())
        .then(data => {
            initializeDataTable(data.data);
        })
        .catch(error => console.error('Error:', error));
}


function initializeDataTable(Suppliers) {
    let table = document.getElementById('SuppliersTable');
    if (!table) {
        table = document.createElement('table');
        table.id = 'SuppliersTable';
        table.className = 'display'; // Clase necesaria para DataTables
        document.getElementById('SuppliersContainer').appendChild(table);
    }

    $(table).DataTable({
        responsive: true,
        data: Suppliers,
        columns: [
            { title: "ID", data: "Id", className: "column-id" },
            { title: "Nombre Compañia", data: "CompanyName", className: "column-name" },
            { title: "Nombre Contacto", data: "ContactName ", className: "column-name" },
            { title: "Titulo contacto", data: "ContactTitle", className: "column-name" },
            { title: "Ciudad", data: "city", className: "column-city" },
            { title: "Pais", data: "country", className: "column-country" },
            { title: "Telefono", data: "phone", className: "column-phone" },
            { title: "Correo", data: "Email", className: "column-email" },
            {
                title: "Acciones",
                data: "id",
                render: function (data) {
                    return `<div class="text-center">
                                <a href="/Suppliers/Detail/${data}" class=""><i class="fa fa-eye"></i></a>
                                <a href="/Suppliers/Edit/${data}" class=""><i class="fa fa-edit"></i></a>
                                <a onclick="Delete('/Suppliers/Delete/${data}')" class=""><i class="fa fa-trash"></i></a>
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
                        $('#SuppliersTable').DataTable().clear().destroy();
                        loadCustomers();
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