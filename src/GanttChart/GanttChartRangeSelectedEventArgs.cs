using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanttChart
{
    /// <summary>
    /// 甘特图范围选择事件参数
    /// </summary>
    public class GanttChartRangeSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// 事件范围
        /// </summary>
        public TimeRange Range { get; private set; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="range"></param>
        public GanttChartRangeSelectedEventArgs(TimeRange range)
        {
            Range = range;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        public GanttChartRangeSelectedEventArgs(DateTime startTime, DateTime endTime)
        {
            Range = new TimeRange { StartTime = startTime, EndTime = endTime };
        }
    }
}
