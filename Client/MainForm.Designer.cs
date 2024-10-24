namespace Client
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.AmmoLabel = new Label();
            this.KillsLabel = new Label();
            this.HealthLabel = new Label();
            this.HealthBar = new ProgressBar();
            this.GameTimer = new Timer(this.components);
            this.CashLabel = new Label();
            this.Player = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)this.Player).BeginInit();
            this.SuspendLayout();
            // 
            // AmmoLabel
            // 
            resources.ApplyResources(this.AmmoLabel, "AmmoLabel");
            this.AmmoLabel.ForeColor = Color.White;
            this.AmmoLabel.Name = "AmmoLabel";
            // 
            // KillsLabel
            // 
            resources.ApplyResources(this.KillsLabel, "KillsLabel");
            this.KillsLabel.ForeColor = Color.White;
            this.KillsLabel.Name = "KillsLabel";
            // 
            // HealthLabel
            // 
            resources.ApplyResources(this.HealthLabel, "HealthLabel");
            this.HealthLabel.ForeColor = Color.White;
            this.HealthLabel.Name = "HealthLabel";
            // 
            // HealthBar
            // 
            resources.ApplyResources(this.HealthBar, "HealthBar");
            this.HealthBar.Name = "HealthBar";
            this.HealthBar.Value = 100;
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 20;
            this.GameTimer.Tick += this.MainTimerEvent;
            // 
            // CashLabel
            // 
            resources.ApplyResources(this.CashLabel, "CashLabel");
            this.CashLabel.ForeColor = Color.White;
            this.CashLabel.Name = "CashLabel";
            // 
            // Player
            // 
            this.Player.Image = Assets.PlayerUp;
            resources.ApplyResources(this.Player, "Player");
            this.Player.Name = "Player";
            this.Player.TabStop = false;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.DarkOliveGreen;
            this.Controls.Add(this.CashLabel);
            this.Controls.Add(this.Player);
            this.Controls.Add(this.HealthBar);
            this.Controls.Add(this.HealthLabel);
            this.Controls.Add(this.KillsLabel);
            this.Controls.Add(this.AmmoLabel);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "MainForm";
            KeyDown += this.KeyIsDown;
            KeyUp += this.KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)this.Player).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label AmmoLabel;
        private System.Windows.Forms.Label KillsLabel;
        private System.Windows.Forms.Label HealthLabel;
        private System.Windows.Forms.ProgressBar HealthBar;
        private System.Windows.Forms.PictureBox Player;
        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Label CashLabel;
    }
}

