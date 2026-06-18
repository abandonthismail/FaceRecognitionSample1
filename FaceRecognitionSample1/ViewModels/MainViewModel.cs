using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FaceRecognitionSample1.Services;

namespace FaceRecognitionSample1.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        /// <summary>
        /// Gets the navigation service instance.
        /// </summary>
        public INavigationService Navigation { get; }

        public MainViewModel(INavigationService navigationService)
        {
            Navigation = navigationService;

            // Set the initial view on application startup
            Navigation.NavigateTo<FaceRecognitionViewModel>();
        }
    }
}
