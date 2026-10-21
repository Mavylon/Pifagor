using CommunityToolkit.Mvvm.ComponentModel;
using Pifagor.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Pifagor.Core.Services
{
    public class NavigationService : ObservableObject
    {
        private object? _currentView;
        private IServiceProvider _serviceProvider;

        public object? CurrentView {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TViewModel>() where TViewModel : class {
            CurrentView = _serviceProvider.GetService(typeof(TViewModel));
        }

        public void NavigateTo(Type viewModel)
        {
            CurrentView = _serviceProvider.GetService(viewModel);
        }

        public void NavigateTo<TViewModel, TParam>(TParam param) where TViewModel : class
        {
            var factory = _serviceProvider.GetService(typeof(Func<TParam, TViewModel>)) as Func<TParam, TViewModel>;

            if (factory != null) 
            {
                CurrentView = factory(param);
            } 
            else 
            {
                NavigateTo<TViewModel>();
            }
        }
    }
}
