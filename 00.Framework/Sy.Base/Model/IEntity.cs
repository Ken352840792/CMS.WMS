using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Base
{
    /// <summary>
    /// 数据模型接口
    /// </summary>
    public interface IEntity<out TKey>
    {
        /// <summary>
        /// 获取 实体唯一标识，主键
        /// </summary>
        TKey Id { get; }

        DateTime? CreateTime { get; set; }

        DateTime? UpdateTime { get; set; }
    }
}
