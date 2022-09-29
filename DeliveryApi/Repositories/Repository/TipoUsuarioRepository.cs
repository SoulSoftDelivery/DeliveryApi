using Microsoft.EntityFrameworkCore;
using DeliveryApi.Models;
using System.Collections.Generic;
using System.Linq;
using DeliveryApi.Repositories.Interface;

namespace DeliveryApi.Repositories.Repository
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly WebAppDbContext _context;
        public TipoUsuarioRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public int Create(TipoUsuarioModel tipoUsuario)
        {
            if(_context != null)
            {
                _context.tipos_usuarios.Add(tipoUsuario);
                _context.SaveChanges();

                return tipoUsuario.Id;
            }

            return 0;
        }

        public bool Update(TipoUsuarioModel tipoUsuario)
        {
            if (_context != null)
            {
                _context.tipos_usuarios.Update(tipoUsuario);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Delete(TipoUsuarioModel tipoUsuario)
        {
            if (_context != null)
            {
                _context.tipos_usuarios.Remove(tipoUsuario);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public TipoUsuarioModel Get(int tipoUsuarioId)
        {
            if (_context != null)
            {
                var tipoUsuario = _context.tipos_usuarios.FirstOrDefault(x => x.Id == tipoUsuarioId);
                return tipoUsuario;
            }

            return null;
        }

        public List<TipoUsuarioModel> List()
        {
            if (_context != null)
            {
                var tiposUsuarios = _context.tipos_usuarios.ToList();
                return tiposUsuarios;
            }

            return null;
        }
    }
}
