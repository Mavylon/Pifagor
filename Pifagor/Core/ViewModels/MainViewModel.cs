using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pifagor.Core.CustomElements;
using Pifagor.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Pifagor.Core.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {

        public MainViewModel(NavigationService navigationService, IServiceProvider serviceProvider) : base(navigationService,serviceProvider)
        {
            Navigation.NavigateTo<ProgramStartViewModel>();
        }

        [RelayCommand]
        public void NavigateTo(Type viewModel)
        {
            Navigation.NavigateTo(viewModel);
        }
    }
}
