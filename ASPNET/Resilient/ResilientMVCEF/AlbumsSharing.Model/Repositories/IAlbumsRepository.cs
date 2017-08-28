using System.Collections.Generic;
using AlbumsSharing.Model.Entities;

namespace AlbumsSharing.Model.Repositories
{
    public interface IAlbumsRepository: IModelRepository<Albums>
    {
        IEnumerable<Albums> GetAlbumsByTitle(string title);
    }
}