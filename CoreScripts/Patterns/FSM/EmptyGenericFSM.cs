public class EmptyGenericFSM<T> : GenericFSM<T, EmptyFSMStateData>
{
    public EmptyGenericFSM(AbstractFSMStateDatabase<T, EmptyFSMStateData> fSMStateDatabase, AbstractFSMTransitioner<T, EmptyFSMStateData> fSMTransitioner) :
    base(fSMStateDatabase, fSMTransitioner)
    {
    }
}