using PlayerX.Commands;
using PlayerX.Commands.SettingsCommands;
using PlayerX.Models;
using PlayerX.Stores;
using System.Collections.Generic;
using System.Windows.Input;

namespace PlayerX.ViewModels.Settings
{
    public class SettingsViewModel : ViewModelBase
    {
        private string _masterDirectory;
        private ICollection<Menu> _menuItems;
        private NavigationStore _navigationStore;

        public ICollection<Menu> MenuItems
        {
            get
            {
                return _menuItems;
            }
            set
            {
                _menuItems = value;
                HomeCommand = new NavigateCommand(_navigationStore, "home", _menuItems);
            }
        }


        public string MasterDirectory {
            get
            {
                return _masterDirectory;
            }
            set
            {
                _masterDirectory = value;
                OnPropertyChanged(nameof(MasterDirectory));
            } 
        }

        public ICommand HomeCommand { get; set; }

        public ICommand ChooseDirCommand { get; } = null!; 

        public ICommand AddDirCommand { get; } = null!;

        public SettingsViewModel(NavigationStore navigationStore, ICollection<Menu> menuItems)
        {
            _masterDirectory = "Choose a Directory...";
            _navigationStore = navigationStore;
            _menuItems = menuItems;
            HomeCommand = new NavigateCommand(navigationStore, "home", menuItems);
            ChooseDirCommand = new ChooseDirCommand(this);
            AddDirCommand = new AddDirCommand(this);
        }

        public void ChooseDirectory()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                MasterDirectory = dialog.SelectedPath;
            }
        }
    }
}
