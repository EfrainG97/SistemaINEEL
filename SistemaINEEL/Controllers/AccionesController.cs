
using Microsoft.AspNetCore.Mvc;
using SistemaINEEL.Data;
using SistemaINEEL.Filters;

namespace SistemaINEEL.Controllers
{
    [SessionFilter]
    public class AccionesController : Controller
    {
        private readonly AppDBContext _context;

        public AccionesController(AppDBContext context)
        {
            _context = context;
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
