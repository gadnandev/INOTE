using INOTE.Core;
using INOTE.Core.Domain;
using INOTE.Core.EntityValidator;
using INOTE.View.Pages;
using INOTE.ViewModel.Commands;
using INOTE.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public string ActionName { get; set; }

        public string Header { get; set; }

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

        private ICommand _actionCommand;

        public ICommand ActionCommand
        {
            get
            {
                if (_actionCommand == null)
                {
                    _actionCommand = new RelayCommand(
                        this.SaveOrUpdate
                    );
                }
                return _actionCommand;
            }
        }

        public CreateOrUpdateNoteVM(User loggedInUser, Note note)
        {
            _loggedInUser = loggedInUser;
            
            // if the note is null then the action is creation of note, otherwise updation
            if (note == null)
            {
                Note = new Note();
                ActionName = "Save";
            } 
            else
            {
                Note = note;
                ActionName = "Update";
            }
            Header = $"Note {ActionName}";
        }

        private void NavigateNotesPage()
        {
            Frame.Navigate(new NotesPage(_loggedInUser));
        }

        private void SaveOrUpdate()
        {
            NoteValidator validator = new NoteValidator();
            var validationResult = validator.Validate(Note);
            if (validationResult.IsValid)
            {
                if(ActionName.Equals("Save"))
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
                }
                else
                {
                    Note foundNote = UnitOfWork.Notes.SingleOrDefault(n => n.Id == Note.Id);

                    foundNote.Title = Note.Title;
                    foundNote.Content = Note.Content;
                    foundNote.UpdatedDate = DateTime.Now;
                }
                
                if (UnitOfWork.Complete() > 0)
                {
                    MessageBox.Show($"Note Title: '{Note.Title}' {ActionName.ToLower()}d.", $"Note {ActionName}", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigateNotesPage();
                }
                else
                {
                    ErrorText = $"Note could not {ActionName.ToLower()}d.";
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
