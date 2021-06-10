using AutoMapper;
using EmailService;
using FacturacionElectronica.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FacturacionElectronica.Controllers
{
    public class CuentaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<User> _signInManager;
        public CuentaController(IMapper mapper, UserManager<User> userManager, IEmailSender emailSender, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registrar(UserRegistrationModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }
            var user = _mapper.Map<User>(userModel);
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(userModel);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmarCorreo), "Cuenta", new { token, email = user.Email }, Request.Scheme);
            var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink);
            await _emailSender.SendEmailAsync(message);

            await _userManager.AddToRoleAsync(user, "Administrador");
            return RedirectToAction(nameof(RegistroExitoso));
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmarCorreo(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View("Error");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? nameof(ConfirmarCorreo) : "Error");
        }

        [HttpGet]
        public IActionResult RegistroExitoso()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel userModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            var result = await _signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Correo o usuario inválido");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(CuentaController.Login), "Cuenta");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(ConsultaController.Index), "Consulta");
        }

        [HttpGet]
        public IActionResult OlvidoContrasena()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OlvidoContrasena(ForgotPasswordModel forgotPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(forgotPasswordModel);

            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
                return RedirectToAction(nameof(OlvidoContrasenaConfirmacion));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callback = Url.Action(nameof(RecuperarContrasena), "Cuenta", new { token, email = user.Email }, Request.Scheme);
            var message = new Message(new string[] { user.Email }, "Recuperar Contraseña", callback);
            await _emailSender.SendEmailAsync(message);

            return RedirectToAction(nameof(OlvidoContrasenaConfirmacion));
        }
        public IActionResult OlvidoContrasenaConfirmacion()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RecuperarContrasena(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecuperarContrasena(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);

            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);

            if (user == null)
                RedirectToAction(nameof(RecuperarContrasenaConfirmacion));

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);

            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }

            return RedirectToAction(nameof(RecuperarContrasenaConfirmacion));
        }

        [HttpGet]
        public IActionResult RecuperarContrasenaConfirmacion()
        {
            return View();
        }
    }
}
