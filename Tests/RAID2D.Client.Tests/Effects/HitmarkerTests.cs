using RAID2D.Client.Effects;
using System.Drawing;
using System.Windows.Forms;

namespace RAID2D.Client.Tests.Effects;
public class HitmarkerTests
{
    private readonly Hitmarker _testClass;

    public HitmarkerTests()
    {
        _testClass = new Hitmarker();
    }

    [Fact]
    public void CanCallCreatePictureBox()
    {
        // Arrange
        var hitmarkLocation = new Point();
        Action<PictureBox> onHitmarkerExpired = x => { };

        // Act
        var result = _testClass.CreatePictureBox(hitmarkLocation, onHitmarkerExpired);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public void CanGetPictureBox()
    {
        // Assert
        Assert.IsType<PictureBox>(_testClass.PictureBox);

        throw new NotImplementedException("Create or modify test");
    }
}