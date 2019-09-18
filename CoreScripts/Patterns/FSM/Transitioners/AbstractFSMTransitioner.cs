public abstract class AbstractFSMTransitioner<T, W> where W : AbstractFSMData
{
    public abstract FSMState<W> TransitionateState(T fromState, T toState);
}