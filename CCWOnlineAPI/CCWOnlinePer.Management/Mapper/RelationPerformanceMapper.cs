using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using CCWOnlineAPI.Management.Entities;

namespace CCWOnlineAPI.Management.Mapper
{
    public class RelationPerformanceMapper : EntityTypeConfiguration<RelationPerformance>
    {
        public RelationPerformanceMapper()
        {
            this.ToTable("PER_RelationPerformance");

            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            this.Property(s => s.AccountId).IsRequired();
            this.Property(s => s.ReportYear).IsRequired();
            this.Property(s => s.Quarter).IsRequired();
            this.Property(s => s.RelationType).IsRequired();
            this.Property(s => s.RelationAccountId).IsRequired();

            this.Property(s => s.Status).IsRequired();          
        }   
    }
}
