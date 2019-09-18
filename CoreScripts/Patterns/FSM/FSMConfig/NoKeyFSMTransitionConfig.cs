public class NoKeyFSMTransitionConfig<T> : DefaultFSMTransitionsConfig<FSMState<T>, T> where T : AbstractFSMData
{
    public NoKeyFSMTransitionConfig(AbstractFSMStateDatabase<FSMState<T>, T> fSMStateDatabase) : base(fSMStateDatabase)
    {
    }
}