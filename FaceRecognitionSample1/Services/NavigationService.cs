using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FaceRecognitionSample1.Services
{/// <summary>
 /// Implements navigation by invoking a factory delegate to resolve view models,
 /// avoiding direct dependency on the DI container.
 /// </summary>
    public partial class NavigationService : ObservableObject, INavigationService
    {
        private readonly Func<Type, ObservableObject> _viewModelFactory;

        /// <summary>
        /// Backing field for CurrentViewModel, automatically generating the public property.
        /// </summary>
        [ObservableProperty]
        private ObservableObject? _currentViewModel;

        /// <summary>
        /// Initializes a new instance of the NavigationService.
        /// </summary>
        /// <param name="viewModelFactory">A delegate that resolves a view model instance given its type.</param>
        public NavigationService(Func<Type, ObservableObject> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));
        }

        /// <summary>
        /// Resolves the requested ViewModel via the factory and updates CurrentViewModel.
        /// </summary>
        public void NavigateTo<TViewModel>() where TViewModel : ObservableObject
        {
            CurrentViewModel = _viewModelFactory(typeof(TViewModel));
        }

        /// <summary>
        /// Resolves the requested ViewModel via the factory and passes a navigation parameter.
        /// </summary>
        public void NavigateTo<TViewModel>(object parameter) where TViewModel : ObservableObject
        {
            var viewModel = _viewModelFactory(typeof(TViewModel));

            // OPTION: Handle parameter passing if the resolved view model supports initialization.
            // Example: if (viewModel is INavigationAware aware) { aware.OnNavigatedTo(parameter); }

            CurrentViewModel = viewModel;
        }
    }
}