using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinCognitiveServices
{
	public partial class EmotionPage : ContentPage
	{
		public EmotionPage ()
		{
			InitializeComponent ();
		}
        Stream streamCopy; 
        async void btnPhoto_clicked(object sender, EventArgs e)
        {
            var useCamera = ((Button)sender).Text.Contains("cámara");

            var file = await APIServices.TakePhoto(useCamera);
            photo.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                streamCopy = new MemoryStream();
                stream.CopyTo(streamCopy);
                stream.Seek(0, SeekOrigin.Begin);
                file.Dispose();
                return stream;
            });

        }

        async void btnStart_clicked(object sender, EventArgs e)
        {
            if (streamCopy != null)
            {
                streamCopy.Seek(0, SeekOrigin.Begin);
                var emotions = await APIServices.GetEmotions(streamCopy);

                if (emotions != null)
                {
                    lblResult.Text = "---Análisis de Emociones---";
                    DisplayResult(emotions);
                }
                else lblResult.Text = "---No se detectó una cara---";
            }
            else lblResult.Text = "---No has seleccionado una imagen---";
        }

        void DisplayResult(Dictionary<string, float> emotions)
        {
            panelResultados.Children.Clear();

            foreach (var emotion in emotions)
            {
                Label lblEmocion = new Label()
                {
                    Text = emotion.Key,
                    TextColor = Color.Blue,
                    WidthRequest = 90
                };

                BoxView box = new BoxView()
                {
                    Color = Color.Lime,
                    WidthRequest = 150 * emotion.Value,
                    HeightRequest = 30,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };

                Label lblPercent = new Label()
                {
                    Text = emotion.Value.ToString("P4"),
                    TextColor = Color.Maroon
                };

                StackLayout panel = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal
                };

                panel.Children.Add(lblEmocion);
                panel.Children.Add(box);
                panel.Children.Add(lblPercent);
                panelResultados.BackgroundColor = Color.White;
                panelResultados.Children.Add(panel);
            }
        }
    }
}
