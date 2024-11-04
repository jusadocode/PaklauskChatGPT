using RAID2D.Client.Managers;
using RAID2D.Shared.Models;

namespace RAID2D.Client.Players;

public class ServerPlayer
{
    public PictureBox? PictureBox { get; private set; } = null;
    public bool IsRendered = false;

    public void Create(GameState gameState)
    {
        PictureBox = new()
        {
            Tag = "test123",
            Name = "test345",
            Image = ImageManager.GetImageFromDirection(Constants.PlayerName, gameState.Direction),
            Location = gameState.Location,
            Size = Constants.PlayerSize,
            SizeMode = Constants.SizeMode,
        };

        Console.WriteLine($"Spawned Server Player at {PictureBox.Location}");
    }

    public void Update(GameState gameState)
    {
        if (PictureBox == null)
            return;

        PictureBox.Image = ImageManager.GetImageFromDirection(Constants.PlayerName, gameState.Direction);
        PictureBox.Location = gameState.Location;
    }
}
