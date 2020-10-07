using INOTE.Core.Domain;
using INOTE.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INOTE.ViewModel
{
    public class CreateOrUpdateNoteVM : ViewModelBase
    {
        private User _loggedInUser;

        public CreateOrUpdateNoteVM(User loggedInUser)
        {
            _loggedInUser = loggedInUser;
        }
    }
}
