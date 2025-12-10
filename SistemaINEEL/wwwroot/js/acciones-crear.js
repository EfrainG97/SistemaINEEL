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

        $.ajax({
            url: window.accionesUrls.crear,
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert('Consecutivo guardado con exito.\nFolio: ' + response.folioCompleto);

                    window.location.href = '/Home/Index';
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
