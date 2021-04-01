public abstract class AbstractStateSaveManager<T> where T : IStateSnapshot
{
    public abstract T GetStateSnapshot();
}
