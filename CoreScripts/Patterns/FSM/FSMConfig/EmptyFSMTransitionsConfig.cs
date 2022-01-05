public class EmptyFSMTransitionsConfig<Key> : EmptyFSMTransitionsConfigCustomState<IFSMState<EmptyFSMStateData>, Key>
{
    public EmptyFSMTransitionsConfig(EmptyFSMStateDatabaseCustomState<IFSMState<EmptyFSMStateData>, Key> stateDatabase) : base(stateDatabase)
    {
    }

    public EmptyFSMTransitionsConfig(FSMStateLinksData<Key> configData, EmptyFSMStateDatabaseCustomState<IFSMState<EmptyFSMStateData>, Key> stateDatabase) : base(configData, stateDatabase)
    {
    }
}

public class EmptyFSMTransitionsConfigCustomState<State, Key> : DefaultFSMTransitionsConfigCustomState<EmptyFSMStateDatabaseCustomState<State, Key>, State, Key, EmptyFSMStateData> where State : IFSMState<EmptyFSMStateData>
{
    public EmptyFSMTransitionsConfigCustomState(EmptyFSMStateDatabaseCustomState<State, Key> stateDatabase) : base(stateDatabase)
    {
    }

    public EmptyFSMTransitionsConfigCustomState(FSMStateLinksData<Key> configData, EmptyFSMStateDatabaseCustomState<State, Key> stateDatabase) : base(configData, stateDatabase)
    {
    }
}