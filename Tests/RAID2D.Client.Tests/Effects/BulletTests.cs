using RAID2D.Client.Effects;
using RAID2D.Shared.Enums;
using System.Drawing;
using System.Windows.Forms;

namespace RAID2D.Client.Tests.Effects;
public static class BulletTests
{
    [Fact]
    public static void CanCallCreate()
    {
        // Arrange
        var direction = Direction.Left;
        var location = new Point();
        Action<PictureBox> onBulletCreated = x => { };
        Action<PictureBox> onBulletExpired = x => { };

        // Act
        Bullet.Create(direction, location, onBulletCreated, onBulletExpired);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public static void CannotCallCreateWithNullOnBulletCreated()
    {
        Assert.Throws<ArgumentNullException>(() => Bullet.Create(Direction.Left, new Point(), default(Action<PictureBox>), x => { }));
    }

    [Fact]
    public static void CannotCallCreateWithNullOnBulletExpired()
    {
        Assert.Throws<ArgumentNullException>(() => Bullet.Create(Direction.Left, new Point(), x => { }, default(Action<PictureBox>)));
    }
}