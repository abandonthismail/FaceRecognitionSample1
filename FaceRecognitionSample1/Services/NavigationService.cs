using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace FaceRecognitionSample1.Services
{
    public partial class NavigationService : ObservableObject, INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        private object _currentViewModel;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ObservableObject
        {
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();
            CurrentViewModel = viewModel;
        }

        public void NavigateTo<TViewModel>(object parameter) where TViewModel : ObservableObject
        {
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();
           CurrentViewModel = viewModel;
        }
    }
}
