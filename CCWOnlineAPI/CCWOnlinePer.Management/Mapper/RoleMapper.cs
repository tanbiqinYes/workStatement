using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using CCWOnlineAPI.Management.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCWOnlineAPI.Management.Mapper
{
    public class RoleMapper : EntityTypeConfiguration<Role>
    {
        public RoleMapper()
        {
            this.ToTable("PER_Role");

            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            this.Property(s => s.RoleName).HasMaxLength(60);

            this.Property(s => s.Status).IsRequired();
        }
    }
}
