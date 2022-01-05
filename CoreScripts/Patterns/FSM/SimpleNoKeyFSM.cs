public class SimpleNoKeyFSM<Data> : SimpleFSM<IFSMState<Data>, Data> where Data : AbstractFSMData
{
    public SimpleNoKeyFSM(NoKeyFSMStateDatabaseCustomState<Data> fSMStateDatabase) : base(fSMStateDatabase)
    {
    }
}

/*
public class SimpleNoKeyFSMCustomState<State, Data> : SimpleFSMCustomState<NoKeyFSMStateDatabase<Data>, State, State, Data> where Data : AbstractFSMData where State : IFSMState<Data>
{
    public SimpleNoKeyFSMCustomState(NoKeyFSMStateDatabaseCustomState<Data> fSMStateDatabase) : base(fSMStateDatabase)
    {
    }
}*/
