using Domain;
using IRepository;
using IService;

namespace Service
{
    public class PPermissionService : BaseService<PPermission>, IPPermissionService
    {
        public PPermissionService(IBaseRepository<PPermission> baseRepository) : base(baseRepository)
        {
        }
    }
}
