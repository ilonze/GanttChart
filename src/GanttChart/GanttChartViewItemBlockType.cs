using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanttChart
{
    /// <summary>
    /// 甘特图项的换行模式
    /// </summary>
    public enum GanttChartViewItemBlockType
    {
        /// <summary>
        /// 紧凑
        /// </summary>
        Compact,
        /// <summary>
        /// 文字超过换行
        /// </summary>
        TextOverBlock,
        /// <summary>
        /// 强制换行
        /// </summary>
        Block,
        /// <summary>
        /// 回归
        /// </summary>
        Return
    }
}
