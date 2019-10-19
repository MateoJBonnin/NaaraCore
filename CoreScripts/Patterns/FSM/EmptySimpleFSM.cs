public class EmptySimpleFSM<T> : SimpleFSM<T, EmptyFSMStateData>
{
    public EmptySimpleFSM(AbstractFSMStateDatabase<T, EmptyFSMStateData> fSMStateDatabase) : base(fSMStateDatabase)
    {
    }
}