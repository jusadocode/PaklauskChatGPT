namespace RAID2D.Client.States;

public static class StateFlyweightFactory
{
    private static readonly Dictionary<string, IEntityState> states = [];

    static StateFlyweightFactory()
    {
        states["Flee"] = new FleeState();
        states["Chase"] = new ChaseState();
        states["Wander"] = new WanderState();
    }

    public static IEntityState GetState(string stateName)
    {
        if (!states.ContainsKey(stateName))
            throw new ArgumentException($"State '{stateName}' does not exist.");

        return states[stateName];
    }
}
