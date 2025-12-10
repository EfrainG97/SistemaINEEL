$(document).ready(function () {
    var consecutivoIdCancelar = null;
    var consecutivoIdEliminar = null;
    
    // Inicializar tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
    
    // Inicializar modales
    var modalVerMotivo = new bootstrap.Modal(document.getElementById('modalVerMotivo'));
    var modalCancelar = new bootstrap.Modal(document.getElementById('modalCancelar'));
    var modalEliminar = new bootstrap.Modal(document.getElementById('modalEliminar'));
    
    // ========== FUNCION DE FILTRADO DINAMICO =========="
    
    function filtrarTabla() {
        var filtroFolio = $('#filtroFolio').val().toLowerCase().trim();
        var filtroAsunto = $('#filtroAsunto').val().toLowerCase().trim();
        var fechaDesde = $('#fechaDesde').val();
        var fechaHasta = $('#fechaHasta').val();
        var filtroUsuario = $('#filtroUsuario').val().toLowerCase();
        var filtroEstado = $('#filtroEstado').val();
        
        var filasVisibles = 0;
        var totalFilas = $('.fila-reporte').length;
        
        $('.fila-reporte').each(function () {
            var fila = $(this);
            var folio = fila.data('folio') || '';
            var asunto = fila.data('asunto') || '';
            var fecha = fila.data('fecha') || '';
            var creador = (fila.data('creador') || '').toString().toLowerCase();
            var estado = fila.data('estado') || '';
            
            var cumpleFolio = !filtroFolio || folio.indexOf(filtroFolio) !== -1;
            var cumpleAsunto = !filtroAsunto || asunto.indexOf(filtroAsunto) !== -1;
            var cumpleFechaDesde = !fechaDesde || fecha >= fechaDesde;
            var cumpleFechaHasta = !fechaHasta || fecha <= fechaHasta;
            var cumpleUsuario = !filtroUsuario || creador.indexOf(filtroUsuario) !== -1;
            var cumpleEstado = !filtroEstado || estado === filtroEstado;
            
            if (cumpleFolio && cumpleAsunto && cumpleFechaDesde && cumpleFechaHasta && cumpleUsuario && cumpleEstado) {
                fila.show();
                filasVisibles++;
            } else {
                fila.hide();
            }
        });
        
        // Actualizar contador de resultados
        var hayFiltrosActivos = filtroFolio || filtroAsunto || fechaDesde || fechaHasta || filtroUsuario || filtroEstado;
        if (hayFiltrosActivos) {
            $('#contadorResultados').html('<i class="bi bi-funnel-fill me-1"></i>Mostrando ' + filasVisibles + ' de ' + totalFilas + ' registros');
        } else {
            $('#contadorResultados').text('');
        }
        
        // Mostrar mensaje si no hay resultados
        if (filasVisibles === 0 && totalFilas > 0) {
            $('#sinResultadosReporte').removeClass('d-none');
            $('#tablaReportes').addClass('d-none');
        } else {
            $('#sinResultadosReporte').addClass('d-none');
            $('#tablaReportes').removeClass('d-none');
        }
    }
    
    // ========== EVENTOS DE FILTRADO DINAMICO =========="
    
    // Filtrado al escribir (con debounce para mejor rendimiento)
    var timeoutId;
    $('#filtroFolio, #filtroAsunto').on('keyup', function () {
        clearTimeout(timeoutId);
        timeoutId = setTimeout(function() {
            filtrarTabla();
        }, 200);
    });
    
    // Filtrado al cambiar selects o fechas
    $('#fechaDesde, #fechaHasta, #filtroUsuario, #filtroEstado').on('change', function () {
        filtrarTabla();
    });
    
    // Limpiar filtros
    $('#btnLimpiarFiltros').on('click', function () {
        $('#filtroFolio').val('');
        $('#filtroAsunto').val('');
        $('#fechaDesde').val('');
        $('#fechaHasta').val('');
        $('#filtroUsuario').val('');
        $('#filtroEstado').val('');
        $('.fila-reporte').show();
        $('#contadorResultados').text('');
        $('#sinResultadosReporte').addClass('d-none');
        $('#tablaReportes').removeClass('d-none');
    });
    
    // Exportar a Excel
    $('#btnExportar').on('click', function (e) {
        // El enlace ya tiene el href correcto, simplemente permitir la navegacion
        // No se necesita prevenir el comportamiento por defecto
    });
    
    // ========== VER MOTIVO DE CANCELACION =========="
    
    $(document).on('click', '.btn-ver-motivo', function () {
        var folio = $(this).data('folio');
        var motivo = $(this).data('motivo');
        var canceladoPor = $(this).data('canceladopor');
        
        $('#folioVerMotivo').text(folio);
        $('#canceladoPorVerMotivo').text(canceladoPor);
        $('#motivoVerMotivo').text(motivo || 'No se especifico motivo');
        
        modalVerMotivo.show();
    });
    
    // ========== CANCELAR CONSECUTIVO =========="
    
    $(document).on('click', '.btn-cancelar-consecutivo', function () {
        consecutivoIdCancelar = $(this).data('id');
        var folio = $(this).data('folio');
        
        $('#folioCancelar').text(folio);
        $('#motivoCancelacion').val('');
        $('#contadorCaracteres').text('0');
        
        modalCancelar.show();
    });
    
    $('#motivoCancelacion').on('input', function () {
        var longitud = $(this).val().length;
        $('#contadorCaracteres').text(longitud);
    });
    
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

        var btn = $(this);
        btn.prop('disabled', true).html('<span class="spinner-border spinner-border-sm me-1"></span>Procesando...');

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
                    mostrarExito('Consecutivo cancelado exitosamente');
                    setTimeout(function() {
                        location.reload();
                    }, 1500);
                } else {
                    alert('Error: ' + (response.message || 'No se pudo cancelar el consecutivo'));
                    btn.prop('disabled', false).html('<i class="bi bi-x-octagon me-1"></i>Confirmar Cancelacion');
                }
            },
            error: function () {
                alert('Error al cancelar el consecutivo. Por favor, intente nuevamente.');
                btn.prop('disabled', false).html('<i class="bi bi-x-octagon me-1"></i>Confirmar Cancelacion');
            }
        });
    });
    
    $('#modalCancelar').on('hidden.bs.modal', function () {
        $('#motivoCancelacion').val('');
        $('#contadorCaracteres').text('0');
        consecutivoIdCancelar = null;
        $('#btnConfirmarCancelar').prop('disabled', false).html('<i class="bi bi-x-octagon me-1"></i>Confirmar Cancelacion');
    });
    
    // ========== ELIMINAR CONSECUTIVO =========="
    
    $(document).on('click', '.btn-eliminar-consecutivo', function () {
        consecutivoIdEliminar = $(this).data('id');
        var folio = $(this).data('folio');
        
        $('#folioEliminar').text(folio);
        
        modalEliminar.show();
    });
    
    $('#btnConfirmarEliminar').on('click', function () {
        var btn = $(this);
        btn.prop('disabled', true).html('<span class="spinner-border spinner-border-sm me-1"></span>Eliminando...');

        $.ajax({
            url: window.accionesUrls.eliminar,
            type: 'POST',
            data: { id: consecutivoIdEliminar },
            success: function (response) {
                if (response.success) {
                    modalEliminar.hide();
                    mostrarExito('Consecutivo eliminado exitosamente');
                    setTimeout(function() {
                        location.reload();
                    }, 1500);
                } else {
                    alert('Error: ' + (response.message || 'No se pudo eliminar el consecutivo'));
                    btn.prop('disabled', false).html('<i class="bi bi-trash me-1"></i>Si, eliminar');
                }
            },
            error: function () {
                alert('Error al eliminar el consecutivo. Por favor, intente nuevamente.');
                btn.prop('disabled', false).html('<i class="bi bi-trash me-1"></i>Si, eliminar');
            }
        });
    });
    
    $('#modalEliminar').on('hidden.bs.modal', function () {
        consecutivoIdEliminar = null;
        $('#btnConfirmarEliminar').prop('disabled', false).html('<i class="bi bi-trash me-1"></i>Si, eliminar');
    });
    
    // ========== FUNCION PARA MOSTRAR EXITO =========="
    
    function mostrarExito(mensaje) {
        var toastHtml = '<div class="position-fixed top-0 end-0 p-3" style="z-index: 9999">' +
            '<div class="toast align-items-center text-white bg-success border-0 show" role="alert">' +
            '<div class="d-flex">' +
            '<div class="toast-body">' +
            '<i class="bi bi-check-circle-fill me-2"></i>' + mensaje +
            '</div>' +
            '<button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>' +
            '</div>' +
            '</div>' +
            '</div>';
        
        $('body').append(toastHtml);
    }
});
