using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using CCWOnlineAPI.Management.Entities;

namespace CCWOnlineAPI.Management.Mapper
{
    public class QuestionTypeMapper : EntityTypeConfiguration<QuestionType>
    {   
        public QuestionTypeMapper()
        {
            this.ToTable("PER_QuestionType");

            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            this.Property(s => s.ReportYear).IsRequired();
            this.Property(s => s.ReportQuarter).IsRequired();

            this.Property(s => s.Questiontype).HasMaxLength(60);

            this.Property(s => s.QuestionTypeValue).IsRequired();

            this.Property(s => s.Status).IsRequired();
            this.Property(s => s.AccountId).IsRequired();        
        }
    }
}
