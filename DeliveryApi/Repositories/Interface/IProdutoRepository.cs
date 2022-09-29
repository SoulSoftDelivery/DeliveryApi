using DeliveryApi.Models;
using System.Collections.Generic;

namespace DeliveryApi.Repositories.Interface
{
    public interface IProdutoRepository
    {
        int Create(ProdutoModel produto);
        bool Update(ProdutoModel produto);
        bool Delete(ProdutoModel produto);
        ProdutoModel Get(int produtoId);
        List<ProdutoModel> List(int empresaId);
    }
}
