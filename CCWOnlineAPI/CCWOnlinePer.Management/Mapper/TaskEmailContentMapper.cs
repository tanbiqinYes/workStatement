using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CCWOnlineAPI.Management.Entities;

namespace CCWOnlineAPI.Management.Mapper
{
    public class TaskEmailContentMapper : EntityTypeConfiguration<TaskEmailContent>
    {
        public TaskEmailContentMapper()
        {
            this.ToTable("PER_TaskEmailContent");

            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            this.Property(s => s.EmailContent).HasMaxLength(512);
            this.Property(s => s.ReportEmailContent).HasMaxLength(512);
            this.Property(s => s.Status).IsRequired();
        }
    }
}
