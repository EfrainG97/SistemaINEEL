using Microsoft.AspNetCore.Mvc;
using SistemaINEEL.Filters;
using ServiciosAPI.Interfaces;
using LibreriaModelos;

namespace SistemaINEEL.Controllers
{
    [SessionFilter]
    public class AccionesController : Controller
    {
        private readonly IConsecutivoService _consecutivoService;
        private readonly IReporteService _reporteService;
        private readonly ISistemaService _sistemaService;

        public AccionesController(IConsecutivoService consecutivoService, IReporteService reporteService, ISistemaService sistemaService)
        {
            _consecutivoService = consecutivoService;
            _reporteService = reporteService;
            _sistemaService = sistemaService;
        }

        public async Task<IActionResult> Consultar()
        {
            try
            {
                var consecutivos = await _consecutivoService.GetConsecutivosAsync();
                
                // Obtener el rol del usuario de la sesión
                var nombreRol = HttpContext.Session.GetString("NombreRol")?.Trim().ToLowerInvariant() ?? "usuario";
                ViewData["EsAdmin"] = nombreRol == "admin";
                
                return View(consecutivos);
            }
            catch (Exception ex)
            {
                ViewData["EsAdmin"] = false;
                return View(new List<Consecutivo>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                // Verificar que el usuario sea admin
                var nombreRol = HttpContext.Session.GetString("NombreRol")?.Trim().ToLowerInvariant() ?? "usuario";
                if (nombreRol != "admin")
                {
                    return Json(new { success = false, message = "No tiene permisos para eliminar consecutivos" });
                }

                await _consecutivoService.DeleteConsecutivoAsync(id);
                return Json(new { success = true, message = "Consecutivo eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al eliminar el consecutivo: " + ex.Message });
            }
        }

        public async Task<IActionResult> Crear()
        {
            try
            {
                int sistemaId = 1;
                var sistema = await _sistemaService.GetSistemaByIdAsync(sistemaId);
                ViewData["GerenciaActual"] = sistema?.Gerencia ?? "XX";
            }
            catch (Exception ex)
            {
                ViewData["GerenciaActual"] = "XX";
            }
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(string Remitente, string Destinatario, string Asunto, string Fecha)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Remitente) || 
                    string.IsNullOrWhiteSpace(Destinatario) || 
                    string.IsNullOrWhiteSpace(Asunto))
                {
                    return Json(new { success = false, message = "Todos los campos son requeridos" });
                }

                if (string.IsNullOrWhiteSpace(Fecha) || !DateOnly.TryParse(Fecha, out DateOnly fechaConvertida))
                {
                    return Json(new { success = false, message = "La fecha es requerida y debe tener un formato válido" });
                }
                int sistemaId = 1;
                var sistema = await _sistemaService.GetSistemaByIdAsync(sistemaId);
                var gerencia = sistema?.Gerencia ?? "XX";
                var año = DateTime.Now.Year;
                // Usar GetAllConsecutivosAsync para incluir también los eliminados lógicamente
                // y así evitar que se repitan los IDs
                var consecutivos = await _consecutivoService.GetAllConsecutivosAsync();
                
                int siguienteId = 1;
                var idsExistentes = new HashSet<int>();
                
                foreach (var consecutivo in consecutivos)
                {
                    if (!string.IsNullOrEmpty(consecutivo.FolioCompleto))
                    {
                        var partes = consecutivo.FolioCompleto.Split('/');
                        if (partes.Length >= 2 && int.TryParse(partes[1], out int idFolio))
                        {
                            idsExistentes.Add(idFolio);
                        }
                    }
                }
                
                while (idsExistentes.Contains(siguienteId))
                {
                    siguienteId++;
                }
                
                string idFormateado = siguienteId.ToString("D3");
                
                string folioCompleto = $"{gerencia}/{idFormateado}/{año}";
                
                var usuarioIdStr = HttpContext.Session.GetString("UsuarioID");
                if (string.IsNullOrWhiteSpace(usuarioIdStr) || !int.TryParse(usuarioIdStr, out int usuarioId))
                {
                    return Json(new { success = false, message = "No se pudo obtener el ID del usuario de la sesión" });
                }
                
                var nuevoConsecutivo = new Consecutivo
                {
                    FolioCompleto = folioCompleto,
                    Remitente = Remitente,
                    Destinatario = Destinatario,
                    Asunto = Asunto,
                    Fecha = fechaConvertida,
                    UsuarioID = usuarioId
                };
                
                await _consecutivoService.PostConsecutivoAsync(nuevoConsecutivo);
                
                return Json(new { 
                    success = true, 
                    folioCompleto = folioCompleto,
                    idGenerado = idFormateado
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al guardar el consecutivo: " + ex.Message });
            }
        }
    }
}
