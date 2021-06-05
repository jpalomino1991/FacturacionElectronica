using FacturacionElectronica.Data;
using FacturacionElectronica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionElectronica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            dynamic data = new System.Dynamic.ExpandoObject();
            data.comprobante = null;
            return View(data);
        }

        public ActionResult Consulta(int tipoComprobante, int serie, int numero, decimal monto, DateTime fecha)
        {
            List<ComprobanteAnonimo> comprobante = _context.ComprobanteAnonimo.FromSqlInterpolated($"taComprobanteUsuarioAnominoLeer @NumeroSerie = {serie},@NumeroComprobante = {numero},@MontoTotal = {monto},@FechaComprobante = {fecha}").ToList();
            return PartialView("_ResultadoPartial",comprobante);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
