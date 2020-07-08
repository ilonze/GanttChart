using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanttChart
{
    /// <summary>
    /// 甘特图行
    /// </summary>
    public class GanttChartViewRow
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 顶部
        /// </summary>
        internal int Top { get; set; }
        /// <summary>
        /// 按钮
        /// </summary>
        internal int Bottom { get; set; }
        /// <summary>
        /// 项集合
        /// </summary>
        public List<GanttChartViewItem> Items { get; set; }
    }
}
