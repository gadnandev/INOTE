using INOTE.Core.Domain;
using INOTE.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INOTE.Core.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(INoteContext context) : base(context)
        {
        }

        public INoteContext INoteContext
        {
            get { return Context as INoteContext; }
        }

        public bool Login(User user)
        {
            var cypherPassword = StringSecurity.Hash(user.Password);
            var foundUser = INoteContext.Users.SingleOrDefault(u => u.Username.Equals(user.Username));
            
            return foundUser != null && StringSecurity.Verify(user.Password, foundUser.Password);
        }

        public User Register(User user)
        {
            var registeredUser = user;

            IEnumerable<User> enumerable = Find(u => u.Username.Equals(user.Username));

            if(enumerable.Any())
            {
                return null;
            }

            registeredUser.Password = StringSecurity.Hash(user.Password);

            return INoteContext.Users.Add(registeredUser);
        }
    }
}
