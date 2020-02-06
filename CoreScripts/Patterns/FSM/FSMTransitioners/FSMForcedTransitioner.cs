using System;

public class FSMForcedTransitioner<T, W> : AbstractFSMTransitioner<T, W> where W : AbstractFSMData
{
    protected AbstractFSMStateDatabase<T, W> stateDatabase;

    public FSMForcedTransitioner(AbstractFSMStateDatabase<T, W> stateDatabase)
    {
        this.stateDatabase = stateDatabase;
    }

    public override FSMState<W> TransitionateState(T fromState, T toState)
    {
        return this.stateDatabase.GetStateByType(toState);
    }
}

public class FSMForcedTransitionerCustomState<R, T, W> : AbstractFSMTransitionerCustomState<R, T, W> where W : AbstractFSMData where R : FSMState<W>
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