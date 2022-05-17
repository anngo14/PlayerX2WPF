namespace PlayerX.Commands
{
    public class OpenUrlCommand : CommandBase
    {
        private readonly string _url;

        public OpenUrlCommand(string url)
        {
            _url = url;
        }
        public override void Execute(object? parameter)
        {
            if(_url == null)
            {
                return;
            }

            var uri = _url;
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            System.Diagnostics.Process.Start(psi);
        }
    }
}
