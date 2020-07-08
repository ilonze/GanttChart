using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GanttChart
{
    /// <summary>
    /// 甘特图控件
    /// 该部分类主要是甘特图的属性
    /// </summary>
    public partial class GanttChartView
    {
        #region Fonts
        private Font _ColumnDateTextFont = DefaultFont;

        /// <summary>
        /// 日期字体 
        /// </summary>
        public Font ColumnDateTextFont
        {
            get { return _ColumnDateTextFont; }
            set { _ColumnDateTextFont = value; OnPropertyChanged(nameof(ColumnDateTextFont), value); }
        }

        private Font _ColumnHourTextFont = DefaultFont;

        /// <summary>
        /// 时间字体 
        /// </summary>
        public Font ColumnHourTextFont
        {
            get { return _ColumnHourTextFont; }
            set { _ColumnHourTextFont = value; OnPropertyChanged(nameof(ColumnHourTextFont), value); }
        }

        private Font _RowHeaderTextFont = DefaultFont;

        /// <summary>
        /// 行标题字体 
        /// </summary>
        public Font RowHeaderTextFont
        {
            get { return _RowHeaderTextFont; }
            set { _RowHeaderTextFont = value; OnPropertyChanged(nameof(RowHeaderTextFont), value); }
        }

        private Font _ItemTextFont = DefaultFont;

        /// <summary>
        /// 项字体 
        /// </summary>
        public Font ItemTextFont
        {
            get { return _ItemTextFont; }
            set { _ItemTextFont = value; OnPropertyChanged(nameof(ItemTextFont), value); }
        }

        private Font _TooltipTextFont = DefaultFont;

        /// <summary>
        /// 提示文字字体 
        /// </summary>
        public Font TooltipTextFont
        {
            get { return _TooltipTextFont; }
            set { _TooltipTextFont = value; OnPropertyChanged(nameof(TooltipTextFont), value); }
        }
        #endregion
        #region Colors
        private Color _GridColor = Color.FromArgb(210, 210, 210);

        /// <summary>
        /// 网格颜色 
        /// </summary>
        public Color GridColor
        {
            get { return _GridColor; }
            set { _GridColor = value; OnPropertyChanged(nameof(GridColor), value); }
        }

        private Color _ColumnDateTextColor = Color.FromArgb(102, 102, 102);

        /// <summary>
        /// 日期文字颜色 
        /// </summary>
        public Color ColumnDateTextColor
        {
            get { return _ColumnDateTextColor; }
            set { _ColumnDateTextColor = value; OnPropertyChanged(nameof(ColumnDateTextColor), value); }
        }

        private Color _ColumnHourTextColor = Color.FromArgb(102, 102, 102);

        /// <summary>
        /// 小时文字颜色 
        /// </summary>
        public Color ColumnHourTextColor
        {
            get { return _ColumnHourTextColor; }
            set { _ColumnHourTextColor = value; OnPropertyChanged(nameof(ColumnHourTextColor), value); }
        }

        private Color _HeaderBackColor = Color.FromArgb(247, 247, 247);

        /// <summary>
        /// 标题背景颜色 
        /// </summary>
        public Color HeaderBackColor
        {
            get { return _HeaderBackColor; }
            set { _HeaderBackColor = value; OnPropertyChanged(nameof(HeaderBackColor), value); }
        }

        private Color _ContentBackColor = Color.White;

        /// <summary>
        /// 内容背景颜色 
        /// </summary>
        public Color ContentBackColor
        {
            get { return _ContentBackColor; }
            set { _ContentBackColor = value; OnPropertyChanged(nameof(ContentBackColor), value); }
        }

        private Color _CurrentTimeLineColor = Color.Red;

        /// <summary>
        /// 当前时间线颜色 
        /// </summary>
        public Color CurrentTimeLineColor
        {
            get { return _CurrentTimeLineColor; }
            set { _CurrentTimeLineColor = value; OnPropertyChanged(nameof(CurrentTimeLineColor), value); }
        }

        private Color _ItemTextColor = Color.FromArgb(51, 51, 51);

        /// <summary>
        /// 项文字颜色 
        /// </summary>
        public Color ItemTextColor
        {
            get { return _ItemTextColor; }
            set { _ItemTextColor = value; OnPropertyChanged(nameof(ItemTextColor), value); }
        }

        private Color _ItemBackColor = Color.FromArgb(190, 190, 190);

        /// <summary>
        /// 项背景颜色 
        /// </summary>
        public Color ItemBackColor
        {
            get { return _ItemBackColor; }
            set { _ItemBackColor = value; OnPropertyChanged(nameof(ItemBackColor), value); }
        }

        private Color _ItemBorderColor = Color.FromArgb(18, 170, 11);

        /// <summary>
        /// 项边框颜色 
        /// </summary>
        public Color ItemBorderColor
        {
            get { return _ItemBorderColor; }
            set { _ItemBorderColor = value; OnPropertyChanged(nameof(ItemBorderColor), value); }
        }

        private Color _ItemStartFlagColor = Color.Red;

        /// <summary>
        /// 项首标志颜色 
        /// </summary>
        public Color ItemStartFlagColor
        {
            get { return _ItemStartFlagColor; }
            set { _ItemStartFlagColor = value; OnPropertyChanged(nameof(ItemStartFlagColor), value); }
        }

        private Color _ItemSubRangeBackColor = Color.FromArgb(153, 201, 255);

        /// <summary>
        /// 项时间范围背景颜色 
        /// </summary>
        public Color ItemSubRangeBackColor
        {
            get { return _ItemSubRangeBackColor; }
            set { _ItemSubRangeBackColor = value; OnPropertyChanged(nameof(ItemSubRangeBackColor), value); }
        }

        private Color _ItemSelectionTextColor = Color.FromArgb(18, 170, 11);

        /// <summary>
        /// 选中项文字颜色 
        /// </summary>
        public Color ItemSelectionTextColor
        {
            get { return _ItemSelectionTextColor; }
            set { _ItemSelectionTextColor = value; OnPropertyChanged(nameof(ItemSelectionTextColor), value); }
        }

        private Color _ItemSelectionBackColor = Color.FromArgb(229, 249, 229);

        /// <summary>
        /// 选中项背景颜色 
        /// </summary>
        public Color ItemSelectionBackColor
        {
            get { return _ItemSelectionBackColor; }
            set { _ItemSelectionBackColor = value; OnPropertyChanged(nameof(ItemSelectionBackColor), value); }
        }

        private Color _ItemSelectionBorderColor = Color.FromArgb(18, 170, 11);

        /// <summary>
        /// 选中项边框颜色 
        /// </summary>
        public Color ItemSelectionBorderColor
        {
            get { return _ItemSelectionBorderColor; }
            set { _ItemSelectionBorderColor = value; OnPropertyChanged(nameof(ItemSelectionBorderColor), value); }
        }

        private Color _ItemSelectionSubRangeBackColor = Color.FromArgb(153, 201, 255);

        /// <summary>
        /// 选中项时间范围背景颜色 
        /// </summary>
        public Color ItemSelectionSubRangeBackColor
        {
            get { return _ItemSelectionSubRangeBackColor; }
            set { _ItemSelectionSubRangeBackColor = value; OnPropertyChanged(nameof(ItemSelectionSubRangeBackColor), value); }
        }

        private Color _RowHeaderTextColor = Color.FromArgb(102, 102, 102);

        /// <summary>
        /// 行标题文字颜色 
        /// </summary>
        public Color RowHeaderTextColor
        {
            get { return _RowHeaderTextColor; }
            set { _RowHeaderTextColor = value; OnPropertyChanged(nameof(RowHeaderTextColor), value); }
        }

        private Color _RowSelectionHeaderTextColor = Color.FromArgb(102, 102, 102);

        /// <summary>
        /// 选中行标题文字颜色 
        /// </summary>
        public Color RowSelectionHeaderTextColor
        {
            get { return _RowSelectionHeaderTextColor; }
            set { _RowSelectionHeaderTextColor = value; OnPropertyChanged(nameof(RowSelectionHeaderTextColor), value); }
        }

        private Color _RowSelectionBorderColor = Color.FromArgb(247, 247, 247);

        /// <summary>
        /// 选中行边框颜色 
        /// </summary>
        public Color RowSelectionBorderColor
        {
            get { return _RowSelectionBorderColor; }
            set { _RowSelectionBorderColor = value; OnPropertyChanged(nameof(RowSelectionBorderColor), value); }
        }

        private Color _RowSelectionHeaderBackColor = Color.FromArgb(247, 247, 247);

        /// <summary>
        /// 选中行标题背景颜色 
        /// </summary>
        public Color RowSelectionHeaderBackColor
        {
            get { return _RowSelectionHeaderBackColor; }
            set { _RowSelectionHeaderBackColor = value; OnPropertyChanged(nameof(RowSelectionHeaderBackColor), value); }
        }

        private Color _RowSelectionContentBackColor = Color.FromArgb(247, 247, 247);

        /// <summary>
        /// 选中行内容背景颜色 
        /// </summary>
        public Color RowSelectionContentBackColor
        {
            get { return _RowSelectionContentBackColor; }
            set { _RowSelectionContentBackColor = value; OnPropertyChanged(nameof(RowSelectionContentBackColor), value); }
        }

        private Color _TooltipTextColor = Color.FromArgb(237, 237, 237);

        /// <summary>
        /// 提示文字颜色 
        /// </summary>
        public Color TooltipTextColor
        {
            get { return _TooltipTextColor; }
            set { _TooltipTextColor = value; OnPropertyChanged(nameof(TooltipTextColor), value); }
        }

        private Color _TooltipBorderColor = Color.FromArgb(102, 0, 0, 0);

        /// <summary>
        /// 提示边框颜色 
        /// </summary>
        public Color TooltipBorderColor
        {
            get { return _TooltipBorderColor; }
            set { _TooltipBorderColor = value; OnPropertyChanged(nameof(TooltipBorderColor), value); }
        }

        private Color _TooltipBackColor = Color.FromArgb(102, 0, 0, 0);

        /// <summary>
        /// 提示背景颜色 
        /// </summary>
        public Color TooltipBackColor
        {
            get { return _TooltipBackColor; }
            set { _TooltipBackColor = value; OnPropertyChanged(nameof(TooltipBackColor), value); }
        }

        private Color _TimeRangeSelecetionBackColor = Color.FromArgb(153, 201, 255);

        /// <summary>
        /// 选中时间范围背景颜色 
        /// </summary>
        public Color TimeRangeSelecetionBackColor
        {
            get { return _TimeRangeSelecetionBackColor; }
            set { _TimeRangeSelecetionBackColor = value; OnPropertyChanged(nameof(TimeRangeSelecetionBackColor), value); }
        }

        private Color _SelectionRectangleBackColor = Color.FromArgb(80, 210, 210, 210);

        /// <summary>
        /// 选择框背景颜色 
        /// </summary>
        public Color SelectionRectangleBackColor
        {
            get { return _SelectionRectangleBackColor; }
            set { _SelectionRectangleBackColor = value; OnPropertyChanged(nameof(SelectionRectangleBackColor), value); }
        }

        private Color _ItemWarnningTextColor = Color.Red;

        /// <summary>
        /// 项警告文字颜色 
        /// </summary>
        public Color ItemWarnningTextColor
        {
            get { return _ItemWarnningTextColor; }
            set { _ItemWarnningTextColor = value; OnPropertyChanged(nameof(ItemWarnningTextColor), value); }
        }
        #endregion
        #region Mouse
        private float _MouseWheelSensitivity = 0.5f;

        /// <summary>
        /// 鼠标滚轮灵敏度 
        /// </summary>
        [DefaultValue(0.5f)]
        public float MouseWheelSensitivity
        {
            get { return _MouseWheelSensitivity; }
            set { _MouseWheelSensitivity = value; OnPropertyChanged(nameof(MouseWheelSensitivity), value); }
        }
        #endregion
        #region Size
        private int _ItemHeight = 30;

        /// <summary>
        /// 项高度 
        /// </summary>
        [DefaultValue(30)]
        public int ItemHeight
        {
            get { return _ItemHeight; }
            set { _ItemHeight = value; OnPropertyChanged(nameof(ItemHeight), value); }
        }

        private int _RowHeaderWidth = 150;

        /// <summary>
        /// 行标题宽度 
        /// </summary>
        [DefaultValue(150)]
        public int RowHeaderWidth
        {
            get { return _RowHeaderWidth; }
            set { _RowHeaderWidth = value; OnPropertyChanged(nameof(RowHeaderWidth), value); }
        }

        private int _RowHeight = 100;

        /// <summary>
        /// 行高 
        /// </summary>
        [DefaultValue(100)]
        public int RowHeight
        {
            get { return _RowHeight; }
            set { _RowHeight = value; OnPropertyChanged(nameof(RowHeight), value); }
        }

        private int _ColumnDateHeight = 30;

        /// <summary>
        /// 日期高度 
        /// </summary>
        [DefaultValue(30)]
        public int ColumnDateHeight
        {
            get { return _ColumnDateHeight; }
            set { _ColumnDateHeight = value; OnPropertyChanged(nameof(ColumnDateHeight), value); }
        }

        private int _ColumnHourHeight = 30;

        /// <summary>
        /// 小时高度 
        /// </summary>
        [DefaultValue(30)]
        public int ColumnHourHeight
        {
            get { return _ColumnHourHeight; }
            set { _ColumnHourHeight = value; OnPropertyChanged(nameof(ColumnHourHeight), value); }
        }

        private int _DateWidth = MinDateWidth;

        /// <summary>
        /// 日期宽度 
        /// </summary>
        [DefaultValue(MinDateWidth)]
        public int DateWidth
        {
            get { return _DateWidth; }
            set { _DateWidth = value; OnPropertyChanged(nameof(DateWidth), value); }
        }

        private int _HScrollHeight = 15;

        /// <summary>
        /// 横向滚动条高度 
        /// </summary>
        [DefaultValue(15)]
        public int HScrollHeight
        {
            get { return _HScrollHeight; }
            set { _HScrollHeight = value; OnPropertyChanged(nameof(HScrollHeight), value); }
        }

        private int _VScrollWidth = 15;

        /// <summary>
        /// 纵向滚动条宽度 
        /// </summary>
        [DefaultValue(15)]
        public int VScrollWidth
        {
            get { return _VScrollWidth; }
            set { _VScrollWidth = value; OnPropertyChanged(nameof(VScrollWidth), value); }
        }

        private int _ItemSpacing = 2;

        /// <summary>
        /// 项的行间距 
        /// </summary>
        [DefaultValue(2)]
        public int ItemSpacing
        {
            get { return _ItemSpacing; }
            set { _ItemSpacing = value; OnPropertyChanged(nameof(ItemSpacing), value); }
        }

        private int _RowSpacing = 5;

        /// <summary>
        /// 行间距 
        /// </summary>
        [DefaultValue(5)]
        public int RowSpacing
        {
            get { return _RowSpacing; }
            set { _RowSpacing = value; OnPropertyChanged(nameof(RowSpacing), value); }
        }
        #endregion
        #region Control
        private bool _IsFixedRowHeight = false;

        /// <summary>
        /// 是否固定行高 
        /// </summary>
        [DefaultValue(false)]
        public bool IsFixedRowHeight
        {
            get { return _IsFixedRowHeight; }
            set { _IsFixedRowHeight = value; OnPropertyChanged(nameof(IsFixedRowHeight), value); }
        }

        private bool _EnableEdit = false;

        /// <summary>
        /// 启用编辑 
        /// </summary>
        [DefaultValue(false)]
        public bool EnableEdit
        {
            get { return _EnableEdit; }
            set { _EnableEdit = value; OnPropertyChanged(nameof(EnableEdit), value); }
        }

        private bool _AllowItemNotWrap = true;

        /// <summary>
        /// 允许项文字换行 
        /// </summary>
        [DefaultValue(true)]
        public bool AllowItemNotWrap
        {
            get { return _AllowItemNotWrap; }
            set { _AllowItemNotWrap = value; OnPropertyChanged(nameof(AllowItemNotWrap), value); }
        }

        private bool _IsShowItemBorder = false;

        /// <summary>
        /// 是否显示项边框 
        /// </summary>
        [DefaultValue(false)]
        public bool IsShowItemBorder
        {
            get { return _IsShowItemBorder; }
            set { _IsShowItemBorder = value; OnPropertyChanged(nameof(IsShowItemBorder), value); }
        }

        private bool _AllowHeaderNotWrap = true;

        /// <summary>
        /// 允许行标题文字换行 
        /// </summary>
        [DefaultValue(true)]
        public bool AllowHeaderNotWrap
        {
            get { return _AllowHeaderNotWrap; }
            set { _AllowHeaderNotWrap = value; OnPropertyChanged(nameof(AllowHeaderNotWrap), value); }
        }

        private GanttChartViewItemBlockType _BlockType = GanttChartViewItemBlockType.TextOverBlock;

        /// <summary>
        /// 项换行模式 
        /// </summary>
        public GanttChartViewItemBlockType BlockType
        {
            get { return _BlockType; }
            set { _BlockType = value; OnPropertyChanged(nameof(BlockType), value); }
        }

        private bool _IsShowSelectionBorder = false;

        /// <summary>
        /// 显示行选中高亮边框 
        /// </summary>
        [DefaultValue(false)]
        public bool IsShowSelectionBorder
        {
            get { return _IsShowSelectionBorder; }
            set { _IsShowSelectionBorder = value; OnPropertyChanged(nameof(IsShowSelectionBorder), value); }
        }

        private bool _AllowSelectRange = false;

        /// <summary>
        /// 允许选中时间范围 
        /// </summary>
        [DefaultValue(false)]
        public bool AllowSelectRange
        {
            get { return _AllowSelectRange; }
            set { _AllowSelectRange = value; OnPropertyChanged(nameof(AllowSelectRange), value); }
        }

        private Func<GanttChartViewItem, GanttChartViewItem, bool> _SelectionItemSameComparator = null;

        /// <summary>
        /// 选择项的同类项比较器
        /// </summary>
        public Func<GanttChartViewItem, GanttChartViewItem, bool> SelectionItemSameComparator
        {
            get { return _SelectionItemSameComparator; }
            set { _SelectionItemSameComparator = value; }
        }
        #endregion
        #region DataSource
        private DateTime _StartDate = DateTime.Today;

        /// <summary>
        /// 开始日期 
        /// </summary>
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; OnPropertyChanged(nameof(StartDate), value); }
        }

        private DateTime _EndDate = DateTime.Today.AddDays(1);

        /// <summary>
        /// 结束日期 
        /// </summary>
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; OnPropertyChanged(nameof(EndDate), value); }
        }

        private GanttChartCollection<GanttChartViewRow> _Rows = new GanttChartCollection<GanttChartViewRow>();

        /// <summary>
        /// 行集合 
        /// </summary>
        public GanttChartCollection<GanttChartViewRow> Rows
        {
            get { return _Rows; }
            set { _Rows = value; OnPropertyChanged(nameof(Rows), value); }
        }

        private GanttChartViewRow _SelectionRow = null;

        /// <summary>
        /// 选中的行 
        /// </summary>
        public GanttChartViewRow SelectionRow
        {
            get { return _SelectionRow; }
            set { _SelectionRow = value; OnPropertyChanged(nameof(SelectionRow), value); }
        }

        private GanttChartViewItem _SelectionItem = null;

        /// <summary>
        /// 选中的项 
        /// </summary>
        public GanttChartViewItem SelectionItem
        {
            get { return _SelectionItem; }
            set { _SelectionItem = value; OnPropertyChanged(nameof(SelectionItem), value); }
        }

        private TimeRange _SelectionTimeRange = null;

        /// <summary>
        /// 选中的时间范围 
        /// </summary>
        public TimeRange SelectionTimeRange
        {
            get { return _SelectionTimeRange; }
            set { _SelectionTimeRange = value; OnPropertyChanged(nameof(SelectionTimeRange), value); }
        }

        private DateTime _DateSplitTime = DateTime.MinValue;

        /// <summary>
        /// 日分割时间 
        /// </summary>
        public DateTime DateSplitTime
        {
            get { return _DateSplitTime; }
            set { _DateSplitTime = value; OnPropertyChanged(nameof(DateSplitTime), value); }
        }

        private string _DateTimeShowText = "时间";

        /// <summary>
        /// 时间显示文本 
        /// </summary>
        [DefaultValue("时间")]
        public string DateTimeShowText
        {
            get { return _DateTimeShowText; }
            set { _DateTimeShowText = value; OnPropertyChanged(nameof(DateTimeShowText), value); }
        }

        private string _RowTitleShowText = "资源";

        /// <summary>
        /// 行头显示文本 
        /// </summary>
        [DefaultValue("资源")]
        public string RowTitleShowText
        {
            get { return _RowTitleShowText; }
            set { _RowTitleShowText = value; OnPropertyChanged(nameof(RowTitleShowText), value); }
        }
        #endregion

        #region ReadOnly
        public int ColumnHeight
        {
            get { return ColumnDateHeight + ColumnHourHeight; }
        }

        public int TotalDays
        {
            get
            {
                return (int)(EndDate.Date - StartDate.Date).TotalDays + 1;
            }
        }
        public DateTime StartTime
        {
            get
            {
                return StartDate.Date.Add(DateSplitTime - DateSplitTime.Date);
            }
        }
        public DateTime EndTime
        {
            get
            {
                return EndDate.Date.Add(DateSplitTime - DateSplitTime.Date);
            }
        }
        private Size TooltipSize
        {
            get
            {
                var size = TextRenderer.MeasureText(_ShowTooltipItem.Tooltip, TooltipTextFont, Size.Empty);
                size.Height = size.Height + (int)((_ShowTooltipItem.Tooltip.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Length - 1) * 1.7);
                return size;
            }
        }
        private Size TooltipOuterSize
        {
            get
            {
                var size = TooltipSize;
                size.Width += RowSpacing * 2;
                size.Height += RowSpacing * 2;
                return size;
            }
        }
        #endregion

        #region Const
        private const int MinDateWidth = 3 * 48;
        #endregion
    }
}
