﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Endomondo.DataAccess;
using Endomondo.Infrastructure;
using Endomondo.Messages;
using Endomondo.Models;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Location = Endomondo.Models.Location;

namespace Endomondo.ViewModels
{
    public class TrackingPageViewModel : ViewModelBase
    {
        private const int DelayTimeInSeconds = 20;

        private readonly IAlarm _alarm;
        private readonly IJourneyRepository _journeyRepository;
        private bool _isBackgroundTaskRunning;

        public ObservableCollection<TestModel> TestData { get; set; }

        public Journey Journey { get; set; }

        public DelegateCommand StopCommand { get; }

        private double _distance;

        public double Distance
        {
            get => _distance;
            set
            {
                _distance = value;
                RaisePropertyChanged("Distance");
            }
        }

        public TrackingPageViewModel(INavigationService navigationService, IAlarm alarm,
            IJourneyRepository journeyRepository)
            : base(navigationService)
        {
            _alarm = alarm;
            _journeyRepository = journeyRepository;
            _isBackgroundTaskRunning = true;

            StopCommand = new DelegateCommand(StopAsync);
            TestData = new ObservableCollection<TestModel>();

            MessagingCenter.Subscribe<LocationMessage>(this, nameof(LocationMessage),
                message =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    HandleNewLocationAsync(message);
                });
            });
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Journey = new Journey(DateTime.Now);

            await _journeyRepository.AddAsync(Journey);

            _alarm.SetAlarmForBackgroundServices(0);
        }

        private async void StopAsync()
        {
            _isBackgroundTaskRunning = false;

            var navigationParameters = new NavigationParameters
            {
                {"journeyId", Journey.Id}
            };

            await NavigationService.NavigateAsync("ResultPage", navigationParameters);
        }

        private async Task HandleNewLocationAsync(LocationMessage locationMessage)
        {
            if (!_isBackgroundTaskRunning)
                return;

            Journey.Locations.Add(new Location(locationMessage.Latitude,
                locationMessage.Longitude, locationMessage.WriteTime));

            var distanceFromLatestLocation =
                CalculateDistanceFromLatestLocation(locationMessage.Latitude, locationMessage.Longitude);

            if (Journey.Locations.Count == 1 || distanceFromLatestLocation > 4)
            {
                Journey.Distance += distanceFromLatestLocation;
                Distance = Journey.Distance;

                await _journeyRepository.UpdateAsync(Journey);
            }

            var testText = Journey.Id + " | " + Math.Round(distanceFromLatestLocation, 2) + " | " + locationMessage.WriteTime + " | "
                           + locationMessage.Latitude + " | " + locationMessage.Longitude;
            TestData.Add(new TestModel() { Text = testText });

            _alarm.SetAlarmForBackgroundServices(DelayTimeInSeconds);
        }

        private double CalculateDistanceFromLatestLocation(double latitude, double longitude)
        {
            var latestLocation = Journey.Locations.OrderBy(l => l.WriteTime).FirstOrDefault();

            if (latestLocation == null)
                return 0;

            return Xamarin.Essentials.Location.CalculateDistance(latestLocation.Latitude,
                latestLocation.Longitude, latitude,
                longitude, DistanceUnits.Kilometers) * 1000;
        }

        //private async void CalculateDistanceForExistingJourneys()
        //{
        //    var journeys = await _journeyRepository.GetAllWithLocationsAsync();

        //    foreach (var journey in journeys)
        //    {
        //        journey.Distance = 0;

        //        for (int i = 1; i < journey.Locations.Count; i++)
        //        {
        //            var previousLocation = journey.Locations[i - 1];
        //            var location = journey.Locations[i];

        //            var distance = Xamarin.Essentials.Location.CalculateDistance(previousLocation.Latitude,
        //                previousLocation.Longitude, location.Latitude,
        //                location.Longitude, DistanceUnits.Kilometers) * 1000;

        //            if (distance > 4)
        //            {
        //                journey.Distance += distance;
        //            }
        //        }

        //        journey.Distance = Math.Round(journey.Distance, 2);
        //        await _journeyRepository.UpdateAsync(journey);
        //    }
        //}
    }
}
