using Domain;
using IRepository;
using IService;

namespace Service
{
    public class PAccountService : BaseService<PAccount>, IPAccountService
    {
        public PAccountService(IBaseRepository<PAccount> baseRepository) : base(baseRepository)
        {
        }
    }
}
