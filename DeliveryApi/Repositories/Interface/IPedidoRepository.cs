using DeliveryApi.Models;
using System.Collections.Generic;

namespace DeliveryApi.Repositories.Interface
{
    public interface IPedidoRepository
    {
        int Create(PedidoModel pedido);
        int CreatePedidoProduto(PedidoProdutoModel pedidoProduto);
        bool Update(PedidoModel pedido);
        bool Delete(PedidoModel pedido);
        PedidoModel Get(int pedidoId);
        List<PedidoModel> List(int empresaId);
    }
}
