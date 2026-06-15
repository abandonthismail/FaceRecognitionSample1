using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FaceRecognitionSample1.Infrastructure;
using FaceRecognitionSample1.Models;
using FaceRecognitionSample1.Services;
using FaceRecognitionSample1.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace FaceRecognitionSample1.ViewModels
{
    public enum RecognitionMode
    {
        OneToOne,   // 1:1
        OneToMany   // 1:N
    }
    public enum RecognitionOperationalMode
    {
        Trigger,    // Manual
        RealTime    // Automatic
    }

    public record MetricItem(string Name, string Value);

    public partial class FaceRecognitionViewModel : ObservableObject
    {
        private readonly ICameraProvider _cameraProvider;
        private readonly IFaceRecognitionProvider _faceRecognitionProvider;
        private readonly ISettingsService _settingsService;
        private readonly INavigationService _navigationService;

        // Hold references to child ViewModels to prevent re-creation
        private readonly OneToOneViewModel _oneToOneViewModel;
        private readonly OneToManyViewModel _oneToManyViewModel;

        // Current active ViewModel rendered in ContentControl
        [ObservableProperty]
        private object _currentResultViewModel;

        // Current selected recognition mode bounded to RadioButtons
        [ObservableProperty]
        private RecognitionMode _selectedMode;

        // Current selected recognition operation mode bounded to RadioButtons
        [ObservableProperty]
        private RecognitionOperationalMode _selectedRecognitionMode = RecognitionOperationalMode.Trigger;

        // Camera live streaming
        [ObservableProperty]
        private BitmapSource _cameraFrame;

        // Current text in the search box
        [ObservableProperty]
        private string _searchText;

        // Current attribute results
        [ObservableProperty]
        private FaceAttributeResults? _currentAttricutes;

        public ObservableCollection<MetricItem> VisibleMetrics { get; } = new();

        private readonly List<MetricDefinition> _metricRegistry = new()
        {
            new("Mask",   s => s.ShowMask,     r => r.MaskScore?.ToString("F6")),
            new("Smile",  s => s.ShowSmile,    r => r.SmileScore?.ToString("F6"))
        };

        public ISettingsService Settings { get; }

        [RelayCommand]
        private void OpenUserManagement()
        {
            // TODO: ユーザ管理画面（別ウィンドウやダイアログ）を開く処理
            System.Windows.MessageBox.Show(FaceRecognitionSample1.Properties.Resources.UserManagementOpenMessage);
        }

        [RelayCommand]
        private void OpenSettings()
        {
            // TODO: 設定画面を開く処理
            System.Windows.MessageBox.Show(FaceRecognitionSample1.Properties.Resources.SettingsOpenMessage);
        }

        public FaceRecognitionViewModel(
            ICameraProvider cameraProvider,
            IFaceRecognitionProvider faceRecognitionProvider,
            ISettingsService settingsService,
            INavigationService navigationService,
            OneToOneViewModel oneToOneViewModel,
            OneToManyViewModel oneToManyViewModel)
        {
            // Inject dependencies
            _cameraProvider = cameraProvider;
            _faceRecognitionProvider = faceRecognitionProvider;
            _navigationService = navigationService;
            _settingsService = settingsService;

            _oneToOneViewModel = oneToOneViewModel;
            _oneToManyViewModel = oneToManyViewModel;

            // Set default mode and initialize the view explicitly
            _selectedMode = RecognitionMode.OneToOne;
            _currentResultViewModel = _oneToOneViewModel;

            Settings = settingsService;

            // TODO: Remvoe
            FaceAttributeResults results = new FaceAttributeResults(
                0.1234567, 0.098765, 0.85f, 0.65f, 10f
            );

            OnAttributeAnalyzed(results);
        }

        // Automatically invoked by CommunityToolkit.Mvvm when SelectedMode changes
        partial void OnSelectedModeChanged(RecognitionMode value)
        {
            if (value == RecognitionMode.OneToOne)
            {
                CurrentResultViewModel = _oneToOneViewModel;
            }
            else
            {
                CurrentResultViewModel = _oneToManyViewModel;
            }
        }

        private void OnAttributeAnalyzed(FaceAttributeResults result)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                VisibleMetrics.Clear();

                var activeItems = _metricRegistry
                    .Where(def => def.CheckSetting(_settingsService))
                    .Select(def => new { def.DisplayName, ValueText = def.GetValueText(result) })
                    .Where(x => x.ValueText != null)
                    .Select(x => new MetricItem(x.DisplayName, x.ValueText!));

                foreach (var item in activeItems)
                {
                    VisibleMetrics.Add(item);
                }
            });
        }

    }
}
