using Prism.Navigation;

namespace Endomondo.ViewModels
{
    public class HomePageViewModel
    {
        private INavigationService _navigationService;

        public HomePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
