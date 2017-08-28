using System.Data.Entity.Infrastructure;

namespace AlbumsSharing.Model.Repositories
{
    public interface IModelRepository<T> where T : class
    {
        T Add(T entity);
        void Delete(T entity);
        T GetById(int id);
        DbQuery<T> GetQueryEntity();
        void Update(T entity);
    }
}