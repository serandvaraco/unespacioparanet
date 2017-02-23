using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinCognitiveServices
{
	public partial class ComputerVisioPage : ContentPage
	{
		public ComputerVisioPage ()
		{
			InitializeComponent ();
		}

        Stream streamCopy; 
        async void btnPhoto_Clicked(object sender, EventArgs e)
        {
            var useCamera = ((Button)sender).Text.Contains("cámara");

            var file = await APIServices.TakePhoto(useCamera);
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

        async void btnAnalizeImage_Clicked(object sender, EventArgs e)
        {
            if (streamCopy != null)
            {
                streamCopy.Seek(0, SeekOrigin.Begin);
                var vision = await APIServices.GetAnalisysComputerVisio(streamCopy);

                var adulto = vision.Adult;
                lblAdult.Text = String.Format("Contenido Adulto: {0} ({1})", adulto.IsAdultContent, adulto.AdultScore.ToString("P4"));
                lblRacist.Text = String.Format("Contenido Racista: {0} ({1})", adulto.IsRacyContent, adulto.RacyScore.ToString("P4"));

                var categorias = vision.Categories;
                lblCategories.Text = "Categorias: ";
                categorias.ToList().ForEach(cat => lblCategories.Text =
                        lblCategories.Text + String.Format("{0} ({1}), ", cat.Name, cat.Score.ToString("P4")));

                var color = vision.Color;
                lblColor.Text = String.Format("Accent Color: {0}\nColor dominante:\nFondo: {1}\tFrente: {2}\n¿Es Blanco y Negro? {3}\nColores dominantes: ",
                    color.AccentColor, color.DominantColorBackground,
                    color.DominantColorForeground, color.IsBWImg);
                color.DominantColors.ToList().ForEach(x => lblColor.Text = lblColor.Text + x + ", ");

                var descripcion = vision.Description;
                lblTags.Text = "Tags: ";
                lblCaptions.Text = "Captions: ";
                vision.Description.Tags.ToList().ForEach(tag => lblTags.Text = lblTags.Text + tag + ", ");
                vision.Description.Captions.ToList().ForEach(cap => lblCaptions.Text = lblCaptions.Text + String.Format("{0} ({1}), ", cap.Text, cap.Confidence.ToString("P4")));

                var caras = vision.Faces;
                lblFaces.Text = "Caras: ";
                caras.ToList().ForEach(cara => lblFaces.Text = lblFaces.Text + String.Format("{0} ({1}), ", cara.Gender, cara.Age));

                var tags = vision.Tags;
                lblTags2.Text = "Tags 2: ";
                tags.ToList().ForEach(tag => lblTags2.Text = lblTags2.Text + String.Format("{0} - {1} ({2}), ", tag.Name, tag.Hint, tag.Confidence.ToString("P4")));
            }
        }
    }
}
