using INOTE.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INOTE.Core.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User Register(User user);

        User Login(User user);
    }
}
