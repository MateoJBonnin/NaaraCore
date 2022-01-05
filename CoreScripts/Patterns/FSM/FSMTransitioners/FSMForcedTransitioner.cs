public class FSMForcedTransitioner<Key, Data> : AbstractFSMTransitioner<Key, Data> where Data : AbstractFSMData
{
    protected AbstractFSMStateDatabase<Key, Data> stateDatabase;

    public FSMForcedTransitioner(AbstractFSMStateDatabase<Key, Data> stateDatabase)
    {
        this.stateDatabase = stateDatabase;
    }


    public override IFSMState<Data> TransitionateState(Key fromState, Key toState)
    {
        return this.stateDatabase.GetStateByType(toState);
    }
}

public class FSMForcedTransitionerCustomState<R, T, W> : AbstractFSMTransitionerCustomState<R, T, W> where W : AbstractFSMData where R : IFSMState<W>
{
    protected AbstractFSMStateDatabaseCustomState<R, T, W> stateDatabase;

    public FSMForcedTransitionerCustomState(AbstractFSMStateDatabaseCustomState<R, T, W> stateDatabase)
    {
        this.stateDatabase = stateDatabase;
    }

    public override R TransitionateState(T fromState, T toState)
    {
        return this.stateDatabase.GetStateByType(toState);
    }
}
