using PlayerX.Commands;
using PlayerX.Models;
using PlayerX.Stores;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;

namespace PlayerX.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {

        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public ICommand HomeCommand { get; } = null!;

        public ICommand FullScreenCommand { get; } = null!;

        public ICommand ExitFullScreenCommand { get; } = null!;

        public MainViewModel(NavigationStore navigationStore, ICollection<Menu> menuItems)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            HomeCommand = new NavigateCommand(navigationStore, "home", menuItems);
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

    }
}
