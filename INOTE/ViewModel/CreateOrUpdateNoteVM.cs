using INOTE.Core.Domain;
using INOTE.Core.EntityValidator;
using INOTE.View.Pages;
using INOTE.ViewModel.Commands;
using INOTE.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace INOTE.ViewModel
{
    public class CreateOrUpdateNoteVM : ViewModelBase
    {
        private User _loggedInUser;

        private Note _note;

        public Note Note
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged("Note");
            }
        }

        private string _errorText;

        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                OnPropertyChanged("ErrorText");
            }
        }

        private ICommand _backCommand;

        public ICommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                {
                    _backCommand = new RelayCommand(
                        this.NavigateNotesPage
                    );
                }
                return _backCommand;
            }
        }

        private ICommand _saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        this.Save
                    );
                }
                return _saveCommand;
            }
        }

        public CreateOrUpdateNoteVM(User loggedInUser)
        {
            _loggedInUser = loggedInUser;
            Note = new Note();
        }

        private void NavigateNotesPage()
        {
            Frame.Navigate(new NotesPage(_loggedInUser));
        }

        private void Save()
        {
            NoteValidator validator = new NoteValidator();
            var validationResult = validator.Validate(Note);
            if (validationResult.IsValid)
            {
                Note note = new Note
                {
                    Title = Note.Title,
                    Content = Note.Content,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    UserId = _loggedInUser.Id
                };

                UnitOfWork.Notes.Add(note);
                if (UnitOfWork.Complete() > 0)
                {
                    MessageBox.Show($"Note {Note.Title} saved.", "Note Save", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigateNotesPage();
                }
                else
                {
                    ErrorText = "Note could not saved";
                }
            } 
            else
            {
                // show first error message
                ErrorText = validationResult.Errors[0].ErrorMessage;
            }
        }

    }
}
