using DeliveryApi.Models;
using System.Collections.Generic;
using System.Linq;
using DeliveryApi.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApi.Repositories.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly WebAppDbContext _context;
        public UsuarioRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public int Create(UsuarioModel usuario)
        {
            if(_context != null)
            {
                _context.usuarios.Add(usuario);
                _context.SaveChanges();

                return usuario.Id;
            }

            return 0;
        }

        public bool Update(UsuarioModel usuario)
        {
            if (_context != null)
            {
                _context.usuarios.Update(usuario);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Delete(UsuarioModel usuario)
        {
            if (_context != null)
            {
                _context.usuarios.Remove(usuario);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public UsuarioModel Get(int usuarioId)
        {
            if (_context != null)
            {
                var usuario = _context.usuarios.FirstOrDefault(x => x.Id == usuarioId);
                return usuario;
            }

            return null;
        }

        public List<UsuarioModel> List(int empresaId)
        {
            if (_context != null)
            {
                var usuarios = _context.usuarios.AsNoTracking().Where(x => x.EmpresaId == empresaId).ToList();
                return usuarios;
            }

            return null;
        }

        public UsuarioModel ConsultaPorEmail(string email)
        {
            if (_context != null)
            {
                var usuario = _context.usuarios.AsNoTracking().FirstOrDefault(x => x.Email == email);
                return usuario;
            }

            return null;
        }
    }
}
