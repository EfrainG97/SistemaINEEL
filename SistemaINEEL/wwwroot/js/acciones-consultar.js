$(document).ready(function () {
    var consecutivoIdCancelar = null;
    var modalCancelar = new bootstrap.Modal(document.getElementById('modalCancelar'));

    // Manejar click en botón de cancelar
    $('.btn-cancelar').on('click', function () {
        consecutivoIdCancelar = $(this).data('id');
        var folio = $(this).data('folio');
        $('#folioCancelar').text(folio);
        $('#motivoCancelacion').val('');
        $('#contadorCaracteres').text('0');
        modalCancelar.show();
    });

    // Contador de caracteres
    $('#motivoCancelacion').on('input', function () {
        var longitud = $(this).val().length;
        $('#contadorCaracteres').text(longitud);
    });

    // Confirmar cancelación
    $('#btnConfirmarCancelar').on('click', function () {
        var motivo = $('#motivoCancelacion').val().trim();

        if (!motivo) {
            alert('Por favor, ingrese el motivo de cancelación');
            return;
        }

        if (motivo.length > 500) {
            alert('El motivo de cancelación no puede exceder 500 caracteres');
            return;
        }

        // Deshabilitar botón mientras se procesa
        $(this).prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Procesando...');

        $.ajax({
            url: window.accionesUrls.cancelar,
            type: 'POST',
            data: {
                id: consecutivoIdCancelar,
                motivo: motivo
            },
            success: function (response) {
                if (response.success) {
                    modalCancelar.hide();
                    alert('Consecutivo cancelado exitosamente');
                    location.reload();
                } else {
                    alert('Error: ' + (response.message || 'No se pudo cancelar el consecutivo'));
                    $('#btnConfirmarCancelar').prop('disabled', false).html('<i class="bi bi-x-circle"></i> Confirmar Cancelación');
                }
            },
            error: function () {
                alert('Error al cancelar el consecutivo. Por favor, intente nuevamente.');
                $('#btnConfirmarCancelar').prop('disabled', false).html('<i class="bi bi-x-circle"></i> Confirmar Cancelación');
            }
        });
    });

    // Limpiar modal al cerrar
    $('#modalCancelar').on('hidden.bs.modal', function () {
        $('#motivoCancelacion').val('');
        $('#contadorCaracteres').text('0');
        consecutivoIdCancelar = null;
        $('#btnConfirmarCancelar').prop('disabled', false).html('<i class="bi bi-x-circle"></i> Confirmar Cancelación');
    });

    // Manejar eliminación
    $('.btn-eliminar').on('click', function () {
        var consecutivoId = $(this).data('id');
        var folio = $(this).data('folio');

        if (confirm('¿Está seguro de que desea eliminar el consecutivo "' + folio + '"?\n\nEsta acción no se puede deshacer.')) {
            $.ajax({
                url: window.accionesUrls.eliminar,
                type: 'POST',
                data: { id: consecutivoId },
                success: function (response) {
                    if (response.success) {
                        alert('Consecutivo eliminado exitosamente');
                        location.reload();
                    } else {
                        alert('Error: ' + (response.message || 'No se pudo eliminar el consecutivo'));
                    }
                },
                error: function () {
                    alert('Error al eliminar el consecutivo. Por favor, intente nuevamente.');
                }
            });
        }
    });
});
