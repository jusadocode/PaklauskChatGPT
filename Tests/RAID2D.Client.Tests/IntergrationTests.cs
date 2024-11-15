using Moq;
using RAID2D.Client.Services;
using RAID2D.Shared.Enums;
using RAID2D.Shared.Models;
using System.Windows.Forms;

namespace RAID2D.Client.Tests;

public class IntergrationTests
{
    [Fact]
    public void PlayerTakesDamage_WhenCollidingWithEnemy()
    {

        var form = new MainForm();
        form.RestartGame();

        var player = form.Controls.OfType<PictureBox>().FirstOrDefault(pb => pb.Tag as string == Constants.PlayerTag);
        var enemy = new PictureBox { Tag = Constants.EnemyTag, Bounds = player.Bounds };

        form.Controls.Add(enemy);
        int initialHealth = form.player.Health;

        form.HandleEnemyInteraction(enemy);

        Assert.True(form.player.Health < initialHealth);
    }
    [Fact]
    public void RestartGame_RespawnsPlayerAndSpawnsEntities()
    {

        var form = new MainForm();

        form.RestartGame();

        var player = form.Controls.OfType<PictureBox>().FirstOrDefault(pb => pb.Tag as string == Constants.PlayerTag);
        Assert.NotNull(player);

        var animals = form.Controls.OfType<PictureBox>().Count(pb => pb.Tag as string == Constants.AnimalTag);
        var enemies = form.Controls.OfType<PictureBox>().Count(pb => pb.Tag as string == Constants.EnemyTag);

        Assert.Equal((double)Constants.AnimalCount, animals);
        Assert.Equal((double)Constants.EnemyCount, enemies);
    }

    [Fact]
    public void SendDataToServer_SendsGameState_WhenConnected()
    {
        var mockServerConnection = new Mock<ServerConnection>();
        mockServerConnection.Setup(server => server.IsConnected()).Returns(true);

        var form = new MainForm
        (
           mockServerConnection.Object 
        );

        form.player.PictureBox.Location = new System.Drawing.Point(100, 100);
        form.player.Direction = Direction.Left;

        form.SendDataToServer();

        mockServerConnection.Verify(
            server => server.SendGameStateAsync(It.Is<GameState>(gs =>
                gs.Location == form.player.PictureBox.Location &&
                gs.Direction == form.player.Direction)),
            Times.Once,
            "Game state should be sent to the server when connected."
        );
    }
}
