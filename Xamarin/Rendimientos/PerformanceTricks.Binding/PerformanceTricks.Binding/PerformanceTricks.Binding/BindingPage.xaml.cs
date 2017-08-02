using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PerformanceTricks.Binding
{
	
	public partial class BindingPage : ContentPage
	{
        readonly BindingModel _mainBindingModel;

        public BindingPage()
        {
            _mainBindingModel = new BindingModel();
            _mainBindingModel.Welcome = "Welcome to Xamarin Forms!!";
            this.BindingContext = _mainBindingModel;
            InitializeComponent();
            //lblText.Text = _mainBindingModel.Welcome; 
        }

        protected override void OnAppearing()
        {
            _mainBindingModel.RecopiledInformationCalculate(); 
            base.OnAppearing();
        }
    }
}