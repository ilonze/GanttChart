using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanttChart
{
    /// <summary>
    /// 甘特图项
    /// </summary>
    public class GanttChartViewItem : TimeRange
    {
        /// <summary>
        /// 项的时间范围集合
        /// </summary>
        public ICollection<TimeRange> Ranges { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 提示内容
        /// </summary>
        public string Tooltip { get; set; }
        /// <summary>
        /// 顶部位置
        /// </summary>
        internal int Top { get; set; }
        /// <summary>
        /// 文本宽度
        /// </summary>
        internal int TextWidth { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 是否警告
        /// </summary>
        public bool IsWarnning { get; set; }
    }
}
