using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanttChart
{
    /// <summary>
    /// 甘特图属性改变事件参数
    /// </summary>
    public class GanttChartPropertyChangeEventArgs : EventArgs
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public GanttChartPropertyChangeEventArgs(string name, object value)
        {
            PropertyName = name;
            PropertyValue = value;
        }
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public object PropertyValue { get; set; }
    }
}
