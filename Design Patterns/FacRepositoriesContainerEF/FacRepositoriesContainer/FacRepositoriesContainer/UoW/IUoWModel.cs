using FacRepositoriesContainer.Repository;

namespace FacRepositoriesContainer.UoW
{
    public interface IUoWModel
    {
        IAlbumsRepository Albums { get; }

        void Commit();
    }
}