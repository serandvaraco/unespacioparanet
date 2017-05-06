using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinCognitiveServices
{
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		}


        void btnEmotion_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EmotionPage());
        }

        void btnVision_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ComputerVisioPage());
        }
    }
}
