public class NoKeyGenericFSM<Data> : GenericFSMCustomState<NoKeyFSMStateDatabaseCustomState<Data>,
    AbstractFSMTransitionerCustomState<IFSMState<Data>, IFSMState<Data>, Data>, IFSMState<Data>,
    IFSMState<Data>, Data> where Data : AbstractFSMData
{
    public NoKeyGenericFSM(NoKeyFSMStateDatabaseCustomState<Data> database, AbstractFSMTransitionerCustomState<IFSMState<Data>, IFSMState<Data>, Data> transitioner) : base(database, transitioner)
    {
    }
}

/*public class NoKeyGenericFSMCustomState<State, Data> : GenericFSMCustomState<NoKeyFSMStateDatabaseCustomState<Data>, AbstractFSMTransitionerCustomState<State, State, Data>, State, State, Data>
    where Data : AbstractFSMData where State : IFSMState<Data>
{
    public NoKeyGenericFSMCustomState(NoKeyFSMStateDatabaseCustomState<State, Data> database, AbstractFSMTransitionerCustomState<State, State, Data> transitioner) : base(database, transitioner)
    {
    }
}*/
