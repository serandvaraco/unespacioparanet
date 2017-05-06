using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureStorage
{
    public class Helpers
    {

        public static string FilePath()
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Archivos de Imagenes (*.png) |*.png";
            dialog.Title = "Serleccione la Imagen a subir";
            dialog.Multiselect = false;
            DialogResult dialogResult = dialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {

                return dialog.FileName;
            }

            return string.Empty;

        }

    }
}
