using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureStorage.Manager.Table
{
    public interface ITableStorage
    {
        T GetEntity<T>(string partitionKey, string rowKey) where T : ITableEntity;
        CloudTable GetTable(string tableName);
        void Insert<T>(T entity) where T : ITableEntity;
        void InsertBatch<T>(IEnumerable<T> entities) where T : ITableEntity;
        IEnumerable<T> QueryValueString<T>(string property, string queryComparisons, string value) where T : ITableEntity;
        void Update<T>(T oldEntity, T newEntity) where T : ITableEntity;
    }
}