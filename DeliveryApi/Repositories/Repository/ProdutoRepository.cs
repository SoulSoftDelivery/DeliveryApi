using DeliveryApi.Models;
using System.Collections.Generic;
using System.Linq;
using DeliveryApi.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

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
                //Modificação total
                //_context.Entry(produto).State = EntityState.Modified;

                //Modificação parcial
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

        public ProdutoModel GetById(int produtoId)
        {
            if (_context != null)
            {
                var produto = _context.produtos.Include(cp => cp.CategoriaProduto).Include(tm => tm.TipoMedida).AsNoTracking().FirstOrDefault(p => p.Id == produtoId);
                return produto;
            }

            return null;
        }

        public List<ProdutoModel> GetListByEmpresaId(int empresaId)
        {
            if (_context != null)
            {
                var produtos = _context.produtos.Include(cp => cp.CategoriaProduto).Include(tm => tm.TipoMedida).AsNoTracking().Where(p => p.EmpresaId == empresaId).ToList();
                return produtos;
            }

            return null;
        }
    }
}
