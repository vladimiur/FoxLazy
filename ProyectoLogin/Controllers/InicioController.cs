//Sirven como referencia de servicios
using ProyectoLogin.Models;
using ProyectoLogin.Recursos;
using ProyectoLogin.Servicios.Contrato;
//este no
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Mvc;

namespace ProyectoLogin.Controllers
{
    public class InicioController : Controller
    {
        private readonly IUsuarioService _usuarioServicio;

        public InicioController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }


        //Devuelve la vista
        public IActionResult Registrarse()
        {
            return View();
        }
        //Resive la solicitu de la vista 
        [HttpPost]
        public async Task<IActionResult>Registrarse(Usuario modelo)
        {
            //nos hara una clave encriptada formato SHA256
            modelo.Clave = Utilidades.EncriptarClave(modelo.Clave);

            Usuario usuario_creado = await _usuarioServicio.SaveUsuario(modelo);

            if (usuario_creado.IdUsuario > 0)
                return RedirectToAction("IniciarSesion", "Inicio");
            //si no tenemos ningun registro crearemos un viewdata
            //creamos un viewdata sirve para compartir informacion con nuestra vista
            ViewData["Mensaje"] = "No se pudo crear el usuario";


            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {
            //resive el correo y la clave encriptada de nuestro usuario para dar acceso
            Usuario usuario_encontrado = await _usuarioServicio.GetUsuario(correo, Utilidades.EncriptarClave(clave));
            
            //Nos mandara un mensaje si el usuario no existe en nuestra base de datos o no encuentra
            if(usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }
            //si encuentra usuario usara esta lista
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario_encontrado.NombreUsuario)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            //creamos propiedades de autenticacion
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };
            //creamos las propiedades para iniciar sesion
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                ) ;

            //nos dirigira a la ruta del sistema en este caso la vista Index
            return RedirectToAction("Index", "Home");
        }
    }
}
