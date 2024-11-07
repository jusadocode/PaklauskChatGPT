namespace RAID2D.Client.Commands.DayTime;

public class SetNightCommand(DayTime dayTime) : ICommand
{
    public void Execute()
    {
        dayTime.SetNight();
    }

    public void Undo()
    {
        dayTime.SetDay();
    }
}
