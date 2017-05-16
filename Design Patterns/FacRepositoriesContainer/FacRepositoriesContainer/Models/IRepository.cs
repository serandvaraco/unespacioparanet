using System.Collections.Generic;

namespace FacRepositoriesContainer.Models
{
    public interface IRepository
    {
        IEnumerable<dynamic> GetAll();
    }
}