using INOTE.View;
using INOTE.View.Pages;
using INOTE.ViewModel.Commands;
using INOTE.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace INOTE.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        private ICommand _logout;

        public ICommand Logout
        {
            get
            {
                if (_logout == null)
                {
                    _logout = new RelayCommand(
                       this.NavigateLoginPage
                    );
                }
                return _logout;
            }
        }

        private void NavigateLoginPage()
        {
            MainWindow.Frame.Navigate(new LoginPage());
        }

    }
}
