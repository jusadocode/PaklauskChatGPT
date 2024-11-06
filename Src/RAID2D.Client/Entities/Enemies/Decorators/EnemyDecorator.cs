﻿using RAID2D.Client;
using RAID2D.Client.Entities.Enemies;

public abstract class EnemyDecorator : IEnemy
{
    private readonly IEnemy baseEnemy;

    public EnemyDecorator(IEnemy enemy)
    {
        baseEnemy = enemy;
    }

    public PictureBox PictureBox => baseEnemy.PictureBox;

    public abstract void UpdateAppearance();

    public PictureBox Create()
    {
        PictureBox pictureBox = baseEnemy.Create();
        UpdateAppearance();
        return pictureBox;
    }

}
