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
using System.Windows;
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

        private Note _selectedNote;

        public Note SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                OnPropertyChanged("SelectedNote");
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
                if (_previousCommand == null)
                {
                    _previousCommand = new RelayCommand(this.Previous);
                }
                return _previousCommand;
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

        private ICommand _createOrUpdateNoteCommand;

        public ICommand CreateOrUpdateNoteCommand
        {
            get
            {
                if (_createOrUpdateNoteCommand == null)
                {
                    _createOrUpdateNoteCommand = new RelayCommand(this.CreateOrUpdateNote);
                }
                return _createOrUpdateNoteCommand;
            }
        }

        private ICommand _deleteNoteCommand;

        public ICommand DeleteNoteCommand
        {
            get 
            {
                if (_deleteNoteCommand == null)
                {
                    _deleteNoteCommand = new RelayCommand(this.DeleteNote);
                }
                return _deleteNoteCommand;
            }
        }

        private void CreateOrUpdateNote()
        {
            NavigateCreateOrUpdateNotePage(SelectedNote);
        }

        private void DeleteNote()
        {
            var Result = MessageBox.Show($"Would you like to remove {SelectedNote.Title}?", "Note Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes)
            {
                UnitOfWork.Notes.Remove(SelectedNote);

                if (UnitOfWork.Complete() > 0)
                {
                    UserNotes = UnitOfWork.Notes.GetUserNotes(_loggedInUser, SearchText, _currentPageNumber, 10);
                }
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

        public void NavigateCreateOrUpdateNotePage(Note note)
        {
            Frame.Navigate(new CreateOrUpdateNotePage(_loggedInUser, note));
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
