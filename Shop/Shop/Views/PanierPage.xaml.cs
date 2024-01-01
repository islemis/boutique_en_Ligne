using Xamarin.Forms;
using Shop.ViewModels;

namespace Shop.Views
{
    public partial class PanierPage : ContentPage
    {
      public readonly PanierViewModel _viewModel ;
        public PanierPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new PanierViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
