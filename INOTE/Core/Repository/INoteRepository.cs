using INOTE.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INOTE.Core.Repository
{
    public interface INoteRepository : IRepository<Note>
    {
        IEnumerable<Note> GetUserNotes(User user, string searchText, int pageNumber, int pageOffset);

        int GetNotesCount(User user, string searchText, int pageOffset);
    }
}
