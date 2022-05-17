using PlayerX.Models;
using PlayerX.Stores;
using PlayerX.ViewModels.FolderView;
using PlayerX.ViewModels.Home;
using PlayerX.ViewModels.Player;
using PlayerX.ViewModels.Settings;
using PlayerX.ViewModels.Title;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerX.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        private string _to;

        private BaseModel _model;

        private ICollection<Menu> _menuItems;

        public NavigateCommand(NavigationStore navigationStore, string to, ICollection<Menu> menuItems, BaseModel model = null!)
        {
            _navigationStore = navigationStore;
            _to = to;
            _model = model;
            _menuItems = menuItems;
        }

        public override void Execute(object? parameter)
        {
            switch (_to)
            {
                case "title":
                    _navigationStore.CurrentViewModel = new TitleViewModel(_navigationStore, _menuItems);
                    break;
                case "home":
                    _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore, _menuItems);
                    break;
                case "settings":
                    _navigationStore.CurrentViewModel = new SettingsViewModel(_navigationStore, _menuItems);
                    break;
                case "folder":
                    _navigationStore.CurrentViewModel = new FolderViewViewModel(_navigationStore, (Folder) _model, _menuItems);
                    break;
                case "player":
                    _navigationStore.CurrentViewModel = new PlayerViewModel(_navigationStore, (Media) _model, _menuItems);
                    break;
            }
        }
    }
}
