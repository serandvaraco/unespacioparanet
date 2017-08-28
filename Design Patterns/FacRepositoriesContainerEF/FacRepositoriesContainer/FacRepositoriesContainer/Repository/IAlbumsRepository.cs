using FacRepositoriesContainer.Models;
using System.Collections.Generic;

namespace FacRepositoriesContainer.Repository
{
    public interface IAlbumsRepository:IModelRepository<AlbumsModel>
    {
        IEnumerable<AlbumsModel> GetAlbumsByTitle(string title);
    }
}
