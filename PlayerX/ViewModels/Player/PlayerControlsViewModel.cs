using PlayerX.Commands;
using PlayerX.Models;
using PlayerX.Stores;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace PlayerX.ViewModels.Player
{
    public class PlayerControlsViewModel : ViewModelBase
    {
        public event EventHandler PlayRequested;
        public event EventHandler PauseRequested;
        public event EventHandler ForwardRequested;
        public event EventHandler ReverseRequested;

        private Media _media;

        public Media Media
        {
            get
            {
                return _media;
            }
            set
            {
                _media = value;
                OnPropertyChanged(nameof(Media));
            }
        }

        public ICommand BackToListCommand { get; set; }

        public ICommand PlayCommand { get; set; }
        public ICommand PauseCommand { get; set; }
        public ICommand ReverseCommand { get; set; }
        public ICommand ForwardCommand { get; set; }
        public ICommand RaiseCommand { get; set; }
      
        public PlayerControlsViewModel(NavigationStore navigationStore, Media Media, ICollection<Menu> menuItems)
        {
            _media = Media;
            BackToListCommand = new NavigateCommand(navigationStore, "folder", menuItems, _media.Folder);
            RaiseCommand = new RaiseEventCommand(this);
        }

        protected virtual void OnPauseRequested(EventArgs e)
        {
            PauseRequested?.Invoke(this, e);
        }

        protected virtual void OnPlayRequested(EventArgs e)
        {
            PlayRequested?.Invoke(this, e);
        }

        protected virtual void OnForwardRequested(EventArgs e)
        {
            ForwardRequested?.Invoke(this, e);
        }

        protected virtual void OnReverseRequested(EventArgs e)
        {
            ReverseRequested?.Invoke(this, e);
        }

        public void Emit(string type)
        {
            switch (type)
            {
                case "pause":
                    OnPauseRequested(EventArgs.Empty);
                    break;
                case "play":
                    OnPlayRequested(EventArgs.Empty);   
                    break;
                case "forward":
                    OnForwardRequested(EventArgs.Empty);
                    break;
                case "reverse":
                    OnReverseRequested(EventArgs.Empty);
                    break;
            }
        }
    }
}
