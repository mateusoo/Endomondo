using System;
using Endomondo.DataAccess;
using Endomondo.Models;
using Prism.Commands;
using Prism.Navigation;

namespace Endomondo.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly IRouteRepository _routeRepository;

        public DelegateCommand StartCommand { get; }

        public DelegateCommand NavigateToHistoryCommand { get; set; }

        public HomePageViewModel(INavigationService navigationService,
            IRouteRepository routeRepository) : base(navigationService)
        {
            _routeRepository = routeRepository;

            StartCommand = new DelegateCommand(StartAsync);
            NavigateToHistoryCommand = new DelegateCommand(NavigateToHistoryAsync);
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            //TEST
            //await _routeRepository.AddAsync(new Route() { Duration = 30, Date = DateTime.Today});

            //var x = await _routeRepository.GetAsync(1);

            //var y = await _routeRepository.GetAllAsync();
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
