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
        private readonly IJourneyRepository _journeyRepository;

        public ICommand RemoveCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var journey = (e as Journey);
                    RemoveJourneyAsync(journey);
                });
            }
        }

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
        }

        public async Task LoadJourneysAsync()
        {
            var journeys = await _journeyRepository.GetAllAsync();

            Journeys = new ObservableCollection<Journey>(journeys);
        }

        public async void ShowJourneyResultAsync(int journeyId)
        {
            if (Journeys.FirstOrDefault(j => j.Id == journeyId) == null)
                return;

            var navigationParameters = new NavigationParameters
            {
                {"journeyId", journeyId}
            };

            await NavigationService.NavigateAsync("SummaryPage", navigationParameters);
        }

        public async void RemoveJourneyAsync(Journey journeyToRemove)
        {
            await _journeyRepository.RemoveAsync(journeyToRemove);
            Journeys.Remove(journeyToRemove);
        }
    }
}
