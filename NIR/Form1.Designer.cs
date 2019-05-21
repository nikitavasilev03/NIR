namespace NIR
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.doMagic = new System.Windows.Forms.Button();
            this.playOriginal = new System.Windows.Forms.Button();
            this.playNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(24, 20);
            this.chart1.Margin = new System.Windows.Forms.Padding(2);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            this.chart1.Size = new System.Drawing.Size(1126, 514);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // doMagic
            // 
            this.doMagic.Location = new System.Drawing.Point(299, 553);
            this.doMagic.Name = "doMagic";
            this.doMagic.Size = new System.Drawing.Size(183, 60);
            this.doMagic.TabIndex = 1;
            this.doMagic.Text = "doMagic";
            this.doMagic.UseVisualStyleBackColor = true;
            this.doMagic.Click += new System.EventHandler(this.doMagic_Click);
            // 
            // playOriginal
            // 
            this.playOriginal.Location = new System.Drawing.Point(488, 553);
            this.playOriginal.Name = "playOriginal";
            this.playOriginal.Size = new System.Drawing.Size(183, 60);
            this.playOriginal.TabIndex = 2;
            this.playOriginal.Text = "playOriginal";
            this.playOriginal.UseVisualStyleBackColor = true;
            this.playOriginal.Click += new System.EventHandler(this.playOriginal_Click);
            // 
            // playNew
            // 
            this.playNew.Location = new System.Drawing.Point(677, 553);
            this.playNew.Name = "playNew";
            this.playNew.Size = new System.Drawing.Size(183, 60);
            this.playNew.TabIndex = 3;
            this.playNew.Text = "playNew";
            this.playNew.UseVisualStyleBackColor = true;
            this.playNew.Click += new System.EventHandler(this.playNew_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 625);
            this.Controls.Add(this.playNew);
            this.Controls.Add(this.playOriginal);
            this.Controls.Add(this.doMagic);
            this.Controls.Add(this.chart1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button doMagic;
        private System.Windows.Forms.Button playOriginal;
        private System.Windows.Forms.Button playNew;
    }
}

