using PlayerX.Commands;
using PlayerX.Commands.FolderViewCommands;
using PlayerX.Models;
using PlayerX.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace PlayerX.ViewModels.FolderView
{
    public class FolderViewViewModel : ViewModelBase
    {
        private Folder _folder;
        private ObservableCollection<MediaItemViewModel> _mediaItems;
        private NavigationStore _navigationStore;
        private ICollection<Menu> _menuItems;
        public Folder Folder
        {
            get
            {
                return _folder;
            }
            set
            {
                _folder = value;
                _mediaItems = new ObservableCollection<MediaItemViewModel>();

                //Get All files in Directory and Subdirectories
                string[] allFiles = Directory.GetFiles(_folder.Directory, "*", SearchOption.AllDirectories);
                allFiles = allFiles.OrderBy(x => x.Length).ThenBy(x => x).ToArray();
                //Add Files as Media items
                for(int i = 0; i < allFiles.Length; i++)
                {
                    var file = allFiles[i];
                    var fullFileName = GetFullFileName(file);
                    var tmp = fullFileName.Split(".");
                    var fileNameArray = new string[tmp.Length - 1];
                    Array.Copy(tmp, fileNameArray, tmp.Length - 1);
                    var fileName = string.Join(".", fileNameArray);
                    var fileExtension = tmp[tmp.Length - 1];
                    
                    if (!IsValidExtension(fileExtension))
                    {
                        continue;
                    }

                    var mediaItem = new MediaItemViewModel(_navigationStore, _menuItems)
                    {
                        Media = new Media
                        {
                            FileName = fileName,
                            FilePath = file,
                            Folder = _folder
                        }

                    };

                    var currMedia = mediaItem.Media;
                    for (int j = i + 1; j < allFiles.Length; j++)
                    {
                        var nextFile = allFiles[j];
                        var nextFullFileName = GetFullFileName(nextFile);
                        tmp = nextFullFileName.Split(".");
                        var nextFileName = tmp[0];
                        var nextFileExtension = tmp[1];
                        if (!IsValidExtension(nextFileExtension))
                        {
                            continue;
                        }

                        var nextMedia = new Media
                        {
                            FileName = nextFileName,
                            FilePath = nextFile,
                            Folder = _folder
                        };

                        currMedia.Next = nextMedia;
                        currMedia = currMedia.Next;
                    }
                  
                    _mediaItems.Add(mediaItem);
                }
            }
        }
        public ObservableCollection<MediaItemViewModel> MediaItems
        {
            get
            {
                return _mediaItems;
            }
            set
            {
                _mediaItems = value;
                OnPropertyChanged(nameof(MediaItems));
            }
        }

        public bool SortAscending { get; set; } = false;
        public ICommand HomeCommand { get; set; } = null!;
        public ICommand SortCommand { get; set; } = null!;

        public FolderViewViewModel(NavigationStore navigationStore, Folder? folder, ICollection<Menu> menuItems)
        {
            _navigationStore = navigationStore;
            _menuItems = menuItems;
            Folder = folder;
            HomeCommand = new NavigateCommand(navigationStore, "home", menuItems);
            SortCommand = new SortCommand(this);
            SortCommand.Execute(null);
        }

        private string GetFullFileName(string filePath)
        {
            string[] split = filePath.Split("\\");

            string fullFileName = split[split.Length - 1];

            return fullFileName;
        }

        private bool IsValidExtension(string extension)
        {
            extension = extension.ToLower();
            if(extension == "wmv" || extension == "mp4" || extension == "h264")
            {
                return true;
            }
            return false;
        }
    }
}
