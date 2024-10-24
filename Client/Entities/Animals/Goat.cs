namespace Client.Entities.Animals;

public class Goat : IAnimal
{
    public PictureBox? PictureBox { get; private set; }

    PictureBox IAnimal.Create()
    {
        if (PictureBox is not null)
        {
            return PictureBox;
        }
        else
        {
            PictureBox = new()
            {
                Tag = "animal",
                Name = "goat",
                Image = Assets.goatdown,
                Left = Rand.Next(0, 900),
                Top = Rand.Next(0, 800),
                Size = new Size(145, 145),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };

            return PictureBox;
        }
    }
}
