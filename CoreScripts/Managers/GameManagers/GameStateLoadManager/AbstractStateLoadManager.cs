public abstract class AbstractStateLoadManager<T> : AbstractApplicationManager where T : StateSnapshot
{
    public abstract void Load(T stateSnapshot);
}