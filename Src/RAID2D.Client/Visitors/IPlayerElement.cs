namespace RAID2D.Client.Visitors;
public interface IPlayerElement
{
    void Accept(IPlayerVisitor visitor);
}
