using RAID2D.Client.Players;
using RAID2D.Client.Utils;
using Xunit.Abstractions;

namespace RAID2D.Client.Tests;

public class PlayerTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void TestIfPlayerSpawnsInMiddleOfScreen()
    {
        Player player = new();
        player.Create();

        testOutputHelper.WriteLine(player.PictureBox.Location.ToString());

        Assert.Equal(player.PictureBox.Location, Location.MiddleOfScreen(Constants.PlayerSize));
    }
}
