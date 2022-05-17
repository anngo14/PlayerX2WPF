using PlayerX.Commands;
using PlayerX.Models;
using PlayerX.Stores;
using System.Collections.Generic;
using System.Windows.Input;

namespace PlayerX.ViewModels.Title
{
    public class TitleViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ICommand NavigateCommand { get; } = null!;

        public TitleViewModel(NavigationStore navigationStore, ICollection<Menu> menuItems)
        {
            _navigationStore = navigationStore;
            NavigateCommand = new NavigateCommand(_navigationStore, "home", menuItems);
        }
    }
}
