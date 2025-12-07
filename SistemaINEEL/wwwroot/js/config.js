$(document).ready(function () {
    cargarUsuarios();
});

// Función para cargar usuarios
function cargarUsuarios() {
    $.ajax({
        url: '/Sistema/GetUsuarios',
        type: 'GET',
        dataType: 'json',
        success: function (usuarios) {
            var tablaBody = $('#tablaUsuariosBody');
            tablaBody.empty();
            if (usuarios.length === 0) {
                tablaBody.append('<tr><td colspan="5" style="text-align:center">No hay datos disponibles</td></tr>');
            } else {
                $.each(usuarios, function (index, usuario) {
                    var nombreRol = obtenerNombreRol(usuario.idRol);
                    var fila = '<tr>' +
                        '<td>' + usuario.usuarioID + '</td>' +
                        '<td>' + usuario.numEmpleado + '</td>' +
                        '<td>' + usuario.nombreUsuario + '</td>' +
                        '<td>' + nombreRol + '</td>' +
                        '<td>' +
                        '<button class="btn-edit" onclick="abrirModalEditar(' + usuario.usuarioID + ', ' + usuario.numEmpleado + ', \'' + usuario.nombreUsuario + '\', \'' + usuario.password + '\', ' + usuario.idRol + ')">Editar</button> ' +
                        '<button class="btn-delete" onclick="eliminarUsuario(' + usuario.usuarioID + ')">Eliminar</button>' +
                        '</td>' +
                        '</tr>';
                    tablaBody.append(fila);
                });
            }
        },
        error: function () {
            alert('Error al cargar los usuarios.');
        }
    });
}

// Función auxiliar para obtener el nombre del rol
function obtenerNombreRol(idRol) {
    switch (idRol) {
        case 1: return 'Admin';
        case 2: return 'Usuario';
        default: return 'Desconocido';
    }
}

// Función para crear usuario
function crearUsuario(num, user, pass, rol) {
    $.ajax({
        url: '/Sistema/CrearUsuarios',
        type: 'POST',
        data: {
            Num: num,
            User: user,
            Pass: pass,
            Rol: rol
        },
        success: function () {
            alert('Usuario creado exitosamente');
            $('#formCrearUsuario')[0].reset();
            cargarUsuarios();
        },
        error: function () {
            alert('Error al crear el usuario.');
        }
    });
}

// Función para eliminar usuario
function eliminarUsuario(usuarioId) {
    if (confirm('¿Está seguro de que desea eliminar este usuario?')) {
        $.ajax({
            url: '/Sistema/EliminarUsuario',
            type: 'DELETE',
            data: { usuarioId: usuarioId },
            success: function () {
                alert('Usuario eliminado exitosamente');
                cargarUsuarios();
            },
            error: function () {
                alert('Error al eliminar el usuario.');
            }
        });
    }
}

// Función para editar usuario
function editarUsuario(usuarioId, num, user, pass, rol) {
    $.ajax({
        url: '/Sistema/EditarUsuario',
        type: 'PUT',
        data: {
            usuarioId: usuarioId,
            Num: num,
            User: user,
            Pass: pass,
            Rol: rol
        },
        success: function () {
            alert('Usuario actualizado exitosamente');
            cerrarModal();
            cargarUsuarios();
        },
        error: function () {
            alert('Error al actualizar el usuario.');
        }
    });
}

// Función para editar gerencia
function editarGerencia(sistemaId, nuevaGerencia) {
    $.ajax({
        url: '/Sistema/EditarGerencia',
        type: 'PUT',
        data: {
            sistemaId: sistemaId,
            nuevaGerencia: nuevaGerencia
        },
        success: function (resp) {
            alert('Gerencia actualizada exitosamente');
            $('#formEditarGerencia')[0].reset();
            if (resp && resp.gerenciaActual) {
                $('#gerenciaActualLabel').text(resp.gerenciaActual);
            }
        },
        error: function () {
            alert('Error al actualizar la gerencia.');
        }
    });
}

// Función para abrir modal de edición
function abrirModalEditar(usuarioId, numEmpleado, nombreUsuario, password, idRol) {
    $('#editUsuarioId').val(usuarioId);
    $('#editNumEmpleado').val(numEmpleado);
    $('#editNombreUsuario').val(nombreUsuario);
    $('#editPassword').val(password);
    $('#editRol').val(idRol);
    $('#modalEditarUsuario').css('display', 'block');
}

// Función para cerrar modal
function cerrarModal() {
    $('#modalEditarUsuario').css('display', 'none');
    $('#formEditarUsuario')[0].reset();
}

// Document ready
$(document).ready(function () {
    // Botón para cargar usuarios
    $('#btnCargarUsuarios').click(function () {
        cargarUsuarios();
    });

    // Submit form crear usuario
    $('#formCrearUsuario').submit(function (e) {
        e.preventDefault();
        var num = $('#numEmpleado').val();
        var user = $('#nombreUsuario').val();
        var pass = $('#password').val();
        var rol = $('#rol').val();
        crearUsuario(num, user, pass, rol);
    });

    // Submit form editar usuario
    $('#formEditarUsuario').submit(function (e) {
        e.preventDefault();
        var usuarioId = $('#editUsuarioId').val();
        var num = $('#editNumEmpleado').val();
        var user = $('#editNombreUsuario').val();
        var pass = $('#editPassword').val();
        var rol = $('#editRol').val();
        editarUsuario(usuarioId, num, user, pass, rol);
    });

    // Submit form editar gerencia
    $('#formEditarGerencia').submit(function (e) {
        e.preventDefault();
        var sistemaId = $('#sistemaId').val();
        var nuevaGerencia = $('#nuevaGerencia').val();
        editarGerencia(sistemaId, nuevaGerencia);
    });

    // Cerrar modal al hacer clic en la X
    $('.close').click(function () {
        cerrarModal();
    });

    // Cerrar modal al hacer clic fuera de él
    $(window).click(function (event) {
        if (event.target.id === 'modalEditarUsuario') {
            cerrarModal();
        }
    });
});
