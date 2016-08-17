using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureStorage.Manager.Blob
{
    public interface IBlobStorage
    {
        CloudBlobContainer GetContainer(string containerName);
        MemoryStream GetFile(string blobReference);
        void SendFile(string pathFile, string blobReference = "blobName");
        void SetPermmission(BlobContainerPublicAccessType type);
    }
}