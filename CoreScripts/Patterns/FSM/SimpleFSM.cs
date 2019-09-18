public class SimpleFSM<T, W> : GenericFSM<T, W> where W : AbstractFSMData
{
    public SimpleFSM(AbstractFSMStateDatabase<T, W> fSMStateDatabase) : base(fSMStateDatabase, new FSMForcedTransitioner<T, W>(fSMStateDatabase))
    {
    }
}