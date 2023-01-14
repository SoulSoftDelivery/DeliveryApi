using DeliveryApi.Models;
using System.Collections.Generic;
using System.Linq;
using DeliveryApi.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApi.Repositories.Repository
{
    public class TipoPedidoRepository : ITipoPedidoRepository
    {
        private readonly WebAppDbContext _context;
        public TipoPedidoRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public int Create(TipoPedidoModel tipoPedido)
        {
            if(_context != null)
            {
                _context.tipos_pedidos.Add(tipoPedido);
                _context.SaveChanges();

                return tipoPedido.Id;
            }

            return 0;
        }

        public bool Update(TipoPedidoModel tipoPedido)
        {
            if (_context != null)
            {
                _context.tipos_pedidos.Update(tipoPedido);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Delete(TipoPedidoModel tipoPedido)
        {
            if (_context != null)
            {
                _context.tipos_pedidos.Remove(tipoPedido);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public TipoPedidoModel Get(int tipoPedidoId)
        {
            if (_context != null)
            {
                var tipoPedido = _context.tipos_pedidos.AsNoTracking().FirstOrDefault(x => x.Id == tipoPedidoId);
                return tipoPedido;
            }

            return null;
        }

        public List<TipoPedidoModel> List()
        {
            if (_context != null)
            {
                var tipospedidos = _context.tipos_pedidos.AsNoTracking().ToList();
                return tipospedidos;
            }

            return null;
        }
    }
}
