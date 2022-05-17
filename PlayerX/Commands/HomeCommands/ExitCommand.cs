namespace PlayerX.Commands.HomeCommands
{
    public class ExitCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
