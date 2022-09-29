using DeliveryApi.Models;
using System.Collections.Generic;

namespace DeliveryApi.Repositories.Interface
{
    public interface IClienteRepository
    {
        int Create(ClienteModel cliente);
        bool Update(ClienteModel cliente);
        bool Delete(ClienteModel cliente);
        ClienteModel Get(int clienteId);
        List<ClienteModel> List(int empresaId);
    }
}
