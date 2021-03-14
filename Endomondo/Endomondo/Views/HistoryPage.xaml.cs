using System.Threading.Tasks;
using Endomondo.Models;
using Endomondo.ViewModels;
using Xamarin.Forms;

namespace Endomondo.Views
{
    public partial class HistoryPage : ContentPage
    {
        private readonly HistoryPageViewModel _viewModel;

        public HistoryPage()
        {
            InitializeComponent();
            _viewModel = (HistoryPageViewModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            Task.Run(async () =>
            {
                await _viewModel.LoadJourneysAsync();
            });
        }

        private void OnItemSelection(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = e.SelectedItem as Journey;

            if (selectedItem == null)
                return;

            _viewModel.ShowJourneyResultAsync(selectedItem.Id);
        }
    }
}
