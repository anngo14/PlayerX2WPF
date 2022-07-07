using PlayerX.Helpers;
using PlayerX.ViewModels.Settings;
using System.Windows;

namespace PlayerX.Commands.SettingsCommands
{
    public class AddDirCommand : CommandBase
    {
        private SettingsViewModel _viewModel;
        public AddDirCommand(SettingsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            MasterDirectoryHelper helper = new MasterDirectoryHelper(_viewModel.MenuItems);
            var _masterDirectory = _viewModel.MasterDirectory;

            if (_masterDirectory == null)
            {
                _viewModel.MasterDirectory = "";
                MessageBox.Show("Please make sure a folder name and directory is chosen!", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                return;
            }
            helper.WriteMasterDirectory(_masterDirectory);
            helper.ReadMasterDirectory();
            _viewModel.HomeCommand.Execute(null);
        }
    }
}
