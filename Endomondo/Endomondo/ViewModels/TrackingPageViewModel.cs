using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace Endomondo.ViewModels
{
    public class TrackingPageViewModel : ViewModelBase
    {
        public DelegateCommand StopCommand { get; }

        public TrackingPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            StopCommand = new DelegateCommand(StopAsync);
        }

        private async void StopAsync()
        {
            await NavigationService.NavigateAsync("ResultPage");
        }
    }
}
