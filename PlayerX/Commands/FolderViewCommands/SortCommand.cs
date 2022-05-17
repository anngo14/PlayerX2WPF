using PlayerX.Models;
using PlayerX.ViewModels.FolderView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerX.Commands.FolderViewCommands
{
    public class SortCommand : CommandBase
    {
        private FolderViewViewModel _viewModel;
        public SortCommand(FolderViewViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            if (_viewModel.SortAscending)
            {
                var sorted = _viewModel.MediaItems.OrderByDescending(x => x.Media.FileName.Length).ThenByDescending(x => x.Media.FileName);
                var sortedCollection = new ObservableCollection<MediaItemViewModel>(sorted);
                _viewModel.MediaItems = sortedCollection;
            }
            else
            {
                var sorted = _viewModel.MediaItems.OrderBy(x => x.Media.FileName.Length).ThenBy(x => x.Media.FileName);
                var sortedCollection = new ObservableCollection<MediaItemViewModel>(sorted);
                _viewModel.MediaItems = sortedCollection;
            }
            _viewModel.SortAscending = !_viewModel.SortAscending;
        }
    }
}
