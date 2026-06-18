using System.Windows;

namespace FaceRecognitionSample1.Services
{
    /// <summary>
    /// WPF-specific implementation of the dialog service using MessageBox.
    /// </summary>
    public class DialogService : IDialogService
    {
        public bool ShowConfirmation(string message, string title)
        {
            var result = MessageBox.Show(
                message,
                title,
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            return result == MessageBoxResult.Yes;
        }

        public void ShowInformation(string message, string title)
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        public void ShowError(string message, string title)
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

    }
}