using System.Linq;
using PhapY.EntityFramework;
using PhapY.MultiTenancy;

namespace PhapY.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly PhapYDbContext _context;

        public DefaultTenantCreator(PhapYDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
