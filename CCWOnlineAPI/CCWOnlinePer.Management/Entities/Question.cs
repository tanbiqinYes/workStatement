using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCWOnlineAPI.Management.Entities
{
    /// <summary>
    /// 题库考核题干表
    /// </summary>
    public class Question
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id { get; set; }

        ///// <summary>
        ///// 年份
        ///// </summary>
        //public int ReportYear { get; set; }

        ///// <summary>
        ///// 季度
        ///// </summary>
        //public int ReportQuarter { get; set; }

        /// <summary>
        /// 考核类型
        /// </summary>
        public string QuestionTypeId { get; set; }

        ///// <summary>
        ///// 题干ID
        ///// </summary>
        //public int ParentId { get; set; }

        /// <summary>
        /// 题目类型
        /// </summary>
        public int QType { get; set; }

        /// <summary>
        /// 题干/选项
        /// </summary>
        public string QuestionName { get; set; }

        /// <summary>
        /// 题目满分
        /// </summary>
        public int QuestionValue { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sequence { get; set; }

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
