using Microsoft.AspNetCore.Mvc;
using SistemaINEEL.Filters;
using ServiciosAPI.Interfaces;

namespace SistemaINEEL.Controllers
{
    [SessionFilter]
    public class AccionesController : Controller
    {
        private readonly IConsecutivoService _consecutivoService;
        private readonly IReporteService _reporteService;

        public AccionesController(IConsecutivoService consecutivoService, IReporteService reporteService)
        {
            _consecutivoService = consecutivoService;
            _reporteService = reporteService;
        }

        public IActionResult Create()
        {
            return View();
        }

        //public async Task<IActionResult> CrearConsecutivo()
        //{

        //}





    }
}
