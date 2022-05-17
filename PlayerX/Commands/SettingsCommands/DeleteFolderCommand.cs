using PlayerX.ViewModels.Settings;
using System;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace PlayerX.Commands.SettingsCommands
{
    public class DeleteFolderCommand : CommandBase
    {
        private FolderItemViewModel _viewModel;
        public DeleteFolderCommand(FolderItemViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            var menuToDelete = _viewModel.Menu;
            if (menuToDelete == null)
            {
                MessageBox.Show("No Folder to Delete!", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                return;
            }
            var menuTextToSkip = JsonSerializer.Serialize(menuToDelete);
            var file = AppDomain.CurrentDomain.BaseDirectory + @"\playerx.txt";
            string text = "";
            using (StreamReader sr = File.OpenText(file))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                   if(s != menuTextToSkip)
                    {
                        text += s + '\n';
                    }
                }
            }
            File.WriteAllText(file, text);
            _viewModel.Visibility = Visibility.Collapsed;
            MessageBox.Show($"{menuToDelete.Name} has been deleted! This will reflect once the application has been restarted", "Successful Deletion", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);

        }
    }
}
