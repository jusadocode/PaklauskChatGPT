﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace RAID2D.Client.Entities.Enemies.Decorators;

internal class PulsingEnemyDecorator : EnemyDecorator
{
    private int pulseSpeed = 1; 
    private float currentIntensity = 0f; 

    private Timer pulseTimer;

    public PulsingEnemyDecorator(IEnemy enemy) : base(enemy)
    {
        pulseTimer = new Timer();
        pulseTimer.Interval = 100; 
        pulseTimer.Tick += (sender, e) => UpdatePulseEffect();
        pulseTimer.Start();
        this.PictureBox.Tag += Constants.PulsingEnemyTag;

        UpdateAppearance();

    }

    // Updates the appearance of the enemy, applying a pulsing effect
    public override void UpdateAppearance()
    {
        Bitmap originalImage = new Bitmap(this.PictureBox.Image);
        Bitmap pulsingImage = new Bitmap(originalImage.Width, originalImage.Height);

        using (Graphics g = Graphics.FromImage(pulsingImage))
        {
            g.DrawImage(originalImage, 0, 0);

            using (Brush redBrush = new SolidBrush(Color.FromArgb((int)(currentIntensity * 255), Color.Red)))
            {
                g.FillRectangle(redBrush, 0, 0, pulsingImage.Width, pulsingImage.Height);
            }
        }

        this.PictureBox.Image = pulsingImage;
            
    }

    private void UpdatePulseEffect()
    {
        currentIntensity += pulseSpeed * 0.05f;

        if (currentIntensity >= 1f || currentIntensity <= 0f)
        {
            pulseSpeed = -pulseSpeed;
        }

        UpdateAppearance();
    }
}

