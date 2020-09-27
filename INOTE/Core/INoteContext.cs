using INOTE.Core.Domain;
using INOTE.Core.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INOTE.Core
{
    public class INoteContext : DbContext
    {
        public INoteContext() : base("name=INoteContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
        }
    }
}
