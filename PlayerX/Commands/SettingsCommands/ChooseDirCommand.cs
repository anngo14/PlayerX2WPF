using PlayerX.ViewModels;
using PlayerX.ViewModels.Settings;

namespace PlayerX.Commands.SettingsCommands
{
    public class ChooseDirCommand : CommandBase
    {
        private ViewModelBase _viewModel;
        public ChooseDirCommand(ViewModelBase viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            if(parameter.ToString() == "settings")
            {
                (_viewModel as SettingsViewModel).ChooseDirectory();
            }
        }
    }
}
