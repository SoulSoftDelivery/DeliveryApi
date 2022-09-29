using Microsoft.EntityFrameworkCore;
using DeliveryApi.Models;
using System.Collections.Generic;
using System.Linq;
using DeliveryApi.Repositories.Interface;

namespace DeliveryApi.Repositories.Repository
{
    public class SituacaoPedidoRepository : ISituacaoPedidoRepository
    {
        private readonly WebAppDbContext _context;
        public SituacaoPedidoRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public int Create(SituacaoPedidoModel situacaoPedido)
        {
            if(_context != null)
            {
                _context.situacoes_pedidos.Add(situacaoPedido);
                _context.SaveChanges();

                return situacaoPedido.Id;
            }

            return 0;
        }

        public bool Update(SituacaoPedidoModel situacaoPedido)
        {
            if (_context != null)
            {
                _context.situacoes_pedidos.Update(situacaoPedido);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Delete(SituacaoPedidoModel situacaoPedido)
        {
            if (_context != null)
            {
                _context.situacoes_pedidos.Remove(situacaoPedido);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public SituacaoPedidoModel Get(int situacaoPedidoId)
        {
            if (_context != null)
            {
                var situacaoPedido = _context.situacoes_pedidos.FirstOrDefault(x => x.Id == situacaoPedidoId);
                return situacaoPedido;
            }

            return null;
        }

        public List<SituacaoPedidoModel> List()
        {
            if (_context != null)
            {
                var situacoesPedidos = _context.situacoes_pedidos.ToList();
                return situacoesPedidos;
            }

            return null;
        }
    }
}
