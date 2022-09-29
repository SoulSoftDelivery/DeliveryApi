using DeliveryApi.Models;
using System.Collections.Generic;
using System.Linq;
using DeliveryApi.Repositories.Interface;

namespace DeliveryApi.Repositories.Repository
{
    public class CategoriaProdutoRepository : ICategoriaProdutoRepository
    {
        private readonly WebAppDbContext _context;
        public CategoriaProdutoRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public int Create(CategoriaProdutoModel categoriaProduto)
        {
            if(_context != null)
            {
                _context.categorias_produtos.Add(categoriaProduto);
                _context.SaveChanges();
                return categoriaProduto.Id;
            }

            return 0;
        }

        public bool Update(CategoriaProdutoModel categoriaProduto)
        {
            if (_context != null)
            {
                _context.categorias_produtos.Update(categoriaProduto);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Delete(CategoriaProdutoModel categoriaProduto)
        {
            if (_context != null)
            {
                _context.categorias_produtos.Remove(categoriaProduto);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public CategoriaProdutoModel Get(int categoriaProdutoId)
        {
            if (_context != null)
            {
                var categoriaProduto = _context.categorias_produtos.FirstOrDefault(x => x.Id == categoriaProdutoId);
                return categoriaProduto;
            }

            return null;
        }

        public List<CategoriaProdutoModel> List()
        {
            if (_context != null)
            {
                var categoriasProdutos = _context.categorias_produtos.ToList();
                return categoriasProdutos;
            }

            return null;
        }
    }
}
