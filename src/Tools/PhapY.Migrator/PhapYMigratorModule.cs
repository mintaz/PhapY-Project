using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using PhapY.EntityFramework;

namespace PhapY.Migrator
{
    [DependsOn(typeof(PhapYDataModule))]
    public class PhapYMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<PhapYDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}