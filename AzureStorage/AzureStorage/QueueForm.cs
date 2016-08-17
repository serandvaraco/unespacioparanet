using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            queueStorage.SetQueueName("queuetest");

            InitializeComponent();
        }

        Manager.Queue.IQueueStorage queueStorage;




        
        private void btnGetQueue_Click(object sender, EventArgs e)
        {
            lstResults.Items.Add(queueStorage.GetMessage());

        }

        private void btnSendQueue_Click(object sender, EventArgs e)
        {
            queueStorage.SendMessage("Test");
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
