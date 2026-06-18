using System;

namespace FaceRecognitionSample1.Services
{
    /// <summary>
    /// Abstracts the UI thread dispatcher to maintain ViewModel testability 
    /// without directly referencing System.Windows.
    /// </summary>
    public interface IDispatcherService
    {
        /// <summary>
        /// Executes the specified action synchronously on the UI thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        void Invoke(Action action);
    }
}