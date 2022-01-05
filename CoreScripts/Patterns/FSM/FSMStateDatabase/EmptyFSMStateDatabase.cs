using System.Collections.Generic;

public class EmptyFSMStateDatabase<Key> : DefaultFSMStateDatabase<Key, EmptyFSMStateData>
{
    public EmptyFSMStateDatabase(Dictionary<Key, IFSMState<EmptyFSMStateData>> statesDatabase) : base(statesDatabase)
    {
    }
}

public class EmptyFSMStateDatabaseCustomState<State, Key> : DefaultFSMStateDatabaseCustomState<State, Key, EmptyFSMStateData> where State : IFSMState<EmptyFSMStateData>
{
    public EmptyFSMStateDatabaseCustomState()
    {
    }

    public EmptyFSMStateDatabaseCustomState(Dictionary<Key, State> statesDatabase) : base(statesDatabase)
    {
    }
}
