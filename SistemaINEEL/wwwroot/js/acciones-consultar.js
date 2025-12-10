$(document).ready(function () {
    var consecutivoIdCancelar = null;
    var modalCancelar = new bootstrap.Modal(document.getElementById('modalCancelar'));
    var modalVerMotivo = new bootstrap.Modal(document.getElementById('modalVerMotivo'));

    // Funcion para filtrar la tabla
    function filtrarTabla() {
        var filtroFolio = $('#filtroFolio').val().toLowerCase().trim();
        var filtroAsunto = $('#filtroAsunto').val().toLowerCase().trim();
        var filtroFecha = $('#filtroFecha').val();
        var filtroCreador = $('#filtroCreador').val().toLowerCase().trim();
        
        var filasVisibles = 0;
        var totalFilas = $('.fila-consecutivo').length;
        
        $('.fila-consecutivo').each(function () {
            var fila = $(this);
            var folio = fila.data('folio') || '';
            var asunto = fila.data('asunto') || '';
            var fecha = fila.data('fecha') || '';
            var creador = fila.data('creador') || '';
            
            var coincideFolio = filtroFolio === '' || folio.indexOf(filtroFolio) !== -1;
            var coincideAsunto = filtroAsunto === '' || asunto.indexOf(filtroAsunto) !== -1;
            var coincideFecha = filtroFecha === '' || fecha === filtroFecha;
            var coincideCreador = filtroCreador === '' || creador.indexOf(filtroCreador) !== -1;
            
            if (coincideFolio && coincideAsunto && coincideFecha && coincideCreador) {
                fila.show();
                filasVisibles++;
            } else {
                fila.hide();
            }
        });
        
        // Mostrar mensaje si no hay resultados
        if (filasVisibles === 0 && totalFilas > 0) {
            $('#sinResultados').removeClass('d-none');
            $('#tablaConsecutivos').addClass('d-none');
        } else {
            $('#sinResultados').addClass('d-none');
            $('#tablaConsecutivos').removeClass('d-none');
        }
        
        // Actualizar contador de resultados
        if (filtroFolio || filtroAsunto || filtroFecha || filtroCreador) {
            $('#contadorResultados').text('Mostrando ' + filasVisibles + ' de ' + totalFilas + ' registros');
        } else {
            $('#contadorResultados').text('');
        }
    }
    
    // Eventos de filtrado
    $('#filtroFolio, #filtroAsunto').on('keyup', function () {
        filtrarTabla();
    });
    
    $('#filtroFecha, #filtroCreador').on('change', function () {
        filtrarTabla();
    });
    
    // Limpiar filtros
    $('#btnLimpiarFiltros').on('click', function () {
        $('#filtroFolio').val('');
        $('#filtroAsunto').val('');
        $('#filtroFecha').val('');
        $('#filtroCreador').val('');
        filtrarTabla();
    });

    // Manejar click en boton de ver motivo
    $('.btn-ver-motivo').on('click', function () {
        var folio = $(this).data('folio');
        var motivo = $(this).data('motivo');
        var canceladoPor = $(this).data('canceladopor');
        
        $('#folioVerMotivo').text(folio);
        $('#canceladoPorVerMotivo').text(canceladoPor);
        $('#motivoVerMotivo').text(motivo || 'No se especifico motivo');
        
        modalVerMotivo.show();
    });

    // Manejar click en boton de cancelar
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

    // Confirmar cancelacion
    $('#btnConfirmarCancelar').on('click', function () {
        var motivo = $('#motivoCancelacion').val().trim();

        if (!motivo) {
            alert('Por favor, ingrese el motivo de cancelacion');
            return;
        }

        if (motivo.length > 500) {
            alert('El motivo de cancelacion no puede exceder 500 caracteres');
            return;
        }

        // Deshabilitar boton mientras se procesa
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
                    $('#btnConfirmarCancelar').prop('disabled', false).html('<i class="bi bi-x-circle"></i> Confirmar Cancelacion');
                }
            },
            error: function () {
                alert('Error al cancelar el consecutivo. Por favor, intente nuevamente.');
                $('#btnConfirmarCancelar').prop('disabled', false).html('<i class="bi bi-x-circle"></i> Confirmar Cancelacion');
            }
        });
    });

    // Limpiar modal al cerrar
    $('#modalCancelar').on('hidden.bs.modal', function () {
        $('#motivoCancelacion').val('');
        $('#contadorCaracteres').text('0');
        consecutivoIdCancelar = null;
        $('#btnConfirmarCancelar').prop('disabled', false).html('<i class="bi bi-x-circle"></i> Confirmar Cancelacion');
    });

    // Manejar eliminacion
    $('.btn-eliminar').on('click', function () {
        var consecutivoId = $(this).data('id');
        var folio = $(this).data('folio');

        if (confirm('Esta seguro de que desea eliminar el consecutivo "' + folio + '"?\n\nEsta accion no se puede deshacer.')) {
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
