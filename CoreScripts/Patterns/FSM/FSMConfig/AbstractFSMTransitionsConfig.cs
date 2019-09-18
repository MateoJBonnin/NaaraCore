public abstract class AbstractFSMTransitionsConfig<T, W> where W : AbstractFSMData
{
    protected abstract AbstractFSMStateDatabase<T, W> FSMStateDatabase { get; set; }

    public abstract void ConfigureConnections(FSMStateLinksData<T> configData);
    public abstract void RemoveTransition(T from, T to);
    public abstract void SetTransition(T from, T to);
    public abstract FSMState<W> GetStateFromTransition(T from, T to);

    public static FSMStateLink<T> StateToConfig(T from, T to)
    {
        FSMStateLink<T> config = new FSMStateLink<T>(from, to);
        return config;
    }
}