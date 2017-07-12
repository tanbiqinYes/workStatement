using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCWOnlineAPI.Management.Entities
{
    /// <summary>
    /// 用户信息表 
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 账户名称
        /// </summary>
        public string AccountName { get; set; }

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
}
