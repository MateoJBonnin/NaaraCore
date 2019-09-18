using System.Collections.Generic;

public class DefaultFSMTransitionsConfig<T, W> : AbstractFSMTransitionsConfig<T, W> where W : AbstractFSMData
{
    protected GenericDatabaseStructure<T, T> TransitionsDatabase { get; private set; }
    protected override AbstractFSMStateDatabase<T, W> FSMStateDatabase { get; set; }

    public DefaultFSMTransitionsConfig(AbstractFSMStateDatabase<T, W> stateDatabase)
    {
        this.Init(stateDatabase);
    }

    public DefaultFSMTransitionsConfig(FSMStateLinksData<T> configData, AbstractFSMStateDatabase<T, W> stateDatabase)
    {
        this.Init(stateDatabase);
        this.ConfigureConnections(configData);
    }

    public void FSMConfigUtil<R>(FSMStateLinksData<T> configData, AbstractFSMStateDatabase<T, W> stateDatabase) where R : FSMState<W>
    {
        this.Init(stateDatabase);
        this.ConfigureConnections(configData);
    }

    public void Init(AbstractFSMStateDatabase<T, W> stateDatabase)
    {
        this.TransitionsDatabase = new GenericDatabaseStructure<T, T>();
        this.FSMStateDatabase = stateDatabase;
    }

    public override void ConfigureConnections(FSMStateLinksData<T> configData)
    {
        foreach (FSMStateLink<T> stateConn in configData.linksData)
            this.SetTransition(stateConn.stateFrom, stateConn.stateTo);
    }

    public override void SetTransition(T from, T to)
    {
        this.TransitionsDatabase.RegisterData(from, to);
    }

    public override void RemoveTransition(T from, T to)
    {
        this.TransitionsDatabase.RemoveData(from, to);
    }

    public override FSMState<W> GetStateFromTransition(T from, T to)
    {
        List<T> transitionsFrom = this.TransitionsDatabase.GetData(from);

        if (transitionsFrom != null && transitionsFrom.Contains(from))
            return this.FSMStateDatabase.GetStateByType(transitionsFrom.Find(state => state.Equals(to)));

        return null;
    }
}