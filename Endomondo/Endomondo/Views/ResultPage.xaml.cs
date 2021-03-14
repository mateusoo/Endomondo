using Endomondo.ViewModels;
using Xamarin.Forms;

namespace Endomondo.Views
{
    public partial class ResultPage : ContentPage
    {
        private ResultPageViewModel _viewModel;

        public ResultPage()
        {
            InitializeComponent();
            _viewModel = (ResultPageViewModel)BindingContext;
        }

        protected override bool OnBackButtonPressed()
        {
            _viewModel.NavigateHomeAsync();
            return true;
        }
    }
}
