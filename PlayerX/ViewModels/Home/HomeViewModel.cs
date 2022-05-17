using PlayerX.Commands;
using PlayerX.Commands.HomeCommands;
using PlayerX.Models;
using PlayerX.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PlayerX.ViewModels.Home
{
    public class HomeViewModel : ViewModelBase
    {
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        private string _time;

        public ObservableCollection<HomeItemViewModel> MenuItems { get; set; }

        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        public ICommand SettingsCommand { get; } = null!;

        public ICommand ExitCommand { get; } = null!;

        public HomeViewModel(NavigationStore navigationStore, ICollection<Menu> menuItems)
        {
            _time = "";

            MenuItems = new ObservableCollection<HomeItemViewModel>();
            foreach (var menu in menuItems)
            {
                var homeItemViewModel = new HomeItemViewModel(navigationStore, menuItems)
                {
                    Menu = menu
                };
                MenuItems.Add(homeItemViewModel);
            }
           
            Timer.Tick += new EventHandler(Timer_Click);

            Timer.Interval = new TimeSpan(0, 0, 1);

            Timer.Start();

            SettingsCommand = new NavigateCommand(navigationStore, "settings", menuItems);
            ExitCommand = new ExitCommand();
        }

        private void Timer_Click(object sender, EventArgs e)
        {
            DateTime d;
            d = DateTime.Now;

            Time = d.ToLongTimeString();
        }

    }
}
