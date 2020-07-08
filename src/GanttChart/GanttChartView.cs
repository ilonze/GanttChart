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
    /// </summary>
    [ToolboxBitmap(typeof(GanttChartView), "GanttChart.GanttChartView.bmp")]
    public partial class GanttChartView: Control
    {
        #region Private Members
        private HScrollBar _HScrollBar;
        private VScrollBar _VScrollBar;

        private TableLayoutPanel _Table;
        private Control _ContentControl;

        private GanttChartBrushes Brushes;
        #endregion

        #region ctor
        public GanttChartView()
        {
            _Table = new TableLayoutPanel();
            _Table.ColumnCount = 2;
            _Table.RowCount = 2;
            _Table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            _Table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, VScrollWidth));
            _Table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _Table.RowStyles.Add(new RowStyle(SizeType.Absolute, HScrollHeight));

            _ContentControl = new GanttChartCanvas();
            _ContentControl.Dock = DockStyle.Fill;
            _ContentControl.Margin = new Padding(0);
            _Table.Controls.Add(_ContentControl, 0, 0);

            _HScrollBar = new HScrollBar();
            _HScrollBar.Value = 0;
            _HScrollBar.Height = HScrollHeight;
            _HScrollBar.SmallChange = 1;
            _HScrollBar.LargeChange = 2;
            //_HScrollBar.Dock = DockStyle.Fill;
            _HScrollBar.Visible = true;
            _Table.Controls.Add(_HScrollBar, 0, 1);
            

            _VScrollBar = new VScrollBar();
            _VScrollBar.Value = 0;
            _VScrollBar.Width = VScrollWidth;
            _VScrollBar.SmallChange = 1;
            _VScrollBar.LargeChange = 2;
            //_VScrollBar.Dock = DockStyle.Fill;
            _VScrollBar.Visible = true;
            _Table.Controls.Add(_VScrollBar, 1, 0);

            _Table.Dock = DockStyle.Fill;
            Controls.Add(_Table);

            InitEvent();
        }

        #endregion
        /// <summary>
        /// 调整滚动条
        /// </summary>
        private void AdjustScrollbar()
        {
            var bottom = ItemHeight + RowSpacing * 2;
            if (Rows != null && Rows.Any())
            {
                if (StartDate != Rows.StartDate || Rows.IsChanged)
                {
                    ComputeRowsAndItemsSize();
                    Rows.IsChanged = false;
                    Rows.StartDate = StartDate;
                }
                var last = Rows.Last();
                bottom = last.Bottom;
            }
            if (IsFixedRowHeight)
            {
                var h = ColumnHeight + (Rows?.Count ?? 0) * RowHeight + 200;
                if(h > Height - HScrollHeight)
                {
                    _VScrollBar.Maximum = h - Height + HScrollHeight + 1;
                }
                else
                {
                    _VScrollBar.Maximum = 0;
                }
            }
            else
            {
                _VScrollBar.Maximum = bottom - Height + HScrollHeight + 1 + 200;
            }
            _VScrollBar.Minimum = 0;

            _HScrollBar.Maximum = ((int)(EndDate.Date - StartDate.Date).TotalDays + 1) * DateWidth - Width + RowHeaderWidth + VScrollWidth + 1 + 200;
            _HScrollBar.Minimum = 0;

            _VScrollBar.Enabled = _VScrollBar.Maximum > 0;
            _HScrollBar.Enabled = _HScrollBar.Maximum > 0;
        }
        /// <summary>
        /// 重绘
        /// </summary>
        public new void Invalidate()
        {
            base.Invalidate();
            _ContentControl.Invalidate();
        }
        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            this.Brushes?.Dispose();
            base.Dispose(disposing);
        }
        class GanttChartCanvas : Control
        {
            public GanttChartCanvas()
            {
                SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                SetStyle(ControlStyles.ResizeRedraw, true);
                SetStyle(ControlStyles.Selectable, true);
            }
            protected override void OnPaintBackground(PaintEventArgs pevent) { }
        }
    }
}
