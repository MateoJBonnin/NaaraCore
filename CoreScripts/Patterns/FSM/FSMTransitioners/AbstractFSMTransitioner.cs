public abstract class AbstractFSMTransitioner<Key, Data> : AbstractFSMTransitionerCustomState<FSMState<Data>, Key, Data> where Data : AbstractFSMData
{
}

public abstract class AbstractFSMTransitionerCustomState<State, Key, Data> where Data : AbstractFSMData where State : FSMState<Data>
{
    public abstract State TransitionateState(Key fromState, Key toState);
}