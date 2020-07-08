using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GanttChart
{
    /// <summary>
    /// 甘特图笔刷
    /// </summary>
    class GanttChartBrushes: IDisposable
    {
        GanttChartView View { get; set; }
        public GanttChartBrushes(GanttChartView view)
        {
            View = view;
            this.GridBrush = new SolidBrush(view.GridColor);

            this.ColumnDateTextBrush = new SolidBrush(view.ColumnDateTextColor);
            this.ColumnHourTextBrush = new SolidBrush(view.ColumnHourTextColor);
            this.HeaderBackBrush = new SolidBrush(view.HeaderBackColor);

            this.BackBrush = new SolidBrush(view.BackColor);
            this.ContentBackBrush = new SolidBrush(view.ContentBackColor);

            this.CurrentTimeLineBrush = new SolidBrush(view.CurrentTimeLineColor);

            this.ItemTextBrush = new SolidBrush(view.ItemTextColor);
            this.ItemBackBrush = new SolidBrush(view.ItemBackColor);
            this.ItemBorderBrush = new SolidBrush(view.ItemBorderColor);
            this.ItemStartFlagBrush = new SolidBrush(view.ItemStartFlagColor);
            this.ItemSubRangeBackBrush = new SolidBrush(view.ItemSubRangeBackColor);
            this.ItemSelectionTextBrush = new SolidBrush(view.ItemSelectionTextColor);
            this.ItemSelectionBackBrush = new SolidBrush(view.ItemSelectionBackColor);
            this.ItemSelectionBorderBrush = new SolidBrush(view.ItemSelectionBorderColor);
            this.ItemSelectionSubRangeBackBrush = new SolidBrush(view.ItemSelectionSubRangeBackColor);
            this.ItemWarnningTextBrush = new SolidBrush(view.ItemWarnningTextColor);

            this.RowHeaderTextBrush = new SolidBrush(view.RowHeaderTextColor);
            this.RowSelectionHeaderTextBrush = new SolidBrush(view.RowSelectionHeaderTextColor);
            this.RowSelectionBorderBrush = new SolidBrush(view.RowSelectionBorderColor);
            this.RowSelectionHeaderBackBrush = new SolidBrush(view.RowSelectionHeaderBackColor);
            this.RowSelectionContentBackBrush = new SolidBrush(view.RowSelectionContentBackColor);

            this.TooltipTextBrush = new SolidBrush(view.TooltipTextColor);
            this.TooltipBorderBrush = new SolidBrush(view.TooltipBorderColor);
            this.TooltipBackBrush = new SolidBrush(view.TooltipBackColor);

            this.TimeRangeSelecetionBackBrush = new SolidBrush(view.TimeRangeSelecetionBackColor);
            this.SelectionRectangleBackBrush = new SolidBrush(view.SelectionRectangleBackColor);

            this.GridPen = new Pen(this.GridBrush, 1);
            this.CurrentTimeLinePen = new Pen(this.CurrentTimeLineBrush, 1);
            this.ItemBorderPen = new Pen(this.ItemBorderBrush, 1);
            this.ItemBackPen = new Pen(this.ItemBackBrush, 1);
            this.ItemSelectionBorderPen = new Pen(this.ItemSelectionBorderBrush, 1);
            this.RowSelectionBorderPen = new Pen(this.RowSelectionBorderBrush, 1);
            this.TooltipBorderPen = new Pen(this.TooltipBorderBrush, 1);
            this.ItemStartFlagPen = new Pen(this.ItemStartFlagBrush, 2);
            this.ItemSubRangeBackPen = new Pen(this.ItemSubRangeBackBrush, 1);
            this.ItemSelectionSubRangeBackPen = new Pen(this.ItemSelectionSubRangeBackBrush, 1);
        }
        public SolidBrush GridBrush { get; set; }

        public SolidBrush ColumnDateTextBrush { get; set; }
        public SolidBrush ColumnHourTextBrush { get; set; }
        public SolidBrush HeaderBackBrush { get; set; }

        public SolidBrush BackBrush { get; set; }
        public SolidBrush ContentBackBrush { get; set; }

        public SolidBrush CurrentTimeLineBrush { get; set; }

        public SolidBrush ItemTextBrush { get; set; }
        public SolidBrush ItemBackBrush { get; set; }
        public SolidBrush ItemBorderBrush { get; set; }
        public SolidBrush ItemStartFlagBrush { get; set; }
        public SolidBrush ItemSubRangeBackBrush { get; set; }
        public SolidBrush ItemSelectionTextBrush { get; set; }
        public SolidBrush ItemSelectionBackBrush { get; set; }
        public SolidBrush ItemSelectionBorderBrush { get; set; }
        public SolidBrush ItemSelectionSubRangeBackBrush { get; set; }
        public SolidBrush ItemWarnningTextBrush { get; set; }


        public SolidBrush RowHeaderTextBrush { get; set; }
        public SolidBrush RowSelectionHeaderTextBrush { get; set; }
        public SolidBrush RowSelectionBorderBrush { get; set; }
        public SolidBrush RowSelectionHeaderBackBrush { get; set; }
        public SolidBrush RowSelectionContentBackBrush { get; set; }

        public SolidBrush TooltipTextBrush { get; set; }
        public SolidBrush TooltipBorderBrush { get; set; }
        public SolidBrush TooltipBackBrush { get; set; }

        public SolidBrush TimeRangeSelecetionBackBrush { get; set; }
        public SolidBrush SelectionRectangleBackBrush { get; set; }

        public Pen GridPen { get; }
        public Pen CurrentTimeLinePen { get; }
        public Pen ItemBorderPen { get; }
        public Pen ItemBackPen { get; }
        public Pen ItemSelectionBorderPen { get; }
        public Pen RowSelectionBorderPen { get; }
        public Pen TooltipBorderPen { get; }
        public Pen ItemStartFlagPen { get; }
        public Pen ItemSubRangeBackPen { get; }
        public Pen ItemSelectionSubRangeBackPen { get; }

        public void Refresh()
        {
            this.GridBrush.Color = View.GridColor;

            this.ColumnDateTextBrush.Color = View.ColumnDateTextColor;
            this.ColumnHourTextBrush.Color = View.ColumnHourTextColor;
            this.HeaderBackBrush.Color = View.HeaderBackColor;

            this.ContentBackBrush.Color = View.ContentBackColor;

            this.CurrentTimeLineBrush.Color = View.CurrentTimeLineColor;

            this.ItemTextBrush.Color = View.ItemTextColor;
            this.ItemBackBrush.Color = View.ItemBackColor;
            this.ItemBorderBrush.Color = View.ItemBorderColor;
            this.ItemStartFlagBrush.Color = View.ItemStartFlagColor;
            this.ItemSubRangeBackBrush.Color = View.ItemSubRangeBackColor;
            this.ItemSelectionTextBrush.Color = View.ItemSelectionTextColor;
            this.ItemSelectionBackBrush.Color = View.ItemSelectionBackColor;
            this.ItemSelectionBorderBrush.Color = View.ItemSelectionBorderColor;
            this.ItemSelectionSubRangeBackBrush.Color = View.ItemSelectionSubRangeBackColor;
            this.ItemWarnningTextBrush.Color = View.ItemWarnningTextColor;

            this.RowHeaderTextBrush.Color = View.RowHeaderTextColor;
            this.RowSelectionHeaderTextBrush.Color = View.RowSelectionHeaderTextColor;
            this.RowSelectionBorderBrush.Color = View.RowSelectionBorderColor;
            this.RowSelectionHeaderBackBrush.Color = View.RowSelectionHeaderBackColor;
            this.RowSelectionContentBackBrush.Color = View.RowSelectionContentBackColor;

            this.TooltipTextBrush.Color = View.TooltipTextColor;
            this.TooltipBorderBrush.Color = View.TooltipBorderColor;
            this.TooltipBackBrush.Color = View.TooltipBackColor;

            this.TimeRangeSelecetionBackBrush.Color = View.TimeRangeSelecetionBackColor;
            this.SelectionRectangleBackBrush.Color = View.SelectionRectangleBackColor;
        }

        public void Dispose()
        {
            this.GridPen.Dispose();
            this.CurrentTimeLinePen.Dispose();
            this.ItemBorderPen.Dispose();
            this.ItemBackPen.Dispose();
            this.ItemSelectionBorderPen.Dispose();
            this.RowSelectionBorderPen.Dispose();
            this.TooltipBorderPen.Dispose();
            this.ItemStartFlagPen.Dispose();
            this.ItemSubRangeBackPen.Dispose();
            this.ItemSelectionSubRangeBackPen.Dispose();

            this.BackBrush.Dispose();
            this.GridBrush.Dispose();

            this.ColumnDateTextBrush.Dispose();
            this.ColumnHourTextBrush.Dispose();
            this.HeaderBackBrush.Dispose();

            this.ContentBackBrush.Dispose();

            this.CurrentTimeLineBrush.Dispose();

            this.ItemTextBrush.Dispose();
            this.ItemBackBrush.Dispose();
            this.ItemBorderBrush.Dispose();
            this.ItemStartFlagBrush.Dispose();
            this.ItemSubRangeBackBrush.Dispose();
            this.ItemSelectionTextBrush.Dispose();
            this.ItemSelectionBackBrush.Dispose();
            this.ItemSelectionBorderBrush.Dispose();
            this.ItemSelectionSubRangeBackBrush.Dispose();
            this.ItemWarnningTextBrush.Dispose();

            this.RowHeaderTextBrush.Dispose();
            this.RowSelectionHeaderTextBrush.Dispose();
            this.RowSelectionBorderBrush.Dispose();
            this.RowSelectionHeaderBackBrush.Dispose();
            this.RowSelectionContentBackBrush.Dispose();

            this.TooltipTextBrush.Dispose();
            this.TooltipBorderBrush.Dispose();
            this.TooltipBackBrush.Dispose();

            this.TimeRangeSelecetionBackBrush.Dispose();
            this.SelectionRectangleBackBrush.Dispose();
        }
    }
}
