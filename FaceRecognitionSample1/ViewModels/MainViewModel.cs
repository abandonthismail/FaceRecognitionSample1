using CommunityToolkit.Mvvm.ComponentModel;
using FaceRecognitionSample1.Services;

namespace FaceRecognitionSample1.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public INavigationService Navigation { get; }

        public MainViewModel(INavigationService navigationService)
        {
            Navigation = navigationService;
            Navigation.NavigateTo<FaceRecognitionViewModel>();
        }
    }
}
