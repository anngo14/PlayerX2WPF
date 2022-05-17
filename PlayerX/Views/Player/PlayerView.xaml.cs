using PlayerX.ViewModels.Player;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PlayerX.Views.Player
{
    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerView : UserControl
    {

        private bool show;
        private PlayerControlsViewModel vm;
        public PlayerView()
        {
            InitializeComponent();
            show = false;
        }

        private void ToggleVisibility()
        {
            show = !show;
            Cursor = show ? Cursors.Arrow : Cursors.None;
            Controls.Visibility = show ? Visibility.Visible : Visibility.Hidden;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Focus();
            Controls.Visibility = Visibility.Hidden;
            Player.LoadedBehavior = MediaState.Manual;
            Player.UnloadedBehavior = MediaState.Manual;
            Player.Play();
            Cursor = Cursors.None;
            var uriSource = new Uri(@"/PlayerX;component/Images/pauseicon.png", UriKind.Relative);
            Controls.PlayImg.Source = new BitmapImage(uriSource);
            Controls.PlayBtn.CommandParameter = "pause";
            vm = Controls.DataContext as PlayerControlsViewModel;
            
            vm.PlayRequested += (sender, e) =>
            {
                Player.Play();
                var uriSource = new Uri(@"/PlayerX;component/Images/pauseicon.png", UriKind.Relative);
                Controls.PlayImg.Source = new BitmapImage(uriSource);
                Controls.PlayBtn.CommandParameter = "pause";
            };

            vm.PauseRequested += (sender, e) =>
            {
                Player.Pause();
                var uriSource = new Uri(@"/PlayerX;component/Images/playicon.png", UriKind.Relative);
                Controls.PlayImg.Source = new BitmapImage(uriSource);
                Controls.PlayBtn.CommandParameter = "play";
            };

            vm.ForwardRequested += (sender, e) =>
            {
                Player.Position = Player.Position + TimeSpan.FromSeconds(10);
            };

            vm.ReverseRequested += (sender, e) =>
            {
                Player.Position = Player.Position - TimeSpan.FromSeconds(10);
            };
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Focus();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.M)
            {
                ToggleVisibility();
            }
            else if (e.Key == Key.P)
            {
                Player.Pause();
                var uriSource = new Uri(@"/PlayerX;component/Images/playicon.png", UriKind.Relative);
                Controls.PlayImg.Source = new BitmapImage(uriSource);
                Controls.PlayBtn.CommandParameter = "play";
            }
            else if(e.Key == Key.Space)
            {
                Player.Play();
                var uriSource = new Uri(@"/PlayerX;component/Images/pauseicon.png", UriKind.Relative);
                Controls.PlayImg.Source = new BitmapImage(uriSource);
                Controls.PlayBtn.CommandParameter = "pause";
            }
            else if(e.Key == Key.OemPeriod)
            {
                Player.Position = Player.Position + TimeSpan.FromSeconds(10);
            } 
            else if(e.Key == Key.OemComma)
            {
                Player.Position = Player.Position - TimeSpan.FromSeconds(10);
            }
        }

        private void Player_MediaEnded(object sender, RoutedEventArgs e)
        {
            if(vm.Media.Next != null)
            {
                var next = vm.Media.Next;
                vm.Media = next;
                Player.Source = new System.Uri(next.FilePath);
                Player.Play();
            }
            else
            {
                Player.Close();
            }
        }
    }
}
