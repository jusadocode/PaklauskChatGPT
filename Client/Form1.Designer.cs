namespace Client
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            txtAmmo = new Label();
            txtScore = new Label();
            label1 = new Label();
            healthBar = new ProgressBar();
            GameTimer = new System.Windows.Forms.Timer(components);
            valueLabel = new Label();
            player = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            SuspendLayout();
            // 
            // txtAmmo
            // 
            txtAmmo.AutoSize = true;
            txtAmmo.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtAmmo.ForeColor = Color.White;
            txtAmmo.Location = new Point(12, 953);
            txtAmmo.Name = "txtAmmo";
            txtAmmo.Size = new Size(93, 24);
            txtAmmo.TabIndex = 0;
            txtAmmo.Text = "Ammo: 0";
            // 
            // txtScore
            // 
            txtScore.AutoSize = true;
            txtScore.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtScore.ForeColor = Color.White;
            txtScore.Location = new Point(832, 9);
            txtScore.Name = "txtScore";
            txtScore.Size = new Size(71, 24);
            txtScore.TabIndex = 0;
            txtScore.Text = "Kills: 0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(1613, 953);
            label1.Name = "label1";
            label1.Size = new Size(82, 24);
            label1.TabIndex = 0;
            label1.Text = "Health: ";
            // 
            // healthBar
            // 
            healthBar.Location = new Point(1701, 954);
            healthBar.Name = "healthBar";
            healthBar.Size = new Size(187, 23);
            healthBar.TabIndex = 1;
            healthBar.Value = 100;
            // 
            // GameTimer
            // 
            GameTimer.Enabled = true;
            GameTimer.Interval = 20;
            GameTimer.Tick += MainTimerEvent;
            // 
            // valueLabel
            // 
            valueLabel.AutoSize = true;
            valueLabel.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            valueLabel.ForeColor = Color.White;
            valueLabel.Location = new Point(943, 9);
            valueLabel.Name = "valueLabel";
            valueLabel.Size = new Size(87, 24);
            valueLabel.TabIndex = 3;
            valueLabel.Text = "Value: 0";
            // 
            // player
            // 
            player.Image = Properties.Resources.up;
            player.Location = new Point(427, 471);
            player.Name = "player";
            player.Size = new Size(71, 100);
            player.SizeMode = PictureBoxSizeMode.AutoSize;
            player.TabIndex = 2;
            player.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkOliveGreen;
            ClientSize = new Size(1904, 1041);
            Controls.Add(valueLabel);
            Controls.Add(player);
            Controls.Add(healthBar);
            Controls.Add(label1);
            Controls.Add(txtScore);
            Controls.Add(txtAmmo);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Zombie Shootout Game MOO ICT";
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)player).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label txtAmmo;
        private System.Windows.Forms.Label txtScore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar healthBar;
        private System.Windows.Forms.PictureBox player;
        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Label valueLabel;
    }
}

