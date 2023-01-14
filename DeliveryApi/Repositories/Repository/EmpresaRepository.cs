using DeliveryApi.Models;
using System.Collections.Generic;
using System.Linq;
using DeliveryApi.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApi.Repositories.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly WebAppDbContext _context;
        public EmpresaRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public int Create(EmpresaModel empresa)
        {
            if(_context != null)
            {
                _context.empresas.Add(empresa);
                _context.SaveChanges();

                return empresa.Id;
            }

            return 0;
        }

        public bool Update(EmpresaModel empresa)
        {
            if (_context != null)
            {
                _context.empresas.Update(empresa);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Delete(EmpresaModel empresa)
        {
            if (_context != null)
            {
                _context.empresas.Remove(empresa);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public EmpresaModel Get(int empresaId)
        {
            if (_context != null)
            {
                var empresa = _context.empresas.AsNoTracking().FirstOrDefault(x => x.Id == empresaId);
                return empresa;
            }

            return null;
        }

        public List<EmpresaModel> List()
        {
            if (_context != null)
            {
                var empresas = _context.empresas.AsNoTracking().ToList();
                return empresas;
            }

            return null;
        }
    }
}
