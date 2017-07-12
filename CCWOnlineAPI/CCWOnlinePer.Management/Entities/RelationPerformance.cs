using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCWOnlineAPI.Management.Entities
{
    /// <summary>
    /// 考核关系表
    /// </summary>
    public class RelationPerformance
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 账户Id
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public int ReportYear { get; set; }

        /// <summary>
        /// 季度
        /// </summary>
        public int Quarter { get; set; }

        /// <summary>
        /// 关系类型
        /// </summary>
        public int RelationType { get; set; }    

        /// <summary>
        /// 关联用户ID
        /// </summary>
        public int RelationAccountId { get; set; }

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
