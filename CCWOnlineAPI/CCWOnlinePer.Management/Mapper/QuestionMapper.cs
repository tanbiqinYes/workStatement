using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CCWOnlineAPI.Management.Entities;

namespace CCWOnlineAPI.Management.Mapper
{
    public class QuestionMapper : EntityTypeConfiguration<Question>
    {
        public QuestionMapper()
        {
            this.ToTable("PER_Question");

            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            //this.Property(s => s.ReportYear).IsRequired();
            //this.Property(s => s.ReportQuarter).IsRequired();
            this.Property(s => s.QuestionTypeId).IsRequired();
            //this.Property(s => s.ParentId).IsRequired();
            this.Property(s => s.QType).IsRequired();

            this.Property(s => s.QuestionName).HasMaxLength(60);

            this.Property(s => s.QuestionValue).IsRequired();

            this.Property(s => s.Status).IsRequired();
        }
    }
}
