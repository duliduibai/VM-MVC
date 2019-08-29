using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VM.Repository
{
    public abstract class VmRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public VmDbContext DbContext { get; private set; }
        public DbSet<TEntity> DbSet { get; private set; }
        public VmRepository(VmDbContext context)
        {
            ///TODO: What does Guard mean?
            //Guard.ArgumentNotNull(context, "context");
            DbContext = context;
            DbSet = DbContext.Set<TEntity>();
        }
        public void Add(TEntity instance)
        {
            //DbSet.Add(instance);
            //DbContext.SaveChanges();
            DbSet.Attach(instance);
            DbContext.Entry(instance).State = EntityState.Added;
            DbContext.SaveChanges();
        }

        public int Count(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.Where(filter).Count();
        }

        public void Delete(TEntity instance)
        {
            DbSet.Attach(instance);
            DbContext.Entry(instance).State = EntityState.Deleted;
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public IEnumerable<TEntity> Get()
        {
            return this.DbSet.AsEnumerable();
        }


        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            ///TODO: Why AsQuerable()???this.DbSet.Where(filter)本身就是Querable
            return this.DbSet.Where(filter).AsQueryable();
        }

        public IEnumerable<TEntity> Get<TOrderKey>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize, Expression<Func<TEntity, TOrderKey>> sortExpression, bool isAsc = true)
        {
            if (isAsc)
            {
                return this.DbSet
                    .Where(filter)
                    .OrderBy(sortExpression)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize);
            }
            else
            {
                return this.DbSet
                    .Where(filter)
                    .OrderByDescending(sortExpression)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize);
            }
        }

        public void Update(TEntity instance)
        {
            DbSet.Attach(instance);
            DbContext.Entry(instance).State = EntityState.Modified;
            DbContext.SaveChanges();
        }
    }
}
