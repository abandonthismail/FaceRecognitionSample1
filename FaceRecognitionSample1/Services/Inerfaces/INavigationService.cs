using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FaceRecognitionSample1.Services
{
    public interface INavigationService
    {
        object CurrentViewModel { get; }

        void NavigateTo<TViewModel>() where TViewModel : ObservableObject;

        void NavigateTo<TViewModel>(object parameter) where TViewModel : ObservableObject;
    }
}