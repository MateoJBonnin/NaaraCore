public class SimpleFSM<T, W> : GenericFSM<T, W> where W : AbstractFSMData
{
    public SimpleFSM(AbstractFSMStateDatabase<T, W> fSMStateDatabase) : base(fSMStateDatabase, new FSMForcedTransitioner<T, W>(fSMStateDatabase))
    {
    }
}

public class SimpleFSMCustomState<Database, State, Key, Data> : GenericFSMCustomState<Database, AbstractFSMTransitionerCustomState<State, Key, Data>, State, Key, Data> where Data : AbstractFSMData where State : FSMState<Data> where Database : AbstractFSMStateDatabaseCustomState<State, Key, Data>
{
    public SimpleFSMCustomState(Database fSMStateDatabase) : base(fSMStateDatabase, new FSMForcedTransitionerCustomState<State, Key, Data>(fSMStateDatabase))
    {
    }
}