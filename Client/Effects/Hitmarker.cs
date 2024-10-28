﻿using Client.Utils;

namespace Client.Effects;

public class Hitmarker
{
    public PictureBox PictureBox { get; private set; } = new();

    public PictureBox CreatePictureBox(Point hitmarkLocation, Action<PictureBox>? onHitmarkerExpired)
    {
        PictureBox = new()
        {
            Tag = Constants.HitmarkerTag,
            Name = Constants.HitmarkerTag,
            Image = Assets.Hitmarker,
            Location = hitmarkLocation,
            Size = Constants.HitmarkerSize,
            SizeMode = Constants.SizeMode,
        };

        Timer? timer = new()
        {
            Enabled = true,
            Interval = Constants.HitmarkDuration,
        };
        timer.Tick += (s, e) =>
        {
            onHitmarkerExpired?.Invoke(PictureBox);

            timer.Stop();
            timer.Dispose();
            timer = null;
        };

        Console.WriteLine($"Spawned hitmarker at {PictureBox.Location}");

        return PictureBox;
    }
}
