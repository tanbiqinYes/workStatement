using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCWOnlineAPI.Management.Entities
{
    /// <summary>
    /// 题库考核题
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 问题ID
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// 答题类型
        /// </summary>
        public int AnswerType { get; set; }

        /// <summary>
        /// 提交用户
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// 上级/同事
        /// </summary>
        public int OtherAccount { get; set; }

        /// <summary>
        /// 考核类型
        /// </summary>
        public string AnswerValue { get; set; }

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
