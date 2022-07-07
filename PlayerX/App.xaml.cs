using PlayerX.Helpers;
using PlayerX.Models;
using PlayerX.Stores;
using PlayerX.ViewModels;
using PlayerX.ViewModels.Title;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace PlayerX
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private ICollection<Menu> _menuItems;

        public App()
        {
            _navigationStore = new NavigationStore();
            _menuItems = new List<Menu>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MasterDirectoryHelper helper = new MasterDirectoryHelper(_menuItems);
            if (helper.MasterDirectoryExists())
            {
                helper.ReadMasterDirectory();
            }
            else
            {
                helper.WriteMasterDirectory(null);
                helper.ReadMasterDirectory();
            }
            _navigationStore.CurrentViewModel = new TitleViewModel(_navigationStore, _menuItems);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore, _menuItems)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }

  
}
