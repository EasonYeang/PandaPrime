using Domain;
using System.Data.Entity.ModelConfiguration;

namespace Mapping
{
    public class PPermissionMap : EntityTypeConfiguration<PPermission>
    {
        public PPermissionMap()
        {
            this.ToTable("PPermission");
            this.HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            this.Property(r => r.Name).HasMaxLength(256).IsUnicode();
            this.Property(r => r.FilePath).HasColumnType("text").IsUnicode();
            this.Property(r => r.Icon).HasMaxLength(128).IsUnicode();
        }
    }
}
