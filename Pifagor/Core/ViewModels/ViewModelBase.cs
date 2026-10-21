using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Pifagor.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pifagor.Core.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        public NavigationService Navigation { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
        public ViewModelBase(NavigationService navigationService, IServiceProvider serviceProvider)
        {
            Navigation = navigationService;
            ServiceProvider = serviceProvider;
        }
    }
}
