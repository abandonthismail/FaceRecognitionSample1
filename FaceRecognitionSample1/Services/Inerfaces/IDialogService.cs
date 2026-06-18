using System.Threading.Tasks;

namespace FaceRecognitionSample1.Services
{
    /// <summary>
    /// Defines the contract for displaying dialogs to the user,
    /// abstracting away UI-specific implementations like MessageBox.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Shows a confirmation dialog and returns true if the user confirms.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="title">The title of the dialog.</param>
        /// <returns>True if the user confirmed (e.g., clicked Yes/OK); otherwise, false.</returns>
        bool ShowConfirmation(string message, string title);

        /// <summary>
        /// Shows an informational message dialog.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="title">The title of the dialog.</param>
        void ShowInformation(string message, string title);

        /// <summary>
        /// Shows an error message dialog.
        /// </summary>
        /// <param name="message">The error message to display.</param>
        /// <param name="title">The title of the error dialog.</param>
        void ShowError(string message, string title);
    }
}