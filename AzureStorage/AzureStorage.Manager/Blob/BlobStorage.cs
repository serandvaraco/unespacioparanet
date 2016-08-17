using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace AzureStorage.Manager.Blob
{
    public class BlobStorage : IBlobStorage
    {

        CloudStorageAccount _storageAccount =
            StorageAccount.CreateStorageAccount();

        CloudBlobClient blobClient;
        CloudBlobContainer container;

        public BlobStorage()
        {
            CloudBlobClient blobClient =
                _storageAccount.CreateCloudBlobClient();

        }

        public CloudBlobContainer GetContainer(string containerName)
        {
            blobClient = _storageAccount.CreateCloudBlobClient();
            container = blobClient.GetContainerReference(containerName);

            container.CreateIfNotExists();

            return container;
        }

        public void SetPermmission(BlobContainerPublicAccessType type)
        {
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = type });

        }

        public void SendFile(string pathFile, string blobReference = "blobName")
        {
            CloudBlockBlob blockBlob =
                container.GetBlockBlobReference(blobReference);

            using (var fileStream = System.IO.File.OpenRead(pathFile))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }

        public MemoryStream GetFile(string blobReference)
        {
            CloudBlockBlob blockBlob =
               container.GetBlockBlobReference(blobReference);
            MemoryStream memoryStream;

            using (memoryStream = new MemoryStream())
            {
                blockBlob.DownloadToStream(memoryStream);
            }

            return memoryStream;
        }
    }
}
