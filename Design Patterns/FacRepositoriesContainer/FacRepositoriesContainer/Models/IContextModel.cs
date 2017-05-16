using System.Collections.Generic;

namespace FacRepositoriesContainer.Models
{
    public interface IContextModel
    {
        int Execute(string query);
        void BeginTRansaction();
        IEnumerable<dynamic> ExecuteQuery(string query, object parameters = null);
        void Rollback();
        void Commit();

    }
}