using DeliveryApi.Models;
using System.Collections.Generic;

namespace DeliveryApi.Repositories.Interface
{
    public interface ITipoEnderecoRepository
    {
        int Create(TipoEnderecoModel tipoEndereco);
        bool Update(TipoEnderecoModel tipoEndereco);
        bool Delete(TipoEnderecoModel tipoEndereco);
        TipoEnderecoModel Get(int tipoEnderecoId);
        List<TipoEnderecoModel> List();
    }
}
