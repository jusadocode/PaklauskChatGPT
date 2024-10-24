﻿using Client.Utils;

namespace Client.Entities.Enemies;

public class Zombie : IEnemy
{
    public PictureBox? PictureBox { get; private set; }

    PictureBox IEnemy.Create()
    {
        if (PictureBox is not null)
        {
            return PictureBox;
        }
        else
        {
            PictureBox = new()
            {
                Tag = "enemy",
                Name = "zombie",
                Image = Assets.zdown,
                Left = Rand.Next(0, 900),
                Top = Rand.Next(0, 800),
                SizeMode = PictureBoxSizeMode.AutoSize
            };

            return PictureBox;
        }
    }
}

