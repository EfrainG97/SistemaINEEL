using Microsoft.AspNetCore.Mvc;
using SistemaINEEL.Filters;
using ServiciosAPI.Interfaces;
using LibreriaModelos;

namespace SistemaINEEL.Controllers
{
    [SessionFilter]
    [AdminFilter]
    public class SistemaController : Controller
    {
        #region Campos
        private readonly ISistemaService _sistemaService;
        private readonly IUsuarioService _usuarioService;
        #endregion

        #region Constructor
        public SistemaController(ISistemaService sistemaService, IUsuarioService usuarioService)
        {
            _sistemaService = sistemaService;
            _usuarioService = usuarioService;
        }
        #endregion

        #region Métodos GET
        public async Task<IActionResult> Config()
        {
            int sistemaId = 1;
            var sistema = await _sistemaService.GetSistemaByIdAsync(sistemaId);
            ViewData["GerenciaActual"] = sistema?.Gerencia ?? "No definida";
            ViewData["SistemaId"] = sistemaId;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                var usuarios = await _usuarioService.GetUsuariosAsync();
                return Json(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al obtener los usuarios: " + ex.Message });
            }
        }
        #endregion

        #region Métodos POST
        [HttpPost]
        public async Task<IActionResult> CrearUsuarios(int Num, string User, string Pass, int Rol)
        {
            try
            {
                var nuevoUsuario = new Usuario
                {
                    NumEmpleado = Num,
                    NombreUsuario = User,
                    Password = Pass,
                    IDRol = Rol
                };

                await _usuarioService.PostUsuarioAsync(nuevoUsuario);
                return RedirectToAction("Config");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al crear el usuario: " + ex.Message;
                return RedirectToAction("Config");
            }
        }
        #endregion

        #region Métodos PUT
        [HttpPut]
        public async Task<IActionResult> EditarGerencia(int sistemaId, string nuevaGerencia)
        {
            try
            {
                var sistema = await _sistemaService.GetSistemaByIdAsync(sistemaId);
                if (sistema == null)
                {
                    return NotFound();
                }
                sistema.Gerencia = nuevaGerencia;
                await _sistemaService.PutSistemaAsync(sistema);
                return Json(new { gerenciaActual = sistema.Gerencia });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al actualizar la gerencia: " + ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(int usuarioId, int Num, string User, string Pass, int Rol)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(usuarioId);
                if (usuario == null)
                {
                    return NotFound();
                }
                usuario.NumEmpleado = Num;
                usuario.NombreUsuario = User;
                usuario.Password = Pass;
                usuario.IDRol = Rol;
                await _usuarioService.PutUsuarioAsync(usuario);
                return RedirectToAction("Config");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al actualizar el usuario: " + ex.Message;
                return RedirectToAction("Config");
            }
        }
        #endregion

        #region Métodos DELETE
        [HttpDelete]
        public async Task<IActionResult> EliminarUsuario(int usuarioId)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(usuarioId);
                if (usuario == null)
                {
                    return NotFound();
                }
                await _usuarioService.DeleteUsuarioAsync(usuarioId);
                return RedirectToAction("Config");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar el usuario: " + ex.Message;
                return RedirectToAction("Config");
            }
        }
        #endregion
    }
}
