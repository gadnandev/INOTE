using INOTE.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INOTE.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly INoteContext _context;

        public IUserRepository Users { get; private set; }

        public UnitOfWork(INoteContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
