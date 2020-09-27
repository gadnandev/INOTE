using INOTE.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INOTE.Core.EntityConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(32);

            Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);

            Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
