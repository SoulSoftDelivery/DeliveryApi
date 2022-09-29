using DeliveryApi.Models;
using System.Collections.Generic;

namespace DeliveryApi.Repositories.Interface
{
    public interface ISituacaoPedidoRepository
    {
        int Create(SituacaoPedidoModel situacaoPedido);
        bool Update(SituacaoPedidoModel situacaoPedido);
        bool Delete(SituacaoPedidoModel situacaoPedido);
        SituacaoPedidoModel Get(int situacaoPedidoId);
        List<SituacaoPedidoModel> List();
    }
}
