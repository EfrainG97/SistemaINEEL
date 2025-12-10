$(document).ready(function () {
    $('#formCrearConsecutivo').on('submit', function (e) {
        e.preventDefault();

        var formData = {
            Remitente: $('#Remitente').val(),
            Destinatario: $('#Destinatario').val(),
            Asunto: $('#Asunto').val(),
            Fecha: $('#Fecha').val()
        };

        $.ajax({
            url: window.accionesUrls.crear,
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    // Actualizar el folio con el ID generado
                    $('#folioPreview').val(response.folioCompleto);

                    // Mostrar alerta de éxito
                    alert('Consecutivo guardado con éxito.\nFolio: ' + response.folioCompleto);

                    // Limpiar el formulario
                    $('#formCrearConsecutivo')[0].reset();

                    // Resetear el folio a XX
                    var gerencia = window.gerenciaActual;
                    var año = new Date().getFullYear();
                    $('#folioPreview').val(gerencia + '/XX/' + año);
                } else {
                    alert('Error: ' + (response.message || 'No se pudo guardar el consecutivo'));
                }
            },
            error: function () {
                alert('Error al guardar el consecutivo. Por favor, intente nuevamente.');
            }
        });
    });
});
