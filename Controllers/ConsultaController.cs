using FacturacionElectronica.Data;
using FacturacionElectronica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        public async Task<ActionResult> Consulta([Bind("tipoComprobante,serie,numero,monto,fecha")] AccesoAnonimoModel anonimo)
        {
            if (ModelState.IsValid)
            {
                List<ComprobanteAnonimo> comprobante = await _context.ComprobanteAnonimo.FromSqlInterpolated($"taComprobanteUsuarioAnominoLeer @NumeroSerie = {anonimo.serie},@NumeroComprobante = {anonimo.numero},@MontoTotal = {anonimo.monto},@FechaComprobante = {anonimo.fecha}").ToListAsync();
                return PartialView("_ResultadoPartial", comprobante);
            }
            else
                return View(anonimo);
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
