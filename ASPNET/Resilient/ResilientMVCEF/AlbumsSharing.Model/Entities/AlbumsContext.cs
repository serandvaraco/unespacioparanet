using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumsSharing.Model.Entities
{
    public class AlbumsContext : DbContext
    {
        public AlbumsContext() : base("AlbumsDB") { }
        public IDbSet<Albums> Albums { get; set; }

    }
}
