using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Endomondo.DataAccess;
using Endomondo.Models;
using Prism.Navigation;

namespace Endomondo.ViewModels
{
    public class ResultPageViewModel : ViewModelBase
    {
        private readonly IJourneyRepository _journeyRepository;

        public ObservableCollection<TestModel> Locations { get; set; }
            = new ObservableCollection<TestModel>();

        public ResultPageViewModel(INavigationService navigationService,
            IJourneyRepository journeyRepository)
            : base(navigationService)
        {
            _journeyRepository = journeyRepository;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var journeyId = parameters.GetValue<int>("journeyId");

            var journey = await _journeyRepository.GetAsync(journeyId);

            foreach (var location in journey.Locations)
            {
                Locations.Add(new TestModel() {Text = location.ToString()});
            }
        }
    }
}
