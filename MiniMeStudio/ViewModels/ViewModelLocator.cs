using System;
using System.Windows.Controls;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

using Microsoft.Extensions.Configuration;

using MiniMeStudio.Contracts.Services;
using MiniMeStudio.Contracts.Views;
using MiniMeStudio.Core.Contracts.Services;
using MiniMeStudio.Core.Services;
using MiniMeStudio.Models;
using MiniMeStudio.Services;
using MiniMeStudio.Views;

namespace MiniMeStudio.ViewModels
{
    public class ViewModelLocator
    {
        private IPageService PageService
            => SimpleIoc.Default.GetInstance<IPageService>();

        public ShellViewModel ShellViewModel
            => SimpleIoc.Default.GetInstance<ShellViewModel>();

        public MainViewModel MainViewModel
            => SimpleIoc.Default.GetInstance<MainViewModel>();

        public MasterDetailViewModel MasterDetailViewModel
            => SimpleIoc.Default.GetInstance<MasterDetailViewModel>();

        //public LoginViewModel LoginViewModel
        //    => SimpleIoc.Default.GetInstance<LoginViewModel>();

        public UsersViewModel UsersViewModel
          => SimpleIoc.Default.GetInstance<UsersViewModel>();

        public MiniBuilderViewModel MiniBuilderViewModel
            => SimpleIoc.Default.GetInstance<MiniBuilderViewModel>();

        public ViewModelLocator()
        {
            // App Host
            SimpleIoc.Default.Register<IApplicationHostService, ApplicationHostService>();

            // Core Services
            SimpleIoc.Default.Register<IFileService, FileService>();
            SimpleIoc.Default.Register<ISampleDataService, SampleDataService>();

            // Services
            SimpleIoc.Default.Register<IThemeSelectorService, ThemeSelectorService>();
            SimpleIoc.Default.Register<IPersistAndRestoreService, PersistAndRestoreService>();
            SimpleIoc.Default.Register<IPageService, PageService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();

            // Window
            SimpleIoc.Default.Register<IShellWindow, ShellWindow>();
            SimpleIoc.Default.Register<ShellViewModel>();
            SimpleIoc.Default.Register<MiniBuilderViewModel>();

            // Pages
            Register<MainViewModel, MainPage>();
            Register<MasterDetailViewModel, MasterDetailPage>();
            Register<UsersViewModel, UsersPage>();

        }

        private void Register<VM, V>()
            where VM : ViewModelBase
            where V : Page
        {
            SimpleIoc.Default.Register<VM>();
            SimpleIoc.Default.Register<V>();
            PageService.Configure<VM, V>();
        }

        public void AddConfiguration(IConfiguration configuration)
        {
            var appConfig = configuration
                .GetSection(nameof(AppConfig))
                .Get<AppConfig>();

            // Register configurations to IoC
            SimpleIoc.Default.Register(() => configuration);
            SimpleIoc.Default.Register(() => appConfig);
        }
    }
}
