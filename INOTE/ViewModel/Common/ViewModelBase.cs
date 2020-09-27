using INOTE.Core;
using INOTE.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace INOTE.ViewModel.Common
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected Frame Frame;

        protected UnitOfWork UnitOfWork;

        public ViewModelBase()
        {
            Frame = MainWindow.Frame;
            UnitOfWork = new UnitOfWork(new INoteContext());
        }

    }
}
