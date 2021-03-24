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

        private int _hours;

        public int Hours
        {
            get => _hours;
            set
            {
                _hours = value;
                RaisePropertyChanged("Hours");
            }
        }

        private int _minutes;

        public int Minutes
        {
            get => _minutes;
            set
            {
                _minutes = value;
                RaisePropertyChanged("Minutes");
            }
        }

        private int _seconds;

        public int Seconds
        {
            get => _seconds;
            set
            {
                _seconds = value;
                RaisePropertyChanged("Seconds");
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

            Hours = Journey.DurationTimeSpan.Hours;
            Minutes = Journey.DurationTimeSpan.Minutes;
            Seconds = Journey.DurationTimeSpan.Seconds;
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
