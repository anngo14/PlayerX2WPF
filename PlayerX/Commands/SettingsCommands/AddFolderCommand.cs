using PlayerX.Models;
using PlayerX.ViewModels.Settings;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace PlayerX.Commands.SettingsCommands
{
    public class AddFolderCommand : CommandBase
    {
        private SettingsViewModel _viewModel;
        public AddFolderCommand(SettingsViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            var folderName = _viewModel.FolderName;
            var folderDir = _viewModel.FolderDirectory;

            if(folderName == null || folderDir == null)
            {
                _viewModel.FolderName = "";
                _viewModel.FolderDirectory = "";
                MessageBox.Show("Please make sure a folder name and directory is chosen!", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                return;
            }

            var folder = new Folder
            {
                Title = folderName,
                Directory = folderDir
            };

            var menu = new Menu
            {
                Name = folderName,
                Folder = folder
            };

            if(_viewModel.MenuItems.Any(x => x.Name == menu.Name))
            {
                _viewModel.FolderName = "";
                _viewModel.FolderDirectory = "";
                MessageBox.Show("A folder with this name already exists! Please Try Again with a unique folder name!", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                return;
            }

            var file = AppDomain.CurrentDomain.BaseDirectory + @"\playerx.txt";
            using (StreamWriter sw = File.AppendText(file))
            {
                sw.WriteLine(JsonSerializer.Serialize(menu));
            }

            var menuItems = _viewModel.MenuItems;
            menuItems.Add(menu);

            _viewModel.MenuItems = menuItems;
            _viewModel.HomeCommand.Execute(null);
        }
    }
}
