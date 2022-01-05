public class NoKeyFSMTransitionConfig<Data> : DefaultFSMTransitionsConfig<IFSMState<Data>, Data> where Data : AbstractFSMData
{
    public NoKeyFSMTransitionConfig(AbstractFSMStateDatabase<IFSMState<Data>, Data> fSMStateDatabase) : base(fSMStateDatabase)
    {
    }

    public NoKeyFSMTransitionConfig(FSMStateLinksData<IFSMState<Data>> configData, AbstractFSMStateDatabase<IFSMState<Data>, Data> stateDatabase) : base(configData, stateDatabase)
    {
    }
}

/*public class NoKeyFSMTransitionConfigCustomState<State, Data> : DefaultFSMTransitionsConfigCustomState<NoKeyFSMStateDatabaseCustomState<State, Data>, State, State, Data> where Data : AbstractFSMData where State : IFSMState<Data>
{
    public NoKeyFSMTransitionConfigCustomState(NoKeyFSMStateDatabaseCustomState<State, Data> stateDatabase) : base(stateDatabase)
    {
    }

    public NoKeyFSMTransitionConfigCustomState(FSMStateLinksData<State> configData, NoKeyFSMStateDatabaseCustomState<State, Data> stateDatabase) : base(configData, stateDatabase)
    {
    }
}*/
