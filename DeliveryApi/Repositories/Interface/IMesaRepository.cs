using DeliveryApi.Models;
using System.Collections.Generic;

namespace DeliveryApi.Repositories.Interface
{
    public interface IMesaRepository
    {
        int Create(MesaModel mesa);
        bool Update(MesaModel mesa);
        bool Delete(MesaModel mesa);
        MesaModel Get(int mesaId);
        List<MesaModel> List(int empresaId);
    }
}
