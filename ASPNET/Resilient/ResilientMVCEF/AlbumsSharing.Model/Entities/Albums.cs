using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumsSharing.Model.Entities
{
    public class Albums
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateLaunch { get; set; }
        public string Artist { get; set; }
    }
}
