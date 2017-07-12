using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using CCWOnlineAPI.Management.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCWOnlineAPI.Management.Mapper
{
    public class DictionariesMapper : EntityTypeConfiguration<Dictionaries>
    {
        public DictionariesMapper()
        {
            this.ToTable("PER_Dictionary");

            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            this.Property(s => s.ParentId).IsRequired();
            this.Property(s => s.DicType).IsRequired();

            this.Property(s => s.DicName).HasMaxLength(60);

            this.Property(s => s.Status).IsRequired();

            this.Property(s => s.Remark).HasMaxLength(256);
        }
    }
}
