using DeliveryApi.Models;

namespace DeliveryApi.Repositories.Interface
{
    public interface IErroRepository
    {
        int Notify(ErroModel erro);
    }
}
