using System.Data.Entity;
using FacRepositoriesContainer.Models;
using System.Collections.Generic;
using System.Linq;

namespace FacRepositoriesContainer.Repository
{
    public class AlbumsRepositories : ModelRepository<AlbumsModel>, IAlbumsRepository
    {
        public AlbumsRepositories(DbContext dbcontext) : base(dbcontext){ }
        public IEnumerable<AlbumsModel> GetAlbumsByTitle(string title) => dbSet.Where(x=>x.Name.ToLower().Contains(title.ToLower()));
    }
}