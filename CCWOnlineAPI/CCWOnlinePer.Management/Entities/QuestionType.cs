using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCWOnlineAPI.Management.Entities
{
    /// <summary>
    /// 题库考核类型表
    /// </summary>
    public class QuestionType
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
        /// 考核类型
        /// </summary>
        public string Questiontype { get; set; }

        /// <summary>
        /// 权重分值
        /// </summary>
        public int QuestionTypeValue { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int AccountId { get; set; }

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
