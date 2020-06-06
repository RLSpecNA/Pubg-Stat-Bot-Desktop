namespace PUBG_Application.Forms
{
    partial class NormalSinglePlayer
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.yearDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.graphPlotBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelFraggerRatingValue = new System.Windows.Forms.Label();
            this.labelAverageSurvivedTimeValue = new System.Windows.Forms.Label();
            this.labelLongestKillValue = new System.Windows.Forms.Label();
            this.labelWinPercentValue = new System.Windows.Forms.Label();
            this.labelMaxKillsValue = new System.Windows.Forms.Label();
            this.labelWinsValue = new System.Windows.Forms.Label();
            this.labelHeadshotPercentValue = new System.Windows.Forms.Label();
            this.labelGamesPlayedValue = new System.Windows.Forms.Label();
            this.labelAdrValue = new System.Windows.Forms.Label();
            this.labelAverageSurvivedTime = new System.Windows.Forms.Label();
            this.labelFraggerRating = new System.Windows.Forms.Label();
            this.labelWinPercent = new System.Windows.Forms.Label();
            this.labelLongestKill = new System.Windows.Forms.Label();
            this.labelWins = new System.Windows.Forms.Label();
            this.labelMaxKills = new System.Windows.Forms.Label();
            this.labelGamesPlayed = new System.Windows.Forms.Label();
            this.labelHeadshotPercent = new System.Windows.Forms.Label();
            this.labelAdr = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graphPlotBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.panel1.Controls.Add(this.cartesianChart1);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 946);
            this.panel1.TabIndex = 0;
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(12, 419);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(770, 332);
            this.cartesianChart1.TabIndex = 7;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.yearDataGridViewTextBoxColumn,
            this.monthDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.graphPlotBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(16, 757);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(685, 177);
            this.dataGridView1.TabIndex = 6;
            // 
            // yearDataGridViewTextBoxColumn
            // 
            this.yearDataGridViewTextBoxColumn.DataPropertyName = "Year";
            this.yearDataGridViewTextBoxColumn.HeaderText = "Year";
            this.yearDataGridViewTextBoxColumn.Name = "yearDataGridViewTextBoxColumn";
            // 
            // monthDataGridViewTextBoxColumn
            // 
            this.monthDataGridViewTextBoxColumn.DataPropertyName = "Month";
            this.monthDataGridViewTextBoxColumn.HeaderText = "Month";
            this.monthDataGridViewTextBoxColumn.Name = "monthDataGridViewTextBoxColumn";
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            // 
            // graphPlotBindingSource
            // 
            this.graphPlotBindingSource.DataSource = typeof(PUBG_Application.GraphPlot);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(707, 820);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.panel3.Location = new System.Drawing.Point(402, 107);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(380, 306);
            this.panel3.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.panel2.Controls.Add(this.labelFraggerRatingValue);
            this.panel2.Controls.Add(this.labelAverageSurvivedTimeValue);
            this.panel2.Controls.Add(this.labelLongestKillValue);
            this.panel2.Controls.Add(this.labelWinPercentValue);
            this.panel2.Controls.Add(this.labelMaxKillsValue);
            this.panel2.Controls.Add(this.labelWinsValue);
            this.panel2.Controls.Add(this.labelHeadshotPercentValue);
            this.panel2.Controls.Add(this.labelGamesPlayedValue);
            this.panel2.Controls.Add(this.labelAdrValue);
            this.panel2.Controls.Add(this.labelAverageSurvivedTime);
            this.panel2.Controls.Add(this.labelFraggerRating);
            this.panel2.Controls.Add(this.labelWinPercent);
            this.panel2.Controls.Add(this.labelLongestKill);
            this.panel2.Controls.Add(this.labelWins);
            this.panel2.Controls.Add(this.labelMaxKills);
            this.panel2.Controls.Add(this.labelGamesPlayed);
            this.panel2.Controls.Add(this.labelHeadshotPercent);
            this.panel2.Controls.Add(this.labelAdr);
            this.panel2.Location = new System.Drawing.Point(12, 107);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(380, 306);
            this.panel2.TabIndex = 2;
            // 
            // labelFraggerRatingValue
            // 
            this.labelFraggerRatingValue.AutoSize = true;
            this.labelFraggerRatingValue.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFraggerRatingValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelFraggerRatingValue.Location = new System.Drawing.Point(217, 278);
            this.labelFraggerRatingValue.Name = "labelFraggerRatingValue";
            this.labelFraggerRatingValue.Size = new System.Drawing.Size(0, 23);
            this.labelFraggerRatingValue.TabIndex = 13;
            // 
            // labelAverageSurvivedTimeValue
            // 
            this.labelAverageSurvivedTimeValue.AutoSize = true;
            this.labelAverageSurvivedTimeValue.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAverageSurvivedTimeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelAverageSurvivedTimeValue.Location = new System.Drawing.Point(217, 103);
            this.labelAverageSurvivedTimeValue.Name = "labelAverageSurvivedTimeValue";
            this.labelAverageSurvivedTimeValue.Size = new System.Drawing.Size(0, 23);
            this.labelAverageSurvivedTimeValue.TabIndex = 8;
            // 
            // labelLongestKillValue
            // 
            this.labelLongestKillValue.AutoSize = true;
            this.labelLongestKillValue.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLongestKillValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelLongestKillValue.Location = new System.Drawing.Point(217, 245);
            this.labelLongestKillValue.Name = "labelLongestKillValue";
            this.labelLongestKillValue.Size = new System.Drawing.Size(0, 23);
            this.labelLongestKillValue.TabIndex = 12;
            // 
            // labelWinPercentValue
            // 
            this.labelWinPercentValue.AutoSize = true;
            this.labelWinPercentValue.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWinPercentValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelWinPercentValue.Location = new System.Drawing.Point(217, 68);
            this.labelWinPercentValue.Name = "labelWinPercentValue";
            this.labelWinPercentValue.Size = new System.Drawing.Size(0, 23);
            this.labelWinPercentValue.TabIndex = 7;
            // 
            // labelMaxKillsValue
            // 
            this.labelMaxKillsValue.AutoSize = true;
            this.labelMaxKillsValue.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMaxKillsValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelMaxKillsValue.Location = new System.Drawing.Point(217, 211);
            this.labelMaxKillsValue.Name = "labelMaxKillsValue";
            this.labelMaxKillsValue.Size = new System.Drawing.Size(0, 23);
            this.labelMaxKillsValue.TabIndex = 11;
            // 
            // labelWinsValue
            // 
            this.labelWinsValue.AutoSize = true;
            this.labelWinsValue.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWinsValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelWinsValue.Location = new System.Drawing.Point(217, 33);
            this.labelWinsValue.Name = "labelWinsValue";
            this.labelWinsValue.Size = new System.Drawing.Size(0, 23);
            this.labelWinsValue.TabIndex = 6;
            // 
            // labelHeadshotPercentValue
            // 
            this.labelHeadshotPercentValue.AutoSize = true;
            this.labelHeadshotPercentValue.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeadshotPercentValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelHeadshotPercentValue.Location = new System.Drawing.Point(217, 176);
            this.labelHeadshotPercentValue.Name = "labelHeadshotPercentValue";
            this.labelHeadshotPercentValue.Size = new System.Drawing.Size(0, 23);
            this.labelHeadshotPercentValue.TabIndex = 10;
            // 
            // labelGamesPlayedValue
            // 
            this.labelGamesPlayedValue.AutoSize = true;
            this.labelGamesPlayedValue.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGamesPlayedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelGamesPlayedValue.Location = new System.Drawing.Point(217, 1);
            this.labelGamesPlayedValue.Name = "labelGamesPlayedValue";
            this.labelGamesPlayedValue.Size = new System.Drawing.Size(0, 23);
            this.labelGamesPlayedValue.TabIndex = 5;
            // 
            // labelAdrValue
            // 
            this.labelAdrValue.AutoSize = true;
            this.labelAdrValue.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAdrValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelAdrValue.Location = new System.Drawing.Point(217, 141);
            this.labelAdrValue.Name = "labelAdrValue";
            this.labelAdrValue.Size = new System.Drawing.Size(0, 23);
            this.labelAdrValue.TabIndex = 9;
            // 
            // labelAverageSurvivedTime
            // 
            this.labelAverageSurvivedTime.AutoSize = true;
            this.labelAverageSurvivedTime.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAverageSurvivedTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelAverageSurvivedTime.Location = new System.Drawing.Point(-4, 103);
            this.labelAverageSurvivedTime.Name = "labelAverageSurvivedTime";
            this.labelAverageSurvivedTime.Size = new System.Drawing.Size(169, 23);
            this.labelAverageSurvivedTime.TabIndex = 4;
            this.labelAverageSurvivedTime.Text = "Avg survival time";
            // 
            // labelFraggerRating
            // 
            this.labelFraggerRating.AutoSize = true;
            this.labelFraggerRating.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFraggerRating.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelFraggerRating.Location = new System.Drawing.Point(-4, 278);
            this.labelFraggerRating.Name = "labelFraggerRating";
            this.labelFraggerRating.Size = new System.Drawing.Size(146, 23);
            this.labelFraggerRating.TabIndex = 8;
            this.labelFraggerRating.Text = "Fragger Rating";
            // 
            // labelWinPercent
            // 
            this.labelWinPercent.AutoSize = true;
            this.labelWinPercent.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWinPercent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelWinPercent.Location = new System.Drawing.Point(0, 68);
            this.labelWinPercent.Name = "labelWinPercent";
            this.labelWinPercent.Size = new System.Drawing.Size(64, 23);
            this.labelWinPercent.TabIndex = 3;
            this.labelWinPercent.Text = "Win %";
            // 
            // labelLongestKill
            // 
            this.labelLongestKill.AutoSize = true;
            this.labelLongestKill.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLongestKill.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelLongestKill.Location = new System.Drawing.Point(-4, 245);
            this.labelLongestKill.Name = "labelLongestKill";
            this.labelLongestKill.Size = new System.Drawing.Size(112, 23);
            this.labelLongestKill.TabIndex = 7;
            this.labelLongestKill.Text = "Longest Kill";
            // 
            // labelWins
            // 
            this.labelWins.AutoSize = true;
            this.labelWins.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWins.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelWins.Location = new System.Drawing.Point(0, 33);
            this.labelWins.Name = "labelWins";
            this.labelWins.Size = new System.Drawing.Size(56, 23);
            this.labelWins.TabIndex = 2;
            this.labelWins.Text = "Wins:";
            // 
            // labelMaxKills
            // 
            this.labelMaxKills.AutoSize = true;
            this.labelMaxKills.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMaxKills.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelMaxKills.Location = new System.Drawing.Point(-4, 211);
            this.labelMaxKills.Name = "labelMaxKills";
            this.labelMaxKills.Size = new System.Drawing.Size(91, 23);
            this.labelMaxKills.TabIndex = 6;
            this.labelMaxKills.Text = "Max Kills";
            // 
            // labelGamesPlayed
            // 
            this.labelGamesPlayed.AutoSize = true;
            this.labelGamesPlayed.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGamesPlayed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelGamesPlayed.Location = new System.Drawing.Point(-4, 1);
            this.labelGamesPlayed.Name = "labelGamesPlayed";
            this.labelGamesPlayed.Size = new System.Drawing.Size(147, 23);
            this.labelGamesPlayed.TabIndex = 0;
            this.labelGamesPlayed.Text = "Games Played";
            // 
            // labelHeadshotPercent
            // 
            this.labelHeadshotPercent.AutoSize = true;
            this.labelHeadshotPercent.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeadshotPercent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelHeadshotPercent.Location = new System.Drawing.Point(-4, 176);
            this.labelHeadshotPercent.Name = "labelHeadshotPercent";
            this.labelHeadshotPercent.Size = new System.Drawing.Size(63, 23);
            this.labelHeadshotPercent.TabIndex = 5;
            this.labelHeadshotPercent.Text = "H/S %";
            // 
            // labelAdr
            // 
            this.labelAdr.AutoSize = true;
            this.labelAdr.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAdr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(169)))), ((int)(((byte)(0)))));
            this.labelAdr.Location = new System.Drawing.Point(-4, 141);
            this.labelAdr.Name = "labelAdr";
            this.labelAdr.Size = new System.Drawing.Size(48, 23);
            this.labelAdr.TabIndex = 4;
            this.labelAdr.Text = "ADR";
            // 
            // InnerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(794, 946);
            this.Controls.Add(this.panel1);
            this.Name = "InnerForm";
            this.Text = "SquadFppForm";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graphPlotBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelGamesPlayed;
        private System.Windows.Forms.Label labelHeadshotPercent;
        private System.Windows.Forms.Label labelAdr;
        private System.Windows.Forms.Label labelWinPercent;
        private System.Windows.Forms.Label labelWins;
        private System.Windows.Forms.Label labelMaxKills;
        private System.Windows.Forms.Label labelFraggerRating;
        private System.Windows.Forms.Label labelLongestKill;
        private System.Windows.Forms.Label labelAverageSurvivedTime;
        private System.Windows.Forms.Label labelFraggerRatingValue;
        private System.Windows.Forms.Label labelLongestKillValue;
        private System.Windows.Forms.Label labelMaxKillsValue;
        private System.Windows.Forms.Label labelHeadshotPercentValue;
        private System.Windows.Forms.Label labelAdrValue;
        private System.Windows.Forms.Label labelAverageSurvivedTimeValue;
        private System.Windows.Forms.Label labelWinPercentValue;
        private System.Windows.Forms.Label labelWinsValue;
        private System.Windows.Forms.Label labelGamesPlayedValue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn yearDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn monthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource graphPlotBindingSource;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
    }
}