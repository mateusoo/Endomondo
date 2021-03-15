using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Endomondo.DataAccess;
using Endomondo.Models;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace Endomondo.ViewModels
{
    public class HistoryPageViewModel : ViewModelBase
    {
        public ICommand Command { get; set; }

        private readonly IJourneyRepository _journeyRepository;

        private ObservableCollection<Journey> _journeys;

        public ObservableCollection<Journey> Journeys
        {
            get => _journeys;
            set
            {
                _journeys = value;
                RaisePropertyChanged("Journeys");
            }
        }

        public HistoryPageViewModel(INavigationService navigationService,
            IJourneyRepository journeyRepository)
            : base(navigationService)
        {
            _journeyRepository = journeyRepository;
            Command = new Command(Execute);
        }

        public async void Execute()
        {
            await NavigationService.NavigateAsync("TrackingPage");

        }

        public async Task LoadJourneysAsync()
        {
            var journeys = await _journeyRepository.GetAllAsync();

            Journeys = new ObservableCollection<Journey>(journeys);
        }

        public async void ShowJourneyResultAsync(int journeyId)
        {
            var navigationParameters = new NavigationParameters
            {
                {"journeyId", journeyId}
            };

            await NavigationService.NavigateAsync("ResultPage", navigationParameters);
        }
    }
}
