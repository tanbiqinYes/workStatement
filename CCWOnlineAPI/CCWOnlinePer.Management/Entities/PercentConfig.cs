using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCWOnlineAPI.Management.Entities
{
    /// <summary>
    /// 考核权重表
    /// </summary>
    public class PercentConfig
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public int ReportYear { get; set; }

        /// <summary>
        /// 季度
        /// </summary>
        public int ReportQuarter { get; set; }

        /// <summary>
        /// 权重名称
        /// </summary>
        public string PercentName { get; set; }

        /// <summary>
        /// 权重分值
        /// </summary>
        public double PercentValue { get; set; }

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
