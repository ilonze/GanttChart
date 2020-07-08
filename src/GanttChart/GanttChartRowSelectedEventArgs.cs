using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanttChart
{
    /// <summary>
    /// 甘特图行选中事件参数
    /// </summary>
    public class GanttChartRowSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// 行
        /// </summary>
        public GanttChartViewRow Row { get; private set; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="row"></param>
        public GanttChartRowSelectedEventArgs(GanttChartViewRow row)
        {
            Row = row;
        }
    }
}
