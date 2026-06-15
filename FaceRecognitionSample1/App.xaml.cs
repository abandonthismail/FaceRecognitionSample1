using FaceRecognitionSample1.Services;
using FaceRecognitionSample1.Services.Interfaces;
using FaceRecognitionSample1.Services.Mocks;
using FaceRecognitionSample1.ViewModels;
using FaceRecognitionSample1.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace FaceRecognitionSample1
{
    public partial class App : Application
    {
        public IServiceProvider? ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Register global exception handlers to surface runtime errors
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            try
            {
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);
                ServiceProvider = serviceCollection.BuildServiceProvider();

                // Resolve MainWindow and inject MainViewModel as DataContext
                var mainWindow = new MainWindow
                {
                    DataContext = ServiceProvider.GetRequiredService<MainViewModel>()
                };

                mainWindow.Show();
            }
            catch (System.Exception ex)
            {
                // Show details to help diagnose the runtime error
                MessageBox.Show($"Unhandled exception during startup:\n{ex}", "Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Register Services
            services.AddSingleton<ICameraProvider, MockCameraAdapter>();
            services.AddSingleton<IFaceRecognitionProvider, MockFaceRecognitionAdapter>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // Register ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<FaceRecognitionViewModel>();
            services.AddTransient<OneToOneViewModel>();
            services.AddTransient<OneToManyViewModel>();
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Unhandled UI exception:\n{e.Exception}", "Unhandled UI Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                MessageBox.Show($"Unhandled domain exception:\n{ex}", "Unhandled Domain Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}