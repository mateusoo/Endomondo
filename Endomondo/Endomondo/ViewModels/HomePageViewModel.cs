using System;
using Endomondo.DataAccess;
using Endomondo.Models;
using Prism.Commands;
using Prism.Navigation;

namespace Endomondo.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly IJourneyRepository _journeyRepository;

        public DelegateCommand StartCommand { get; }

        public DelegateCommand NavigateToHistoryCommand { get; set; }

        public HomePageViewModel(INavigationService navigationService,
            IJourneyRepository journeyRepository) : base(navigationService)
        {
            _journeyRepository = journeyRepository;

            StartCommand = new DelegateCommand(StartAsync);
            NavigateToHistoryCommand = new DelegateCommand(NavigateToHistoryAsync);
        }

        private async void StartAsync()
        {
            await NavigationService.NavigateAsync("TrackingPage");
        }

        private async void NavigateToHistoryAsync()
        {
            await NavigationService.NavigateAsync("HistoryPage");
        }
    }
}
