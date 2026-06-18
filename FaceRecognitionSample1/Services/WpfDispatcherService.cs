using System;
using System.Windows;

namespace FaceRecognitionSample1.Services
{
    /// <summary>
    /// WPF-specific implementation of the dispatcher service.
    /// </summary>
    public class WpfDispatcherService : IDispatcherService
    {
        public void Invoke(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}