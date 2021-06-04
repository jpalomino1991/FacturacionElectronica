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
            return View();
        }

        [HttpPost]
        public IActionResult Index(ConsultaModel consulta)
        {
            List<ComprobanteAnonimo> comprobante = _context.ComprobanteAnonimo.FromSqlInterpolated($"taComprobanteUsuarioAnominoLeer @NumeroSerie = {consulta.serie},@NumeroComprobante = {consulta.numero},@MontoTotal = {consulta.monto},@FechaComprobante = {consulta.fecha}").ToList();
            if(comprobante != null)
            {
                return Ok(comprobante[0]);
            }
            return Ok();
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
