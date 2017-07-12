using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CCWOnlineAPI.Management.Entities;

namespace CCWOnlineAPI.Management.Mapper
{
    public class QuestionInfoMapper : EntityTypeConfiguration<QuestionInfo>
    {
        public QuestionInfoMapper()
        {
            this.ToTable("PER_QuestionInfo");

            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            this.Property(s => s.QuestionId).IsRequired();

            this.Property(s => s.Sequence).IsRequired();

            this.Property(s => s.QuestionItemName).HasMaxLength(512);

            this.Property(s => s.QuestionItemValue).IsRequired();

            this.Property(s => s.Status).IsRequired();
        }
    }
}

