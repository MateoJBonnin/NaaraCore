public class FSMRestrictedTransitioner<T, W> : AbstractFSMTransitioner<T, W> where W : AbstractFSMData
{
    protected AbstractFSMTransitionsConfig<T, W> abstractFSMConfig;

    public FSMRestrictedTransitioner(AbstractFSMTransitionsConfig<T, W> abstractFSMConfig)
    {
        this.abstractFSMConfig = abstractFSMConfig;
    }

    public override FSMState<W> TransitionateState(T fromState, T toState)
    {
        return abstractFSMConfig.GetStateFromTransition(fromState, toState);
    }
}
