using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;
using CCWOnlineAPI.Management.Entities;
using CCWOnlineAPI.Management.Mapper;

namespace CCWOnlineAPI.Management.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加个人资料数据，若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class Account : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Account> manager, string authenticationType)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // 在此处添加自定义用户声明
            return userIdentity;
        }

        /// <summary>
        /// 部门ID
        /// </summary>
        public string DepartMentId { get; set; }

        /// <summary>
        /// 岗位ID
        /// </summary>
        public string WorkPostId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string AccountRoleId { get; set; }

        /// <summary>
        /// 域账户名称
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 登录token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// token和个人信息更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<Account>
    {
        public ApplicationDbContext()
            : base("ManageMentConnection", throwIfV1Schema: false)
        {
        }

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
            base.OnModelCreating(modelBuilder);

            // 默认表名是AspNetUsers,我们可以把它改成任意我们想要的
            modelBuilder.Entity<IdentityUser>()
                .ToTable("PER_Account");
            modelBuilder.Entity<Account>()
                .ToTable("PER_Account");

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

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}