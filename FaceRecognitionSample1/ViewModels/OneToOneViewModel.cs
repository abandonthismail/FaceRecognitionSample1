using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FaceRecognitionSample1.ViewModels
{
    public partial class OneToOneViewModel : ObservableObject
    {
        [ObservableProperty]
        private BitmapSource _queryImage;

        [ObservableProperty]
        private BitmapSource _targetImage;

        [ObservableProperty]
        private double? _score;

        [ObservableProperty]
        private bool? _isMatch; // true: Match(Green), false: Mismatch(Red), null: None(-)
    }
}
