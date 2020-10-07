using FluentValidation;
using INOTE.Core.Domain;
using INOTE.Core.EntityValidator;
using INOTE.View;
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
using System.Windows.Controls;
using System.Windows.Input;

namespace INOTE.ViewModel
{
    public class LoginVM : ViewModelBase
    {
        public User User { get; set; }

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

        private ICommand _navigateRegisterPageCommand;

        public ICommand NavigateRegisterPageCommand
        {
            get
            {
                if (_navigateRegisterPageCommand == null)
                {
                    _navigateRegisterPageCommand = new RelayCommand(
                       this.NavigateRegisterPage
                    );
                }
                return _navigateRegisterPageCommand;
            }
        }

        private ICommand _loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                if(_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(this.Login);
                }
                return _loginCommand;
            }
        }

        public LoginVM()
        {
            User = new User();
        }

        private void Login()
        {
            UserValidator validator = new UserValidator();
            var validationResult = validator.Validate(User, o => { o.IncludeRuleSets("Login"); });
            if(validationResult.IsValid)
            {
                var loggedInUser = UnitOfWork.Users.Login(User);
                if (loggedInUser != null)
                {
                    Frame.Navigate(new NotesPage(loggedInUser));
                }
                else
                {
                    ErrorText = "Invalid Credential";
                }
            }
            else
            {
                // show first error message
                ErrorText = validationResult.Errors[0].ErrorMessage;
            } 
        }

        public void NavigateRegisterPage()
        {
            Frame.Navigate(new RegisterPage());
        }
       
    }
}
