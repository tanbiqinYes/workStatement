using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CCWOnlineAPI.Management.Entities;

namespace CCWOnlineAPI.Management.Mapper
{
    public class AnswerMapper : EntityTypeConfiguration<Answer>
    {
        public AnswerMapper()
        {
            this.ToTable("PER_Answer");

            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            this.Property(s => s.QuestionId).IsRequired();
            this.Property(s => s.AnswerType).IsRequired();
            this.Property(s => s.AccountId).IsRequired();
            this.Property(s => s.OtherAccount).IsRequired();

            this.Property(s => s.AnswerValue).HasMaxLength(1024);

            this.Property(s => s.Status).IsRequired();
        }
    }
}
