using System;
namespace FacRepositoriesContainer.Models
{
    public class AlbumsInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<AlbumsContext>
    {
        protected override void Seed(AlbumsContext context)
        {

            context.Albums.Add(new AlbumsModel
            {
                Artist = "Fania All Stars",
                DateLaunch = new DateTime(1973, 05, 16),
                Name = "Fania Vol 12"
            });
            context.SaveChanges();

            base.Seed(context);
        }
    }
}