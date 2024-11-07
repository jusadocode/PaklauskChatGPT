namespace RAID2D.Client.Commands.DayTime;

public class SetDayCommand(DayTime dayTime) : ICommand
{
    public void Execute()
    {
        dayTime.SetDay();
    }

    public void Undo()
    {
        dayTime.SetNight();
    }
}

