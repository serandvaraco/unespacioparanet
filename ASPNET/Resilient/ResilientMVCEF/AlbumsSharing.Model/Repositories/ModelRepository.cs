using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumsSharing.Model.Repositories
{
    public class ModelRepository<T> : IModelRepository<T> where T:class
    {
        private DbContext context { get; set; }
        protected IDbSet<T> dbSet { get; set; }
        public ModelRepository(DbContext dbcontext)
        {
            context =  dbcontext ?? throw new ArgumentNullException("the repository is null"); ;
            dbSet = context.Set<T>(); 
        }

        public T GetById(int id) => dbSet.Find(id);
        public T Add(T entity) => dbSet.Add(entity);
        public void Update(T entity) => context.Entry(entity).State = EntityState.Modified;
        public void Delete(T entity) => context.Entry(entity).State = EntityState.Deleted;
        public DbQuery<T> GetQueryEntity() => context.Set<T>(); 

    }
}
