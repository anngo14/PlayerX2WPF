using PlayerX.Commands;
using PlayerX.Models;
using PlayerX.Stores;
using System.Collections.Generic;
using System.Windows.Input;

namespace PlayerX.ViewModels.FolderView
{
    public class MediaItemViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        private Media _media;
        private ICollection<Menu> _menuItems;

        public Media Media
        {
            get
            {
                return _media;
            }
            set
            {
                _media = value;
                PlayerCommand = new NavigateCommand(_navigationStore, "player", _menuItems, _media);
            }
        }
        public ICommand PlayerCommand { get; set; } = null!;
        public MediaItemViewModel(NavigationStore navigationStore, ICollection<Menu> menuItems)
        {
            _navigationStore = navigationStore;
            _menuItems = menuItems;
        }
    }
}
