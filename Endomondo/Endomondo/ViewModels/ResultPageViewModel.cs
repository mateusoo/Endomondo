using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace Endomondo.ViewModels
{
    public class ResultPageViewModel : ViewModelBase
    {
        public ResultPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {

        }
    }
}
