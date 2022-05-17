using PlayerX.ViewModels.Player;

namespace PlayerX.Commands
{
    public class RaiseEventCommand : CommandBase
    {
        private PlayerControlsViewModel _playerControlsViewModel;
        public RaiseEventCommand(PlayerControlsViewModel viewModel)
        {
            _playerControlsViewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
           _playerControlsViewModel.Emit(parameter.ToString());
        }
    }
}
