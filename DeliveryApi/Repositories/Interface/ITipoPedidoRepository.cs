using DeliveryApi.Models;
using System.Collections.Generic;

namespace DeliveryApi.Repositories.Interface
{
    public interface ITipoPedidoRepository
    {
        int Create(TipoPedidoModel tipoPedido);
        bool Update(TipoPedidoModel tipoPedido);
        bool Delete(TipoPedidoModel tipoPedido);
        TipoPedidoModel Get(int tipoPedidoId);
        List<TipoPedidoModel> List();
    }
}
