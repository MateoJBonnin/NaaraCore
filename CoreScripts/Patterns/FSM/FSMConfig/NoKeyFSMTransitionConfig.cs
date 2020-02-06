public class NoKeyFSMTransitionConfig<Data> : DefaultFSMTransitionsConfig<FSMState<Data>, Data> where Data : AbstractFSMData
{
    public NoKeyFSMTransitionConfig(AbstractFSMStateDatabase<FSMState<Data>, Data> fSMStateDatabase) : base(fSMStateDatabase)
    {
    }

    public NoKeyFSMTransitionConfig(FSMStateLinksData<FSMState<Data>> configData, AbstractFSMStateDatabase<FSMState<Data>, Data> stateDatabase) : base(configData, stateDatabase)
    {
    }
}

public class NoKeyFSMTransitionConfigCustomState<State, Data> : DefaultFSMTransitionsConfigCustomState<NoKeyFSMStateDatabaseCustomState<State, Data>, State, State, Data> where Data : AbstractFSMData where State : FSMState<Data>
{
    public NoKeyFSMTransitionConfigCustomState(NoKeyFSMStateDatabaseCustomState<State, Data> stateDatabase) : base(stateDatabase)
    {
    }

    public NoKeyFSMTransitionConfigCustomState(FSMStateLinksData<State> configData, NoKeyFSMStateDatabaseCustomState<State, Data> stateDatabase) : base(configData, stateDatabase)
    {
    }
}