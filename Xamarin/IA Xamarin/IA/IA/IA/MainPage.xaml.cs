using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IA
{

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        Stream streamCopy;
        private async void btnCamera_Clicked(object sender, EventArgs e)
        {
            var file = await API_IA.TakePhoto(true);
            imgPhoto.Source = ImageSource.FromStream(() =>
            {

                var stream = file.GetStream();
                streamCopy = new MemoryStream();
                stream.CopyTo(streamCopy);
                stream.Seek(0, SeekOrigin.Begin);
                file.Dispose();
                return stream;
            });
        }
        private void btnStartVision_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = API_IA.GetAnalisysComputerVisio(streamCopy);
                StringBuilder sb = new StringBuilder();
                foreach (var item in result.Description?.Captions)
                    sb.Append(item);

                lblResults.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                lblResults.Text = ex.Message;
            }
            finally
            {
                progressBar.IsVisible = false;
            }
        }




        private async void btnStart_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await API_IA.GetEmotionsAPI(streamCopy);

                StringBuilder sb = new StringBuilder();
                foreach (var item in result.Values)
                    sb.Append(item);

                lblResults.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                lblResults.Text = ex.Message;
            }
            finally
            {
                progressBar.IsVisible = false;
            }
        }


    }
}
