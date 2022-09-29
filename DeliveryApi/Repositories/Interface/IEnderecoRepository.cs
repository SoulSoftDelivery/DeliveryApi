using DeliveryApi.Models;

namespace DeliveryApi.Repositories.Interface
{
    public interface IEnderecoRepository
    {
        int Create(EnderecoModel Endereco);
        bool Update(EnderecoModel Endereco);
        bool Delete(EnderecoModel Endereco);
        EnderecoModel Get(int EnderecoId);
    }
}
