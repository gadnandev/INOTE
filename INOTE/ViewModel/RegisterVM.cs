using FluentValidation;
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
    public class RegisterVM : ViewModelBase
    {
        private ICommand _navigateLoginPageCommand;

        public ICommand NavigateLoginPageCommand
        {
            get
            {
                if (_navigateLoginPageCommand == null)
                {
                    _navigateLoginPageCommand = new RelayCommand(
                       this.NavigateLoginPage
                    );
                }
                return _navigateLoginPageCommand;
            }
        }

        private ICommand _registerCommand;

        public ICommand RegisterCommand
        {
            get 
            {
                if(_registerCommand == null)
                {
                    _registerCommand = new RelayCommand(
                        this.Register
                    );
                }
                return _registerCommand;
            }
        }

        private string _passwordConf;

        public string PasswordConf
        {
            get { return _passwordConf; }
            set 
            { 
                _passwordConf = value;
                OnPropertyChanged("PasswordConf");
            }
        }

        private User _registeredUser;

        public User RegisteredUser
        {
            get { return _registeredUser; }
            set 
            {
                _registeredUser = value;
                OnPropertyChanged("RegisteredUser");
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

        public RegisterVM()
        {
            RegisteredUser = new User();
        }

        private void Register()
        {
            if(RegisteredUser.Password != null && !PasswordConf.Equals(RegisteredUser.Password))
            {
                ErrorText = "Please check password confirmation";
                return;
            }

            UserValidator validator = new UserValidator();
            var validationResult = validator.Validate(RegisteredUser, o => { o.IncludeRuleSets("Register"); });
            if (validationResult.IsValid)
            {
                var createdUser = UnitOfWork.Users.Register(RegisteredUser);
                if (createdUser != null)
                {
                    MessageBox.Show($"User {createdUser.Username} registred.", "Register", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (UnitOfWork.Complete() > 0)
                    {
                        Frame.Navigate(new LoginPage());
                    }
                }
                else
                {
                    ErrorText = "User registration failed";
                }
            }
            else
            {
                // show first error message
                ErrorText = validationResult.Errors[0].ErrorMessage;
            }
        }

        public void NavigateLoginPage()
        {
            Frame.Navigate(new LoginPage());
        }
    }
}
