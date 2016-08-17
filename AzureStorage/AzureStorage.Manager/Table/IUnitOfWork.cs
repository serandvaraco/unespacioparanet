using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage.Manager.Table
{
    public interface IUnitOfWork<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);

        T GetEntity(string partitionKey, string rowKey);

        IEnumerable<T> Query(string filter);

    }
}
