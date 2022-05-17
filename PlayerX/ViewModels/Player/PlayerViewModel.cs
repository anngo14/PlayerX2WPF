using PlayerX.Models;
using PlayerX.Stores;
using System.Collections.Generic;

namespace PlayerX.ViewModels.Player
{
    public class PlayerViewModel : ViewModelBase
    {
        private Media _media;
        private PlayerControlsViewModel _controls;

        public Media Media 
        { 
            get
            {
                return _media;
            }
            set
            {
                _media = value; 
            }
        }

        public PlayerControlsViewModel Controls 
        { 
            get 
            { 
                return _controls; 
            }
            set
            {
                _controls = value;
            }
        }

        public PlayerViewModel(NavigationStore navigationStore, Media? Media, ICollection<Menu> menuItems)
        {
            _media = Media;
            _controls = new PlayerControlsViewModel(navigationStore, Media, menuItems);
        }
    }
}
