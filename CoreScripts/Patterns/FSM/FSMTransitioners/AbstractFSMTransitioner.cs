public abstract class AbstractFSMTransitioner<Key, Data> : AbstractFSMTransitionerCustomState<IFSMState<Data>, Key, Data> where Data : AbstractFSMData
{
}

public abstract class AbstractFSMTransitionerCustomState<State, Key, Data> where Data : AbstractFSMData where State : IFSMState<Data>
{
    public abstract State TransitionateState(Key fromState, Key toState);
}