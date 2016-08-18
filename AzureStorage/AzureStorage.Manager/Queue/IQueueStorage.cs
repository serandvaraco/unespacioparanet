using Microsoft.WindowsAzure.Storage.Queue;

namespace AzureStorage.Manager.Queue
{
    public interface IQueueStorage
    {
        void DeleteMessage();
        string GetMessage();
        void SetQueueName(string queueName);
        void SendMessage(string message);
        void UpdateMessage(string newMessge);
        void DeleteQueue(); 
    }
}