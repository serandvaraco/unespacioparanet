using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace AlbumsSharing.Model.Entities
{
    public class AlbumsInitializer : DropCreateDatabaseIfModelChanges<AlbumsContext>
    {
        protected override void Seed(AlbumsContext context)
        {
            context.Albums.Add(new Albums
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
