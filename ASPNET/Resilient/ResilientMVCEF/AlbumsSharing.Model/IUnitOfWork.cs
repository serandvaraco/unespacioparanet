using AlbumsSharing.Model.Repositories;

namespace AlbumsSharing.Model
{
    public interface IUnitOfWork
    {
        IAlbumsRepository Albums { get; }

        void Commit();
    }
}