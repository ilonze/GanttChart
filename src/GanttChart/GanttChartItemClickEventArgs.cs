using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanttChart
{
    /// <summary>
    /// 甘特图项单击事件参数
    /// </summary>
    public class GanttChartItemClickEventArgs : EventArgs
    {
        /// <summary>
        /// 项
        /// </summary>
        public GanttChartViewItem Item { get; private set; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="item"></param>
        public GanttChartItemClickEventArgs(GanttChartViewItem item)
        {
            Item = item;
        }
    }
}
