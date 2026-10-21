using CommunityToolkit.Mvvm.Input;
using Pifagor.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pifagor.Core.ViewModels.Menu
{
    public partial class DiscreteMathMenuViewModel : ViewModelBase
    {
        public DiscreteMathMenuViewModel(NavigationService navigationService, IServiceProvider serviceProvider) : base(navigationService, serviceProvider) { }

        [RelayCommand]
        public void NavigateTo(Type viewModel)
        {
            Navigation.NavigateTo(viewModel);
        }
    }
}
