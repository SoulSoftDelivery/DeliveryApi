using DeliveryApi.Models;
using System.Collections.Generic;

namespace DeliveryApi.Repositories.Interface
{
    public interface ICategoriaProdutoRepository
    {
        int Create(CategoriaProdutoModel categoriaProduto);
        bool Update(CategoriaProdutoModel categoriaProduto);
        bool Delete(CategoriaProdutoModel categoriaProduto);
        CategoriaProdutoModel Get(int categoriaProdutoId);
        List<CategoriaProdutoModel> List();
    }
}
