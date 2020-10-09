using INOTE.Core.Domain;
using INOTE.Core.Helper;
using INOTE.View.Pages;
using INOTE.ViewModel.Commands;
using INOTE.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace INOTE.ViewModel
{
    public class NoteVm : ViewModelBase
    {
        private User _loggedInUser;

        private int _currentPageNumber;

        public int CurrentPageNumber
        {
            get { return _currentPageNumber; }
            set
            {
                _currentPageNumber = value;
                OnPropertyChanged("CurrentPageNumber");
            }
        }

        private int _totalPageCount;

        public int TotalPageCount
        {
            get { return _totalPageCount; }
            set
            {
                _totalPageCount = value;
                OnPropertyChanged("TotalPageCount");
            }
        }

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set 
            {
                _searchText = value;
                CurrentPageNumber = 1;
                CalculateTotalPageCount();
                GetNotes();
                OnPropertyChanged("SearchText");
            }
        }

        private IEnumerable<Note> _userNotes;

        public IEnumerable<Note> UserNotes
        {
            get { return _userNotes; }
            set
            {
                _userNotes = value;
                OnPropertyChanged("UserNotes");
            }
        }

        private ICommand _previousCommand;

        public ICommand PreviousCommand
        {
            get
            {
                if (_newNoteCommand == null)
                {
                    _newNoteCommand = new RelayCommand(this.Previous);
                }
                return _newNoteCommand;
            }
        }

        private ICommand _nextCommand;

        public ICommand NextCommand
        {
            get
            {
                if (_nextCommand == null)
                {
                    _nextCommand = new RelayCommand(this.Next);
                }
                return _nextCommand;
            }
        }

        private ICommand _newNoteCommand;

        public ICommand NewNoteCommand
        {
            get 
            {
                if (_newNoteCommand == null)
                {
                    _previousCommand = new RelayCommand(this.NavigateCreateOrUpdateNotePage);
                }
                return _previousCommand;
            }
        }

        private void Next()
        {
            if (CurrentPageNumber < TotalPageCount)
            {
                ++CurrentPageNumber;
                GetNotes();
            }
        }

        private void Previous()
        {
            if (CurrentPageNumber > 1)
            {
                --CurrentPageNumber;
                GetNotes();
            }
        }

        public void NavigateCreateOrUpdateNotePage()
        {
            Frame.Navigate(new CreateOrUpdateNotePage(_loggedInUser));
        }

        public NoteVm(User loggedInUser)
        {
            _loggedInUser = loggedInUser;
            CalculateTotalPageCount();
            CurrentPageNumber = 1;

            GetNotes();
        }

        private void CalculateTotalPageCount()
        {
            TotalPageCount = UnitOfWork.Notes.GetNotesCount(_loggedInUser, SearchText, 10);
        }

        private void GetNotes()
        {
            UserNotes = UnitOfWork.Notes.GetUserNotes(_loggedInUser, SearchText, _currentPageNumber, 10);
        }
       
    }
}
