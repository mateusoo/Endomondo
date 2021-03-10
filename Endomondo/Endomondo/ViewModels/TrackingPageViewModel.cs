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
using Xamarin.Forms;

namespace Endomondo.ViewModels
{
    public class TrackingPageViewModel : ViewModelBase
    {
        private readonly IAlarm _alarm;
        private readonly IJourneyRepository _journeyRepository;
        private bool _isBackgroundTaskRunning;

        public ObservableCollection<TestModel> TestData { get; set; }

        public Journey Journey { get; set; }

        public DelegateCommand StopCommand { get; }

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

            await _journeyRepository.UpdateAsync(Journey);

            var testText = locationMessage.WriteTime + "  | " + locationMessage.Latitude
                           + " | " + locationMessage.Longitude;
            TestData.Add(new TestModel() { Text = testText });

            _alarm.SetAlarmForBackgroundServices(10);
        }
    }
}
