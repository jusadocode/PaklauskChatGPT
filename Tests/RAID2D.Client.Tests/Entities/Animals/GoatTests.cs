using RAID2D.Client.Entities.Animals;
using System.Windows.Forms;

namespace RAID2D.Client.Tests.Entities.Animals;
public class GoatTests
{
    private readonly Goat _testClass;

    public GoatTests()
    {
        _testClass = new Goat();
    }

    [Fact]
    public void CanCallCreate()
    {
        // Act
        var result = ((IAnimal)_testClass).Create();

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