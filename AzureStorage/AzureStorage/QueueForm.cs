using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureStorage
{
    public partial class QueueForm : Form
    {
        public QueueForm()
        {
            queueStorage = new Manager.Queue.QueueStorage();
            queueStorage.SetQueueName("queuetestItTalent");


            blobStorage = new Manager.Blob.BlobStorage();
            blobStorage.GetContainer("micontenedor");
            blobStorage.SetPermmission(Microsoft.WindowsAzure.Storage.Blob.BlobContainerPublicAccessType.Blob);

            var _valueSetPolicy = blobStorage.SetPoliciesPermission("policyTestItTalent");
            var _valuePolicy = blobStorage.GetPoliciesPermission("policyTestItTalent");
            

            blobStorage.SendFile(Directory.GetFiles(@"C:\Users\Administrador\Downloads\ZoomIt")[0]); 
            
            InitializeComponent();
        }

        Manager.Queue.IQueueStorage queueStorage;
        Manager.Blob.BlobStorage blobStorage; 




        private void btnGetQueue_Click(object sender, EventArgs e)
        {
            lstResults.Items.Add(queueStorage.GetMessage());

        }

        int pos = 0;
        private void btnSendQueue_Click(object sender, EventArgs e)
        {
            queueStorage.SendMessage("Test" + pos++);
        }

        private void btnUpdateQueue_Click(object sender, EventArgs e)
        {
            queueStorage.UpdateMessage("Updated!!!");

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            queueStorage.DeleteMessage();
        }
    }
}
