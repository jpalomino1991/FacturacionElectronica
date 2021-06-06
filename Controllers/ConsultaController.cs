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
    public class ConsultaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ConsultaController> _logger;

        public ConsultaController(ApplicationDbContext context, ILogger<ConsultaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        public ActionResult AccesoAnonimo()
        {
            return View();
        }

        public async Task<ActionResult> DownloadXML(string codigo)
        {
            List<taComprobanteArchivo> archivo = await _context.TaComprobanteArchivo.FromSqlInterpolated($"taComprobanteArchivoLeerPorCodigoComprobante @CodigoComprobante = {codigo} ,@CodigoTipoComprobanteArchivo = 1").ToListAsync();
            if(archivo.Count > 0)
            {
                return File(archivo[0].ItemImage,"application/xml",archivo[0].NombreArchivo);
            }
            return Ok();
        }

        public async Task<ActionResult> DownloadPDF(string codigo)
        {
            List<taComprobanteArchivo> archivo = await _context.TaComprobanteArchivo.FromSqlInterpolated($"taComprobanteArchivoLeerPorCodigoComprobante @CodigoComprobante = {codigo} ,@CodigoTipoComprobanteArchivo = 2").ToListAsync();
            if (archivo.Count > 0)
            {
                return File(archivo[0].ItemImage, "application/xml", archivo[0].NombreArchivo);
            }
            return Ok();
        }
    }
}
