using DeliveryApi.Models;
using System.Collections.Generic;

namespace DeliveryApi.Repositories.Interface
{
    public interface IUsuarioRepository
    {
        int Create(UsuarioModel usuario);
        bool Update(UsuarioModel usuario);
        bool Delete(UsuarioModel usuario);
        UsuarioModel Get(int usuarioId);
        List<UsuarioModel> List(int empresaId);
        UsuarioModel ConsultaPorEmail(string email);
    }
}
