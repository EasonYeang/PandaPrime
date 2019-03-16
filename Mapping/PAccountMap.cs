using Domain;
using System.Data.Entity.ModelConfiguration;

namespace Mapping
{
    public class PAccountMap : EntityTypeConfiguration<PAccount>
    {
        public PAccountMap()
        {
            this.ToTable("PAccount");
            this.HasKey(d => d.ID).Property(d => d.ID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            this.Property(r => r.Account).HasMaxLength(64).IsUnicode();
            this.Property(r => r.UserName).HasMaxLength(64).IsUnicode();
            this.Property(r => r.Password).HasMaxLength(64).IsUnicode();
            this.Property(r => r.NickName).HasMaxLength(64).IsUnicode();
        }
    }
}
