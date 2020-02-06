public class SimpleNoKeyFSM<Data> : SimpleNoKeyFSMCustomState<FSMState<Data>, Data> where Data : AbstractFSMData
{
    public SimpleNoKeyFSM(NoKeyFSMStateDatabaseCustomState<FSMState<Data>, Data> fSMStateDatabase) : base(fSMStateDatabase)
    {
    }
}

public class SimpleNoKeyFSMCustomState<State, Data> : SimpleFSMCustomState<NoKeyFSMStateDatabaseCustomState<State, Data>, State, State, Data> where Data : AbstractFSMData where State : FSMState<Data>
{
    public SimpleNoKeyFSMCustomState(NoKeyFSMStateDatabaseCustomState<State, Data> fSMStateDatabase) : base(fSMStateDatabase)
    {
    }
}