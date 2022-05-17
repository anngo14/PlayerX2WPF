using PlayerX.Commands;
using PlayerX.Models;
using PlayerX.Stores;
using System.Collections.Generic;
using System.Windows.Input;

namespace PlayerX.ViewModels.Home
{
    public class HomeItemViewModel : ViewModelBase
    {
        private Menu _menu;
        private NavigationStore _navigationStore;
        private ICollection<Menu> _menuItems;

        public Menu Menu 
        {
            get
            {
                return _menu;
            }
            set
            {
                _menu = value;
                if(_menu != null && _menu.Folder != null)
                {
                    FolderCommand = new NavigateCommand(_navigationStore, "folder", _menuItems, _menu.Folder);
                } 
                else if(_menu != null && _menu.Folder == null) 
                {
                    FolderCommand = new OpenUrlCommand(_menu.UrlPath);
                }
            } 
        }

        public ICommand FolderCommand { get; set; } = null!;

        public HomeItemViewModel(NavigationStore navigationStore, ICollection<Menu> menuItems)
        {
            _menu = Menu;
            _navigationStore = navigationStore;
            _menuItems = menuItems;
        }
    }
}
