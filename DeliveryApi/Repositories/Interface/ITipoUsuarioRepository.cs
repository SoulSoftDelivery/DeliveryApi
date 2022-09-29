using DeliveryApi.Models;
using System.Collections.Generic;

namespace DeliveryApi.Repositories.Interface
{
    public interface ITipoUsuarioRepository
    {
        int Create(TipoUsuarioModel tipoUsuario);
        bool Update(TipoUsuarioModel tipoUsuario);
        bool Delete(TipoUsuarioModel tipoUsuario);
        TipoUsuarioModel Get(int tipoUsuarioId);
        List<TipoUsuarioModel> List();
    }
}
