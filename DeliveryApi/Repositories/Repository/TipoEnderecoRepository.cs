using Microsoft.EntityFrameworkCore;
using DeliveryApi.Models;
using System.Collections.Generic;
using System.Linq;
using DeliveryApi.Repositories.Interface;

namespace DeliveryApi.Repositories.Repository
{
    public class TipoEnderecoRepository : ITipoEnderecoRepository
    {
        private readonly WebAppDbContext _context;
        public TipoEnderecoRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public int Create(TipoEnderecoModel tipoEndereco)
        {
            if(_context != null)
            {
                _context.tipos_enderecos.Add(tipoEndereco);
                _context.SaveChanges();

                return tipoEndereco.Id;
            }

            return 0;
        }

        public bool Update(TipoEnderecoModel tipoEndereco)
        {
            if (_context != null)
            {
                _context.tipos_enderecos.Update(tipoEndereco);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Delete(TipoEnderecoModel tipoEndereco)
        {
            if (_context != null)
            {
                _context.tipos_enderecos.Remove(tipoEndereco);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public TipoEnderecoModel Get(int tipoEnderecoId)
        {
            if (_context != null)
            {
                var tipoEndereco = _context.tipos_enderecos.FirstOrDefault(x => x.Id == tipoEnderecoId);
                return tipoEndereco;
            }

            return null;
        }

        public List<TipoEnderecoModel> List()
        {
            if (_context != null)
            {
                var tiposUsuarios = _context.tipos_enderecos.ToList();
                return tiposUsuarios;
            }

            return null;
        }
    }
}
