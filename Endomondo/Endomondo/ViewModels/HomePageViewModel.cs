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

        public override async void Initialize(INavigationParameters parameters)
        {
            //TEST
            //await _journeyRepository.AddAsync(new Journey() { Duration = 30, DateTime = DateTime.Now});

            //var x = await _journeyRepository.GetAsync(1);

            //var y = await _journeyRepository.GetAllAsync();
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
