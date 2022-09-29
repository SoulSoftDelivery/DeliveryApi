using DeliveryApi.Models;
using System.Collections.Generic;
using System.Linq;
using DeliveryApi.Repositories.Interface;

namespace DeliveryApi.Repositories.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly WebAppDbContext _context;
        public PedidoRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public int Create(PedidoModel pedido)
        {
            if(_context != null)
            {
                _context.pedidos.Add(pedido);
                _context.SaveChanges();

                return pedido.Id;
            }

            return 0;
        }
        
        public int CreatePedidoProduto(PedidoProdutoModel pedidoProduto)
        {
            if (_context != null)
            {
                _context.pedidos_produtos.Add(pedidoProduto);
                _context.SaveChanges();

                return pedidoProduto.Id;
            }

            return 0;
        }

        public bool Update(PedidoModel pedido)
        {
            if (_context != null)
            {
                _context.pedidos.Update(pedido);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Delete(PedidoModel pedido)
        {
            if (_context != null)
            {
                _context.pedidos.Remove(pedido);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public PedidoModel Get(int pedidoId)
        {
            if (_context != null)
            {
                var pedido = _context.pedidos.FirstOrDefault(x => x.Id == pedidoId);
                return pedido;
            }

            return null;
        }

        public List<PedidoModel> List(int empresaId)
        {
            if (_context != null)
            {
                var pedidos = _context.pedidos.Where(x => x.EmpresaId == empresaId).ToList();
                return pedidos;
            }

            return null;
        }
    }
}
