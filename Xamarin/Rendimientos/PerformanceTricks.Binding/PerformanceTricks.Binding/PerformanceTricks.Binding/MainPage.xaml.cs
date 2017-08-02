using Xamarin.Forms;

namespace PerformanceTricks.Binding
{
	public partial class MainPage : ContentPage
	{
        readonly MainPageBinding _mainPageBinding;
        public MainPage()
		{
            _mainPageBinding = new MainPageBinding();
            this.BindingContext = _mainPageBinding; 
            InitializeComponent();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _mainPageBinding.RecopiledInformationCalculate();
        }
    }
}
