using Client.Utils;

namespace Client.Entities.Animals;

public class Boar : IAnimal
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
                Name = "boar",
                Image = Assets.boardown,
                Left = Rand.Next(0, 900),
                Top = Rand.Next(0, 800),
                Size = new Size(145, 145),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };

            return PictureBox;
        }
    }
}
