using Microsoft.AspNetCore.Mvc;
using SistemaINEEL.Filters;
using ServiciosAPI.Interfaces;
using LibreriaModelos;
using System.Collections.Generic;

namespace SistemaINEEL.Controllers
{
    [SessionFilter]
    public class AccionesController : Controller
    {
        private readonly IConsecutivoService _consecutivoService;
        private readonly IReporteService _reporteService;
        private readonly ISistemaService _sistemaService;
        private readonly IUsuarioService _usuarioService;

        public AccionesController(IConsecutivoService consecutivoService, IReporteService reporteService, ISistemaService sistemaService, IUsuarioService usuarioService)
        {
            _consecutivoService = consecutivoService;
            _reporteService = reporteService;
            _sistemaService = sistemaService;
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Consultar()
        {
            try
            {
                var consecutivos = await _consecutivoService.GetConsecutivosAsync();
                
                var nombreRol = HttpContext.Session.GetString("NombreRol")?.Trim().ToLowerInvariant() ?? "usuario";
                ViewData["EsAdmin"] = nombreRol == "admin";
                
                var usuarioIdStr = HttpContext.Session.GetString("UsuarioID");
                int? usuarioIdActual = null;
                if (!string.IsNullOrWhiteSpace(usuarioIdStr) && int.TryParse(usuarioIdStr, out int userId))
                {
                    usuarioIdActual = userId;
                }
                ViewData["UsuarioIDActual"] = usuarioIdActual;
                
                var nombresUsuarios = new Dictionary<int, string>();
                try
                {
                    var usuarios = await _usuarioService.GetUsuariosAsync();
                    foreach (var usuario in usuarios)
                    {
                        nombresUsuarios[usuario.UsuarioID] = usuario.NombreUsuario ?? $"Usuario {usuario.UsuarioID}";
                    }
                }
                catch
                {
                }
                
                ViewData["NombresUsuarios"] = nombresUsuarios;
                
                return View(consecutivos);
            }
            catch
            {
                ViewData["EsAdmin"] = false;
                ViewData["UsuarioIDActual"] = null;
                ViewData["NombresUsuarios"] = new Dictionary<int, string>();
                return View(new List<Consecutivo>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
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

        [HttpPost]
        public async Task<IActionResult> Cancelar(int id, string motivo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(motivo))
                {
                    return Json(new { success = false, message = "El motivo de cancelacion es requerido" });
                }

                if (motivo.Length > 500)
                {
                    return Json(new { success = false, message = "El motivo de cancelacion no puede exceder 500 caracteres" });
                }

                var consecutivo = await _consecutivoService.GetConsecutivoByIdAsync(id);
                if (consecutivo == null)
                {
                    return Json(new { success = false, message = "Consecutivo no encontrado" });
                }

                if (consecutivo.CanceladoPor > 0)
                {
                    return Json(new { success = false, message = "Este consecutivo ya esta cancelado" });
                }

                var usuarioIdStr = HttpContext.Session.GetString("UsuarioID");
                if (string.IsNullOrWhiteSpace(usuarioIdStr) || !int.TryParse(usuarioIdStr, out int usuarioIdActual))
                {
                    return Json(new { success = false, message = "No se pudo obtener el ID del usuario de la sesion" });
                }

                var nombreRol = HttpContext.Session.GetString("NombreRol")?.Trim().ToLowerInvariant() ?? "usuario";
                bool esAdmin = nombreRol == "admin";
                
                if (!esAdmin && consecutivo.UsuarioID != usuarioIdActual)
                {
                    return Json(new { success = false, message = "Solo puede cancelar los consecutivos que usted creo" });
                }

                consecutivo.CanceladoPor = usuarioIdActual;
                consecutivo.MotivoCan = motivo.Trim();

                await _consecutivoService.PutConsecutivoAsync(consecutivo);
                
                return Json(new { success = true, message = "Consecutivo cancelado exitosamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al cancelar el consecutivo: " + ex.Message });
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
            catch
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
                    return Json(new { success = false, message = "La fecha es requerida y debe tener un formato valido" });
                }
                int sistemaId = 1;
                var sistema = await _sistemaService.GetSistemaByIdAsync(sistemaId);
                var gerencia = sistema?.Gerencia ?? "XX";
                var anio = DateTime.Now.Year;

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
                
                string folioCompleto = $"{gerencia}/{idFormateado}/{anio}";
                
                var usuarioIdStr = HttpContext.Session.GetString("UsuarioID");
                if (string.IsNullOrWhiteSpace(usuarioIdStr) || !int.TryParse(usuarioIdStr, out int usuarioId))
                {
                    return Json(new { success = false, message = "No se pudo obtener el ID del usuario de la sesion" });
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
