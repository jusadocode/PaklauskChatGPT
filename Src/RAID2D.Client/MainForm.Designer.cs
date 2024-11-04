namespace RAID2D.Client
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.AmmoLabel = new Label();
            this.KillsLabel = new Label();
            this.HealthLabel = new Label();
            this.HealthBar = new ProgressBar();
            this.CashLabel = new Label();
            this.FpsLabel = new Label();
            this.SuspendLayout();
            // 
            // AmmoLabel
            // 
            this.AmmoLabel.AutoSize = true;
            this.AmmoLabel.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold);
            this.AmmoLabel.ForeColor = Color.White;
            this.AmmoLabel.Location = new Point(12, 1047);
            this.AmmoLabel.Name = "AmmoLabel";
            this.AmmoLabel.Size = new Size(93, 24);
            this.AmmoLabel.TabIndex = 0;
            this.AmmoLabel.Text = "Ammo: 0";
            // 
            // KillsLabel
            // 
            this.KillsLabel.AutoSize = true;
            this.KillsLabel.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold);
            this.KillsLabel.ForeColor = Color.White;
            this.KillsLabel.Location = new Point(878, 9);
            this.KillsLabel.Name = "KillsLabel";
            this.KillsLabel.Size = new Size(71, 24);
            this.KillsLabel.TabIndex = 0;
            this.KillsLabel.Text = "Kills: 0";
            // 
            // HealthLabel
            // 
            this.HealthLabel.AutoSize = true;
            this.HealthLabel.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold);
            this.HealthLabel.ForeColor = Color.White;
            this.HealthLabel.Location = new Point(1633, 1047);
            this.HealthLabel.Name = "HealthLabel";
            this.HealthLabel.Size = new Size(82, 24);
            this.HealthLabel.TabIndex = 0;
            this.HealthLabel.Text = "Health: ";
            // 
            // HealthBar
            // 
            this.HealthBar.Location = new Point(1721, 1048);
            this.HealthBar.Name = "HealthBar";
            this.HealthBar.Size = new Size(187, 23);
            this.HealthBar.Step = 1;
            this.HealthBar.TabIndex = 1;
            this.HealthBar.Value = 100;
            // 
            // CashLabel
            // 
            this.CashLabel.AutoSize = true;
            this.CashLabel.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold);
            this.CashLabel.ForeColor = Color.White;
            this.CashLabel.Location = new Point(972, 9);
            this.CashLabel.Name = "CashLabel";
            this.CashLabel.Size = new Size(80, 24);
            this.CashLabel.TabIndex = 3;
            this.CashLabel.Text = "Cash: 0";
            // 
            // FpsLabel
            // 
            this.FpsLabel.AutoSize = true;
            this.FpsLabel.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold);
            this.FpsLabel.ForeColor = Color.White;
            this.FpsLabel.Location = new Point(12, 9);
            this.FpsLabel.Name = "FpsLabel";
            this.FpsLabel.Size = new Size(72, 24);
            this.FpsLabel.TabIndex = 4;
            this.FpsLabel.Text = "FPS: 0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.DarkOliveGreen;
            this.ClientSize = new Size(1920, 1080);
            this.Controls.Add(this.FpsLabel);
            this.Controls.Add(this.CashLabel);
            this.Controls.Add(this.HealthBar);
            this.Controls.Add(this.HealthLabel);
            this.Controls.Add(this.KillsLabel);
            this.Controls.Add(this.AmmoLabel);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Icon = (Icon)resources.GetObject("$this.Icon");
            this.Name = "MainForm";
            this.Text = "RAID2D";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label AmmoLabel;
        private System.Windows.Forms.Label KillsLabel;
        private System.Windows.Forms.Label HealthLabel;
        private System.Windows.Forms.ProgressBar HealthBar;
        private System.Windows.Forms.Label CashLabel;
        private Label FpsLabel;
    }
}

