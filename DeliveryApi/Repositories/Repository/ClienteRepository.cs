using DeliveryApi.Models;
using System.Collections.Generic;
using System.Linq;
using DeliveryApi.Repositories.Interface;

namespace DeliveryApi.Repositories.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly WebAppDbContext _context;
        public ClienteRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public int Create(ClienteModel cliente)
        {
            if(_context != null)
            {
                _context.clientes.Add(cliente);
                _context.SaveChanges();

                return cliente.Id;
            }

            return 0;
        }

        public bool Update(ClienteModel cliente)
        {
            if (_context != null)
            {
                _context.clientes.Update(cliente);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Delete(ClienteModel cliente)
        {
            if (_context != null)
            {
                _context.clientes.Remove(cliente);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public ClienteModel Get(int clienteId)
        {
            if (_context != null)
            {
                var cliente = _context.clientes.FirstOrDefault(x => x.Id == clienteId);
                return cliente;
            }

            return null;
        }

        public List<ClienteModel> List(int empresaId)
        {
            if (_context != null)
            {
                var clientes = _context.clientes.Where(x => x.EmpresaId == empresaId).ToList();
                return clientes;
            }

            return null;
        }
    }
}
