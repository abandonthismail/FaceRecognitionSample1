using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FaceRecognitionSample1.Services
{
    /// <summary>
    /// Provides navigation management across view models using a decoupled architecture.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Gets the currently active view model.
        /// </summary>
        ObservableObject? CurrentViewModel { get; }

        /// <summary>
        /// Navigates to the specified view model type.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the target view model.</typeparam>
        void NavigateTo<TViewModel>() where TViewModel : ObservableObject;

        /// <summary>
        /// Navigates to the specified view model type with a parameter.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the target view model.</typeparam>
        /// <param name="parameter">The parameter object passed to the destination view model.</param>
        void NavigateTo<TViewModel>(object parameter) where TViewModel : ObservableObject;
    }
}