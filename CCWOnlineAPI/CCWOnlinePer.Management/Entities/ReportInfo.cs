using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCWOnlineAPI.Management.Entities
{
    /// <summary>
    /// 工作报告信息表
    /// </summary>
    public class ReportInfo
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
        /// 月份
        /// </summary>
        public int ReportMonth { get; set; }

        /// <summary>
        /// 固定问题1 填写
        /// </summary>
        public string Q1 { get; set; }

        /// <summary>
        /// 固定问题2 填写
        /// </summary>
        public string Q2 { get; set; }

        /// <summary>
        /// 固定问题3 填写
        /// </summary>
        public string Q3 { get; set; }

        /// <summary>
        /// 固定问题4 填写
        /// </summary>
        public string Q4 { get; set; }

        /// <summary>
        /// 固定问题5 填写
        /// </summary>
        public string Q5 { get; set; }

        /// <summary>
        /// 固定问题6 填写
        /// </summary>
        public string Q6 { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } 
    }
}
