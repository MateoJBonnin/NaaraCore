public class SimpleNoKeyFSM<T> : SimpleFSM<FSMState<T>, T> where T : AbstractFSMData
{
    public SimpleNoKeyFSM(AbstractFSMStateDatabase<FSMState<T>, T> fSMStateDatabase) : base(fSMStateDatabase)
    {
    }
}