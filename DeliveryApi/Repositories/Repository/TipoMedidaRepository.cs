using Microsoft.EntityFrameworkCore;
using DeliveryApi.Models;
using System.Collections.Generic;
using System.Linq;
using DeliveryApi.Repositories.Interface;

namespace DeliveryApi.Repositories.Repository
{
    public class TipoMedidaRepository : ITipoMedidaRepository
    {
        private readonly WebAppDbContext _context;
        public TipoMedidaRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public int Create(TipoMedidaModel tipoMedida)
        {
            if(_context != null)
            {
                _context.tipos_medidas.Add(tipoMedida);
                _context.SaveChanges();

                return tipoMedida.Id;
            }

            return 0;
        }

        public bool Update(TipoMedidaModel tipoMedida)
        {
            if (_context != null)
            {
                _context.tipos_medidas.Update(tipoMedida);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Delete(TipoMedidaModel tipoMedida)
        {
            if (_context != null)
            {
                _context.tipos_medidas.Remove(tipoMedida);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public TipoMedidaModel Get(int tipoMedidaId)
        {
            if (_context != null)
            {
                var tipoMedida = _context.tipos_medidas.FirstOrDefault(x => x.Id == tipoMedidaId);
                return tipoMedida;
            }

            return null;
        }

        public List<TipoMedidaModel> List()
        {
            if (_context != null)
            {
                var tiposmedidas = _context.tipos_medidas.ToList();
                return tiposmedidas;
            }

            return null;
        }
    }
}
