namespace RAID2D.Client.Commands.DayTime;

/// <summary>
/// The remote control class to execute commands. (Invoker)
/// </summary>
public class DayTimeController
{
    private readonly Stack<ICommand> commandHistory = new();
    private ICommand? currentCommand;

    public void SetCommand(ICommand command)
    {
        this.currentCommand = command;
    }

    public void Run()
    {
        if (currentCommand == null)
            throw new InvalidOperationException("Command is not set.");

        currentCommand.Execute();
        commandHistory.Push(currentCommand);
    }

    public void Undo()
    {
        if (commandHistory.Count == 0)
        {
            Console.WriteLine("No command to undo.");
            return;
        }

        ICommand lastCommand = commandHistory.Pop();
        lastCommand.Undo();
    }
}
