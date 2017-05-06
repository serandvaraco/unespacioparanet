using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorage.Manager.Table
{
    public class TableStorage : ITableStorage
    {
        CloudStorageAccount _storageAccount =
         StorageAccount.CreateStorageAccount();

        CloudTableClient tableClient;
        CloudTable table;

        public TableStorage()
        {
            tableClient =
                _storageAccount.CreateCloudTableClient();
        }

        public CloudTable GetTable(string tableName)
        {
            table = tableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
            return table;
        }

        public void Insert<T>(T entity) where T : ITableEntity
        {
            TableOperation insertOperation =
                TableOperation.Insert(entity);

            table.Execute(insertOperation);

        }

        public void InsertBatch<T>(IEnumerable<T> entities) where T : ITableEntity
        {
            TableBatchOperation batchOperation =
                new TableBatchOperation();


            entities.ToList().ForEach(x =>
            {
                batchOperation.Insert(x);
            });

            table.ExecuteBatch(batchOperation);

        }

        public void Update<T>(T oldEntity, T newEntity) where T : ITableEntity
        {
            TableOperation retrieveOperation =
                TableOperation.Retrieve<T>(oldEntity.PartitionKey, oldEntity.RowKey);

            TableResult retrievedResult = table.Execute(retrieveOperation);

            T updateEntity = (T)retrievedResult.Result;

            if (updateEntity == null)
                return;

            TableOperation updateOperation = TableOperation.Replace(newEntity);
            table.Execute(updateOperation);
        }

        public T GetEntity<T>(string partitionKey, string rowKey) where T : ITableEntity
        {
            TableOperation retrieveOperation =
                TableOperation.Retrieve<T>(partitionKey, rowKey);

            TableResult retrievedResult = table.Execute(retrieveOperation);

            if (retrievedResult.Result != null)
                return (T)retrievedResult.Result;

            throw new NullReferenceException("La entidad no existe");
        }

        public IEnumerable<T> QueryValueString<T>
            (string property, string queryComparisons, string value) where T : ITableEntity
        {

            var queryResult = new TableQuery().Where(
                TableQuery.GenerateFilterCondition(property, queryComparisons, value));

            return (IEnumerable<T>)table.ExecuteQuery(queryResult);

        }

    }
}
