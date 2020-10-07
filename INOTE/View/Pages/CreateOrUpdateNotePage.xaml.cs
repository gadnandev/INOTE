﻿using INOTE.Core.Domain;
using INOTE.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace INOTE.View.Pages
{
    /// <summary>
    /// Interaction logic for CreateOrUpdateNotePage.xaml
    /// </summary>
    public partial class CreateOrUpdateNotePage : Page
    {
        public CreateOrUpdateNotePage(User user)
        {
            InitializeComponent();
            this.DataContext = new CreateOrUpdateNoteVM(user);
        }
    }
}
