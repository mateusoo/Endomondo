using Endomondo.ViewModels;
using Xamarin.Forms;

namespace Endomondo.Views
{
    public partial class SummaryPage : ContentPage
    {
        private SummaryPageViewModel _viewModel;

        public SummaryPage()
        {
            InitializeComponent();
            _viewModel = (SummaryPageViewModel) BindingContext;
        }

        protected override bool OnBackButtonPressed()
        {
            _viewModel.NavigateHomeAsync();
            return true;
        }
    }
}
