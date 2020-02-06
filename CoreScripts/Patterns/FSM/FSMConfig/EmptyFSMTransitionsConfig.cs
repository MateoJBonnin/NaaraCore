public class EmptyFSMTransitionsConfig<Key> : EmptyFSMTransitionsConfigCustomState<FSMState<EmptyFSMStateData>, Key>
{
    public EmptyFSMTransitionsConfig(EmptyFSMStateDatabaseCustomState<FSMState<EmptyFSMStateData>, Key> stateDatabase) : base(stateDatabase)
    {
    }

    public EmptyFSMTransitionsConfig(FSMStateLinksData<Key> configData, EmptyFSMStateDatabaseCustomState<FSMState<EmptyFSMStateData>, Key> stateDatabase) : base(configData, stateDatabase)
    {
    }
}

public class EmptyFSMTransitionsConfigCustomState<State, Key> : DefaultFSMTransitionsConfigCustomState<EmptyFSMStateDatabaseCustomState<State, Key>, State, Key, EmptyFSMStateData> where State : FSMState<EmptyFSMStateData>
{
    public EmptyFSMTransitionsConfigCustomState(EmptyFSMStateDatabaseCustomState<State, Key> stateDatabase) : base(stateDatabase)
    {
    }

    public EmptyFSMTransitionsConfigCustomState(FSMStateLinksData<Key> configData, EmptyFSMStateDatabaseCustomState<State, Key> stateDatabase) : base(configData, stateDatabase)
    {
    }
}