using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAID2D.Client.States;
public static class StateFlyweightFactory
{
    private static readonly Dictionary<string, IEntityState> states = new();

    static StateFlyweightFactory()
    {
        states["Flee"] = new FleeState();
        states["Chase"] = new ChaseState();
        states["Idle"] = new IdleState();
    }

    public static IEntityState GetState(string stateName)
    {
        if (!states.ContainsKey(stateName))
            throw new ArgumentException($"State '{stateName}' does not exist.");

        return states[stateName];
    }
}
