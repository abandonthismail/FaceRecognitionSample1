using System;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FaceRecognitionSample1.ViewModels
{
    public partial class UserItemViewModel : ObservableObject
    {
        private readonly Action _onSelectionChanged;

        // ▢ チェックボックスの選択状態
        [ObservableProperty]
        private bool _isSelected;

        // 選択状態が変わったら、親ViewModelに通知して削除ボタンの活性状態などを更新する
        partial void OnIsSelectedChanged(bool value)
        {
            _onSelectionChanged?.Invoke();
        }

        // ユーザー情報プロパティ
        public string UserId { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public ImageSource? FaceThumbnail { get; init; } // 顔写真の画像データ

        public UserItemViewModel(Action onSelectionChanged)
        {
            _onSelectionChanged = onSelectionChanged;
        }
    }
}