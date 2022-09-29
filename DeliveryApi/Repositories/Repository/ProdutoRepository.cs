using DeliveryApi.Models;
using System.Collections.Generic;
using System.Linq;
using DeliveryApi.Repositories.Interface;

namespace DeliveryApi.Repositories.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly WebAppDbContext _context;
        public ProdutoRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public int Create(ProdutoModel produto)
        {
            if(_context != null)
            {
                _context.produtos.Add(produto);
                _context.SaveChanges();

                return produto.Id;
            }

            return 0;
        }

        public bool Update(ProdutoModel produto)
        {
            if (_context != null)
            {
                _context.produtos.Update(produto);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Delete(ProdutoModel produto)
        {
            if (_context != null)
            {
                _context.produtos.Remove(produto);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public ProdutoModel Get(int produtoId)
        {
            if (_context != null)
            {
                var produto = _context.produtos.FirstOrDefault(x => x.Id == produtoId);
                return produto;
            }

            return null;
        }

        public List<ProdutoModel> List(int empresaId)
        {
            if (_context != null)
            {
                var produtos = _context.produtos.Where(x => x.EmpresaId == empresaId).ToList();
                return produtos;
            }

            return null;
        }
    }
}
