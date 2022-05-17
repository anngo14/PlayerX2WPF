using System.Windows;
using System.Windows.Input;

namespace PlayerX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PlayerWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.OemTilde)
            {
                if(PlayerWindow.WindowState == WindowState.Normal)
                {
                    PlayerWindow.WindowState = WindowState.Maximized;
                    PlayerWindow.WindowStyle = WindowStyle.None;
                } 
                else if(PlayerWindow.WindowState == WindowState.Maximized)
                {
                    PlayerWindow.WindowState = WindowState.Normal;
                    PlayerWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                }  
            }
        }
    }
}
