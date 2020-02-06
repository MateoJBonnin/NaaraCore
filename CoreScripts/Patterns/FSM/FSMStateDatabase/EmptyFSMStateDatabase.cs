using System.Collections.Generic;

public class EmptyFSMStateDatabase<Key> : EmptyFSMStateDatabaseCustomState<FSMState<EmptyFSMStateData>, Key>
{
    public EmptyFSMStateDatabase(Dictionary<Key, FSMState<EmptyFSMStateData>> statesDatabase) : base(statesDatabase)
    {
    }
}

public class EmptyFSMStateDatabaseCustomState<State, Key> : DefaultFSMStateDatabaseCustomState<State, Key, EmptyFSMStateData> where State : FSMState<EmptyFSMStateData>
{
    public EmptyFSMStateDatabaseCustomState() : base()
    {
    }

    public EmptyFSMStateDatabaseCustomState(Dictionary<Key, State> statesDatabase) : base(statesDatabase)
    {
    }
}