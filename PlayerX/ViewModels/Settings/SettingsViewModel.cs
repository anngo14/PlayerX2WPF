using PlayerX.Commands;
using PlayerX.Commands.SettingsCommands;
using PlayerX.Models;
using PlayerX.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PlayerX.ViewModels.Settings
{
    public class SettingsViewModel : ViewModelBase
    {
        private string _folderName;
        private ICollection<Menu> _menuItems;
        private NavigationStore _navigationStore;
        public string FolderName
        {
            get 
            { 
                return _folderName; 
            }
            set
            {
                _folderName = value;
                OnPropertyChanged(nameof(FolderName));
            }
        }

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
                GetFolderItems();
            }
        }

        public ObservableCollection<FolderItemViewModel> FolderItems { get; set; }

        public string FolderDirectory { get; set; }

        public ICommand HomeCommand { get; set; }

        public ICommand AddFolderCommand { get; } = null!; 

        public ICommand ChooseDirCommand { get; } = null!; 

        public SettingsViewModel(NavigationStore navigationStore, ICollection<Menu> menuItems)
        {
            _folderName = "Enter a Folder Name...";
            _navigationStore = navigationStore;
            _menuItems = menuItems;
            GetFolderItems();
            HomeCommand = new NavigateCommand(navigationStore, "home", menuItems);
            ChooseDirCommand = new ChooseDirCommand(this);
            AddFolderCommand = new AddFolderCommand(this);
        }

        public void ChooseFolder()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                FolderDirectory = dialog.SelectedPath;
            }
        }

        private void GetFolderItems()
        {
            FolderItems = new ObservableCollection<FolderItemViewModel>();
            foreach (var menu in _menuItems)
            {
                if (menu.Folder == null)
                {
                    continue;
                }
                FolderItems.Add(new FolderItemViewModel(_navigationStore, menu.Folder, _menuItems));
            }
        }
    }
}
