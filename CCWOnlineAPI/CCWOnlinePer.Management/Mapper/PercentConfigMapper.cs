using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CCWOnlineAPI.Management.Entities;

namespace CCWOnlineAPI.Management.Mapper
{
    public class PercentConfigMapper : EntityTypeConfiguration<PercentConfig>
    {
        public PercentConfigMapper()
        {
            this.ToTable("PER_PercentConfig");

            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            this.Property(s => s.ReportYear).IsRequired();
            this.Property(s => s.ReportQuarter).IsRequired();

            this.Property(s => s.PercentName).HasMaxLength(60);
            this.Property(s => s.PercentValue).IsRequired();

            this.Property(s => s.Status).IsRequired();   
        }
    }
}
