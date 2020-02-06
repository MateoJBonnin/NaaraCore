public abstract class AbstractFSMTransitionsConfig<Key, Data> : AbstractFSMTransitionsConfigCustomState<AbstractFSMStateDatabase<Key, Data>, FSMState<Data>, Key, Data> where Data : AbstractFSMData
{
}

public abstract class AbstractFSMTransitionsConfigCustomState<Database, State, Key, Data> where Data : AbstractFSMData where State : FSMState<Data> where Database : AbstractFSMStateDatabaseCustomState<State, Key, Data>
{
    protected abstract Database FSMStateDatabase { get; set; }

    public abstract void ConfigureConnections(FSMStateLinksData<Key> configData);
    public abstract void RemoveTransition(Key from, Key to);
    public abstract void SetTransition(Key from, Key to);
    public abstract State GetStateFromTransition(Key from, Key to);

    public static FSMStateLink<Key> StateToConfig(Key from, Key to)
    {
        FSMStateLink<Key> config = new FSMStateLink<Key>(from, to);
        return config;
    }
}