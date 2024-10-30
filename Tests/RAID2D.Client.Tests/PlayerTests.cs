using RAID2D.Client.Utils;
using Xunit.Abstractions;

namespace RAID2D.Client.Tests;

public class PlayerTests(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper output = testOutputHelper;

    [Fact]
    public void TestIfPlayerSpawnsInMiddleOfScreen()
    {
        Player player = new();
        player.Create();

        output.WriteLine(player.PictureBox.Location.ToString());

        Assert.Equal(player.PictureBox.Location, Location.MiddleOfScreen(Constants.PlayerSize));
    }
}
