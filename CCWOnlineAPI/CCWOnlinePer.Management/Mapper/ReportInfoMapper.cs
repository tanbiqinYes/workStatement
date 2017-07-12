using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CCWOnlineAPI.Management.Entities;

namespace CCWOnlineAPI.Management.Mapper
{
    public class ReportInfoMapper : EntityTypeConfiguration<ReportInfo>
    {
        public ReportInfoMapper()
        {
            this.ToTable("PER_ReportInfo");

            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            this.Property(s => s.AccountId).IsRequired();
            this.Property(s => s.ReportYear).IsRequired();
            this.Property(s => s.ReportMonth).IsRequired();

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
