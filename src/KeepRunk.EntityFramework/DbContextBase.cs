using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace KeepRunk.EntityFramework
{
    public class DbContextBase:DbContext
    {
        protected DbContextBase(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer<DbContextBase>(null);
            Configuration.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
