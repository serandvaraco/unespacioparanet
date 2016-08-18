using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;

namespace AzureStorage.Manager.Queue
{
    public class QueueStorage : IQueueStorage
    {
        CloudStorageAccount _storageAccount = StorageAccount.CreateStorageAccount();

        CloudQueueClient queueClient;
        CloudQueue queue;

        public QueueStorage()
        {
            queueClient =
                _storageAccount.CreateCloudQueueClient();

        }

        public void SetQueueName(string queueName)
        {
            queue = queueClient.GetQueueReference(queueName.ToLower());
            queue.CreateIfNotExists();
        }

        public void SendMessage(string message)
        {
            CloudQueueMessage queueMessage = new CloudQueueMessage(message);
            queue.AddMessage(queueMessage);

        }

        public string GetMessage()
        {
            CloudQueueMessage peekedMessage = queue.PeekMessage();
            return peekedMessage.AsString;

        }

        public void UpdateMessage(string newMessge)
        {
            CloudQueueMessage message = queue.GetMessage();
            message.SetMessageContent(newMessge);
            queue.UpdateMessage(message,
                TimeSpan.FromSeconds(60.0),
                MessageUpdateFields.Content | MessageUpdateFields.Visibility);


        }

        public void DeleteMessage()
        {
            CloudQueueMessage retrievedMessage = queue.GetMessage();
            queue.DeleteMessage(retrievedMessage);
        }

        public void DeleteQueue()
        {
            queue.Delete(); 
        }
    }
}
