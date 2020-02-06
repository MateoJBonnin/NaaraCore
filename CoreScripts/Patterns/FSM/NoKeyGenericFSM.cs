public class NoKeyGenericFSM<Data> : GenericFSMCustomState<NoKeyFSMStateDatabaseCustomState<FSMState<Data>, Data>, AbstractFSMTransitionerCustomState<FSMState<Data>, FSMState<Data>, Data>, FSMState<Data>, FSMState<Data>, Data> where Data : AbstractFSMData
{
    public NoKeyGenericFSM(NoKeyFSMStateDatabaseCustomState<FSMState<Data>, Data> database, AbstractFSMTransitionerCustomState<FSMState<Data>, FSMState<Data>, Data> transitioner) : base(database, transitioner)
    {
    }
}

public class NoKeyGenericFSMCustomState<State, Data> : GenericFSMCustomState<NoKeyFSMStateDatabaseCustomState<State, Data>, AbstractFSMTransitionerCustomState<State, State, Data>, State, State, Data> where Data : AbstractFSMData where State : FSMState<Data>
{
    public NoKeyGenericFSMCustomState(NoKeyFSMStateDatabaseCustomState<State, Data> database, AbstractFSMTransitionerCustomState<State, State, Data> transitioner) : base(database, transitioner)
    {
    }
}