using RAID2D.Client.Managers;
using System.Drawing;

namespace RAID2D.Client.Tests.Managers;
public static class DropManagerTests
{
    [Fact]
    public static void CanCallGetRandomAmmoDropData()
    {
        // Act
        var result = DropManager.GetRandomAmmoDropData();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public static void CanCallGetRandomAnimalDropDataByAnimalName()
    {
        // Arrange
        var animalName = "TestValue995101404";

        // Act
        var result = DropManager.GetRandomAnimalDropDataByAnimalName(animalName);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public static void CannotCallGetRandomAnimalDropDataByAnimalNameWithInvalidAnimalName(string value)
    {
        Assert.Throws<ArgumentNullException>(() => DropManager.GetRandomAnimalDropDataByAnimalName(value));
    }

    [Fact]
    public static void GetRandomAnimalDropDataByAnimalNamePerformsMapping()
    {
        // Arrange
        var animalName = "TestValue132551684";

        // Act
        var result = DropManager.GetRandomAnimalDropDataByAnimalName(animalName);

        // Assert
        Assert.Same(animalName, result.AnimalName);
    }

    [Fact]
    public static void CanCallGetRandomMedicalDropData()
    {
        // Act
        var result = DropManager.GetRandomMedicalDropData();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public static void CanCallGetRandomValuableDropData()
    {
        // Act
        var result = DropManager.GetRandomValuableDropData();

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Fact]
    public static void CanCallGetAmmoDropData()
    {
        // Arrange
        var dropName = "TestValue1925032451";

        // Act
        var result = DropManager.GetAmmoDropData(dropName);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public static void CannotCallGetAmmoDropDataWithInvalidDropName(string value)
    {
        Assert.Throws<ArgumentNullException>(() => DropManager.GetAmmoDropData(value));
    }

    [Fact]
    public static void CanCallGetAnimalDropData()
    {
        // Arrange
        var dropName = "TestValue1643356672";

        // Act
        var result = DropManager.GetAnimalDropData(dropName);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public static void CannotCallGetAnimalDropDataWithInvalidDropName(string value)
    {
        Assert.Throws<ArgumentNullException>(() => DropManager.GetAnimalDropData(value));
    }

    [Fact]
    public static void CanCallGetMedicalDropData()
    {
        // Arrange
        var dropName = "TestValue926484779";

        // Act
        var result = DropManager.GetMedicalDropData(dropName);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public static void CannotCallGetMedicalDropDataWithInvalidDropName(string value)
    {
        Assert.Throws<ArgumentNullException>(() => DropManager.GetMedicalDropData(value));
    }

    [Fact]
    public static void CanCallGetValuableDropData()
    {
        // Arrange
        var dropName = "TestValue2012669724";

        // Act
        var result = DropManager.GetValuableDropData(dropName);

        // Assert
        throw new NotImplementedException("Create or modify test");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public static void CannotCallGetValuableDropDataWithInvalidDropName(string value)
    {
        Assert.Throws<ArgumentNullException>(() => DropManager.GetValuableDropData(value));
    }
}

public class AmmoDropDataTests
{
    private readonly AmmoDropData _testClass;
    private string _name;
    private uint _ammoAmount;
    private uint _dropChance;
    private Image _image;

    public AmmoDropDataTests()
    {
        _name = "TestValue2146812240";
        _ammoAmount = (uint)1079347513;
        _dropChance = (uint)1514883232;
        _image = new Bitmap(1, 1);
        _testClass = new AmmoDropData(_name, _ammoAmount, _dropChance, _image);
    }

    [Fact]
    public void CanConstruct()
    {
        // Act
        var instance = new AmmoDropData(_name, _ammoAmount, _dropChance, _image);

        // Assert
        Assert.NotNull(instance);
    }

    [Fact]
    public void ImplementsIEquatable_AmmoDropData()
    {
        // Arrange
        var same = new AmmoDropData(_name, _ammoAmount, _dropChance, _image);
        var different = new AmmoDropData("TestValue787680328", (uint)817980181, (uint)1646515868, new Bitmap(1, 1));

        // Assert
        Assert.False(_testClass.Equals(default(object)));
        Assert.False(_testClass.Equals(new object()));
        Assert.True(_testClass.Equals((object)same));
        Assert.False(_testClass.Equals((object)different));
        Assert.True(_testClass.Equals(same));
        Assert.False(_testClass.Equals(different));
        Assert.Equal(same.GetHashCode(), _testClass.GetHashCode());
        Assert.NotEqual(different.GetHashCode(), _testClass.GetHashCode());
        Assert.True(_testClass == same);
        Assert.False(_testClass == different);
        Assert.False(_testClass != same);
        Assert.True(_testClass != different);
    }

    [Fact]
    public void NameIsInitializedCorrectly()
    {
        Assert.Equal(_name, _testClass.Name);
    }

    [Fact]
    public void AmmoAmountIsInitializedCorrectly()
    {
        Assert.Equal(_ammoAmount, _testClass.AmmoAmount);
    }

    [Fact]
    public void DropChanceIsInitializedCorrectly()
    {
        Assert.Equal(_dropChance, _testClass.DropChance);
    }

    [Fact]
    public void ImageIsInitializedCorrectly()
    {
        Assert.Same(_image, _testClass.Image);
    }
}

public class AnimalDropDataTests
{
    private readonly AnimalDropData _testClass;
    private string _name;
    private string _animalName;
    private uint _healthAmount;
    private uint _dropChance;
    private Image _image;

    public AnimalDropDataTests()
    {
        _name = "TestValue1418130559";
        _animalName = "TestValue1838347350";
        _healthAmount = (uint)88123424;
        _dropChance = (uint)124081663;
        _image = new Bitmap(1, 1);
        _testClass = new AnimalDropData(_name, _animalName, _healthAmount, _dropChance, _image);
    }

    [Fact]
    public void CanConstruct()
    {
        // Act
        var instance = new AnimalDropData(_name, _animalName, _healthAmount, _dropChance, _image);

        // Assert
        Assert.NotNull(instance);
    }

    [Fact]
    public void ImplementsIEquatable_AnimalDropData()
    {
        // Arrange
        var same = new AnimalDropData(_name, _animalName, _healthAmount, _dropChance, _image);
        var different = new AnimalDropData("TestValue1503418710", "TestValue2092873945", (uint)315547292, (uint)1856310978, new Bitmap(1, 1));

        // Assert
        Assert.False(_testClass.Equals(default(object)));
        Assert.False(_testClass.Equals(new object()));
        Assert.True(_testClass.Equals((object)same));
        Assert.False(_testClass.Equals((object)different));
        Assert.True(_testClass.Equals(same));
        Assert.False(_testClass.Equals(different));
        Assert.Equal(same.GetHashCode(), _testClass.GetHashCode());
        Assert.NotEqual(different.GetHashCode(), _testClass.GetHashCode());
        Assert.True(_testClass == same);
        Assert.False(_testClass == different);
        Assert.False(_testClass != same);
        Assert.True(_testClass != different);
    }

    [Fact]
    public void NameIsInitializedCorrectly()
    {
        Assert.Equal(_name, _testClass.Name);
    }

    [Fact]
    public void AnimalNameIsInitializedCorrectly()
    {
        Assert.Equal(_animalName, _testClass.AnimalName);
    }

    [Fact]
    public void HealthAmountIsInitializedCorrectly()
    {
        Assert.Equal(_healthAmount, _testClass.HealthAmount);
    }

    [Fact]
    public void DropChanceIsInitializedCorrectly()
    {
        Assert.Equal(_dropChance, _testClass.DropChance);
    }

    [Fact]
    public void ImageIsInitializedCorrectly()
    {
        Assert.Same(_image, _testClass.Image);
    }
}

public class MedicalDropDataTests
{
    private readonly MedicalDropData _testClass;
    private string _name;
    private uint _healthAmount;
    private uint _dropChance;
    private Image _image;

    public MedicalDropDataTests()
    {
        _name = "TestValue1916405786";
        _healthAmount = (uint)386908071;
        _dropChance = (uint)22394455;
        _image = new Bitmap(1, 1);
        _testClass = new MedicalDropData(_name, _healthAmount, _dropChance, _image);
    }

    [Fact]
    public void CanConstruct()
    {
        // Act
        var instance = new MedicalDropData(_name, _healthAmount, _dropChance, _image);

        // Assert
        Assert.NotNull(instance);
    }

    [Fact]
    public void ImplementsIEquatable_MedicalDropData()
    {
        // Arrange
        var same = new MedicalDropData(_name, _healthAmount, _dropChance, _image);
        var different = new MedicalDropData("TestValue83270459", (uint)727551261, (uint)1370124080, new Bitmap(1, 1));

        // Assert
        Assert.False(_testClass.Equals(default(object)));
        Assert.False(_testClass.Equals(new object()));
        Assert.True(_testClass.Equals((object)same));
        Assert.False(_testClass.Equals((object)different));
        Assert.True(_testClass.Equals(same));
        Assert.False(_testClass.Equals(different));
        Assert.Equal(same.GetHashCode(), _testClass.GetHashCode());
        Assert.NotEqual(different.GetHashCode(), _testClass.GetHashCode());
        Assert.True(_testClass == same);
        Assert.False(_testClass == different);
        Assert.False(_testClass != same);
        Assert.True(_testClass != different);
    }

    [Fact]
    public void NameIsInitializedCorrectly()
    {
        Assert.Equal(_name, _testClass.Name);
    }

    [Fact]
    public void HealthAmountIsInitializedCorrectly()
    {
        Assert.Equal(_healthAmount, _testClass.HealthAmount);
    }

    [Fact]
    public void DropChanceIsInitializedCorrectly()
    {
        Assert.Equal(_dropChance, _testClass.DropChance);
    }

    [Fact]
    public void ImageIsInitializedCorrectly()
    {
        Assert.Same(_image, _testClass.Image);
    }
}

public class ValuableDropDataTests
{
    private readonly ValuableDropData _testClass;
    private string _name;
    private uint _cashAmount;
    private uint _dropChance;
    private Image _image;

    public ValuableDropDataTests()
    {
        _name = "TestValue1477099031";
        _cashAmount = (uint)168021216;
        _dropChance = (uint)242539979;
        _image = new Bitmap(1, 1);
        _testClass = new ValuableDropData(_name, _cashAmount, _dropChance, _image);
    }

    [Fact]
    public void CanConstruct()
    {
        // Act
        var instance = new ValuableDropData(_name, _cashAmount, _dropChance, _image);

        // Assert
        Assert.NotNull(instance);
    }

    [Fact]
    public void ImplementsIEquatable_ValuableDropData()
    {
        // Arrange
        var same = new ValuableDropData(_name, _cashAmount, _dropChance, _image);
        var different = new ValuableDropData("TestValue403937213", (uint)1131928258, (uint)1460731056, new Bitmap(1, 1));

        // Assert
        Assert.False(_testClass.Equals(default(object)));
        Assert.False(_testClass.Equals(new object()));
        Assert.True(_testClass.Equals((object)same));
        Assert.False(_testClass.Equals((object)different));
        Assert.True(_testClass.Equals(same));
        Assert.False(_testClass.Equals(different));
        Assert.Equal(same.GetHashCode(), _testClass.GetHashCode());
        Assert.NotEqual(different.GetHashCode(), _testClass.GetHashCode());
        Assert.True(_testClass == same);
        Assert.False(_testClass == different);
        Assert.False(_testClass != same);
        Assert.True(_testClass != different);
    }

    [Fact]
    public void NameIsInitializedCorrectly()
    {
        Assert.Equal(_name, _testClass.Name);
    }

    [Fact]
    public void CashAmountIsInitializedCorrectly()
    {
        Assert.Equal(_cashAmount, _testClass.CashAmount);
    }

    [Fact]
    public void DropChanceIsInitializedCorrectly()
    {
        Assert.Equal(_dropChance, _testClass.DropChance);
    }

    [Fact]
    public void ImageIsInitializedCorrectly()
    {
        Assert.Same(_image, _testClass.Image);
    }
}