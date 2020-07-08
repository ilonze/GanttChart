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
    /// 该部分类主要处理事件
    /// </summary>
    public partial class GanttChartView
    {
        private bool _IsShiftPress = false;
        private bool _IsCtrlPress = false;
        private bool _IsAltPress = false;
        private bool _IsMouseDown = false;
        private bool _IsAjustSize = false;
        private bool _IsDragItem = false;
        private bool _IsShowTooltip = false;
        private Point _DragStartLocation = Point.Empty;
        private Point _DragEndLocation = Point.Empty;
        private Point _ShowTooltipPoint = Point.Empty;
        private Rectangle _SelectionRange = Rectangle.Empty;
        private GanttChartViewItem _DragItem = null;
        private GanttChartViewItem _ShowTooltipItem = null;
        public event EventHandler<GanttChartPropertyChangeEventArgs> PropertyChanged;
        public event EventHandler<GanttChartItemClickEventArgs> ItemClick;
        public event EventHandler<GanttChartItemDoubleClickEventArgs> ItemDoubleClick;
        public event EventHandler<GanttChartRowSelectedEventArgs> RowSelected;
        public event EventHandler<GanttChartRangeSelectedEventArgs> RangeSelected;

        /// <summary>
        /// 初始化事件
        /// </summary>
        protected void InitEvent()
        {
            _VScrollBar.Scroll += new ScrollEventHandler(VScrollBar_Scroll);
            _HScrollBar.Scroll += new ScrollEventHandler(HScrollBar_Scroll);
            _ContentControl.Paint += OnContentControlPaint;
            _ContentControl.MouseDown += OnMouseDown;
            _ContentControl.MouseUp += OnMouseUp;
            _ContentControl.MouseMove += OnMouseMove;
            _ContentControl.MouseClick += OnMouseClick;
            _ContentControl.MouseDoubleClick += OnMouseDoubleClick;
        }
        /// <summary>
        /// 销毁事件
        /// </summary>
        protected void DisposeEvent()
        {

        }
        /// <summary>
        /// 键盘按下事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            _IsShiftPress = e.Shift;
            _IsCtrlPress = e.Control;
            _IsAltPress = e.Alt;
            base.OnKeyDown(e);
        }
        /// <summary>
        /// 键盘弹起事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            _IsShiftPress = e.Shift;
            _IsCtrlPress = e.Control;
            _IsAltPress = e.Alt;
            base.OnKeyUp(e);
        }
        /// <summary>
        /// 鼠标单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (_DragStartLocation != Point.Empty && e.X != _DragStartLocation.X && _DragStartLocation.Y != e.Y)
            {
                return;
            }
            if (IsHoverRowHeader(e.Location) || IsHoverRow(e.Location))
            {
                _SelectionRow = GetRow(e.Location);
                Invalidate();
                RowSelected?.Invoke(this, new GanttChartRowSelectedEventArgs(_SelectionRow));
            }
            if (IsHoverItem(e.Location))
            {
                _SelectionItem = GetItem(e.Location);
                Invalidate();
                ItemClick?.Invoke(this, new GanttChartItemClickEventArgs(_SelectionItem));
            }
        }
        /// <summary>
        /// 鼠标双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (IsHoverItem(e.Location))
            {
                _SelectionItem = GetItem(e.Location);
                Invalidate();
                if(ItemDoubleClick != null)
                {
                    if (_IsShowTooltip)
                    {
                        _IsShowTooltip = false;
                        _ShowTooltipItem = null;
                        _ShowTooltipPoint = Point.Empty;
                    }
                    ItemDoubleClick(this, new GanttChartItemDoubleClickEventArgs(_SelectionItem));
                }
            }
        }
        /// <summary>
        /// 鼠标滚动事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (_IsShowTooltip)
            {
                _IsShowTooltip = false;
                _ShowTooltipItem = null;
                _ShowTooltipPoint = Point.Empty;
            }
            //int i = e.Delta > 0 ? -1 : 1;
            var scrollbar = _IsShiftPress ? _HScrollBar : (ScrollBar)_VScrollBar;
            var h = scrollbar.Value - (int)(e.Delta * MouseWheelSensitivity);
            if (h > scrollbar.Maximum)
            {
                h = scrollbar.Maximum;
            }
            if (h < scrollbar.Minimum)
            {
                h = scrollbar.Minimum;
            }
            scrollbar.Value = h;
            Invalidate();
            base.OnMouseWheel(e);
        }
        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (_SelectionRow != null || _SelectionItem != null || _DragItem != null)
            {
                _SelectionRow = null;
                _SelectionItem = null;
                _DragItem = null;
                Invalidate();
            }
            _IsMouseDown = e.Button == MouseButtons.Left && true;
            if (_IsMouseDown)
            {
                _SelectionRange = Rectangle.Empty;
                _SelectionTimeRange = null;
                _DragStartLocation = e.Location;
                if (IsHoverDateSplitLine(e.Location) && !IsHoverItem(e.Location))
                {
                    _IsAjustSize = true;
                }
                else if (IsHoverItem(e.Location))
                {
                    _IsDragItem = true;
                    _DragItem = GetItem(e.Location);
                }
            }
        }
        /// <summary>
        /// 控件创建事件
        /// </summary>
        protected override void OnCreateControl()
        {
            this.Brushes = new GanttChartBrushes(this);
            _HScrollBar.Width = this.Width - VScrollWidth;
            _VScrollBar.Height = this.Height - HScrollHeight;
            AdjustScrollbar();
            base.OnCreateControl();
        }
        /// <summary>
        /// 控件大小变化事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            _HScrollBar.Width = this.Width - VScrollWidth;
            _VScrollBar.Height = this.Height - HScrollHeight;
            AdjustScrollbar();
            Invalidate();
            base.OnSizeChanged(e);
        }
        /// <summary>
        /// 控件绘制事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnContentControlPaint(object sender, PaintEventArgs e)
        {
            int bottom = ColumnHeight + ItemHeight + RowSpacing * 2;
            if (Rows != null && Rows.Any())
            {
                var last = Rows.Last();
                bottom = last.Bottom;
            }
            DrawBack(e.Graphics, bottom);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            DrawHeader(e.Graphics, bottom);
            DrawColumnHeader(e.Graphics);
            DrawTimeLine(e.Graphics, bottom);

            DrawSelectingRectangle(e.Graphics);

            DrawSelectionRange(e.Graphics);

            DrawRows(e.Graphics, bottom);

            DrawItemTooltip(e.Graphics);
        }
        /// <summary>
        /// 鼠标弹起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (_IsMouseDown)
            {
                if (_IsAjustSize)
                {
                    _IsAjustSize = false;
                    var w = e.X - _DragStartLocation.X + DateWidth;
                    if (w < MinDateWidth)
                    {
                        DateWidth = MinDateWidth;
                    }
                    else
                    {
                        DateWidth = (w / 48 + (w % 48 == 0 ? 0 : 1)) * 48;
                    }
                    if(Rows != null)
                    {
                        Rows.IsChanged = true;
                    }
                    AdjustScrollbar();
                    Invalidate();
                }
                else if (_IsDragItem)
                {
                    _IsDragItem = false;
                    //TODO:触发Drag事件
                    Invalidate();
                }
                else if(!_DragStartLocation.IsEmpty && e.X != _DragStartLocation.X && e.Y != _DragStartLocation.Y && _DragStartLocation.X > RowHeaderWidth && _DragStartLocation.Y > ColumnHeight)
                {
                    _DragEndLocation = e.Location;
                    var row = GetRow(_DragStartLocation);
                    if (row != null)
                    {
                        var width = Math.Abs(_DragEndLocation.X - _DragStartLocation.X);
                        if (_DragEndLocation.X < RowHeaderWidth)
                        {
                            width -= RowHeaderWidth - _DragEndLocation.X;
                        }
                        else if (_DragEndLocation.X > ((int)(EndDate.Date - StartDate.Date).TotalDays + 1) * DateWidth + RowHeaderWidth - _HScrollBar.Value)
                        {
                            width = ((int)(EndDate.Date - StartDate.Date).TotalDays + 1) * DateWidth + RowHeaderWidth - _HScrollBar.Value - _DragStartLocation.X;
                        }
                        var start = new Point(Math.Min(_DragStartLocation.X, Math.Max(_DragEndLocation.X, RowHeaderWidth)), row.Top + 1);
                        _SelectionRange = new Rectangle(start, new Size(width, row.Bottom - row.Top - 2));

                        Invalidate();
                        var mw = DateWidth / 24f / 60f;
                        var ts = StartDate.AddMinutes((int)((_DragStartLocation.X - RowHeaderWidth + _HScrollBar.Value) / mw));
                        var te = StartDate.AddMinutes((int)((_DragEndLocation.X - RowHeaderWidth + _HScrollBar.Value) / mw));
                        if (te < ts)
                        {
                            var tt = te;
                            te = ts;
                            ts = tt;
                        }
                        _SelectionTimeRange = new TimeRange { StartTime = ts, EndTime = te };
                        RangeSelected?.Invoke(this, new GanttChartRangeSelectedEventArgs(ts, te));
                    }
                }
            }
            _IsMouseDown = false;
            _DragStartLocation = Point.Empty;
            _DragEndLocation = Point.Empty;
        }
        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_IsMouseDown)
            {
                if (_IsShowTooltip)
                {
                    _IsShowTooltip = false;
                    _ShowTooltipItem = null;
                    _ShowTooltipPoint = Point.Empty;

                    Invalidate();
                }
                if (_IsDragItem)
                {

                }
                else if (_DragStartLocation.X > RowHeaderWidth && _DragStartLocation.Y > ColumnHeight)
                {
                    //TODO: 画选择框
                    _DragEndLocation = e.Location;
                    Invalidate();
                }
                return;
            }
            else if (IsHoverDateSplitLine(e.Location) && !IsHoverItem(e.Location))
            {
                Cursor = Cursors.VSplit;
            }
            //else if (IsHoverRowHeader(e))
            //{
            //    Cursor = DefaultCursor;
            //}
            else if (IsHoverItem(e.Location))
            {
                Cursor = Cursors.Hand;
                //TODO:显示Tooltip
                if (!_IsShowTooltip || GetItem(e.Location) != _ShowTooltipItem)
                {
                    _IsShowTooltip = true;
                    _ShowTooltipItem = GetItem(e.Location);
                    _ShowTooltipPoint = ComputeTooltipLocation(e.Location);

                    Invalidate();
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                Cursor = DefaultCursor;
            }
            if (_IsShowTooltip)
            {
                _IsShowTooltip = false;
                _ShowTooltipItem = null;
                _ShowTooltipPoint = Point.Empty;

                Invalidate();
            }
        }


        /// <summary>
        /// 横向滚动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }
        /// <summary>
        /// 纵向滚动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }
        /// <summary>
        /// 属性改变事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        private void OnPropertyChanged(string name, object value)
        {
            this.Brushes?.Refresh();
            var args = new GanttChartPropertyChangeEventArgs(name, value);
            PropertyChanged?.Invoke(this, args);
            if(name == nameof(BlockType))
            {
                Rows.IsChanged = true;
            }
            AdjustScrollbar();
            Invalidate();
        }
    }
}
