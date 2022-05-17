using PlayerX.Models;
using PlayerX.Stores;
using PlayerX.ViewModels;
using PlayerX.ViewModels.Home;
using PlayerX.ViewModels.Title;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace PlayerX
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private ICollection<Menu> _menuItems;

        public App()
        {
            _navigationStore = new NavigationStore();
            _menuItems = new List<Menu>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var file = AppDomain.CurrentDomain.BaseDirectory+ @"\playerx.txt";
            if (File.Exists(file))
            {
                //Read Directories and Custom Folders
                OpenFile(file);
            }
            else
            {
                //Create File with Default Directories
                CreateInitialFile(file);
            }
            _navigationStore.CurrentViewModel = new TitleViewModel(_navigationStore, _menuItems);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore, _menuItems)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private void CreateInitialFile(string fileName)
        {
            using (StreamWriter sw = File.CreateText(fileName))
            {
                var youtubeMenu = new Menu
                {
                    Name = "Youtube",
                    ImgPath = "/Images/youtube.png",
                    UrlPath = "https://www.youtube.com"
                };

                var spotifyMenu = new Menu
                {
                    Name = "Spotify",
                    ImgPath = "/Images/spotify.png",
                    UrlPath = "https://open.spotify.com/"
                };

                var videoMenu = new Menu
                {
                    Name = "Videos",
                    ImgPath = "/Images/movie.png",
                    Folder = new Folder
                    {
                        Title = "Videos",
                        Directory = "C:\\Users\\Andrew\\Desktop\\Dramas"
                    }
                };

                sw.WriteLine(JsonSerializer.Serialize(youtubeMenu));
                sw.WriteLine(JsonSerializer.Serialize(spotifyMenu));
                sw.WriteLine(JsonSerializer.Serialize(videoMenu));
            }
        }

        private void OpenFile(string fileName)
        {
            using (StreamReader sr = File.OpenText(fileName))
            {
                string s = "";
                while((s = sr.ReadLine()) != null)
                {
                    Menu menu = JsonSerializer.Deserialize<Menu>(s);
                    _menuItems.Add(menu);
                }
            }
        }
    }

  
}
