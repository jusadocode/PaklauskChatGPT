﻿using Client.Utils;

namespace Client.Entities.Animals;

public class Boar : IAnimal
{
    public PictureBox PictureBox { get; private set; } = new();

    PictureBox IAnimal.Create()
    {
        PictureBox = new()
        {
            Tag = Constants.AnimalTag,
            Name = Constants.BoarName,
            Image = Assets.AnimalBoar,
            Location = Rand.LocationOnScreen(Constants.AnimalSize),
            Size = Constants.AnimalSize,
            SizeMode = Constants.SizeMode,
        };

        return PictureBox;
    }
}
