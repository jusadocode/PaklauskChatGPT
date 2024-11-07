namespace RAID2D.Client.Commands.DayTime;

public class SetNightCommand(DayTimeUnit dayTime) : ICommand
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
