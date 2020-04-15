using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MusicSearchWPF.Services;
using MusicSearchWPF.ViewModels;
using MusicSearchWPF.Views;
using SimpleInjector;

namespace MusicSearchWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RegisterServices();
            Start<HomeViewModel>();
        }

        private void RegisterServices()
        {
            Services = new Container();

            Services.RegisterSingleton<IMessenger, Messenger>();
            Services.RegisterSingleton<IFlyWeight, FlyWeight>();
            Services.RegisterSingleton<MainViewModel>();
            Services.RegisterSingleton<HomeViewModel>();
            Services.RegisterSingleton<DetailViewModel>();
            Services.RegisterSingleton<IMusicApiClient, MusicApiClient>();
            Services.Verify();
        }

        private void Start<T>() where T : ViewModelBase
        {
            var windowViewModel = Services.GetInstance<MainViewModel>();
            windowViewModel.CurrentViewModel = Services.GetInstance<T>();
            var window = new MainWindow { DataContext = windowViewModel };
            window.ShowDialog();
        }
    }
}
