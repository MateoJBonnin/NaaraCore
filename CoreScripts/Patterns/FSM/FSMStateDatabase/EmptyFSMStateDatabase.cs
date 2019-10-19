using System.Collections.Generic;

public class EmptyFSMStateDatabase<T> : DefaultFSMStateDatabase<T, EmptyFSMStateData>
{
    public EmptyFSMStateDatabase() : base()
    {
    }

    public EmptyFSMStateDatabase(Dictionary<T, FSMState<EmptyFSMStateData>> statesDatabase) : base(statesDatabase)
    {
    }
}