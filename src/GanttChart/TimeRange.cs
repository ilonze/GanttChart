using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanttChart
{
    /// <summary>
    /// 时间范围
    /// </summary>
    public class TimeRange
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 左边
        /// </summary>
        internal int Left { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        internal int Width { get; set; }
    }
}
