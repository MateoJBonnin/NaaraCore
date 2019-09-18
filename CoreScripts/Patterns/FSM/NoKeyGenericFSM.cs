public class NoKeyGenericFSM<T> : GenericFSM<FSMState<T>, T> where T : AbstractFSMData
{
    public NoKeyGenericFSM(NoKeyFSMStateDatabase<T> fSMStateDatabase, AbstractFSMTransitioner<FSMState<T>, T> fSMTransitioner) :
        base(fSMStateDatabase, fSMTransitioner)
    {
    }
}