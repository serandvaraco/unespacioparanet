using AzureStorage.Manager.Blob;
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
    public partial class blobForm : Form
    {
        public blobForm()
        {
            InitializeComponent();
        }

        string fullPath;

        private void btnPreview_Click(object sender, EventArgs e)
        {
            fullPath = Helpers.FilePath();
            pbPreview.SizeMode = PictureBoxSizeMode.StretchImage; 
            pbPreview.Image = Image.FromFile(fullPath);
        }

        AzureStorage.Manager.Blob.IBlobStorage _blob = new BlobStorage();
        private void btnUpload_Click(object sender, EventArgs e)
        {

            _blob.GetContainer("imagescontainer");
            _blob.SetPermmission(Microsoft.WindowsAzure.Storage.Blob.BlobContainerPublicAccessType.Blob); 
            _blob.SendFile(fullPath, new FileInfo(fullPath).Name);

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            _blob.GetContainer("imagescontainer");
            MemoryStream memoryStream =
                _blob.GetFile(new FileInfo(fullPath).Name);

            pbBlobStorage.SizeMode = PictureBoxSizeMode.StretchImage;
            pbBlobStorage.Image = Image.FromStream(memoryStream);


        }
    }
}
