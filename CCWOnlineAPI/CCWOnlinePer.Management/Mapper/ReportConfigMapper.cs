using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using CCWOnlineAPI.Management.Entities;

namespace CCWOnlineAPI.Management.Mapper
{
    public class ReportConfigMapper : EntityTypeConfiguration<ReportConfig>
    {
        public ReportConfigMapper() 
        {
            this.ToTable("PER_ReportConfig");

            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            this.Property(s => s.DepartMentId).IsRequired();

            this.Property(s => s.Q1).HasMaxLength(512);
            this.Property(s => s.Q2).HasMaxLength(512);
            this.Property(s => s.Q3).HasMaxLength(512);
            this.Property(s => s.Q4).HasMaxLength(512);
            this.Property(s => s.Q5).HasMaxLength(512);
            this.Property(s => s.Q6).HasMaxLength(512);

            this.Property(s => s.Status).IsRequired();

            this.Property(s => s.Remark).HasMaxLength(256);
        }
    }
}
