using PlayerX.ViewModels;
using PlayerX.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                (_viewModel as SettingsViewModel).ChooseFolder();
            } 
            else if(parameter.ToString() == "folderItem")
            {
                (_viewModel as FolderItemViewModel).ChooseFolder();
            }
        }
    }
}
