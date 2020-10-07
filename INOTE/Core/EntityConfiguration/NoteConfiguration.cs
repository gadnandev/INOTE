using INOTE.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INOTE.Core.EntityConfiguration
{
    public class NoteConfiguration : EntityTypeConfiguration<Note>
    {
        public NoteConfiguration()
        {
            Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(50);

            Property(n => n.Content)
                .IsRequired()
                .HasMaxLength(1000);

            Property(n => n.UserId)
                .IsRequired();

            HasRequired(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserId)
                .WillCascadeOnDelete(true);
        }
    }
}
