using Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;

namespace Utility
{
    public class PrimeContext : DbContext
    {
        public PrimeContext() : base("Panda")
        {
            //取消当数据库模型发生改变时删除当前数据库重建新数据库的设置。
            Database.SetInitializer<PrimeContext>(null);
        }

        #region Tables

        public DbSet<PAccount> PAccount { get; set; }
        public DbSet<PPermission> PPermission { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Load all map files -- inherited from EntityTypeConfiguration
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.Load("Mapping"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
