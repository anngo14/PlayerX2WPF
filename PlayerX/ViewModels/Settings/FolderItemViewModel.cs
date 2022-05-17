using PlayerX.Commands.SettingsCommands;
using PlayerX.Models;
using PlayerX.Stores;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PlayerX.ViewModels.Settings
{
    public class FolderItemViewModel : ViewModelBase
    {
        private Visibility _visibility = Visibility.Visible;
        private ICollection<Menu> _menuItems;
        private Menu _menu;
        public Menu Menu
        {
            get
            {
                return _menu;
            }
            set
            {
                _menu = value;
            }
        }
        public Visibility Visibility
        {
            get
            {
                return _visibility;
            }
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }
        public Folder Folder { get; set; }
        public string FolderDirectory { get; set; }
        public ICommand ChooseDirCommand { get; set; }
        public ICommand DeleteCommand{ get; set; }

        public FolderItemViewModel(NavigationStore navigationStore, Folder folder, ICollection<Menu> menuItems)
        {
            _menuItems = menuItems;
            Menu = _menuItems.Single(a => a.Name == folder.Title);
            Folder = folder;
            FolderDirectory = folder.Directory;
            ChooseDirCommand = new ChooseDirCommand(this);
            DeleteCommand = new DeleteFolderCommand(this);
        }

        public void ChooseFolder()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                var oldMenu = JsonSerializer.Serialize(_menu);
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                FolderDirectory = dialog.SelectedPath;
                _menu.Folder.Directory = FolderDirectory;
                var newMenu = JsonSerializer.Serialize(_menu);

                //Edit menu item in player.txt
                var file = AppDomain.CurrentDomain.BaseDirectory + @"\playerx.txt";
                string text = File.ReadAllText(file);
                text = text.Replace(oldMenu, newMenu);
                File.WriteAllText(file, text);
            }
        }
    }
}
