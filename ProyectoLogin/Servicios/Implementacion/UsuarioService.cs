﻿using Microsoft.EntityFrameworkCore;
using ProyectoLogin.Models;
using ProyectoLogin.Servicios.Contrato;

namespace ProyectoLogin.Servicios.Implementacion
{
    //creamos una herencia
    public class UsuarioService : IUsuarioService
    {

        private readonly LacyCompanyContext _dbContext; 

        public UsuarioService(LacyCompanyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> GetUsuario(string correo, string clave)
        {
            Usuario usuario_encontrado = await _dbContext.Usuarios.Where(u => u.Correo == correo && u.Clave == clave).FirstOrDefaultAsync();
            return usuario_encontrado; 
        }
        //
        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _dbContext.Usuarios.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }
    }
}
