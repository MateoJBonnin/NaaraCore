public abstract class AbstractStateSaveManager<T> : AbstractGameplayManager
{
    public abstract T GetStateSnapshot();
}