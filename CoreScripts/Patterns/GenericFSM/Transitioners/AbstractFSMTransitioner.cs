public abstract class AbstractFSMTransitioner<T, W> where W : AbstractFSMData
{
    protected FSMConfig<T, W> AbstractFSMConfig { get; set; }

    public AbstractFSMTransitioner(FSMConfig<T, W> fSMConfig)
    {
        this.AbstractFSMConfig = fSMConfig;
    }

    public abstract FSMState<W> TransitionateState(T fromState, T toState);
}
