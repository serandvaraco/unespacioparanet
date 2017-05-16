using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacRepositoriesContainer.Models
{
    public class ProjectRepository : IRepository
    {
        private readonly IContextModel _context;
        public ProjectRepository(IContextModel context)
        {
            _context = context;
        }
        IEnumerable<dynamic> IRepository.GetAll()
        {
            return  _context.ExecuteQuery("SELECT p.* FROM Proyecto p ");
        }
    }
}