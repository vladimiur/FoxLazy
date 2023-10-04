using Microsoft.EntityFrameworkCore;
using ProyectoLogin.Models;

namespace ProyectoLogin.Servicios.Contrato
{
    public interface IUsuarioService
    {
        //Nos devuelve el usuario
        Task<Usuario> GetUsuario(string correo, string clave);
        //Este resive solo el modelo de usuario
        //este guarda el usuario
        Task<Usuario> SaveUsuario(Usuario modelo);
    }
}
