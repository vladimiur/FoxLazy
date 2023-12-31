﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoLogin.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ProyectoLogin.Controllers
{
    //solo dejara acceder a personas loguiadas personas con acceso
    [Authorize]
    public class HomeController : Controller
    {
        private readonly LacyCompanyContext ContextoDatos;
        public HomeController(LacyCompanyContext xContextoDatos)
        {
            ContextoDatos = xContextoDatos;
        }

        public IActionResult Index()
        {
            //guaramos el nombre del usuario que este loguiado
            ClaimsPrincipal claimuser = HttpContext.User;
            string nombreUsuario = "";

            if (claimuser.Identity.IsAuthenticated)
            {
                nombreUsuario = claimuser.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();
            }
            //va almacenar nuestro nombre de usuario
            ViewData["nombreUsuario"] = nombreUsuario;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Tienda()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Formulario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Formulario(Clientes xclientes)
        {
            if (ModelState.IsValid)
            {
                ContextoDatos.clientes.Add(xclientes);
                await ContextoDatos.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        public IActionResult Acerca()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //le ponemos task porque estamso trabjando de forma asyncronica
        public async Task<IActionResult> CerrarSesion()
        {
            //cerramos la sesion
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //nos retorna a la pagina de inicio sesion
            return RedirectToAction("IniciarSesion", "Inicio");
        }
    }
}