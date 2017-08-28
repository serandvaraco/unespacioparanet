using AlbumsSharing.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AlbumsSharing.Model.Repositories
{
    public class AlbumsRepository : ModelRepository<Albums>, IAlbumsRepository
    {
        public AlbumsRepository(DbContext dbcontext) : base(dbcontext)
        {
        }

        public IEnumerable<Albums> GetAlbumsByTitle(string title) 
            => dbSet.Where(x => x.Name.ToLower().Contains(title.ToLower()));
    }
}
