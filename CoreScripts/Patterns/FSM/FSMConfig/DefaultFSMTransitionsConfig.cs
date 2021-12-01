using System.Collections.Generic;

public class DefaultFSMTransitionsConfig<Key, Data> : DefaultFSMTransitionsConfigCustomState<AbstractFSMStateDatabase<Key, Data>, FSMState<Data>, Key, Data>
    where Data : AbstractFSMData
{
    public DefaultFSMTransitionsConfig(AbstractFSMStateDatabase<Key, Data> stateDatabase) : base(stateDatabase)
    {
    }

    public DefaultFSMTransitionsConfig(FSMStateLinksData<Key> configData, AbstractFSMStateDatabase<Key, Data> stateDatabase) : base(configData, stateDatabase)
    {
    }
}

public class DefaultFSMTransitionsConfigCustomState<Database, State, Key, Data> : AbstractFSMTransitionsConfigCustomState<Database, State, Key, Data>
    where Data : AbstractFSMData where State : FSMState<Data> where Database : AbstractFSMStateDatabaseCustomState<State, Key, Data>
{
    protected GenericDatabase<Key, Key> TransitionsDatabase
    {
        get;
        private set;
    }

    protected override Database FSMStateDatabase
    {
        get;
        set;
    }

    public DefaultFSMTransitionsConfigCustomState(Database stateDatabase)
    {
        this.Init(stateDatabase);
    }

    public DefaultFSMTransitionsConfigCustomState(FSMStateLinksData<Key> configData, Database stateDatabase)
    {
        this.Init(stateDatabase);
        this.ConfigureConnections(configData);
    }

    public void FSMConfigUtil<R>(FSMStateLinksData<R> configData, Database stateDatabase) where R : Key
    {
        this.Init(stateDatabase);
        this.ConfigureConnections(configData);
    }

    public void Init(Database stateDatabase)
    {
        this.TransitionsDatabase = new GenericDatabase<Key, Key>();
        this.FSMStateDatabase = stateDatabase;
    }

    public override void ConfigureConnections(FSMStateLinksData<Key> configData)
    {
        foreach (FSMStateLink<Key> stateConn in configData.linksData)
            this.SetTransition(stateConn.stateFrom, stateConn.stateTo);
    }

    public void ConfigureConnections<R>(FSMStateLinksData<R> configData) where R : Key
    {
        foreach (FSMStateLink<R> stateConn in configData.linksData)
            this.SetTransition(stateConn.stateFrom, stateConn.stateTo);
    }

    public override void SetTransition(Key from, Key to)
    {
        this.TransitionsDatabase.AddData(from, to);
    }

    public override void RemoveTransition(Key from, Key to)
    {
        this.TransitionsDatabase.RemoveData(from, to);
    }

    public override State GetStateFromTransition(Key from, Key to)
    {
        List<Key> transitionsFrom = this.TransitionsDatabase.GetData(from);

        if (transitionsFrom != null && transitionsFrom.Contains(to))
        {
            return this.FSMStateDatabase.GetStateByType(transitionsFrom.Find(state => state.Equals(to)));
        }

        return null;
    }
}
