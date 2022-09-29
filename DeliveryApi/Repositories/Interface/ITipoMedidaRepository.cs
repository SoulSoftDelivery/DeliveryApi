using DeliveryApi.Models;
using System.Collections.Generic;

namespace DeliveryApi.Repositories.Interface
{
    public interface ITipoMedidaRepository
    {
        int Create(TipoMedidaModel tipoMedida);
        bool Update(TipoMedidaModel tipoMedida);
        bool Delete(TipoMedidaModel tipoMedida);
        TipoMedidaModel Get(int tipoMedidaId);
        List<TipoMedidaModel> List();
    }
}
