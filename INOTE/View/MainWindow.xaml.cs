using INOTE.Core.Domain;
using INOTE.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace INOTE.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame Frame;

        private static ToolBarTray Toolbar;

        private static TextBlock Username;
        private static TextBlock Email;

        public MainWindow()
        {
            InitializeComponent();
            Frame = MainWindowFrame;

            Toolbar = MainToolbar;
            Username = UsernameTb;
            Email = EmailTb;

            MainWindowFrame.Navigate(new LoginPage());
        }

        public static void SetMainToolbarVisibility(bool isVisible, User user = null)
        {
            if(isVisible)
            {
                Toolbar.Visibility = Visibility.Visible;
                Username.Text = user.Username;
                Email.Text = user.Email;
            }
            else
            {
                Toolbar.Visibility = Visibility.Hidden;
            }
        }
    }
}
