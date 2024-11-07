namespace RAID2D.Client.Entities.Enemies.Prototype;

public interface IPrototype
{
    IPrototype ShallowClone();
    IPrototype DeepClone();
}
