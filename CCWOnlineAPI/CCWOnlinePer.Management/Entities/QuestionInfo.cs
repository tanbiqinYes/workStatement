using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCWOnlineAPI.Management.Entities
{
    /// <summary>
    /// 题库考核题干表
    /// </summary>
    public class QuestionInfo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 题干ID
        /// </summary>
        public string QuestionId { get; set; }

        /// <summary>
        /// 选项名称
        /// </summary>
        public string QuestionItemName { get; set; }

        /// <summary>
        /// 选项满分
        /// </summary>
        public int QuestionItemValue { get; set; }

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

