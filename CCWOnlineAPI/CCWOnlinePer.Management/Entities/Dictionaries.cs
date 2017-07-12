using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCWOnlineAPI.Management.Entities
{
    /// <summary>
    /// 绩效考核字典表
    /// </summary>
    public class Dictionaries
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        public int DicType { get; set; }

        /// <summary>
        /// 字典描述
        /// </summary>
        public string DicName { get; set; }
        
        /// <summary>
        /// 字典状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备用字段
        /// </summary>
        public string Remark { get; set; }
    }
}
