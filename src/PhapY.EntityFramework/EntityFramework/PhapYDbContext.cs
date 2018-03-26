using System.Data.Common;
using Abp.Zero.EntityFramework;
using PhapY.Authorization.Roles;
using PhapY.Authorization.Users;
using PhapY.MultiTenancy;

namespace PhapY.EntityFramework
{
    public class PhapYDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public PhapYDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in PhapYDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of PhapYDbContext since ABP automatically handles it.
         */
        public PhapYDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public PhapYDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public PhapYDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
