namespace RAID2D.Client.Commands;

public interface ICommand
{
    public void Execute();
    public void Undo();
}
