using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Endomondo.DataAccess;
using Endomondo.Models;
using Prism.Navigation;

namespace Endomondo.ViewModels
{
    public class SummaryPageViewModel : ViewModelBase
    {
        private readonly IJourneyRepository _journeyRepository;

        public DelegateCommand ShowRouteCommand { get; set; }

        private Journey _journey;

        public Journey Journey
        {
            get => _journey;
            set
            {
                _journey = value;
                RaisePropertyChanged("Journey");
            }
        }

        public SummaryPageViewModel(INavigationService navigationService, 
            IJourneyRepository journeyRepository)
            : base(navigationService)
        {
            _journeyRepository = journeyRepository;

            ShowRouteCommand = new DelegateCommand(ShowRouteAsync);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var journeyId = parameters.GetValue<int>("journeyId");

            Journey = await _journeyRepository.GetAsync(journeyId);
        }

        public async void NavigateHomeAsync()
        {
            await NavigationService.NavigateAsync("BottomNavigationPage");
        }

        private async void ShowRouteAsync()
        {
            var navigationParameters = new NavigationParameters
            {
                {"journeyId", Journey.Id}
            };

            await NavigationService.NavigateAsync("ResultPage", navigationParameters);
        }
    }
}
