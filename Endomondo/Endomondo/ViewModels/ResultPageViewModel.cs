using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Endomondo.DataAccess;
using Endomondo.Models;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Endomondo.ViewModels
{
    public class ResultPageViewModel : ViewModelBase
    {
        private readonly IJourneyRepository _journeyRepository;

        public Polyline Polyline { get; set; } = new Polyline();

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

            var locations = journey.Locations
                .Select(l => new Position(l.Latitude, l.Longitude))
                .ToList();

            Polyline = new Polyline
            {
                StrokeColor = Color.Coral,
                StrokeWidth = 12
            };

            foreach (var location in locations)
            {
                Polyline.Geopath.Add(location);
            }

            RaisePropertyChanged("Polyline");
        }

        public async void NavigateHomeAsync()
        {
            await NavigationService.NavigateAsync("HomePage");
        }
    }
}
