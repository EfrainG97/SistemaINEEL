$(document).ready(function () {
    var hoy = new Date().toISOString().split('T')[0];
    $('#Fecha').val(hoy);

    $('#formCrearConsecutivo').on('submit', function (e) {
        e.preventDefault();

        var formData = {
            Remitente: $('#Remitente').val(),
            Destinatario: $('#Destinatario').val(),
            Asunto: $('#Asunto').val(),
            Fecha: $('#Fecha').val()
        };

        // Deshabilitar botón mientras procesa
        var btnSubmit = $(this).find('button[type="submit"]');
        btnSubmit.prop('disabled', true).html('<span class="spinner-border spinner-border-sm me-1"></span>Guardando...');

        $.ajax({
            url: window.accionesUrls.crear,
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    Swal.fire({
                        icon: 'success',
                        title: '¡Consecutivo Creado!',
                        html: '<p>El consecutivo se ha guardado correctamente.</p><p class="mt-2"><strong>Folio:</strong> <span class="text-primary fw-bold">' + response.folioCompleto + '</span></p>',
                        confirmButtonColor: '#4A7C59',
                        confirmButtonText: 'Aceptar'
                    }).then(function() {
                        window.location.href = '/Home/Index';
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: response.message || 'No se pudo guardar el consecutivo',
                        confirmButtonColor: '#1E5AA8'
                    });
                    btnSubmit.prop('disabled', false).html('<i class="fas fa-save me-1"></i> Guardar Consecutivo');
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Error de conexión',
                    text: 'Error al guardar el consecutivo. Por favor, intente nuevamente.',
                    confirmButtonColor: '#1E5AA8'
                });
                btnSubmit.prop('disabled', false).html('<i class="fas fa-save me-1"></i> Guardar Consecutivo');
            }
        });
    });
});
