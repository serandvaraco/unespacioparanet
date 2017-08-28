using System.Data.Entity;

namespace FacRepositoriesContainer.Models
{
    public class AlbumsContext : DbContext
    {
        public AlbumsContext() : base("AlbumsDB") { }
        public IDbSet<AlbumsModel> Albums { get; set; }
    }
}