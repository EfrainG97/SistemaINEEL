
using Microsoft.AspNetCore.Mvc;
using Proyecto_Ord.Data;
using Proyecto_Ord.Filters;

namespace Proyecto_Ord.Controllers
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
