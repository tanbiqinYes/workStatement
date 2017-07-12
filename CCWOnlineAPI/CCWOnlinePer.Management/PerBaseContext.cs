﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CCWOnlineAPI.Management
{
    using CCWOnlineAPI.Management.Entities;
    using CCWOnlineAPI.Management.Mapper;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PerBaseContext : DbContext
    {
        public PerBaseContext()
            : base("name=ManageMentConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        //public DbSet<Account> Account { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Dictionaries> Dictionaries { get; set; }
        public DbSet<PercentConfig> PercentConfig { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<QuestionInfo> QuestionInfo { get; set; }
        public DbSet<QuestionType> QuestionType { get; set; }
        public DbSet<RelationPerformance> RelationPerformance { get; set; }
        public DbSet<ReportConfig> ReportConfig { get; set; }
        public DbSet<ReportInfo> ReportInfo { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<TaskEmailContent> TaskEmailContent { get; set; }
        public DbSet<TaskManagement> TaskManagement { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new AccountMapper());
            modelBuilder.Configurations.Add(new AnswerMapper());
            modelBuilder.Configurations.Add(new DictionariesMapper());
            modelBuilder.Configurations.Add(new PercentConfigMapper());
            modelBuilder.Configurations.Add(new QuestionInfoMapper());
            modelBuilder.Configurations.Add(new QuestionMapper());
            modelBuilder.Configurations.Add(new QuestionTypeMapper());
            modelBuilder.Configurations.Add(new RelationPerformanceMapper());
            modelBuilder.Configurations.Add(new ReportConfigMapper());
            modelBuilder.Configurations.Add(new ReportInfoMapper());
            modelBuilder.Configurations.Add(new RoleMapper());
            modelBuilder.Configurations.Add(new TaskEmailContentMapper());
            modelBuilder.Configurations.Add(new TaskManagementMapper());

            base.OnModelCreating(modelBuilder);
        }
    }
}
