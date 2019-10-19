public class EmptyFSMTransitionsConfig<T> : DefaultFSMTransitionsConfig<T, EmptyFSMStateData>
{
    public EmptyFSMTransitionsConfig(AbstractFSMStateDatabase<T, EmptyFSMStateData> stateDatabase) : base(stateDatabase)
    {
    }

    public EmptyFSMTransitionsConfig(FSMStateLinksData<T> configData, AbstractFSMStateDatabase<T, EmptyFSMStateData> stateDatabase) : base(configData, stateDatabase)
    {
    }
}