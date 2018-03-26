using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace PhapY.EntityFramework.Repositories
{
    public abstract class PhapYRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<PhapYDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected PhapYRepositoryBase(IDbContextProvider<PhapYDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class PhapYRepositoryBase<TEntity> : PhapYRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected PhapYRepositoryBase(IDbContextProvider<PhapYDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
