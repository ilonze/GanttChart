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
    /// 该部分类用于处理甘特图的绘制渲染
    /// </summary>
    public partial class GanttChartView
    {
        /// <summary>
        /// 画背景
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="bottom"></param>
        protected virtual void DrawBack(Graphics graphics, int bottom)
        {
            graphics.FillRectangle(this.Brushes.BackBrush, _ContentControl.DisplayRectangle);
            var range = new RectangleF(new PointF(0, 0), new SizeF(TotalDays * DateWidth + RowHeaderWidth - _HScrollBar.Value, bottom - _VScrollBar.Value));
            graphics.SetClip(range);
            graphics.FillRectangle(this.Brushes.HeaderBackBrush, range);
            range = new RectangleF(new PointF(0, ColumnHeight), new SizeF(TotalDays * DateWidth + RowHeaderWidth - _HScrollBar.Value, bottom - ColumnHeight - _VScrollBar.Value));
            graphics.FillRectangle(this.Brushes.ContentBackBrush, range);
            if (_SelectionRow != null)
            {
                var selectionHeader = new Rectangle(1, _SelectionRow.Top + 1 - _VScrollBar.Value, RowHeaderWidth - 2, (int)(_SelectionRow.Bottom - _SelectionRow.Top - 1));
                var selectionContent = new Rectangle(RowHeaderWidth, _SelectionRow.Top + 1 - _VScrollBar.Value, (int)(TotalDays * DateWidth - _HScrollBar.Value - 1), selectionHeader.Height);
                graphics.FillRectangle(this.Brushes.RowSelectionHeaderBackBrush, selectionHeader);
                graphics.FillRectangle(this.Brushes.RowSelectionContentBackBrush, selectionContent);
            }
            graphics.ResetClip();
        }
        /// <summary>
        /// 画没有数据
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="bottom"></param>
        protected virtual void DrawNonData(Graphics graphics, int bottom)
        {

        }
        /// <summary>
        /// 画提示框
        /// </summary>
        /// <param name="graphics"></param>
        protected virtual void DrawItemTooltip(Graphics graphics)
        {
            if (!_IsShowTooltip) return;
            var size = TooltipSize;
            var border = new Rectangle(_ShowTooltipPoint, new Size(size.Width + RowSpacing * 2, size.Height + RowSpacing * 2));
            var content = new Point(_ShowTooltipPoint.X + RowSpacing, _ShowTooltipPoint.Y + RowSpacing);
            graphics.FillRectangle(this.Brushes.TooltipBackBrush, border);
            graphics.DrawRectangle(this.Brushes.TooltipBorderPen, border);
            graphics.DrawString(_ShowTooltipItem.Tooltip, TooltipTextFont, this.Brushes.TooltipTextBrush, content);
            //TextRenderer.DrawText(graphics, _ShowTooltipItem.Tooltip, TooltipTextFont, content, TooltipTextColor);
        }
        /// <summary>
        /// 画选中范围
        /// </summary>
        /// <param name="graphics"></param>
        protected virtual void DrawSelectionRange(Graphics graphics)
        {
            if (!AllowSelectRange || _SelectionRange.IsEmpty) return;
            graphics.FillRectangle(this.Brushes.TimeRangeSelecetionBackBrush, _SelectionRange);
        }
        /// <summary>
        /// 画选中的矩形
        /// </summary>
        /// <param name="graphics"></param>
        protected virtual void DrawSelectingRectangle(Graphics graphics)
        {
            if (!AllowSelectRange || _DragStartLocation.IsEmpty || _DragEndLocation.IsEmpty || _DragStartLocation == _DragEndLocation || _DragStartLocation.X <= RowHeaderWidth || _DragStartLocation.Y <= ColumnHeight) return;
            var row = GetRow(_DragStartLocation);
            if (row == null) return;
            var start = new Point(Math.Min(_DragStartLocation.X, Math.Max(_DragEndLocation.X, RowHeaderWidth)), Math.Min(_DragStartLocation.Y, Math.Max(_DragEndLocation.Y, row.Top)));
            var height = Math.Abs(Math.Min(_DragEndLocation.Y, row.Bottom - _VScrollBar.Value) - _DragStartLocation.Y);
            var width = Math.Abs(_DragEndLocation.X - _DragStartLocation.X);
            if (_DragEndLocation.Y < row.Top)
            {
                height -= row.Top - _DragEndLocation.Y;
            }
            if (_DragEndLocation.X < RowHeaderWidth)
            {
                width -= RowHeaderWidth - _DragEndLocation.X;
            }
            else if(_DragEndLocation.X > TotalDays * DateWidth + RowHeaderWidth - _HScrollBar.Value)
            {
                width = TotalDays * DateWidth + RowHeaderWidth - _HScrollBar.Value - _DragStartLocation.X;
            }
            var rect = new RectangleF(start, new SizeF(width, height));
            graphics.FillRectangle(this.Brushes.SelectionRectangleBackBrush, rect);
        }
        /// <summary>
        /// 画项的时间范围
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="item"></param>
        /// <param name="sub"></param>
        protected virtual void DrawItemTimeRange(Graphics graphics, GanttChartViewItem item, TimeRange sub)
        {
            var width = sub.Width;
            var right = sub.Left + sub.Width;
            if (right == item.Left + item.Width)
            {
                width = width - 2;
                if(width < 1)
                {
                    return;
                }
            }
            if (item == _SelectionItem)
            {
                graphics.FillRectangle(this.Brushes.ItemSelectionSubRangeBackBrush, sub.Left - _HScrollBar.Value, item.Top - _VScrollBar.Value, width, ItemHeight);
                graphics.DrawRectangle(this.Brushes.ItemSelectionSubRangeBackPen, sub.Left - _HScrollBar.Value, item.Top - _VScrollBar.Value, width, ItemHeight);
            }
            else
            {
                graphics.FillRectangle(this.Brushes.ItemSubRangeBackBrush, sub.Left - _HScrollBar.Value, item.Top - _VScrollBar.Value, width, ItemHeight);
                graphics.DrawRectangle(this.Brushes.ItemSubRangeBackPen, sub.Left - _HScrollBar.Value, item.Top - _VScrollBar.Value, width, ItemHeight);
            }
        }
        /// <summary>
        /// 画项
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="titleFormat"></param>
        /// <param name="item"></param>
        /// <param name="range"></param>
        protected virtual void DrawItem(Graphics graphics, StringFormat titleFormat, GanttChartViewItem item, RectangleF range)
        {
            var clip = new RectangleF(range.X, (range.Y < ColumnHeight ? ColumnHeight : range.Y) + 1, range.Width, (range.Y < ColumnHeight ? (range.Height - (ColumnHeight - range.Y)) : range.Height) - 2);
            graphics.SetClip(clip);
            if (item.Ranges != null && item.Ranges.Any())
            {
                graphics.FillRectangle(this.Brushes.ItemBackBrush, item.Left - _HScrollBar.Value, item.Top - _VScrollBar.Value, Math.Max(item.Width - 2, 2), ItemHeight);
                graphics.DrawRectangle(this.Brushes.ItemBackPen, item.Left - _HScrollBar.Value, item.Top - _VScrollBar.Value, Math.Max(item.Width - 2, 2), ItemHeight);
                var subs = item.Ranges.OrderBy(r => r.StartTime);
                foreach (var sub in subs)
                {
                    DrawItemTimeRange(graphics, item, sub);
                }
            }
            else
            {
                graphics.FillRectangle(this.Brushes.ItemSubRangeBackBrush, item.Left - _HScrollBar.Value, item.Top - _VScrollBar.Value, Math.Max(item.Width - 2, 2), ItemHeight);
                graphics.DrawRectangle(this.Brushes.ItemSubRangeBackPen, item.Left - _HScrollBar.Value, item.Top - _VScrollBar.Value, Math.Max(item.Width - 2, 2), ItemHeight);
            }
            if (item == _SelectionItem)
            {
                var textBrush = item.IsWarnning ? this.Brushes.ItemWarnningTextBrush : this.Brushes.ItemSelectionTextBrush;
                graphics.DrawLine(this.Brushes.ItemStartFlagPen, item.Left - _HScrollBar.Value + 1, item.Top - _VScrollBar.Value + ItemHeight / 2, item.Left - _HScrollBar.Value + 1, item.Top - _VScrollBar.Value + ItemHeight);
                graphics.DrawRectangle(this.Brushes.ItemSelectionBorderPen, item.Left - _HScrollBar.Value, item.Top - _VScrollBar.Value, Math.Max(item.Width - 2, 2), ItemHeight);
                graphics.DrawString(item.Title, ItemTextFont, textBrush, new RectangleF(item.Left - _HScrollBar.Value + 2, item.Top - _VScrollBar.Value + 1, 0, ItemHeight - 2), titleFormat);
            }
            else if(_SelectionItem != null && SelectionItemSameComparator != null && SelectionItemSameComparator(item, _SelectionItem))
            {
                var textBrush = item.IsWarnning ? this.Brushes.ItemWarnningTextBrush : this.Brushes.ItemSelectionTextBrush;
                graphics.DrawLine(this.Brushes.ItemStartFlagPen, item.Left - _HScrollBar.Value + 1, item.Top - _VScrollBar.Value + ItemHeight / 2, item.Left - _HScrollBar.Value + 1, item.Top - _VScrollBar.Value + ItemHeight);
                if (IsShowItemBorder)
                    graphics.DrawRectangle(this.Brushes.ItemBorderPen, item.Left - _HScrollBar.Value, item.Top - _VScrollBar.Value, Math.Max(item.Width - 2, 2), ItemHeight);
                graphics.DrawString(item.Title, ItemTextFont, textBrush, new RectangleF(item.Left - _HScrollBar.Value + 2, item.Top - _VScrollBar.Value + 1, 0, ItemHeight - 2), titleFormat);
            }
            else
            {
                var textBrush = item.IsWarnning ? this.Brushes.ItemWarnningTextBrush : this.Brushes.ItemTextBrush;
                graphics.DrawLine(this.Brushes.ItemStartFlagPen, item.Left - _HScrollBar.Value + 1, item.Top - _VScrollBar.Value + ItemHeight / 2, item.Left - _HScrollBar.Value + 1, item.Top - _VScrollBar.Value + ItemHeight);
                if (IsShowItemBorder)
                    graphics.DrawRectangle(this.Brushes.ItemBorderPen, item.Left - _HScrollBar.Value, item.Top - _VScrollBar.Value, Math.Max(item.Width - 2, 2), ItemHeight);
                graphics.DrawString(item.Title, ItemTextFont, textBrush, new RectangleF(item.Left - _HScrollBar.Value + 2, item.Top - _VScrollBar.Value + 1, 0, ItemHeight - 2), titleFormat);
            }
            graphics.ResetClip();
        }
        /// <summary>
        /// 画行
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="headerFormat"></param>
        /// <param name="titleFormat"></param>
        /// <param name="row"></param>
        /// <param name="range"></param>
        /// <param name="rowRect"></param>
        protected virtual void DrawRow(Graphics graphics, StringFormat headerFormat, StringFormat titleFormat, GanttChartViewRow row, RectangleF range, RectangleF rowRect)
        {
            graphics.SetClip(range);
            var drawY = rowRect.Y + RowSpacing;
            var drawHeight = rowRect.Height - RowSpacing * 2;
            if (drawHeight > (_ContentControl.Height - ColumnHeight) / 4)
            {
                drawY = Math.Max(rowRect.Y, ColumnHeight);
                drawHeight = Math.Min(rowRect.Bottom - RowSpacing, _ContentControl.Height) - drawY;
            }
            graphics.DrawString(row.Title, RowHeaderTextFont, this.Brushes.RowHeaderTextBrush, new RectangleF(rowRect.X + RowSpacing, drawY, RowHeaderWidth - RowSpacing * 2, drawHeight), headerFormat);
            graphics.ResetClip();
            if(row.Items != null)
            {
                var items = row.Items.OrderBy(r=>r.StartTime);
                foreach (var item in items)
                {
                    if (item.Left + item.Width - _HScrollBar.Value > RowHeaderWidth)
                    {
                        if (item.Left - _HScrollBar.Value < range.Right)
                        {
                            DrawItem(graphics, titleFormat, item, new RectangleF(RowHeaderWidth + 1, rowRect.Y + 1, rowRect.Width - RowHeaderWidth - 2, rowRect.Height - 2));
                        }
                    }
                }
            }
            graphics.SetClip(range);
            graphics.DrawLine(this.Brushes.GridPen, 0, row.Bottom - _VScrollBar.Value, rowRect.Width, row.Bottom - _VScrollBar.Value);
            if (_SelectionRow == row && IsShowSelectionBorder)
            {
                var selectionRect = new Rectangle(1, row.Top + 1, (int)(range.Width - 3), (int)(row.Bottom - row.Top - 2));
                graphics.DrawRectangle(this.Brushes.RowSelectionBorderPen, selectionRect);
            }
            graphics.ResetClip();
        }
        /// <summary>
        /// 画所有行
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="bottom"></param>
        protected virtual void DrawRows(Graphics graphics, int bottom)
        {
            var range = new RectangleF(0, ColumnHeight, RowHeaderWidth + TotalDays * DateWidth - _HScrollBar.Value, bottom - _VScrollBar.Value + 1);
            graphics.DrawLine(this.Brushes.GridPen, RowHeaderWidth, ColumnDateHeight, range.Width - 1, ColumnDateHeight);
            graphics.SetClip(range);
            graphics.DrawLine(this.Brushes.GridPen, 0, ColumnHeight, range.Width - 1, ColumnHeight);
            if (Rows == null || !Rows.Any()) 
            {
                DrawNonData(graphics, bottom);
                graphics.DrawLine(this.Brushes.GridPen, 0, bottom, range.Width - 1, bottom);
                return;
            }
            graphics.ResetClip();
            var headerFormat = new StringFormat();
            headerFormat.Alignment = StringAlignment.Near;
            headerFormat.LineAlignment = StringAlignment.Center;
            var titleFormat = new StringFormat();
            titleFormat.Alignment = StringAlignment.Near;
            titleFormat.LineAlignment = StringAlignment.Center;

            titleFormat.FormatFlags = BlockType == GanttChartViewItemBlockType.Compact ? StringFormatFlags.LineLimit : StringFormatFlags.NoWrap;
            foreach (var row in Rows)
            {
                if (row.Bottom - _VScrollBar.Value > ColumnHeight)
                {
                    var rowRect = new RectangleF(new PointF(0, row.Top - _VScrollBar.Value), new SizeF(range.Width, row.Bottom - row.Top + 1));
                    if (!range.IntersectsWith(rowRect)) break;
                    DrawRow(graphics, headerFormat, titleFormat, row, range, rowRect);
                }
            }
        }
        /// <summary>
        /// 画列
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="format"></param>
        /// <param name="date"></param>
        /// <param name="range"></param>
        /// <param name="startX"></param>
        /// <param name="width"></param>
        /// <param name="cellHour"></param>
        protected virtual void DrawColumn(Graphics graphics, StringFormat format, DateTime date, RectangleF range, int startX, int width, int cellHour)
        {
            var offsetMinutes = (int)(DateSplitTime - DateSplitTime.Date).TotalMinutes % (60 * cellHour);
            var offset = (int)(offsetMinutes / 60f * width);
            startX = startX - offset;
            var dt = date.AddMinutes(-offsetMinutes);
            for (DateTime i = dt; i < dt.AddDays(1); i = i.AddHours(cellHour))
            {
                var rectangle = new RectangleF(startX, ColumnDateHeight + 1, 0, ColumnHourHeight - 2);
                if (range.IntersectsWith(rectangle) && (offset != 0 || date.Hour != i.Hour))
                {
                    graphics.DrawString(i.Hour == 0 ? "0" : i.Hour.ToString("##00"), ColumnDateTextFont, this.Brushes.ColumnHourTextBrush, rectangle, format);
                    //graphics.DrawLine(this.Brushes.GridPen, rectangle.X, ColumnDateHeight, rectangle.X, ColumnDateHeight + ColumnHourHeight / 5);
                    graphics.DrawLine(this.Brushes.GridPen, rectangle.X, ColumnDateHeight + ColumnHourHeight / 5 * 4, rectangle.X, ColumnDateHeight + ColumnHourHeight - 1);
                }
                startX += width * cellHour;
            }
        }
        /// <summary>
        /// 画列头
        /// </summary>
        /// <param name="graphics"></param>
        protected virtual void DrawColumnHeader(Graphics graphics)
        {
            var range = new RectangleF(new PointF(RowHeaderWidth + 1, 1), new SizeF(TotalDays * DateWidth - 1, _ContentControl.Height - 2));
            graphics.SetClip(range);
            int cellHour = GetCellHours();
            var format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            format.FormatFlags = StringFormatFlags.LineLimit;
            var x = RowHeaderWidth;
            for (var i = StartTime; i <= EndTime; i = i.AddDays(1))
            {
                var dx = x - _HScrollBar.Value;
                var rectangle = new RectangleF(dx + 1, 1, DateWidth - 1, ColumnDateHeight - 2);
                if (rectangle.Right > RowHeaderWidth)
                {
                    if (!range.IntersectsWith(rectangle))
                    {
                        x += DateWidth;
                        continue;
                    }
                    graphics.DrawString(i.ToString("yyyy-MM-dd") + " 星期" + ("日一二三四五六"[(int)i.DayOfWeek]), ColumnDateTextFont, this.Brushes.ColumnDateTextBrush, rectangle, format);
                    DrawColumn(graphics, format, i, range, (int)rectangle.X - 1, DateWidth / 24, cellHour);
                }
                x += DateWidth;
            }
            graphics.ResetClip();
        }
        /// <summary>
        /// 画行头
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="bottom"></param>
        protected virtual void DrawHeader(Graphics graphics, int bottom)
        {
            var range = new RectangleF(new PointF(0, 0), new SizeF(RowHeaderWidth + TotalDays * DateWidth, bottom - _VScrollBar.Value));
            graphics.SetClip(range);
            graphics.DrawLine(this.Brushes.GridPen, range.X, range.Y, range.X + RowHeaderWidth, range.Y + ColumnHeight);
            graphics.DrawString(this.DateTimeShowText, this.ColumnDateTextFont, this.Brushes.ColumnDateTextBrush,
                new Rectangle(0, 0, RowHeaderWidth - RowSpacing, ColumnDateHeight),
                new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });
            graphics.DrawString(this.RowTitleShowText, this.RowHeaderTextFont, this.Brushes.RowHeaderTextBrush,
                new Rectangle(RowSpacing, ColumnDateHeight, RowHeaderWidth - RowSpacing, ColumnHourHeight - RowSpacing),
                new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far });
            graphics.ResetClip();
        }
        /// <summary>
        /// 画时间线
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="bottom"></param>
        protected virtual void DrawTimeLine(Graphics graphics, int bottom)
        {
            var range = new RectangleF(new PointF(0, 0), new SizeF(RowHeaderWidth + TotalDays * DateWidth, bottom - _VScrollBar.Value));
            graphics.SetClip(range);
            graphics.DrawRectangle(this.Brushes.GridPen, _ContentControl.DisplayRectangle);
            var x = RowHeaderWidth - 1;
            if(StartTime == DateTime.Today.Add(DateSplitTime - DateSplitTime.Date))
            {
                graphics.DrawLine(this.Brushes.CurrentTimeLinePen, x, 0, x, _ContentControl.Height);
            }
            else
            {
                graphics.DrawLine(this.Brushes.GridPen, x, 0, x, _ContentControl.Height);
            }
            for (var i = StartTime; i <= EndTime; i = i.AddDays(1))
            {
                x += DateWidth;
                var dx = x - _HScrollBar.Value;
                if (dx + 1 > RowHeaderWidth)
                {
                    if (dx + 1 > _ContentControl.Width) break;
                    if(i.AddDays(1) == DateTime.Today.Add(DateSplitTime - DateSplitTime.Date))
                    {
                        graphics.DrawLine(this.Brushes.CurrentTimeLinePen, dx, 0, dx, _ContentControl.Height);
                    }
                    else
                    {
                        graphics.DrawLine(this.Brushes.GridPen, dx, 0, dx, _ContentControl.Height);
                    }
                }
            }
            graphics.ResetClip();
        }
        /// <summary>
        /// 计算单元格小时
        /// </summary>
        /// <returns></returns>
        private int GetCellHours()
        {
            var halfHourWidth = DateWidth / 48;
            int cellHour;
            if (halfHourWidth >= 12)
            {
                cellHour = 1;
            }
            else if (halfHourWidth >= 6)
            {
                cellHour = 2;
            }
            else if (halfHourWidth >= 4)
            {
                cellHour = 3;
            }
            else if (halfHourWidth >= 3)
            {
                cellHour = 4;
            }
            else if (halfHourWidth >= 2)
            {
                cellHour = 6;
            }
            else
            {
                cellHour = 12;
            }
            return cellHour;
        }
    }
}
