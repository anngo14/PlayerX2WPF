using PlayerX.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace PlayerX.Helpers
{
    public class MasterDirectoryHelper
    {
        private string file = AppDomain.CurrentDomain.BaseDirectory + @"\playerx.txt";
        private ICollection<Menu> _menuItems;
        public MasterDirectoryHelper(ICollection<Menu> menuItems)
        {
            _menuItems = menuItems;
        }

        public bool MasterDirectoryExists()
        {
            return File.Exists(file);
        }

        public void ReadMasterDirectory()
        {
            _menuItems.Clear();
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

            _menuItems.Add(youtubeMenu);
            _menuItems.Add(spotifyMenu);

            using (StreamReader sr = File.OpenText(file))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    var masterDirectory = s;
                    SearchDirectory(masterDirectory);
                }
            }
        }

        public void WriteMasterDirectory(string? directory)
        {
            using (StreamWriter sw = File.CreateText(file))
            {
                if(directory != null)
                {
                    sw.WriteLine(directory);
                }
            }
        }

        private void SearchDirectory(string path)
        {
            string[] directories = Directory.GetDirectories(path);
            if (directories.Length > 0)
            {
                foreach (string directory in directories)
                {
                    SearchDirectory(directory);
                }
            }
            else
            {
                var split = path.Split('\\');
                var shortName = split[split.Length - 1];
                var menuItem = new Menu
                {
                    Name = shortName,
                    ImgPath = "/Images/folder-icon.png",
                    Folder = new Folder
                    {
                        Title = shortName,
                        Directory = path
                    }
                };
                _menuItems.Add(menuItem);
            }
        }
    }
}
