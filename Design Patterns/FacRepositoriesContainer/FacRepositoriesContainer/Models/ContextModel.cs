using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FacRepositoriesContainer.Models
{
    public class ContextModel : IContextModel
    {
        private System.Data.IDbConnection _cn;
        private System.Data.IDbTransaction _transaction;

        public ContextModel()
        {
            _cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DB"]);
            Open();
        }

        void IContextModel.BeginTRansaction()
        {
            _transaction = _cn.BeginTransaction();
        }

        int IContextModel.Execute(string query)
        {
            if (_transaction != null)
                return _transaction.Connection.Execute(query);

            return _cn.Execute(query);
        }

        IEnumerable<dynamic> IContextModel.ExecuteQuery(string query, object parameters)
        {
            if (_transaction != null)
                return _transaction.Connection.Query(query, parameters);

            return _cn.Query(query, parameters);
        }

        private void Open()
        {
            if (_cn.State == System.Data.ConnectionState.Closed)
                _cn.Open();
        }

        void IContextModel.Commit()
        {
            _transaction.Commit();
        }

        void IContextModel.Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            catch { }
        }
    }
}