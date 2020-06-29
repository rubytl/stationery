using Stationery.Common.Entities;

namespace Stationery.Membership.Data.Repositories
{
    public class TenantRepository : EntityBaseRepository<Tenant>, ITenantRepository
    {
        public TenantRepository(CatalogDbContext context)
            : base(context)
        { }
    }
}
