using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCWOnlineAPI.Management.Entities
{
    /// <summary>
    /// 任务管理配置表
    /// </summary>
    public class TaskManagement
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 任务年份
        /// </summary>
        public int TaskYear { get; set; }

        /// <summary>
        /// 任务所在季度
        /// </summary>
        public int Quarter { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }  

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
