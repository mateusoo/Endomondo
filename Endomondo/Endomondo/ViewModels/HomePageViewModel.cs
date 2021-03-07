using System;
using Endomondo.DataAccess;
using Endomondo.Models;
using Prism.Navigation;

namespace Endomondo.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private IRouteRepository _routeRepository;

        public HomePageViewModel(INavigationService navigationService, 
            IRouteRepository routeRepository) : base(navigationService)
        { 
            _routeRepository = routeRepository;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            //TEST
            //await _routeRepository.AddAsync(new Route() {Duration = 30, Date = DateTime.Today});

            //var x = await _routeRepository.GetAsync(1);

            //var y = await _routeRepository.GetAllAsync();
        }
    }
}
