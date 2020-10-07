using INOTE.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INOTE.Core.Repository
{
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        public NoteRepository(DbContext context) : base(context)
        {
        }

        public INoteContext INoteContext
        {
            get { return Context as INoteContext; }
        }

        public int GetNotesCount(User user, string searchText, int pageOffset)
        {
            if(searchText != null && searchText.Length > 0 )
            {
                return (int)(Math.Ceiling((double)Find(n => n.UserId == user.Id && n.Title.Contains(searchText)).Count() / (double)pageOffset));
            }
            else
            {
                return (int)(Math.Ceiling((double)Find(n => n.UserId == user.Id).Count() / (double)pageOffset));
            }
        }

        public IEnumerable<Note> GetUserNotes(User user, string searchText, int pageNumber, int pageOffset)
        {
            IQueryable<Note> query = INoteContext.Notes
                .Include(n => n.User)
                .Where(n => n.UserId == user.Id);
                

            if(searchText != null && searchText.Length > 0)
            {
                query = query.Where(n => n.Title.Contains(searchText));
            }

            return query
                .OrderBy(n => n.Title)
                .Skip((pageNumber - 1) * pageOffset)
                .Take(pageOffset).ToList();
        }
    }
}
