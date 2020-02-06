public class AutoInitGameplayManagerContainer : AbstractGameplayManagerContainer
{
    public AutoInitGameplayManagerContainer(GameplayManager manager) : base(manager)
    {
        this.SetState(ManagerReadyStates.Ready);
    }
}

public class AutoInitGameplayManagerContainer<T> : AbstractGameplayManagerContainer<T> where T : class, GameplayManager
{
    public AutoInitGameplayManagerContainer(T manager) : base(manager)
    {
        this.SetState(ManagerReadyStates.Ready);
    }
}