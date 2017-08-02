using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PerformanceTricks.List
{
    public partial class MainPage : ContentPage
	{

        private readonly MainPageViewModel _viewModel;

        public MainPage()
		{
			InitializeComponent();
            _viewModel = new MainPageViewModel();
            this.BindingContext = _viewModel; 

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnAppearingAsync();
        }
    }
}
