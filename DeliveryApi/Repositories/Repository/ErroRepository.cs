using DeliveryApi.Models;
using DeliveryApi.Repositories.Interface;

namespace DeliveryApi.Repositories.Repository
{
    public class ErroRepository : IErroRepository
    {
        private readonly WebAppDbContext _context;
        public ErroRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public int Notify(ErroModel erro)
        {
            if(_context != null)
            {
                _context.erros.Add(erro);
                _context.SaveChanges();

                return erro.Id;
            }

            return 0;
        }
    }
}
