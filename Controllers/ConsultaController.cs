using FacturacionElectronica.Data;
using FacturacionElectronica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FacturacionElectronica.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ConsultaController> _logger;
        private UserManager<User> _userManager;

        public ConsultaController(ApplicationDbContext context, ILogger<ConsultaController> logger, UserManager<User> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var numeroDocumento = user.NumeroDocumento;

            List<ComprobanteAnonimo> comprobantes = await _context.ComprobanteAnonimo.FromSqlInterpolated($"taComprobanteUsuarioLeer @PorNumeroDocumento = 1,@NumeroDocumento = {numeroDocumento},@PorSerieNumero = 0,@NumeroSerie = '',@NumeroComprobante = 0,@PorFechaComprobante = 0,@FechaComprobanteA = '',@FechaComprobanteB = ''").ToListAsync();

            return View(comprobantes);
        }

        public ActionResult Resultado(ComprobanteAnonimo comprobante)
        {
            if (ModelState.IsValid)
            {
                return View(comprobante);
            }
            else
                return View();
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
            AccesoAnonimoModel anonimo = new AccesoAnonimoModel();
            return View(anonimo);
        }

        [HttpPost]
        public async Task<ActionResult> AccesoAnonimo(AccesoAnonimoModel anonimo)
        {
            if (ModelState.IsValid)
            {

                List<ComprobanteAnonimo> comprobante = await _context.ComprobanteAnonimo.FromSqlInterpolated($"taComprobanteUsuarioAnominoLeer @NumeroSerie = {anonimo.serie},@NumeroComprobante = {anonimo.numero},@MontoTotal = {anonimo.monto},@FechaComprobante = {DateTime.Parse(anonimo.fecha)}").ToListAsync();
                if (comprobante.Count > 0)
                {
                    return RedirectToAction(nameof(ConsultaController.Resultado), "Consulta", new RouteValueDictionary(comprobante[0]));
                }
                else
                {
                    anonimo.error = "No hay resultados";
                    return View(anonimo);
                }
            }
            else
                return View(anonimo);
        }

        public async Task<ActionResult> DownloadXML(string codigo)
        {
            List<taComprobanteArchivo> archivo = await _context.TaComprobanteArchivo.FromSqlInterpolated($"taComprobanteArchivoLeerPorCodigoComprobante @CodigoComprobante = {codigo} ,@CodigoTipoComprobanteArchivo = 1").ToListAsync();
            if (archivo.Count > 0)
            {
                return File(archivo[0].ItemImage, "application/xml", archivo[0].NombreArchivo);
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
