using Microsoft.EntityFrameworkCore;
using DeliveryApi.Models;
using System.Collections.Generic;
using System.Linq;
using DeliveryApi.Repositories.Interface;

namespace DeliveryApi.Repositories.Repository
{
    public class MesaRepository : IMesaRepository
    {
        private readonly WebAppDbContext _context;
        public MesaRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public int Create(MesaModel mesa)
        {
            if(_context != null)
            {
                _context.mesas.Add(mesa);
                _context.SaveChanges();

                return mesa.Id;
            }

            return 0;
        }

        public bool Update(MesaModel mesa)
        {
            if (_context != null)
            {
                _context.mesas.Update(mesa);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Delete(MesaModel mesa)
        {
            if (_context != null)
            {
                _context.mesas.Remove(mesa);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public MesaModel Get(int mesaId)
        {
            if (_context != null)
            {
                var mesa = _context.mesas.AsNoTracking().FirstOrDefault(x => x.Id == mesaId);
                return mesa;
            }

            return null;
        }

        public List<MesaModel> List(int empresaId)
        {
            if (_context != null)
            {
                var mesas = _context.mesas.AsNoTracking().Where(x => x.EmpresaId == empresaId).ToList();
                return mesas;
            }

            return null;
        }
    }
}
