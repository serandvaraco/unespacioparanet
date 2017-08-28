using FacRepositoriesContainer.Models;
using FacRepositoriesContainer.Repository;

namespace FacRepositoriesContainer.UoW
{
    public class UoWModel : IUoWModel
    {
        AlbumsContext context; 
        public UoWModel(AlbumsContext dbContext)
        {
            context = dbContext; 
        }
        public void Commit() => context.SaveChanges();

        private IAlbumsRepository _albums; 
        public IAlbumsRepository Albums =>  _albums ?? (_albums = new AlbumsRepositories(context));  

    }
}