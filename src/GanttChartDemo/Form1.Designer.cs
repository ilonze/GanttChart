namespace GanttChartDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            GanttChart.GanttChartCollection<GanttChart.GanttChartViewRow> ganttChartCollection_11 = new GanttChart.GanttChartCollection<GanttChart.GanttChartViewRow>();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ganttChartView1 = new GanttChart.GanttChartView();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ganttChartView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 1;
            // 
            // ganttChartView1
            // 
            this.ganttChartView1.BackColor = System.Drawing.Color.White;
            this.ganttChartView1.BlockType = GanttChart.GanttChartViewItemBlockType.Return;
            this.ganttChartView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ganttChartView1.EndDate = new System.DateTime(2019, 11, 12);
            this.ganttChartView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.ganttChartView1.Location = new System.Drawing.Point(0, 0);
            this.ganttChartView1.Name = "ganttChartView1";
            this.ganttChartView1.Rows = ganttChartCollection_11;
            this.ganttChartView1.Size = new System.Drawing.Size(800, 450);
            this.ganttChartView1.StartDate = new System.DateTime(2019, 11, 07);
            this.ganttChartView1.TabIndex = 0;
            this.ganttChartView1.Text = "ganttChartView1";
            this.ganttChartView1.ItemHeight = 20;
            this.ganttChartView1.AllowSelectRange = true;
            this.ganttChartView1.DateSplitTime = System.DateTime.MinValue.AddHours(8);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GanttChart.GanttChartView ganttChartView1;
        private System.Windows.Forms.Panel panel1;
    }
}

