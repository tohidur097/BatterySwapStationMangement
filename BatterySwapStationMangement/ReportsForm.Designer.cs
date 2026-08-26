namespace BatterySwapStationManagement
{
    partial class ReportsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblTodayEarnings = new System.Windows.Forms.Label();
            this.lblTodaySwaps = new System.Windows.Forms.Label();
            this.chartUsage = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartUsage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTodayEarnings
            // 
            this.lblTodayEarnings.AutoSize = true;
            this.lblTodayEarnings.Location = new System.Drawing.Point(64, 156);
            this.lblTodayEarnings.Name = "lblTodayEarnings";
            this.lblTodayEarnings.Size = new System.Drawing.Size(82, 13);
            this.lblTodayEarnings.TabIndex = 0;
            this.lblTodayEarnings.Text = "Today\'s earning";
            // 
            // lblTodaySwaps
            // 
            this.lblTodaySwaps.AutoSize = true;
            this.lblTodaySwaps.Location = new System.Drawing.Point(48, 179);
            this.lblTodaySwaps.Name = "lblTodaySwaps";
            this.lblTodaySwaps.Size = new System.Drawing.Size(74, 13);
            this.lblTodaySwaps.TabIndex = 1;
            this.lblTodaySwaps.Text = "Today\'s Swap";
            // 
            // chartUsage
            // 
            this.chartUsage.BackColor = System.Drawing.Color.Transparent;
            this.chartUsage.BackImageTransparentColor = System.Drawing.Color.Transparent;
            this.chartUsage.BackSecondaryColor = System.Drawing.Color.Transparent;
            this.chartUsage.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chartUsage.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartUsage.Legends.Add(legend1);
            this.chartUsage.Location = new System.Drawing.Point(1, 195);
            this.chartUsage.Name = "chartUsage";
            this.chartUsage.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.LabelForeColor = System.Drawing.Color.White;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartUsage.Series.Add(series1);
            this.chartUsage.Size = new System.Drawing.Size(375, 253);
            this.chartUsage.TabIndex = 2;
            this.chartUsage.Text = "chart1";
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BatterySwapStationMangement.Properties.Resources._1731399531573;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chartUsage);
            this.Controls.Add(this.lblTodaySwaps);
            this.Controls.Add(this.lblTodayEarnings);
            this.DoubleBuffered = true;
            this.Name = "ReportsForm";
            this.Text = "ReportsForm";
            ((System.ComponentModel.ISupportInitialize)(this.chartUsage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTodayEarnings;
        private System.Windows.Forms.Label lblTodaySwaps;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartUsage;
    }
}