public abstract class AbstractStateLoadManager<T> where T : IStateSnapshot
{
    public abstract void Load(T stateSnapshot);
}
