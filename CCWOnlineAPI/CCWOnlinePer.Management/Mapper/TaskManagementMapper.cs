using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CCWOnlineAPI.Management.Entities;

namespace CCWOnlineAPI.Management.Mapper
{
    public class TaskManagementMapper : EntityTypeConfiguration<TaskManagement>
    {
        public TaskManagementMapper()
        {
            this.ToTable("PER_TaskManagement");

            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            this.Property(s => s.TaskYear).IsRequired();
            this.Property(s => s.Quarter).IsRequired();
            this.Property(s => s.RoleId).IsRequired();

            this.Property(s => s.StartDate).IsRequired();
            this.Property(s => s.EndDate).IsRequired();
            this.Property(s => s.Status).IsRequired();
        }
    }
}
