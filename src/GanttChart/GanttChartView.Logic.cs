using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GanttChart
{
    /// <summary>
    /// 甘特图控件
    /// 该部分类处理甘特图逻辑
    /// </summary>
    public partial class GanttChartView
    {
        /// <summary>
        /// 鼠标是否经过日分割线
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool IsHoverDateSplitLine(Point e)
        {
            var t = (e.X + _HScrollBar.Value - RowHeaderWidth + this.DateWidth) % this.DateWidth;
            return e.X > this.RowHeaderWidth + 3 && e.X < ((EndDate.Date - StartDate.Date).TotalDays + 1) * DateWidth + RowHeaderWidth - _HScrollBar.Value && (this.DateWidth - 2 <= t || t <= 1);
        }
        /// <summary>
        /// 鼠标是否经过行标题
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool IsHoverRowHeader(Point e)
        {
            return e.X <= this.RowHeaderWidth && e.Y > this.ColumnHeight;
        }
        /// <summary>
        /// 鼠标是否经过行
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool IsHoverRow(Point e)
        {
            var y = e.Y + _VScrollBar.Value;
            return  e.X < ((EndDate.Date - StartDate.Date).TotalDays + 1) * DateWidth + RowHeaderWidth - _HScrollBar.Value && e.Y > this.ColumnHeight && Rows != null ? Rows.Any(r=>r.Top <= y && r.Bottom >= y) : false;
        }
        /// <summary>
        /// 鼠标是否经过项
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool IsHoverItem(Point e)
        {
            if(e.X > this.RowHeaderWidth && e.Y > this.ColumnHeight && Rows != null)
            {
                var row = GetRow(e);
                if (row != null && row.Items != null && row.Items.Any())
                {
                    var x = e.X + _HScrollBar.Value;
                    var y = e.Y + _VScrollBar.Value;
                    return row.Items.Any(r=>r.Left<=x && x <= r.Left + Math.Max(r.Width, r.TextWidth) && r.Top <= y && y <= r.Top + ItemHeight);
                }
            }
            return false;
        }
        /// <summary>
        /// 获取项
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private GanttChartViewItem GetItem(Point e)
        {
            if (e.X > this.RowHeaderWidth && e.X < ((EndDate.Date - StartDate.Date).TotalDays + 1) * DateWidth + RowHeaderWidth - _HScrollBar.Value && e.Y > this.ColumnHeight && Rows != null)
            {
                var row = GetRow(e);
                if (row != null && row.Items != null && row.Items.Any())
                {
                    var x = e.X + _HScrollBar.Value;
                    var y = e.Y + _VScrollBar.Value;
                    return row.Items.OrderBy(r=>r.Left).LastOrDefault(r => r.Left <= x && x <= r.Left + Math.Max(r.Width, r.TextWidth) && r.Top <= y && y <= r.Top + ItemHeight);
                }
            }
            return null;
        }
        /// <summary>
        /// 获取行
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private GanttChartViewRow GetRow(Point e)
        {
            if (e.X < ((EndDate.Date - StartDate.Date).TotalDays + 1) * DateWidth + RowHeaderWidth - _HScrollBar.Value && e.Y > this.ColumnHeight && e.Y + _VScrollBar.Value < (Rows == null || !Rows.Any() ? 0 : Rows.Max(r=>r.Bottom)) && Rows != null)
            {
                var y = e.Y + _VScrollBar.Value;
                return Rows.FirstOrDefault(r => r.Top <= y && r.Bottom >= y);
            }
            return null;
        }
        /// <summary>
        /// 计算所有行和项的大小
        /// </summary>
        private void ComputeRowsAndItemsSize()
        {
            var minuteWidth = DateWidth / 24f / 60f;
            var currentRowY = ColumnHeight;
            foreach (var row in Rows)
            {
                row.Top = currentRowY;
                var currentItemY = currentRowY + RowSpacing;
                row.Bottom = currentRowY;
                if(row.Items != null)
                {
                    var beforeTop = row.Top + RowSpacing;
                    var beforeRight = 0;
                    var items = row.Items.OrderBy(r => r.StartTime).ToList();
                    for ( int i = 0; i < items.Count; i++)
                    {
                        var item = items[i];
                        if (item.EndTime >= StartTime && item.StartTime <= EndTime && item.EndTime >= item.StartTime)
                        {
                            item.Left = (int)((item.StartTime - StartTime).TotalMinutes * minuteWidth) + RowHeaderWidth;
                            item.Width = (int)((item.EndTime - item.StartTime).TotalMinutes * minuteWidth);
                            item.TextWidth = TextRenderer.MeasureText(item.Title, ItemTextFont, System.Drawing.Size.Empty).Width;
                            if (item.Left < beforeRight || BlockType == GanttChartViewItemBlockType.Block && i > 0)
                            {
                                var its = items.Take(i).GroupBy(r=>r.Top).Select(r=>r.OrderByDescending(p => p.Left + p.TextWidth).First());
                                if(BlockType == GanttChartViewItemBlockType.Return && its.Any(r=>r.Left + r.TextWidth < item.Left))
                                {
                                    var it = its.First(r => r.Left + r.TextWidth < item.Left);
                                    item.Top = it.Top;
                                    beforeTop = it.Top;
                                }
                                else
                                {
                                    if(BlockType == GanttChartViewItemBlockType.Return && its.Any())
                                    {
                                        beforeTop = its.Max(r => r.Top);
                                    }
                                    item.Top = beforeTop + ItemHeight + ItemSpacing;
                                    beforeTop += ItemHeight + ItemSpacing;
                                }
                            }
                            else
                            {
                                if (i > 0)
                                {
                                    if (BlockType == GanttChartViewItemBlockType.Return && beforeTop != row.Top + RowSpacing)
                                    {
                                        beforeTop = row.Top + RowSpacing;
                                    }
                                }
                                item.Top = beforeTop;
                            }
                            if (BlockType == GanttChartViewItemBlockType.Compact)
                            {
                                beforeRight = item.Left + item.Width;
                            }
                            else
                            {
                                beforeRight = item.Left + Math.Max(item.TextWidth, item.Width);
                            }
                            if (item.Ranges != null && item.Ranges.Any())
                            {
                                var subs = item.Ranges.OrderBy(r => r.StartTime);
                                foreach (var sub in subs)
                                {
                                    sub.Left = item.Left + (int)((sub.StartTime - item.StartTime).TotalMinutes * minuteWidth);
                                    sub.Width = (int)((sub.EndTime - sub.StartTime).TotalMinutes * minuteWidth);
                                }
                            }
                        }
                        else
                        {
                            //row.Items.Remove(item);
                            //i--;
                        }
                    }
                }
                if (row.Items == null || !row.Items.Any(r=>r.Left > 0))
                {
                    row.Bottom = row.Top + ItemHeight + RowSpacing * 2;
                }
                else
                {
                    row.Bottom = Math.Max(row.Items.Max(r => r.Top), ColumnHeight + RowSpacing) + ItemHeight + RowSpacing;
                }
                currentRowY = row.Bottom;
            }
        }
        /// <summary>
        /// 计算提示框的位置
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private Point ComputeTooltipLocation(Point e)
        {
            var size = TooltipOuterSize;
            var x = e.X;
            if (e.X + size.Width > _ContentControl.Width)
            {
                var t = e.X - size.Width;
                if (t < 0 && 0 - t < (e.X + size.Width) - _ContentControl.Width || t >= 0)
                {
                    x = t;
                }
            }
            var y = _ShowTooltipItem.Top - _VScrollBar.Value + ItemHeight + ItemSpacing;
            if(y + size.Height > _ContentControl.Height)
            {
                var t = _ShowTooltipItem.Top - _VScrollBar.Value - ItemSpacing - size.Height;
                if (t < 0 && 0 - t < (y + size.Height) - _ContentControl.Height || t >= 0)
                {
                    y = t;
                }
            }
            return new Point(x, y);
        }
    }
}
