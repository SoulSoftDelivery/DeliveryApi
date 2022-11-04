using DeliveryApi.Models;
using System.Collections.Generic;
using System.Linq;
using DeliveryApi.Repositories.Interface;

namespace DeliveryApi.Repositories.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly WebAppDbContext _context;
        public EnderecoRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public int Create(EnderecoModel endereco)
        {
            if(_context != null)
            {
                _context.enderecos.Add(endereco);
                _context.SaveChanges();

                return endereco.Id;
            }

            return 0;
        }

        public bool Update(EnderecoModel endereco)
        {
            if (_context != null)
            {
                _context.enderecos.Update(endereco);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Delete(EnderecoModel endereco)
        {
            if (_context != null)
            {
                _context.enderecos.Remove(endereco);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public EnderecoModel Get(int enderecoId)
        {
            if (_context != null)
            {
                var endereco = _context.enderecos.FirstOrDefault(x => x.Id == enderecoId);
                return endereco;
            }

            return null;
        }


        public EnderecoModel EnderecoByClienteId(int clienteId)
        {
            if (_context != null)
            {
                var endereco = _context.enderecos.FirstOrDefault(x => x.ClienteId == clienteId);
                return endereco;
            }

            return null;
        }
    }
}
