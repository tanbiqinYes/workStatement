using CCWOnlineAPI.Management.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace CCWOnlineAPI.Management.Mapper
{
    public class AccountMapper : EntityTypeConfiguration<Account>
    {
        public AccountMapper() 
        {
            this.ToTable("PER_Account");

            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Id).IsRequired();

            this.Property(s => s.AccountName).HasMaxLength(60);

            this.Property(s => s.DepartMentId).IsRequired();
            this.Property(s => s.WorkPostId).IsRequired();
            this.Property(s => s.AccountRoleId).IsRequired();

            this.Property(s => s.DomainName).HasMaxLength(60);
            this.Property(s => s.DomainName).IsRequired();

            this.Property(s => s.Status).IsRequired();

            this.Property(s => s.Remark).HasMaxLength(256);
            this.Property(s => s.Token).HasMaxLength(256);
        }
    }
}
