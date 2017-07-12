using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCWOnlineAPI.Management.Entities
{
    /// <summary>
    /// 任务管理邮件内容表(邮件模板)
    /// </summary>
    public class TaskEmailContent
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string EmailContent { get; set; }

        /// <summary>
        /// 工作报告邮件内容
        /// </summary>
        public string ReportEmailContent { get; set; }

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
