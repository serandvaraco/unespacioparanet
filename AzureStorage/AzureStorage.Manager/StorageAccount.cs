using Microsoft.WindowsAzure.Storage;
using System.Configuration;

namespace AzureStorage.Manager
{
    public class StorageAccount
    {
        public static CloudStorageAccount CreateStorageAccount()
        {

            CloudStorageAccount _storageAccount = null;

            if (CloudStorageAccount.TryParse(
                ConfigurationManager.AppSettings["CloudStorageString"],
               out _storageAccount))

                return _storageAccount;

            throw new StorageException("Cuentas Invalidas");

        }

       

    }
}
