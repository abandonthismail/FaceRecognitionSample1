using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FaceRecognitionSample1.ViewModels
{
    public partial class MatchCandidate : ObservableObject
    {
        [ObservableProperty]
        private string _rankText; // e.g., "2nd", "3rd" or "2位", "3位"

        [ObservableProperty]
        private BitmapSource _image;

        [ObservableProperty]
        private double _score;
    }

    public partial class OneToManyViewModel : ObservableObject
    {
        [ObservableProperty]
        private BitmapSource _queryImage;

        [ObservableProperty]
        private BitmapSource _topCandidateImage;

        [ObservableProperty]
        private double? _score;

        [ObservableProperty]
        private bool? _isMatch;

        // Collection for 2nd to 5th candidates
        public ObservableCollection<MatchCandidate> SubCandidates { get; } = new();

        public OneToManyViewModel()
        {
            // Temporary mock data for UI preview
            // In production, this will be cleared and populated dynamically
            SubCandidates.Add(new MatchCandidate { RankText = FaceRecognitionSample1.Properties.Resources.Rank2, Score = 0.8931 });
            SubCandidates.Add(new MatchCandidate { RankText = FaceRecognitionSample1.Properties.Resources.Rank3, Score = 0.8127 });
            SubCandidates.Add(new MatchCandidate { RankText = FaceRecognitionSample1.Properties.Resources.Rank4, Score = 0.7423 });
            SubCandidates.Add(new MatchCandidate { RankText = FaceRecognitionSample1.Properties.Resources.Rank5, Score = 0.6894 });
        }
    }
}
