using Prism.Commands;
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
        private readonly IStepCounter _stepCounter;
        private readonly IJourneyRepository _journeyRepository;
        private bool _isBackgroundTaskRunning;
        private int? _initialNumberOfSteps;
        private readonly Timer _timer;

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

        private double _speed;

        public double Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                RaisePropertyChanged("Speed");
            }
        }

        private int _numberOfSteps;

        public int NumberOfSteps
        {
            get => _numberOfSteps;
            set
            {
                _numberOfSteps = value;
                RaisePropertyChanged("NumberOfSteps");
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

        public TrackingPageViewModel(INavigationService navigationService, IAlarm alarm,
            IStepCounter stepCounter, IJourneyRepository journeyRepository)
            : base(navigationService)
        {
            _alarm = alarm;
            _stepCounter = stepCounter;
            _journeyRepository = journeyRepository;
            _isBackgroundTaskRunning = true;

            StopCommand = new DelegateCommand(StopAsync);
            _timer = new Timer();

            MessagingCenter.Subscribe<LocationMessage>(this, nameof(LocationMessage),
                message =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    HandleNewLocationAsync(message);
                });
            });

            if (_stepCounter.IsAvailable())
            {
                _stepCounter.StepCountChanged += StepCounter_StepCountChanged;
                _stepCounter.Start();
            }
        }
        
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            StartTimer();

            Journey = new Journey(DateTime.Now);

            await _journeyRepository.AddAsync(Journey);

            _alarm.SetAlarmForBackgroundServices(0);
        }

        private async void StopAsync()
        {
            if (Journey.Locations.Count == 0)
                return;

            _timer.Stop();
            _stepCounter.Stop();
            _isBackgroundTaskRunning = false;

            Journey.DurationTimeSpan = _timer.Duration;
            Journey.MaxSpeed = Journey.Locations.Max(l => l.Speed);
            Journey.AverageSpeed = Journey.Distance / Journey.DurationTimeSpan.TotalSeconds;
            Journey.NumberOfSteps = NumberOfSteps;

            await _journeyRepository.UpdateAsync(Journey);

            var navigationParameters = new NavigationParameters
            {
                {"journeyId", Journey.Id}
            };

            await NavigationService.NavigateAsync("SummaryPage", navigationParameters);
        }

        private async Task HandleNewLocationAsync(LocationMessage locationMessage)
        {
            if (!_isBackgroundTaskRunning)
                return;

            var distanceFromLatestLocation =
                CalculateDistanceFromLatestLocation(locationMessage.Latitude, locationMessage.Longitude);

            if (Journey.Locations.Count == 0 || distanceFromLatestLocation > 4)
            {
                var location = new Location(locationMessage.Latitude,
                    locationMessage.Longitude, locationMessage.WriteTime);

                location.Speed = CalculateSpeedFromLatestLocation(distanceFromLatestLocation, location.WriteTime);

                Journey.Locations.Add(location);

                Journey.Distance += distanceFromLatestLocation;
                Distance = Journey.Distance;

                Journey.AverageSpeed = Journey.Distance / Journey.Duration;
                Speed = Journey.AverageSpeed;

                Journey.NumberOfSteps = NumberOfSteps;

                await _journeyRepository.UpdateAsync(Journey);
            }

            _alarm.SetAlarmForBackgroundServices(DelayTimeInSeconds);
        }

        private double CalculateDistanceFromLatestLocation(double latitude, double longitude)
        {
            var latestLocation = Journey.Locations
                .OrderByDescending(l => l.WriteTime)
                .FirstOrDefault();

            if (latestLocation == null)
                return 0;

            var distance = Xamarin.Essentials.Location.CalculateDistance(latestLocation.Latitude,
                latestLocation.Longitude, latitude,
                longitude, DistanceUnits.Kilometers) * 1000;

            return Math.Round(distance, 2);
        }

        private double CalculateSpeedFromLatestLocation(double distance, DateTime writeTime)
        {
            var latestLocation = Journey.Locations
                .OrderByDescending(l => l.WriteTime)
                .FirstOrDefault();

            if (latestLocation == null)
                return 0;

            var speed = distance / (writeTime - latestLocation.WriteTime).TotalSeconds;

            return Math.Round(speed, 2);
        }

        private void StartTimer()
        {
            _timer.Start();
            _timer.Ticked += OnCountdownTicked;
        }

        private void OnCountdownTicked()
        {
            Seconds = _timer.Duration.Seconds;
            Hours = _timer.Duration.Hours;
            Minutes = _timer.Duration.Minutes;

            Journey.DurationTimeSpan = _timer.Duration;
        }

        private void StepCounter_StepCountChanged(object sender, StepCountChangedEventArgs e)
        {
            if (e.Value == null)
                return;

            if (_initialNumberOfSteps == null)
            {
                _initialNumberOfSteps = (int)e.Value;
            }

            NumberOfSteps = (int)e.Value - (int)_initialNumberOfSteps;
        }

        //private async void CalculateDistanceForExistingJourneys()
        //{
        //    var journeys = await _journeyRepository.GetAllWithLocationsAsync();

        //    try
        //    {

        //        foreach (var journey in journeys)
        //        {
        //            journey.Distance = 0;

        //            for (int i = 1; i < journey.Locations.Count; i++)
        //            {
        //                var previousLocation = journey.Locations[i - 1];
        //                var location = journey.Locations[i];

        //                var distance = Xamarin.Essentials.Location.CalculateDistance(previousLocation.Latitude,
        //                    previousLocation.Longitude, location.Latitude,
        //                    location.Longitude, DistanceUnits.Kilometers) * 1000;

        //                location.Speed = distance / (location.WriteTime - previousLocation.WriteTime).TotalSeconds;

        //                if (distance > 4)
        //                {
        //                    journey.Distance += distance;
        //                }
        //            }

        //            if (journey.Locations.Count > 1)
        //            {
        //                journey.DurationTimeSpan = journey.Locations.Last().WriteTime - journey.Locations.First().WriteTime;
        //            }

        //            journey.Distance = Math.Round(journey.Distance, 2);
        //            journey.MaxSpeed = journey.Locations.Max(l => l.Speed);
        //            journey.AverageSpeed = journey.Distance / journey.DurationTimeSpan.TotalSeconds;

        //            await _journeyRepository.UpdateAsync(journey);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}
    }
}
