using DeliveryApi.Models;
using System.Collections.Generic;

namespace DeliveryApi.Repositories.Interface
{
    public interface IEmpresaRepository
    {
        int Create(EmpresaModel empresa);
        bool Update(EmpresaModel empresa);
        bool Delete(EmpresaModel empresa);
        EmpresaModel Get(int empresaId);
        List<EmpresaModel> List();
    }
}
