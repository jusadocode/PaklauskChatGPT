﻿using Client.Utils;

namespace Client.Entities.Animals;

public class Goat : IAnimal
{
    public PictureBox PictureBox { get; private set; } = new();

    PictureBox IAnimal.Create()
    {
        PictureBox = new()
        {
            Tag = Constants.AnimalTag,
            Name = Constants.GoatName,
            Image = Assets.AnimalGoat,
            Location = Rand.LocationOnScreen(Constants.AnimalSize),
            Size = Constants.AnimalSize,
            SizeMode = Constants.SizeMode,
        };

        return PictureBox;
    }
}
