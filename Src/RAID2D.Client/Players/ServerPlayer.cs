using RAID2D.Client.Managers;
using RAID2D.Client.Visitors;
using RAID2D.Shared.Models;

namespace RAID2D.Client.Players;

public class ServerPlayer : IPlayerElement 
{
    public PictureBox? PictureBox { get; private set; } = null;
    public bool IsRendered = false;

    public uint MaxKills { get; private set; }
    public uint MaxCash { get; private set; }

    public void Create(GameState gameState)
    {
        PictureBox = new()
        {
            Tag = Constants.ServerPlayerTag,
            Name = Constants.ServerPlayerName,
            Image = ImageManager.GetImageFromDirection(Constants.PlayerName, gameState.Direction),
            Location = gameState.Location,
            Size = Constants.PlayerSize,
            SizeMode = Constants.SizeMode,
        };

        Console.WriteLine($"Spawned Server Player at {PictureBox.Location}");
    }

    public void Update(GameState gameState)
    {
        if (MaxKills < gameState.Kills)
            MaxKills = gameState.Kills;

        if (MaxCash < gameState.Cash)
            MaxCash = gameState.Cash;

        if (PictureBox == null)
            return;

        PictureBox.Image = gameState.IsDead ? Assets.PlayerDead : ImageManager.GetImageFromDirection(Constants.PlayerName, gameState.Direction);
        PictureBox.Location = gameState.Location;
    }

    public void Accept(IPlayerVisitor visitor)
    {
        visitor.Visit(this);
    }
}
