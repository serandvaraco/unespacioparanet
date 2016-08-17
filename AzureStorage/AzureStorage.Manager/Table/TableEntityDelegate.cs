using Microsoft.WindowsAzure.Storage.Table;

namespace AzureStorage.Manager.Table
{
    public class TableEntityDelegate : TableEntity
    {
        public TableEntityDelegate(string partitionKey, string rowKey)
            :base(partitionKey,rowKey)
        {}
    }
}
